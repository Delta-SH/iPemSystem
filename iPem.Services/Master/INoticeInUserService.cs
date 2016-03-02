using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// Notice in users service interface
    /// </summary>
    public partial interface INoticeInUserService {
        IPagedList<NoticeInUser> GetAllNoticesInUsers(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<NoticeInUser> GetNoticesInUser(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        void AddNoticeInUser(NoticeInUser notice);

        void AddNoticesInUsers(IList<NoticeInUser> notices);

        void UpdateNoticeInUser(NoticeInUser notice);

        void UpdateNoticesInUsers(IList<NoticeInUser> notices);

        void DeleteNoticesInUser(NoticeInUser notice);

        void DeleteNoticesInUsers(IList<NoticeInUser> notices);
    }
}
