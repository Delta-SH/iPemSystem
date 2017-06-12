using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class ActAlmService : IActAlmService {

        #region Fields

        private readonly IA_AAlarmRepository _actRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ActAlmService(
            IA_AAlarmRepository actRepository,
            ICacheManager cacheManager) {
            this._actRepository = actRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<A_AAlarm> GetAlmsInArea(string area, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlmsInAreaAsList(area), pageIndex, pageSize);
        }

        public List<A_AAlarm> GetAlmsInAreaAsList(string area) {
            return _actRepository.GetAlarmsInArea(area);
        }

        public IPagedList<A_AAlarm> GetAlmsInStation(string station, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlmsInStationAsList(station), pageIndex, pageSize);
        }

        public List<A_AAlarm> GetAlmsInStationAsList(string station) {
            return _actRepository.GetAlarmsInStation(station);
        }

        public IPagedList<A_AAlarm> GetAlmsInRoom(string room, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlmsInRoomAsList(room), pageIndex, pageSize);
        }

        public List<A_AAlarm> GetAlmsInRoomAsList(string room) {
            return _actRepository.GetAlarmsInRoom(room);
        }

        public IPagedList<A_AAlarm> GetAlmsInDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlmsInDeviceAsList(device), pageIndex, pageSize);
        }

        public List<A_AAlarm> GetAlmsInDeviceAsList(string device) {
            return _actRepository.GetAlarmsInDevice(device);
        }

        public IPagedList<A_AAlarm> GetAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlmsAsList(start, end), pageIndex, pageSize);
        }

        public List<A_AAlarm> GetAlmsAsList(DateTime start, DateTime end) {
            return _actRepository.GetAlarms(start, end);
        }

        public IPagedList<A_AAlarm> GetAllAlms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAllAlmsAsList(), pageIndex, pageSize);
        }

        public List<A_AAlarm> GetAllAlmsAsList() {
            return _actRepository.GetAlarms();
        }

        #endregion

    }
}
