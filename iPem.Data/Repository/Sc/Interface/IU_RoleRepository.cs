using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 角色信息表
    /// </summary>
    public partial interface IU_RoleRepository {
        /// <summary>
        /// 获得指定角色编号的角色
        /// </summary>
        U_Role GetRoleById(string id);

        /// <summary>
        /// 获得指定角色名称的角色
        /// </summary>
        U_Role GetRoleByName(string name);

        /// <summary>
        /// 获得指定用户编号的角色
        /// </summary>
        U_Role GetRoleByUid(string id);

        /// <summary>
        /// 获得所有的角色
        /// </summary>
        List<U_Role> GetRoles();

        /// <summary>
        /// 新增角色
        /// </summary>
        void Insert(IList<U_Role> entities);

        /// <summary>
        /// 更新角色
        /// </summary>
        void Update(IList<U_Role> entities);

        /// <summary>
        /// 删除角色
        /// </summary>
        void Delete(IList<U_Role> entities);
    }
}