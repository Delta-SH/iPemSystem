using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 脚本升级API
    /// </summary>
    public partial interface ISDBScriptService {
        /// <summary>
        /// 获取所有的脚本升级记录
        /// </summary>
        List<S_DBScript> GetEntities();

        /// <summary>
        /// 添加脚本升级记录
        /// </summary>
        void Add(params S_DBScript[] scripts);

        /// <summary>
        /// 更新脚本升级记录
        /// </summary>
        void Update(params S_DBScript[] scripts);

        /// <summary>
        /// 删除脚本升级记录
        /// </summary>
        void Remove(params string[] ids);

        /// <summary>
        /// 获取所有的脚本升级记录(分页)
        /// </summary>
        IPagedList<S_DBScript> GetPagedDBScripts(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
