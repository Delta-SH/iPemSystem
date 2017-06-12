using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisElecService : IHisElecService {

        #region Fields

        private readonly IV_ElecRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisElecService(
            IV_ElecRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<V_Elec> GetEnergies(string id, EnmSSH type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Elec>(this.GetEnergiesAsList(id, type, start, end), pageIndex, pageSize);
        }

        public List<V_Elec> GetEnergiesAsList(string id, EnmSSH type, DateTime start, DateTime end) {
            return _hisRepository.GetValues(id, type, start, end);
        }

        public IPagedList<V_Elec> GetEnergies(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Elec>(this.GetEnergiesAsList(id, type, formula, start, end), pageIndex, pageSize);
        }

        public List<V_Elec> GetEnergiesAsList(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _hisRepository.GetValues(id, type, formula, start, end);
        }

        public IPagedList<V_Elec> GetEnergies(EnmSSH type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Elec>(this.GetEnergiesAsList(type, formula, start, end), pageIndex, pageSize);
        }

        public List<V_Elec> GetEnergiesAsList(EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _hisRepository.GetValues(type, formula, start, end);
        }

        public IPagedList<V_Elec> GetEnergies(EnmSSH type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Elec>(this.GetEnergiesAsList(type, start, end), pageIndex, pageSize);
        }

        public List<V_Elec> GetEnergiesAsList(EnmSSH type, DateTime start, DateTime end) {
            return _hisRepository.GetValues(type, start, end);
        }

        public IPagedList<V_Elec> GetEnergies(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Elec>(this.GetEnergiesAsList(start, end), pageIndex, pageSize);
        }

        public List<V_Elec> GetEnergiesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetValues(start, end);
        }

        #endregion

    }
}
