using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 角色相关信息表
    /// </summary>
    [Serializable]
    public partial class U_EntitiesInRole : BaseEntity {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public EnmSSH Type { get; set; }

        /// <summary>
        /// 角色菜单
        /// </summary>
        public List<int> Menus { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public List<EnmPermission> Permissions { get; set; }

        /// <summary>
        /// 角色区域/站点/机房/设备授权
        /// </summary>
        public List<string> Authorizations { get; set; }
    }
}