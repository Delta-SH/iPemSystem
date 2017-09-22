using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 摄像机通道信息表
    /// </summary>
    [Serializable]
    public partial class V_Channel : BaseEntity {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 码流
        /// </summary>
        public int Mask { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 零通道
        /// </summary>
        public bool Zero { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 所属摄像机
        /// </summary>
        public string CameraId { get; set; }
    }
}
