using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial class ProfileService : IProfileService {

        #region Fields

        private readonly IProfileRepository _profileRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProfileService(
            IProfileRepository profileRepository,
            ICacheManager cacheManager) {
            this._profileRepository = profileRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public UserProfile GetUserProfile(Guid userId) {
            return _profileRepository.GetEntity(userId);
        }

        public void SaveUserProfile(UserProfile profile) {
            var key = string.Format(GlobalCacheKeys.Ur_RssPointsResultPattern, profile.UserId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _profileRepository.Save(profile);
        }

        public void DeleteUserProfile(Guid userId) {
            var key = string.Format(GlobalCacheKeys.Ur_RssPointsResultPattern, userId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _profileRepository.Delete(userId);
        }

        #endregion

    }
}
