using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class M_NodesInReservationRepository : IM_NodesInReservationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public M_NodesInReservationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Method

        public List<M_NodeInReservation> GetNodesInReservations() {
            var entities = new List<M_NodeInReservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_NodesInReservation_Repository_GetNodesInReservations, null)) {
                while(rdr.Read()) {
                    var entity = new M_NodeInReservation();
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmSSHHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_NodeInReservation> GetNodesInReservationsInType(EnmSSH type) {
            SqlParameter[] parms = { new SqlParameter("@NodeType", SqlDbType.Int) };
            parms[0].Value = (int)type;

            var entities = new List<M_NodeInReservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_NodesInReservation_Repository_GetNodesInReservationsInType, parms)) {
                while(rdr.Read()) {
                    var entity = new M_NodeInReservation();
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmSSHHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<M_NodeInReservation> GetNodesInReservationsInReservation(string id) {
            SqlParameter[] parms = { new SqlParameter("@ReservationId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<M_NodeInReservation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_M_NodesInReservation_Repository_GetNodesInReservationsInReservation, parms)) {
                while(rdr.Read()) {
                    var entity = new M_NodeInReservation();
                    entity.ReservationId = SqlTypeConverter.DBNullStringHandler(rdr["ReservationId"]);
                    entity.NodeId = SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]);
                    entity.NodeType = SqlTypeConverter.DBNullEnmSSHHandler(rdr["NodeType"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void Insert(IList<M_NodeInReservation> entities) {
            SqlParameter[] parms = { new SqlParameter("@ReservationId", SqlDbType.VarChar,100),
                                     new SqlParameter("@NodeId", SqlDbType.VarChar,100),
                                     new SqlParameter("@NodeType", SqlDbType.Int) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.ReservationId);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.NodeId);
                        parms[2].Value = (int)entity.NodeType;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_NodesInReservation_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<string> entities) {
            SqlParameter[] parms = { new SqlParameter("@ReservationId", SqlDbType.VarChar, 100) };
            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var id in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_M_NodesInReservation_Repository_Delete, parms);
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