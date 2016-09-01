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

        public IPagedList<HisValue> GetValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesAsList(device, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(device, start, end);
        }

        public IPagedList<HisValue> GetValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(device, point, start, end);
        }

        public IPagedList<HisValue> GetValues(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisValue>(this.GetValuesAsList(points, start, end), pageIndex, pageSize);
        }

        public List<HisValue> GetValuesAsList(string[] points, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(points, start, end);
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
