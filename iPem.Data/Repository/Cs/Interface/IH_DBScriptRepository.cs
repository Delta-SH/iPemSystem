using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 脚本升级表
    /// </summary>
    public partial interface IH_DBScriptRepository {
        /// <summary>
        /// 获取所有的脚本升级记录
        /// </summary>
        List<H_DBScript> GetEntities();

        /// <summary>
        /// 添加脚本升级记录
        /// </summary>
        void Insert(IList<H_DBScript> entities);

        /// <summary>
        /// 更新脚本升级记录
        /// </summary>
        void Update(IList<H_DBScript> entities);

        /// <summary>
        /// 删除脚本升级记录
        /// </summary>
        void Delete(IList<string> ids);
    }
}
