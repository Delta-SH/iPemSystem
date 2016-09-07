using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IAppointmentService {
        Appointment GetAppointment(Guid id);

        IPagedList<Appointment> GetAppointments(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Appointment> GetAppointmentsAsList(DateTime start, DateTime end);

        IPagedList<Appointment> GetAllAppointments(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Appointment> GetAllAppointmentsAsList();

        void Add(Appointment entity);

        void AddRange(List<Appointment> entities);

        void Update(Appointment entity);

        void Delete(Appointment entity);
    }
}