using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class FormulaService : IFormulaService {

        #region Fields

        private readonly IFormulaRepository _formulaRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FormulaService(
            IFormulaRepository formulaRepository,
            ICacheManager cacheManager) {
            this._formulaRepository = formulaRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public virtual Formula GetFormula(string id, EnmOrganization type, EnmFormula formulaType) {
            return _formulaRepository.GetEntity(id, type, formulaType);
        }

        public virtual IPagedList<Formula> GetFormulas(string id, EnmOrganization type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Formula>(this.GetFormulasAsList(id, type), pageIndex, pageSize);
        }

        public virtual List<Formula> GetFormulasAsList(string id, EnmOrganization type) {
            return _formulaRepository.GetEntities(id, type);
        }

        public virtual IPagedList<Formula> GetAllFormulas(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Formula>(this.GetAllFormulasAsList(), pageIndex, pageSize);
        }

        public virtual List<Formula> GetAllFormulasAsList() {
            return _formulaRepository.GetEntities();
        }

        public virtual void Save(Formula formula) {
            if(formula == null)
                throw new ArgumentNullException("formula");

            _formulaRepository.Save(formula);
        }

        public virtual void SaveRange(List<Formula> formulas) {
            if(formulas == null)
                throw new ArgumentNullException("formulas");

            _formulaRepository.Save(formulas);
        }

        #endregion

    }
}
