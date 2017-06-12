using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface INodesInAppointmentRepository {
        List<M_NodeInReservation> GetEntities();

        List<M_NodeInReservation> GetEntities(EnmSSH type);

        List<M_NodeInReservation> GetEntities(Guid appointmentId);

        void Insert(M_NodeInReservation entity);

        void Insert(List<M_NodeInReservation> entities);

        void Delete(Guid appointmentId);

        void Delete(List<Guid> appointmentIds);
    }
}