using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// Department service
    /// </summary>
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
            List<Department> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DepartmentRepository)) {
                result = _cacheManager.Get<List<Department>>(GlobalCacheKeys.Rs_DepartmentRepository);
            } else {
                result = _departmentRepository.GetEntities();
                _cacheManager.Set<List<Department>>(GlobalCacheKeys.Rs_DepartmentRepository, result);
            }

            return new PagedList<Department>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
