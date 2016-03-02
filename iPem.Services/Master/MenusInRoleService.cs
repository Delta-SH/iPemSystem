using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Services.Master {
    /// <summary>
    /// Menu service
    /// </summary>
    public partial class MenusInRoleService : IMenusInRoleService {

        #region Fields

        private readonly IMenusInRoleRepository _menusInRoleRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MenusInRoleService(
            IMenusInRoleRepository menusInRoleRepository,
            ICacheManager cacheManager) {
            this._menusInRoleRepository = menusInRoleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public MenusInRole GetMenusInRole(Guid id) {
            return _menusInRoleRepository.GetEntity(id);
        }

        public void AddMenusInRole(MenusInRole menus) {
            if(menus == null)
                throw new ArgumentException("menus");

            var key = string.Format(GlobalCacheKeys.Cs_MenusInRolePattern, menus.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menusInRoleRepository.Insert(menus);
        }

        public void DeleteMenusInRole(Guid id) {
            var key = string.Format(GlobalCacheKeys.Cs_MenusInRolePattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menusInRoleRepository.Delete(id);
        }

        #endregion

    }
}
