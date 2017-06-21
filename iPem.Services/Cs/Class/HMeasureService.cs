using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HMeasureService : IHMeasureService {

        #region Fields

        private readonly IV_HMeasureRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HMeasureService(
            IV_HMeasureRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_HMeasure> GetMeasuresInArea(string id, DateTime start, DateTime end) {
            return _repository.GetMeasuresInArea(id, start, end);
        }

        public List<V_HMeasure> GetMeasuresInStation(string id, DateTime start, DateTime end) {
            return _repository.GetMeasuresInStation(id, start, end);
        }

        public List<V_HMeasure> GetMeasuresInRoom(string id, DateTime start, DateTime end) {
            return _repository.GetMeasuresInRoom(id, start, end);
        }

        public List<V_HMeasure> GetMeasuresInDevice(string id, DateTime start, DateTime end) {
            return _repository.GetMeasuresInDevice(id, start, end);
        }

        public List<V_HMeasure> GetMeasuresInPoint(string device, string point, DateTime start, DateTime end) {
            return _repository.GetMeasuresInPoint(device, point, start, end);
        }

        public List<V_HMeasure> GetMeasures(DateTime start, DateTime end) {
            return _repository.GetMeasures(start, end);
        }

        public IPagedList<V_HMeasure> GetPagedMeasures(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_HMeasure>(this.GetMeasures(start, end), pageIndex, pageSize);
        }

        #endregion

    }
}
