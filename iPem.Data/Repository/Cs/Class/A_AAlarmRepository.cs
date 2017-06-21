using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class A_AAlarmRepository : IA_AAlarmRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public A_AAlarmRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<A_AAlarm> GetAlarmsInArea(string id) {
            SqlParameter[] parms = { new SqlParameter("@AreaId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAlarmsInArea, parms)) {
                while(rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetAlarmsInStation(string id) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAlarmsInStation, parms)) {
                while(rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetAlarmsInRoom(string id) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAlarmsInRoom, parms)) {
                while(rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetAlarmsInDevice(string id) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAlarmsInDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetAlarms(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<A_AAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAlarmsInSpan, parms)) {
                while(rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetAlarms() {
            var entities = new List<A_AAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAlarms, null)) {
                while(rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetAllAlarms() {
            var entities = new List<A_AAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetAllAlarms, null)) {
                while (rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetPrimaryAlarms(string id) {
            SqlParameter[] parms = { new SqlParameter("@PrimaryId", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetPrimaryAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetRelatedAlarms(string id) {
            SqlParameter[] parms = { new SqlParameter("@RelatedId", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetRelatedAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_AAlarm> GetFilterAlarms(string id) {
            SqlParameter[] parms = { new SqlParameter("@FilterId", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<A_AAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_GetFilterAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_AAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.AlarmTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AlarmTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.AlarmValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AlarmValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Confirm(IList<A_AAlarm> alarms) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Confirmed", SqlDbType.Int),
                                     new SqlParameter("@Confirmer", SqlDbType.VarChar, 100),
                                     new SqlParameter("@ConfirmedTime", SqlDbType.DateTime)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var alarm in alarms) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(alarm.Id);
                        parms[1].Value = (int)alarm.Confirmed;
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(alarm.Confirmer);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeNullableChecker(alarm.ConfirmedTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_A_AAlarm_Repository_Confirm, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion

    }
}
