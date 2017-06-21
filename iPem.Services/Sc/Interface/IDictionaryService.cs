using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 系统参数API
    /// </summary>
    public partial interface IDictionaryService {
        /// <summary>
        /// 获得指定的系统参数
        /// </summary>
        M_Dictionary GetDictionary(int id);

        /// <summary>
        /// 获得所有的系统参数
        /// </summary>
        List<M_Dictionary> GetDictionaries();

        /// <summary>
        /// 获得所有的系统参数(分页)
        /// </summary>
        IPagedList<M_Dictionary> GetPagedDictionaries(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 更新系统参数
        /// </summary>
        void Update(params M_Dictionary[] dictionaries);
    }
}
