using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IFormulaRepository {

        M_Formula GetEntity(string id, EnmSSH type, EnmFormula formulaType);

        List<M_Formula> GetEntities(string id, EnmSSH type);

        List<M_Formula> GetEntities();

        void Save(M_Formula entity);

        void Save(List<M_Formula> entities);

    }
}
