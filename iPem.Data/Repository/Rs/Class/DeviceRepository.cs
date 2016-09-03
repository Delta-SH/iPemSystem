using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
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

        public Device GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Device entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Device_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.FsuName = SqlTypeConverter.DBNullStringHandler(rdr["FsuName"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<Device> GetEntities(string parent) {
            SqlParameter[] parms = { new SqlParameter("@Parent", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(parent);

            var entities = new List<Device>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Device_Repository_GetEntitiesByParent, parms)) {
                while(rdr.Read()) {
                    var entity = new Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.FsuName = SqlTypeConverter.DBNullStringHandler(rdr["FsuName"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<Device> GetEntities() {
            var entities = new List<Device>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Device_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.FsuName = SqlTypeConverter.DBNullStringHandler(rdr["FsuName"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
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
