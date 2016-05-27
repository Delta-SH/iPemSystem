using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class FsuRepository : IFsuRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public Fsu GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            Fsu entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Fsu_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new Fsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                    entity.SubDeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);

                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                    entity.FsuComment = SqlTypeConverter.DBNullStringHandler(rdr["FsuComment"]);
                    entity.IsOnLine = SqlTypeConverter.DBNullBooleanHandler(rdr["IsOnLine"]);
                }
            }
            return entity;
        }

        public List<Fsu> GetEntities() {
            var entities = new List<Fsu>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_Fsu_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new Fsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.DeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["DeviceTypeId"]);
                    entity.SubDeviceTypeId = SqlTypeConverter.DBNullStringHandler(rdr["SubDeviceTypeId"]);
                    entity.SysName = SqlTypeConverter.DBNullStringHandler(rdr["SysName"]);
                    entity.SysCode = SqlTypeConverter.DBNullStringHandler(rdr["SysCode"]);
                    entity.Model = SqlTypeConverter.DBNullStringHandler(rdr["Model"]);
                    entity.ProdId = SqlTypeConverter.DBNullStringHandler(rdr["ProdId"]);
                    entity.BrandId = SqlTypeConverter.DBNullStringHandler(rdr["BrandId"]);
                    entity.SuppId = SqlTypeConverter.DBNullStringHandler(rdr["SuppId"]);
                    entity.SubCompId = SqlTypeConverter.DBNullStringHandler(rdr["SubCompId"]);
                    entity.StartTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["StartTime"]);
                    entity.ScrapTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ScrapTime"]);
                    entity.StatusId = SqlTypeConverter.DBNullInt32Handler(rdr["StatusId"]);
                    entity.Contact = SqlTypeConverter.DBNullStringHandler(rdr["Contact"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);

                    entity.IP = SqlTypeConverter.DBNullStringHandler(rdr["IP"]);
                    entity.Port = SqlTypeConverter.DBNullInt32Handler(rdr["Port"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                    entity.FsuComment = SqlTypeConverter.DBNullStringHandler(rdr["FsuComment"]);
                    entity.IsOnLine = SqlTypeConverter.DBNullBooleanHandler(rdr["IsOnLine"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
