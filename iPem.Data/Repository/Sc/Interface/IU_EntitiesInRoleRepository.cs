using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 角色对象关系映射表(角色区域、角色权限、角色菜单)
    /// </summary>
    public partial interface IU_EntitiesInRoleRepository {
        /// <summary>
        /// 获得指定角色的对象关系映射信息
        /// </summary>
        U_EntitiesInRole GetEntitiesInRole(Guid id);

        /// <summary>
        /// 新增角色对象关系映射信息
        /// </summary>
        void Insert(U_EntitiesInRole entities);

        /// <summary>
        /// 删除指定角色的对象关系映射信息
        /// </summary>
        void Delete(Guid id);
    }
}
