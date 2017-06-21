using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 系统参数表
    /// </summary>
    public partial interface IM_DictionaryRepository {
        /// <summary>
        /// 获得指定的系统参数
        /// </summary>
        M_Dictionary GetDictionary(int id);

        /// <summary>
        /// 获得所有的系统参数
        /// </summary>
        List<M_Dictionary> GetDictionaries();

        /// <summary>
        /// 更新系统参数
        /// </summary>
        void Update(IList<M_Dictionary> entities);
    }
}