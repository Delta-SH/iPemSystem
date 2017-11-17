using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class H_WebEventRepository : IH_WebEventRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_WebEventRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_WebEvent> GetWebEvents(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = start;
            parms[1].Value = end;

            var entities = new List<H_WebEvent>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_H_WebEvent_Repository_GetWebEvents, parms)) {
                while (rdr.Read()) {
                    var entity = new H_WebEvent();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Level = SqlTypeConverter.DBNullEventLevelHandler(rdr["Level"]);
                    entity.Type = SqlTypeConverter.DBNullEventTypeHandler(rdr["Type"]);
                    entity.ShortMessage = SqlTypeConverter.DBNullStringHandler(rdr["ShortMessage"]);
                    entity.FullMessage = SqlTypeConverter.DBNullStringHandler(rdr["FullMessage"]);
                    entity.IpAddress = SqlTypeConverter.DBNullStringHandler(rdr["IpAddress"]);
                    entity.PageUrl = SqlTypeConverter.DBNullStringHandler(rdr["PageUrl"]);
                    entity.ReferrerUrl = SqlTypeConverter.DBNullStringHandler(rdr["ReferrerUrl"]);
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_WebEvent> GetWebEvents(DateTime? start = null, DateTime? end = null, EnmEventLevel[] levels = null, EnmEventType[] types = null) {
            var query = @"SELECT * FROM [dbo].[H_WebEvents]";
            var conditions = new List<String>();
            if (start.HasValue) conditions.Add(String.Format(@"[CreatedTime] >= '{0}'", CommonHelper.DateTimeConverter(start.Value)));
            if (end.HasValue) conditions.Add(String.Format(@"[CreatedTime] <= '{0}'", CommonHelper.DateTimeConverter(end.Value)));
            if (levels !=null && levels.Length>0){
                var _levels = new List<int>();
                foreach(var level in levels)
                    _levels.Add((int)level);

                conditions.Add(String.Format(@"[Level] IN ({0})", string.Join(",", _levels)));
            }

            if(types != null && types.Length > 0) {
                var _types = new List<int>();
                foreach(var type in types)
                    _types.Add((int)type);

                conditions.Add(String.Format(@"[Type] IN ({0})", string.Join(",", _types)));
            }

            var orders = new List<String>();
            orders.Add(@"[CreatedTime] DESC");
            
            if (conditions.Count > 0)
                query += @" WHERE " + string.Join(@" AND ", conditions);
            if (orders.Count > 0)
                query += @" ORDER BY " + string.Join(@",", orders);

            var entities = new List<H_WebEvent>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, query, null)) {
                while (rdr.Read()) {
                    var entity = new H_WebEvent();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Level = SqlTypeConverter.DBNullEventLevelHandler(rdr["Level"]);
                    entity.Type = SqlTypeConverter.DBNullEventTypeHandler(rdr["Type"]);
                    entity.ShortMessage = SqlTypeConverter.DBNullStringHandler(rdr["ShortMessage"]);
                    entity.FullMessage = SqlTypeConverter.DBNullStringHandler(rdr["FullMessage"]);
                    entity.IpAddress = SqlTypeConverter.DBNullStringHandler(rdr["IpAddress"]);
                    entity.PageUrl = SqlTypeConverter.DBNullStringHandler(rdr["PageUrl"]);
                    entity.ReferrerUrl = SqlTypeConverter.DBNullStringHandler(rdr["ReferrerUrl"]);
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<H_WebEvent> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Level", SqlDbType.Int),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@ShortMessage", SqlDbType.VarChar,512),
                                     new SqlParameter("@FullMessage", SqlDbType.VarChar),
                                     new SqlParameter("@IpAddress", SqlDbType.VarChar,200),
                                     new SqlParameter("@PageUrl", SqlDbType.VarChar,512),
                                     new SqlParameter("@ReferrerUrl", SqlDbType.VarChar,512),
                                     new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = entity.Level;
                        parms[2].Value = entity.Type;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.ShortMessage);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.FullMessage);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.IpAddress);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.PageUrl);
                        parms[7].Value = SqlTypeConverter.DBNullStringChecker(entity.ReferrerUrl);
                        parms[8].Value = DBNull.Value;
                        if(entity.UserId.HasValue) parms[8].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId.Value);
                        parms[9].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_WebEvent_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<H_WebEvent> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_H_WebEvent_Repository_Delete, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Clear(DateTime? start = null, DateTime? end = null) {
            var conditions = new List<String>();
            if (start.HasValue)
                conditions.Add(String.Format(@"[CreatedTime] >= '{0}'", CommonHelper.DateTimeConverter(start.Value)));
            if (end.HasValue)
                conditions.Add(String.Format(@"[CreatedTime] <= '{0}'", CommonHelper.DateTimeConverter(end.Value)));

            var query = @"DELETE FROM [dbo].[H_WebEvents]";
            if (conditions.Count > 0)
                query = query + @" WHERE " + String.Join(@" AND ", conditions.ToArray());

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, query, null);
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
