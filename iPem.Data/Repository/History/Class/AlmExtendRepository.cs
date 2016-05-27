using iPem.Core.Domain.History;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.History {
    public partial class AlmExtendRepository : IAlmExtendRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AlmExtendRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the entities
        /// </summary>
        /// <param name="entities">entities</param>
        public void Update(List<AlmExtend> entities) {
            SqlParameter[] parms = {   new SqlParameter("@Id", SqlDbType.VarChar,100),
                                       new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                       new SqlParameter("@ConfirmedStatus", SqlDbType.Int),
                                       new SqlParameter("@ConfirmedTime", SqlDbType.DateTime),
                                       new SqlParameter("@Confirmer", SqlDbType.VarChar,200) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.ProjectId);
                        parms[2].Value = (int)entity.ConfirmedStatus;
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeNullableChecker(entity.ConfirmedTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Confirmer);

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Hs.Sql_AlmExtend_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Update the confirm fields
        /// </summary>
        /// <param name="entities">entities</param>
        public void UpdateConfirm(List<AlmExtend> entities) {
            SqlParameter[] parms = {   new SqlParameter("@Id", SqlDbType.VarChar,100),
                                       new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                       new SqlParameter("@ConfirmedStatus", SqlDbType.Int),
                                       new SqlParameter("@ConfirmedTime", SqlDbType.DateTime),
                                       new SqlParameter("@Confirmer", SqlDbType.VarChar,200) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.ProjectId);
                        parms[2].Value = (int)entity.ConfirmedStatus;
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeNullableChecker(entity.ConfirmedTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Confirmer);

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Hs.Sql_AlmExtend_Repository_UpdateConfirm, parms);
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
