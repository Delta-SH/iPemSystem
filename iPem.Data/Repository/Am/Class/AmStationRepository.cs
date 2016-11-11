using iPem.Core.Domain.Am;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Am {
    public partial class AmStationRepository : IAmStationRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AmStationRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<AmStation> GetEntities(string type) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(type);

            var entities = new List<AmStation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Am.Sql_Station_Repository_GetEntitiesByType, parms)) {
                while(rdr.Read()) {
                    var entity = new AmStation();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullStringHandler(rdr["Type"]);
                    entity.Parent = SqlTypeConverter.DBNullStringHandler(rdr["Parent"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<AmStation> GetEntities() {
            var entities = new List<AmStation>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Am.Sql_Station_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new AmStation();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullStringHandler(rdr["Type"]);
                    entity.Parent = SqlTypeConverter.DBNullStringHandler(rdr["Parent"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
