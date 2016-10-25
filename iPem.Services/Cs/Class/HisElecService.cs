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

        private readonly IHisElecRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisElecService(
            IHisElecRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisElec> GetEnergies(string id, EnmOrganization type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisElec>(this.GetEnergiesAsList(id, type, start, end), pageIndex, pageSize);
        }

        public List<HisElec> GetEnergiesAsList(string id, EnmOrganization type, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(id, type, start, end);
        }

        public IPagedList<HisElec> GetEnergies(string id, EnmOrganization type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisElec>(this.GetEnergiesAsList(id, type, formula, start, end), pageIndex, pageSize);
        }

        public List<HisElec> GetEnergiesAsList(string id, EnmOrganization type, EnmFormula formula, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(id, type, formula, start, end);
        }

        public IPagedList<HisElec> GetEnergies(EnmOrganization type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisElec>(this.GetEnergiesAsList(type, formula, start, end), pageIndex, pageSize);
        }

        public List<HisElec> GetEnergiesAsList(EnmOrganization type, EnmFormula formula, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(type, formula, start, end);
        }

        public IPagedList<HisElec> GetEnergies(EnmOrganization type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisElec>(this.GetEnergiesAsList(type, start, end), pageIndex, pageSize);
        }

        public List<HisElec> GetEnergiesAsList(EnmOrganization type, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(type, start, end);
        }

        public IPagedList<HisElec> GetEnergies(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisElec>(this.GetEnergiesAsList(start, end), pageIndex, pageSize);
        }

        public List<HisElec> GetEnergiesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        #endregion

    }
}
