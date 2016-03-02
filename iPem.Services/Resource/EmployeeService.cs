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
            IList<Employee> employees = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_EmployeesRepository)) {
                employees = _cacheManager.Get<IList<Employee>>(GlobalCacheKeys.Rs_EmployeesRepository);
            } else {
                employees = _employeeRepository.GetEntities();
                _cacheManager.Set<IList<Employee>>(GlobalCacheKeys.Rs_EmployeesRepository, employees);
            }

            var result = new PagedList<Employee>(employees, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Employee> GetDeptEmployees(string id, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Rs_EmployeesInDepartmentPattern, id);

            IList<Employee> employees = null;
            if(_cacheManager.IsSet(key)) {
                employees = _cacheManager.Get<IList<Employee>>(key);
            } else {
                employees = _employeeRepository.GetEntitiesByDept(id);
                _cacheManager.Set<IList<Employee>>(key, employees);
            }

            var result = new PagedList<Employee>(employees, pageIndex, pageSize);
            return result;
        }

        #endregion
    }
}
