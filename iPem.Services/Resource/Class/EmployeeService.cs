using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// Employee service
    /// </summary>
    public partial class EmployeeService : IEmployeeService {

        #region Fields

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeService(
            IEmployeeRepository employeeRepository,
            ICacheManager cacheManager) {
            this._employeeRepository = employeeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Employee GetEmpolyee(string id) {
            return _employeeRepository.GetEntity(id);
        }

        public Employee GetEmpolyeeByNo(string id) {
            return _employeeRepository.GetEntityByNo(id);
        }

        public IPagedList<Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Employee> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_EmployeesRepository)) {
                result = _cacheManager.Get<List<Employee>>(GlobalCacheKeys.Rs_EmployeesRepository);
            } else {
                result = _employeeRepository.GetEntities();
                _cacheManager.Set<List<Employee>>(GlobalCacheKeys.Rs_EmployeesRepository, result);
            }

            return new PagedList<Employee>(result, pageIndex, pageSize);
        }

        public IPagedList<Employee> GetEmployeesInDepartment(string dept, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Rs_EmployeesInDepartmentPattern, dept);

            List<Employee> result = null;
            if(_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<Employee>>(key);
            } else {
                result = _employeeRepository.GetEntitiesByDept(dept);
                _cacheManager.Set<List<Employee>>(key, result);
            }

            return new PagedList<Employee>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
