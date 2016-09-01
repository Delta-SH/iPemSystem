using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class DepartmentService : IDepartmentService {

        #region Fields

        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DepartmentService(
            IDepartmentRepository departmentRepository,
            ICacheManager cacheManager) {
            this._departmentRepository = departmentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Department GetDepartment(string id) {
            return _departmentRepository.GetEntity(id);
        }

        public Department GetDepartmentByCode(string code) {
            return _departmentRepository.GetEntityByCode(code);
        }

        public IPagedList<Department> GetAllDepartments(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Department>(this.GetAllDepartmentsAsList(), pageIndex, pageSize);
        }

        public List<Department> GetAllDepartmentsAsList() {
            return _departmentRepository.GetEntities();
        }

        #endregion

    }
}
