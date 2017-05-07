using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class HisFtpRepository : IHisFtpRepository {

        #region Fields

        private readonly string _databaseConnectionString;
        private const string _delimiter = "┆";

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisFtpRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<HisFtp> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisFtp>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisFtp_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisFtp();
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.EventType = SqlTypeConverter.DBNullEnmFtpEventHandler(rdr["EventType"]);
                    entity.Message = SqlTypeConverter.DBNullStringHandler(rdr["Message"]);
                    entity.EventTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EventTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisFtp> GetEntities(DateTime start, DateTime end, EnmFtpEvent type) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@EventType", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)type;

            var entities = new List<HisFtp>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisFtp_Repository_GetEntitiesByType, parms)) {
                while(rdr.Read()) {
                    var entity = new HisFtp();
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.EventType = SqlTypeConverter.DBNullEnmFtpEventHandler(rdr["EventType"]);
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
