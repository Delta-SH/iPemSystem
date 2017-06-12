using System;

namespace iPem.Core.Enum {
    /// <summary>
    /// 参数分类
    /// </summary>
    public enum EnmDictionary {
        /// <summary>
        /// Fsu统一接口参数
        /// </summary>
        Ws = 1,
        /// <summary>
        /// 告警播报参数
        /// </summary>
        Ts,
        /// <summary>
        /// 能耗参数
        /// </summary>
        Elec,
        /// <summary>
        /// 报表相关参数
        /// </summary>
        Report
    }

    /// <summary>
    /// 公式类型
    /// </summary>
    public enum EnmFormula {
        /// <summary>
        /// 空调
        /// </summary>
        KT,
        /// <summary>
        /// 照明
        /// </summary>
        ZM,
        /// <summary>
        /// 办公
        /// </summary>
        BG,
        /// <summary>
        /// 设备
        /// </summary>
        SB,
        /// <summary>
        /// 开关电源
        /// </summary>
        KGDY,
        /// <summary>
        /// UPS
        /// </summary>
        UPS,
        /// <summary>
        /// 其他
        /// </summary>
        QT,
        /// <summary>
        /// 总量
        /// </summary>
        ZL,
        /// <summary>
        /// PUE
        /// </summary>
        PUE
    }
}
