using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using iPem.Core.Enum;

namespace iPem.Core.Data {
    public class SqlDbManager : IDbManager {

        #region Fields

        private readonly IDataProvider _dataProvider;

        #endregion

        #region Ctor

        public SqlDbManager(IDataProvider dataProvider) {
            if (dataProvider == null)
                throw new ArgumentNullException("dataProvider");

            this._dataProvider = dataProvider;
            this.CurrentDbSets = new Dictionary<EnmDbType, DbEntity>();
            this.CurrentConnetions = new Dictionary<EnmDbType, string>();
            this.Initializer();
        }

        #endregion

        #region Utilities

        protected virtual string[] ParseCommands(string filePath, bool throwExceptionIfNonExists) {
            if (!File.Exists(filePath)) {
                if (throwExceptionIfNonExists)
                    throw new ArgumentException(string.Format("Specified file doesn't exist - {0}", filePath));

                return new string[0];
            }


            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream)) {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null) {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
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

        protected virtual string CreateConnectionString(DbEntity entity) {
            var builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = false;
            builder.DataSource = String.Format("{0},{1}", entity.IP, entity.Port);
            builder.InitialCatalog = entity.Name;
            builder.UserID = entity.Uid;
            builder.Password = entity.Password;
            builder.PersistSecurityInfo = false;
            builder.MultipleActiveResultSets = true;
            builder.ConnectTimeout = 120;
            return builder.ConnectionString;
        }

        #endregion

        #region Methods

        public virtual void Initializer() {
            foreach(var entity in this._dataProvider.GetEntites()) {
                this.CurrentDbSets[entity.Type] = entity;
            }

            foreach(var entity in this.CurrentDbSets.Values) {
                this.CurrentConnetions[entity.Type] = CreateConnectionString(entity);
            }
        }

        public virtual void Clean() {
            this.CurrentDbSets.Clear();
            this.CurrentConnetions.Clear();
        }

        public virtual bool DatabaseIsInstalled() {
            if(!IsValid(EnmDbType.Rs))
                return false;

            if(!IsValid(EnmDbType.Cs))
                return false;

            if(!IsValid(EnmDbType.Sc))
                return false;

            return true;
        }

        public virtual bool IsValid(EnmDbType dt) {
            return CurrentDbSets.ContainsKey(dt) && CurrentConnetions.ContainsKey(dt);
        }

        #endregion

        #region Properties

        public virtual Dictionary<EnmDbType, DbEntity> CurrentDbSets { get; private set; }

        public virtual Dictionary<EnmDbType, string> CurrentConnetions { get; private set; }

        #endregion

    }
}
