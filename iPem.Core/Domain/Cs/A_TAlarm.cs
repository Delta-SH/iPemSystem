using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 告警流水表
    /// </summary>
    [Serializable]
    public partial class A_TAlarm {
        /// <summary>
        /// 告警流水唯一标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// FSU编码
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string SignalId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string SignalNumber { get; set; }

        /// <summary>
        /// 告警流水号
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 网管告警编码
        /// </summary>
        public string NMAlarmId { get; set; }

        /// <summary>
        /// 告警时间
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// 告警级别
        /// </summary>
        public EnmAlarm AlarmLevel { get; set; }

        /// <summary>
        /// 告警触发值
        /// </summary>
        public double AlarmValue { get; set; }

        /// <summary>
        /// 告警标记(告警开始、告警结束)
        /// </summary>
        public EnmFlag AlarmFlag { get; set; }

        /// <summary>
        /// 告警描述
        /// </summary>
        public string AlarmDesc { get; set; }

        /// <summary>
        /// 告警预留字段
        /// </summary>
        public string AlarmRemark { get; set; }
    }
}
