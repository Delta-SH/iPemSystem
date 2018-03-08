using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class DepartmentService : IDepartmentService {

        #region Fields

        private readonly IC_DepartmentRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DepartmentService(
            IC_DepartmentRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Department GetDepartmentById(string id) {
            return _repository.GetDepartmentById(id);
        }

        public C_Department GetDepartmentByCode(string code) {
            return _repository.GetDepartmentByCode(code);
        }

        public List<C_Department> GetDepartments() {
            var key = GlobalCacheKeys.Rs_DepartmentsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_Department>(key).ToList();
            } else {
                var data = _repository.GetDepartments();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<C_Department> GetPagedDepartments(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Department>(this.GetDepartments(), pageIndex, pageSize);
        }

        #endregion

    }
}
