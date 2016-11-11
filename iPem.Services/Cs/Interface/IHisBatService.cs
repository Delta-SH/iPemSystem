using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisBatService {
        IPagedList<HisBat> GetHisBats(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisBat> GetHisBatsAsList(string device, DateTime start, DateTime end);

        IPagedList<HisBat> GetHisBats(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisBat> GetHisBatsAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<HisBat> GetHisBats(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisBat> GetHisBatsAsList(DateTime start, DateTime end);

        List<HisBat> GetProcedures(string device, DateTime start, DateTime end);

        List<HisBat> GetProcedures(string device, string point, DateTime start, DateTime end);

        List<HisBat> GetProcedures(DateTime start, DateTime end);
    }
}
