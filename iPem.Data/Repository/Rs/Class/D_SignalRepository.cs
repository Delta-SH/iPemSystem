using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace iPem.Data.Repository.Rs {
    public partial class D_SignalRepository : ID_SignalRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public D_SignalRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<D_Signal> GetAbsThresholds() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAbsThresholds, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetPerThresholds() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetPerThresholds, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetSavedPeriods() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetSavedPeriods, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetStorageRefTimes() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetStorageRefTimes, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmLimits() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmLimits, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmLevels() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmLevels, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmDelays() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmDelays, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmRecoveryDelays() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmRecoveryDelays, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmFilterings() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmFilterings, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmInferiors() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmInferiors, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmConnections() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmConnections, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Signal> GetAlarmReversals() {
            var entities = new List<D_Signal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetAlarmReversals, null)) {
                while (rdr.Read()) {
                    var entity = new D_Signal();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.DeviceName = SqlTypeConverter.DBNullStringHandler(rdr["DeviceName"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.Current = SqlTypeConverter.DBNullStringHandler(rdr["Current"]);
                    entity.Normal = SqlTypeConverter.DBNullStringHandler(rdr["Normal"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmID"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public D_SimpleSignal GetSimpleSignal(string device, string point) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(point);

            D_SimpleSignal entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetSimpleSignal, parms)) {
                if (rdr.Read()) {
                    entity = new D_SimpleSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.OfficialName = SqlTypeConverter.DBNullStringHandler(rdr["OfficialName"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }
                }
            }
            return entity;
        }

        public List<D_SimpleSignal> GetSimpleSignals(IEnumerable<Kv<string, string>> pairs) {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine(@"
            CREATE TABLE #TEMP(
	            [DeviceId] [varchar](100) NOT NULL,
	            [PointId] [varchar](100) NOT NULL,
	            PRIMARY KEY CLUSTERED 
	            (
		            [DeviceId] ASC,
		            [PointId] ASC
	            )
            );");

            sqlBuilder.Append(@"
            INSERT INTO #TEMP([DeviceId],[PointId])
            ");

            sqlBuilder.AppendLine(string.Join(@"
            UNION ALL
            ", pairs.Select(p => string.Format(@"SELECT '{0}','{1}'", p.Key, p.Value))));

            sqlBuilder.Append(@"
            SELECT S.[DeviceId],S.[PointId],P.[Code],P.[Number],P.[Type] AS [PointType],S.[Name] AS [PointName],P.[Name] AS [OfficialName],P.[UnitState],P.[AlarmID] FROM [dbo].[D_Signal] S 
            INNER JOIN [dbo].[P_Point] P ON S.[PointID]=P.[ID]
            INNER JOIN #TEMP T ON S.[DeviceId]=T.[DeviceId] AND S.[PointId]=T.[PointId];
            DROP TABLE #TEMP;");

            var entities = new List<D_SimpleSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sqlBuilder.ToString(), null)) {
                while (rdr.Read()) {
                    var entity = new D_SimpleSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.OfficialName = SqlTypeConverter.DBNullStringHandler(rdr["OfficialName"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_SimpleSignal> GetSimpleSignalsInDevice(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);

            var entities = new List<D_SimpleSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetSimpleSignalsInDevice, parms)) {
                while (rdr.Read()) {
                    var entity = new D_SimpleSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.OfficialName = SqlTypeConverter.DBNullStringHandler(rdr["OfficialName"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_SimpleSignal> GetSimpleSignalsInDevices(IEnumerable<string> devices) {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine(@"
            CREATE TABLE #TEMP(
	            [DeviceId] [varchar](100) NOT NULL,
	            PRIMARY KEY CLUSTERED 
	            (
		            [DeviceId] ASC
	            )
            );");

            sqlBuilder.Append(@"
            INSERT INTO #TEMP([DeviceId])
            ");

            sqlBuilder.AppendLine(string.Join(@"
            UNION ALL
            ", devices.Select(d => string.Format(@"SELECT '{0}'", d))));

            sqlBuilder.Append(@"
            SELECT S.[DeviceId],S.[PointId],P.[Code],P.[Number],P.[Type] AS [PointType],S.[Name] AS [PointName],P.[Name] AS [OfficialName],P.[UnitState],P.[AlarmID] FROM [dbo].[D_Signal] S 
            INNER JOIN [dbo].[P_Point] P ON S.[PointID]=P.[ID]
            INNER JOIN #TEMP T ON S.[DeviceId]=T.[DeviceId];
            DROP TABLE #TEMP;");

            var entities = new List<D_SimpleSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sqlBuilder.ToString(), null)) {
                while (rdr.Read()) {
                    var entity = new D_SimpleSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Number = SqlTypeConverter.DBNullStringHandler(rdr["Number"]);
                    entity.PointType = SqlTypeConverter.DBNullEnmPointHandler(rdr["PointType"]);
                    entity.PointName = SqlTypeConverter.DBNullStringHandler(rdr["PointName"]);
                    entity.OfficialName = SqlTypeConverter.DBNullStringHandler(rdr["OfficialName"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);

                    //判断是否为告警信号
                    if (entity.PointType == EnmPoint.DI) {
                        var alarmId = SqlTypeConverter.DBNullStringHandler(rdr["AlarmId"]);
                        if (!string.IsNullOrWhiteSpace(alarmId))
                            entity.PointType = EnmPoint.AL;
                    }

                    entities.Add(entity);
                }
            }
            return entities;
        }

        public D_VSignal GetVSignal(string device, string point) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullStringChecker(point);

            D_VSignal entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetVSignal, parms)) {
                if (rdr.Read()) {
                    entity = new D_VSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["FormulaText"]);
                    entity.FormulaValue = SqlTypeConverter.DBNullStringHandler(rdr["FormulaValue"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                }
            }
            return entity;
        }

        public List<D_VSignal> GetVSignals() {
            var entities = new List<D_VSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetVSignals1, null)) {
                while (rdr.Read()) {
                    var entity = new D_VSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["FormulaText"]);
                    entity.FormulaValue = SqlTypeConverter.DBNullStringHandler(rdr["FormulaValue"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_VSignal> GetVSignals(IEnumerable<Kv<string, string>> pairs) {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine(@"
            CREATE TABLE #TEMP(
	            [DeviceId] [varchar](100) NOT NULL,
	            [PointId] [varchar](100) NOT NULL,
	            PRIMARY KEY CLUSTERED 
	            (
		            [DeviceId] ASC,
		            [PointId] ASC
	            )
            );");

            sqlBuilder.Append(@"
            INSERT INTO #TEMP([DeviceId],[PointId])
            ");

            sqlBuilder.AppendLine(string.Join(@"
            UNION ALL
            ", pairs.Select(p => string.Format(@"SELECT '{0}','{1}'", p.Key, p.Value))));

            sqlBuilder.Append(@"
            SELECT * FROM [dbo].[D_VSignal] S 
            INNER JOIN #TEMP T ON S.[DeviceId]=T.[DeviceId] AND S.[PointId]=T.[PointId];
            DROP TABLE #TEMP;");

            var entities = new List<D_VSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, sqlBuilder.ToString(), null)) {
                while (rdr.Read()) {
                    var entity = new D_VSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["FormulaText"]);
                    entity.FormulaValue = SqlTypeConverter.DBNullStringHandler(rdr["FormulaValue"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_VSignal> GetVSignals(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);

            var entities = new List<D_VSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetVSignals2, parms)) {
                while (rdr.Read()) {
                    var entity = new D_VSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["FormulaText"]);
                    entity.FormulaValue = SqlTypeConverter.DBNullStringHandler(rdr["FormulaValue"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_VSignal> GetVSignals(string device, EnmVSignalCategory category) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Category", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = (int)category;

            var entities = new List<D_VSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetVSignals3, parms)) {
                while (rdr.Read()) {
                    var entity = new D_VSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["FormulaText"]);
                    entity.FormulaValue = SqlTypeConverter.DBNullStringHandler(rdr["FormulaValue"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_VSignal> GetVSignals(EnmVSignalCategory category) {
            SqlParameter[] parms = { new SqlParameter("@Category", SqlDbType.Int) };
            parms[0].Value = (int)category;

            var entities = new List<D_VSignal>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_GetVSignals4, parms)) {
                while (rdr.Read()) {
                    var entity = new D_VSignal();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullEnmPointHandler(rdr["Type"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["FormulaText"]);
                    entity.FormulaValue = SqlTypeConverter.DBNullStringHandler(rdr["FormulaValue"]);
                    entity.UnitState = SqlTypeConverter.DBNullStringHandler(rdr["UnitState"]);
                    entity.SavedPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["SavedPeriod"]);
                    entity.StaticPeriod = SqlTypeConverter.DBNullInt32Handler(rdr["StaticPeriod"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void InsertVSignal(IList<D_VSignal> entities) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@Type",SqlDbType.Int),
                                     new SqlParameter("@FormulaText", SqlDbType.VarChar),
                                     new SqlParameter("@FormulaValue", SqlDbType.VarChar),
                                     new SqlParameter("@UnitState", SqlDbType.VarChar,160),
                                     new SqlParameter("@SavedPeriod",SqlDbType.Int),
                                     new SqlParameter("@StaticPeriod",SqlDbType.Int),
                                     new SqlParameter("@Category",SqlDbType.VarChar),
                                     new SqlParameter("@Remark",SqlDbType.VarChar,1024)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.PointId);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[3].Value = entity.Type;
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.FormulaText);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.FormulaValue);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.UnitState);
                        parms[7].Value = SqlTypeConverter.DBNullInt32Checker(entity.SavedPeriod);
                        parms[8].Value = SqlTypeConverter.DBNullInt32Checker(entity.StaticPeriod);
                        parms[9].Value = (int)entity.Category;
                        parms[10].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_InsertVSignal, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void UpdateVSignal(IList<D_VSignal> entities) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@Type",SqlDbType.Int),
                                     new SqlParameter("@FormulaText", SqlDbType.VarChar),
                                     new SqlParameter("@FormulaValue", SqlDbType.VarChar),
                                     new SqlParameter("@UnitState", SqlDbType.VarChar,160),
                                     new SqlParameter("@SavedPeriod",SqlDbType.Int),
                                     new SqlParameter("@StaticPeriod",SqlDbType.Int),
                                     new SqlParameter("@Category",SqlDbType.VarChar),
                                     new SqlParameter("@Remark",SqlDbType.VarChar,1024)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.PointId);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[3].Value = entity.Type;
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.FormulaText);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.FormulaValue);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.UnitState);
                        parms[7].Value = SqlTypeConverter.DBNullInt32Checker(entity.SavedPeriod);
                        parms[8].Value = SqlTypeConverter.DBNullInt32Checker(entity.StaticPeriod);
                        parms[9].Value = (int)entity.Category;
                        parms[10].Value = SqlTypeConverter.DBNullStringChecker(entity.Remark);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_UpdateVSignal, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void DeleteVSignal(IList<D_VSignal> entities) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.PointId);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_D_Signal_Repository_DeleteVSignal, parms);
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