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

        private readonly INoticeRepository _noticeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoticeService(
            INoticeRepository noticeRepository,
            ICacheManager cacheManager) {
            this._noticeRepository = noticeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public virtual H_Notice GetNotice(Guid id) {
            return _noticeRepository.GetEntity(id);
        }

        public virtual IPagedList<H_Notice> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetAllNoticesAsList(), pageIndex, pageSize);
        }

        public virtual List<H_Notice> GetAllNoticesAsList() {
            return _noticeRepository.GetEntities();
        }

        public virtual IPagedList<H_Notice> GetNotices(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetNoticesAsList(start, end), pageIndex, pageSize);
        }

        public virtual List<H_Notice> GetNoticesAsList(DateTime start, DateTime end) {
            var result = this.GetAllNoticesAsList();
            if(result.Count > 0) result = result.FindAll(r => r.CreatedTime >= start && r.CreatedTime <= end);
            return result;
        }

        public virtual IPagedList<H_Notice> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Notice>(this.GetNoticesAsList(uid), pageIndex, pageSize);
        }

        public virtual List<H_Notice> GetNoticesAsList(Guid uid) {
            return _noticeRepository.GetEntities(uid);
        }

        public virtual int GetUnreadCount(Guid uid) {
            return _noticeRepository.GetUnreadCount(uid);
        }

        public virtual void Add(H_Notice notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticeRepository.Insert(notice);
        }

        public virtual void Update(H_Notice notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticeRepository.Update(notice);
        }

        public virtual void Remove(H_Notice notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticeRepository.Delete(notice);
        }

        #endregion

    }
}