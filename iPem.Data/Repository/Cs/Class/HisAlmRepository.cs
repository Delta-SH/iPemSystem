using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
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

        public List<HisAlm> GetEntitiesInArea(string area, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@AreaId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(area);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisAlm_Repository_GetEntitiesByArea, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
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
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisAlm> GetEntitiesInStation(string station, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(station);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisAlm_Repository_GetEntitiesByStation, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
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
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisAlm> GetEntitiesInRoom(string room, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(room);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisAlm_Repository_GetEntitiesByRoom, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
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
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisAlm> GetEntitiesInDevice(string device, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisAlm_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
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
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<HisAlm> GetEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<HisAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_HisAlm_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new HisAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
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
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
