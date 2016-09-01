using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class HisStaticRepository : IHisStaticRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisStaticRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<HisStatic> GetEntities(string device, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisStatic>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisStatic_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new HisStatic();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.MaxValue = SqlTypeConverter.DBNullDoubleHandler(rdr["MaxValue"]);
                    entity.MinValue = SqlTypeConverter.DBNullDoubleHandler(rdr["MinValue"]);
                    entity.AvgValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AvgValue"]);
                    entity.MaxTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["MaxTime"]);
                    entity.MinTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["MinTime"]);
                    entity.Total = SqlTypeConverter.DBNullInt32Handler(rdr["Total"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisStatic> GetEntities(string device, string point, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(point);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisStatic>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisStatic_Repository_GetEntitiesByPoint, parms)) {
                while(rdr.Read()) {
                    var entity = new HisStatic();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.MaxValue = SqlTypeConverter.DBNullDoubleHandler(rdr["MaxValue"]);
                    entity.MinValue = SqlTypeConverter.DBNullDoubleHandler(rdr["MinValue"]);
                    entity.AvgValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AvgValue"]);
                    entity.MaxTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["MaxTime"]);
                    entity.MinTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["MinTime"]);
                    entity.Total = SqlTypeConverter.DBNullInt32Handler(rdr["Total"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisStatic> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisStatic>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisStatic_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisStatic();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.BeginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BeginTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.MaxValue = SqlTypeConverter.DBNullDoubleHandler(rdr["MaxValue"]);
                    entity.MinValue = SqlTypeConverter.DBNullDoubleHandler(rdr["MinValue"]);
                    entity.AvgValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AvgValue"]);
                    entity.MaxTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["MaxTime"]);
                    entity.MinTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["MinTime"]);
                    entity.Total = SqlTypeConverter.DBNullInt32Handler(rdr["Total"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
