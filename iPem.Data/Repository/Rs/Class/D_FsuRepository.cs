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
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.RoomTypeId = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.VendorId = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]);
                    entity.VendorName = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]);
                }
            }
            return entity;
        }

        public List<D_Fsu> GetFsusInRoom(string id) {
            SqlParameter[] parms = { new SqlParameter("@RoomId", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            var entities = new List<D_Fsu>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_GetFsusInRoom, parms)) {
                while (rdr.Read()) {
                    var entity = new D_Fsu();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Code = SqlTypeConverter.DBNullStringHandler(rdr["Code"]);
                    entity.Name = SqlTypeConverter.DBNullStringHandler(rdr["Name"]);
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.RoomTypeId = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.VendorId = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]);
                    entity.VendorName = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]);
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
                    entity.AreaId = SqlTypeConverter.DBNullStringHandler(rdr["AreaId"]);
                    entity.AreaName = SqlTypeConverter.DBNullStringHandler(rdr["AreaName"]);
                    entity.StationId = SqlTypeConverter.DBNullStringHandler(rdr["StationId"]);
                    entity.StationName = SqlTypeConverter.DBNullStringHandler(rdr["StationName"]);
                    entity.StaTypeId = SqlTypeConverter.DBNullStringHandler(rdr["StaTypeId"]);
                    entity.RoomId = SqlTypeConverter.DBNullStringHandler(rdr["RoomId"]);
                    entity.RoomName = SqlTypeConverter.DBNullStringHandler(rdr["RoomName"]);
                    entity.RoomTypeId = SqlTypeConverter.DBNullStringHandler(rdr["RoomTypeId"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.VendorId = SqlTypeConverter.DBNullStringHandler(rdr["VendorId"]);
                    entity.VendorName = SqlTypeConverter.DBNullStringHandler(rdr["VendorName"]);
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
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.GroupId = SqlTypeConverter.DBNullStringHandler(rdr["GroupId"]);
                    entity.UpgradeStatus = SqlTypeConverter.DBNullUpgradeStatusHandler(rdr["UpgradeStatus"]);
                    entity.UpgradeResult = SqlTypeConverter.DBNullStringHandler(rdr["UpgradeResult"]);
                    entity.UpgradeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["UpgradeTime"]);
                    entity.Upgrader = SqlTypeConverter.DBNullStringHandler(rdr["Upgrader"]);

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
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.FtpUid = SqlTypeConverter.DBNullStringHandler(rdr["FtpUid"]);
                    entity.FtpPwd = SqlTypeConverter.DBNullStringHandler(rdr["FtpPwd"]);
                    entity.FtpFilePath = SqlTypeConverter.DBNullStringHandler(rdr["FtpFilePath"]);
                    entity.FtpAuthority = SqlTypeConverter.DBNullInt32Handler(rdr["FtpAuthority"]);
                    entity.ChangeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["ChangeTime"]);
                    entity.LastTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastTime"]);
                    entity.Status = SqlTypeConverter.DBNullBooleanHandler(rdr["Status"]);
                    entity.Comment = SqlTypeConverter.DBNullStringHandler(rdr["Comment"]);
                    entity.GroupId = SqlTypeConverter.DBNullStringHandler(rdr["GroupId"]);
                    entity.UpgradeStatus = SqlTypeConverter.DBNullUpgradeStatusHandler(rdr["UpgradeStatus"]);
                    entity.UpgradeResult = SqlTypeConverter.DBNullStringHandler(rdr["UpgradeResult"]);
                    entity.UpgradeTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["UpgradeTime"]);
                    entity.Upgrader = SqlTypeConverter.DBNullStringHandler(rdr["Upgrader"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public void UpdateFsus() {
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_UpdateFsus, null);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void UpdateExes(IEnumerable<D_ExtFsu> entities) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100),
                                     new SqlParameter("@UpgradeStatus", SqlDbType.Int),
                                     new SqlParameter("@UpgradeResult", SqlDbType.VarChar, 512),
                                     new SqlParameter("@UpgradeTime", SqlDbType.DateTime),
                                     new SqlParameter("@Upgrader", SqlDbType.VarChar, 200) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = (int)entity.UpgradeStatus;
                        parms[2].Value = SqlTypeConverter.DBNullStringChecker(entity.UpgradeResult);
                        parms[3].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.UpgradeTime);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Upgrader);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_D_Fsu_Repository_UpdateExes, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion

    }
}
