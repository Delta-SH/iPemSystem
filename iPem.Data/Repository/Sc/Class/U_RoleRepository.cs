using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class U_RoleRepository : IU_RoleRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public U_RoleRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public U_Role GetRoleById(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            U_Role entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_GetRoleById, parms)) {
                if (rdr.Read()) {
                    entity = new U_Role();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Config = SqlTypeConverter.DBNullInt32Handler(rdr["Config"]);
                    entity.ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]);
                }
            }
            return entity;
        }

        public U_Role GetRoleByName(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);

            U_Role entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_GetRoleByName, parms)) {
                if (rdr.Read()) {
                    entity = new U_Role();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Config = SqlTypeConverter.DBNullInt32Handler(rdr["Config"]);
                    entity.ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]);
                }
            }
            return entity;
        }

        public U_Role GetRoleByUid(string uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(uid);

            U_Role entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_GetRoleByUid, parms)) {
                if (rdr.Read()) {
                    entity = new U_Role();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Config = SqlTypeConverter.DBNullInt32Handler(rdr["Config"]);
                    entity.ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]);
                }
            }
            return entity;
        }

        public List<U_Role> GetRoles() {
            var entities = new List<U_Role>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_GetRoles, null)) {
                while (rdr.Read()) {
                    var entity = new U_Role();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Config = SqlTypeConverter.DBNullInt32Handler(rdr["Config"]);
                    entity.ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<U_Role> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@Type",SqlDbType.Int),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit),
                                     new SqlParameter("@Config",SqlDbType.Int),
                                     new SqlParameter("@ValuesJson",SqlDbType.NText)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = entity.Type;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[4].Value = entity.Enabled;
                        parms[5].Value = entity.Config;
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.ValuesJson);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<U_Role> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@Type",SqlDbType.Int),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit),
                                     new SqlParameter("@Config",SqlDbType.Int),
                                     new SqlParameter("@ValuesJson",SqlDbType.NText)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = entity.Type;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[4].Value = entity.Enabled;
                        parms[5].Value = entity.Config;
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.ValuesJson);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<U_Role> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_Role_Repository_Delete, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion

    }
}