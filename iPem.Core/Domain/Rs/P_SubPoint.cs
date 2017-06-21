using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 标准信号参数表
    /// </summary>
    [Serializable]
    public partial class P_SubPoint : BaseEntity {
        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 站点类型
        /// </summary>
        public C_StationType StationType { get; set; }

        /// <summary>
        /// 告警级别
        /// </summary>
        public EnmAlarm AlarmLevel { get; set; }

        /// <summary>
        /// 告警门限
        /// </summary>
        public double AlarmLimit { get; set; }

        /// <summary>
        /// 告警回差
        /// </summary>
        public double AlarmReturnDiff { get; set; }

        /// <summary>
        /// 告警延时（秒）
        /// </summary>
        public int AlarmDelay { get; set; }

        /// <summary>
        /// 告警恢复延时（秒）
        /// </summary>
        public int AlarmRecoveryDelay { get; set; }

        /// <summary>
        /// 触发模式
        /// </summary>
        /// <remarks>
        /// >、<、=、！= (针对遥信)
        /// </remarks>
        public int TriggerTypeId { get; set; }

        /// <summary>
        /// 存储周期（秒）
        /// </summary>
        public int SavedPeriod { get; set; }

        /// <summary>
        /// 绝对阀值
        /// </summary>
        public double AbsoluteThreshold { get; set; }

        /// <summary>
        /// 百分比阀值
        /// </summary>
        public double PerThreshold { get; set; }

        /// <summary>
        /// 统计周期(分钟，5的倍数)
        /// </summary>
        public int StaticPeriod { get; set; }
    }
}
