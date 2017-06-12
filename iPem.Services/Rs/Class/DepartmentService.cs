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

        private readonly IC_DepartmentRepository _departmentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DepartmentService(
            IC_DepartmentRepository departmentRepository,
            ICacheManager cacheManager) {
            this._departmentRepository = departmentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Department GetDepartment(string id) {
            return _departmentRepository.GetEntity(id);
        }

        public C_Department GetDepartmentByCode(string code) {
            return _departmentRepository.GetEntityByCode(code);
        }

        public IPagedList<C_Department> GetAllDepartments(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Department>(this.GetAllDepartmentsAsList(), pageIndex, pageSize);
        }

        public List<C_Department> GetAllDepartmentsAsList() {
            return _departmentRepository.GetEntities();
        }

        #endregion

    }
}
