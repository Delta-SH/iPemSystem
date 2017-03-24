using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IExtAlarmService {

        IPagedList<ExtAlarm> GetAllExtAlarms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlarm> GetAllExtAlarmsAsList();

        void Update(List<ExtAlarm> entities);

        IPagedList<ExtAlarm> GetHisExtAlarms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlarm> GetHisExtAlarmsAsList(DateTime start, DateTime end);

    }
}
