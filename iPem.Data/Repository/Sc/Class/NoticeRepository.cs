using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class NoticeRepository : INoticeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoticeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual Notice GetEntity(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            Notice entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Notice();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Title = SqlTypeConverter.DBNullStringHandler(rdr["Title"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public virtual List<Notice> GetEntities() {
            var entities = new List<Notice>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_GetEntities1, null)) {
                while(rdr.Read()) {
                    var entity = new Notice();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Title = SqlTypeConverter.DBNullStringHandler(rdr["Title"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual List<Notice> GetEntities(Guid uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);

            var entities = new List<Notice>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_GetEntities2, parms)) {
                while(rdr.Read()) {
                    var entity = new Notice();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Title = SqlTypeConverter.DBNullStringHandler(rdr["Title"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual int GetUnreadCount(Guid uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                var count = SqlHelper.ExecuteScalar(conn, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_GetUnreadCount, parms);
                if(count != null && count != DBNull.Value)
                    return Convert.ToInt32(count);
            }

            return 0;
        }

        public virtual void Insert(Notice entity) {
            Insert(new List<Notice>() { entity });
        }

        public virtual void Insert(List<Notice> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Title", SqlDbType.VarChar,512),
                                     new SqlParameter("@Content", SqlDbType.VarChar),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Title);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[4].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Update(Notice entity) {
            Update(new List<Notice>() { entity });
        }

        public virtual void Update(List<Notice> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Title", SqlDbType.VarChar,512),
                                     new SqlParameter("@Content", SqlDbType.VarChar),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Title);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[4].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(Notice entity) {
            Delete(new List<Notice>() { entity });
        }

        public virtual void Delete(List<Notice> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Notice_Repository_Delete, parms);
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
