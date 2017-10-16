using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 卡片信息表
    /// </summary>
    [Serializable]
    public partial class M_Card : BaseEntity {
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
        /// 卡片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡片用户
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 卡片密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public EnmCardType Type { get; set; }

        /// <summary>
        /// 卡片状态
        /// </summary>
        public EnmCardStatus Status { get; set; }

        /// <summary>
        /// 卡片状态变更时间
        /// </summary>
        public DateTime StatusTime { get; set; }

        /// <summary>
        /// 卡片状态变更原因
        /// </summary>
        public string StatusReason { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
