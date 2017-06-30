using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class P_PointRepository : IP_PointRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public P_PointRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<P_Point> GetPointsInDevice(string id) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100) };
            parms[0].Value = id;

            var entities = new List<P_Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_P_Point_Repository_GetPointsInDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new P_Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.AlarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.DeviceType = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.LogicType = new C_LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.DeviceEffect = SqlTypeConverter.DBNullStringHandler(rdr["DeviceEffect"]);
                    entity.BusiEffect = SqlTypeConverter.DBNullStringHandler(rdr["BusiEffect"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<P_Point> GetPointsInProtocol(string id) {
            SqlParameter[] parms = { new SqlParameter("@ProtocolId", SqlDbType.VarChar, 100) };
            parms[0].Value = id;

            var entities = new List<P_Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_P_Point_Repository_GetPointsInProtocol, parms)) {
                while(rdr.Read()) {
                    var entity = new P_Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.AlarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.DeviceType = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.LogicType = new C_LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.DeviceEffect = SqlTypeConverter.DBNullStringHandler(rdr["DeviceEffect"]);
                    entity.BusiEffect = SqlTypeConverter.DBNullStringHandler(rdr["BusiEffect"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<P_Point> GetPoints() {
            var entities = new List<P_Point>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_P_Point_Repository_GetPoints, null)) {
                while(rdr.Read()) {
                    var entity = new P_Point();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.AlarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.DeviceType = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.LogicType = new C_LogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["LogicTypeName"]) };
                    entity.AlarmComment = SqlTypeConverter.DBNullStringHandler(rdr["AlarmComment"]);
                    entity.NormalComment = SqlTypeConverter.DBNullStringHandler(rdr["NormalComment"]);
                    entity.DeviceEffect = SqlTypeConverter.DBNullStringHandler(rdr["DeviceEffect"]);
                    entity.BusiEffect = SqlTypeConverter.DBNullStringHandler(rdr["BusiEffect"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Interpret = SqlTypeConverter.DBNullStringHandler(rdr["Interpret"]);
                    entity.ExtSet1 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet1"]);
                    entity.ExtSet2 = SqlTypeConverter.DBNullStringHandler(rdr["ExtSet2"]);
                    entity.Description = SqlTypeConverter.DBNullStringHandler(rdr["Description"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public P_SubPoint GetSubPoint(string point, string statype) {
            SqlParameter[] parms = { new SqlParameter("@PointId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@StationTypeId", SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(point);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(statype);

            P_SubPoint entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_P_Point_Repository_GetSubPoint, parms)) {
                if (rdr.Read()) {
                    entity = new P_SubPoint();
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.StationType = new C_StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeName"]) };
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                }
            }
            return entity;
        }

        public List<P_SubPoint> GetSubPointsInPoint(string id) {
            SqlParameter[] parms = { new SqlParameter("@PointId", SqlDbType.VarChar, 100) };
            parms[0].Value = id;

            var entities = new List<P_SubPoint>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_P_Point_Repository_GetSubPointsInPoint, parms)) {
                while (rdr.Read()) {
                    var entity = new P_SubPoint();
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.StationType = new C_StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeName"]) };
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<P_SubPoint> GetSubPoints() {
            var entities = new List<P_SubPoint>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_P_Point_Repository_GetSubPoints, null)) {
                while (rdr.Read()) {
                    var entity = new P_SubPoint();
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.StationType = new C_StationType { Id = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeName"]) };
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.TriggerTypeId = SqlTypeConverter.DBNullInt32Handler(rdr["TriggerTypeId"]);
                    entity.AlarmLimit = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmLimit"]);
                    entity.AlarmReturnDiff = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmReturnDiff"]);
                    entity.AlarmRecoveryDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmRecoveryDelay"]);
                    entity.AlarmDelay = SqlTypeConverter.DBNullInt32Handler(rdr["AlarmDelay"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.AbsoluteThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["AbsoluteThreshold"]);
                    entity.PerThreshold = SqlTypeConverter.DBNullDoubleHandler(rdr["PerThreshold"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
