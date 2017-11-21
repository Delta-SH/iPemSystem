using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPem.Site.Infrastructure {
    public class ApiWorkContext : IApiWorkContext {

        #region Fields

        /// <summary>
        /// 公共API
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 资源数据API
        /// </summary>
        private readonly IAreaService _areaService;
        private readonly IStationService _stationService;
        private readonly IStationTypeService _stationTypeService;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IDeviceService _deviceService;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IFsuService _fsuService;
        private readonly IPointService _pointService;
        private readonly ILogicTypeService _logicTypeService;
        private readonly ISCVendorService _vendorService;
        private readonly IGroupService _groupService;

        /// <summary>
        /// 历史数据API
        /// </summary>
        private readonly IAAlarmService _aalarmService;
        private readonly IHAlarmService _halarmService;
        private readonly IAMeasureService _ameasureService;
        private readonly IHMeasureService _hmeasureService;

        /// <summary>
        /// 应用数据API
        /// </summary>
        private readonly IRoleService _roleService;
        private readonly IEntitiesInRoleService _authService;
        private readonly IUserService _userService;

        /// <summary>
        /// 全局变量
        /// </summary>
        private U_Role _cachedRole;
        private U_User _cachedUser;
        private U_EntitiesInRole _cachedAuth;
        private List<C_StationType> _cachedStationTypes;
        private List<C_RoomType> _cachedRoomTypes;
        private List<C_DeviceType> _cachedDeviceTypes;
        private List<C_SubDeviceType> _cachedSubDeviceTypes;
        private List<C_LogicType> _cachedLogicTypes;
        private List<C_SubLogicType> _cachedSubLogicTypes;
        private List<C_SCVendor> _cachedVendors;
        private List<SSHArea> _cachedAllAreas;
        private List<SSHArea> _cachedAreas;
        private List<S_Station> _cachedAllStations;
        private List<S_Station> _cachedStations;
        private List<S_Room> _cachedAllRooms;
        private List<S_Room> _cachedRooms;
        private List<D_Fsu> _cachedAllFsus;
        private List<D_Fsu> _cachedFsus;
        private List<D_Device> _cachedAllDevices;
        private List<D_Device> _cachedDevices;
        private HashSet<string> _cachedDeviceKeys;
        private List<P_Point> _cachedPoints;
        private List<P_SubPoint> _cachedSubPoints;
        private List<P_Point> _cachedALPoints;
        private List<AlmStore<A_AAlarm>> _cachedActAlarms;
        private List<AlmStore<A_AAlarm>> _cachedAllAlarms;
        private List<C_Group> _cachedGroups;

        #endregion

        #region Ctor

        public ApiWorkContext(
            ICacheManager cacheManager,
            IAreaService areaService,
            IStationService stationService,
            IStationTypeService stationTypeService,
            IRoomService roomService,
            IRoomTypeService roomTypeService,
            IDeviceService deviceService,
            IDeviceTypeService deviceTypeService,
            IFsuService fsuService,
            IPointService pointService,
            ILogicTypeService logicTypeService,
            ISCVendorService vendorService,
            IAAlarmService aalarmService,
            IHAlarmService halarmService,
            IAMeasureService ameasureService,
            IHMeasureService hmeasureService,
            IRoleService roleService,
            IEntitiesInRoleService authService,
            IUserService userService,
            IGroupService groupService) {
            this._cacheManager = cacheManager;
            this._areaService = areaService;
            this._stationService = stationService;
            this._stationTypeService = stationTypeService;
            this._roomService = roomService;
            this._roomTypeService = roomTypeService;
            this._deviceService = deviceService;
            this._deviceTypeService = deviceTypeService;
            this._fsuService = fsuService;
            this._pointService = pointService;
            this._logicTypeService = logicTypeService;
            this._vendorService = vendorService;
            this._aalarmService = aalarmService;
            this._halarmService = halarmService;
            this._ameasureService = ameasureService;
            this._hmeasureService = hmeasureService;
            this._roleService = roleService;
            this._authService = authService;
            this._userService = userService;
            this._groupService = groupService;
        }

        #endregion

        #region Methods

        public U_Role GetRole(string id) {
            if (string.IsNullOrWhiteSpace(id)) 
                throw new ArgumentNullException("id");

            if (_cachedRole != null)
                return _cachedRole;

            var role = _roleService.GetRoleById(new Guid(id));
            if (role == null) throw new iPemException("current role not found.");
            return _cachedRole = role;
        }

        public U_User GetUser(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            if (_cachedUser != null)
                return _cachedUser;

            var user = _userService.GetUserByName(name);
            if (user == null) throw new iPemException("current user not found.");
            return _cachedUser = user;
        }

        public EnmLoginResults Login(string name, string password) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            return _userService.Validate(name, password);
        }

        public U_EntitiesInRole GetAuth(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (_cachedAuth != null)
                return _cachedAuth;

            var auth = _authService.GetEntitiesInRole(new Guid(id));
            if (auth == null) throw new iPemException("current auth not found.");
            return _cachedAuth = auth;
        }

        public List<C_StationType> GetStationTypes() {
            if (_cachedStationTypes != null)
                return _cachedStationTypes;

            return _cachedStationTypes = _stationTypeService.GetStationTypes();
        }

        public List<C_RoomType> GetRoomTypes() {
            if (_cachedRoomTypes != null)
                return _cachedRoomTypes;

            return _cachedRoomTypes = _roomTypeService.GetRoomTypes();
        }

        public List<C_DeviceType> GetDeviceTypes() {
            if (_cachedDeviceTypes != null)
                return _cachedDeviceTypes;

            return _cachedDeviceTypes = _deviceTypeService.GetDeviceTypes();
        }

        public List<C_SubDeviceType> GetSubDeviceTypes() {
            if (_cachedSubDeviceTypes != null)
                return _cachedSubDeviceTypes;

            return _cachedSubDeviceTypes = _deviceTypeService.GetSubDeviceTypes();
        }

        public List<C_LogicType> GetLogicTypes() {
            if (_cachedLogicTypes != null)
                return _cachedLogicTypes;

            return _cachedLogicTypes = _logicTypeService.GetLogicTypes();
        }

        public List<C_SubLogicType> GetSubLogicTypes() {
            if (_cachedSubLogicTypes != null)
                return _cachedSubLogicTypes;

            return _cachedSubLogicTypes = _logicTypeService.GetSubLogicTypes();
        }

        public List<C_SCVendor> GetVendors() {
            if (_cachedVendors != null)
                return _cachedVendors;

            return _cachedVendors = _vendorService.GetVendors();
        }

        public List<SSHArea> GetAllAreas() {
            if (_cachedAllAreas != null)
                return _cachedAllAreas;

            if (_cacheManager.IsSet(GlobalCacheKeys.SSH_Areas))
                return _cachedAllAreas = _cacheManager.Get<List<SSHArea>>(GlobalCacheKeys.SSH_Areas);

            _cachedAllAreas = new List<SSHArea>();
            foreach (var _current in _areaService.GetAreas()) {
                _cachedAllAreas.Add(new SSHArea { Current = _current });
            }

            foreach (var current in _cachedAllAreas) {
                current.Initializer(_cachedAllAreas);
            }

            _cacheManager.Set(GlobalCacheKeys.SSH_Areas, _cachedAllAreas);
            return _cachedAllAreas;
        }

        public List<SSHArea> GetAreas(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (U_Role.SuperId.Equals(new Guid(id)))
                return this.GetAllAreas();

            if (_cachedAreas != null)
                return _cachedAreas;

            var cachedKey = string.Format(GlobalCacheKeys.SSH_AreasPattern, id);
            if (_cacheManager.IsSet(cachedKey))
                return _cachedAreas = _cacheManager.Get<List<SSHArea>>(cachedKey);

            _cachedAreas = new List<SSHArea>();
            var auths = this.GetAuth(id).Areas;
            if (auths.Count == 0) return _cachedAreas;

            foreach (var _current in _areaService.GetAreas()) {
                if (!auths.Contains(_current.Id)) continue;
                _cachedAreas.Add(new SSHArea { Current = _current });
            }

            foreach (var _current in _cachedAreas) {
                _current.Initializer(_cachedAreas);
            }

            _cacheManager.Set(cachedKey, _cachedAreas);
            return _cachedAreas;
        }

        public List<S_Station> GetAllStations() {
            if (_cachedAllStations != null)
                return _cachedAllStations;

            return _cachedAllStations = _stationService.GetStations();
        }

        public List<S_Station> GetStations(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (U_Role.SuperId.Equals(new Guid(id)))
                return this.GetAllStations();

            if (_cachedStations != null)
                return _cachedStations;

            _cachedStations = new List<S_Station>();
            var auth = this.GetAuth(id);
            if (auth.Areas.Count > 0) {
                var keys = new HashSet<string>(auth.Areas);
                foreach (var _current in _stationService.GetStations()) {
                    if (!keys.Contains(_current.AreaId)) continue;
                    _cachedStations.Add(_current);
                }
            }

            return _cachedStations;
        }

        public List<S_Room> GetAllRooms() {
            if (_cachedAllRooms != null)
                return _cachedAllRooms;

            return _cachedAllRooms = _roomService.GetRooms();
        }

        public List<S_Room> GetRooms(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (U_Role.SuperId.Equals(new Guid(id)))
                return this.GetAllRooms();

            if (_cachedRooms != null)
                return _cachedRooms;

            _cachedRooms = new List<S_Room>();
            var auth = this.GetAuth(id);
            if (auth.Areas.Count > 0) {
                var keys = new HashSet<string>(auth.Areas);
                foreach (var _current in _roomService.GetRooms()) {
                    if (!keys.Contains(_current.AreaId)) continue;
                    _cachedRooms.Add(_current);
                }
            }

            return _cachedRooms;
        }

        public List<D_Fsu> GetAllFsus() {
            if (_cachedAllFsus != null)
                return _cachedAllFsus;

            return _cachedAllFsus = _fsuService.GetFsus();
        }

        public List<D_Fsu> GetFsus(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (U_Role.SuperId.Equals(new Guid(id)))
                return this.GetAllFsus();

            if (_cachedFsus != null)
                return _cachedFsus;

            _cachedFsus = new List<D_Fsu>();
            var auth = this.GetAuth(id);
            if (auth.Areas.Count > 0) {
                var keys = new HashSet<string>(auth.Areas);
                foreach (var _current in _fsuService.GetFsus()) {
                    if (!keys.Contains(_current.AreaId)) continue;
                    _cachedFsus.Add(_current);
                }
            }

            return _cachedFsus;
        }

        public List<D_Device> GetAllDevices() {
            if (_cachedAllDevices != null)
                return _cachedAllDevices;

            return _cachedAllDevices = _deviceService.GetDevices();
        }

        public List<D_Device> GetDevices(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (U_Role.SuperId.Equals(new Guid(id)))
                return this.GetAllDevices();

            if (_cachedDevices != null)
                return _cachedDevices;

            _cachedDevices = new List<D_Device>();
            var auth = this.GetAuth(id);
            if (auth.Areas.Count > 0) {
                var keys = new HashSet<string>(auth.Areas);
                foreach (var _current in _deviceService.GetDevices()) {
                    if (!keys.Contains(_current.AreaId)) continue;
                    _cachedDevices.Add(_current);
                }
            }

            return _cachedDevices;
        }

        public HashSet<string> GetDeviceKeys(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (_cachedDeviceKeys != null)
                return _cachedDeviceKeys;

            _cachedDeviceKeys = new HashSet<string>();
            foreach (var _current in this.GetDevices(id)) {
                _cachedDeviceKeys.Add(_current.Id);
            }

            return _cachedDeviceKeys;
        }

        public List<P_Point> GetPoints() {
            if (_cachedPoints != null)
                return _cachedPoints;

            return _cachedPoints = _pointService.GetPoints();
        }

        public List<P_SubPoint> GetSubPoints() {
            if (_cachedSubPoints != null)
                return _cachedSubPoints;

            return _cachedSubPoints = _pointService.GetSubPoints();
        }

        public List<P_Point> GetALPoints() {
            if (_cachedALPoints != null)
                return _cachedALPoints;

            return _cachedALPoints = this.GetPoints().FindAll(p => p.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(p.AlarmId));
        }

        public EnmPoint GetPointType(P_Point point) {
            if (point.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(point.AlarmId))
                return EnmPoint.AL;

            return point.Type;
        }

        public List<C_Group> GetGroups() {
            if (_cachedGroups != null)
                return _cachedGroups;

            return _cachedGroups = _groupService.GetGroups();
        }

        public List<AlmStore<A_AAlarm>> ActAlarms(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (_cachedActAlarms != null)
                return _cachedActAlarms;

            var _allAlarms = this.AllAlarms();
            if (_allAlarms.Count > 0 && !U_Role.SuperId.Equals(new Guid(id))) {
                var keys = this.GetAuth(id).Areas;
                _allAlarms = _allAlarms.FindAll(a => a.Current.RoomId == "-1" || keys.Contains(a.Current.AreaId));
            }

            return _cachedActAlarms = _allAlarms;
        }

        public List<AlmStore<A_AAlarm>> AllAlarms() {
            if (_cachedAllAlarms != null)
                return _cachedAllAlarms;

            //实时告警缓存10s，减小服务器压力
            List<A_AAlarm> _allAlarms = null;
            IEnumerable<AlmStore<A_AAlarm>> _activeAlarms = null;
            IEnumerable<AlmStore<A_AAlarm>> _systemAlarms = null;

            if (_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms)) {
                _activeAlarms = _cacheManager.Get<IEnumerable<AlmStore<A_AAlarm>>>(GlobalCacheKeys.Active_Alarms);
            } else {
                if (_allAlarms == null) _allAlarms = _aalarmService.GetAlarms();
                var nomAlarms = _allAlarms.FindAll(a => a.RoomId != "-1");
                if (nomAlarms.Count > 0) {
                    _activeAlarms = from alarm in nomAlarms
                                    join point in this.GetALPoints() on alarm.PointId equals point.Id
                                    join device in this.GetAllDevices() on alarm.DeviceId equals device.Id
                                    join room in this.GetAllRooms() on device.RoomId equals room.Id
                                    join station in this.GetAllStations() on room.StationId equals station.Id
                                    join area in this.GetAllAreas() on station.AreaId equals area.Current.Id
                                    select new AlmStore<A_AAlarm> {
                                        Current = alarm,
                                        PointName = point.Name,
                                        DeviceName = device.Name,
                                        DeviceTypeId = device.Type.Id,
                                        SubDeviceTypeId = device.SubType.Id,
                                        SubLogicTypeId = device.SubLogicType.Id,
                                        RoomName = room.Name,
                                        RoomTypeId = room.Type.Id,
                                        StationName = station.Name,
                                        StationTypeId = station.Type.Id,
                                        AreaName = area.Current.Name,
                                        AreaFullName = area.ToString()
                                    };
                }

                _cacheManager.Set(GlobalCacheKeys.Active_Alarms, _activeAlarms, GlobalCacheInterval.ActAlarm_Interval);
            }

            if (_cacheManager.IsSet(GlobalCacheKeys.System_Alarms)) {
                _systemAlarms = _cacheManager.Get<IEnumerable<AlmStore<A_AAlarm>>>(GlobalCacheKeys.System_Alarms);
            } else {
                if (_allAlarms == null) _allAlarms = _aalarmService.GetAlarms();
                var sysAlarms = _allAlarms.FindAll(a => a.RoomId == "-1");
                if (sysAlarms.Count > 0) {
                    _systemAlarms = from alarm in sysAlarms
                                    join point in this.GetALPoints() on alarm.PointId equals point.Id
                                    join gp in this.GetGroups() on alarm.DeviceId equals gp.Id
                                    select new AlmStore<A_AAlarm> {
                                        Current = alarm,
                                        PointName = point.Name,
                                        DeviceName = gp.Name,
                                        DeviceTypeId = SSHSystem.SC.Type.Id,
                                        SubDeviceTypeId = SSHSystem.SC.SubType.Id,
                                        SubLogicTypeId = SSHSystem.SC.SubLogicType.Id,
                                        RoomName = SSHSystem.Room.Name,
                                        RoomTypeId = SSHSystem.Room.Type.Id,
                                        StationName = SSHSystem.Station.Name,
                                        StationTypeId = SSHSystem.Station.Type.Id,
                                        AreaName = SSHSystem.Area.Name,
                                        AreaFullName = SSHSystem.Area.Name
                                    };
                }

                _cacheManager.Set(GlobalCacheKeys.System_Alarms, _systemAlarms, GlobalCacheInterval.ActAlarm_Interval);
            }

            if (_activeAlarms == null) _activeAlarms = new List<AlmStore<A_AAlarm>>();
            if (_systemAlarms == null) _systemAlarms = new List<AlmStore<A_AAlarm>>();
            return _cachedAllAlarms = _activeAlarms.Concat(_systemAlarms).OrderByDescending(a => a.Current.AlarmTime).ToList();
        }

        public List<AlmStore<A_AAlarm>> AlarmsToStore(string id, List<A_AAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_AAlarm>>();

            return (from alarm in alarms
                    join point in this.GetALPoints() on alarm.PointId equals point.Id
                    join device in this.GetDevices(id) on alarm.DeviceId equals device.Id
                    join room in this.GetRooms(id) on device.RoomId equals room.Id
                    join station in this.GetStations(id) on room.StationId equals station.Id
                    join area in this.GetAreas(id) on station.AreaId equals area.Current.Id
                    orderby alarm.AlarmTime descending
                    select new AlmStore<A_AAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        DeviceName = device.Name,
                        DeviceTypeId = device.Type.Id,
                        SubDeviceTypeId = device.SubType.Id,
                        SubLogicTypeId = device.SubLogicType.Id,
                        RoomName = room.Name,
                        RoomTypeId = room.Type.Id,
                        StationName = station.Name,
                        StationTypeId = station.Type.Id,
                        AreaName = area.Current.Name,
                        AreaFullName = area.ToString()
                    }).ToList();
        }

        public List<AlmStore<A_HAlarm>> AlarmsToStore(string id, List<A_HAlarm> alarms) {
            if(alarms == null || alarms.Count == 0) 
                return new List<AlmStore<A_HAlarm>>();

            return (from alarm in alarms
                    join point in this.GetALPoints() on alarm.PointId equals point.Id
                    join device in this.GetDevices(id) on alarm.DeviceId equals device.Id
                    join room in this.GetRooms(id) on device.RoomId equals room.Id
                    join station in this.GetStations(id) on room.StationId equals station.Id
                    join area in this.GetAreas(id) on station.AreaId equals area.Current.Id
                    orderby alarm.StartTime descending
                    select new AlmStore<A_HAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        DeviceName = device.Name,
                        DeviceTypeId = device.Type.Id,
                        SubDeviceTypeId = device.SubType.Id,
                        SubLogicTypeId = device.SubLogicType.Id,
                        RoomName = room.Name,
                        RoomTypeId = room.Type.Id,
                        StationName = station.Name,
                        StationTypeId = station.Type.Id,
                        AreaName = area.Current.Name,
                        AreaFullName = area.ToString()
                    }).ToList();
        }

        public bool IsAlarmPoint(P_Point point) {
            if (point == null) return false;
            return point.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(point.AlarmId);
        }

        #endregion

    }
}