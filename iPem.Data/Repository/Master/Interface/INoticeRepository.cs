using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Notice repository interface
    /// </summary>
    public partial interface INoticeRepository {
        Notice GetEntity(Guid id);

        List<Notice> GetEntities();

        List<Notice> GetEntities(Guid uid);

        int GetUnreadCount(Guid uid);

        void Insert(Notice entity);

        void Insert(List<Notice> entities);

        void Update(Notice entity);

        void Update(List<Notice> entities);

        void Delete(Notice entity);

        void Delete(List<Notice> entities);
    }
}
