using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface INoticeService {
        H_Notice GetNotice(Guid id);

        IPagedList<H_Notice> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_Notice> GetAllNoticesAsList();

        IPagedList<H_Notice> GetNotices(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_Notice> GetNoticesAsList(DateTime start, DateTime end);

        IPagedList<H_Notice> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_Notice> GetNoticesAsList(Guid uid);

        int GetUnreadCount(Guid uid);

        void Add(H_Notice notice);

        void Update(H_Notice notice);

        void Remove(H_Notice notice);
    }
}
