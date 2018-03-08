using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 活动告警API
    /// </summary>
    public partial interface IAAlarmService {
        /// <summary>
        /// 获得指定区域的告警
        /// </summary>
        List<A_AAlarm> GetAlarmsInArea(string id);

        /// <summary>
        /// 获得指定站点的告警
        /// </summary>
        List<A_AAlarm> GetAlarmsInStation(string id);

        /// <summary>
        /// 获得指定机房的告警
        /// </summary>
        List<A_AAlarm> GetAlarmsInRoom(string id);

        /// <summary>
        /// 获得指定设备的告警
        /// </summary>
        List<A_AAlarm> GetAlarmsInDevice(string id);

        /// <summary>
        /// 获得指定时间内的告警
        /// </summary>
        List<A_AAlarm> GetAlarmsInSpan(DateTime start, DateTime end);

        /// <summary>
        /// 获得所有的告警
        /// </summary>
        List<A_AAlarm> GetAlarms();

        /// <summary>
        /// 获取指定时间段内告警(包括次告警、关联告警、屏蔽告警等)
        /// </summary>
        List<A_AAlarm> GetAllAlarmsInSpan(DateTime start, DateTime end);

        /// <summary>
        /// 获取全部告警(包括次告警、关联告警、屏蔽告警等)
        /// </summary>
        List<A_AAlarm> GetAllAlarms();

        /// <summary>
        /// 获取指定告警的次告警
        /// </summary>
        List<A_AAlarm> GetPrimaryAlarms(string id);

        /// <summary>
        /// 获取指定告警的关联告警
        /// </summary>
        List<A_AAlarm> GetRelatedAlarms(string id);

        /// <summary>
        /// 获取指定告警的过滤告警
        /// </summary>
        List<A_AAlarm> GetFilterAlarms(string id);

        /// <summary>
        /// 获得所有的告警(分页)
        /// </summary>
        IPagedList<A_AAlarm> GetPagedAlarms(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 确认指定的告警
        /// </summary>
        void Confirm(params A_AAlarm[] alarms);

        /// <summary>
        /// 获得指定的告警
        /// </summary>
        A_AAlarm GetAlarm(string id);

        /// <summary>
        /// 删除指定的告警（包括告警接口中的告警）
        /// </summary>
        void RemoveAlarms(params A_AAlarm[] alarms);
    }
}
