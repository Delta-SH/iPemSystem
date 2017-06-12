using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisAlmService {
        IPagedList<A_HAlarm> GetAlmsInArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_HAlarm> GetAlmsInAreaAsList(string area, DateTime start, DateTime end);

        IPagedList<A_HAlarm> GetAlmsInStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_HAlarm> GetAlmsInStationAsList(string station, DateTime start, DateTime end);

        IPagedList<A_HAlarm> GetAlmsInRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_HAlarm> GetAlmsInRoomAsList(string room, DateTime start, DateTime end);

        IPagedList<A_HAlarm> GetAlmsInDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_HAlarm> GetAlmsInDeviceAsList(string device, DateTime start, DateTime end);

        IPagedList<A_HAlarm> GetAlmsInPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_HAlarm> GetAlmsInPointAsList(string point, DateTime start, DateTime end);

        IPagedList<A_HAlarm> GetAllAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_HAlarm> GetAllAlmsAsList(DateTime start, DateTime end);
    }
}
