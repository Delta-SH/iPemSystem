using iPem.Core.Enum;
using System;

namespace iPem.Site.Models.API {
    public class API_GV_Signal {
        /// <summary>
        /// 所属设备
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编号
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 信号值
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 信号描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 信号描述
        /// </summary>
        public int State { get; set; }
    }
}