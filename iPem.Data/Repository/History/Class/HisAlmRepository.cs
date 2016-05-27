using iPem.Core.Domain.History;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.History {
    public partial class HisAlmRepository : IHisAlmRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisAlmRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>history alarm list</returns>
        public List<HisAlm> GetEntities(string device, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_HisAlm_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.AlmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlmLevel"]);
                    entity.Frequency = SqlTypeConverter.DBNullInt32Handler(rdr["Frequency"]);
                    entity.AlmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlmDesc"]);
                    entity.NormalDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalDesc"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.ValueUnit = SqlTypeConverter.DBNullStringHandler(rdr["ValueUnit"]);
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.ConfirmedStatus = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["ConfirmedStatus"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Gets entities from the repository by the specific datetime
        /// </summary>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns>history alarm list</returns>
        public List<HisAlm> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_HisAlm_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.AlmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlmLevel"]);
                    entity.Frequency = SqlTypeConverter.DBNullInt32Handler(rdr["Frequency"]);
                    entity.AlmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlmDesc"]);
                    entity.NormalDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalDesc"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.ValueUnit = SqlTypeConverter.DBNullStringHandler(rdr["ValueUnit"]);
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.ConfirmedStatus = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["ConfirmedStatus"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
