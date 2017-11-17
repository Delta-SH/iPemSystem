using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class G_PageRepository : IG_PageRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public G_PageRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public G_Page GetEntity(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);

            G_Page entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_GetEntity, parms)) {
                if (rdr.Read()) {
                    entity = new G_Page();
                    entity.RoleId = SqlTypeConverter.DBNullStringHandler(rdr["RoleId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.IsHome = SqlTypeConverter.DBNullBooleanHandler(rdr["IsHome"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.ObjId = SqlTypeConverter.DBNullStringHandler(rdr["ObjId"]);
                    entity.ObjType = SqlTypeConverter.DBNullInt32Handler(rdr["ObjType"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                }
            }
            return entity;
        }

        public Boolean ExistEntity(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);

            var result = 0;
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                var cnt = SqlHelper.ExecuteScalar(conn, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_ExistEntity, parms);
                if (cnt != null && cnt != DBNull.Value) { result = Convert.ToInt32(cnt); }
            }

            return result > 0;
        }

        public List<G_Page> GetEntities() {
            var entities = new List<G_Page>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new G_Page();
                    entity.RoleId = SqlTypeConverter.DBNullStringHandler(rdr["RoleId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.IsHome = SqlTypeConverter.DBNullBooleanHandler(rdr["IsHome"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.ObjId = SqlTypeConverter.DBNullStringHandler(rdr["ObjId"]);
                    entity.ObjType = SqlTypeConverter.DBNullInt32Handler(rdr["ObjType"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<G_Page> GetEntities(string role) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(role);

            var entities = new List<G_Page>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_GetEntitiesInRole, parms)) {
                while (rdr.Read()) {
                    var entity = new G_Page();
                    entity.RoleId = SqlTypeConverter.DBNullStringHandler(rdr["RoleId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.IsHome = SqlTypeConverter.DBNullBooleanHandler(rdr["IsHome"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.ObjId = SqlTypeConverter.DBNullStringHandler(rdr["ObjId"]);
                    entity.ObjType = SqlTypeConverter.DBNullInt32Handler(rdr["ObjType"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<G_Page> GetEntities(string role, string id, int type) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ObjId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ObjType", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(role);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[2].Value = SqlTypeConverter.DBNullInt32Checker(type);

            var entities = new List<G_Page>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_GetEntitiesInObj, parms)) {
                while(rdr.Read()) {
                    var entity = new G_Page();
                    entity.RoleId = SqlTypeConverter.DBNullStringHandler(rdr["RoleId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.IsHome = SqlTypeConverter.DBNullBooleanHandler(rdr["IsHome"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.ObjId = SqlTypeConverter.DBNullStringHandler(rdr["ObjId"]);
                    entity.ObjType = SqlTypeConverter.DBNullInt32Handler(rdr["ObjType"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<String> GetNames(string role, string id, int type) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ObjId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ObjType", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(role);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[2].Value = SqlTypeConverter.DBNullInt32Checker(type);

            var entities = new List<String>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_GetNamesInObj, parms)) {
                while (rdr.Read()) {
                    entities.Add(SqlTypeConverter.DBNullStringHandler(rdr["Name"]));
                }
            }
            return entities;
        }

        public void Insert(IList<G_Page> entities) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Name", SqlDbType.VarChar, 200),
                                     new SqlParameter("@IsHome", SqlDbType.Bit),
                                     new SqlParameter("@Content", SqlDbType.Text),
                                     new SqlParameter("@ObjId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ObjType", SqlDbType.Int),
                                     new SqlParameter("@Remark", SqlDbType.VarChar, 512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.RoleId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = entity.IsHome;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.ObjId);
                        parms[5].Value = SqlTypeConverter.DBNullInt32Checker(entity.ObjType);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<G_Page> entities) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Name", SqlDbType.VarChar, 200),
                                     new SqlParameter("@IsHome", SqlDbType.Bit),
                                     new SqlParameter("@Content", SqlDbType.Text),
                                     new SqlParameter("@ObjId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ObjType", SqlDbType.Int),
                                     new SqlParameter("@Remark", SqlDbType.VarChar, 512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.RoleId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = entity.IsHome;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.ObjId);
                        parms[5].Value = SqlTypeConverter.DBNullInt32Checker(entity.ObjType);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<string> names) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var name in names) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_Delete, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Clear(string role) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(role);

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_ClearInRole, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Clear() {
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Page_Repository_Clear, null);
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
