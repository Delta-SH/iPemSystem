using iPem.Core.Domain.Common;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Common {
    /// <summary>
    /// 用户信息表
    /// </summary>
    public partial interface IU_UserRepository {
        /// <summary>
        /// 获得指定用户编号的用户
        /// </summary>
        U_User GetUserById(string id);

        /// <summary>
        /// 获得指定用户名称的用户
        /// </summary>
        U_User GetUserByName(string name);

        /// <summary>
        /// 获得所有的用户信息
        /// </summary>
        List<U_User> GetUsers();

        /// <summary>
        /// 获得指定角色的用户信息
        /// </summary>
        List<U_User> GetUsersInRole(string roleId);

        /// <summary>
        /// 新增用户
        /// </summary>
        void Insert(IList<U_User> entities);

        /// <summary>
        /// 更新用户
        /// </summary>
        void Update(IList<U_User> entities);

        /// <summary>
        /// 删除用户
        /// </summary>
        void Delete(IList<U_User> entities);

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        void SetLastLoginDate(string id, DateTime lastDate);

        /// <summary>
        /// 更新用户登录失败时间
        /// </summary>
        void SetFailedPasswordDate(string id, DateTime failedDate);

        /// <summary>
        /// 更新用户锁定信息
        /// </summary>
        void SetLockedOut(string id, Boolean isLockedOut, DateTime lastLockoutDate);

        /// <summary>
        /// 更新密码
        /// </summary>
        void ChangePassword(string uId, String nPwd, EnmPasswordFormat nFormat, String nSalt);

        /// <summary>
        /// 散列盐值
        /// </summary>
        String GenerateSalt();

        /// <summary>
        /// 密码加密
        /// </summary>
        String EncodePassword(String pwd, EnmPasswordFormat format, String salt);
    }
}
