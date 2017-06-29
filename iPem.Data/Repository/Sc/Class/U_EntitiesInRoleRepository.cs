﻿using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class U_EntitiesInRoleRepository : IU_EntitiesInRoleRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public U_EntitiesInRoleRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public U_EntitiesInRole GetEntitiesInRole(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            var entity = new U_EntitiesInRole() { RoleId = id, Menus = new List<U_Menu>(), Areas = new List<string>(), Permissions = new List<EnmPermission>() };
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_GetEntitiesInRole, parms)) {
                while (rdr.Read()) {
                    var menu = new U_Menu();
                    menu.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    menu.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    menu.Icon = SqlTypeConverter.DBNullStringHandler(rdr["Icon"]);
                    menu.Url = SqlTypeConverter.DBNullStringHandler(rdr["Url"]);
                    menu.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    menu.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    menu.LastId = SqlTypeConverter.DBNullInt32Handler(rdr["LastId"]);
                    menu.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entity.Menus.Add(menu);
                }

                if (rdr.NextResult()) {
                    while (rdr.Read()) {
                        entity.Areas.Add(SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]));
                    }
                }

                if (rdr.NextResult()) {
                    while (rdr.Read()) {
                        entity.Permissions.Add(SqlTypeConverter.DBNullEnmPermissionHandler(rdr["Permission"]));
                    }
                }
            }
            return entity;
        }

        public void Insert(U_EntitiesInRole entities) {
            SqlParameter[] parms1 = { new SqlParameter("@RoleId", SqlDbType.VarChar,100), new SqlParameter("@MenuId", SqlDbType.Int) };
            SqlParameter[] parms2 = { new SqlParameter("@RoleId", SqlDbType.VarChar,100), new SqlParameter("@AreaId", SqlDbType.VarChar,100) };
            SqlParameter[] parms3 = { new SqlParameter("@RoleId", SqlDbType.VarChar,100), new SqlParameter("@Permission", SqlDbType.Int) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities.Menus) {
                        parms1[0].Value = SqlTypeConverter.DBNullGuidChecker(entities.RoleId);
                        parms1[1].Value = SqlTypeConverter.DBNullInt32Checker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert1, parms1);
                    }

                    foreach (var entity in entities.Areas) {
                        parms2[0].Value = SqlTypeConverter.DBNullGuidChecker(entities.RoleId);
                        parms2[1].Value = SqlTypeConverter.DBNullStringChecker(entity);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert2, parms2);
                    }

                    foreach (var entity in entities.Permissions) {
                        parms3[0].Value = SqlTypeConverter.DBNullGuidChecker(entities.RoleId);
                        parms3[1].Value = (int)entity;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert3, parms3);
                    }

                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Delete, parms);
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