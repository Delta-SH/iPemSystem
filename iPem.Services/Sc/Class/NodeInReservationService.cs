using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class NodeInReservationService : INodeInReservationService {

        #region Fields

        private readonly IM_NodesInReservationRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NodeInReservationService(
            IM_NodesInReservationRepository repository, 
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<M_NodeInReservation> GetNodesInReservations() {
            return _repository.GetNodesInReservations();
        }

        public List<M_NodeInReservation> GetNodesInReservationsInType(EnmSSH type) {
            return _repository.GetNodesInReservationsInType(type);
        }

        public List<M_NodeInReservation> GetNodesInReservationsInReservation(string id) {
            return _repository.GetNodesInReservationsInReservation(id);
        }

        public IPagedList<M_NodeInReservation> GetPagedNodesInReservations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_NodeInReservation>(this.GetNodesInReservations(), pageIndex, pageSize);
        }

        public void Add(params M_NodeInReservation[] nodes) {
            if(nodes == null || nodes.Length == 0)
                throw new ArgumentNullException("nodes");

            _repository.Insert(nodes);
        }

        public void Remove(params string[] ids) {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException("ids");

            _repository.Delete(ids);
        }

        #endregion

    }
}