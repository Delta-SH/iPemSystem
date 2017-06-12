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

        private readonly IRoleRepository _roleRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoleService(
            IRoleRepository roleRepository,
            ICacheManager cacheManager) {
            this._roleRepository = roleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public virtual U_Role GetRole(Guid id) {
            return _roleRepository.GetEntity(id);
        }

        public virtual U_Role GetRole(string name) {
            return _roleRepository.GetEntity(name);
        }

        public virtual U_Role GetRoleByUid(Guid uid) {
            return _roleRepository.GetEntityByUid(uid);
        }

        public virtual IPagedList<U_Role> GetAllRoles(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Role>(this.GetAllRolesAsList(), pageIndex, pageSize);
        }

        public virtual List<U_Role> GetAllRolesAsList() {
            return _roleRepository.GetEntities().FindAll(r=>r.Id != U_Role.SuperId);
        }

        public virtual IPagedList<U_Role> GetRoles(Guid id, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Role>(this.GetRolesAsList(id), pageIndex, pageSize);
        }

        public virtual List<U_Role> GetRolesAsList(Guid id) {
            var roles = _roleRepository.GetEntities();
            var result = id.Equals(U_Role.SuperId) ? roles : roles.FindAll(r => r.Id.Equals(id));
            return result;
        }

        public virtual IPagedList<U_Role> GetRoles(string[] names, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Role>(this.GetRolesAsList(names), pageIndex, pageSize);
        }

        public virtual List<U_Role> GetRolesAsList(string[] names) {
            var roles = _roleRepository.GetEntities();
            var result = roles.FindAll(r => r.Id != U_Role.SuperId && CommonHelper.ConditionContain(r.Name, names));
            return result;
        }

        public virtual void Add(U_Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Insert(role);
        }

        public virtual void Update(U_Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Update(role);
        }

        public virtual void Remove(U_Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Delete(role);
        }

        #endregion

        #region Validate

        public virtual EnmLoginResults Validate(Guid uid) {
            var role = this.GetRoleByUid(uid);
            if(role == null)
                return EnmLoginResults.RoleNotExist;
            if(!role.Enabled)
                return EnmLoginResults.RoleNotEnabled;

            return EnmLoginResults.Successful;
        }

        #endregion

    }
}
