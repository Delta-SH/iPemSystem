using iPem.Core.Domain.Cs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Cs {
    public partial class H_IDeviceRepository : IH_IDeviceRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public H_IDeviceRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public List<H_IDevice> GetDevicesInTypeId(string type) {
            SqlParameter[] parms = { new SqlParameter("@TypeId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(type);

            var entities = new List<H_IDevice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IDevice_Repository_GetDevicesInTypeId, parms)) {
                while (rdr.Read()) {
                    var entity = new H_IDevice();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_IDevice> GetDevicesInTypeName(string type) {
            SqlParameter[] parms = { new SqlParameter("@TypeName", SqlDbType.VarChar, 200) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(type);

            var entities = new List<H_IDevice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IDevice_Repository_GetDevicesInTypeName, parms)) {
                while (rdr.Read()) {
                    var entity = new H_IDevice();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_IDevice> GetDevicesInStation(string id) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<H_IDevice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IDevice_Repository_GetDevicesInParent, parms)) {
                while (rdr.Read()) {
                    var entity = new H_IDevice();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<H_IDevice> GetDevices() {
            var entities = new List<H_IDevice>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Cs.Sql_H_IDevice_Repository_GetDevices, null)) {
                while (rdr.Read()) {
                    var entity = new H_IDevice();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.TypeId = SqlTypeConverter.DBNullStringHandler(rdr["TypeId"]);
                    entity.TypeName = SqlTypeConverter.DBNullStringHandler(rdr["TypeName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
