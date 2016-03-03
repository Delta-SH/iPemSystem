using iPem.Core.Domain.Master;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class NoticeInUserRepository : INoticeInUserRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoticeInUserRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual IList<NoticeInUser> GetEntities() {
            var entities = new List<NoticeInUser>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_NoticesInUsers_Repository_GetEntities1, null)) {
                while(rdr.Read()) {
                    var entity = new NoticeInUser();
                    entity.NoticeId = SqlTypeConverter.DBNullGuidHandler(rdr["NoticeId"]);
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.Readed = SqlTypeConverter.DBNullBooleanHandler(rdr["Readed"]);
                    entity.ReadTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReadTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual IList<NoticeInUser> GetEntities(Guid uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);

            var entities = new List<NoticeInUser>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_NoticesInUsers_Repository_GetEntities2, parms)) {
                while(rdr.Read()) {
                    var entity = new NoticeInUser();
                    entity.NoticeId = SqlTypeConverter.DBNullGuidHandler(rdr["NoticeId"]);
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.Readed = SqlTypeConverter.DBNullBooleanHandler(rdr["Readed"]);
                    entity.ReadTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReadTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual void Insert(NoticeInUser entity) {
            Insert(new List<NoticeInUser>() { entity });
        }

        public virtual void Insert(IList<NoticeInUser> entities) {
            SqlParameter[] parms = { new SqlParameter("@NoticeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Readed", SqlDbType.Bit),
                                     new SqlParameter("@ReadTime", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.NoticeId);
                        parms[1].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        parms[2].Value = entity.Readed;
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ReadTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_NoticesInUsers_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Update(NoticeInUser entity) {
            Update(new List<NoticeInUser>() { entity });
        }

        public virtual void Update(IList<NoticeInUser> entities) {
            SqlParameter[] parms = { new SqlParameter("@NoticeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Readed", SqlDbType.Bit),
                                     new SqlParameter("@ReadTime", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.NoticeId);
                        parms[1].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        parms[2].Value = entity.Readed;
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ReadTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_NoticesInUsers_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(NoticeInUser entity) {
            Delete(new List<NoticeInUser>() { entity });
        }

        public virtual void Delete(IList<NoticeInUser> entities) {
            SqlParameter[] parms = { new SqlParameter("@NoticeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.NoticeId);
                        parms[1].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_NoticesInUsers_Repository_Delete, parms);
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
