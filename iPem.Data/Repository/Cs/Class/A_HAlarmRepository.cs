using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class A_HAlarmRepository : IA_HAlarmRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public A_HAlarmRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<A_HAlarm> GetAlarmsInArea(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@AreaId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAlarmsInArea, parms)) {
                while(rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetAlarmsInStation(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAlarmsInStation, parms)) {
                while(rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetAlarmsInRoom(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAlarmsInRoom, parms)) {
                while(rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetAlarmsInDevice(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAlarmsInDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetAlarmsInPoint(string device, string point, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(point);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAlarmsInPoint, parms)) {
                while(rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetAlarms(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAlarms, parms)) {
                while(rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetAllAlarms(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetAllAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetPrimaryAlarms(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@PrimaryId", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetPrimaryAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetRelatedAlarms(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@RelatedId", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetRelatedAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetFilterAlarms(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@FilterId", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetFilterAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<A_HAlarm> GetReversalAlarms(string id, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@ReversalId", SqlDbType.VarChar, 200),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<A_HAlarm>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_A_HAlarm_Repository_GetReversalAlarms, parms)) {
                while (rdr.Read()) {
                    var entity = new A_HAlarm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.SerialNo = SqlTypeConverter.DBNullStringHandler(rdr["SerialNo"]);
                    entity.NMAlarmId = SqlTypeConverter.DBNullStringHandler(rdr["NMAlarmId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.AlarmLevel = SqlTypeConverter.DBNullEnmLevelHandler(rdr["AlarmLevel"]);
                    entity.StartValue = SqlTypeConverter.DBNullDoubleHandler(rdr["StartValue"]);
                    entity.EndValue = SqlTypeConverter.DBNullDoubleHandler(rdr["EndValue"]);
                    entity.AlarmDesc = SqlTypeConverter.DBNullStringHandler(rdr["AlarmDesc"]);
                    entity.AlarmRemark = SqlTypeConverter.DBNullStringHandler(rdr["AlarmRemark"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ConfirmedTime"]);
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.PrimaryId = SqlTypeConverter.DBNullStringHandler(rdr["PrimaryId"]);
                    entity.RelatedId = SqlTypeConverter.DBNullStringHandler(rdr["RelatedId"]);
                    entity.FilterId = SqlTypeConverter.DBNullStringHandler(rdr["FilterId"]);
                    entity.ReversalId = SqlTypeConverter.DBNullStringHandler(rdr["ReversalId"]);
                    entity.ReversalCount = SqlTypeConverter.DBNullInt32Handler(rdr["ReversalCount"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ReversalId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
