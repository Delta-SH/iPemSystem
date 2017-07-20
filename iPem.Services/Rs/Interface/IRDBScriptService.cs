using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 脚本升级API
    /// </summary>
    public partial interface IRDBScriptService {
        /// <summary>
        /// 获取所有的脚本升级记录
        /// </summary>
        List<R_DBScript> GetEntities();

        /// <summary>
        /// 添加脚本升级记录
        /// </summary>
        void Add(params R_DBScript[] scripts);

        /// <summary>
        /// 更新脚本升级记录
        /// </summary>
        void Update(params R_DBScript[] scripts);

        /// <summary>
        /// 删除脚本升级记录
        /// </summary>
        void Remove(params string[] ids);

        /// <summary>
        /// 获取所有的脚本升级记录(分页)
        /// </summary>
        IPagedList<R_DBScript> GetPagedDBScripts(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
