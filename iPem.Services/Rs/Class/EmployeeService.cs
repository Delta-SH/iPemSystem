using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class EmployeeService : IEmployeeService {

        #region Fields

        private readonly IU_EmployeeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeService(
            IU_EmployeeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_Employee GetEmployeeById(string id) {
            return _repository.GetEmployeeById(id);
        }

        public U_Employee GetEmployeeByCode(string id) {
            return _repository.GetEmployeeByCode(id);
        }

        public List<U_Employee> GetEmployeesByDept(string id) {
            var key = GlobalCacheKeys.Rs_EmployeesRepository;
            if (!_cacheManager.IsSet(key)) {
                return this.GetEmployees().FindAll(c => c.DeptId == id);
            }

            if (_cacheManager.IsHashSet(key, id)) {
                return _cacheManager.GetFromHash<List<U_Employee>>(key, id);
            } else {
                var data = _repository.GetEmployeesByDept(id);
                _cacheManager.SetInHash(key, id, data);
                return data;
            }
        }

        public List<U_Employee> GetEmployees() {
            var key = GlobalCacheKeys.Rs_EmployeesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetAllFromHash<List<U_Employee>>(key).SelectMany(d => d).ToList();
            } else {
                var data = _repository.GetEmployees();
                var caches = data.GroupBy(d => d.DeptId).Select(d => new KeyValuePair<string, object>(d.Key, d.ToList()));
                _cacheManager.SetRangeInHash(key, caches);
                return data;
            }
        }

        public U_OutEmployee GetOutEmployeeById(string id) {
            return _repository.GetOutEmployeeById(id);
        }

        public List<U_OutEmployee> GetOutEmployeesByEmp(string id) {
            return _repository.GetOutEmployeesByEmp(id);
        }

        public List<U_OutEmployee> GetOutEmployeesByDept(string id) {
            return _repository.GetOutEmployeesByDept(id);
        }

        public List<U_OutEmployee> GetOutEmployees() {
            var key = GlobalCacheKeys.Rs_OutEmployeesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<List<U_OutEmployee>>(key);
            } else {
                var data = _repository.GetOutEmployees();
                _cacheManager.Set(key, data);
                return data;
            }
        }

        public IPagedList<U_Employee> GetPagedEmployees(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Employee>(this.GetEmployees(), pageIndex, pageSize);
        }

        public IPagedList<U_OutEmployee> GetPagedOutEmployees(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_OutEmployee>(this.GetOutEmployees(), pageIndex, pageSize);
        }

        #endregion

    }
}
