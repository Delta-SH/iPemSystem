using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IMenuRepository {
        Menu GetEntity(int id);

        List<Menu> GetEntities();

        void Insert(Menu entity);

        void Insert(List<Menu> entities);

        void Update(Menu entity);

        void Update(List<Menu> entities);

        void Delete(Menu entity);

        void Delete(List<Menu> entities);
    }
}