using iPem.Core;
using iPem.Core.Domain.History;
using iPem.Core.Enum;
using System;

namespace iPem.Services.History {
    public partial interface IActAlmService {

        IPagedList<ActAlm> GetActAlmsByDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<ActAlm> GetActAlmsByLevels(int[] levels, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<ActAlm> GetActAlmsByTime(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<ActAlm> GetAllActAlms(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
