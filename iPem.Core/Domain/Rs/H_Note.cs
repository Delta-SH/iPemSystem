using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 配置同步表
    /// </summary>
    [Serializable]
    public partial class H_Note : BaseEntity {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 本系统所用标识为2
        /// </summary>
        public int SysType { get; set; }

        /// <summary>
        /// 采集组编号
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        /// 需要同步的表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表的类型
        /// </summary>
        public int DtType { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int OpType { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }
    }
}
