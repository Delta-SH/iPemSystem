﻿using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class D_DeviceRepository : ID_DeviceRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public D_DeviceRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public D_Device GetDevice(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            D_Device entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Device_Repository_GetDevice, parms)) {
                if(rdr.Read()) {
                    entity = new D_Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Vendor = new C_SCVendor { Id = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Version = SqlTypeConverter.DBNullStringHandler(rdr["Version"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<D_Device> GetDevicesInStation(string id) {
            SqlParameter[] parms = { new SqlParameter("@StationId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<D_Device>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Device_Repository_GetDevicesInStation, parms)) {
                while (rdr.Read()) {
                    var entity = new D_Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Vendor = new C_SCVendor { Id = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Version = SqlTypeConverter.DBNullStringHandler(rdr["Version"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Device> GetDevicesInRoom(string id) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<D_Device>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Device_Repository_GetDevicesInRoom, parms)) {
                while(rdr.Read()) {
                    var entity = new D_Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Vendor = new C_SCVendor { Id = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Version = SqlTypeConverter.DBNullStringHandler(rdr["Version"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Device> GetDevicesInFsu(string id) {
            SqlParameter[] parms = { new SqlParameter("@FsuId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<D_Device>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Device_Repository_GetDevicesInFsu, parms)) {
                while (rdr.Read()) {
                    var entity = new D_Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Vendor = new C_SCVendor { Id = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Version = SqlTypeConverter.DBNullStringHandler(rdr["Version"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Device> GetDevices() {
            var entities = new List<D_Device>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Device_Repository_GetDevices, null)) {
                while(rdr.Read()) {
                    var entity = new D_Device();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
                    entity.Vendor = new C_SCVendor { Id = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]) };
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Version = SqlTypeConverter.DBNullStringHandler(rdr["Version"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Index = SqlTypeConverter.DBNullInt32Handler(rdr["Index"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
