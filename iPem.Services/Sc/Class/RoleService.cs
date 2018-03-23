using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Sc {
    public partial class RoleService : IRoleService {

        #region Fields

        private readonly IU_RoleRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoleService(
            IU_RoleRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_Role GetRoleById(string id) {
            return _repository.GetRoleById(id);
        }

        public U_Role GetRoleByName(string name) {
            return _repository.GetRoleByName(name);
        }

        public U_Role GetRoleByUid(string uid) {
            return _repository.GetRoleByUid(uid);
        }

        public List<U_Role> GetRoles() {
            return _repository.GetRoles().FindAll(r => !r.Id.Equals(U_Role.SuperId));
        }

        public List<U_Role> GetRolesByRole(string id) {
            var roles = _repository.GetRoles();
            return id.Equals(U_Role.SuperId) ? roles : roles.FindAll(r => r.Id.Equals(id));
        }

        public List<U_Role> GetRoleByNames(string[] names) {
            var roles = _repository.GetRoles();
            return roles.FindAll(r => !r.Id.Equals(U_Role.SuperId) && CommonHelper.ConditionContain(r.Name, names));
        }

        public IPagedList<U_Role> GetPagedRoles(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Role>(this.GetRoles(), pageIndex, pageSize);
        }

        public IPagedList<U_Role> GetPagedRoleByNames(string[] names, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Role>(this.GetRoleByNames(names), pageIndex, pageSize);
        }

        public void Add(params U_Role[] roles) {
            if (roles == null || roles.Length == 0)
                throw new ArgumentNullException("roles");

            _repository.Insert(roles);
        }

        public void Update(params U_Role[] roles) {
            if (roles == null || roles.Length == 0)
                throw new ArgumentNullException("roles");

            _repository.Update(roles);
        }

        public void Remove(params U_Role[] roles) {
            if (roles == null || roles.Length == 0)
                throw new ArgumentNullException("roles");

            _repository.Delete(roles);
        }

        #endregion

        #region Validate

        public EnmLoginResults Validate(string id) {
            var role = this.GetRoleById(id);
            if (role == null)
                return EnmLoginResults.RoleNotExist;
            if (!role.Enabled)
                return EnmLoginResults.RoleNotEnabled;

            return EnmLoginResults.Successful;
        }

        #endregion

    }
}