using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface INoticeInUserService {
        IPagedList<NoticeInUser> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<NoticeInUser> GetAllNoticesAsList();

        IPagedList<NoticeInUser> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        List<NoticeInUser> GetNoticesAsList(Guid uid);

        void Add(NoticeInUser notice);

        void AddRange(List<NoticeInUser> notices);

        void Update(NoticeInUser notice);

        void UpdateRange(List<NoticeInUser> notices);

        void Remove(NoticeInUser notice);

        void RemoveRange(List<NoticeInUser> notices);
    }
}
