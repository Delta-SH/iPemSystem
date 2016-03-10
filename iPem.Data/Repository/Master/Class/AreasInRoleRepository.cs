using iPem.Core.Domain.Master;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class AreasInRoleRepository : IAreasInRoleRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AreasInRoleRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual AreasInRole GetEntities(Guid role) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(role);

            var entity = new AreasInRole { RoleId = role, AreaIds = new List<string>() };
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_AreaInRole_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    entity.AreaIds.Add(SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]));
                }
            }
            return entity;
        }

        public virtual void Insert(AreasInRole entity) {
            Insert(new List<AreasInRole> { entity });
        }

        public virtual void Insert(List<AreasInRole> entities) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar,100),
                                     new SqlParameter("@AreaId", SqlDbType.VarChar,100) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.RoleId);
                        foreach(var id in entity.AreaIds) {
                            parms[1].Value = SqlTypeConverter.DBNullStringHandler(id);
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_AreaInRole_Repository_Insert, parms);
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
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar,100) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var id in roles) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Cs.Sql_AreaInRole_Repository_Delete, parms);
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
