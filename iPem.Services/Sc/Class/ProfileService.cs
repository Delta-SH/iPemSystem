using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class ProfileService : IProfileService {

        #region Fields

        private readonly IU_ProfileRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProfileService(
            IU_ProfileRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_Profile GetProfile(Guid uid) {
            return _repository.GetProfile(uid);
        }

        public void Save(U_Profile profile) {
            _repository.Save(profile);
        }

        public void Remove(Guid uid) {
            _repository.Delete(uid);
        }

        #endregion

    }
}
