using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 关注信号表
    /// </summary>
    public partial interface IU_FollowPointRepository {
        /// <summary>
        /// 获得指定用户的关注信号
        /// </summary>
        List<U_FollowPoint> GetFollowPointsInUser(Guid id);

        /// <summary>
        /// 获得所有的关注信号
        /// </summary>
        List<U_FollowPoint> GetFollowPoints();

        /// <summary>
        /// 新增关注信号
        /// </summary>
        void Insert(IList<U_FollowPoint> entities);

        /// <summary>
        /// 删除关注信号
        /// </summary>
        void Delete(IList<U_FollowPoint> entities);
    }
}
