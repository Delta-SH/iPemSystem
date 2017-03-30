using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPem.Site.Models.BInterface {
    /// <summary>
    /// Represents the point enumeration
    /// </summary>
    /// <remarks>
    /// 4-遥信信号（DI）
    /// 3-遥测信号（AI）
    /// 1-遥控信号（DO）
    /// 2-遥调信号（AO）
    /// 0-告警信号（AL）
    /// </remarks>
    public enum EnmBIPoint {
        DI = 4,
        AI = 3,
        DO = 1,
        AO = 2,
        AL = 0
    }
}