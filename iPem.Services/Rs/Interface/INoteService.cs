using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 配置同步API
    /// </summary>
    public partial interface INoteService {
        /// <summary>
        /// 获得配置同步命令
        /// </summary>
        List<H_Note> GetNotices();

        /// <summary>
        /// 添加配置同步命令
        /// </summary>
        void Add(params H_Note[] notices);

        /// <summary>
        /// 删除配置同步命令
        /// </summary>
        void Remove(params H_Note[] notices);

        /// <summary>
        /// 清空配置同步命令
        /// </summary>
        void Clear();
    }
}
