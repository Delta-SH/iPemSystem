using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 脚本升级表
    /// </summary>
    public partial interface IS_DBScriptRepository {
        /// <summary>
        /// 获取所有的脚本升级记录
        /// </summary>
        List<S_DBScript> GetEntities();

        /// <summary>
        /// 添加脚本升级记录
        /// </summary>
        void Insert(IList<S_DBScript> entities);

        /// <summary>
        /// 更新脚本升级记录
        /// </summary>
        void Update(IList<S_DBScript> entities);

        /// <summary>
        /// 删除脚本升级记录
        /// </summary>
        void Delete(IList<string> ids);
    }
}
