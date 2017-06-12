using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisBatService : IHisBatService {

        #region Fields

        private readonly IV_BatRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisBatService(
            IV_BatRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<V_Bat> GetHisBats(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Bat>(this.GetHisBatsAsList(device, start, end), pageIndex, pageSize);
        }

        public List<V_Bat> GetHisBatsAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetValuesInDevice(device, start, end);
        }

        public IPagedList<V_Bat> GetHisBats(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Bat>(this.GetHisBatsAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<V_Bat> GetHisBatsAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetValuesInPoint(device, point, start, end);
        }

        public IPagedList<V_Bat> GetHisBats(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Bat>(this.GetHisBatsAsList(start, end), pageIndex, pageSize);
        }

        public List<V_Bat> GetHisBatsAsList(DateTime start, DateTime end) {
            return _hisRepository.GetValues(start, end);
        }

        public List<V_Bat> GetProcedures(string device, DateTime start, DateTime end) {
            return _hisRepository.GetProcedures(device, start, end);
        }

        public List<V_Bat> GetProcedures(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetProcedures(device, point, start, end);
        }

        public List<V_Bat> GetProcedures(DateTime start, DateTime end) {
            return _hisRepository.GetProcedures(start, end);
        }

        #endregion

    }
}
