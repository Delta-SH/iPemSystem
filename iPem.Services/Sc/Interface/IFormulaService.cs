using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IFormulaService {

        M_Formula GetFormula(string id, EnmSSH type, EnmFormula formulaType);

        IPagedList<M_Formula> GetFormulas(string id, EnmSSH type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Formula> GetFormulasAsList(string id, EnmSSH type);

        IPagedList<M_Formula> GetAllFormulas(int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Formula> GetAllFormulasAsList();

        void Save(M_Formula formula);

        void SaveRange(List<M_Formula> formulas);

    }
}
