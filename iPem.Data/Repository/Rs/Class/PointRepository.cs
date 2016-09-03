using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class PointRepository : IPointRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<Point> GetEntitiesByDevice(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100) };
            parms[0].Value = device;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Point_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.StationType = new StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StationTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StationTypeName"]) };
                    entity.SubDeviceType = new SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.DeviceType = new DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubLogicType = new SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.LogicType = new LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Point> GetEntitiesByProtocol(string protocol) {
            SqlParameter[] parms = { new SqlParameter("@ProtocolId", SqlDbType.VarChar, 100) };
            parms[0].Value = protocol;

            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Point_Repository_GetEntitiesByProtcol, parms)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.StationType = new StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StationTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StationTypeName"]) };
                    entity.SubDeviceType = new SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.DeviceType = new DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubLogicType = new SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.LogicType = new LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Point> GetEntities() {
            var entities = new List<Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Point_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.StationType = new StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StationTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StationTypeName"]) };
                    entity.SubDeviceType = new SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.DeviceType = new DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubLogicType = new SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.LogicType = new LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
