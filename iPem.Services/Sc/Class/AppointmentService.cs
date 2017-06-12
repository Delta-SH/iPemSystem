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

        public M_Reservation GetAppointment(Guid id) {
            return _appointmentRepository.GetEntity(id);
        }

        public IPagedList<M_Reservation> GetAppointments(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Reservation>(this.GetAppointmentsAsList(start, end), pageIndex, pageSize);
        }

        public List<M_Reservation> GetAppointmentsAsList(DateTime start, DateTime end) {
            return _appointmentRepository.GetEntities(start, end);
        }

        public IPagedList<M_Reservation> GetAllAppointments(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Reservation>(this.GetAllAppointmentsAsList(), pageIndex, pageSize);
        }

        public List<M_Reservation> GetAllAppointmentsAsList() {
            return _appointmentRepository.GetEntities();
        }

        public void Add(M_Reservation entity) {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _appointmentRepository.Insert(entity);
        }

        public void AddRange(List<M_Reservation> entities) {
            if(entities == null)
                throw new ArgumentNullException("entities");

            _appointmentRepository.Insert(entities);
        }

        public void Update(M_Reservation entity) {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _appointmentRepository.Update(entity);
        }

        public void Delete(M_Reservation entity) {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _appointmentRepository.Delete(entity);
        }

        #endregion

    }
}