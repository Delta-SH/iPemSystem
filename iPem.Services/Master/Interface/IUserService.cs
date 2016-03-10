using System;
using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;

namespace iPem.Services.Master {
    /// <summary>
    /// UserService interface
    /// </summary>
    public partial interface IUserService {
        User GetUser(Guid id);

        User GetUser(String name);

        IPagedList<User> GetUsers(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<User> GetUsers(Guid roleId, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue);

        void InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        void SetLastLoginDate(Guid id, DateTime lastDate);

        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        EnmLoginResults Validate(String name, String password);

        bool CheckPassword(String oPwd, EnmPasswordFormat oFormat, String oSalt, String ePwd);

        EnmChangeResults ChangePassword(User user, String oPwd, String nPwd);

        void ForcePassword(User user, String nPwd);
    }
}