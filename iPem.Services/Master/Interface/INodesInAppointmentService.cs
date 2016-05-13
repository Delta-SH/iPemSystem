using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial interface INodesInAppointmentService {
        IPagedList<NodesInAppointment> GetAllNodes(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<NodesInAppointment> GetNodesByType(EnmOrganization type, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<NodesInAppointment> GetNodesInAppointment(Guid appointmentId, int pageIndex = 0, int pageSize = int.MaxValue);

        void AddNodesInAppointment(List<NodesInAppointment> nodes);

        void DeleteNodesInAppointment(Guid appointmentId);
    }
}