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

        public virtual M_Formula GetFormula(string id, EnmSSH type, EnmFormula formulaType) {
            return _formulaRepository.GetEntity(id, type, formulaType);
        }

        public virtual IPagedList<M_Formula> GetFormulas(string id, EnmSSH type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Formula>(this.GetFormulasAsList(id, type), pageIndex, pageSize);
        }

        public virtual List<M_Formula> GetFormulasAsList(string id, EnmSSH type) {
            return _formulaRepository.GetEntities(id, type);
        }

        public virtual IPagedList<M_Formula> GetAllFormulas(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Formula>(this.GetAllFormulasAsList(), pageIndex, pageSize);
        }

        public virtual List<M_Formula> GetAllFormulasAsList() {
            return _formulaRepository.GetEntities();
        }

        public virtual void Save(M_Formula formula) {
            if(formula == null)
                throw new ArgumentNullException("formula");

            _formulaRepository.Save(formula);
        }

        public virtual void SaveRange(List<M_Formula> formulas) {
            if(formulas == null)
                throw new ArgumentNullException("formulas");

            _formulaRepository.Save(formulas);
        }

        #endregion

    }
}
