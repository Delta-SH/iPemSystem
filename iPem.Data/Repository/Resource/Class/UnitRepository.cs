using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class UnitRepository : IUnitRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UnitRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Unit GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Unit entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Unit_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Unit();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Unit> GetEntities() {
            var entities = new List<Unit>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Unit_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Unit();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
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
