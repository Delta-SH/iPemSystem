using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class NodesInAppointmentRepository : INodesInAppointmentRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NodesInAppointmentRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Method

        public virtual List<M_NodeInReservation> GetEntities() {
            var entities = new List<M_NodeInReservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_NodesInAppointment_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new M_NodeInReservation();
                    entity.ReservationId = SqlTypeConverter.DBNullGuidHandler(rdr["AppointmentId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmSSHHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual List<M_NodeInReservation> GetEntities(EnmSSH type) {
            SqlParameter[] parms = { new SqlParameter("@NodeType", SqlDbType.Int) };
            parms[0].Value = (int)type;

            var entities = new List<M_NodeInReservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_NodesInAppointment_Repository_GetEntitiesByNodeType, parms)) {
                while(rdr.Read()) {
                    var entity = new M_NodeInReservation();
                    entity.ReservationId = SqlTypeConverter.DBNullGuidHandler(rdr["AppointmentId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmSSHHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual List<M_NodeInReservation> GetEntities(Guid appointmentId) {
            SqlParameter[] parms = { new SqlParameter("@AppointmentId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(appointmentId);

            var entities = new List<M_NodeInReservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_NodesInAppointment_Repository_GetEntitiesByAppointmentId, parms)) {
                while(rdr.Read()) {
                    var entity = new M_NodeInReservation();
                    entity.ReservationId = SqlTypeConverter.DBNullGuidHandler(rdr["AppointmentId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmSSHHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual void Insert(M_NodeInReservation entity) {
            Insert(new List<M_NodeInReservation> { entity });
        }

        public virtual void Insert(List<M_NodeInReservation> entities) {
            SqlParameter[] parms = { 
                                     new SqlParameter("@AppointmentId", SqlDbType.VarChar,100),
                                     new SqlParameter("@NodeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@NodeType", SqlDbType.Int)
                                   };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.ReservationId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.NodeId);
                        parms[2].Value = (int)entity.NodeType;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_NodesInAppointment_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(Guid appointmentId) {
            Delete(new List<Guid> { appointmentId });
        }

        public virtual void Delete(List<Guid> appointmentIds) {
            SqlParameter[] parms = { new SqlParameter("@AppointmentId", SqlDbType.VarChar, 100) };
            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var id in appointmentIds) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_NodesInAppointment_Repository_Delete, parms);
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