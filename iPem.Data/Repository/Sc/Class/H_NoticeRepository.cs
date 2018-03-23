using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class H_NoticeRepository : IH_NoticeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_NoticeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public H_Notice GetNotice(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            H_Notice entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_GetNotice, parms)) {
                if (rdr.Read()) {
                    entity = new H_Notice();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Title = SqlTypeConverter.DBNullStringHandler(rdr["Title"]);
                    entity.Content = SqlTypeConverter.DBNullStringHandler(rdr["Content"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<H_Notice> GetNotices() {
            var entities = new List<H_Notice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_GetNotices, null)) {
                while (rdr.Read()) {
                    var entity = new H_Notice();
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

        public List<H_Notice> GetNoticesInSpan(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<H_Notice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_GetNoticesInSpan, parms)) {
                while (rdr.Read()) {
                    var entity = new H_Notice();
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

        public List<H_Notice> GetNoticesInUser(string uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(uid);

            var entities = new List<H_Notice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_GetNoticesInUser, parms)) {
                while (rdr.Read()) {
                    var entity = new H_Notice();
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

        public List<H_Notice> GetUnreadNotices(string uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(uid);

            var entities = new List<H_Notice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_GetUnreadNotices, parms)) {
                while (rdr.Read()) {
                    var entity = new H_Notice();
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

        public void Insert(IList<H_Notice> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Title", SqlDbType.VarChar,512),
                                     new SqlParameter("@Content", SqlDbType.VarChar),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Title);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[4].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<H_Notice> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Title", SqlDbType.VarChar,512),
                                     new SqlParameter("@Content", SqlDbType.VarChar),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Title);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Content);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[4].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<H_Notice> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_Notice_Repository_Delete, parms);
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
