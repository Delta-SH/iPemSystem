using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Sc {
    public partial class UserService : IUserService {

        #region Fields

        private readonly IU_UserRepository _repository;
        private readonly ICacheManager _cacheManager;
        private readonly EnmPasswordFormat _passwordFormat;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UserService(
            IU_UserRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._passwordFormat = EnmPasswordFormat.Hashed;
        }

        #endregion

        #region Methods

        public U_User GetUserById(Guid id) {
            return _repository.GetUserById(id);
        }

        public U_User GetUserByName(string name) {
            return _repository.GetUserByName(name);
        }

        public List<U_User> GetUsers() {
            return _repository.GetUsers();
        }

        public List<U_User> GetUsersInRole(Guid role, bool deep = true) {
            if (deep && role.Equals(U_Role.SuperId))
                return this.GetUsers();

            return _repository.GetUsersInRole(role);
        }

        public IPagedList<U_User> GetUsers(Guid role, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_User>(this.GetUsersInRole(role, deep), pageIndex, pageSize);
        }

        public IPagedList<U_User> GetPagedUsers(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_User>(this.GetUsers(), pageIndex, pageSize);
        }

        public void Add(U_User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordFormat = _passwordFormat;
            user.PasswordSalt = _repository.GenerateSalt();
            user.Password = _repository.EncodePassword(user.Password, user.PasswordFormat, user.PasswordSalt);
            _repository.Insert(new List<U_User> { user });
        }

        public void Update(params U_User[] users) {
            if (users == null || users.Length == 0)
                throw new ArgumentNullException("users");

            _repository.Update(users);
        }

        public void Remove(params U_User[] users) {
            if (users == null || users.Length == 0)
                throw new ArgumentNullException("users");

            _repository.Delete(users);
        }

        public void SetLastLoginDate(Guid id, DateTime lastDate) {
            if(id == default(Guid))
                throw new ArgumentNullException("user");

            _repository.SetLastLoginDate(id, lastDate);
        }

        public void SetFailedPasswordDate(Guid id, DateTime failedDate) {
            if(id == default(Guid))
                throw new ArgumentNullException("user");

            _repository.SetFailedPasswordDate(id, failedDate);
        }

        public void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate) {
            if(id == default(Guid))
                throw new ArgumentNullException("user");

            _repository.SetLockedOut(id, isLockedOut, lastLockoutDate);
        }

        #endregion

        #region Validate & Password

        public EnmLoginResults Validate(String name, String password) {
            var user = this.GetUserByName(name);

            if(user == null)
                return EnmLoginResults.NotExist;
            if(!user.Enabled)
                return EnmLoginResults.NotEnabled;
            if(user.LimitedDate < DateTime.Today)
                return EnmLoginResults.Expired;
            if(user.IsLockedOut)
                return EnmLoginResults.Locked;

            if(!this.CheckPassword(password, user.PasswordFormat, user.PasswordSalt, user.Password)) {
                this.SetFailedPasswordDate(user.Id, DateTime.Now);
                return EnmLoginResults.WrongPassword;
            }

            this.SetLastLoginDate(user.Id, DateTime.Now);
            return EnmLoginResults.Successful;
        }

        public bool CheckPassword(String oPwd, EnmPasswordFormat oFormat, String oSalt, String ePwd) {
            return _repository.EncodePassword(oPwd, oFormat, oSalt).Equals(ePwd);
        }

        public EnmChangeResults ChangePassword(U_User user, String oPwd, String nPwd) {
            if(!CheckPassword(oPwd, user.PasswordFormat, user.PasswordSalt, user.Password))
                return EnmChangeResults.WrongPassword;

            user.PasswordSalt = _repository.GenerateSalt();
            user.Password = _repository.EncodePassword(nPwd, user.PasswordFormat, user.PasswordSalt);
            _repository.ChangePassword(user.Id, user.Password, user.PasswordFormat, user.PasswordSalt);
            return EnmChangeResults.Successful;
        }

        public void ForcePassword(U_User user, String nPwd) {
            user.PasswordSalt = _repository.GenerateSalt();
            user.Password = _repository.EncodePassword(nPwd, user.PasswordFormat, user.PasswordSalt);
            _repository.ChangePassword(user.Id, user.Password, user.PasswordFormat, user.PasswordSalt);
        }

        #endregion

    }
}
