using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Master {
    /// <summary>
    /// Role service
    /// </summary>
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

        /// <summary>
        /// Gets a role by identifier
        /// </summary>
        /// <param name="id">role identifier</param>
        /// <returns>Role</returns>
        public virtual Role GetRole(Guid id) {
            return _roleRepository.GetEntity(id);
        }

        /// <summary>
        /// Gets a role by name
        /// </summary>
        /// <param name="name">role name</param>
        /// <returns>Role</returns>
        public virtual Role GetRole(string name) {
            return _roleRepository.GetEntity(name);
        }

        /// <summary>
        /// Gets a role by user id
        /// </summary>
        /// <param name="uid">user id</param>
        /// <returns>Role</returns>
        public virtual Role GetRoleByUid(Guid uid) {
            return _roleRepository.GetEntityByUid(uid);
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Role collection</returns>
        public virtual IPagedList<Role> GetAllRoles(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Role> roles = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RolesRepository)) {
                roles = _cacheManager.Get<List<Role>>(GlobalCacheKeys.Cs_RolesRepository);
            } else {
                roles = _roleRepository.GetEntities();
                _cacheManager.Set<List<Role>>(GlobalCacheKeys.Cs_RolesRepository, roles);
            }

            return new PagedList<Role>(roles, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <param name="id">Current role id</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Role collection</returns>
        public virtual IPagedList<Role> GetAllRoles(Guid id, int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Role> roles = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RolesRepository)) {
                roles = _cacheManager.Get<List<Role>>(GlobalCacheKeys.Cs_RolesRepository);
            } else {
                roles = _roleRepository.GetEntities();
                _cacheManager.Set<List<Role>>(GlobalCacheKeys.Cs_RolesRepository, roles);
            }

            var result = id.Equals(Role.SuperId) ? roles : roles.FindAll(r => r.Id.Equals(id));
            return new PagedList<Role>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <param name="names">Role names</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Role collection</returns>
        public virtual IPagedList<Role> GetAllRoles(string[] names, int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Role> roles = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RolesRepository)) {
                roles = _cacheManager.Get<List<Role>>(GlobalCacheKeys.Cs_RolesRepository);
            } else {
                roles = _roleRepository.GetEntities();
                _cacheManager.Set<List<Role>>(GlobalCacheKeys.Cs_RolesRepository, roles);
            }

            var result = roles.FindAll(r => CommonHelper.ConditionContain(r.Name, names));
            return new PagedList<Role>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Inserts a roles
        /// </summary>
        /// <param name="role">Role</param>
        public virtual void InsertRole(Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RolesRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_RolesRepository);

            _roleRepository.Insert(role);
        }

        /// <summary>
        /// Updates the role
        /// </summary>
        /// <param name="role">Role</param>
        public virtual void UpdateRole(Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RolesRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_RolesRepository);

            _roleRepository.Update(role);
        }

        /// <summary>
        /// Marks Role as deleted 
        /// </summary>
        /// <param name="role">Role</param>
        public virtual void DeleteRole(Role role) {
            if (role == null)
                throw new ArgumentNullException("role");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RolesRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_RolesRepository);

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
