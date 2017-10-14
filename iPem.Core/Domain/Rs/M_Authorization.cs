using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 外协人员信息表
    /// </summary>
    [Serializable]
    public partial class M_Authorization : BaseEntity {
        /// <summary>
        /// 驱动编号
        /// </summary>
        public string DriverId { get; set; }

        /// <summary>
        /// 门禁卡号（十进制，10位）
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 门禁卡号（十六进制）
        /// </summary>
        public string CardHex { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 门禁密码
        /// </summary>
        public string Password { get; set; }
    }
}
