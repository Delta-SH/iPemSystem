using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 关注信号表
    /// </summary>
    [Serializable]
    public partial class U_FollowPoint : BaseEntity {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编号
        /// </summary>
        public string PointId { get; set; }
    }
}
