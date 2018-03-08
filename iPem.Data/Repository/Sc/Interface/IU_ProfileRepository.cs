using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 用户自定义信息表
    /// </summary>
    public partial interface IU_ProfileRepository {
        /// <summary>
        /// 获得用户的订制信息
        /// </summary>
        U_Profile Get(Guid uid, EnmProfile type);

        /// <summary>
        /// 保存用户的订制信息
        /// </summary>
        void Save(U_Profile entity);

        /// <summary>
        /// 删除用户的订制信息
        /// </summary>
        void Delete(Guid uid, EnmProfile type);

        /// <summary>
        /// 清空用户的所有订制信息
        /// </summary>
        void Clear(Guid uid);
    }
}
