using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 参数自检表
    /// </summary>
    [Serializable]
    public partial class V_ParamDiff : BaseEntity {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编号
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 告警门限值（格式：新值&旧值）
        /// </summary>
        public string Threshold { get; set; }

        /// <summary>
        /// 告警等级（格式：新值&旧值）
        /// </summary>
        public string AlarmLevel { get; set; }

        /// <summary>
        /// 网管告警编号（格式：新值&旧值）
        /// </summary>
        public string NMAlarmID { get; set; }

        /// <summary>
        /// 绝对阀值（格式：新值&旧值）
        /// </summary>
        public string AbsoluteVal { get; set; }

        /// <summary>
        /// 百分比阀值（格式：新值&旧值）
        /// </summary>
        public string RelativeVal { get; set; }

        /// <summary>
        /// 存储时间间隔（分钟，格式：新值&旧值）
        /// </summary>
        public string StorageInterval { get; set; }

        /// <summary>
        /// 存储参考时间（格式：新值&旧值）
        /// </summary>
        public string StorageRefTime { get; set; }
    }
}
