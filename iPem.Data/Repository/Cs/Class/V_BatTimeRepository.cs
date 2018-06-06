using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class V_BatTimeRepository : IV_BatTimeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_BatTimeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_BatTime> GetValues(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_BatTime>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatTime_Repository_GetValues1, parms)) {
                while(rdr.Read()) {
                    var entity = new V_BatTime();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatTime> GetValues(DateTime start, DateTime end, EnmBatStatus type) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@Type", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)type;

            var entities = new List<V_BatTime>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatTime_Repository_GetValues2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatTime();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatTime> GetProcedures(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_BatTime>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatTime_Repository_GetProcedures1, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatTime();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatTime> GetProcedures(string device, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_BatTime>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatTime_Repository_GetProcedures2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatTime();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
