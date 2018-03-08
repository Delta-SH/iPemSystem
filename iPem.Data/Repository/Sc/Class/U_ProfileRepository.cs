using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class U_ProfileRepository : IU_ProfileRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public U_ProfileRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public U_Profile Get(Guid uid, EnmProfile type) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);
            parms[1].Value = (int)type;

            U_Profile entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_Profile_Repository_GetProfile, parms)) {
                if(rdr.Read()) {
                    entity = new U_Profile {
                        UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]),
                        Type = SqlTypeConverter.DBNullProfileHandler(rdr["Type"]),
                        ValuesJson = SqlTypeConverter.DBNullStringHandler(rdr["ValuesJson"]),
                        ValuesBinary = SqlTypeConverter.DBNullBytesHandler(rdr["ValuesBinary"]),
                        LastUpdatedDate = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastUpdatedDate"])
                    };
                }
            }

            return entity;
        }

        public void Save(U_Profile entity) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Type", SqlDbType.Int),
                                     new SqlParameter("@ValuesJson", SqlDbType.NText),
                                     new SqlParameter("@ValuesBinary", SqlDbType.Image),
                                     new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                    parms[1].Value = (int)entity.Type;
                    parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.ValuesJson);
                    parms[3].Value = SqlTypeConverter.DBNullBytesChecker(entity.ValuesBinary);
                    parms[4].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastUpdatedDate);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_Profile_Repository_Save, parms);

                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(Guid uid, EnmProfile type) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100),
                                     new SqlParameter("@Type", SqlDbType.Int) };

            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);
            parms[1].Value = (int)type;

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_Profile_Repository_Delete, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Clear(Guid uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_Profile_Repository_Clear, parms);
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
