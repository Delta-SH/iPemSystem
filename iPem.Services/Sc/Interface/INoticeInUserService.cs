using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 用户消息映射API
    /// </summary>
    public partial interface INoticeInUserService {
        /// <summary>
        /// 获得所有的用户消息映射关系
        /// </summary>
        List<H_NoticeInUser> GetNoticesInUsers();

        /// <summary>
        /// 获得指定的用户消息映射关系
        /// </summary>
        List<H_NoticeInUser> GetNoticesInUser(Guid uid);

        /// <summary>
        /// 新增用户消息映射关系
        /// </summary>
        void Add(params H_NoticeInUser[] notices);

        /// <summary>
        /// 更新用户消息映射关系
        /// </summary>
        void Update(params H_NoticeInUser[] notices);

        /// <summary>
        /// 删除用户消息映射关系
        /// </summary>
        void Remove(params H_NoticeInUser[] notices);
    }
}
