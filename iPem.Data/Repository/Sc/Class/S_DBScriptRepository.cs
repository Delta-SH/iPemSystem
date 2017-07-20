using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class S_DBScriptRepository : IS_DBScriptRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public S_DBScriptRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<S_DBScript> GetEntities() {
            var entities = new List<S_DBScript>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_DBScript_Repository_GetEntities, null)) {
                while (rdr.Read()) {
                    var entity = new S_DBScript();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["ID"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["CreateUser"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreateTime"]);
                    entity.Executor = SqlTypeConverter.DBNullStringHandler(rdr["ExecuteUser"]);
                    entity.ExecutedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ExecuteTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Desc"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<S_DBScript> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@Creator", SqlDbType.VarChar,200),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Executor", SqlDbType.VarChar,200),
                                     new SqlParameter("@ExecutedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Executor);
                        parms[5].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ExecutedTime);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_DBScript_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<S_DBScript> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@Creator", SqlDbType.VarChar,200),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Executor", SqlDbType.VarChar,200),
                                     new SqlParameter("@ExecutedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Executor);
                        parms[5].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ExecutedTime);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_DBScript_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<string> ids) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var id in ids) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_DBScript_Repository_Delete, parms);
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
