using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisValueService : IHisValueService {

        #region Fields

        private readonly IHisValueRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisValueService(
            IHisValueRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisValue> GetValuesByDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesByDeviceAsList(device, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesByDeviceAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByDevice(device, start, end);
        }

        public IPagedList<HisValue> GetValuesByPoint(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesByPointAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesByPointAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByPoint(device, point, start, end);
        }

        public IPagedList<HisValue> GetValuesByPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesByPointAsList(point, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesByPointAsList(string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByPoint(point, start, end);
        }

        public IPagedList<HisValue> GetValuesByPoint(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesByPointAsList(points, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesByPointAsList(string[] points, DateTime start, DateTime end) {
            return _hisRepository.GetEntitiesByPoint(points, start, end);
        }

        public IPagedList<HisValue> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesAsList(start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        #endregion

    }
}
