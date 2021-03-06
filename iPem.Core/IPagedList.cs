﻿using System;
using System.Collections.Generic;

namespace iPem.Core {
    /// <summary>
    /// 自定义分页类
    /// </summary>
    public interface IPagedList<T> : IList<T> {
        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}
