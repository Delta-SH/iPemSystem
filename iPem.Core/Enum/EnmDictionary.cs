using System;

namespace iPem.Core.Enum {
    /// <summary>
    /// Represents the system dictionary keys
    /// </summary>
    public enum EnmDictionary {
        Ws = 1,
        Ts,
        Pue,
        Report
    }

    /// <summary>
    /// Represents the formulas
    /// </summary>
    /// <remarks>
    /// KT: 空调
    /// ZM: 照明
    /// BG: 办公
    /// SB: 设备
    /// KGDY: 开关电源
    /// UPS: UPS
    /// QT: 其他
    /// ZL: 总量
    /// </remarks>
    public enum EnmFormula {
        KT,
        ZM,
        BG,
        SB,
        KGDY,
        UPS,
        QT,
        ZL,
        PUE
    }
}
