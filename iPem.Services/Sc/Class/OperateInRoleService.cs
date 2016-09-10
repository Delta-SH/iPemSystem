using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class OperateInRoleService : IOperateInRoleService {

        #region Fields

        private readonly IOperateInRoleRepository _operateRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public OperateInRoleService(
            IOperateInRoleRepository operateRepository,
            ICacheManager cacheManager) {
            this._operateRepository = operateRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public OperateInRole GetOperates(Guid role) {
            if(role.Equals(Role.SuperId)) {
                var operations = new OperateInRole() {
                    RoleId = role,
                    Operates = new List<EnmOperation>()
                };

                foreach(EnmOperation operation in Enum.GetValues(typeof(EnmOperation))) {
                    operations.Operates.Add(operation);
                }

                return operations;
            }

            var key = string.Format(GlobalCacheKeys.Rl_OperationsResultPattern, role);
            if(_cacheManager.IsSet(key))
                return _cacheManager.Get<OperateInRole>(key);

            var result = _operateRepository.GetEntity(role);
            _cacheManager.Set<OperateInRole>(key, result, CachedIntervals.Global_Intervals);

            return result;
        }

        public void Add(OperateInRole operation) {
            if(operation == null)
                throw new ArgumentException("operation");

            var key = string.Format(GlobalCacheKeys.Rl_OperationsResultPattern, operation.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _operateRepository.Insert(operation);
        }

        public void Remove(Guid role) {
            var key = string.Format(GlobalCacheKeys.Rl_OperationsResultPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _operateRepository.Delete(role);
        }

        #endregion

    }
}
