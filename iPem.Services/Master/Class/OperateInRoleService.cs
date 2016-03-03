using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// Operate in role service
    /// </summary>
    public partial class OperateInRoleService : IOperateInRoleService {

        #region Fields

        private readonly IOperateInRoleRepository _operateInRoleRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public OperateInRoleService(
            IOperateInRoleRepository operateInRoleRepository,
            ICacheManager cacheManager) {
            this._operateInRoleRepository = operateInRoleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public OperateInRole GetOperateInRole(Guid id) {
            return _operateInRoleRepository.GetEntity(id);
        }

        public void AddOperateInRole(OperateInRole operation) {
            if(operation == null)
                throw new ArgumentException("menus");

            _operateInRoleRepository.Insert(operation);
        }

        public void DeleteOperateInRole(Guid id) {
            _operateInRoleRepository.Delete(id);
        }

        #endregion

    }
}
