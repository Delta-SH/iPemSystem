using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class V_BatCurveRepository : IV_BatCurveRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_BatCurveRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_BatCurve> GetProcedures(string device, int pack, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PackId", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullInt32Checker(pack);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetProcedures1, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatCurve> GetProcedures(string device, int pack, EnmBatPoint ptype, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PackId", SqlDbType.Int),
                                     new SqlParameter("@PType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullInt32Checker(pack);
            parms[2].Value = (int)ptype;
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[4].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetProcedures2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatCurve> GetValues(string device, int pack, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PackId", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullInt32Checker(pack);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetValues1, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatCurve> GetValues(string device, int pack, EnmBatPoint ptype, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PackId", SqlDbType.Int),
                                     new SqlParameter("@PType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);
            parms[1].Value = SqlTypeConverter.DBNullInt32Checker(pack);
            parms[2].Value = (int)ptype;
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[4].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetValues2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatCurve> GetValues(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetValues, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatCurve> GetMinValues(EnmBatStatus type, EnmBatPoint ptype, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@PType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = (int)type;
            parms[1].Value = (int)ptype;
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetMinValues, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_BatCurve> GetMaxValues(EnmBatStatus type, EnmBatPoint ptype, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@PType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = (int)type;
            parms[1].Value = (int)ptype;
            parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<V_BatCurve>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_BatCurve_Repository_GetMaxValues, parms)) {
                while (rdr.Read()) {
                    var entity = new V_BatCurve();
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.PackId = SqlTypeConverter.DBNullInt32Handler(rdr["PackId"]);
                    entity.Type = SqlTypeConverter.DBNullBatStatusHandler(rdr["Type"]);
                    entity.PType = SqlTypeConverter.DBNullBatPointHandler(rdr["PType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.ProcTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ProcTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion
        
    }
}
