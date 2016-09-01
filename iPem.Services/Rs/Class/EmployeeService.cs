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

        public Employee GetEmpolyeeByCode(string code) {
            return _employeeRepository.GetEntityByCode(code);
        }

        public IPagedList<Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Employee>(this.GetAllEmployeesAsList(), pageIndex, pageSize);
        }

        public List<Employee> GetAllEmployeesAsList() {
            return _employeeRepository.GetEntities();
        }

        public IPagedList<Employee> GetEmployees(string dept, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Employee>(this.GetEmployeesAsList(dept), pageIndex, pageSize);
        }

        public List<Employee> GetEmployeesAsList(string dept) {
            return _employeeRepository.GetEntities(dept);
        }

        #endregion

    }
}
