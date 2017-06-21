using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 用户消息映射表
    /// </summary>
    public partial interface IH_NoticeInUserRepository {
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
        void Insert(IList<H_NoticeInUser> entities);

        /// <summary>
        /// 更新用户消息映射关系
        /// </summary>
        void Update(IList<H_NoticeInUser> entities);

        /// <summary>
        /// 删除用户消息映射关系
        /// </summary>
        void Delete(IList<H_NoticeInUser> entities);
    }
}
