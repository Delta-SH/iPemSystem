using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 资管接口站点表
    /// </summary>
    [Serializable]
    public partial class H_IStation : BaseEntity {
        /// <summary>
        /// 站点编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点类型编号
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 站点类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string AreaId { get; set; }
    }
}
