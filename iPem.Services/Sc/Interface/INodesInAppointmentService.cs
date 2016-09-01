using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface INodesInAppointmentService {
        IPagedList<NodesInAppointment> GetAllNodes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<NodesInAppointment> GetAllNodesAsList();

        IPagedList<NodesInAppointment> GetNodes(EnmOrganization type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<NodesInAppointment> GetNodesAsList(EnmOrganization type);

        IPagedList<NodesInAppointment> GetNodes(Guid appointmentId, int pageIndex = 0, int pageSize = int.MaxValue);

        List<NodesInAppointment> GetNodesAsList(Guid appointmentId);

        void Add(List<NodesInAppointment> nodes);

        void Remove(Guid appointmentId);
    }
}