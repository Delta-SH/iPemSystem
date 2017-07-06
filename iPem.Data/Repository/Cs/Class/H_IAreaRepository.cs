using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class H_IAreaRepository : IH_IAreaRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_IAreaRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_IArea> GetAreasInTypeId(string type) {
            SqlParameter[] parms = { new SqlParameter("@TypeId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(type);

            var entities = new List<H_IArea>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IArea_Repository_GetAreasInTypeId, parms)) {
                while (rdr.Read()) {
                    var entity = new H_IArea();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_IArea> GetAreasInTypeName(string type) {
            SqlParameter[] parms = { new SqlParameter("@TypeName", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(type);

            var entities = new List<H_IArea>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IArea_Repository_GetAreasInTypeName, parms)) {
                while (rdr.Read()) {
                    var entity = new H_IArea();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_IArea> GetAreasInParent(string parent) {
            SqlParameter[] parms = { new SqlParameter("@ParentId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(parent);

            var entities = new List<H_IArea>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IArea_Repository_GetAreasInParent, parms)) {
                while (rdr.Read()) {
                    var entity = new H_IArea();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_IArea> GetAreas() {
            var entities = new List<H_IArea>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IArea_Repository_GetAreas, null)) {
                while (rdr.Read()) {
                    var entity = new H_IArea();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
