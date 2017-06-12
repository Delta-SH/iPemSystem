using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class PointService : IPointService {

        #region Fields

        private readonly IP_PointRepository _pointRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointService(
            IP_PointRepository pointRepository,
            ICacheManager cacheManager) {
            this._pointRepository = pointRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<P_Point> GetAllPoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Point>(this.GetAllPointsAsList(), pageIndex, pageSize);
        }

        public List<P_Point> GetAllPointsAsList() {
            return _pointRepository.GetEntities();
        }

        public IPagedList<P_Point> GetPointsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Point>(this.GetPointsInDeviceAsList(device), pageIndex, pageSize);
        }

        public List<P_Point> GetPointsInDeviceAsList(string device) {
            return _pointRepository.GetEntitiesByDevice(device);
        }

        public IPagedList<P_Point> GetPointsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Point>(this.GetPointsInDeviceAsList(device), pageIndex, pageSize);
        }

        public List<P_Point> GetPointsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do) {
            var types = new List<EnmPoint>();
            if(_ai) types.Add(EnmPoint.AI);
            if(_ao) types.Add(EnmPoint.AO);
            if(_di) types.Add(EnmPoint.DI);
            if(_do) types.Add(EnmPoint.DO);

            if(types.Count == 0) return new List<P_Point>();
            var points = _pointRepository.GetEntitiesByDevice(device);
            return points.FindAll(p => types.Contains(p.Type));
        }

        public IPagedList<P_Point> GetPointsInProtocol(string protocol, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Point>(this.GetPointsInProtocolAsList(protocol), pageIndex, pageSize);
        }

        public List<P_Point> GetPointsInProtocolAsList(string protocol) {
            return _pointRepository.GetEntitiesByProtocol(protocol);
        }

        #endregion

    }
}
