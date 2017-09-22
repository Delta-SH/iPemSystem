using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 站点信息表
    /// </summary>
    [Serializable]
    public partial class S_Station : BaseEntity {
        /// <summary>
        /// 站点编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点类型
        /// </summary>
        public C_StationType Type { get; set; }

        /// <summary>
        /// 所属厂家
        /// </summary>
        public C_SCVendor Vendor { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 海拔标高
        /// </summary>
        public string Altitude { get; set; }

        /// <summary>
        /// 市电引入方式
        /// </summary>
        public int CityElecLoadTypeId { get; set; }

        /// <summary>
        /// 市电容量
        /// </summary>
        public string CityElecCap { get; set; }

        /// <summary>
        /// 市电路数
        /// </summary>
        public int CityElectNumber { get; set; }

        /// <summary>
        /// 市电引入
        /// </summary>
        public string CityElecLoad { get; set; }

        /// <summary>
        /// 维护负责人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 线径
        /// </summary>
        public string LineRadiusSize { get; set; }

        /// <summary>
        /// 线缆长度
        /// </summary>
        public string LineLength { get; set; }

        /// <summary>
        /// 供电性质
        /// </summary>
        /// <remarks>
        /// 枚举值：转供、直供
        /// </remarks>
        public int SuppPowerTypeId { get; set; }

        /// <summary>
        /// 转供信息
        /// </summary>
        public string TranInfo { get; set; }

        /// <summary>
        /// 供电合同号
        /// </summary>
        public string TranContNo { get; set; }

        /// <summary>
        /// 变电站电话
        /// </summary>
        public string TranPhone { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
