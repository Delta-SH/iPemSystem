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

        public U_Employee GetEmployeeByCode(string code) {
            return _repository.GetEmployeeByCode(code);
        }

        public List<U_Employee> GetEmployeesByDept(string dept) {
            return _repository.GetEmployeesByDept(dept);
        }

        public List<U_Employee> GetEmployees() {
            return _repository.GetEmployees();
        }

        public IPagedList<U_Employee> GetPagedEmployees(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Employee>(this.GetEmployees(), pageIndex, pageSize);
        }

        #endregion

    }
}
