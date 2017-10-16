using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 刷卡记录表
    /// </summary>
    [Serializable]
    public partial class H_CardRecord : BaseEntity {
        /// <summary>
        /// 区域编码(第三级区域)
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// 机房编码
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

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
        /// 刷卡时间
        /// </summary>
        public DateTime PunchTime { get; set; }

        /// <summary>
        /// 刷卡状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 刷卡描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 刷卡方向
        /// </summary>
        public EnmDirection Direction { get; set; }
    }
}
