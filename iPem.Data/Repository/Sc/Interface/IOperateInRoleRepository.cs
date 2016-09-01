using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IOperateInRoleRepository {
        OperateInRole GetEntity(Guid role);

        void Insert(OperateInRole entity);

        void Insert(List<OperateInRole> entities);

        void Delete(Guid role);

        void Delete(List<Guid> roles);
    }
}
