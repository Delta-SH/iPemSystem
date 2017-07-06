using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 历史告警表
    /// </summary>
    public partial interface IA_HAlarmRepository {
        /// <summary>
        /// 获得指定区域(第三级区域)的告警
        /// </summary>
        List<A_HAlarm> GetAlarmsInArea(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定站点的告警
        /// </summary>
        List<A_HAlarm> GetAlarmsInStation(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定机房的告警
        /// </summary>
        List<A_HAlarm> GetAlarmsInRoom(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的告警
        /// </summary>
        List<A_HAlarm> GetAlarmsInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定信号的告警
        /// </summary>
        List<A_HAlarm> GetAlarmsInPoint(string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时段内的告警
        /// </summary>
        List<A_HAlarm> GetAlarms(DateTime start, DateTime end);

        /// <summary>
        /// 获得所有的告警(包括系统告警、次告警、关联告警、过滤告警、屏蔽告警)
        /// </summary>
        List<A_HAlarm> GetAllAlarms(DateTime start, DateTime end);

        /// <summary>
        /// 获得非正常告警(包括系统告警、屏蔽告警)
        /// </summary>
        List<A_HAlarm> GetNonAlarms(DateTime start, DateTime end);

        /// <summary>
        /// 获取指定告警的次告警
        /// </summary>
        List<A_HAlarm> GetPrimaryAlarms(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定告警的关联告警
        /// </summary>
        List<A_HAlarm> GetRelatedAlarms(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定告警的过滤告警
        /// </summary>
        List<A_HAlarm> GetFilterAlarms(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定告警的翻转告警
        /// </summary>
        List<A_HAlarm> GetReversalAlarms(string id, DateTime start, DateTime end);
    }
}
