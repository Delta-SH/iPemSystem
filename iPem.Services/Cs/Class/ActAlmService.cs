using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class ActAlmService : IActAlmService {

        #region Fields

        private readonly IActAlmRepository _actRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ActAlmService(
            IActAlmRepository actRepository,
            ICacheManager cacheManager) {
            this._actRepository = actRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<ActAlm> GetAlmsInArea(string area, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ActAlm>(this.GetAlmsInAreaAsList(area), pageIndex, pageSize);
        }

        public List<ActAlm> GetAlmsInAreaAsList(string area) {
            return _actRepository.GetEntitiesInArea(area);
        }

        public IPagedList<ActAlm> GetAlmsInStation(string station, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ActAlm>(this.GetAlmsInStationAsList(station), pageIndex, pageSize);
        }

        public List<ActAlm> GetAlmsInStationAsList(string station) {
            return _actRepository.GetEntitiesInStation(station);
        }

        public IPagedList<ActAlm> GetAlmsInRoom(string room, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ActAlm>(this.GetAlmsInRoomAsList(room), pageIndex, pageSize);
        }

        public List<ActAlm> GetAlmsInRoomAsList(string room) {
            return _actRepository.GetEntitiesInRoom(room);
        }

        public IPagedList<ActAlm> GetAlmsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ActAlm>(this.GetAlmsInDeviceAsList(device), pageIndex, pageSize);
        }

        public List<ActAlm> GetAlmsInDeviceAsList(string device) {
            return _actRepository.GetEntitiesInDevice(device);
        }

        public IPagedList<ActAlm> GetAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ActAlm>(this.GetAlmsAsList(start, end), pageIndex, pageSize);
        }

        public List<ActAlm> GetAlmsAsList(DateTime start, DateTime end) {
            return _actRepository.GetEntities(start, end);
        }

        public IPagedList<ActAlm> GetAllAlms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ActAlm>(this.GetAllAlmsAsList(), pageIndex, pageSize);
        }

        public List<ActAlm> GetAllAlmsAsList() {
            return _actRepository.GetEntities();
        }

        #endregion

    }
}
