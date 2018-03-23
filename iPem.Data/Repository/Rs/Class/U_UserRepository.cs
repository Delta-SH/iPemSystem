using iPem.Core.Domain.Common;
using iPem.Core.Enum;
using iPem.Data.Common;
using iPem.Data.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace iPem.Data.Repository.Rs {
    public partial class U_UserRepository : IU_UserRepository {

        #region Fields

        private readonly string _databaseConnectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public U_UserRepository(string databaseConnectionString) {
            this._databaseConnectionString = databaseConnectionString;
        }

        #endregion

        #region Methods

        public U_User GetUserById(string id) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringHandler(id);

            U_User entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_GetUserById, parms)) {
                if (rdr.Read()) {
                    entity = getEntity(rdr);
                }
            }
            return entity;
        }

        public U_User GetUserByName(string name) {
            SqlParameter[] parms = { new SqlParameter("@UID", SqlDbType.VarChar, 100) };
            parms[0].Value = SqlTypeConverter.DBNullStringHandler(name);

            U_User entity = null;
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_GetUserByName, parms)) {
                if (rdr.Read()) {
                    entity = getEntity(rdr);
                }
            }
            return entity;
        }

        public List<U_User> GetUsers() {
            var entities = new List<U_User>();
            using (var rdr = SqlHelper.ExecuteReader(this._databaseConnectionString, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_GetUsers, null)) {
                while (rdr.Read()) {
                    var entity = getEntity(rdr);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        public List<U_User> GetUsersInRole(string id) {
            return null;
        }

        public void Insert(IList<U_User> entities) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar,100),
                                     new SqlParameter("@EmpID", SqlDbType.VarChar,100),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) ,
                                     new SqlParameter("@UID", SqlDbType.VarChar,100),
                                     new SqlParameter("@PWD", SqlDbType.VarChar,128),
                                     new SqlParameter("@PwdFormat", SqlDbType.Int),
                                     new SqlParameter("@PwdSalt", SqlDbType.VarChar,128),
                                     new SqlParameter("@RoleID", SqlDbType.VarChar,100),
                                     new SqlParameter("@LimitTime", SqlDbType.DateTime),
                                     new SqlParameter("@CreateTime", SqlDbType.DateTime),
                                     new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                                     new SqlParameter("@LastPwdChangedTime", SqlDbType.DateTime),
                                     new SqlParameter("@FailedPwdAttemptCount", SqlDbType.Int),
                                     new SqlParameter("@FailedPwdTime", SqlDbType.DateTime),
                                     new SqlParameter("@IsLockedOut", SqlDbType.Bit),
                                     new SqlParameter("@LastLockoutTime", SqlDbType.DateTime),
                                     new SqlParameter("@Remark", SqlDbType.VarChar,512)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.EmployeeId);
                        parms[2].Value = entity.Enabled;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.Uid);
                        parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.Password);
                        parms[5].Value = entity.PasswordFormat;
                        parms[6].Value = SqlTypeConverter.DBNullStringChecker(entity.PasswordSalt);
                        parms[7].Value = SqlTypeConverter.DBNullStringChecker(entity.RoleId);
                        parms[8].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LimitedDate);
                        parms[9].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedDate);
                        parms[10].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastLoginDate);
                        parms[11].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastPasswordChangedDate);
                        parms[12].Value = SqlTypeConverter.DBNullInt32Checker(entity.FailedPasswordAttemptCount);
                        parms[13].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.FailedPasswordDate);
                        parms[14].Value = entity.IsLockedOut;
                        parms[15].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastLockoutDate);
                        parms[16].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_Insert, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(IList<U_User> entities) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar,100),
                                     new SqlParameter("@EmpID", SqlDbType.VarChar,100),
                                     new SqlParameter("@Enabled", SqlDbType.Bit) ,
                                     new SqlParameter("@UID", SqlDbType.VarChar,100),
                                     //new SqlParameter("@RoleID", SqlDbType.VarChar,100),
                                     new SqlParameter("@LimitTime", SqlDbType.DateTime),
                                     new SqlParameter("@CreateTime", SqlDbType.DateTime),
                                     new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                                     new SqlParameter("@FailedPwdAttemptCount", SqlDbType.Int),
                                     new SqlParameter("@FailedPwdTime", SqlDbType.DateTime),
                                     new SqlParameter("@IsLockedOut", SqlDbType.Bit),
                                     new SqlParameter("@LastLockoutTime", SqlDbType.DateTime),
                                     new SqlParameter("@Remark", SqlDbType.VarChar,512)};

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        parms[1].Value = SqlTypeConverter.DBNullStringChecker(entity.EmployeeId);
                        parms[2].Value = entity.Enabled;
                        parms[3].Value = SqlTypeConverter.DBNullStringChecker(entity.Uid);
                        //parms[4].Value = SqlTypeConverter.DBNullStringChecker(entity.RoleId);
                        parms[4].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LimitedDate);
                        parms[5].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.CreatedDate);
                        parms[6].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastLoginDate);
                        parms[7].Value = SqlTypeConverter.DBNullInt32Checker(entity.FailedPasswordAttemptCount);
                        parms[8].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.FailedPasswordDate);
                        parms[9].Value = entity.IsLockedOut;
                        parms[10].Value = SqlTypeConverter.DBNullDateTimeChecker(entity.LastLockoutDate);
                        parms[11].Value = SqlTypeConverter.DBNullStringChecker(entity.Comment);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_Update, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(IList<U_User> entities) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar, 100) };
            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    foreach (var entity in entities) {
                        parms[0].Value = SqlTypeConverter.DBNullStringChecker(entity.Id);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_Delete, parms);
                    }
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void SetLastLoginDate(string id, DateTime lastDate) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar,100),
                                     new SqlParameter("@LastLoginTime", SqlDbType.DateTime) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
                    parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(lastDate);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_SetLastLoginDate, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void SetFailedPasswordDate(string id, DateTime failedDate) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar,100),
                                     new SqlParameter("@FailedPwdTime", SqlDbType.DateTime) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
                    parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(failedDate);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_SetFailedPasswordDate, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public void SetLockedOut(string id, Boolean isLockedOut, DateTime lastLockoutDate) {
            SqlParameter[] parms = { new SqlParameter("@ID", SqlDbType.VarChar,100),
                                     new SqlParameter("@IsLockedOut", SqlDbType.Bit),
                                     new SqlParameter("@LastLockoutTime", SqlDbType.DateTime) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(id);
                    parms[1].Value = isLockedOut;
                    parms[1].Value = SqlTypeConverter.DBNullDateTimeChecker(lastLockoutDate);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_SetLockedOut, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        private U_User getEntity(SqlDataReader sdr) {
            var entity = new U_User();
            entity.Id = SqlTypeConverter.DBNullStringHandler(sdr["ID"]);
            entity.EmployeeId = SqlTypeConverter.DBNullStringHandler(sdr["EmpID"]);
            entity.Enabled = SqlTypeConverter.DBNullBooleanHandler(sdr["Enabled"]);
            entity.Uid = SqlTypeConverter.DBNullStringHandler(sdr["UID"]);
            entity.Password = SqlTypeConverter.DBNullStringHandler(sdr["PWD"]);
            entity.PasswordFormat = SqlTypeConverter.DBNullEnmPasswordFormatHandler(sdr["PwdFormat"]);
            entity.PasswordSalt = SqlTypeConverter.DBNullStringHandler(sdr["PwdSalt"]);
            entity.RoleId = SqlTypeConverter.DBNullStringHandler(sdr["RoleID"]);
            entity.LimitedDate = SqlTypeConverter.DBNullDateTimeHandler(sdr["LimitTime"]);
            entity.CreatedDate = SqlTypeConverter.DBNullDateTimeHandler(sdr["CreateTime"]);
            entity.LastLoginDate = SqlTypeConverter.DBNullDateTimeHandler(sdr["LastLoginTime"]);
            entity.LastPasswordChangedDate = SqlTypeConverter.DBNullDateTimeHandler(sdr["LastPwdChangedTime"]);
            entity.FailedPasswordAttemptCount = SqlTypeConverter.DBNullInt32Handler(sdr["FailedPwdAttemptCount"]);
            entity.FailedPasswordDate = SqlTypeConverter.DBNullDateTimeHandler(sdr["FailedPwdTime"]);
            entity.IsLockedOut = SqlTypeConverter.DBNullBooleanHandler(sdr["IsLockedOut"]);
            entity.LastLockoutDate = SqlTypeConverter.DBNullDateTimeHandler(sdr["LastLockoutTime"]);
            entity.Comment = SqlTypeConverter.DBNullStringHandler(sdr["Remark"]);
            return entity;
        }

        #endregion

        #region Password

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="uId">用户标识</param>
        /// <param name="nPwd">加密的新密码</param>
        /// <param name="nFormat">新密码加密方式</param>
        /// <param name="nSalt">新密码盐值</param>
        public void ChangePassword(string uId, String nPwd, EnmPasswordFormat nFormat, String nSalt) {
            SqlParameter[] parms = {  new SqlParameter("@ID", SqlDbType.VarChar,100),
                                      new SqlParameter("@PWD", SqlDbType.VarChar,128),
                                      new SqlParameter("@PwdFormat", SqlDbType.Int),
                                      new SqlParameter("@PwdSalt", SqlDbType.VarChar,128) };

            using (var conn = new SqlConnection(this._databaseConnectionString)) {
                if (conn.State != ConnectionState.Open) conn.Open();
                var trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try {
                    parms[0].Value = SqlTypeConverter.DBNullStringChecker(uId);
                    parms[1].Value = nPwd;
                    parms[2].Value = nFormat;
                    parms[3].Value = nSalt;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SqlCommands_Rs.Sql_U_User_Repository_ChangePassword, parms);
                    trans.Commit();
                } catch {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 生成强随机盐值
        /// </summary>
        /// <returns>返回强随机盐值</returns>
        public String GenerateSalt() {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// 密码加密（采用SHA1加密方式或不加密）
        /// </summary>
        /// <param name="pwd">待加密的密码</param>
        /// <param name="format">加密方式</param>
        /// <param name="salt">加密盐值</param>
        /// <returns>返回已经加密的密码</returns>
        public String EncodePassword(String pwd, EnmPasswordFormat format, String salt) {
            if (format == EnmPasswordFormat.Clear) { return pwd; }
            var bytes = Encoding.Unicode.GetBytes(pwd);
            var src = Convert.FromBase64String(salt);
            var dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            var algorithm = HashAlgorithm.Create("SHA1");
            var inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        #endregion

    }
}
