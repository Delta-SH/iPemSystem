using System;

namespace iPem.Core.Enum {
    /// <summary>
    /// 返回结果
    /// </summary>
    public enum EnmAPIResult {
        FAILURE,
        SUCCESS
    }

    /// <summary>
    /// API版本
    /// </summary>
    public enum EnmAPIVersion {
        V10,
        V20 = 20
    }

    /// <summary>
    /// API组态图片类型
    /// </summary>
    public enum EnmAPIImage {
        Png,
        Xaml
    }

    /// <summary>
    /// API节点类型
    /// </summary>
    public enum EnmAPISCObj {
        Area,
        Station,
        Room,
        Device,
        Signal
    }

    /// <summary>
    /// API节点状态
    /// </summary>
    public enum EnmAPIState {
        /// <summary>
        /// 正常数据
        /// </summary>
        NoAlarm,
        /// <summary>
        /// 紧急告警
        /// </summary>
        Critical,
        /// <summary>
        /// 重要告警
        /// </summary>
        Major,
        /// <summary>
        /// 次要告警
        /// </summary>
        Minor,
        /// <summary>
        /// 提示告警
        /// </summary>
        Hint,
        /// <summary>
        /// 无效数据
        /// </summary>
        Invalid
    }

}