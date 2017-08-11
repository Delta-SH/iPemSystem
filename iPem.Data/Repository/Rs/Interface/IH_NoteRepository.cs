using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 配置同步表
    /// </summary>
    public partial interface IH_NoteRepository {

        /// <summary>
        /// 获得配置同步命令
        /// </summary>
        List<H_Note> GetEntities();

        /// <summary>
        /// 添加配置同步命令
        /// </summary>
        void Insert(IList<H_Note> entities);

        /// <summary>
        /// 删除配置同步命令
        /// </summary>
        void Delete(IList<H_Note> entities);

        /// <summary>
        /// 清空配置同步命令
        /// </summary>
        void Clear();

    }
}
