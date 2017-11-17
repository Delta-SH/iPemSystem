using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class H_NoticeInUserRepository : IH_NoticeInUserRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_NoticeInUserRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_NoticeInUser> GetNoticesInUsers() {
            var entities = new List<H_NoticeInUser>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_NoticeInUser_Repository_GetNoticesInUsers, null)) {
                while(rdr.Read()) {
                    var entity = new H_NoticeInUser();
                    entity.NoticeId = SqlTypeConverter.DBNullGuidHandler(rdr["NoticeId"]);
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.Readed = SqlTypeConverter.DBNullBooleanHandler(rdr["Readed"]);
                    entity.ReadTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReadTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_NoticeInUser> GetNoticesInUser(Guid uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);

            var entities = new List<H_NoticeInUser>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_NoticeInUser_Repository_GetNoticesInUser, parms)) {
                while(rdr.Read()) {
                    var entity = new H_NoticeInUser();
                    entity.NoticeId = SqlTypeConverter.DBNullGuidHandler(rdr["NoticeId"]);
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.Readed = SqlTypeConverter.DBNullBooleanHandler(rdr["Readed"]);
                    entity.ReadTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReadTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<H_NoticeInUser> entities) {
            SqlParameter[] parms = { new SqlParameter("@NoticeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Readed", SqlDbType.Bit),
                                     new SqlParameter("@ReadTime", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.NoticeId);
                        parms[1].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        parms[2].Value = entity.Readed;
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ReadTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_NoticeInUser_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<H_NoticeInUser> entities) {
            SqlParameter[] parms = { new SqlParameter("@NoticeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Readed", SqlDbType.Bit),
                                     new SqlParameter("@ReadTime", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.NoticeId);
                        parms[1].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        parms[2].Value = entity.Readed;
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ReadTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_NoticeInUser_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<H_NoticeInUser> entities) {
            SqlParameter[] parms = { new SqlParameter("@NoticeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.NoticeId);
                        parms[1].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_NoticeInUser_Repository_Delete, parms);
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
