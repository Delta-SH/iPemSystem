using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisValueService {
        IPagedList<V_HMeasure> GetValuesByArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByAreaAsList(string area, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValuesByStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByStationAsList(string station, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValuesByRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByRoomAsList(string room, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValuesByDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByDeviceAsList(string device, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValuesByPoint(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByPointAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValuesByPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByPointAsList(string point, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValuesByPoint(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesByPointAsList(string[] points, DateTime start, DateTime end);

        IPagedList<V_HMeasure> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_HMeasure> GetValuesAsList(DateTime start, DateTime end);
    }
}
