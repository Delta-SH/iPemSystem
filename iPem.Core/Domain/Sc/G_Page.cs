using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 组态配置表
    /// </summary>
    [Serializable]
    public class G_Page {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 组态名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否为组态页面集的首页
        /// </summary>
        public bool IsHome { get; set; }

        /// <summary>
        /// 组态配置
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 对象编号
        /// </summary>
        public string ObjId { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public int ObjType { get; set; }

        /// <summary>
        /// 组态备注
        /// </summary>
        public string Remark { get; set; }
    }
}
