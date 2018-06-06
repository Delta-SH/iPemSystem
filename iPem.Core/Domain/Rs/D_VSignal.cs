using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 信号信息
    /// </summary>
    [Serializable]
    public partial class D_VSignal {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 信号编码
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// 信号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public EnmPoint Type { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string FormulaText { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string FormulaValue { get; set; }

        /// <summary>
        /// 单位/状态
        /// </summary>
        public string UnitState { get; set; }

        /// <summary>
        /// 保存周期
        /// </summary>
        public int SavedPeriod { get; set; }

        /// <summary>
        /// 统计周期
        /// </summary>
        public int StaticPeriod { get; set; }

        /// <summary>
        /// 信号分类
        /// </summary>
        public EnmVSignalCategory Category { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
