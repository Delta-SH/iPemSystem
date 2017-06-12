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
        public Guid RoleId { get; set; }

        /// <summary>
        /// 角色菜单
        /// </summary>
        public List<U_Menu> Menus { get; set; }

        /// <summary>
        /// 角色操作权限
        /// </summary>
        public List<EnmOperation> Operates { get; set; }
    }
}
