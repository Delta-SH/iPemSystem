using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Menu repository interface
    /// </summary>
    public partial interface IMenuRepository {
        Menu GetEntity(int id);

        IList<Menu> GetEntities();

        void Insert(Menu entity);

        void Insert(IList<Menu> entities);

        void Update(Menu entity);

        void Update(IList<Menu> entities);

        void Delete(Menu entity);

        void Delete(IList<Menu> entities);
    }
}
