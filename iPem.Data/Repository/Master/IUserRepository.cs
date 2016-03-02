using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// User repository interface
    /// </summary>
    public partial interface IUserRepository {

        User GetEntity(Guid id);

        User GetEntity(string name);

        IList<User> GetEntities();

        IList<User> GetEntities(Guid id);

        void Insert(User entity);

        void Insert(IList<User> entities);

        void Update(User entity);

        void Update(IList<User> entities);

        void Delete(User entity);

        void Delete(IList<User> entities);

        void SetLastLoginDate(Guid id, DateTime lastDate);

        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        void ChangePassword(Guid uId, String nPwd, EnmPasswordFormat nFormat, String nSalt);

        String GenerateSalt();

        String EncodePassword(String pwd, EnmPasswordFormat format, String salt);
    }
}
