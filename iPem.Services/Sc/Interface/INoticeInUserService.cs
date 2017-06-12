using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface INoticeInUserService {
        IPagedList<H_NoticeInUser> GetAllNotices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_NoticeInUser> GetAllNoticesAsList();

        IPagedList<H_NoticeInUser> GetNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_NoticeInUser> GetNoticesAsList(Guid uid);

        void Add(H_NoticeInUser notice);

        void AddRange(List<H_NoticeInUser> notices);

        void Update(H_NoticeInUser notice);

        void UpdateRange(List<H_NoticeInUser> notices);

        void Remove(H_NoticeInUser notice);

        void RemoveRange(List<H_NoticeInUser> notices);
    }
}
