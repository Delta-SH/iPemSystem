using iPem.Core.Domain.Am;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Am {
    public partial class AmDeviceRepository : IAmDeviceRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AmDeviceRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<AmDevice> GetEntities(string type) {
            SqlParameter[] parms = { new SqlParameter("@Type", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(type);

            var entities = new List<AmDevice>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Am.Sql_Device_Repository_GetEntitiesByType, parms)) {
                while(rdr.Read()) {
                    var entity = new AmDevice();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullStringHandler(rdr["Type"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<AmDevice> GetEntities() {
            var entities = new List<AmDevice>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Am.Sql_Device_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new AmDevice();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.Type = SqlTypeConverter.DBNullStringHandler(rdr["Type"]);
                    entity.ParentId = SqlTypeConverter.DBNullStringHandler(rdr["ParentId"]);
                    entity.CreatedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreatedTime"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
