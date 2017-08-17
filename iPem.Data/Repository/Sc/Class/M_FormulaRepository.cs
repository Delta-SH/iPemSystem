using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class M_FormulaRepository : IM_FormulaRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public M_FormulaRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public M_Formula GetFormula(string id, EnmSSH type, EnmFormula formulaType) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int) };
            parms[0].Value = id;
            parms[1].Value = (int)type;
            parms[2].Value = (int)formulaType;

            M_Formula entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Formula_Repository_GetFormula, parms)) {
                if(rdr.Read()) {
                    entity = new M_Formula();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.ComputeType = SqlTypeConverter.DBNullEnmComputeHandler(rdr["ComputeType"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["Formula"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                }
            }
            return entity;
        }

        public List<M_Formula> GetFormulas(string id, EnmSSH type) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int) };
            parms[0].Value = id;
            parms[1].Value = (int)type;

            var entities = new List<M_Formula>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Formula_Repository_GetFormulas, parms)) {
                while(rdr.Read()) {
                    var entity = new M_Formula();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.ComputeType = SqlTypeConverter.DBNullEnmComputeHandler(rdr["ComputeType"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["Formula"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_Formula> GetAllFormulas() {
            var entities = new List<M_Formula>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Formula_Repository_GetAllFormulas, null)) {
                while(rdr.Read()) {
                    var entity = new M_Formula();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Type = SqlTypeConverter.DBNullEnmSSHHandler(rdr["Type"]);
                    entity.FormulaType = SqlTypeConverter.DBNullEnmFormulaHandler(rdr["FormulaType"]);
                    entity.ComputeType = SqlTypeConverter.DBNullEnmComputeHandler(rdr["ComputeType"]);
                    entity.FormulaText = SqlTypeConverter.DBNullStringHandler(rdr["Formula"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Save(IList<M_Formula> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@FormulaType", SqlDbType.Int),
                                     new SqlParameter("@ComputeType", SqlDbType.Int),
                                     new SqlParameter("@Formula", SqlDbType.VarChar),
                                     new SqlParameter("@Comment", SqlDbType.VarChar, 1024),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime)};

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = (int)entity.Type;
                        parms[2].Value = (int)entity.FormulaType;
                        parms[3].Value = (int)entity.ComputeType;
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.FormulaText);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[6].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Formula_Repository_Save, parms);
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
