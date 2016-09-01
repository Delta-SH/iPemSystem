using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class AppointmentService : IAppointmentService {

        #region Fields

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            ICacheManager cacheManager) {
            this._appointmentRepository = appointmentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method

        public Appointment GetAppointment(Guid id) {
            return _appointmentRepository.GetEntity(id);
        }

        public IPagedList<Appointment> GetAppointments(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Appointment>(this.GetAppointmentsAsList(start, end), pageIndex, pageSize);
        }

        public List<Appointment> GetAppointmentsAsList(DateTime start, DateTime end) {
            return _appointmentRepository.GetEntities(start, end);
        }

        public IPagedList<Appointment> GetAllAppointments(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Appointment>(this.GetAllAppointmentsAsList(), pageIndex, pageSize);
        }

        public List<Appointment> GetAllAppointmentsAsList() {
            return _appointmentRepository.GetEntities();
        }

        public void AddAppointment(Appointment entity) {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _appointmentRepository.Insert(entity);
        }

        public void AddAppointments(List<Appointment> entities) {
            if(entities == null)
                throw new ArgumentNullException("entities");

            _appointmentRepository.Insert(entities);
        }

        public void UpdateAppointment(Appointment entity) {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _appointmentRepository.Update(entity);
        }

        public void DeleteAppointment(Appointment entity) {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _appointmentRepository.Delete(entity);
        }

        #endregion

    }
}