using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Notice in users repository interface
    /// </summary>
    public partial interface INoticeInUserRepository {
        IList<NoticeInUser> GetEntities();

        IList<NoticeInUser> GetEntities(Guid uid);

        void Insert(NoticeInUser entity);

        void Insert(IList<NoticeInUser> entities);

        void Update(NoticeInUser entity);

        void Update(IList<NoticeInUser> entities);

        void Delete(NoticeInUser entity);

        void Delete(IList<NoticeInUser> entities);
    }
}
