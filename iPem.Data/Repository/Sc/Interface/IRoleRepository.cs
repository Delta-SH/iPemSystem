using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IRoleRepository {
        U_Role GetEntity(Guid id);

        U_Role GetEntity(string name);

        U_Role GetEntityByUid(Guid uid);

        List<U_Role> GetEntities();

        void Insert(U_Role entity);

        void Insert(List<U_Role> entities);

        void Update(U_Role entity);

        void Update(List<U_Role> entities);

        void Delete(U_Role entity);

        void Delete(List<U_Role> entities);
    }
}