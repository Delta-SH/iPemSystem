using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 资管接口设备表
    /// </summary>
    [Serializable]
    public partial class H_IDevice : BaseEntity {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
