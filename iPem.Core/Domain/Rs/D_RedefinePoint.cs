using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 信号重定义信息
    /// </summary>
    [Serializable]
    public partial class D_RedefinePoint {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 告警等级
        /// </summary>
        public int AlarmLevel { get; set; }

        /// <summary>
        /// 告警门限
        /// </summary>
        public double AlarmLimit { get; set; }

        /// <summary>
        /// 告警回差
        /// </summary>
        public double AlarmReturnDiff { get; set; }

        /// <summary>
        /// 告警触发延迟
        /// </summary>
        public int AlarmDelay { get; set; }

        /// <summary>
        /// 告警恢复延迟
        /// </summary>
        public int AlarmRecoveryDelay { get; set; }

        /// <summary>
        /// 触发类型
        /// </summary>
        public int TriggerTypeID { get; set; }

        /// <summary>
        /// 存储周期
        /// </summary>
        public int SavedPeriod { get; set; }

        /// <summary>
        /// 绝对阈值
        /// </summary>
        public double AbsoluteThreshold { get; set; }

        /// <summary>
        /// 百分比阈值
        /// </summary>
        public double PerThreshold { get; set; }

        /// <summary>
        /// 统计周期
        /// </summary>
        public int StaticPeriod { get; set; }

        /// <summary>
        /// 主次告警
        /// </summary>
        public string InferiorAlarmStr { get; set; }

        /// <summary>
        /// 关联告警
        /// </summary>
        public string ConnAlarmStr { get; set; }

        /// <summary>
        /// 告警过滤
        /// </summary>
        public string AlarmFilteringStr { get; set; }

        /// <summary>
        /// 告警翻转
        /// </summary>
        public string AlarmReversalStr { get; set; }

        /// <summary>
        /// 扩展设置
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
