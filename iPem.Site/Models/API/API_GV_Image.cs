using System;

namespace iPem.Site.Models.API {
    public class API_GV_Image {
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

        /// <summary>
        /// 原图或缩略图内容
        /// 缩略图固定为32x32的png格式
        /// </summary>
        public string Content { get; set; }
    }
}