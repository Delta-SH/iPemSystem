using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisAlmService : IHisAlmService {

        #region Fields

        private readonly IHisAlmRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisAlmService(
            IHisAlmRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisAlm> GetAlmsInArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisAlm>(this.GetAlmsInAreaAsList(area, start, end), pageIndex, pageSize);
        }

        public List<HisAlm> GetAlmsInAreaAsList(string area, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesInArea(area, start, end);
        }

        public IPagedList<HisAlm> GetAlmsInStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisAlm>(this.GetAlmsInStationAsList(station, start, end), pageIndex, pageSize);
        }

        public List<HisAlm> GetAlmsInStationAsList(string station, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesInStation(station, start, end);
        }

        public IPagedList<HisAlm> GetAlmsInRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisAlm>(this.GetAlmsInRoomAsList(room, start, end), pageIndex, pageSize);
        }

        public List<HisAlm> GetAlmsInRoomAsList(string room, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesInRoom(room, start, end);
        }

        public IPagedList<HisAlm> GetAlmsInDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisAlm>(this.GetAlmsInDeviceAsList(device, start, end), pageIndex, pageSize);
        }

        public List<HisAlm> GetAlmsInDeviceAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesInDevice(device, start, end);
        }

        public IPagedList<HisAlm> GetAlmsInPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisAlm>(this.GetAlmsInPointAsList(point, start, end), pageIndex, pageSize);
        }

        public List<HisAlm> GetAlmsInPointAsList(string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(point, start, end);
        }

        public IPagedList<HisAlm> GetAllAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisAlm>(this.GetAllAlmsAsList(start, end), pageIndex, pageSize);
        }

        public List<HisAlm> GetAllAlmsAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        #endregion

    }
}
