using iPem.Core;
using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Services.History {
    public partial interface IActValueService {

        IPagedList<ActValue> GetActValues(string device, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<ActValue> GetActValues(string[] devices, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<ActValue> GetAllActValues(int pageIndex = 0, int pageSize = int.MaxValue);

        void AddValue(ActValue value);

        void AddValues(List<ActValue> values);

        void Clear();

    }
}