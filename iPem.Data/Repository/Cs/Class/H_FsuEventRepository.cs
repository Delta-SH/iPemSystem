using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class H_FsuEventRepository : IH_FsuEventRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_FsuEventRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_FsuEvent> GetEvents(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<H_FsuEvent>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_FsuEvent_Repository_GetEvents, parms)) {
                while(rdr.Read()) {
                    var entity = new H_FsuEvent();
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.EventType = SqlTypeConverter.DBNullEnmFsuEventHandler(rdr["EventType"]);
                    entity.Message = SqlTypeConverter.DBNullStringHandler(rdr["Message"]);
                    entity.EventTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EventTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_FsuEvent> GetEventsInType(DateTime start, DateTime end, EnmFsuEvent type) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@EventType", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)type;

            var entities = new List<H_FsuEvent>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_FsuEvent_Repository_GetEventsInType, parms)) {
                while(rdr.Read()) {
                    var entity = new H_FsuEvent();
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.EventType = SqlTypeConverter.DBNullEnmFsuEventHandler(rdr["EventType"]);
                    entity.Message = SqlTypeConverter.DBNullStringHandler(rdr["Message"]);
                    entity.EventTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EventTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
