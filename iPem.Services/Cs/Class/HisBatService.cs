using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisBatService : IHisBatService {

        #region Fields

        private readonly IHisBatRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisBatService(
            IHisBatRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisBat> GetHisBats(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisBat>(this.GetHisBatsAsList(device, start, end), pageIndex, pageSize);
        }

        public List<HisBat> GetHisBatsAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(device, start, end);
        }

        public IPagedList<HisBat> GetHisBats(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisBat>(this.GetHisBatsAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<HisBat> GetHisBatsAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(device, point, start, end);
        }

        public IPagedList<HisBat> GetHisBats(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisBat>(this.GetHisBatsAsList(start, end), pageIndex, pageSize);
        }

        public List<HisBat> GetHisBatsAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        public List<HisBat> GetProcedures(string device, DateTime start, DateTime end) {
            return _hisRepository.GetProcedures(device, start, end);
        }

        public List<HisBat> GetProcedures(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetProcedures(device, point, start, end);
        }

        public List<HisBat> GetProcedures(DateTime start, DateTime end) {
            return _hisRepository.GetProcedures(start, end);
        }

        #endregion

    }
}
