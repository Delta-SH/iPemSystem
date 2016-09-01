using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
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

        public virtual List<string> GetEntities(Guid role) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(role);

            var entities = new List<string>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_AreaInRole_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    entities.Add(SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]));
                }
            }
            return entities;
        }

        public virtual void Insert(Guid role, List<string> keys) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar,100),
                                     new SqlParameter("@AreaId", SqlDbType.VarChar,100) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullGuidChecker(role);
                    foreach(var key in keys) {
                        parms[1].Value = SqlTypeConverter.DBNullStringHandler(key);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_AreaInRole_Repository_Insert, parms);
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
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_AreaInRole_Repository_Delete, parms);
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
