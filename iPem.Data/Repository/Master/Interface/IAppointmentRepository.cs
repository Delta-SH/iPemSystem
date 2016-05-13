using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Appointment repository interface
    /// </summary>
    public partial interface IAppointmentRepository {
        List<Appointment> GetEntities();

        List<Appointment> GetEntities(DateTime startDate, DateTime endDate);

        Appointment GetEntity(Guid id);

        void Insert(Appointment entity);

        void Insert(List<Appointment> entities);

        void Update(Appointment entity);

        void Update(List<Appointment> entities);

        void Delete(Appointment entity);

        void Delete(List<Appointment> entities);
    }
}