using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 系统消息API
    /// </summary>
    public partial interface INoticeService {
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
        List<H_Notice> GetNoticesInUser(Guid uid);

        /// <summary>
        /// 获得指定用户的未读消息
        /// </summary>
        List<H_Notice> GetUnreadNotices(Guid uid);

        /// <summary>
        /// 获得所有的系统消息
        /// </summary>
        IPagedList<H_Notice> GetPagedNotices(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定时间内的系统消息
        /// </summary>
        IPagedList<H_Notice> GetPagedNoticesInSpan(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定用户的系统消息
        /// </summary>
        IPagedList<H_Notice> GetPagedNoticesInUser(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定用户的未读消息
        /// </summary>
        IPagedList<H_Notice> GetPagedUnreadNotices(Guid uid, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增系统消息
        /// </summary>
        void Add(params H_Notice[] notices);

        /// <summary>
        /// 更新系统消息
        /// </summary>
        void Update(params H_Notice[] notices);

        /// <summary>
        /// 删除系统消息
        /// </summary>
        void Remove(params H_Notice[] notices);
    }
}
