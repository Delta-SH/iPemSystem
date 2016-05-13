using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Repository.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial class NodesInAppointmentService : INodesInAppointmentService {

        #region Fields

        private readonly INodesInAppointmentRepository _nodesInAppointmentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NodesInAppointmentService(INodesInAppointmentRepository nodesInAppointmentRepository, ICacheManager cacheManager) {
            this._nodesInAppointmentRepository = nodesInAppointmentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<NodesInAppointment> GetAllNodes(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _nodesInAppointmentRepository.GetEntities();
            return new PagedList<NodesInAppointment>(result, pageIndex, pageSize);
        }

        public IPagedList<NodesInAppointment> GetNodesByType(EnmOrganization type, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _nodesInAppointmentRepository.GetEntities(type);
            return new PagedList<NodesInAppointment>(result, pageIndex, pageSize);
        }

        public IPagedList<NodesInAppointment> GetNodesInAppointment(Guid appointmentId, int pageIndex = 0, int pageSize = int.MaxValue) {
            if(string.IsNullOrWhiteSpace(appointmentId.ToString()))
                return this.GetAllNodes(pageIndex, pageSize);

            var result = _nodesInAppointmentRepository.GetEntities(appointmentId);
            return new PagedList<NodesInAppointment>(result, pageIndex, pageSize);
        }

        public void AddNodesInAppointment(List<NodesInAppointment> nodes) {
            if(nodes == null)
                throw new ArgumentNullException("nodes");

            _nodesInAppointmentRepository.Insert(nodes);
        }

        public void DeleteNodesInAppointment(Guid appointmentId) {
            if(appointmentId == null)
                throw new ArgumentNullException("appointmentId");

            _nodesInAppointmentRepository.Delete(appointmentId);
        }

        #endregion

    }
}