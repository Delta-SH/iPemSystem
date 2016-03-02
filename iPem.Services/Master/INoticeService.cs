using iPem.Core;
using iPem.Core.Domain.Master;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// NoticeService interface
    /// </summary>
    public partial interface INoticeService {
        Notice GetNotice(Guid id);

        IPagedList<Notice> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Notice> GetAllNotices(DateTime beginDate, DateTime endDate, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Notice> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        int GetUnreadCount(Guid uid);

        void AddNotice(Notice notice);

        void UpdateNotice(Notice notice);

        void DeleteNotice(Notice notice);
    }
}
