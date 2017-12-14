using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class MaskingService : IMaskingService {

        #region Fields

        private readonly IH_MaskingRepository _repository;
        private readonly ID_DeviceRepository _devRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MaskingService(
            IH_MaskingRepository repository,
            ID_DeviceRepository devRepository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._devRepository = devRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_Masking> GetMaskings() {
            var key = GlobalCacheKeys.Rs_MaskingRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<List<H_Masking>>(key);
            } else {
                var data = _repository.GetEntities();
                _cacheManager.Set(key, data);
                return data;
            }
        }

        public HashSet<string> GetHashMaskings() {
            var key = GlobalCacheKeys.Rs_HashMaskingRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<HashSet<string>>(key);
            } else {
                var data = new HashSet<string>();
                foreach (var mask in this.GetMaskings()) {
                    if (mask.Type == EnmMaskType.Station) {
                        foreach (var device in _devRepository.GetDevicesInStation(mask.Id)) {
                            data.Add(CommonHelper.JoinKeys(device.Id, "masking-all"));
                        }
                    } else if (mask.Type == EnmMaskType.Room) {
                        foreach (var device in _devRepository.GetDevicesInRoom(mask.Id)) {
                            data.Add(CommonHelper.JoinKeys(device.Id, "masking-all"));
                        }
                    } else if (mask.Type == EnmMaskType.Device) {
                        data.Add(CommonHelper.JoinKeys(mask.Id, "masking-all"));
                    } else if (mask.Type == EnmMaskType.Point) {
                        var ids = CommonHelper.SplitCondition(mask.Id);
                        if (ids.Length == 2 && !data.Contains(CommonHelper.JoinKeys(ids[0], "masking-all")))
                            data.Add(CommonHelper.JoinKeys(ids[0], ids[1]));
                    }
                }

                _cacheManager.Set(key, data);
                return data;
            }
        }

        public IPagedList<H_Masking> GetPagedMaskings(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_Masking>(this.GetMaskings(), pageIndex, pageSize);
        }

        #endregion

    }
}
