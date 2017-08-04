using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class AMeasureService : IAMeasureService {

        #region Fields

        private readonly IV_AMeasureRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AMeasureService(
            IV_AMeasureRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public V_AMeasure GetMeasure(string device, string signalId, string signalNumber) {
            return _repository.GetMeasure(device, signalId, signalNumber);
        }

        public List<V_AMeasure> GetMeasuresInArea(string id) {
            return _repository.GetMeasuresInArea(id);
        }

        public List<V_AMeasure> GetMeasuresInStation(string id) {
            return _repository.GetMeasuresInStation(id);
        }

        public List<V_AMeasure> GetMeasuresInRoom(string id) {
            return _repository.GetMeasuresInStation(id);
        }

        public List<V_AMeasure> GetMeasuresInDevice(string id) {
            return _repository.GetMeasuresInDevice(id);
        }

        public List<V_AMeasure> GetMeasures(IList<ValuesPair<string, string, string>> keys) {
            return _repository.GetMeasures(keys);
        }

        public List<V_AMeasure> GetMeasures() {
            return _repository.GetMeasures();
        }

        public IPagedList<V_AMeasure> GetPagedMeasures(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_AMeasure>(this.GetMeasures(), pageIndex, pageSize);
        }

        #endregion
        
    }
}