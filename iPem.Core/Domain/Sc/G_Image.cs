using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// 组态图片表
    /// </summary>
    [Serializable]
    public class G_Image {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 图片内容
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public byte[] Thumbnail { get; set; }

        /// <summary>
        /// 更新标识
        /// </summary>
        public string UpdateMark { get; set; }

        /// <summary>
        /// 图片备注
        /// </summary>
        public string Remark { get; set; }
    }
}
