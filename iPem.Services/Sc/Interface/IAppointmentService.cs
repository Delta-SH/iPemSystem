using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IAppointmentService {
        M_Reservation GetAppointment(Guid id);

        IPagedList<M_Reservation> GetAppointments(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Reservation> GetAppointmentsAsList(DateTime start, DateTime end);

        IPagedList<M_Reservation> GetAllAppointments(int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Reservation> GetAllAppointmentsAsList();

        void Add(M_Reservation entity);

        void AddRange(List<M_Reservation> entities);

        void Update(M_Reservation entity);

        void Delete(M_Reservation entity);
    }
}