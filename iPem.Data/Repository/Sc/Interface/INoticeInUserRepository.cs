using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface INoticeInUserRepository {
        List<H_NoticeInUser> GetEntities();

        List<H_NoticeInUser> GetEntities(Guid uid);

        void Insert(H_NoticeInUser entity);

        void Insert(List<H_NoticeInUser> entities);

        void Update(H_NoticeInUser entity);

        void Update(List<H_NoticeInUser> entities);

        void Delete(H_NoticeInUser entity);

        void Delete(List<H_NoticeInUser> entities);
    }
}
