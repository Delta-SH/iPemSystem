using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class U_FollowPointRepository : IU_FollowPointRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public U_FollowPointRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<U_FollowPoint> GetFollowPointsInUser(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            var entities = new List<U_FollowPoint>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_FollowPoint_Repository_GetFollowPointsInUser, parms)) {
                while (rdr.Read()) {
                    var entity = new U_FollowPoint();
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<U_FollowPoint> GetFollowPoints() {
            var entities = new List<U_FollowPoint>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_FollowPoint_Repository_GetFollowPoints, null)) {
                while (rdr.Read()) {
                    var entity = new U_FollowPoint();
                    entity.UserId = SqlTypeConverter.DBNullGuidHandler(rdr["UserId"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<U_FollowPoint> entities) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceId);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.PointId);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_FollowPoint_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<U_FollowPoint> entities) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar,100),
                                     new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.UserId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceId);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.PointId);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_FollowPoint_Repository_Delete, parms);
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
