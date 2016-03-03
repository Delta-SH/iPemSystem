using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using System;
using System.Linq;

namespace iPem.Services.Master {
    /// <summary>
    /// Notice service
    /// </summary>
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

        /// <summary>
        /// Gets a notice by identifier
        /// </summary>
        /// <param name="id">notice identifier</param>
        /// <returns>notice</returns>
        public virtual Notice GetNotice(Guid id) {
            return _noticeRepository.GetEntity(id);
        }

        /// <summary>
        /// Gets all notices
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>notices collection</returns>
        public virtual IPagedList<Notice> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _noticeRepository.GetEntities();
            return new PagedList<Notice>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all notices
        /// </summary>
        /// <param name="beginDate">Begin date</param>
        /// <param name="endDate">End date</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>notices collection</returns>
        public virtual IPagedList<Notice> GetAllNotices(DateTime beginDate, DateTime endDate, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _noticeRepository.GetEntities().ToList();
            if(result.Count > 0)
                result = result.FindAll(r => r.CreatedTime >= beginDate && r.CreatedTime <= endDate);

            return new PagedList<Notice>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all notices by uid
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Role collection</returns>
        public virtual IPagedList<Notice> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _noticeRepository.GetEntities(uid);
            return new PagedList<Notice>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets unread notice count by uid
        /// </summary>
        /// <param name="uid">user id</param>
        /// <returns>the count of the unread notices</returns>
        public virtual int GetUnreadCount(Guid uid) {
            return _noticeRepository.GetUnreadCount(uid);
        }

        /// <summary>
        /// Add a notice
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void AddNotice(Notice notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticeRepository.Insert(notice);
        }

        /// <summary>
        /// Update the notice
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void UpdateNotice(Notice notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticeRepository.Update(notice);
        }

        /// <summary>
        /// Marks a notice as deleted 
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void DeleteNotice(Notice notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticeRepository.Delete(notice);
        }

        #endregion

    }
}