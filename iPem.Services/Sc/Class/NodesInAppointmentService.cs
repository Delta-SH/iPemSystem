using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class NodesInAppointmentService : INodesInAppointmentService {

        #region Fields

        private readonly INodesInAppointmentRepository _nodesInAppointmentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NodesInAppointmentService(
            INodesInAppointmentRepository nodesInAppointmentRepository, 
            ICacheManager cacheManager) {
            this._nodesInAppointmentRepository = nodesInAppointmentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<M_NodeInReservation> GetAllNodes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_NodeInReservation>(this.GetAllNodesAsList(), pageIndex, pageSize);
        }

        public List<M_NodeInReservation> GetAllNodesAsList() {
            return _nodesInAppointmentRepository.GetEntities();
        }

        public IPagedList<M_NodeInReservation> GetNodes(EnmSSH type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_NodeInReservation>(this.GetNodesAsList(type), pageIndex, pageSize);
        }

        public List<M_NodeInReservation> GetNodesAsList(EnmSSH type) {
            return _nodesInAppointmentRepository.GetEntities(type);
        }

        public IPagedList<M_NodeInReservation> GetNodes(Guid appointmentId, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_NodeInReservation>(this.GetNodesAsList(appointmentId), pageIndex, pageSize);
        }

        public List<M_NodeInReservation> GetNodesAsList(Guid appointmentId) {
            return _nodesInAppointmentRepository.GetEntities(appointmentId);
        }

        public void Add(List<M_NodeInReservation> nodes) {
            if(nodes == null)
                throw new ArgumentNullException("nodes");

            _nodesInAppointmentRepository.Insert(nodes);
        }

        public void Remove(Guid appointmentId) {
            if(appointmentId == default(Guid))
                throw new ArgumentNullException("appointmentId");

            _nodesInAppointmentRepository.Delete(appointmentId);
        }

        #endregion

    }
}