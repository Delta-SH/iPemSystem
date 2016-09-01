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

        private readonly IUserRepository _userRepository;
        private readonly ICacheManager _cacheManager;
        private readonly EnmPasswordFormat _passwordFormat;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UserService(
            IUserRepository userRepository,
            ICacheManager cacheManager) {
            this._userRepository = userRepository;
            this._cacheManager = cacheManager;
            this._passwordFormat = EnmPasswordFormat.Hashed;
        }

        #endregion

        #region Methods

        public virtual User GetUser(Guid id) {
            return _userRepository.GetEntity(id);
        }

        public virtual User GetUser(string name) {
            return _userRepository.GetEntity(name);
        }

        public virtual IPagedList<User> GetUsers(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<User>(this.GetUsersAsList(), pageIndex, pageSize);
        }

        public virtual List<User> GetUsersAsList() {
            return _userRepository.GetEntities();
        }

        public virtual IPagedList<User> GetUsers(Guid role, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<User>(this.GetUsersAsList(role, deep), pageIndex, pageSize);
        }

        public virtual List<User> GetUsersAsList(Guid role, bool deep = true) {
            if(deep && role.Equals(Role.SuperId))
                return this.GetUsersAsList();

            return _userRepository.GetEntities(role);
        }

        public virtual void Add(User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordFormat = _passwordFormat;
            user.PasswordSalt = _userRepository.GenerateSalt();
            user.Password = _userRepository.EncodePassword(user.Password, user.PasswordFormat, user.PasswordSalt);
            _userRepository.Insert(user);
        }

        public virtual void Update(User user) {
            if(user == null)
                throw new ArgumentNullException("user");

            _userRepository.Update(user);
        }

        public virtual void Remove(User user) {
            if(user == null)
                throw new ArgumentNullException("user");

            _userRepository.Delete(user);
        }

        public virtual void SetLastLoginDate(Guid id, DateTime lastDate) {
            if(id == default(Guid))
                throw new ArgumentNullException("user");

            _userRepository.SetLastLoginDate(id, lastDate);
        }

        public virtual void SetFailedPasswordDate(Guid id, DateTime failedDate) {
            if(id == default(Guid))
                throw new ArgumentNullException("user");

            _userRepository.SetFailedPasswordDate(id, failedDate);
        }

        public virtual void SetLockedOut(Guid id, Boolean isLockedOut, DateTime lastLockoutDate) {
            if(id == default(Guid))
                throw new ArgumentNullException("user");

            _userRepository.SetLockedOut(id, isLockedOut, lastLockoutDate);
        }

        #endregion

        #region Validate & Password

        public virtual EnmLoginResults Validate(String name, String password) {
            var user = this.GetUser(name);

            if(user == null)
                return EnmLoginResults.NotExist;
            if(!user.Enabled)
                return EnmLoginResults.NotEnabled;
            if(user.LimitDate < DateTime.Today)
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

        public virtual bool CheckPassword(String oPwd, EnmPasswordFormat oFormat, String oSalt, String ePwd) {
            return _userRepository.EncodePassword(oPwd, oFormat, oSalt).Equals(ePwd);
        }

        public virtual EnmChangeResults ChangePassword(User user, String oPwd, String nPwd) {
            if(!CheckPassword(oPwd, user.PasswordFormat, user.PasswordSalt, user.Password))
                return EnmChangeResults.WrongPassword;

            user.PasswordSalt = _userRepository.GenerateSalt();
            user.Password = _userRepository.EncodePassword(nPwd, user.PasswordFormat, user.PasswordSalt);
            _userRepository.ChangePassword(user.Id, user.Password, user.PasswordFormat, user.PasswordSalt);
            return EnmChangeResults.Successful;
        }

        public virtual void ForcePassword(User user, String nPwd) {
            user.PasswordSalt = _userRepository.GenerateSalt();
            user.Password = _userRepository.EncodePassword(nPwd, user.PasswordFormat, user.PasswordSalt);
            _userRepository.ChangePassword(user.Id, user.Password, user.PasswordFormat, user.PasswordSalt);
        }

        #endregion

    }
}
