using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class HisValueRepository : IHisValueRepository {

        #region Fields

        private readonly string _databaseConnectionString;
        private const string _delimiter = "┆";

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisValueRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<HisValue> GetEntities(string device, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisValue_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new HisValue();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.Threshold = SqlTypeConverter.DBNullDoubleHandler(rdr["Threshold"]);
                    entity.State = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["State"]);
                    entity.Time = SqlTypeConverter.DBNullDateTimeHandler(rdr["Time"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisValue> GetEntities(string device, string point, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(point);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisValue_Repository_GetEntitiesByPoint, parms)) {
                while(rdr.Read()) {
                    var entity = new HisValue();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.Threshold = SqlTypeConverter.DBNullDoubleHandler(rdr["Threshold"]);
                    entity.State = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["State"]);
                    entity.Time = SqlTypeConverter.DBNullDateTimeHandler(rdr["Time"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisValue> GetEntities(string[] points, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Points", SqlDbType.VarChar),
                                     new SqlParameter("@Delimiter", SqlDbType.VarChar, 10),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = string.Join(_delimiter, points);
            parms[1].Value = _delimiter;
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisValue_Repository_GetEntitiesByPoints, parms)) {
                while(rdr.Read()) {
                    var entity = new HisValue();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.Threshold = SqlTypeConverter.DBNullDoubleHandler(rdr["Threshold"]);
                    entity.State = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["State"]);
                    entity.Time = SqlTypeConverter.DBNullDateTimeHandler(rdr["Time"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisValue> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisValue_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisValue();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.Threshold = SqlTypeConverter.DBNullDoubleHandler(rdr["Threshold"]);
                    entity.State = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["State"]);
                    entity.Time = SqlTypeConverter.DBNullDateTimeHandler(rdr["Time"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
