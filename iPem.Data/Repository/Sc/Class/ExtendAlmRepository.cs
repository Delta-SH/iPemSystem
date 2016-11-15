using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class ExtendAlmRepository : IExtendAlmRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ExtendAlmRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<ExtAlm> GetEntities() {
            var entities = new List<ExtAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_ExtendAlm_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new ExtAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.Start = SqlTypeConverter.DBNullDateTimeHandler(rdr["Start"]);
                    entity.End = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["End"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Update(List<ExtAlm> entities) {
            SqlParameter[] parms = {   new SqlParameter("@Id", SqlDbType.VarChar,100),
                                       new SqlParameter("@FsuId", SqlDbType.VarChar,100),
                                       new SqlParameter("@Start", SqlDbType.DateTime),
                                       new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                       new SqlParameter("@Confirmed", SqlDbType.TinyInt),
                                       new SqlParameter("@Confirmer", SqlDbType.VarChar,200),
                                       new SqlParameter("@ConfirmedTime", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.FsuId);
                        parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.Start);
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.ProjectId);
                        parms[4].Value = (int)entity.Confirmed;
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.Confirmer);
                        parms[6].Value = SqlTypeConverter.DBNullDateTimeNullableChecker(entity.ConfirmedTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_ExtendAlm_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public List<ExtAlm> GetHisEntities(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@Start",SqlDbType.DateTime),
                                     new SqlParameter("@End",SqlDbType.DateTime) };

            parms[0].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
            parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(end);

            var entities = new List<ExtAlm>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_ExtendAlm_Repository_GetHisEntities, parms)) {
                while(rdr.Read()) {
                    var entity = new ExtAlm();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.Start = SqlTypeConverter.DBNullDateTimeHandler(rdr["Start"]);
                    entity.End = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["End"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.Confirmed = SqlTypeConverter.DBNullEnmConfirmStatusHandler(rdr["Confirmed"]);
                    entity.Confirmer = SqlTypeConverter.DBNullStringHandler(rdr["Confirmer"]);
                    entity.ConfirmedTime = SqlTypeConverter.DBNullDateTimeNullableHandler(rdr["ConfirmedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
