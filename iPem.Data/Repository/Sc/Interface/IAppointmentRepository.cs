using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IAppointmentRepository {
        List<M_Reservation> GetEntities();

        List<M_Reservation> GetEntities(DateTime startDate, DateTime endDate);

        M_Reservation GetEntity(Guid id);

        void Insert(M_Reservation entity);

        void Insert(List<M_Reservation> entities);

        void Update(M_Reservation entity);

        void Update(List<M_Reservation> entities);

        void Delete(M_Reservation entity);

        void Delete(List<M_Reservation> entities);
    }
}