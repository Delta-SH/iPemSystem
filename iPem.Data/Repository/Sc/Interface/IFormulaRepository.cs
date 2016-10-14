using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IFormulaRepository {

        Formula GetEntity(string id, EnmOrganization type, EnmFormula formulaType);

        List<Formula> GetEntities(string id, EnmOrganization type);

        List<Formula> GetEntities();

        void Save(Formula entity);

        void Save(List<Formula> entities);

    }
}
