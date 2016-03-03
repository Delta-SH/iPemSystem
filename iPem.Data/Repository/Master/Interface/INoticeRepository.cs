using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Notice repository interface
    /// </summary>
    public partial interface INoticeRepository {
        Notice GetEntity(Guid id);

        IList<Notice> GetEntities();

        IList<Notice> GetEntities(Guid uid);

        int GetUnreadCount(Guid uid);

        void Insert(Notice entity);

        void Insert(IList<Notice> entities);

        void Update(Notice entity);

        void Update(IList<Notice> entities);

        void Delete(Notice entity);

        void Delete(IList<Notice> entities);
    }
}
