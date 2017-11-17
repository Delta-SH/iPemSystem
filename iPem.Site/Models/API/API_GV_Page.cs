using System;

namespace iPem.Site.Models.API {
    public class API_GV_Page {
        /// <summary>
        /// 组态名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对象ID
        /// </summary>
        public string SCObjID { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public int SCObjType { get; set; }

        /// <summary>
        /// 是否为监控对象组态页面集里的首页
        /// </summary>
        public bool IsHome { get; set; }

        /// <summary>
        /// 组态配置
        /// </summary>
        public string Content { get; set; }
    }
}