using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
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

        public IPagedList<Appointment> GetAllAppointments(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _appointmentRepository.GetEntities();
            return new PagedList<Appointment>(result, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsByDate(DateTime startTime, DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _appointmentRepository.GetEntities(startTime, endTime);
            return new PagedList<Appointment>(result, pageIndex, pageSize);
        }

        public virtual Appointment GetAppointment(Guid id) {
            return _appointmentRepository.GetEntity(id);
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