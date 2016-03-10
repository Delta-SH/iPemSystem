using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Role repository interface
    /// </summary>
    public partial interface IRoleRepository {
        Role GetEntity(Guid id);

        Role GetEntity(string name);

        Role GetEntityByUid(Guid uid);

        List<Role> GetEntities();

        void Insert(Role entity);

        void Insert(List<Role> entities);

        void Update(Role entity);

        void Update(List<Role> entities);

        void Delete(Role entity);

        void Delete(List<Role> entities);
    }
}