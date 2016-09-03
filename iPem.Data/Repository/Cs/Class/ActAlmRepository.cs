using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class ActAlmRepository : IActAlmRepository {

        #region Fields

        private readonly string _databaseConnectionString;

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

        public List<ActAlm> GetEntitiesInArea(string area) {
            SqlParameter[] parms = { new SqlParameter("@AreaId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(area);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_ActAlm_Repository_GetEntitiesByArea, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<ActAlm> GetEntitiesInStation(string station) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(station);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_ActAlm_Repository_GetEntitiesByStation, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<ActAlm> GetEntitiesInRoom(string room) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(room);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_ActAlm_Repository_GetEntitiesByRoom, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<ActAlm> GetEntitiesInDevice(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_ActAlm_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<ActAlm> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_ActAlm_Repository_GetEntitiesByTime, parms)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<ActAlm> GetEntities() {
            var entities = new List<ActAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_ActAlm_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new ActAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
                    entity.EndType = SqlTypeConverter.DBNullEnmEndTypeHandler(rdr["EndType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
