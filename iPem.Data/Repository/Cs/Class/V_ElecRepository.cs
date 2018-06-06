using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class V_ElecRepository : IV_ElecRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public V_ElecRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<V_Elec> GetActive() {
            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetActive1, null)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetActive(EnmSSH type) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int) };
            parms[0].Value = (int)type;

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetActive2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetActive(string id, EnmSSH type) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = (int)type;

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetActive3, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetActive(EnmSSH type, EnmFormula formula) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int) };

            parms[0].Value = (int)type;
            parms[1].Value = (int)formula;

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetActive4, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetHistory(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory1, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetHistory(EnmSSH type, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = (int)type;
            parms[1].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory2, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetHistory(EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = (int)type;
            parms[1].Value = (int)formula;
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory3, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetHistory(string id, EnmSSH type, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = (int)type;
            parms[2].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Elec>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory4, parms)) {
                while(rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetHistory(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = (int)type;
            parms[2].Value = (int)formula;
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[4].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory5, parms)) {
                while(rdr.Read()) {
                    var entity = new V_Elec();
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

        public List<V_Elec> GetEachDay(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = (int)type;
            parms[2].Value = (int)formula;
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[4].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var entities = new List<V_Elec>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory6, parms)) {
                while (rdr.Read()) {
                    var entity = new V_Elec();
                    entity.Id = id;
                    entity.Type = type;
                    entity.FormulaType = formula;
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = entity.StartTime.AddDays(1);
                    entity.Value = SqlTypeConverter.DBNullDoubleHandler(rdr["Value"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public double GetTotal(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int),
                                     new SqlParameter("@Start", SqlDbType.DateTime),
                                     new SqlParameter("@End", SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
            parms[1].Value = (int)type;
            parms[2].Value = (int)formula;
            parms[3].Value = SqlTypeConverter.DBNullDateTimeHandler(start);
            parms[4].Value = SqlTypeConverter.DBNullDateTimeHandler(end);

            var total = 0d;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_V_Elec_Repository_GetHistory7, parms)) {
                if (rdr.Read()) {
                    total = SqlTypeConverter.DBNullDoubleHandler(rdr["Total"]);
                }
            }
            return total;
        }

        #endregion

    }
}
