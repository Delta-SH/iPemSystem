using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class OperateInRoleRepository : IOperateInRoleRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public OperateInRoleRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual OperateInRole GetEntity(Guid role) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(role);

            var entity = new OperateInRole { RoleId = role, Operates = new List<EnmOperation>() };
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_OperateInRole_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    entity.Operates.Add(SqlTypeConverter.DBNullEnmOperationHandler(rdr["OperateId"]));
                }
            }
            return entity;
        }

        public virtual void Insert(OperateInRole entity) {
            Insert(new List<OperateInRole> { entity });
        }

        public virtual void Insert(List<OperateInRole> entities) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar,100),
                                     new SqlParameter("@OperateId", SqlDbType.Int) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.RoleId);
                        foreach(var id in entity.Operates) {
                            parms[1].Value = (Int32)id;
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_OperateInRole_Repository_Insert, parms);
                        }
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(Guid role) {
            Delete(new List<Guid> { role });
        }

        public virtual void Delete(List<Guid> roles) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var id in roles) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_OperateInRole_Repository_Delete, parms);
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
