using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IUserRepository {

        User GetEntity(Guid id);

        User GetEntity(string name);

        List<User> GetEntities();

        List<User> GetEntities(Guid id);

        void Insert(User entity);

        void Insert(List<User> entities);

        void Update(User entity);

        void Update(List<User> entities);

        void Delete(User entity);

        void Delete(List<User> entities);

        void SetLastLoginDate(Guid id, DateTime lastDate);

        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        void ChangePassword(Guid uId, String nPwd, EnmPasswordFormat nFormat, String nSalt);

        String GenerateSalt();

        String EncodePassword(String pwd, EnmPasswordFormat format, String salt);
    }
}
