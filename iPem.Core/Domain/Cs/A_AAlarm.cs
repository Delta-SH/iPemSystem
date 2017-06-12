using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    /// <summary>
    /// 活动告警表
    /// </summary>
    [Serializable]
    public partial class A_AAlarm : BaseEntity {
        /// <summary>
        /// 告警唯一标识
        /// </summary>
        public string Id { get; set; }

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
        /// Fsu编码
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
        /// 告警描述
        /// </summary>
        public string AlarmDesc { get; set; }

        /// <summary>
        /// 告警预留字段
        /// </summary>
        public string AlarmRemark { get; set; }

        /// <summary>
        /// 告警确认状态
        /// </summary>
        public EnmConfirm Confirmed { get; set; }

        /// <summary>
        /// 告警确认用户(未确认时，此值为NULL)
        /// </summary>
        /// <remarks>
        /// 此处为用户对应的员工姓名
        /// </remarks>
        public string Confirmer { get; set; }

        /// <summary>
        /// 告警确认时间(未确认时，此值为NULL)
        /// </summary>
        public DateTime? ConfirmedTime { get; set; }

        /// <summary>
        /// 工程预约编号(非工程预约告警时，此值为NULL)
        /// </summary>
        public string ReservationId { get; set; }

        /// <summary>
        /// 主告警编号(当本身为主告警时，此值为NULL)
        /// </summary>
        public string PrimaryId { get; set; }

        /// <summary>
        /// 关联告警编号(当本身为主关联告警时，此值为NULL)
        /// </summary>
        public string RelatedId { get; set; }

        /// <summary>
        /// 告警过滤编号(当本身为主告警时，此值为NULL)
        /// </summary>
        public string FilterId { get; set; }

        /// <summary>
        /// 翻转告警编号(当本身为主翻转告警时，此值为NULL)
        /// </summary>
        public string ReversalId { get; set; }

        /// <summary>
        /// 翻转告警次数(当本身为主翻转告警时，此值为NULL)
        /// </summary>
        public int ReversalCount { get; set; }

        /// <summary>
        /// 告警入库时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
