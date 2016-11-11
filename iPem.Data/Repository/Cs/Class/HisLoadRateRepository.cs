using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class HisLoadRateRepository : IHisLoadRateRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisLoadRateRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<HisLoadRate> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisLoadRate>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisLoadRate_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisLoadRate();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.Period = SqlTypeConverter.DBNullDateTimeHandler(rdr["Period"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisLoadRate> GetMaxEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisLoadRate>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisLoadRate_Repository_GetMaxEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisLoadRate();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.Period = SqlTypeConverter.DBNullDateTimeHandler(rdr["Period"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisLoadRate> GetMinEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisLoadRate>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisLoadRate_Repository_GetMinEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisLoadRate();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.Period = SqlTypeConverter.DBNullDateTimeHandler(rdr["Period"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
