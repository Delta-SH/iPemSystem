using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IMenuRepository {
        U_Menu GetEntity(int id);

        List<U_Menu> GetEntities();

        void Insert(U_Menu entity);

        void Insert(List<U_Menu> entities);

        void Update(U_Menu entity);

        void Update(List<U_Menu> entities);

        void Delete(U_Menu entity);

        void Delete(List<U_Menu> entities);
    }
}