using iPem.Core.Domain.Resource;
using iPem.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iPem.Data.Repository.Resource {
    public partial class UserRepository : IUserRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UserRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public User GetEntity(string id) {
            SqlParameter[] parms = { new SqlParameter("@Id", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);

            User entity = null;
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_User_Repository_GetEntity, parms)) {
                if(rdr.Read()) {
                    entity = new User();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.PwdFormat = SqlTypeConverter.DBNullInt32Handler(rdr["PwdFormat"]);
                    entity.PwdSalt = SqlTypeConverter.DBNullStringHandler(rdr["PwdSalt"]);
                    entity.OpLevel = SqlTypeConverter.DBNullInt32Handler(rdr["OpLevel"]);
                    entity.OnlineTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["OnlineTime"]);
                    entity.LimitTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LimitTime"]);
                    entity.CreateTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreateTime"]);
                    entity.LastLoginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastLoginTime"]);
                    entity.LastPwdChangedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastPwdChangedTime"]);
                    entity.FailedPwdAttemptCount = SqlTypeConverter.DBNullInt32Handler(rdr["FailedPwdAttemptCount"]);
                    entity.FailedPwdTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["FailedPwdTime"]);
                    entity.IsLockedOut = SqlTypeConverter.DBNullBooleanHandler(rdr["IsLockedOut"]);
                    entity.LastLockoutTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastLockoutTime"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entity.EmpId = SqlTypeConverter.DBNullStringHandler(rdr["EmpId"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                }
            }
            return entity;
        }

        public List<User> GetEntities() {
            var entities = new List<User>();
            using(var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_User_Repository_GetEntities, null)) {
                while(rdr.Read()) {
                    var entity = new User();
                    entity.Id = SqlTypeConverter.DBNullStringHandler(rdr["Id"]);
                    entity.Uid = SqlTypeConverter.DBNullStringHandler(rdr["Uid"]);
                    entity.Pwd = SqlTypeConverter.DBNullStringHandler(rdr["Pwd"]);
                    entity.PwdFormat = SqlTypeConverter.DBNullInt32Handler(rdr["PwdFormat"]);
                    entity.PwdSalt = SqlTypeConverter.DBNullStringHandler(rdr["PwdSalt"]);
                    entity.OpLevel = SqlTypeConverter.DBNullInt32Handler(rdr["OpLevel"]);
                    entity.OnlineTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["OnlineTime"]);
                    entity.LimitTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LimitTime"]);
                    entity.CreateTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["CreateTime"]);
                    entity.LastLoginTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastLoginTime"]);
                    entity.LastPwdChangedTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastPwdChangedTime"]);
                    entity.FailedPwdAttemptCount = SqlTypeConverter.DBNullInt32Handler(rdr["FailedPwdAttemptCount"]);
                    entity.FailedPwdTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["FailedPwdTime"]);
                    entity.IsLockedOut = SqlTypeConverter.DBNullBooleanHandler(rdr["IsLockedOut"]);
                    entity.LastLockoutTime = SqlTypeConverter.DBNullDateTimeHandler(rdr["LastLockoutTime"]);
                    entity.Remark = SqlTypeConverter.DBNullStringHandler(rdr["Remark"]);
                    entity.EmpId = SqlTypeConverter.DBNullStringHandler(rdr["EmpId"]);
                    entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(rdr["Enabled"]);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        #endregion

    }
}
