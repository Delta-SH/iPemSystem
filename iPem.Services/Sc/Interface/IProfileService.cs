using iPem.Core.Domain.Sc;
using System;

namespace iPem.Services.Sc {
    /// <summary>
    /// 用户自定义信息API
    /// </summary>
    public partial interface IProfileService {
        /// <summary>
        /// 获得指定用户的自定义信息
        /// </summary>
        U_Profile GetProfile(Guid uid);

        /// <summary>
        /// 保存指定用户的自定义信息
        /// </summary>
        void Save(U_Profile profile);

        /// <summary>
        /// 删除指定用户的自定义信息
        /// </summary>
        void Remove(Guid uid);
    }
}