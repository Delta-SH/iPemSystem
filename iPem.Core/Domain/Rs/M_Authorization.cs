using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 门禁授权表
    /// </summary>
    [Serializable]
    public partial class M_Authorization : BaseEntity {
        /// <summary>
        /// 十六进制卡号（10位）
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 十进制卡号（10位）
        /// </summary>
        public string DecimalCard {
            get {
                if (string.IsNullOrWhiteSpace(this.CardId))
                    return string.Empty;

                return int.Parse(this.CardId, System.Globalization.NumberStyles.HexNumber).ToString("D10");
            }
        }

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
