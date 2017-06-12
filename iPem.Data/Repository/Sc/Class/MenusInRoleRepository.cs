using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class MenusInRoleRepository : IMenusInRoleRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MenusInRoleRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual U_EntitiesInRole GetEntity(Guid role) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(role);

            var entity = new U_EntitiesInRole { RoleId = role, Menus = new List<U_Menu>() };
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_MenusInRole_Repository_GetEntities, parms)) {
                while(rdr.Read()) {
                    entity.Menus.Add(new U_Menu {
                        Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]),
                        Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]),
                        Icon = SqlTypeConverter.DBNullStringHandler(rdr["Icon"]),
                        Url = SqlTypeConverter.DBNullStringHandler(rdr["Url"]),
                        Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]),
                        Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]),
                        LastId = SqlTypeConverter.DBNullInt32Handler(rdr["LastId"]),
                        Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"])
                    });
                }
            }
            return entity;
        }

        public virtual void Insert(U_EntitiesInRole entity) {
            Insert(new List<U_EntitiesInRole> { entity });
        }

        public virtual void Insert(List<U_EntitiesInRole> entities) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar,100),
                                     new SqlParameter("@MenuId", SqlDbType.Int) };

            using(var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach(var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.RoleId);
                        foreach(var menu in entity.Menus) {
                            parms[1].Value = SqlTypeConverter.DBNullInt32Checker(menu.Id);
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_MenusInRole_Repository_Insert, parms);
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
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_MenusInRole_Repository_Delete, parms);
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