﻿using iPem.Core.Domain.Sc;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Sc {
    public partial class RoleRepository : IRoleRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoleRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public virtual Role GetEntity(Guid id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(id);

            Role entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_GetEntity1, parms)) {
                if(rdr.Read()) {
                    entity = new Role();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public virtual Role GetEntity(string name) {
            SqlParameter[] parms = { new SqlParameter("@Name", name) };
            Role entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_GetEntity2, parms)) {
                if (rdr.Read()) {
                    entity = new Role();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public virtual Role GetEntityByUid(Guid uid) {
            SqlParameter[] parms = { new SqlParameter("@UserId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullGuidChecker(uid);

            Role entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_GetEntityByUid, parms)) {
                if(rdr.Read()) {
                    entity = new Role();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public virtual List<Role> GetEntities() {
            var entities = new List<Role>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_GetEntities, null)) {
                while (rdr.Read()) {
                    var entity = new Role();
                    entity.Id = SqlTypeConverter.DBNullGuidHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public virtual void Insert(Role entity) {
            Insert(new List<Role>() { entity });
        }

        public virtual void Insert(List<Role> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,100),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[3].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Update(Role entity) {
            Update(new List<Role>() { entity });
        }

        public virtual void Update(List<Role> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar,100),
                                     new SqlParameter("@Name", SqlDbType.VarChar,100),
                                     new SqlParameter("@Comment", SqlDbType.VarChar,512),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.Name);
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        parms[3].Value = entity.Enabled;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(Role entity) {
            Delete(new List<Role>() { entity });
        }

        public virtual void Delete(List<Role> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullGuidChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Sc.Sql_Role_Repository_Delete, parms);
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