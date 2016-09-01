using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisValueService {
        IPagedList<HisValue> GetValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesAsList(string device, DateTime start, DateTime end);

        IPagedList<HisValue> GetValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<HisValue> GetValues(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesAsList(string[] points, DateTime start, DateTime end);

        IPagedList<HisValue> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisValue> GetValuesAsList(DateTime start, DateTime end);
    }
}
