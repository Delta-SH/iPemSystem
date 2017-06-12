using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IUserService {
        U_User GetUser(Guid id);

        U_User GetUser(string name);

        IPagedList<U_User> GetUsers(int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_User> GetUsersAsList();

        IPagedList<U_User> GetUsers(Guid role, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_User> GetUsersAsList(Guid role, bool deep = true);

        void Add(U_User user);

        void Update(U_User user);

        void Remove(U_User user);

        void SetLastLoginDate(Guid id, DateTime lastDate);

        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        EnmLoginResults Validate(String name, String password);

        bool CheckPassword(String oPwd, EnmPasswordFormat oFormat, String oSalt, String ePwd);

        EnmChangeResults ChangePassword(U_User user, String oPwd, String nPwd);

        void ForcePassword(U_User user, String nPwd);
    }
}