using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IPointService {
        IPagedList<Point> GetAllPoints(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Point> GetAllPointsAsList();

        IPagedList<Point> GetPointsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Point> GetPointsInDeviceAsList(string device);

        IPagedList<Point> GetPointsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Point> GetPointsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do);

        IPagedList<Point> GetPointsInProtocol(string protocol, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Point> GetPointsInProtocolAsList(string protocol);
    }
}