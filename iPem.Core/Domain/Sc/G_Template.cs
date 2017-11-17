using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 组态模板表
    /// </summary>
    [Serializable]
    public class G_Template {
        /// <summary>
        /// 组态模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组态模板配置
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 组态模板备注
        /// </summary>
        public string Remark { get; set; }
    }
}