using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IUserRepository {

        U_User GetEntity(Guid id);

        U_User GetEntity(string name);

        List<U_User> GetEntities();

        List<U_User> GetEntities(Guid id);

        void Insert(U_User entity);

        void Insert(List<U_User> entities);

        void Update(U_User entity);

        void Update(List<U_User> entities);

        void Delete(U_User entity);

        void Delete(List<U_User> entities);

        void SetLastLoginDate(Guid id, DateTime lastDate);

        void SetFailedPasswordDate(Guid id, DateTime failedDate);

        void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate);

        void ChangePassword(Guid uId, String nPwd, EnmPasswordFormat nFormat, String nSalt);

        String GenerateSalt();

        String EncodePassword(String pwd, EnmPasswordFormat format, String salt);
    }
}
