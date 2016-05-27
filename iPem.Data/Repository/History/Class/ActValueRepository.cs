using iPem.Core.Domain.History;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.History {
    public partial class ActValueRepository : IActValueRepository {

        #region Fields

        private readonly string _databaseConnectionString;
        private const string _delimiter = "┆";

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ActValueRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>active value list</returns>
        public List<ActValue> GetEntities(string device) {
            SqlParameter[] parms = { new SqlParameter("@DeviceId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(device);

            var entities = new List<ActValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActValue_Repository_GetEntitiesByDevice, parms)) {
                while(rdr.Read()) {
                    var entity = new ActValue();
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.MeasuredVal = SqlTypeConverter.DBNullDoubleHandler(rdr["MeasuredVal"]);
                    entity.SetupVal = SqlTypeConverter.DBNullDoubleHandler(rdr["SetupVal"]);
                    entity.Status = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["Status"]);
                    entity.RecordTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RecordTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="devices">the device identifier array</param>
        /// <returns>active value list</returns>
        public List<ActValue> GetEntities(string[] devices) {
            SqlParameter[] parms = { new SqlParameter("@Devices", SqlDbType.VarChar),
                                     new SqlParameter("@Delimiter", SqlDbType.VarChar, 10) };

            parms[0].Value = string.Join(_delimiter, devices);
            parms[1].Value = _delimiter;

            var entities = new List<ActValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActValue_Repository_GetEntitiesByDevices, parms)) {
                while(rdr.Read()) {
                    var entity = new ActValue();
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.MeasuredVal = SqlTypeConverter.DBNullDoubleHandler(rdr["MeasuredVal"]);
                    entity.SetupVal = SqlTypeConverter.DBNullDoubleHandler(rdr["SetupVal"]);
                    entity.Status = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["Status"]);
                    entity.RecordTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RecordTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>active value list</returns>
        public List<ActValue> GetEntities() {
            var entities = new List<ActValue>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Hs.Sql_ActValue_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new ActValue();
                    entity.DeviceCode = SqlTypeConverter.DBNullStringHandler(rdr["DeviceCode"]);
                    entity.DeviceId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceId"]);
                    entity.PointId = SqlTypeConverter.DBNullStringHandler(rdr["PointId"]);
                    entity.MeasuredVal = SqlTypeConverter.DBNullDoubleHandler(rdr["MeasuredVal"]);
                    entity.SetupVal = SqlTypeConverter.DBNullDoubleHandler(rdr["SetupVal"]);
                    entity.Status = SqlTypeConverter.DBNullEnmPointStatusHandler(rdr["Status"]);
                    entity.RecordTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["RecordTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        /// <summary>
        /// Add an entity to the repository
        /// </summary>
        /// <param name="entity">entity</param>
        public void Insert(ActValue entity) {
            Insert(new List<ActValue>() { entity });
        }

        /// <summary>
        /// Add the entities to the repository
        /// </summary>
        /// <param name="entities">entities</param>
        public void Insert(List<ActValue> entities) {
            SqlParameter[] parms = { new SqlParameter("@DeviceCode", SqlDbType.VarChar,100),
                                     new SqlParameter("@DeviceId", SqlDbType.VarChar,100),
                                     new SqlParameter("@PointId", SqlDbType.VarChar,100),
                                     new SqlParameter("@MeasuredVal", SqlDbType.Float),
                                     new SqlParameter("@SetupVal", SqlDbType.Float),
                                     new SqlParameter("@Status", SqlDbType.Int),
                                     new SqlParameter("@RecordTime", SqlDbType.DateTime) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceCode);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.DeviceId);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.PointId);
                        parms[3].Value = SqlTypeConverter.DBNullDoubleChecker(entity.MeasuredVal);
                        parms[4].Value = SqlTypeConverter.DBNullDoubleChecker(entity.SetupVal);
                        parms[5].Value = (int)entity.Status;
                        parms[6].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.RecordTime);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Hs.Sql_ActValue_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Clear the repository
        /// </summary>
        public void Clear() {
            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Hs.Sql_ActValue_Repository_Clear, null);
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
