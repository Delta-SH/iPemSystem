using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface INodesInAppointmentRepository {
        List<NodesInAppointment> GetEntities();

        List<NodesInAppointment> GetEntities(EnmOrganization type);

        List<NodesInAppointment> GetEntities(Guid appointmentId);

        void Insert(NodesInAppointment entity);

        void Insert(List<NodesInAppointment> entities);

        void Delete(Guid appointmentId);

        void Delete(List<Guid> appointmentIds);
    }
}