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
        Report,
        /// <summary>
        /// 脚本升级功能
        /// </summary>
        Script
    }

    /// <summary>
    /// 能耗公式类型
    /// </summary>
    public enum EnmFormula {
        /// <summary>
        /// 空调
        /// </summary>
        KT = 1001,
        /// <summary>
        /// 照明
        /// </summary>
        ZM,
        /// <summary>
        /// 办公
        /// </summary>
        BG,
        /// <summary>
        /// 开关电源
        /// </summary>
        DY,
        /// <summary>
        /// UPS
        /// </summary>
        UPS,
        /// <summary>
        /// IT设备
        /// </summary>
        IT,
        /// <summary>
        /// 其他
        /// </summary>
        QT,
        /// <summary>
        /// 总计
        /// </summary>
        TT,
        /// <summary>
        /// PUE
        /// </summary>
        PUE,
        /// <summary>
        /// 停电标识
        /// </summary>
        TD,
        /// <summary>
        /// 温度标识
        /// </summary>
        WD,
        /// <summary>
        /// 湿度标识
        /// </summary>
        SD,
        /// <summary>
        /// 发电标识
        /// </summary>
        FD,
        /// <summary>
        /// 发电量
        /// </summary>
        FDL,
        /// <summary>
        /// 变压器
        /// </summary>
        BY,
        /// <summary>
        /// 线损
        /// </summary>
        XS
    }

    /// <summary>
    /// 公式运算方式
    /// </summary>
    public enum EnmCompute {
        /// <summary>
        /// 电度
        /// </summary>
        Kwh,
        /// <summary>
        /// 功率
        /// </summary>
        Power
    }
}
