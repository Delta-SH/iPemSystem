using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class V_OfflineRepository : IV_OfflineRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_OfflineRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_Offline> GetActive() {
            var entities = new List<V_Offline>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Offline_Repository_GetActive1, null)) {
                while (rdr.Read()) {
                    var entity = new V_Offline();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_Offline> GetActive(EnmFormula ftype) {
            SqlParameter[] parms = { new SqlParameter("@FormulaType", SqlDbType.Int) };
            parms[0].Value = (int)ftype;

            var entities = new List<V_Offline>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Offline_Repository_GetActive2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Offline();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_Offline> GetActive(EnmSSH type, EnmFormula ftype) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int) };

            parms[0].Value = (int)type;
            parms[1].Value = (int)ftype;

            var entities = new List<V_Offline>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Offline_Repository_GetActive3, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Offline();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_Offline> GetHistory(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Offline>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Offline_Repository_GetHistory1, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Offline();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_Offline> GetHistory(EnmFormula ftype, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@FormulaType", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)ftype;

            var entities = new List<V_Offline>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Offline_Repository_GetHistory2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Offline();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<V_Offline> GetHistory(EnmSSH type, EnmFormula ftype, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);
            parms[2].Value = (int)type;
            parms[3].Value = (int)ftype;

            var entities = new List<V_Offline>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Offline_Repository_GetHistory3, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Offline();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
