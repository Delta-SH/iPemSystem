using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Notice in users repository interface
    /// </summary>
    public partial interface INoticeInUserRepository {
        List<NoticeInUser> GetEntities();

        List<NoticeInUser> GetEntities(Guid uid);

        void Insert(NoticeInUser entity);

        void Insert(List<NoticeInUser> entities);

        void Update(NoticeInUser entity);

        void Update(List<NoticeInUser> entities);

        void Delete(NoticeInUser entity);

        void Delete(List<NoticeInUser> entities);
    }
}
