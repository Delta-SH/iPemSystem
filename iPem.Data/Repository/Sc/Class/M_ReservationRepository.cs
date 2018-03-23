using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class M_ReservationRepository : IM_ReservationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public M_ReservationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public M_Reservation GetReservation(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            M_Reservation entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_GetReservation, parms)) {
                if (rdr.Read()) {
                    entity = new M_Reservation();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.ExpStartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ExpStartTime"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.UserId = SqlTypeConverter.DBNullStringHandler(rdr["UserId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Status = SqlTypeConverter.DBNullResultHandler(rdr["Status"]);
                }
            }
            return entity;
        }

        public List<M_Reservation> GetReservations() {
            var entities = new List<M_Reservation>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_GetReservations, null)) {
                while (rdr.Read()) {
                    var entity = new M_Reservation();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.ExpStartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ExpStartTime"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.UserId = SqlTypeConverter.DBNullStringHandler(rdr["UserId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Status = SqlTypeConverter.DBNullResultHandler(rdr["Status"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_Reservation> GetReservationsInSpan(DateTime start, DateTime end) {
            SqlParameter[] parms = { new SqlParameter("@expStartTime", SqlDbType.DateTime), 
                                     new SqlParameter("@endTime", SqlDbType.DateTime) };

            parms[0].Value = start;
            parms[1].Value = end;

            var entities = new List<M_Reservation>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_GetReservationsInSpan, parms)) {
                while (rdr.Read()) {
                    var entity = new M_Reservation();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.ExpStartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ExpStartTime"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProjectId = SqlTypeConverter.DBNullStringHandler(rdr["ProjectId"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.UserId = SqlTypeConverter.DBNullStringHandler(rdr["UserId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Status = SqlTypeConverter.DBNullResultHandler(rdr["Status"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<M_Reservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@ExpStartTime",SqlDbType.DateTime),
                                     new SqlParameter("@EndTime", SqlDbType.DateTime),
                                     new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Creator", SqlDbType.VarChar,100),
                                     new SqlParameter("@UserId",SqlDbType.VarChar,100),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit),
                                     new SqlParameter("@Status",SqlDbType.Int) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ExpStartTime);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.EndTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.ProjectId);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                        parms[6].Value = SqlTypeConverter.DBNullStringHandler(entity.UserId);
                        parms[7].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[8].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[9].Value = entity.Enabled;
                        parms[10].Value = (int)EnmResult.Undefine;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<M_Reservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,200),
                                     new SqlParameter("@ExpStartTime",SqlDbType.DateTime),
                                     new SqlParameter("@EndTime", SqlDbType.DateTime),
                                     new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Creator", SqlDbType.VarChar,100),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit),
                                     new SqlParameter("@Status",SqlDbType.Int)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.ExpStartTime);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.EndTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.ProjectId);
                        parms[5].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                        parms[6].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[7].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[8].Value = entity.Enabled;
                        parms[9].Value = (int)EnmResult.Undefine;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<M_Reservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_Delete, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Check(String id, DateTime start, EnmResult status) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100), 
                                     new SqlParameter("@StartTime", SqlDbType.DateTime), 
                                     new SqlParameter("@Status", SqlDbType.Int)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
                    parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(start);
                    parms[2].Value = (int)status;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_Reservation_Repository_Check, parms);
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