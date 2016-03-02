using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Operate in role repository interface
    /// </summary>
    public partial interface IOperateInRoleRepository {
        OperateInRole GetEntity(Guid role);

        void Insert(OperateInRole entity);

        void Insert(IList<OperateInRole> entities);

        void Delete(Guid role);

        void Delete(IList<Guid> roles);
    }
}
