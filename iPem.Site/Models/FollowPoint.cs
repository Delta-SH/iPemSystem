using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FollowPoint {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device { get; set; }

        /// <summary>
        /// 信号编号
        /// </summary>
        public string point { get; set; }
    }
}