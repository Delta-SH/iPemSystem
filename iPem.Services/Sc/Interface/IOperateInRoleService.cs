using iPem.Core.Domain.Sc;
using System;

namespace iPem.Services.Sc {
    public partial interface IOperateInRoleService {
        OperateInRole GetOperates(Guid role);

        void Add(OperateInRole operation);

        void Remove(Guid role);
    }
}
