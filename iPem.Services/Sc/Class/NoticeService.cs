using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Sc {
    public partial class NoticeService : INoticeService {

        #region Fields

        private readonly IH_NoticeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoticeService(
            IH_NoticeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public H_Notice GetNotice(Guid id) {
            return _repository.GetNotice(id);
        }

        public List<H_Notice> GetNotices() {
            return _repository.GetNotices();
        }

        public List<H_Notice> GetNoticesInSpan(DateTime start, DateTime end) {
            return _repository.GetNoticesInSpan(start, end);
        }

        public List<H_Notice> GetNoticesInUser(string uid) {
            return _repository.GetNoticesInUser(uid);
        }

        public List<H_Notice> GetUnreadNotices(string uid) {
            return _repository.GetUnreadNotices(uid);
        }

        public IPagedList<H_Notice> GetPagedNotices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetNotices(), pageIndex, pageSize);
        }

        public IPagedList<H_Notice> GetPagedNoticesInSpan(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetNoticesInSpan(start, end), pageIndex, pageSize);
        }

        public IPagedList<H_Notice> GetPagedNoticesInUser(string uid, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetNoticesInUser(uid), pageIndex, pageSize);
        }

        public IPagedList<H_Notice> GetPagedUnreadNotices(string uid, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetUnreadNotices(uid), pageIndex, pageSize);
        }

        public void Add(params H_Notice[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Insert(notices);
        }

        public void Update(params H_Notice[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Update(notices);
        }

        public void Remove(params H_Notice[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Delete(notices);
        }

        #endregion

    }
}