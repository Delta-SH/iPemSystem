using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IUserService {
        User GetUser(Guid id);

        User GetUser(string name);

        IPagedList<User> GetUsers(int pageIndex = 0, int pageSize = int.MaxValue);

        List<User> GetUsersAsList();

        IPagedList<User> GetUsers(Guid role, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue);

        List<User> GetUsersAsList(Guid role, bool deep = true);

        void Add(User user);

        void Update(User user);

        void Remove(User user);

        void SetLastLoginDate(Guid id, DateTime lastDate);

        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        EnmLoginResults Validate(String name, String password);

        bool CheckPassword(String oPwd, EnmPasswordFormat oFormat, String oSalt, String ePwd);

        EnmChangeResults ChangePassword(User user, String oPwd, String nPwd);

        void ForcePassword(User user, String nPwd);
    }
}