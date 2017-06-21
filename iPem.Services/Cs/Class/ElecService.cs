using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class ElecService : IElecService {

        #region Fields

        private readonly IV_ElecRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ElecService(
            IV_ElecRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Elec> GetEnergies(string id, EnmSSH type, DateTime start, DateTime end) {
            return _repository.GetEnergies(id, type, start, end);
        }

        public List<V_Elec> GetEnergies(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _repository.GetEnergies(id, type, formula, start, end);
        }

        public List<V_Elec> GetEnergies(EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _repository.GetEnergies(type, formula, start, end);
        }

        public List<V_Elec> GetEnergies(EnmSSH type, DateTime start, DateTime end) {
            return _repository.GetEnergies(type, start, end);
        }

        public List<V_Elec> GetEnergies(DateTime start, DateTime end) {
            return _repository.GetEnergies(start, end);
        }

        public IPagedList<V_Elec> GetPagedEnergies(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Elec>(this.GetEnergies(start, end), pageIndex, pageSize);
        }

        #endregion

    }
}
