using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Services.Master {
    /// <summary>
    /// Notice service
    /// </summary>
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

        /// <summary>
        /// Gets all notices
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>notices collection</returns>
        public virtual IPagedList<NoticeInUser> GetAllNoticesInUsers(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _noticesInUsersRepository.GetEntities();
            return new PagedList<NoticeInUser>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all notices by uid
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Role collection</returns>
        public virtual IPagedList<NoticeInUser> GetNoticesInUser(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _noticesInUsersRepository.GetEntities(uid);
            return new PagedList<NoticeInUser>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Add a notice
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void AddNoticeInUser(NoticeInUser notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Insert(notice);
        }

        /// <summary>
        /// Add the notices
        /// </summary>
        /// <param name="notices">notices</param>
        public virtual void AddNoticesInUsers(IList<NoticeInUser> notices) {
            if(notices == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Insert(notices);
        }

        /// <summary>
        /// Update the notice
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void UpdateNoticeInUser(NoticeInUser notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Update(notice);
        }

        /// <summary>
        /// Update the notices
        /// </summary>
        /// <param name="notices">notices</param>
        public virtual void UpdateNoticesInUsers(IList<NoticeInUser> notices) {
            if(notices == null)
                throw new ArgumentNullException("notices");

            _noticesInUsersRepository.Update(notices);
        }

        /// <summary>
        /// Marks a notice as deleted 
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void DeleteNoticesInUser(NoticeInUser notice) {
            if(notice == null)
                throw new ArgumentNullException("notice");

            _noticesInUsersRepository.Delete(notice);
        }

        /// <summary>
        /// Marks a notice as deleted 
        /// </summary>
        /// <param name="notice">notice</param>
        public virtual void DeleteNoticesInUsers(IList<NoticeInUser> notices) {
            if(notices == null)
                throw new ArgumentNullException("notices");

            _noticesInUsersRepository.Delete(notices);
        }

        #endregion
    }
}
