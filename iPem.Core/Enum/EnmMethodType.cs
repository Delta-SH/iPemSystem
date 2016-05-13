using System;

namespace iPem.Core.Enum {
    /// <summary>
    /// Represents an method type
    /// </summary>
    /// <remarks>
    /// { Id: 1, Name: "区域" }
    /// { Id: 2, Name: "站点" }
    /// { Id: 3, Name: "机房" }
    /// { Id: 4, Name: "FSU" }
    /// { Id: 5, Name: "UPS" }
    /// { Id: 6, Name: "变压器" }
    /// { Id: 7, Name: "发电机组" }
    /// { Id: 8, Name: "风能设备" }
    /// { Id: 9, Name: "开关熔丝" }
    /// { Id: 10, Name: "移动发电机" }
    /// { Id: 11, Name: "中央空调主机系统" }
    /// { Id: 12, Name: "自动电源切换柜" }
    /// { Id: 13, Name: "设备" }
    /// { Id: 14, Name: "信号" }
    /// { Id: 15, Name: "员工"}
    /// </remarks>
    public enum EnmMethodType {
        Area = 1,
        Station, 
        Room,
        Fsu,
        Ups,
        BianYaQi,
        FaDianJiZu,
        FengNengSheBei,
        KaiGuanRongSi,
        YiDongFaDianJi,
        ZhongYangKongTiaoZhuJiXiTong,
        ZiDongDianYuanQieHuanGui,
        Device,
        Point,
        Employee
    }
}