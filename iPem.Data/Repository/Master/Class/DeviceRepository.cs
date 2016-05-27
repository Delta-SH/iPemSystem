using iPem.Core.Domain.Master;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Master {
    public partial class DeviceRepository : IDeviceRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<Device> GetEntities() {
            var entities = new List<Device>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_Device_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.ProtcolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtcolId"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
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
