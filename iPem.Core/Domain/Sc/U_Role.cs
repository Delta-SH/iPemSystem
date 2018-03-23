using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 角色信息表
    /// </summary>
    [Serializable]
    public partial class U_Role : BaseEntity {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 超级管理员编号
        /// </summary>
        public static string SuperId {
            get { return "a0000000-6000-2000-1000-f00000000000"; }
        }

        public static string RsSuperId {
            get { return "0"; }
        }

    }
}
