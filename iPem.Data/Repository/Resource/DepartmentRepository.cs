using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class DepartmentRepository : IDepartmentRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DepartmentRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Department GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Department entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Department_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Department();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.TypeDesc = SqlTypeConverter.DBNullStringHandler(rdr["TypeDesc"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.PostCode = SqlTypeConverter.DBNullStringHandler(rdr["PostCode"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public Department GetEntityByCode(string code) {
            SqlParameter[] parms = { new SqlParameter("@Code", SqlDbType.VarChar, 50) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(code);

            Department entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Department_Repository_GetEntityByCode, parms)) {
                if(rdr.Read()) {
                    entity = new Department();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.TypeDesc = SqlTypeConverter.DBNullStringHandler(rdr["TypeDesc"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.PostCode = SqlTypeConverter.DBNullStringHandler(rdr["PostCode"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public IList<Department> GetEntities() {
            var entities = new List<Department>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Department_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Department();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.TypeDesc = SqlTypeConverter.DBNullStringHandler(rdr["TypeDesc"]);
                    entity.Phone = SqlTypeConverter.DBNullStringHandler(rdr["Phone"]);
                    entity.PostCode = SqlTypeConverter.DBNullStringHandler(rdr["PostCode"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
