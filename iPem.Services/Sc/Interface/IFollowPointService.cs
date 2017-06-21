using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 关注信号API
    /// </summary>
    public partial interface IFollowPointService {
        /// <summary>
        /// 获得指定用户的关注信号
        /// </summary>
        List<U_FollowPoint> GetFollowPointsInUser(Guid id);

        /// <summary>
        /// 获得所有的关注信号
        /// </summary>
        List<U_FollowPoint> GetFollowPoints();

        /// <summary>
        /// 获得指定用户的关注信号(分页)
        /// </summary>
        IPagedList<U_FollowPoint> GetPagedFollowPointsInUser(Guid id, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得所有的关注信号(分页)
        /// </summary>
        IPagedList<U_FollowPoint> GetPagedFollowPoints(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增关注信号
        /// </summary>
        void Add(params U_FollowPoint[] points);

        /// <summary>
        /// 删除关注信号
        /// </summary>
        void Remove(params U_FollowPoint[] points);
    }
}
