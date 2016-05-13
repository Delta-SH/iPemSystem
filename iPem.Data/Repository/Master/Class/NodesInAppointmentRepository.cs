using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
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

        public virtual List<NodesInAppointment> GetEntities() {
            var entities = new List<NodesInAppointment>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_NodesInAppointment_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new NodesInAppointment();
                    entity.AppointmentId = SqlTypeConverter.DBNullGuidHandler(rdr["AppointmentId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmOrganizationHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual List<NodesInAppointment> GetEntities(EnmOrganization type) {
            SqlParameter[] parms = { new SqlParameter("@NodeType", SqlDbType.Int) };
            parms[0].Value = (int)type;

            var entities = new List<NodesInAppointment>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_NodesInAppointment_Repository_GetEntitiesByNodeType, parms)) {
                while(rdr.Read()) {
                    var entity = new NodesInAppointment();
                    entity.AppointmentId = SqlTypeConverter.DBNullGuidHandler(rdr["AppointmentId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmOrganizationHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual List<NodesInAppointment> GetEntities(Guid appointmentId) {
            SqlParameter[] parms = { new SqlParameter("@AppointmentId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(appointmentId);

            var entities = new List<NodesInAppointment>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_NodesInAppointment_Repository_GetEntitiesByAppointmentId, parms)) {
                while(rdr.Read()) {
                    var entity = new NodesInAppointment();
                    entity.AppointmentId = SqlTypeConverter.DBNullGuidHandler(rdr["AppointmentId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmOrganizationHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual void Insert(NodesInAppointment entity) {
            Insert(new List<NodesInAppointment> { entity });
        }

        public virtual void Insert(List<NodesInAppointment> entities) {
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
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.AppointmentId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.NodeId);
                        parms[2].Value = (int)entity.NodeType;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_NodesInAppointment_Repository_Insert, parms);
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
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_NodesInAppointment_Repository_Delete, parms);
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