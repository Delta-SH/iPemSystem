using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Security.AccessControl;
using iPem.Core;
using iPem.Data.Common;

namespace iPem.Data.Installation {
    public partial class DbInstaller : IDbInstaller {
        #region Utilities

        protected virtual void ExecuteSqlFile(string path, string connectionString) {
            var statements = new List<string>();

            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream)) {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null) {
                    statements.Add(statement);
                }
            }

            ExecuteCommands(statements, connectionString);
        }

        protected virtual string ReadNextStatementFromStream(StreamReader reader) {
            var sb = new StringBuilder();
            while (true) {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null) {
                    if (sb.Length > 0)
                        return sb.ToString();

                    return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }

        protected virtual void ExecuteCommands(IList<string> cmds, string connectionString) {
            using(var conn = new SqlConnection(connectionString)) {
                foreach (var cmd in cmds) {
                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, cmd, null);
                }
            }
        }

        protected virtual bool CheckPermission(string path, bool checkRead, bool checkWrite, bool checkModify, bool checkDelete) {
            var flag = false;
            var flag2 = false;
            var flag3 = false;
            var flag4 = false;
            var flag5 = false;
            var flag6 = false;
            var flag7 = false;
            var flag8 = false;
            var current = WindowsIdentity.GetCurrent();
            AuthorizationRuleCollection rules = null;
            try {
                rules = Directory.GetAccessControl(path).GetAccessRules(true, true, typeof(SecurityIdentifier));
            } catch {
                return true;
            }

            try {
                foreach(FileSystemAccessRule rule in rules) {
                    if(!current.User.Equals(rule.IdentityReference)) { continue; }
                    if(AccessControlType.Deny.Equals(rule.AccessControlType)) {
                        if((FileSystemRights.Delete & rule.FileSystemRights) == FileSystemRights.Delete) { flag4 = true; }
                        if((FileSystemRights.Modify & rule.FileSystemRights) == FileSystemRights.Modify) { flag3 = true; }
                        if((FileSystemRights.Read & rule.FileSystemRights) == FileSystemRights.Read) { flag = true; }
                        if((FileSystemRights.Write & rule.FileSystemRights) == FileSystemRights.Write) { flag2 = true; }
                        continue;
                    }

                    if(AccessControlType.Allow.Equals(rule.AccessControlType)) {
                        if((FileSystemRights.Delete & rule.FileSystemRights) == FileSystemRights.Delete) { flag8 = true; }
                        if((FileSystemRights.Modify & rule.FileSystemRights) == FileSystemRights.Modify) { flag7 = true; }
                        if((FileSystemRights.Read & rule.FileSystemRights) == FileSystemRights.Read) { flag5 = true; }
                        if((FileSystemRights.Write & rule.FileSystemRights) == FileSystemRights.Write) { flag6 = true; }
                    }
                }

                foreach(IdentityReference reference in current.Groups) {
                    foreach(FileSystemAccessRule rule2 in rules) {
                        if(!reference.Equals(rule2.IdentityReference)) { continue; }
                        if(AccessControlType.Deny.Equals(rule2.AccessControlType)) {
                            if((FileSystemRights.Delete & rule2.FileSystemRights) == FileSystemRights.Delete) { flag4 = true; }
                            if((FileSystemRights.Modify & rule2.FileSystemRights) == FileSystemRights.Modify) { flag3 = true; }
                            if((FileSystemRights.Read & rule2.FileSystemRights) == FileSystemRights.Read) { flag = true; }
                            if((FileSystemRights.Write & rule2.FileSystemRights) == FileSystemRights.Write) { flag2 = true; }
                            continue;
                        }

                        if(AccessControlType.Allow.Equals(rule2.AccessControlType)) {
                            if((FileSystemRights.Delete & rule2.FileSystemRights) == FileSystemRights.Delete) { flag8 = true; }
                            if((FileSystemRights.Modify & rule2.FileSystemRights) == FileSystemRights.Modify) { flag7 = true; }
                            if((FileSystemRights.Read & rule2.FileSystemRights) == FileSystemRights.Read) { flag5 = true; }
                            if((FileSystemRights.Write & rule2.FileSystemRights) == FileSystemRights.Write) { flag6 = true; }
                        }
                    }
                }

                var flag9 = !flag4 && flag8;
                var flag10 = !flag3 && flag7;
                var flag11 = !flag && flag5;
                var flag12 = !flag2 && flag6;
                var flag13 = true;
                if(checkRead) { flag13 = flag13 && flag11; }
                if(checkWrite) { flag13 = flag13 && flag12; }
                if(checkModify) { flag13 = flag13 && flag10; }
                if(checkDelete) { flag13 = flag13 && flag9; }
                return flag13;
            } catch { }

            return false;
        }

        #endregion

        #region Methods

        public virtual string CheckPermissions() {
            var result = new List<string>();
            var dirsToCheck = new List<string>();

            var rootDir = CommonHelper.MapPath("~/");
            dirsToCheck.Add(rootDir);
            dirsToCheck.Add(rootDir + "Resources\\install");
            dirsToCheck.Add(rootDir + "Resources\\install\\create");
            dirsToCheck.Add(rootDir + "Resources\\install\\upgrade");
            foreach(var dir in dirsToCheck) {
                if(!CheckPermission(dir, true, true, true, true)) {
                    result.Add(dir);
                }
            }

            var filesToCheck = new List<string>();
            filesToCheck.Add(rootDir + "Web.config");
            filesToCheck.Add(rootDir + "Registry.db");
            foreach(var file in filesToCheck) {
                if(!CheckPermission(file, true, true, true, true)) {
                    result.Add(file);
                }
            }

            return result.Count > 0 ? string.Join(";", result.ToArray()) : null;
        }

        public virtual void InstallDatabase(string connectionString, string databaseName, string filePath) {
            var cmds = String.Format(@"CREATE DATABASE [{0}]", databaseName);
            if(!String.IsNullOrWhiteSpace(filePath)) {
                filePath = filePath.Trim();
                if(!Directory.Exists(filePath)) { 
                    Directory.CreateDirectory(filePath); 
                }

                cmds = String.Format(@"CREATE DATABASE [{0}] ON PRIMARY (NAME = N'{0}', FILENAME = N'{1}\{0}.mdf') LOG ON (NAME = N'{0}_log', FILENAME = N'{1}\{0}_log.ldf')", databaseName, filePath);
            }

            ExecuteCommands(new List<string>() { cmds }, connectionString);
        }

        public virtual void InstallData(string connectionString, string filePath) {
            var fullPath = CommonHelper.MapPath(filePath);
            ExecuteSqlFile(fullPath, connectionString);
        }

        #endregion
    }
}