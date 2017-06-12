using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class NoticeInUserService : INoticeInUserService {

        #region Fields

        private readonly INoticeInUserRepository _noticesInUsersRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoticeInUserService(
            INoticeInUserRepository noticesInUsersRepository,
            ICacheManager cacheManager) {
            this._noticesInUsersRepository = noticesInUsersRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public virtual IPagedList<H_NoticeInUser> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_NoticeInUser>(this.GetAllNoticesAsList(), pageIndex, pageSize);
        }

        public virtual List<H_NoticeInUser> GetAllNoticesAsList() {
            return _noticesInUsersRepository.GetEntities();
        }

        public virtual IPagedList<H_NoticeInUser> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_NoticeInUser>(this.GetNoticesAsList(uid), pageIndex, pageSize);
        }

        public virtual List<H_NoticeInUser> GetNoticesAsList(Guid uid) {
            return _noticesInUsersRepository.GetEntities(uid);
        }

        public virtual void Add(H_NoticeInUser notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Insert(notice);
        }

        public virtual void AddRange(List<H_NoticeInUser> notices) {
            if(notices == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Insert(notices);
        }

        public virtual void Update(H_NoticeInUser notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Update(notice);
        }

        public virtual void UpdateRange(List<H_NoticeInUser> notices) {
            if(notices == null)
                throw new ArgumentNullException("notices");

            _noticesInUsersRepository.Update(notices);
        }

        public virtual void Remove(H_NoticeInUser notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Delete(notice);
        }

        public virtual void RemoveRange(List<H_NoticeInUser> notices) {
            if(notices == null)
                throw new ArgumentNullException("notices");

            _noticesInUsersRepository.Delete(notices);
        }

        #endregion

    }
}
