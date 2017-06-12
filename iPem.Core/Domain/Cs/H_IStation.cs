using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 资管接口站点表
    /// </summary>
    [Serializable]
    public partial class H_IStation : BaseEntity {
        /// <summary>
        /// 站点编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
