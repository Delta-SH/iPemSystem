using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisAlmService {
        IPagedList<HisAlm> GetAlmsInArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisAlm> GetAlmsInAreaAsList(string area, DateTime start, DateTime end);

        IPagedList<HisAlm> GetAlmsInStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisAlm> GetAlmsInStationAsList(string station, DateTime start, DateTime end);

        IPagedList<HisAlm> GetAlmsInRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisAlm> GetAlmsInRoomAsList(string room, DateTime start, DateTime end);

        IPagedList<HisAlm> GetAlmsInDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisAlm> GetAlmsInDeviceAsList(string device, DateTime start, DateTime end);

        IPagedList<HisAlm> GetAllAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisAlm> GetAllAlmsAsList(DateTime start, DateTime end);
    }
}
