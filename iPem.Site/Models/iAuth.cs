using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models
{
    public class iAuth
    {
        private readonly Lazy<List<U_Menu>> _menus;

        public iAuth()
        {
            //延迟加载属性
            this._menus = new Lazy<List<U_Menu>>(() =>
            {
                return EngineContext.Current.Resolve<IMenuService>().GetMenus().FindAll(m => this.Menus.Contains(m.Id));
            });
        }

        /// <summary>
        /// 角色菜单HASH
        /// </summary>
        public HashSet<int> Menus { get; set; }

        /// <summary>
        /// 角色菜单
        /// </summary>
        public List<U_Menu> MenuItems
        {
            get { return _menus.Value; }
        }

        /// <summary>
        /// 角色权限HASH
        /// </summary>
        public HashSet<EnmPermission> Permissions { get; set; }

        /// <summary>
        /// 角色区域/站点/机房/设备权限HASH
        /// </summary>
        public HashSet<string> Authorizations { get; set; }
    }
}