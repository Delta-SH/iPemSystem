using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class ReservationService : IReservationService {

        #region Fields

        private readonly IM_ReservationRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ReservationService(
            IM_ReservationRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method

        public M_Reservation GetReservation(string id) {
            return _repository.GetReservation(id);
        }

        public List<M_Reservation> GetReservations() {
            return _repository.GetReservations();
        }

        public List<M_Reservation> GetReservationsInSpan(DateTime start, DateTime end) {
            return _repository.GetReservationsInSpan(start, end);
        }

        public IPagedList<M_Reservation> GetPagedReservationsInSpan(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Reservation>(this.GetReservationsInSpan(start, end), pageIndex, pageSize);
        }

        public IPagedList<M_Reservation> GetPagedReservations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Reservation>(this.GetReservations(), pageIndex, pageSize);
        }

        public void Add(params M_Reservation[] reservations) {
            if (reservations == null || reservations.Length == 0)
                throw new ArgumentNullException("reservations");

            _repository.Insert(reservations);
        }

        public void Update(params M_Reservation[] reservations) {
            if (reservations == null || reservations.Length == 0)
                throw new ArgumentNullException("reservations");

            _repository.Update(reservations);
        }

        public void Delete(params M_Reservation[] reservations) {
            if (reservations == null || reservations.Length == 0)
                throw new ArgumentNullException("reservations");

            _repository.Delete(reservations);
        }

        public void Check(string id, DateTime start, EnmResult status, string comment) {
            if (id == null || string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            _repository.Check(id, start, status, comment);
        }

        #endregion

    }
}