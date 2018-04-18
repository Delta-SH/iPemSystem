using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Fsu信息表
    /// </summary>
    [Serializable]
    public partial class D_Fsu : BaseEntity {
        /// <summary>
        /// Fsu编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Fsu名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属区域编号
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 所属区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 所属站点编号
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// 所属站点名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 站点类型(根据站点类型确定该设备下每个信号所对应的信号参数)
        /// </summary>
        public string StaTypeId { get; set; }

        /// <summary>
        /// 所属机房编号
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 所属机房名称
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 所属机房类型
        /// </summary>
        public string RoomTypeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 所属厂家编号
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// 所属厂家名称
        /// </summary>
        public string VendorName { get; set; }
    }
}
