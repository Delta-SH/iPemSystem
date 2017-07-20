using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 脚本升级API
    /// </summary>
    public partial interface IHDBScriptService {
        /// <summary>
        /// 获取所有的脚本升级记录
        /// </summary>
        List<H_DBScript> GetEntities();

        /// <summary>
        /// 添加脚本升级记录
        /// </summary>
        void Add(params H_DBScript[] scripts);

        /// <summary>
        /// 更新脚本升级记录
        /// </summary>
        void Update(params H_DBScript[] scripts);

        /// <summary>
        /// 删除脚本升级记录
        /// </summary>
        void Remove(params string[] ids);

        /// <summary>
        /// 获取所有的脚本升级记录(分页)
        /// </summary>
        IPagedList<H_DBScript> GetPagedDBScripts(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}