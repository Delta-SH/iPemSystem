using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 设备信息表
    /// </summary>
    [Serializable]
    public partial class D_Device : BaseEntity {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public C_DeviceType Type { get; set; }

        /// <summary>
        /// 设备子类型
        /// </summary>
        public C_SubDeviceType SubType { get; set; }

        /// <summary>
        /// 逻辑子类
        /// </summary>
        public C_SubLogicType SubLogicType { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }

        /// <summary>
        /// 系统编码
        /// </summary>
        public string SysCode { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 所属厂家
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Productor { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 维护厂家
        /// </summary>
        public string SubCompany { get; set; }

        /// <summary>
        /// 开始使用时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 预计报废时间
        /// </summary>
        public DateTime ScrapTime { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        /// <remarks>
        /// 枚举值：在用-良好,在用-故障,闲置-良好,闲置-故障,返修,报废
        /// </remarks>
        public int StatusId { get; set; }

        /// <summary>
        /// 设备版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 维护负责人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 所属站点
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// 所属站点
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 站点类型(根据站点类型确定该设备下每个信号所对应的信号参数)
        /// </summary>
        public string StaTypeId { get; set; }

        /// <summary>
        /// 所属机房
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 所属机房
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 所属FSU
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// 设备模版
        /// </summary>
        public string ProtocolId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
