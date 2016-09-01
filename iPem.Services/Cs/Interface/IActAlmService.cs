using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IActAlmService {
        IPagedList<ActAlm> GetAlmsInArea(string area, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ActAlm> GetAlmsInAreaAsList(string area);

        IPagedList<ActAlm> GetAlmsInStation(string station, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ActAlm> GetAlmsInStationAsList(string station);

        IPagedList<ActAlm> GetAlmsInRoom(string room, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ActAlm> GetAlmsInRoomAsList(string room);

        IPagedList<ActAlm> GetAlmsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ActAlm> GetAlmsInDeviceAsList(string device);

        IPagedList<ActAlm> GetAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ActAlm> GetAlmsAsList(DateTime start, DateTime end);

        IPagedList<ActAlm> GetAllAlms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<ActAlm> GetAllAlmsAsList();
    }
}
