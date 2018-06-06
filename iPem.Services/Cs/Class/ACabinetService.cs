using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class ACabinetService : IACabinetService {

        #region Fields

        private readonly IV_ACabinetRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ACabinetService(
            IV_ACabinetRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_ACabinet> GetHistory(DateTime start, DateTime end) {
            return _repository.GetHistory(start, end);
        }

        public List<V_ACabinet> GetHistory(EnmVSignalCategory category, DateTime start, DateTime end) {
            return _repository.GetHistory(category, start, end);
        }

        public List<V_ACabinet> GetHistory(string device, EnmVSignalCategory category, DateTime start, DateTime end) {
            return _repository.GetHistory(device, category, start, end);
        }

        public List<V_ACabinet> GetHistory(string device, string point, DateTime start, DateTime end) {
            return _repository.GetHistory(device, point, start, end);
        }

        public List<V_ACabinet> GetLast(string device, EnmVSignalCategory category, DateTime start, DateTime end) {
            return _repository.GetLast(device, category, start, end);
        }

        public List<V_ACabinet> GetLast(EnmVSignalCategory category, DateTime start, DateTime end) {
            return _repository.GetLast(category, start, end);
        }

        #endregion
        
    }
}
