using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 标准信号表
    /// </summary>
    [Serializable]
    public partial class P_Point : BaseEntity {
        /// <summary>
        /// 信号编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 信号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public EnmPoint Type { get; set; }

        /// <summary>
        /// 单位/描述
        /// </summary>
        public string UnitState { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 标准告警编号
        /// </summary>
        public string AlarmId { get; set; }

        /// <summary>
        /// 网管告警编号
        /// </summary>
        public string NMAlarmId { get; set; }

        /// <summary>
        /// 站点类型
        /// </summary>
        public C_StationType StationType { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public C_DeviceType DeviceType { get; set; }

        /// <summary>
        /// 逻辑分类
        /// </summary>
        public C_LogicType LogicType { get; set; }

        /// <summary>
        /// 告警时描述
        /// </summary>
        public string AlarmComment { get; set; }

        /// <summary>
        /// 正常时描述
        /// </summary>
        public string NormalComment { get; set; }

        /// <summary>
        /// 告警对设备的影响
        /// </summary>
        public string DeviceEffect { get; set; }

        /// <summary>
        /// 告警对业务的影响
        /// </summary>
        public string BusiEffect { get; set; }

        /// <summary>
        /// 告警级别
        /// </summary>
        public EnmAlarm AlarmLevel { get; set; }

        /// <summary>
        /// 触发模式
        /// </summary>
        /// <remarks>
        /// >、<、=、！= (针对遥信)
        /// </remarks>
        public int TriggerTypeId { get; set; }

        /// <summary>
        /// 信号说明
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 信号解释
        /// </summary>
        public string Interpret { get; set; }

        /// <summary>
        /// 告警门限
        /// </summary>
        public double AlarmLimit { get; set; }

        /// <summary>
        /// 告警回差
        /// </summary>
        public double AlarmReturnDiff { get; set; }

        /// <summary>
        /// 告警恢复延时（秒）
        /// </summary>
        public int AlarmRecoveryDelay { get; set; }

        /// <summary>
        /// 告警延时（秒）
        /// </summary>
        public int AlarmDelay { get; set; }

        /// <summary>
        /// 存储周期（秒）
        /// </summary>
        public int SavedPeriod { get; set; }

        /// <summary>
        /// 统计周期(分钟，5的倍数)
        /// </summary>
        public int StaticPeriod { get; set; }

        /// <summary>
        /// 绝对阀值
        /// </summary>
        public double AbsoluteThreshold { get; set; }

        /// <summary>
        /// 百分比阀值
        /// </summary>
        public double PerThreshold { get; set; }

        /// <summary>
        /// 扩展设置1
        /// </summary>
        public string ExtSet1 { get; set; }

        /// <summary>
        /// 扩展设置2
        /// </summary>
        public string ExtSet2 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
