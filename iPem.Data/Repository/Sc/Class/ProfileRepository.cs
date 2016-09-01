using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class ProfileRepository : IProfileRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProfileRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual UserProfile GetEntity(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            UserProfile entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Profile_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new UserProfile() {
                        UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]),
                        ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]),
                        ValuesBinary = SqlTypeConverter.DBNullBytesHandler(rdr["ValuesBinary"]),
                        LastUpdatedDate = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastUpdatedDate"])
                    };
                }
            }
            return entity;
        }

        public virtual void Save(UserProfile entity) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@ValuesJson", SqlDbType.NText),
                                     new SqlParameter("@ValuesBinary", SqlDbType.Image),
                                     new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                    parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.ValuesJson);
                    parms[2].Value = SqlTypeConverter.DBNullBytesChecker(entity.ValuesBinary);
                    parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastUpdatedDate);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Profile_Repository_Save, parms);

                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Profile_Repository_Delete, parms);
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
