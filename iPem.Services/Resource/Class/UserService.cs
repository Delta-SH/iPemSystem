using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class UserService : IUserService {

        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly ICacheManager _cacheManager;

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
        }

        #endregion

        #region Methods

        public User GetUser(string id) {
            return _userRepository.GetEntity(id);
        }

        public IPagedList<User> GetAllUsers(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _userRepository.GetEntities();
            return new PagedList<User>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
