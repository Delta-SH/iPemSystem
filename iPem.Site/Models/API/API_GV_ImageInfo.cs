using System;

namespace iPem.Site.Models.API {
    public class API_GV_ImageInfo {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片类型
        /// 0:Png,1:Xaml
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 图片更新标识
        /// </summary>
        public string UpdateMark { get; set; }
    }
}