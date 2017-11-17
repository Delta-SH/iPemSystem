using System;

namespace iPem.Site.Models.API {
    public class API_GV_SCValue {
        /// <summary>
        /// 对象编码
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 监测值
        /// 区域、站点、机房、设备对象为告警等级数值(0、1、2、3、4） 
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 监测值描述
        /// 区域、站点、机房、设备对象为告警等级描述(正常、一级、二级、三级、四级；正常、紧急、重要、一般、提示)
        /// 状态信号为值状态描述
        /// 模拟信号为值单位
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 监测值对应状态
        /// </summary>
        public int State { get; set; }
    }
}