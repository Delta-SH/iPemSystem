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

        private readonly IM_FormulaRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FormulaService(
            IM_FormulaRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public M_Formula GetFormula(string id, EnmSSH type, EnmFormula formulaType) {
            return _repository.GetFormula(id, type, formulaType);
        }

        public List<M_Formula> GetFormulas(string id, EnmSSH type) {
            return _repository.GetFormulas(id, type);
        }

        public List<M_Formula> GetAllFormulas() {
            return _repository.GetAllFormulas();
        }

        public IPagedList<M_Formula> GetPagedFormulas(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Formula>(this.GetAllFormulas(), pageIndex, pageSize);
        }

        public void Save(params M_Formula[] formulas) {
            if (formulas == null || formulas.Length == 0)
                throw new ArgumentNullException("formulas");

            _repository.Save(formulas);
        }

        #endregion

    }
}
