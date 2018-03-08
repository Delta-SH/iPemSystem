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
            SELECT S.[DeviceId],S.[PointId],P.[Code],P.[Number],P.[Type] AS [PointType],S.[Name] AS [PointName],P.[UnitState],P.[AlarmID] FROM [dbo].[D_Signal] S 
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
            SELECT S.[DeviceId],S.[PointId],P.[Code],P.[Number],P.[Type] AS [PointType],S.[Name] AS [PointName],P.[UnitState],P.[AlarmID] FROM [dbo].[D_Signal] S 
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

        #endregion

    }
}
