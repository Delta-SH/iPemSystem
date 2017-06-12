using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class EmployeeService : IEmployeeService {

        #region Fields

        private readonly IU_EmployeeRepository _employeeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeService(
            IU_EmployeeRepository employeeRepository,
            ICacheManager cacheManager) {
            this._employeeRepository = employeeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_Employee GetEmpolyee(string id) {
            return _employeeRepository.GetEntity(id);
        }

        public U_Employee GetEmpolyeeByCode(string code) {
            return _employeeRepository.GetEntityByCode(code);
        }

        public IPagedList<U_Employee> GetEmployeesByDept(string dept, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Employee>(this.GetEmployeesByDeptAsList(dept), pageIndex, pageSize);
        }

        public List<U_Employee> GetEmployeesByDeptAsList(string dept) {
            return _employeeRepository.GetEntitiesByDept(dept);
        }

        public IPagedList<U_Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Employee>(this.GetAllEmployeesAsList(), pageIndex, pageSize);
        }

        public List<U_Employee> GetAllEmployeesAsList() {
            return _employeeRepository.GetEntities();
        }

        #endregion

    }
}
