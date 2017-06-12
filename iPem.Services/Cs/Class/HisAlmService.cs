using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisAlmService : IHisAlmService {

        #region Fields

        private readonly IA_HAlarmRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisAlmService(
            IA_HAlarmRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<A_HAlarm> GetAlmsInArea(string area, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAlmsInAreaAsList(area, start, end), pageIndex, pageSize);
        }

        public List<A_HAlarm> GetAlmsInAreaAsList(string area, DateTime start, DateTime end) {
            return _hisRepository.GetAlarmsInArea(area, start, end);
        }

        public IPagedList<A_HAlarm> GetAlmsInStation(string station, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAlmsInStationAsList(station, start, end), pageIndex, pageSize);
        }

        public List<A_HAlarm> GetAlmsInStationAsList(string station, DateTime start, DateTime end) {
            return _hisRepository.GetAlarmsInStation(station, start, end);
        }

        public IPagedList<A_HAlarm> GetAlmsInRoom(string room, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAlmsInRoomAsList(room, start, end), pageIndex, pageSize);
        }

        public List<A_HAlarm> GetAlmsInRoomAsList(string room, DateTime start, DateTime end) {
            return _hisRepository.GetAlarmsInRoom(room, start, end);
        }

        public IPagedList<A_HAlarm> GetAlmsInDevice(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAlmsInDeviceAsList(device, start, end), pageIndex, pageSize);
        }

        public List<A_HAlarm> GetAlmsInDeviceAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetAlarmsInDevice(device, start, end);
        }

        public IPagedList<A_HAlarm> GetAlmsInPoint(string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAlmsInPointAsList(point, start, end), pageIndex, pageSize);
        }

        public List<A_HAlarm> GetAlmsInPointAsList(string point, DateTime start, DateTime end) {
            return _hisRepository.GetAlarmsInPoint(point, start, end);
        }

        public IPagedList<A_HAlarm> GetAllAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAllAlmsAsList(start, end), pageIndex, pageSize);
        }

        public List<A_HAlarm> GetAllAlmsAsList(DateTime start, DateTime end) {
            return _hisRepository.GetAlarms(start, end);
        }

        #endregion

    }
}
