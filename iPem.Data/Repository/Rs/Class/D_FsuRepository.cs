using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Rs {
    public partial class D_FsuRepository : ID_FsuRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public D_FsuRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public D_Fsu GetFsu(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            D_Fsu entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_GetFsu, parms)) {
                if(rdr.Read()) {
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
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
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.FsuCode = SqlTypeConverter.DBNullStringHandler(rdr["FsuCode"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);

                    entity.VendorId = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]);
                    entity.VendorName = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                }
            }
            return entity;
        }

        public List<D_Fsu> GetFsuInRoom(string parent) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(parent);

            var entities = new List<D_Fsu>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_GetFsuInRoom, parms)) {
                while(rdr.Read()) {
                    var entity = new D_Fsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
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
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.FsuCode = SqlTypeConverter.DBNullStringHandler(rdr["FsuCode"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);

                    entity.VendorId = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]);
                    entity.VendorName = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<D_Fsu> GetFsus() {
            var entities = new List<D_Fsu>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_GetFsus, null)) {
                while(rdr.Read()) {
                    var entity = new D_Fsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Type = new C_DeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeName"]) };
                    entity.SubType = new C_SubDeviceType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeName"]) };
                    entity.SubLogicType = new C_SubLogicType { Id = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeId"]), Name = SqlTypeConverter.DBNullStringHandler(rdr["SubLogicTypeName"]) };
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
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.FsuId = SqlTypeConverter.DBNullStringHandler(rdr["FsuId"]);
                    entity.FsuCode = SqlTypeConverter.DBNullStringHandler(rdr["FsuCode"]);
                    entity.ProtocolId = SqlTypeConverter.DBNullStringHandler(rdr["ProtocolId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);

                    entity.VendorId = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]);
                    entity.VendorName = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public D_ExtFsu GetExtFsu(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            D_ExtFsu entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_GetExtFsu, parms)) {
                if(rdr.Read()) {
                    entity = new D_ExtFsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.GroupId = SqlTypeConverter.DBNullStringHandler(rdr["GroupId"]);
                }
            }
            return entity;
        }

        public List<D_ExtFsu> GetExtFsus() {
            var entities = new List<D_ExtFsu>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_GetExtFsus, null)) {
                while(rdr.Read()) {
                    var entity = new D_ExtFsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.GroupId = SqlTypeConverter.DBNullStringHandler(rdr["GroupId"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
