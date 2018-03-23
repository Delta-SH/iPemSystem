using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 角色对象关系映射API(角色区域、角色权限、角色菜单)
    /// </summary>
    public partial interface IEntitiesInRoleService {
        /// <summary>
        /// 获得指定角色的对象关系映射信息
        /// </summary>
        U_EntitiesInRole GetEntitiesInRole(string id);

        /// <summary>
        /// 新增角色对象关系映射信息
        /// </summary>
        void Add(U_EntitiesInRole entities);

        /// <summary>
        /// 删除指定角色的对象关系映射信息
        /// </summary>
        void Remove(string id);
    }
}
