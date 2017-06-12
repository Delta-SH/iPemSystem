using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class AppointmentRepository : IAppointmentRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AppointmentRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual List<M_Reservation> GetEntities() {
            var entities = new List<M_Reservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Appointment_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new M_Reservation();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProjectId = SqlTypeConverter.DBNullGuidHandler(rdr["ProjectId"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual List<M_Reservation> GetEntities(DateTime startTime, DateTime endTime) {
            SqlParameter[] parms = { 
                                       new SqlParameter("@startTime", SqlDbType.DateTime), 
                                       new SqlParameter("@endTime", SqlDbType.DateTime) 
                                   };
            parms[0].Value = startTime;
            parms[1].Value = endTime;

            var entities = new List<M_Reservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Appointment_Repository_GetEntitiesByDate, parms)) {
                while(rdr.Read()) {
                    var entity = new M_Reservation();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProjectId = SqlTypeConverter.DBNullGuidHandler(rdr["ProjectId"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual M_Reservation GetEntity(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);
            M_Reservation entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Appointment_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new M_Reservation();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.EndTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["EndTime"]);
                    entity.ProjectId = SqlTypeConverter.DBNullGuidHandler(rdr["ProjectId"]);
                    entity.Creator = SqlTypeConverter.DBNullStringHandler(rdr["Creator"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public virtual void Insert(M_Reservation entity) {
            Insert(new List<M_Reservation> { entity });
        }

        public virtual void Insert(List<M_Reservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@StartTime", SqlDbType.DateTime),
                                     new SqlParameter("@EndTime", SqlDbType.DateTime),
                                     new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Creator", SqlDbType.VarChar,100),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.StartTime);
                        parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.EndTime);
                        parms[3].Value = SqlTypeConverter.DBNullGuidChecker(entity.ProjectId);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                        parms[5].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[7].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Appointment_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Update(M_Reservation entity) {
            Update(new List<M_Reservation> { entity });
        }

        public virtual void Update(List<M_Reservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@StartTime", SqlDbType.DateTime),
                                     new SqlParameter("@EndTime", SqlDbType.DateTime),
                                     new SqlParameter("@ProjectId", SqlDbType.VarChar,100),
                                     new SqlParameter("@Creator", SqlDbType.VarChar,100),
                                     new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.StartTime);
                        parms[2].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.EndTime);
                        parms[3].Value = SqlTypeConverter.DBNullGuidChecker(entity.ProjectId);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Creator);
                        parms[5].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedTime);
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[7].Value = entity.Enabled;

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Appointment_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(M_Reservation entity) {
            Delete(new List<M_Reservation> { entity });
        }

        public virtual void Delete(List<M_Reservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Appointment_Repository_Delete, parms);
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