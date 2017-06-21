using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 用户自定义信息表
    /// </summary>
    public partial interface IU_ProfileRepository {
        /// <summary>
        /// 获得指定用户的自定义信息
        /// </summary>
        U_Profile GetProfile(Guid id);

        /// <summary>
        /// 保存指定用户的自定义信息
        /// </summary>
        void Save(U_Profile entity);

        /// <summary>
        /// 删除指定用户的自定义信息
        /// </summary>
        void Delete(Guid id);
    }
}
