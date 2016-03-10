using System;
using System.Collections.Generic;
using System.Linq;
using iPem.Core;
using iPem.Core.Enum;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Core.Caching;
using iPem.Services.Common;

namespace iPem.Services.Master {
    /// <summary>
    /// User service
    /// </summary>
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

        /// <summary>
        /// Gets a user by the identifier
        /// </summary>
        /// <param name="id">user identifier</param>
        /// <returns>User</returns>
        public virtual User GetUser(Guid id) {
            return _userRepository.GetEntity(id);
        }

        /// <summary>
        /// Gets a user by the name
        /// </summary>
        /// <param name="name">user name</param>
        /// <returns>User</returns>
        public virtual User GetUser(String name) {
            return _userRepository.GetEntity(name);
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>user collection</returns>
        public virtual IPagedList<User> GetUsers(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<User> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_UsersRepository)) {
                result = _cacheManager.Get<List<User>>(GlobalCacheKeys.Cs_UsersRepository);
            } else {
                result = _userRepository.GetEntities();
                _cacheManager.Set<List<User>>(GlobalCacheKeys.Cs_UsersRepository, result);
            }

            return new PagedList<User>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <param name="role">Role id</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>user collection</returns>
        public virtual IPagedList<User> GetUsers(Guid role, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue) {
            if(deep && role.Equals(Role.SuperId))
                return this.GetUsers(pageIndex, pageSize);

            List<User> result = null;
            var key = string.Format(GlobalCacheKeys.Rl_UsersResultPattern, role);
            if(_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<User>>(key);
            } else {
                result = _userRepository.GetEntities(role);
                _cacheManager.Set<List<User>>(key, result);
            }

            return new PagedList<User>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Inserts a user
        /// </summary>
        /// <param name="user">user</param>
        public virtual void InsertUser(User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            //var key = string.Format(GlobalCacheKeys.Cs_UsersRepository, ".+"); 
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_UsersRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_UsersRepository);

            var key = string.Format(GlobalCacheKeys.Rl_UsersResultPattern, user.RoleId);
            if(_cacheManager.IsSet(key))
                _cacheManager.Remove(key);

            user.PasswordFormat = _passwordFormat;
            user.PasswordSalt = _userRepository.GenerateSalt();
            user.Password = _userRepository.EncodePassword(user.Password, user.PasswordFormat, user.PasswordSalt);
            _userRepository.Insert(user);
        }

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="user">user</param>
        public virtual void UpdateUser(User user) {
            if(user == null)
                throw new ArgumentNullException("user");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_UsersRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_UsersRepository);

            var key = string.Format(GlobalCacheKeys.Rl_UsersResultPattern, user.RoleId);
            if(_cacheManager.IsSet(key))
                _cacheManager.Remove(key);

            _userRepository.Update(user);
        }

        /// <summary>
        /// Marks user as deleted 
        /// </summary>
        /// <param name="user">user</param>
        public virtual void DeleteUser(User user) {
            if(user == null)
                throw new ArgumentNullException("user");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_UsersRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_UsersRepository);

            var key = string.Format(GlobalCacheKeys.Rl_UsersResultPattern, user.RoleId);
            if(_cacheManager.IsSet(key))
                _cacheManager.Remove(key);

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
