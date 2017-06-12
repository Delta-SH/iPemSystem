using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 系统菜单表
    /// </summary>
    [Serializable]
    public partial class U_Menu : BaseEntity {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单图标路径(相对路径)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 菜单解释说明
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 菜单序号(排序时使用)
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 上级菜单，0表示顶级菜单
        /// </summary>
        public int LastId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
