using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface INoticeService {
        Notice GetNotice(Guid id);

        IPagedList<Notice> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Notice> GetAllNoticesAsList();

        IPagedList<Notice> GetNotices(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Notice> GetNoticesAsList(DateTime start, DateTime end);

        IPagedList<Notice> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Notice> GetNoticesAsList(Guid uid);

        int GetUnreadCount(Guid uid);

        void Add(Notice notice);

        void Update(Notice notice);

        void Remove(Notice notice);
    }
}
