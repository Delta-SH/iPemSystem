using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisValueService : IHisValueService {

        #region Fields

        private readonly IV_HMeasureRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisValueService(
            IV_HMeasureRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<V_HMeasure> GetValuesByArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByAreaAsList(area, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByAreaAsList(string area, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByArea(area, start, end);
        }

        public IPagedList<V_HMeasure> GetValuesByStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByStationAsList(station, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByStationAsList(string station, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByStation(station, start, end);
        }

        public IPagedList<V_HMeasure> GetValuesByRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByRoomAsList(room, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByRoomAsList(string room, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByRoom(room, start, end);
        }

        public IPagedList<V_HMeasure> GetValuesByDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByDeviceAsList(device, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByDeviceAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByDevice(device, start, end);
        }

        public IPagedList<V_HMeasure> GetValuesByPoint(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByPointAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByPointAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByPoint(device, point, start, end);
        }

        public IPagedList<V_HMeasure> GetValuesByPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByPointAsList(point, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByPointAsList(string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByPoint(point, start, end);
        }

        public IPagedList<V_HMeasure> GetValuesByPoint(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesByPointAsList(points, start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesByPointAsList(string[] points, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByPoint(points, start, end);
        }

        public IPagedList<V_HMeasure> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetValuesAsList(start, end), pageIndex, pageSize);
        }

        public List<V_HMeasure> GetValuesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        #endregion

    }
}
