using iPem.Core;
using iPem.Core.Caching;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class BatCurveService : IBatCurveService {

        #region Fields

        private readonly IV_BatCurveRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BatCurveService(
            IV_BatCurveRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_BatCurve> GetProcedures(string device, DateTime start, DateTime end) {
            return _repository.GetProcedures(device, start, end);
        }

        public List<V_BatCurve> GetProcedures(string device, string point, DateTime start, DateTime end) {
            return _repository.GetProcedures(device, point, start, end);
        }

        public List<V_BatCurve> GetValues(string device, DateTime start, DateTime end) {
            return _repository.GetValues(device, start, end);
        }

        public List<V_BatCurve> GetValues(string device, string point, DateTime start, DateTime end) {
            return _repository.GetValues(device, point, start, end);
        }

        public List<V_BatCurve> GetValues(DateTime start, DateTime end) {
            return _repository.GetValues(start, end);
        }

        public IPagedList<V_BatCurve> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_BatCurve>(this.GetValues(start, end), pageIndex, pageSize);
        }

        #endregion
        
    }
}
