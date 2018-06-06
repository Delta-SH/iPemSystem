using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class V_ACabinetRepository : IV_ACabinetRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_ACabinetRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_ACabinet> GetHistory(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_ACabinet>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ACabinet_Repository_GetHistory1, parms)) {
                while(rdr.Read()) {
                    var entity = new V_ACabinet();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.AValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AValue"]);
                    entity.AValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AValueTime"]);
                    entity.BValue = SqlTypeConverter.DBNullDoubleHandler(rdr["BValue"]);
                    entity.BValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BValueTime"]);
                    entity.CValue = SqlTypeConverter.DBNullDoubleHandler(rdr["CValue"]);
                    entity.CValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CValueTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_ACabinet> GetHistory(EnmVSignalCategory category, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@Category", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)category;

            var entities = new List<V_ACabinet>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ACabinet_Repository_GetHistory2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_ACabinet();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.AValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AValue"]);
                    entity.AValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AValueTime"]);
                    entity.BValue = SqlTypeConverter.DBNullDoubleHandler(rdr["BValue"]);
                    entity.BValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BValueTime"]);
                    entity.CValue = SqlTypeConverter.DBNullDoubleHandler(rdr["CValue"]);
                    entity.CValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CValueTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_ACabinet> GetHistory(string device, EnmVSignalCategory category, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@Category", SqlDbType.Int),
                                     new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)category;
            parms[3].Value = SqlTypeConverter.DBNullStringHandler(device);

            var entities = new List<V_ACabinet>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ACabinet_Repository_GetHistory3, parms)) {
                while (rdr.Read()) {
                    var entity = new V_ACabinet();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.AValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AValue"]);
                    entity.AValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AValueTime"]);
                    entity.BValue = SqlTypeConverter.DBNullDoubleHandler(rdr["BValue"]);
                    entity.BValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BValueTime"]);
                    entity.CValue = SqlTypeConverter.DBNullDoubleHandler(rdr["CValue"]);
                    entity.CValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CValueTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_ACabinet> GetHistory(string device, string point, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@DeviceId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = SqlTypeConverter.DBNullStringHandler(device);
            parms[3].Value = SqlTypeConverter.DBNullStringHandler(point);

            var entities = new List<V_ACabinet>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ACabinet_Repository_GetHistory4, parms)) {
                while (rdr.Read()) {
                    var entity = new V_ACabinet();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.AValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AValue"]);
                    entity.AValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AValueTime"]);
                    entity.BValue = SqlTypeConverter.DBNullDoubleHandler(rdr["BValue"]);
                    entity.BValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BValueTime"]);
                    entity.CValue = SqlTypeConverter.DBNullDoubleHandler(rdr["CValue"]);
                    entity.CValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CValueTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_ACabinet> GetLast(string device, EnmVSignalCategory category, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@Category", SqlDbType.Int),
                                     new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)category;
            parms[3].Value = SqlTypeConverter.DBNullStringHandler(device);

            var entities = new List<V_ACabinet>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ACabinet_Repository_GetLast1, parms)) {
                while (rdr.Read()) {
                    var entity = new V_ACabinet();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.AValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AValue"]);
                    entity.AValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AValueTime"]);
                    entity.BValue = SqlTypeConverter.DBNullDoubleHandler(rdr["BValue"]);
                    entity.BValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BValueTime"]);
                    entity.CValue = SqlTypeConverter.DBNullDoubleHandler(rdr["CValue"]);
                    entity.CValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CValueTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_ACabinet> GetLast(EnmVSignalCategory category, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@Category", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)category;

            var entities = new List<V_ACabinet>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_ACabinet_Repository_GetLast2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_ACabinet();
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.Category = SqlTypeConverter.DBNullVSignalCategoryHandler(rdr["Category"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entity.ValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ValueTime"]);
                    entity.AValue = SqlTypeConverter.DBNullDoubleHandler(rdr["AValue"]);
                    entity.AValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["AValueTime"]);
                    entity.BValue = SqlTypeConverter.DBNullDoubleHandler(rdr["BValue"]);
                    entity.BValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["BValueTime"]);
                    entity.CValue = SqlTypeConverter.DBNullDoubleHandler(rdr["CValue"]);
                    entity.CValueTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CValueTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }
        #endregion

    }
}
