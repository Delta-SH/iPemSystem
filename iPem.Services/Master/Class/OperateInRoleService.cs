using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

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
            if(id.Equals(Role.SuperId)) {
                var operations = new OperateInRole() {
                    RoleId = id,
                    OperateIds = new List<EnmOperation>()
                };

                foreach(EnmOperation operation in Enum.GetValues(typeof(EnmOperation))) {
                    operations.OperateIds.Add(operation);
                }

                return operations;
            }

            return _operateInRoleRepository.GetEntity(id);
        }

        public void AddOperateInRole(OperateInRole operation) {
            if(operation == null)
                throw new ArgumentException("menus");

            var key = string.Format(GlobalCacheKeys.Rl_OperationsResultPattern, operation.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _operateInRoleRepository.Insert(operation);
        }

        public void DeleteOperateInRole(Guid id) {
            var key = string.Format(GlobalCacheKeys.Rl_OperationsResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _operateInRoleRepository.Delete(id);
        }

        #endregion

    }
}
