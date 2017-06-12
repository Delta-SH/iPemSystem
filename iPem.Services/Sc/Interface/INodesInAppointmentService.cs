using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface INodesInAppointmentService {
        IPagedList<M_NodeInReservation> GetAllNodes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_NodeInReservation> GetAllNodesAsList();

        IPagedList<M_NodeInReservation> GetNodes(EnmSSH type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_NodeInReservation> GetNodesAsList(EnmSSH type);

        IPagedList<M_NodeInReservation> GetNodes(Guid appointmentId, int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_NodeInReservation> GetNodesAsList(Guid appointmentId);

        void Add(List<M_NodeInReservation> nodes);

        void Remove(Guid appointmentId);
    }
}