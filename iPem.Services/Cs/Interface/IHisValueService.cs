using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisValueService {
        IPagedList<HisValue> GetValuesByArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByAreaAsList(string area, DateTime start, DateTime end);

        IPagedList<HisValue> GetValuesByStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByStationAsList(string station, DateTime start, DateTime end);

        IPagedList<HisValue> GetValuesByRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByRoomAsList(string room, DateTime start, DateTime end);

        IPagedList<HisValue> GetValuesByDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByDeviceAsList(string device, DateTime start, DateTime end);

        IPagedList<HisValue> GetValuesByPoint(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByPointAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<HisValue> GetValuesByPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByPointAsList(string point, DateTime start, DateTime end);

        IPagedList<HisValue> GetValuesByPoint(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesByPointAsList(string[] points, DateTime start, DateTime end);

        IPagedList<HisValue> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesAsList(DateTime start, DateTime end);
    }
}
