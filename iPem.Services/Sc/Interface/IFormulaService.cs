using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IFormulaService {

        Formula GetFormula(string id, EnmOrganization type, EnmFormula formulaType);

        IPagedList<Formula> GetFormulas(string id, EnmOrganization type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Formula> GetFormulasAsList(string id, EnmOrganization type);

        IPagedList<Formula> GetAllFormulas(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Formula> GetAllFormulasAsList();

        void Save(Formula formula);

        void SaveRange(List<Formula> formulas);

    }
}
