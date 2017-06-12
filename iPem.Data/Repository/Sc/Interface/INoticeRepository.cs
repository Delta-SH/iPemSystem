using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface INoticeRepository {
        H_Notice GetEntity(Guid id);

        List<H_Notice> GetEntities();

        List<H_Notice> GetEntities(Guid uid);

        int GetUnreadCount(Guid uid);

        void Insert(H_Notice entity);

        void Insert(List<H_Notice> entities);

        void Update(H_Notice entity);

        void Update(List<H_Notice> entities);

        void Delete(H_Notice entity);

        void Delete(List<H_Notice> entities);
    }
}
