using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
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

        public List<V_BatCurve> GetProcedures(string device, int pack, DateTime start, DateTime end) {
            return _repository.GetProcedures(device, pack, start, end);
        }

        public List<V_BatCurve> GetProcedures(string device, int pack, EnmBatPoint ptype, DateTime start, DateTime end) {
            return _repository.GetProcedures(device, pack, ptype, start, end);
        }

        public List<V_BatCurve> GetValues(string device, int pack, DateTime start, DateTime end) {
            return _repository.GetValues(device, pack, start, end);
        }

        public List<V_BatCurve> GetValues(string device, int pack, EnmBatPoint ptype, DateTime start, DateTime end) {
            return _repository.GetValues(device, pack, ptype, start, end);
        }

        public List<V_BatCurve> GetValues(DateTime start, DateTime end) {
            return _repository.GetValues(start, end);
        }

        public List<V_BatCurve> GetMinValues(EnmBatStatus type, EnmBatPoint ptype, DateTime start, DateTime end) {
            return _repository.GetMinValues(type, ptype, start, end);
        }

        public List<V_BatCurve> GetMaxValues(EnmBatStatus type, EnmBatPoint ptype, DateTime start, DateTime end) {
            return _repository.GetMaxValues(type, ptype, start, end);
        }

        #endregion
        
    }
}
