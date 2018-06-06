using System;

namespace iPem.Site.Models {
    public class RtValues {
        /// <summary>
        /// 超频告警
        /// </summary>
        public int chaoPin { get; set; }

        /// <summary>
        /// 超短告警
        /// </summary>
        public double chaoDuan { get; set; }

        /// <summary>
        /// 超长告警
        /// </summary>
        public double chaoChang { get; set; }

        /// <summary>
        /// 系统设备完好率
        /// 为了规避频繁告警，报表统计时将忽略告警历时小于该阈值的告警
        /// </summary>
        public double whlHuLue { get; set; }

        /// <summary>
        /// 故障处理及时率
        /// 报表统计时将计算超过该规定处理时长的告警
        /// </summary>
        public double jslGuiDing { get; set; }

        /// <summary>
        /// 故障处理及时率
        /// 为了规避频繁告警，报表统计时将忽略告警历时小于该阈值的告警
        /// </summary>
        public double jslHuLue { get; set; }

        /// <summary>
        /// 告警确认及时率
        /// 报表统计时将计算超过该规定确认时长的告警
        /// </summary>
        public double jslQueRen { get; set; }

        /// <summary>
        /// 直流系统可用度(核心站点)
        /// 设备类型
        /// </summary>
        public string[] hxzlxtkydLeiXing { get; set; }

        /// <summary>
        /// 直流系统可用度(核心站点)
        /// 输出电压过低告警
        /// </summary>
        public string[] hxzlxtkydXinHao { get; set; }

        /// <summary>
        /// 交流不间断系统可用度(核心站点)
        /// 设备类型
        /// </summary>
        public string[] hxjlxtkydLeiXing { get; set; }

        /// <summary>
        /// 交流不间断系统可用度(核心站点)
        /// 输出电压过低告警
        /// </summary>
        public string[] hxjlxtkydXinHao { get; set; }

        /// <summary>
        /// 交流不间断系统可用度(核心站点)
        /// 旁路运行告警
        /// </summary>
        public string[] hxjlxtkydPangLuXinHao { get; set; }

        /// <summary>
        /// 温控系统可用度(核心站点)
        /// 温度信号
        /// </summary>
        public string[] hxwkxtkydWenDuXinHao { get; set; }

        /// <summary>
        /// 温控系统可用度(核心站点)
        /// 高温告警
        /// </summary>
        public string[] hxwkxtkydGaoWenXinHao { get; set; }

        /// <summary>
        /// 监控可用度(核心站点)
        /// 监控系统采集设备中断告警
        /// </summary>
        public string[] hxjkkydXinHao { get; set; }

        /// <summary>
        /// 监控可用度(核心站点)
        /// 动环监控采集设备类型
        /// </summary>
        public string[] hxjkkydLeiXing { get; set; }

        /// <summary>
        /// 关键监控测点接入率(其他站点)
        /// 关键信号
        /// </summary>
        public string[] qtgjjkcdjrlXinHao { get; set; }

        /// <summary>
        /// 关键监控测点接入率(其他站点)
        /// 电源设备
        /// </summary>
        public string[] qtgjjkcdjrlLeiXing { get; set; }

        /// <summary>
        /// 温控容量合格率(其他站点)
        /// 温度信号
        /// </summary>
        public string[] qtwkrlhglWenDuXinHao { get; set; }

        /// <summary>
        /// 温控容量合格率(其他站点)
        /// 高温告警信号
        /// </summary>
        public string[] qtwkrlhglGaoWenXinHao { get; set; }

        /// <summary>
        /// 直流系统可用度(其他站点)
        /// 开关电源一次下电告警
        /// </summary>
        public string[] qtzlxtkydXinHao { get; set; }

        /// <summary>
        /// 直流系统可用度(其他站点)
        /// 开关电源设备类型
        /// </summary>
        public string[] qtzlxtkydLeiXing { get; set; }

        /// <summary>
        /// 监控故障处理及时率(其他站点)
        /// 站点动环通信中断告警
        /// </summary>
        public string[] qtjkgzcljslXinHao { get; set; }

        /// <summary>
        /// 开关电源带载合格率(其他站点)
        /// 开关电源设备类型
        /// </summary>
        public string[] qtkgdydzhglLeiXing { get; set; }

        /// <summary>
        /// 开关电源带载合格率(其他站点)
        /// 工作状态（均充、浮充、放电）
        /// </summary>
        public string[] qtkgdydzhglztXinHao { get; set; }

        /// <summary>
        /// 开关电源带载合格率(其他站点)
        /// 负载电流信号
        /// </summary>
        public string[] qtkgdydzhglfzXinHao { get; set; }

        /// <summary>
        /// 蓄电池后备时长合格率(其他站点)
        /// 蓄电池组总电压
        /// </summary>
        public string[] qtxdchbschglXinHao { get; set; }

        /// <summary>
        /// 蓄电池后备时长合格率(其他站点)
        /// 合格电压
        /// </summary>
        public double qtxdchbschglDianYa { get; set; }

        /// <summary>
        /// 蓄电池后备时长合格率(其他站点)
        /// 放电时间
        /// </summary>
        public double qtxdchbschglShiJian { get; set; }

        /// <summary>
        /// 油机发电统计
        /// 设备类型
        /// </summary>
        public string[] fdjzLeiXing { get; set; }

        /// <summary>
        /// 变压器能耗统计
        /// 设备类型
        /// </summary>
        public string[] byqnhLeiXing { get; set; }

        /// <summary>
        /// 能耗首页第一指标
        /// </summary>
        public int indicator01 { get; set; }

        /// <summary>
        /// 能耗首页第二指标
        /// </summary>
        public int indicator02 { get; set; }

        /// <summary>
        /// 能耗首页第三指标
        /// </summary>
        public int indicator03 { get; set; }

        /// <summary>
        /// 能耗首页第四指标
        /// </summary>
        public int indicator04 { get; set; }

        /// <summary>
        /// 能耗首页第五指标
        /// </summary>
        public int indicator05 { get; set; }

        /// <summary>
        /// 能耗首页统计指标
        /// </summary>
        public int indicator { get; set; }

        /// <summary>
        /// 工程预约审核
        /// 生效时长
        /// </summary>
        public double gcyyshsxShiChang { get; set; }
    }
}