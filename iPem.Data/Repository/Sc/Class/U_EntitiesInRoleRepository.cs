using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc
{
    public partial class U_EntitiesInRoleRepository : IU_EntitiesInRoleRepository
    {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public U_EntitiesInRoleRepository(string databaseConnectionString)
        {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public U_EntitiesInRole GetEntitiesInRole(string id)
        {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entity = new U_EntitiesInRole() { RoleId = id, Menus = new List<int>(), Permissions = new List<EnmPermission>(), Authorizations = new List<string>() };
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_GetEntitiesInRole, parms))
            {
                while (rdr.Read())
                {
                    entity.Menus.Add(SqlTypeConverter.DBNullInt32Handler(rdr["MenuId"]));
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        entity.Permissions.Add(SqlTypeConverter.DBNullEnmPermissionHandler(rdr["Permission"]));
                    }
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        entity.Authorizations.Add(SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]));
                        entity.Type = EnmSSH.Area;
                    }
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        entity.Authorizations.Add(SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]));
                        entity.Type = EnmSSH.Station;
                    }
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        entity.Authorizations.Add(SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]));
                        entity.Type = EnmSSH.Room;
                    }
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        entity.Authorizations.Add(SqlTypeConverter.DBNullStringHandler(rdr["NodeId"]));
                        entity.Type = EnmSSH.Device;
                    }
                }
            }
            return entity;
        }

        public void Insert(U_EntitiesInRole entities)
        {
            SqlParameter[] parms1 = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100), new SqlParameter("@MenuId", SqlDbType.Int) };
            SqlParameter[] parms2 = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100), new SqlParameter("@Permission", SqlDbType.Int) };
            SqlParameter[] parms3 = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100), new SqlParameter("@NodeId", SqlDbType.VarChar, 100) };

            using (var conn = new SqlConnection(this._databaseConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (var entity in entities.Menus)
                    {
                        parms1[0].Value = SqlTypeConverter.DBNullStringChecker(entities.RoleId);
                        parms1[1].Value = SqlTypeConverter.DBNullInt32Checker(entity);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert1, parms1);
                    }

                    foreach (var entity in entities.Permissions)
                    {
                        parms2[0].Value = SqlTypeConverter.DBNullStringChecker(entities.RoleId);
                        parms2[1].Value = (int)entity;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert2, parms2);
                    }

                    foreach (var entity in entities.Authorizations)
                    {
                        parms3[0].Value = SqlTypeConverter.DBNullStringChecker(entities.RoleId);
                        parms3[1].Value = SqlTypeConverter.DBNullStringChecker(entity);
                        if (entities.Type == EnmSSH.Area)
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert3, parms3);
                        if (entities.Type == EnmSSH.Station)
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert4, parms3);
                        if (entities.Type == EnmSSH.Room)
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert5, parms3);
                        if (entities.Type == EnmSSH.Device)
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Insert6, parms3);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(string id)
        {
            SqlParameter[] parms = { new SqlParameter("@RoleId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            using (var conn = new SqlConnection(this._databaseConnectionString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_U_EntitiesInRole_Repository_Delete, parms);
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion

    }
}