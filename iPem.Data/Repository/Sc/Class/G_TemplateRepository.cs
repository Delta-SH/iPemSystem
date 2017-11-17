using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class G_TemplateRepository : IG_TemplateRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public G_TemplateRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public G_Template GetEntity(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(name);

            G_Template entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_GetEntity, parms)) {
                if (rdr.Read()) {
                    entity = new G_Template();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
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
                var cnt = SqlHelper.ExecuteScalar(conn, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_ExistEntity, parms);
                if (cnt != null && cnt != DBNull.Value) { result = Convert.ToInt32(cnt); }
            }

            return result > 0;
        }

        public List<G_Template> GetEntities() {
            var entities = new List<G_Template>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new G_Template();
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<String> GetNames() {
            var entities = new List<String>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_GetNames, null)) {
                while (rdr.Read()) {
                    entities.Add(SqlTypeConverter.DBNullStringHandler(rdr["Name"]));
                }
            }
            return entities;
        }

        public void Insert(IList<G_Template> entities) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Content", SqlDbType.Text),
                                     new SqlParameter("@Remark", SqlDbType.VarChar, 512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<G_Template> entities) {
            SqlParameter[] parms = { new SqlParameter("@Name", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Content", SqlDbType.Text),
                                     new SqlParameter("@Remark", SqlDbType.VarChar, 512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_Update, parms);
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
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_Delete, parms);
                    }
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
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_G_Template_Repository_Clear, null);
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
