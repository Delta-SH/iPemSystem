using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 用户信息API
    /// </summary>
    public partial interface IUserService {
        /// <summary>
        /// 获得指定用户编号的用户
        /// </summary>
        U_User GetUserById(Guid id);

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
        List<U_User> GetUsersInRole(Guid id, bool deep = true);

        /// <summary>
        /// 获得指定角色的用户信息（分页）
        /// </summary>
        IPagedList<U_User> GetUsers(Guid id, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得所有的用户信息(分页)
        /// </summary>
        IPagedList<U_User> GetPagedUsers(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增用户
        /// </summary>
        void Add(U_User user);

        /// <summary>
        /// 更新用户
        /// </summary>
        void Update(params U_User[] users);

        /// <summary>
        /// 删除用户
        /// </summary>
        void Remove(params U_User[] users);

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        void SetLastLoginDate(Guid id, DateTime lastDate);

        /// <summary>
        /// 更新用户登录失败时间
        /// </summary>
        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        /// <summary>
        /// 更新用户锁定信息
        /// </summary>
        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        /// <summary>
        /// 用户登录
        /// </summary>
        EnmLoginResults Validate(String name, String password);

        /// <summary>
        /// 校验密码
        /// </summary>
        bool CheckPassword(String oPwd, EnmPasswordFormat oFormat, String oSalt, String ePwd);

        /// <summary>
        /// 更新密码
        /// </summary>
        EnmChangeResults ChangePassword(U_User user, String oPwd, String nPwd);

        /// <summary>
        /// 重置密码
        /// </summary>
        void ForcePassword(U_User user, String nPwd);
    }
}