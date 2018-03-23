using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;

namespace iPem.Services.Sc {
    /// <summary>
    /// 用户自定义信息API
    /// </summary>
    public partial interface IProfileService {
        /// <summary>
        /// 获得用户的订制信息
        /// </summary>
        U_Profile GetProfile(string uid, EnmProfile type);

        /// <summary>
        /// 保存用户的订制信息
        /// </summary>
        void SaveProfile(U_Profile profile);

        /// <summary>
        /// 删除用户的订制信息
        /// </summary>
        void RemoveProfile(string uid, EnmProfile type);

        /// <summary>
        /// 清空用户的所有订制信息
        /// </summary>
        /// <param name="uid"></param>
        void ClearProfiles(string uid);
    }
}