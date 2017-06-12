using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisBatService {
        IPagedList<V_Bat> GetHisBats(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Bat> GetHisBatsAsList(string device, DateTime start, DateTime end);

        IPagedList<V_Bat> GetHisBats(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Bat> GetHisBatsAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<V_Bat> GetHisBats(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Bat> GetHisBatsAsList(DateTime start, DateTime end);

        List<V_Bat> GetProcedures(string device, DateTime start, DateTime end);

        List<V_Bat> GetProcedures(string device, string point, DateTime start, DateTime end);

        List<V_Bat> GetProcedures(DateTime start, DateTime end);
    }
}
