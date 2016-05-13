using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial interface IAppointmentService {
        IPagedList<Appointment> GetAllAppointments(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Appointment> GetAppointmentsByDate(DateTime startTime, DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue);

        Appointment GetAppointment(Guid id);

        void AddAppointment(Appointment entity);

        void AddAppointments(List<Appointment> entities);

        void UpdateAppointment(Appointment entity);

        void DeleteAppointment(Appointment entity);
    }
}