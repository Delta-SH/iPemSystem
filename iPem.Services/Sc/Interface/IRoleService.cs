using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 角色信息API
    /// </summary>
    public partial interface IRoleService {
        /// <summary>
        /// 获得指定角色编号的角色
        /// </summary>
        U_Role GetRoleById(Guid id);

        /// <summary>
        /// 获得指定角色名称的角色
        /// </summary>
        U_Role GetRoleByName(string name);

        /// <summary>
        /// 获得指定用户编号的角色
        /// </summary>
        U_Role GetRoleByUid(Guid id);

        /// <summary>
        /// 获得所有的角色
        /// </summary>
        List<U_Role> GetRoles();

        /// <summary>
        /// 获得指定角色下属的角色
        /// </summary>
        List<U_Role> GetRolesByRole(Guid id);

        /// <summary>
        /// 获得指定角色名称的角色
        /// </summary>
        List<U_Role> GetRoleByNames(string[] names);

        /// <summary>
        /// 获得所有的角色（分页）
        /// </summary>
        IPagedList<U_Role> GetPagedRoles(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定角色名称的角色（分页）
        /// </summary>
        IPagedList<U_Role> GetPagedRoleByNames(string[] names, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增角色
        /// </summary>
        void Add(params U_Role[] roles);

        /// <summary>
        /// 更新角色
        /// </summary>
        void Update(params U_Role[] roles);

        /// <summary>
        /// 删除角色
        /// </summary>
        void Remove(params U_Role[] roles);

        /// <summary>
        /// 验证角色
        /// </summary>
        EnmLoginResults Validate(Guid uid);
    }
}
