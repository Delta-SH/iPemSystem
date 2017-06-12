using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IPointService {
        IPagedList<P_Point> GetAllPoints(int pageIndex = 0, int pageSize = int.MaxValue);

        List<P_Point> GetAllPointsAsList();

        IPagedList<P_Point> GetPointsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue);

        List<P_Point> GetPointsInDeviceAsList(string device);

        IPagedList<P_Point> GetPointsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do, int pageIndex = 0, int pageSize = int.MaxValue);

        List<P_Point> GetPointsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do);

        IPagedList<P_Point> GetPointsInProtocol(string protocol, int pageIndex = 0, int pageSize = int.MaxValue);

        List<P_Point> GetPointsInProtocolAsList(string protocol);
    }
}