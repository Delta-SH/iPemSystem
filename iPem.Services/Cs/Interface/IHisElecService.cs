using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisElecService {

        IPagedList<V_Elec> GetEnergies(string id, EnmSSH type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Elec> GetEnergiesAsList(string id, EnmSSH type, DateTime start, DateTime end);

        IPagedList<V_Elec> GetEnergies(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Elec> GetEnergiesAsList(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        IPagedList<V_Elec> GetEnergies(EnmSSH type, EnmFormula formula, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Elec> GetEnergiesAsList(EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        IPagedList<V_Elec> GetEnergies(EnmSSH type, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Elec> GetEnergiesAsList(EnmSSH type, DateTime start, DateTime end);

        IPagedList<V_Elec> GetEnergies(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Elec> GetEnergiesAsList(DateTime start, DateTime end);

    }
}
