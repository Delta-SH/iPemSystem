using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisElecService {

        IPagedList<HisElec> GetEnergies(string id, EnmOrganization type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisElec> GetEnergiesAsList(string id, EnmOrganization type, DateTime start, DateTime end);

        IPagedList<HisElec> GetEnergies(string id, EnmOrganization type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisElec> GetEnergiesAsList(string id, EnmOrganization type, EnmFormula formula, DateTime start, DateTime end);

        IPagedList<HisElec> GetEnergies(EnmOrganization type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisElec> GetEnergiesAsList(EnmOrganization type, EnmFormula formula, DateTime start, DateTime end);

        IPagedList<HisElec> GetEnergies(EnmOrganization type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisElec> GetEnergiesAsList(EnmOrganization type, DateTime start, DateTime end);

        IPagedList<HisElec> GetEnergies(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisElec> GetEnergiesAsList(DateTime start, DateTime end);

    }
}
