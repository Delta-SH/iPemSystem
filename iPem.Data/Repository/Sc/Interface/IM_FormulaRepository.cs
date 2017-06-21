using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 能耗公式表
    /// </summary>
    public partial interface IM_FormulaRepository {
        /// <summary>
        /// 获得指定的公式
        /// </summary>
        M_Formula GetFormula(string id, EnmSSH type, EnmFormula formulaType);

        /// <summary>
        /// 获得指定的公式
        /// </summary>
        List<M_Formula> GetFormulas(string id, EnmSSH type);

        /// <summary>
        /// 获得所有的公式
        /// </summary>
        List<M_Formula> GetAllFormulas();

        /// <summary>
        /// 保存能耗公式
        /// </summary>
        void Save(IList<M_Formula> entities);
    }
}
