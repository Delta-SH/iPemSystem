using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 脚本升级表
    /// </summary>
    public partial interface IR_DBScriptRepository {
        /// <summary>
        /// 获取所有的脚本升级记录
        /// </summary>
        List<R_DBScript> GetEntities();

        /// <summary>
        /// 添加脚本升级记录
        /// </summary>
        void Insert(IList<R_DBScript> entities);

        /// <summary>
        /// 更新脚本升级记录
        /// </summary>
        void Update(IList<R_DBScript> entities);

        /// <summary>
        /// 删除脚本升级记录
        /// </summary>
        void Delete(IList<string> ids);
    }
}
