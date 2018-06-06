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

        public List<V_Elec> GetActive() {
            return _repository.GetActive();
        }

        public List<V_Elec> GetActive(EnmSSH type) {
            return _repository.GetActive(type);
        }

        public List<V_Elec> GetActive(string id, EnmSSH type) {
            return _repository.GetActive(id, type);
        }

        public List<V_Elec> GetActive(EnmSSH type, EnmFormula formula) {
            return _repository.GetActive(type, formula);
        }

        public List<V_Elec> GetHistory(DateTime start, DateTime end) {
            return _repository.GetHistory(start, end);
        }

        public List<V_Elec> GetHistory(EnmSSH type, DateTime start, DateTime end) {
            return _repository.GetHistory(type, start, end);
        }

        public List<V_Elec> GetHistory(EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _repository.GetHistory(type, formula, start, end);
        }

        public List<V_Elec> GetHistory(string id, EnmSSH type, DateTime start, DateTime end) {
            return _repository.GetHistory(id, type, start, end);
        }

        public List<V_Elec> GetHistory(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _repository.GetHistory(id, type, formula, start, end);
        }

        public List<V_Elec> GetEachDay(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _repository.GetEachDay(id, type, formula, start, end);
        }

        public double GetTotal(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end) {
            return _repository.GetTotal(id, type, formula, start, end);
        }

        public double GetCurrentMonthTotal(string id, EnmSSH type, EnmFormula formula) {
            var now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, 1);
            return _repository.GetTotal(id, type, formula, start, now);
        }

        public double GetLastMonthTotal(string id, EnmSSH type, EnmFormula formula) {
            var now = DateTime.Now;
            var current = new DateTime(now.Year, now.Month, 1);
            var start = current.AddMonths(-1);
            var end = current.AddSeconds(-1);
            return _repository.GetTotal(id, type, formula, start, end);
        }

        public double GetCurrentYearTotal(string id, EnmSSH type, EnmFormula formula) {
            var now = DateTime.Now;
            var start = new DateTime(now.Year, 1, 1);
            return _repository.GetTotal(id, type, formula, start, now);
        }

        public double GetLastYearTotal(string id, EnmSSH type, EnmFormula formula) {
            var now = DateTime.Now;
            var currnt = new DateTime(now.Year, 1, 1);
            var start = currnt.AddYears(-1);
            var end = currnt.AddSeconds(-1);
            return _repository.GetTotal(id, type, formula, start, end);
        }

        #endregion

    }
}