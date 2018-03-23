using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Common;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Common;
using iPem.Services.Common;
using iPem.Services.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class UserService : IUserService {

        #region Fields

        private readonly IU_UserRepository _repository;
        private readonly IU_UserRepository _userRepository;
        private readonly ICacheManager _cacheManager;
        private readonly EnmPasswordFormat _passwordFormat;
        private readonly IRoleService _roleService;
        private readonly IServiceGetter _svcGetter;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UserService(
            IU_UserRepository repository,
            ICacheManager cacheManager,
            IRoleService roleService,
            IServiceGetter svcGetter) {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._passwordFormat = EnmPasswordFormat.Clear;
            this._roleService = roleService;
            this._svcGetter = svcGetter;
            this._userRepository = svcGetter.GetByName<IU_UserRepository>("sc_user_repository");
        }

        #endregion

        #region Methods

        public U_User GetUserById(string id) {
            var user = _repository.GetUserById(id);
            if (user != null) {
                if (user.RoleId.Equals(U_Role.RsSuperId)) {
                    user.RoleId = U_Role.SuperId;
                } else {
                    var role = _roleService.GetRoleByUid(user.Id);
                    if (role != null) {
                        user.RoleId = role.Id;
                    }
                }
            }
            return user;
        }

        public U_User GetUserByName(string name) {
            var user = _repository.GetUserByName(name);
            if (user != null) {
                if (user.RoleId.Equals(U_Role.RsSuperId)) {
                    user.RoleId = U_Role.SuperId;
                } else {
                    var role = _roleService.GetRoleByUid(user.Id);
                    if (role != null) {
                        user.RoleId = role.Id;
                    }
                }
            }

            return user;
        }

        public List<U_User> GetUsers() {
            var users = _repository.GetUsers();
            var csUsers = _userRepository.GetUsers();

            if (users != null && users.Count > 0) {
                foreach (var user in users) {
                    if (user.RoleId.Equals(U_Role.RsSuperId)) {
                        user.RoleId = U_Role.SuperId;
                        continue;
                    }

                    var current = csUsers.Find(u => u.Id.Equals(user.Id));
                    if (current != null) {
                        user.RoleId = current.RoleId;
                    }
                }
            }

            return users;
        }

        public IPagedList<U_User> GetPagedUsers(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_User>(this.GetUsers(), pageIndex, pageSize);
        }

        public List<U_User> GetUsersInRole(string role, bool deep = true) {
            if (deep && role.Equals(U_Role.SuperId))
                return this.GetUsers();

            var users = this.GetUsers().FindAll(u => u.RoleId.Equals(role.ToString()));

            return users;
        }

        public IPagedList<U_User> GetUsers(string role, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_User>(this.GetUsersInRole(role, deep), pageIndex, pageSize);
        }

        public void Add(U_User user) {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordFormat = _passwordFormat;
            user.PasswordSalt = _repository.GenerateSalt();
            user.Password = _repository.EncodePassword(user.Password, user.PasswordFormat, user.PasswordSalt);
            _repository.Insert(new List<U_User> { user });
            _userRepository.Insert(new List<U_User> { user });
        }

        public void Update(params U_User[] users) {
            if (users == null || users.Length == 0)
                throw new ArgumentNullException("users");

            _repository.Update(users);
            foreach (var user in users) {
                var scUser = _userRepository.GetUserById(user.Id);
                if (scUser == null) {
                    _userRepository.Insert(new List<U_User> { user });
                } else {
                    _userRepository.Update(new List<U_User> { user });
                }
            }
        }

        public void Remove(params U_User[] users) {
            if (users == null || users.Length == 0)
                throw new ArgumentNullException("users");

            _repository.Delete(users);
            _userRepository.Delete(users);
        }

        public void SetLastLoginDate(String id, DateTime lastDate) {
            //if (id == default(Guid))
            //    throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("user");

            _repository.SetLastLoginDate(id, lastDate);
        }

        public void SetFailedPasswordDate(String id, DateTime failedDate) {
            //if (id == default(Guid))
            //    throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("user");

            _repository.SetFailedPasswordDate(id, failedDate);
        }

        public void SetLockedOut(String id, Boolean isLockedOut, DateTime lastLockoutDate) {
            //if (id == default(Guid))
            //    throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("user");

            _repository.SetLockedOut(id, isLockedOut, lastLockoutDate);
        }

        #endregion

        #region Validate & Password

        public EnmLoginResults Validate(String name, String password) {
            var user = this.GetUserByName(name);

            if (user == null)
                return EnmLoginResults.NotExist;
            if (!user.Enabled)
                return EnmLoginResults.NotEnabled;
            if (user.LimitedDate < DateTime.Today)
                return EnmLoginResults.Expired;
            if (user.IsLockedOut)
                return EnmLoginResults.Locked;

            if (!this.CheckPassword(password, user.PasswordFormat, user.PasswordSalt, user.Password)) {
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
            if (!CheckPassword(oPwd, user.PasswordFormat, user.PasswordSalt, user.Password))
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
