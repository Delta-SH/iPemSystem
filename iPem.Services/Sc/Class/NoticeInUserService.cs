using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class NoticeInUserService : INoticeInUserService {

        #region Fields

        private readonly IH_NoticeInUserRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoticeInUserService(
            IH_NoticeInUserRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_NoticeInUser> GetNoticesInUsers() {
            return _repository.GetNoticesInUsers();
        }

        public List<H_NoticeInUser> GetNoticesInUser(string uid) {
            return _repository.GetNoticesInUser(uid);
        }

        public void Add(params H_NoticeInUser[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Insert(notices);
        }

        public void Update(params H_NoticeInUser[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Update(notices);
        }

        public void Remove(params H_NoticeInUser[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Delete(notices);
        }

        #endregion

    }
}
