using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IActAlmService {
        IPagedList<A_AAlarm> GetAlmsInArea(string area, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_AAlarm> GetAlmsInAreaAsList(string area);

        IPagedList<A_AAlarm> GetAlmsInStation(string station, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_AAlarm> GetAlmsInStationAsList(string station);

        IPagedList<A_AAlarm> GetAlmsInRoom(string room, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_AAlarm> GetAlmsInRoomAsList(string room);

        IPagedList<A_AAlarm> GetAlmsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_AAlarm> GetAlmsInDeviceAsList(string device);

        IPagedList<A_AAlarm> GetAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_AAlarm> GetAlmsAsList(DateTime start, DateTime end);

        IPagedList<A_AAlarm> GetAllAlms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_AAlarm> GetAllAlmsAsList();
    }
}
