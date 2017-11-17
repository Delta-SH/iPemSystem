using System;
using System.Collections.Generic;

namespace iPem.Site.Models.API {
    public class API_GV_SCObj {
        /// <summary>
        /// 对象编码
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public int Type { get; set; }
    }
}