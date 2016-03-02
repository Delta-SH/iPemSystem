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

        IList<Role> GetEntities();

        void Insert(Role entity);

        void Insert(IList<Role> entities);

        void Update(Role entity);

        void Update(IList<Role> entities);

        void Delete(Role entity);

        void Delete(IList<Role> entities);
    }
}