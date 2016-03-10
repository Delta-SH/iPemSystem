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

        void Insert(List<OperateInRole> entities);

        void Delete(Guid role);

        void Delete(List<Guid> roles);
    }
}
