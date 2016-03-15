using iPem.Core.Domain.History;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.History {
    public partial class ActAlmRepository : IActAlmRepository {

        #region Fields

        private readonly string _databaseConnectionString;
        private const string _delimiter = "┆";

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ActAlmRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>active alarm list</returns>
        public List<ActAlm> GetEntities(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActAlm_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.AlmFlag = SqlTypeConverter.DBNullEnmFlagHandler(rdr["AlmFlag"]);
                    entity.AlmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlmLevel"]);
                    entity.Frequency = SqlTypeConverter.DBNullInt32Handler(rdr["Frequency"]);
                    entity.AlmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlmDesc"]);
                    entity.NormalDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalDesc"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.ValueUnit = SqlTypeConverter.DBNullStringHandler(rdr["ValueUnit"]);
                    entity.EndType = SqlTypeConverter.DBNullInt32Handler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Gets entities from the repository by the specific alarm levels
        /// </summary>
        /// <param name="levels">the alarm level array</param>
        /// <returns>active alarm list</returns>
        public List<ActAlm> GetEntities(int[] levels) {
            SqlParameter[] parms = { new SqlParameter("@Levels", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Delimiter", SqlDbType.VarChar, 10) };

            parms[0].Value = string.Join(_delimiter, levels);
            parms[1].Value = _delimiter;

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActAlm_Repository_GetEntitiesByLevel, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.AlmFlag = SqlTypeConverter.DBNullEnmFlagHandler(rdr["AlmFlag"]);
                    entity.AlmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlmLevel"]);
                    entity.Frequency = SqlTypeConverter.DBNullInt32Handler(rdr["Frequency"]);
                    entity.AlmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlmDesc"]);
                    entity.NormalDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalDesc"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.ValueUnit = SqlTypeConverter.DBNullStringHandler(rdr["ValueUnit"]);
                    entity.EndType = SqlTypeConverter.DBNullInt32Handler(rdr["EndType"]);
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
        /// <returns>active alarm list</returns>
        public List<ActAlm> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActAlm_Repository_GetEntitiesByTime, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.AlmFlag = SqlTypeConverter.DBNullEnmFlagHandler(rdr["AlmFlag"]);
                    entity.AlmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlmLevel"]);
                    entity.Frequency = SqlTypeConverter.DBNullInt32Handler(rdr["Frequency"]);
                    entity.AlmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlmDesc"]);
                    entity.NormalDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalDesc"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.ValueUnit = SqlTypeConverter.DBNullStringHandler(rdr["ValueUnit"]);
                    entity.EndType = SqlTypeConverter.DBNullInt32Handler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>active alarm list</returns>
        public List<ActAlm> GetEntities() {
            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActAlm_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.AlmFlag = SqlTypeConverter.DBNullEnmFlagHandler(rdr["AlmFlag"]);
                    entity.AlmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlmLevel"]);
                    entity.Frequency = SqlTypeConverter.DBNullInt32Handler(rdr["Frequency"]);
                    entity.AlmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlmDesc"]);
                    entity.NormalDesc = SqlTypeConverter.DBNullStringHandler(rdr["NormalDesc"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.ValueUnit = SqlTypeConverter.DBNullStringHandler(rdr["ValueUnit"]);
                    entity.EndType = SqlTypeConverter.DBNullInt32Handler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
