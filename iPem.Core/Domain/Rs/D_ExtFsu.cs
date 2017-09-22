using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Fsu扩展信息
    /// </summary>
    [Serializable]
    public partial class D_ExtFsu : BaseEntity {
        /// <summary>
        /// Fsu编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// SC组编号
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Fsu IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Fsu端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Fsu注册时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// Fsu离线时间
        /// </summary>
        public DateTime LastTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态(在线、离线)
        /// </summary>
        public bool Status { get; set; }
    }
}
