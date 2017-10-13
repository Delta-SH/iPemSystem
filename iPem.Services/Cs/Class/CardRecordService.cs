using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class CardRecordService : ICardRecordService {

        #region Fields

        private readonly IH_CardRecordRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public CardRecordService(
            IH_CardRecordRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_CardRecord> GetRecords(DateTime start, DateTime end) {
            return _repository.GetEntities(start, end);
        }

        public List<H_CardRecord> GetRecordsInCard(DateTime start, DateTime end, string id) {
            return _repository.GetEntitiesInCard(start, end, id);
        }

        public List<H_CardRecord> GetRecordsInDevice(DateTime start, DateTime end, string id) {
            return _repository.GetEntitiesInDevice(start, end, id);
        }

        public List<H_CardRecord> GetRecordsInRoom(DateTime start, DateTime end, string id) {
            return _repository.GetEntitiesInRoom(start, end, id);
        }

        public List<H_CardRecord> GetRecordsInStation(DateTime start, DateTime end, string id) {
            return _repository.GetEntitiesInStation(start, end, id);
        }

        public List<H_CardRecord> GetRecordsInArea(DateTime start, DateTime end, string id) {
            return _repository.GetEntitiesInArea(start, end, id);
        }

        #endregion
        
    }
}
