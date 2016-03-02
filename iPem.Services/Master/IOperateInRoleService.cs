using iPem.Core.Domain.Master;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// OperateInRoleService interface
    /// </summary>
    public partial interface IOperateInRoleService {
        OperateInRole GetOperateInRole(Guid id);

        void AddOperateInRole(OperateInRole operation);

        void DeleteOperateInRole(Guid id);
    }
}
