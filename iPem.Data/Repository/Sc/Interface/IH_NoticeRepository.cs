using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 系统消息表
    /// </summary>
    public partial interface IH_NoticeRepository {
        /// <summary>
        /// 获得指定编号的系统消息
        /// </summary>
        H_Notice GetNotice(Guid id);

        /// <summary>
        /// 获得所有的系统消息
        /// </summary>
        List<H_Notice> GetNotices();

        /// <summary>
        /// 获得指定时间内的系统消息
        /// </summary>
        List<H_Notice> GetNoticesInSpan(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定用户的系统消息
        /// </summary>
        List<H_Notice> GetNoticesInUser(string uid);

        /// <summary>
        /// 获得指定用户的未读消息
        /// </summary>
        List<H_Notice> GetUnreadNotices(string uid);

        /// <summary>
        /// 新增系统消息
        /// </summary>
        void Insert(IList<H_Notice> entities);

        /// <summary>
        /// 更新系统消息
        /// </summary>
        void Update(IList<H_Notice> entities);

        /// <summary>
        /// 删除系统消息
        /// </summary>
        void Delete(IList<H_Notice> entities);
    }
}
