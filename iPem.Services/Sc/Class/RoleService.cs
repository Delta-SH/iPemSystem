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

        public virtual Role GetRole(Guid id) {
            return _roleRepository.GetEntity(id);
        }

        public virtual Role GetRole(string name) {
            return _roleRepository.GetEntity(name);
        }

        public virtual Role GetRoleByUid(Guid uid) {
            return _roleRepository.GetEntityByUid(uid);
        }

        public virtual IPagedList<Role> GetAllRoles(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Role>(this.GetAllRolesAsList(), pageIndex, pageSize);
        }

        public virtual List<Role> GetAllRolesAsList() {
            return _roleRepository.GetEntities();
        }

        public virtual IPagedList<Role> GetRoles(Guid id, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Role>(this.GetRolesAsList(id), pageIndex, pageSize);
        }

        public virtual List<Role> GetRolesAsList(Guid id) {
            var roles = _roleRepository.GetEntities();
            var result = id.Equals(Role.SuperId) ? roles : roles.FindAll(r => r.Id.Equals(id));
            return result;
        }

        public virtual IPagedList<Role> GetRoles(string[] names, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Role>(this.GetRolesAsList(names), pageIndex, pageSize);
        }

        public virtual List<Role> GetRolesAsList(string[] names) {
            var roles = _roleRepository.GetEntities();
            var result = roles.FindAll(r => CommonHelper.ConditionContain(r.Name, names));
            return result;
        }

        public virtual void Add(Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Insert(role);
        }

        public virtual void Update(Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Update(role);
        }

        public virtual void Remove(Role role) {
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
