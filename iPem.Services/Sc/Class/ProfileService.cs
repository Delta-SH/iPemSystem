using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
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

        public U_Profile GetProfile(Guid uid) {
            return _profileRepository.GetEntity(uid);
        }

        public void Save(U_Profile profile) {
            _profileRepository.Save(profile);
        }

        public void Remove(Guid uid) {
            _profileRepository.Delete(uid);
        }

        #endregion

    }
}
