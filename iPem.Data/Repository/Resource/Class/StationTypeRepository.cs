using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class StationTypeRepository : IStationTypeRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationTypeRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public StationType GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            StationType entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_StationType_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new StationType();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                }
            }
            return entity;
        }

        public List<StationType> GetEntities() {
            var entities = new List<StationType>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_StationType_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new StationType();
                    entity.Id = SqlTypeConverter.DBNullInt32Handler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
