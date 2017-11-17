using System;

namespace iPem.Site.Models.API {
    public class API_GV_OpSignal {
        /// <summary>
        /// 信号编码（设备+信号）
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 操作值
        /// </summary>
        public double Value { get; set; }
    }
}