using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 能耗公式API
    /// </summary>
    public partial interface IFormulaService {
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
        /// 获得所有的公式(分页)
        /// </summary>
        IPagedList<M_Formula> GetPagedFormulas(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 保存能耗公式
        /// </summary>
        void Save(params M_Formula[] formulas);
    }
}
