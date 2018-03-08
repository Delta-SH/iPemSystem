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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// Work context for the application
    /// </summary>
    public class iPemWorkContext : IWorkContext {

        #region Fields

        /// <summary>
        /// 公共API
        /// </summary>
        private readonly HttpContextBase _httpContext;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 资源数据API
        /// </summary>
        private readonly IAreaService _areaService;
        private readonly ICameraService _cameraService;
        private readonly IDepartmentService _departmentService;
        private readonly IDeviceService _deviceService;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IEmployeeService _employeeService;
        private readonly IEnumMethodService _enumMethodService;
        private readonly IFsuService _fsuService;
        private readonly IGroupService _groupService;
        private readonly ILogicTypeService _logicTypeService;
        private readonly IMaskingService _maskingService;
        private readonly IPointService _pointService;
        private readonly IProtocolService _protocolService;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly ISCVendorService _scVendorService;
        private readonly ISignalService _signalService;
        private readonly IStationService _stationService;
        private readonly IStationTypeService _stationTypeService;

        /// <summary>
        /// 历史数据API
        /// </summary>
        private readonly IAAlarmService _actAlarmService;
        private readonly IHIDeviceService _iDeviceService;
        private readonly IHIStationService _iStationService;
        private readonly IHIAreaService _iAreaService;

        /// <summary>
        /// 应用数据API
        /// </summary>
        private readonly IDictionaryService _dictionaryService;
        private readonly IEntitiesInRoleService _entitiesInRoleService;
        private readonly IProfileService _profileService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        /// <summary>
        /// 全局变量（用户相关）
        /// </summary>
        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private U_Role _cachedRole;
        private U_User _cachedUser;
        private U_Employee _cachedEmployee;
        private iAuth _cachedAuths;
        private WsValues _cachedWsValues;
        private TsValues _cachedTsValues;
        private RtValues _cachedRtValues;
        private IEnumerable<FollowPoint> _cachedProfileFollows;
        private IEnumerable<SeniorCondition> _cachedProfileConditions;
        private IEnumerable<MatrixTemplate> _cachedProfileMatrixs;

        /// <summary>
        /// 全局变量（配置相关）
        /// </summary>
        private List<C_LogicType> _cachedLogicTypes;
        private List<C_SubLogicType> _cachedSubLogicTypes;
        private List<C_Department> _cachedDepartments;
        private List<C_DeviceType> _cachedDeviceTypes;
        private List<C_SubDeviceType> _cachedSubDeviceTypes;
        private List<C_RoomType> _cachedRoomTypes;
        private List<C_StationType> _cachedStationTypes;
        private List<C_EnumMethod> _cachedAreaTypes;
        private List<C_SCVendor> _cachedVendors;
        private List<SSHDevice> _cachedAllDevices;
        private List<SSHCamera> _cachedAllCameras;
        private List<SSHFsu> _cachedAllFsus;
        private List<SSHRoom> _cachedAllRooms;
        private List<SSHStation> _cachedAllStations;
        private List<SSHArea> _cachedAllAreas;
        private List<SSHArea> _cachedAreas;
        private List<SSHStation> _cachedStations;
        private List<SSHRoom> _cachedRooms;
        private List<SSHFsu> _cachedFsus;
        private List<SSHDevice> _cachedDevices;
        private List<SSHCamera> _cachedCameras;
        private HashSet<string> _cachedDeviceKeys;
        private HashSet<string> _cachedHashMaskings;
        private List<H_Masking> _cachedMaskings;
        private List<C_Group> _cachedGroups;
        private List<P_Point> _cachedPoints;
        private List<P_SubPoint> _cachedSubPoints;
        private List<P_Point> _cachedAL;
        private List<AlmStore<A_AAlarm>> _cachedActAlarms;
        private List<AlmStore<A_AAlarm>> _cachedAllAlarms;

        #endregion

        #region Ctor

        public iPemWorkContext(
            HttpContextBase httpContext,
            ICacheManager cacheManager,
            IAreaService areaService,
            ICameraService cameraService,
            IDepartmentService departmentService,
            IDeviceService deviceService,
            IDeviceTypeService deviceTypeService,
            IEmployeeService employeeService,
            IEnumMethodService enumMethodService,
            IFsuService fsuService,
            IGroupService groupService,
            ILogicTypeService rsLogicTypeService,
            IMaskingService maskingService,
            IPointService pointService,
            IProtocolService protocolService,
            IRoomService roomService,
            IRoomTypeService roomTypeService,
            ISCVendorService scVendorService,
            ISignalService signalService,
            IStationService stationService,
            IStationTypeService stationTypeService,
            IAAlarmService actAlarmService,
            IHIDeviceService iDeviceService,
            IHIStationService iStationService,
            IHIAreaService iAreaService,
            IDictionaryService dictionaryService,
            IEntitiesInRoleService entitiesInRoleService,
            IProfileService profileService,
            IRoleService roleService,
            IUserService userService) {
            this._httpContext = httpContext;
            this._cacheManager = cacheManager;
            this._areaService = areaService;
            this._cameraService = cameraService;
            this._departmentService = departmentService;
            this._deviceService = deviceService;
            this._deviceTypeService = deviceTypeService;
            this._employeeService = employeeService;
            this._enumMethodService = enumMethodService;
            this._fsuService = fsuService;
            this._groupService = groupService;
            this._logicTypeService = rsLogicTypeService;
            this._maskingService = maskingService;
            this._pointService = pointService;
            this._protocolService = protocolService;
            this._roomService = roomService;
            this._roomTypeService = roomTypeService;
            this._scVendorService = scVendorService;
            this._signalService = signalService;
            this._stationService = stationService;
            this._stationTypeService = stationTypeService;
            this._actAlarmService = actAlarmService;
            this._iDeviceService = iDeviceService;
            this._iStationService = iStationService;
            this._iAreaService = iAreaService;
            this._dictionaryService = dictionaryService;
            this._entitiesInRoleService = entitiesInRoleService;
            this._profileService = profileService;
            this._roleService = roleService;
            this._userService = userService;
        }

        #endregion

        #region Properties Methods

        public Boolean IsAuthenticated() {
            return (_httpContext != null &&
                   _httpContext.Request != null &&
                   _httpContext.Request.IsAuthenticated &&
                   (_httpContext.User.Identity is FormsIdentity));
        }

        public Guid Identifier() {
            if (_cachedIdentifier.HasValue)
                return _cachedIdentifier.Value;

            if (_httpContext == null ||
               _httpContext.Request == null ||
               !_httpContext.Request.IsAuthenticated ||
               !(_httpContext.User.Identity is FormsIdentity)) {
                throw new iPemException("Unauthorized");
            }

            var authCookie = _httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) throw new iPemException("Cookie not found.");

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null) throw new iPemException("Encrypted ticket is invalid.");

            _cachedIdentifier = new Guid(ticket.UserData);
            return _cachedIdentifier.Value;
        }

        private Store AppStore() {
            if (_cachedStore != null) {
                _cachedStore.ResetExpire();
                return _cachedStore;
            }

            var identifier = this.Identifier();
            if (!EngineContext.Current.WorkStores.ContainsKey(identifier)) {
                _cachedStore = Store.CreateInstance(identifier);
            } else {
                _cachedStore = EngineContext.Current.WorkStores[identifier];
                _cachedStore.ResetExpire();
            }

            return _cachedStore;
        }

        public U_Role Role() {
            if (_cachedRole != null)
                return _cachedRole;

            var store = this.AppStore();
            if (store.Role != null)
                return _cachedRole = store.Role;

            if (!IsAuthenticated()) throw new iPemException("Unauthorized");
            var role = _roleService.GetRoleByUid(this.User().Id);
            if (role == null) throw new iPemException("Current role not found.");
            return store.Role = _cachedRole = role;
        }

        public U_User User() {
            if (_cachedUser != null)
                return _cachedUser;

            var store = this.AppStore();
            if (store.User != null)
                return _cachedUser = store.User;

            if (!IsAuthenticated()) throw new iPemException("Unauthorized");
            var user = _userService.GetUserByName(_httpContext.User.Identity.Name);
            if (user == null) throw new iPemException("Current user not found.");
            return store.User = _cachedUser = user;
        }

        public U_Employee Employee() {
            if (_cachedEmployee != null)
                return _cachedEmployee;

            var store = this.AppStore();
            if (store.Employee != null) {
                _cachedEmployee = store.Employee;
                return _cachedEmployee;
            }

            return store.Employee = _cachedEmployee = _employeeService.GetEmployeeById(this.User().EmployeeId);
        }

        public IEnumerable<FollowPoint> ProfileFollows() {
            if (_cachedProfileFollows != null)
                return _cachedProfileFollows;

            var userId = this.User().Id;
            var cacheId = string.Format(GlobalCacheKeys.ProfileFollowPattern, userId);
            var lockId = string.Format("LOCK:PROFILE:{0}:FOLLOW", userId);
            if (!_cacheManager.IsSet(cacheId)) {
                //获得临界锁,同步多个请求
                using (_cacheManager.AcquireLock(lockId, 10)) {
                    if (!_cacheManager.IsSet(cacheId)) {
                        var profile = _profileService.GetProfile(userId, EnmProfile.Follow);
                        if (profile != null && !string.IsNullOrWhiteSpace(profile.ValuesJson)) {
                            _cachedProfileFollows = JsonConvert.DeserializeObject<IEnumerable<FollowPoint>>(profile.ValuesJson);
                        } else {
                            _cachedProfileFollows = new List<FollowPoint>();
                        }

                        _cacheManager.AddItemsToSet<FollowPoint>(cacheId, _cachedProfileFollows);
                    } else {
                        _cachedProfileFollows = _cacheManager.GetItemsFromSet<FollowPoint>(cacheId);
                    }
                }
            } else {
                _cachedProfileFollows = _cacheManager.GetItemsFromSet<FollowPoint>(cacheId);
            }

            return _cachedProfileFollows;
        }

        public IEnumerable<SeniorCondition> ProfileConditions() {
            if (_cachedProfileConditions != null)
                return _cachedProfileConditions;

            var userId = this.User().Id;
            var cacheId = string.Format(GlobalCacheKeys.ProfileConditionPattern, userId);
            var lockId = string.Format("LOCK:PROFILE:{0}:CONDITION", userId);
            if (!_cacheManager.IsSet(cacheId)) {
                //获得临界锁,同步多个请求
                using (_cacheManager.AcquireLock(lockId, 10)) {
                    if (!_cacheManager.IsSet(cacheId)) {
                        var profile = _profileService.GetProfile(userId, EnmProfile.Condition);
                        if (profile != null && !string.IsNullOrWhiteSpace(profile.ValuesJson)) {
                            _cachedProfileConditions = JsonConvert.DeserializeObject<IEnumerable<SeniorCondition>>(profile.ValuesJson);
                        } else {
                            _cachedProfileConditions = new List<SeniorCondition>();
                        }

                        _cacheManager.AddItemsToList<SeniorCondition>(cacheId, _cachedProfileConditions);
                    } else {
                        _cachedProfileConditions = _cacheManager.GetItemsFromList<SeniorCondition>(cacheId);
                    }
                }
            } else {
                _cachedProfileConditions = _cacheManager.GetItemsFromList<SeniorCondition>(cacheId);
            }

            return _cachedProfileConditions;
        }

        public IEnumerable<MatrixTemplate> ProfileMatrixs() {
            if (_cachedProfileMatrixs != null)
                return _cachedProfileMatrixs;

            var userId = this.User().Id;
            var cacheId = string.Format(GlobalCacheKeys.ProfileMatrixPattern, userId);
            var lockId = string.Format("LOCK:PROFILE:{0}:MATRIX", userId);
            if (!_cacheManager.IsSet(cacheId)) {
                //获得临界锁,同步多个请求
                using (_cacheManager.AcquireLock(lockId, 10)) {
                    if (!_cacheManager.IsSet(cacheId)) {
                        var profile = _profileService.GetProfile(userId, EnmProfile.Matrix);
                        if (profile != null && !string.IsNullOrWhiteSpace(profile.ValuesJson)) {
                            _cachedProfileMatrixs = JsonConvert.DeserializeObject<IEnumerable<MatrixTemplate>>(profile.ValuesJson);
                        } else {
                            _cachedProfileMatrixs = new List<MatrixTemplate>();
                        }

                        _cacheManager.AddItemsToList<MatrixTemplate>(cacheId, _cachedProfileMatrixs);
                    } else {
                        _cachedProfileMatrixs = _cacheManager.GetItemsFromList<MatrixTemplate>(cacheId);
                    }
                }
            } else {
                _cachedProfileMatrixs = _cacheManager.GetItemsFromList<MatrixTemplate>(cacheId);
            }

            return _cachedProfileMatrixs;
        }

        public iAuth Authorizations() {
            if (_cachedAuths != null)
                return _cachedAuths;

            var auth = _entitiesInRoleService.GetEntitiesInRole(this.Role().Id);
            return _cachedAuths = new iAuth { Menus = new HashSet<int>(auth.Menus), Areas = new HashSet<string>(auth.Areas), Permissions = new HashSet<EnmPermission>(auth.Permissions) };
        }

        public DateTime GetLastNoticeTime() {
            return this.AppStore().LastNoticeTime;
        }

        public void SetLastNoticeTime(DateTime value) {
            this.AppStore().LastNoticeTime = value;
        }

        public DateTime GetLastSpeechTime() {
            return this.AppStore().LastSpeechTime;
        }

        public void SetLastSpeechTime(DateTime value) {
            this.AppStore().LastSpeechTime = value;
        }

        public DateTime LastLoginTime() {
            return this.AppStore().CreatedTime;
        }

        public WsValues WsValues() {
            if (_cachedWsValues != null)
                return _cachedWsValues;

            if (_cacheManager.IsSet(GlobalCacheKeys.Dictionary_Ws))
                return _cachedWsValues = _cacheManager.Get<WsValues>(GlobalCacheKeys.Dictionary_Ws);

            var ws = _dictionaryService.GetDictionary((int)EnmDictionary.Ws);
            if (ws != null && !string.IsNullOrWhiteSpace(ws.ValuesJson))
                _cachedWsValues = JsonConvert.DeserializeObject<WsValues>(ws.ValuesJson);

            _cacheManager.Set(GlobalCacheKeys.Dictionary_Ws, _cachedWsValues);
            return _cachedWsValues;
        }

        public TsValues TsValues() {
            if (_cachedTsValues != null)
                return _cachedTsValues;

            if (_cacheManager.IsSet(GlobalCacheKeys.Dictionary_Ts))
                return _cachedTsValues = _cacheManager.Get<TsValues>(GlobalCacheKeys.Dictionary_Ts);

            var ts = _dictionaryService.GetDictionary((int)EnmDictionary.Ts);
            if (ts != null && !string.IsNullOrWhiteSpace(ts.ValuesJson))
                _cachedTsValues = JsonConvert.DeserializeObject<TsValues>(ts.ValuesJson);

            _cacheManager.Set(GlobalCacheKeys.Dictionary_Ts, _cachedTsValues);
            return _cachedTsValues;
        }

        public RtValues RtValues() {
            if (_cachedRtValues != null)
                return _cachedRtValues;

            if (_cacheManager.IsSet(GlobalCacheKeys.Dictionary_Rt))
                return _cachedRtValues = _cacheManager.Get<RtValues>(GlobalCacheKeys.Dictionary_Rt);

            var rt = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
            if (rt != null && !string.IsNullOrWhiteSpace(rt.ValuesJson))
                _cachedRtValues = JsonConvert.DeserializeObject<RtValues>(rt.ValuesJson);

            _cacheManager.Set(GlobalCacheKeys.Dictionary_Rt, _cachedRtValues);
            return _cachedRtValues;
        }

        public List<C_LogicType> LogicTypes() {
            if (_cachedLogicTypes != null)
                return _cachedLogicTypes;

            return _cachedLogicTypes = _logicTypeService.GetLogicTypes();
        }

        public List<C_SubLogicType> SubLogicTypes() {
            if (_cachedSubLogicTypes != null)
                return _cachedSubLogicTypes;

            return _cachedSubLogicTypes = _logicTypeService.GetSubLogicTypes();
        }

        public List<C_DeviceType> DeviceTypes() {
            if (_cachedDeviceTypes != null)
                return _cachedDeviceTypes;

            return _cachedDeviceTypes = _deviceTypeService.GetDeviceTypes();
        }

        public List<C_SubDeviceType> SubDeviceTypes() {
            if (_cachedSubDeviceTypes != null)
                return _cachedSubDeviceTypes;

            return _cachedSubDeviceTypes = _deviceTypeService.GetSubDeviceTypes();
        }

        public List<C_RoomType> RoomTypes() {
            if (_cachedRoomTypes != null)
                return _cachedRoomTypes;

            return _cachedRoomTypes = _roomTypeService.GetRoomTypes();
        }

        public List<C_StationType> StationTypes() {
            if (_cachedStationTypes != null)
                return _cachedStationTypes;

            return _cachedStationTypes = _stationTypeService.GetStationTypes();
        }

        public List<C_EnumMethod> AreaTypes() {
            if (_cachedAreaTypes != null)
                return _cachedAreaTypes;

            return _cachedAreaTypes = _enumMethodService.GetEnumsByType(EnmMethodType.Area, "类型");
        }

        public List<C_SCVendor> Vendors() {
            if (_cachedVendors != null)
                return _cachedVendors;

            return _cachedVendors = _scVendorService.GetVendors();
        }

        public List<C_Department> Departments() {
            if (_cachedDepartments != null)
                return _cachedDepartments;

            return _cachedDepartments = _departmentService.GetDepartments();
        }
        
        public List<SSHArea> AllAreas() {
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

        public List<SSHStation> AllStations() {
            if (_cachedAllStations != null)
                return _cachedAllStations;

            _cachedAllStations = new List<SSHStation>();
            foreach (var _current in _stationService.GetStations()) {
                _cachedAllStations.Add(new SSHStation { Current = _current });
            }

            return _cachedAllStations;
        }

        public List<SSHRoom> AllRooms() {
            if (_cachedAllRooms != null)
                return _cachedAllRooms;

            _cachedAllRooms = new List<SSHRoom>();
            foreach (var _current in _roomService.GetRooms()) {
                _cachedAllRooms.Add(new SSHRoom { Current = _current });
            }

            return _cachedAllRooms;
        }

        public List<SSHFsu> AllFsus() {
            if (_cachedAllFsus != null)
                return _cachedAllFsus;

            _cachedAllFsus = new List<SSHFsu>();
            foreach (var _current in _fsuService.GetFsus()) {
                _cachedAllFsus.Add(new SSHFsu { Current = _current });
            }

            return _cachedAllFsus;
        }

        public List<SSHDevice> AllDevices() {
            if (_cachedAllDevices != null)
                return _cachedAllDevices;

            _cachedAllDevices = new List<SSHDevice>();
            foreach (var _current in _deviceService.GetDevices()) {
                _cachedAllDevices.Add(new SSHDevice { Current = _current });
            }

            return _cachedAllDevices;
        }

        public List<SSHCamera> AllCameras() {
            if (_cachedAllCameras != null)
                return _cachedAllCameras;

            _cachedAllCameras = new List<SSHCamera>();
            foreach (var _current in _cameraService.GetCameras()) {
                _cachedAllCameras.Add(new SSHCamera { Current = _current });
            }

            return _cachedAllCameras;
        }

        public List<SSHArea> Areas() {
            if (this.Role().Id == U_Role.SuperId)
                return this.AllAreas();

            if (_cachedAreas != null)
                return _cachedAreas;

            var cachedKey = string.Format(GlobalCacheKeys.SSH_AreasPattern, this.Role().Id);
            if (_cacheManager.IsSet(cachedKey))
                return _cachedAreas = _cacheManager.Get<List<SSHArea>>(cachedKey);

            _cachedAreas = new List<SSHArea>();
            var auths = this.Authorizations().Areas;
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

        public List<SSHStation> Stations() {
            if (this.Role().Id == U_Role.SuperId)
                return this.AllStations();

            if (_cachedStations != null)
                return _cachedStations;

            _cachedStations = new List<SSHStation>();
            foreach (var _current in _stationService.GetStations()) {
                if (!this.Authorizations().Areas.Contains(_current.AreaId)) continue;
                _cachedStations.Add(new SSHStation { Current = _current });
            }

            return _cachedStations;
        }

        public List<SSHRoom> Rooms() {
            if (this.Role().Id == U_Role.SuperId)
                return this.AllRooms();

            if (_cachedRooms != null)
                return _cachedRooms;

            _cachedRooms = new List<SSHRoom>();
            foreach (var _current in _roomService.GetRooms()) {
                if (!this.Authorizations().Areas.Contains(_current.AreaId)) continue;
                _cachedRooms.Add(new SSHRoom { Current = _current });
            }

            return _cachedRooms;
        }

        public List<SSHFsu> Fsus() {
            if (this.Role().Id == U_Role.SuperId)
                return this.AllFsus();

            if (_cachedFsus != null)
                return _cachedFsus;

            _cachedFsus = new List<SSHFsu>();
            foreach (var _current in _fsuService.GetFsus()) {
                if (!this.Authorizations().Areas.Contains(_current.AreaId)) continue;
                _cachedFsus.Add(new SSHFsu { Current = _current });
            }

            return _cachedFsus;
        }

        public List<SSHDevice> Devices() {
            if (this.Role().Id == U_Role.SuperId)
                return this.AllDevices();

            if (_cachedDevices != null)
                return _cachedDevices;

            _cachedDevices = new List<SSHDevice>();
            foreach (var _current in _deviceService.GetDevices()) {
                if (!this.Authorizations().Areas.Contains(_current.AreaId)) continue;
                _cachedDevices.Add(new SSHDevice { Current = _current });
            }

            return _cachedDevices;
        }

        public List<SSHCamera> Cameras() {
            if (this.Role().Id == U_Role.SuperId)
                return this.AllCameras();

            if (_cachedCameras != null)
                return _cachedCameras;

            _cachedCameras = new List<SSHCamera>();
            foreach (var _current in _cameraService.GetCameras()) {
                if (!this.Authorizations().Areas.Contains(_current.AreaId)) continue;
                _cachedCameras.Add(new SSHCamera { Current = _current });
            }

            return _cachedCameras;
        }

        public HashSet<string> DeviceKeys() {
            if (_cachedDeviceKeys != null)
                return _cachedDeviceKeys;

            _cachedDeviceKeys = new HashSet<string>();
            foreach (var _current in Devices()) {
                _cachedDeviceKeys.Add(_current.Current.Id);
            }

            return _cachedDeviceKeys;
        }

        public List<C_Group> Groups() {
            if (_cachedGroups != null)
                return _cachedGroups;

            return _cachedGroups = _groupService.GetGroups();
        }

        public List<P_Point> Points() {
            if (_cachedPoints != null)
                return _cachedPoints;

            return _cachedPoints = _pointService.GetPoints();
        }

        public List<P_SubPoint> SubPoints() {
            if (_cachedSubPoints != null)
                return _cachedSubPoints;

            return _cachedSubPoints = _pointService.GetSubPoints();
        }

        public List<P_Point> AL() {
            if (_cachedAL != null)
                return _cachedAL;

            _cachedAL = this.Points().FindAll(p => p.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(p.AlarmId));
            return _cachedAL;
        }

        public List<AlmStore<A_AAlarm>> ActAlarms() {
            if (_cachedActAlarms != null)
                return _cachedActAlarms;

            _cachedActAlarms = this.AllAlarms();
            if (_cachedActAlarms.Count > 0 && this.Role().Id != U_Role.SuperId) {
                var keys = this.Authorizations().Areas;
                _cachedActAlarms = _cachedActAlarms.FindAll(a => Common.IsSystemAlarm(a.Current.FsuId) || keys.Contains(a.Current.AreaId));
            }

            return _cachedActAlarms;
        }

        /// <summary>
        /// 获得所有活动告警
        /// <para>
        /// 活动告警未排序,获得后需手动排序
        /// </para>
        /// </summary>
        public List<AlmStore<A_AAlarm>> AllAlarms() {
            if (_cachedAllAlarms != null)
                return _cachedAllAlarms;

            // 缓存活动告警，减小服务器压力
            List<A_AAlarm> _allAlarms = null;
            
            #region 正常告警
            IEnumerable<AlmStore<A_AAlarm>> _actAlarms = null;            
            if (!_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms)) {
                //获得临界锁,同步多个请求
                using (_cacheManager.AcquireLock("LOCK:ACTIVE:ALARM", 10)) {
                    if (!_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms)) {
                        if (_allAlarms == null) _allAlarms = _actAlarmService.GetAlarms();
                        var _alarms = _allAlarms.FindAll(a => !Common.IsSystemAlarm(a.FsuId));
                        if (_alarms.Count > 0) {
                            var _signals = _signalService.GetSimpleSignals(_alarms.Select(a => new Kv<string, string>(a.DeviceId, a.PointId)));
                            var _points = this.AL();
                            var _devices = this.AllDevices();
                            _actAlarms = from alarm in _alarms
                                         join signal in _signals on new { alarm.DeviceId, alarm.PointId } equals new { signal.DeviceId, signal.PointId }
                                         join point in _points on alarm.PointId equals point.Id
                                         join device in _devices on alarm.DeviceId equals device.Current.Id
                                         select new AlmStore<A_AAlarm> {
                                             Current = alarm,
                                             PointName = signal.PointName,
                                             AlarmName = point.Name,
                                             DeviceName = device.Current.Name,
                                             DeviceTypeId = device.Current.Type.Id,
                                             SubDeviceTypeId = device.Current.SubType.Id,
                                             SubLogicTypeId = device.Current.SubLogicType.Id,
                                             RoomName = device.Current.RoomName,
                                             RoomTypeId = device.Current.RoomTypeId,
                                             StationName = device.Current.StationName,
                                             StationTypeId = device.Current.StaTypeId,
                                             AreaName = device.Current.AreaName,
                                             SubCompany = device.Current.SubCompany ?? "--"
                                         };

                            _cacheManager.AddItemsToList<AlmStore<A_AAlarm>>(GlobalCacheKeys.Active_Alarms, _actAlarms, GlobalCacheInterval.ActAlarm_Interval);
                        }
                    } else {
                        _actAlarms = _cacheManager.GetItemsFromList<AlmStore<A_AAlarm>>(GlobalCacheKeys.Active_Alarms);
                    }
                }
            } else {
                _actAlarms = _cacheManager.GetItemsFromList<AlmStore<A_AAlarm>>(GlobalCacheKeys.Active_Alarms);
            }
            #endregion

            #region SC告警
            IEnumerable<AlmStore<A_AAlarm>> _scAlarms = null;
            if (!_cacheManager.IsSet(GlobalCacheKeys.System_SC_Alarms)) {
                //获得临界锁,同步多个请求
                using (_cacheManager.AcquireLock("LOCK:SYSTEM:SC:ALARM", 10)) {
                    if (!_cacheManager.IsSet(GlobalCacheKeys.System_SC_Alarms)) {
                        if (_allAlarms == null) _allAlarms = _actAlarmService.GetAlarms();
                        var _alarms = _allAlarms.FindAll(a => Common.IsSystemSCAlarm(a.FsuId));
                        if (_alarms.Count > 0) {
                            var _points = this.AL();
                            var _groups = this.Groups();
                            _scAlarms = from alarm in _alarms
                                        join point in _points on alarm.PointId equals point.Id
                                        join sc in _groups on alarm.DeviceId equals sc.Id
                                        select new AlmStore<A_AAlarm> {
                                            Current = alarm,
                                            PointName = point.Name,
                                            AlarmName = point.Name,
                                            DeviceName = sc.Name,
                                            DeviceTypeId = SSHSystem.SC.Type.Id,
                                            SubDeviceTypeId = SSHSystem.SC.SubType.Id,
                                            SubLogicTypeId = SSHSystem.SC.SubLogicType.Id,
                                            RoomName = SSHSystem.Room.Name,
                                            RoomTypeId = SSHSystem.Room.Type.Id,
                                            StationName = SSHSystem.Station.Name,
                                            StationTypeId = SSHSystem.Station.Type.Id,
                                            AreaName = SSHSystem.Area.Name,
                                            SubCompany = "--"
                                        };

                            _cacheManager.AddItemsToList<AlmStore<A_AAlarm>>(GlobalCacheKeys.System_SC_Alarms, _scAlarms, GlobalCacheInterval.ActAlarm_Interval);
                        }
                    } else {
                        _scAlarms = _cacheManager.GetItemsFromList<AlmStore<A_AAlarm>>(GlobalCacheKeys.System_SC_Alarms);
                    }
                }
            } else {
                _scAlarms = _cacheManager.GetItemsFromList<AlmStore<A_AAlarm>>(GlobalCacheKeys.System_SC_Alarms);
            }
            #endregion

            #region FSU告警
            IEnumerable<AlmStore<A_AAlarm>> _fsuAlarms = null;
            if (!_cacheManager.IsSet(GlobalCacheKeys.System_FSU_Alarms)) {
                //获得临界锁,同步多个请求
                using (_cacheManager.AcquireLock("LOCK:SYSTEM:FSU:ALARM", 10)) {
                    if (!_cacheManager.IsSet(GlobalCacheKeys.System_FSU_Alarms)) {
                        if (_allAlarms == null) _allAlarms = _actAlarmService.GetAlarms();
                        var _alarms = _allAlarms.FindAll(a => Common.IsSystemFSUAlarm(a.FsuId));
                        if (_alarms.Count > 0) {
                            var _points = this.AL();
                            var _fsus = this.AllFsus();
                            _fsuAlarms = from alarm in _alarms
                                         join point in _points on alarm.PointId equals point.Id
                                         join fsu in _fsus on alarm.DeviceId equals fsu.Current.Id
                                         select new AlmStore<A_AAlarm> {
                                             Current = alarm,
                                             PointName = point.Name,
                                             AlarmName = point.Name,
                                             DeviceName = fsu.Current.Name,
                                             DeviceTypeId = SSHSystem.FSU.Type.Id,
                                             SubDeviceTypeId = SSHSystem.FSU.SubType.Id,
                                             SubLogicTypeId = SSHSystem.FSU.SubLogicType.Id,
                                             RoomName = fsu.Current.RoomName,
                                             RoomTypeId = fsu.Current.RoomTypeId,
                                             StationName = fsu.Current.StationName,
                                             StationTypeId = fsu.Current.StaTypeId,
                                             AreaName = fsu.Current.AreaName,
                                             SubCompany = "--"
                                         };

                            _cacheManager.AddItemsToList<AlmStore<A_AAlarm>>(GlobalCacheKeys.System_FSU_Alarms, _fsuAlarms, GlobalCacheInterval.ActAlarm_Interval);
                        }
                    } else {
                        _fsuAlarms = _cacheManager.GetItemsFromList<AlmStore<A_AAlarm>>(GlobalCacheKeys.System_FSU_Alarms);
                    }
                }
            } else {
                _fsuAlarms = _cacheManager.GetItemsFromList<AlmStore<A_AAlarm>>(GlobalCacheKeys.System_FSU_Alarms);
            }
            #endregion

            _cachedAllAlarms = new List<AlmStore<A_AAlarm>>();
            if (_actAlarms != null && _actAlarms.Any()) {
                _cachedAllAlarms.AddRange(_actAlarms);
            }
            if (_scAlarms != null && _scAlarms.Any()) {
                _cachedAllAlarms.AddRange(_scAlarms);
            }
            if (_fsuAlarms != null && _fsuAlarms.Any()) {
                _cachedAllAlarms.AddRange(_fsuAlarms);
            }

            return _cachedAllAlarms = _cachedAllAlarms.OrderByDescending(a => a.Current.AlarmTime).ToList();
        }

        public List<H_Masking> Maskings() {
            if (_cachedMaskings != null)
                return _cachedMaskings;

            return _cachedMaskings = _maskingService.GetMaskings();
        }

        public HashSet<string> HashMaskings() {
            if (_cachedHashMaskings != null)
                return _cachedHashMaskings;

            return _cachedHashMaskings = _maskingService.GetHashMaskings();
        }

        #endregion

        #region Methods

        public void ResetRole() {
            this.AppStore().Role = null;
        }

        public void ResetUser() {
            this.AppStore().User = null;
        }

        public void ResetEmployee() {
            this.AppStore().Employee = null;
        }

        public void ResetProfile(EnmProfile type) {
            var userId = this.User().Id;
            if(type == EnmProfile.Follow){
                var cacheId = string.Format(GlobalCacheKeys.ProfileFollowPattern, userId);
                if (_cacheManager.IsSet(cacheId)) {
                    _cacheManager.Remove(cacheId);
                }
            }

            if (type == EnmProfile.Condition) {
                var cacheId = string.Format(GlobalCacheKeys.ProfileConditionPattern, userId);
                if (_cacheManager.IsSet(cacheId)) {
                    _cacheManager.Remove(cacheId);
                }
            }

            if (type == EnmProfile.Matrix) {
                var cacheId = string.Format(GlobalCacheKeys.ProfileMatrixPattern, userId);
                if (_cacheManager.IsSet(cacheId)) {
                    _cacheManager.Remove(cacheId);
                }
            }
        }

        public List<iSSHDevice> iDevices(DateTime date) {
            var iAreas = _iAreaService.GetAreas(date);
            var iStations = _iStationService.GetStations(date);
            var iDevices = _iDeviceService.GetDevices(date);
            var iFull = (from iDevice in iDevices
                         join iStation in iStations on iDevice.StationId equals iStation.Id
                         join iArea in iAreas on iStation.AreaId equals iArea.Id
                         select new iSSHDevice { Current = iDevice, iStation = iStation, iArea = iArea }).ToList();
            
            return iFull;
        }

        public List<iSSHStation> iStations(DateTime date) {
            var iAreas = _iAreaService.GetAreas(date);
            var iStations = _iStationService.GetStations(date);
            var iDevices = _iDeviceService.GetDevices(date);
            var iFull = (from iStation in iStations
                         join iArea in iAreas on iStation.AreaId equals iArea.Id
                         select new iSSHStation { Current = iStation, iArea = iArea }).ToList();

            return iFull;
        }

        public List<iSSHArea> iAreas(DateTime date) {
            return _iAreaService.GetAreas(date).Select(a => new iSSHArea { Current = a }).ToList();
        }

        public List<P_Point> GetPoints(bool _ai, bool _ao, bool _di, bool _do, bool _al) {
            return this.Points().FindAll(p => 
                   (_ai && p.Type == EnmPoint.AI)
                || (_ao && p.Type == EnmPoint.AO)
                || (_di && p.Type == EnmPoint.DI && string.IsNullOrWhiteSpace(p.AlarmId))
                || (_do && p.Type == EnmPoint.DO)
                || (_al && p.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(p.AlarmId))
            );
        }

        public List<S_Station> GetStationsWithPoints(IList<string> points) {
            return _stationService.GetStationsWithPoints(points).FindAll(s => this.Authorizations().Areas.Contains(s.AreaId));
        }

        public List<AlmStore<A_AAlarm>> AlarmsToStore(List<A_AAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_AAlarm>>();

            var _signals = _signalService.GetSimpleSignals(alarms.Select(a => new Kv<string, string>(a.DeviceId, a.PointId)));
            var _points = this.AL();
            var _devices = this.Devices();
            return (from alarm in alarms
                    join signal in _signals on new { alarm.DeviceId, alarm.PointId } equals new { signal.DeviceId, signal.PointId }
                    join point in _points on alarm.PointId equals point.Id
                    join device in _devices on alarm.DeviceId equals device.Current.Id
                    orderby alarm.AlarmTime descending
                    select new AlmStore<A_AAlarm> {
                        Current = alarm,
                        PointName = signal.PointName,
                        AlarmName = point.Name,
                        DeviceName = device.Current.Name,
                        DeviceTypeId = device.Current.Type.Id,
                        SubDeviceTypeId = device.Current.SubType.Id,
                        SubLogicTypeId = device.Current.SubLogicType.Id,
                        RoomName = device.Current.RoomName,
                        RoomTypeId = device.Current.RoomTypeId,
                        StationName = device.Current.StationName,
                        StationTypeId = device.Current.StaTypeId,
                        AreaName = device.Current.Name,
                        SubCompany = device.Current.SubCompany ?? "--"
                    }).ToList();
        }

        public List<AlmStore<A_HAlarm>> AlarmsToStore(List<A_HAlarm> alarms) {
            if(alarms == null || alarms.Count == 0) 
                return new List<AlmStore<A_HAlarm>>();

            var _signals = _signalService.GetSimpleSignals(alarms.Select(a => new Kv<string, string>(a.DeviceId, a.PointId)));
            var _points = this.AL();
            var _devices = this.Devices(); 
            return (from alarm in alarms
                    join signal in _signals on new { alarm.DeviceId, alarm.PointId } equals new { signal.DeviceId, signal.PointId }
                    join point in _points on alarm.PointId equals point.Id
                    join device in _devices on alarm.DeviceId equals device.Current.Id
                    orderby alarm.StartTime descending
                    select new AlmStore<A_HAlarm> {
                        Current = alarm,
                        PointName = signal.PointName,
                        AlarmName = point.Name,
                        DeviceName = device.Current.Name,
                        DeviceTypeId = device.Current.Type.Id,
                        SubDeviceTypeId = device.Current.SubType.Id,
                        SubLogicTypeId = device.Current.SubLogicType.Id,
                        RoomName = device.Current.RoomName,
                        RoomTypeId = device.Current.RoomTypeId,
                        StationName = device.Current.StationName,
                        StationTypeId = device.Current.StaTypeId,
                        AreaName = device.Current.Name,
                        SubCompany = device.Current.SubCompany ?? "--"
                    }).ToList();
        }

        public List<AlmStore<A_AAlarm>> AlarmsToSc(List<A_AAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_AAlarm>>();

            var _points = this.AL();
            var _groups = this.Groups();
            return (from alarm in alarms
                    join point in _points on alarm.PointId equals point.Id
                    join sc in _groups on alarm.DeviceId equals sc.Id
                    select new AlmStore<A_AAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        AlarmName = point.Name,
                        DeviceName = sc.Name,
                        DeviceTypeId = SSHSystem.SC.Type.Id,
                        SubDeviceTypeId = SSHSystem.SC.SubType.Id,
                        SubLogicTypeId = SSHSystem.SC.SubLogicType.Id,
                        RoomName = SSHSystem.Room.Name,
                        RoomTypeId = SSHSystem.Room.Type.Id,
                        StationName = SSHSystem.Station.Name,
                        StationTypeId = SSHSystem.Station.Type.Id,
                        AreaName = SSHSystem.Area.Name,
                        SubCompany = "--"
                    }).ToList();
        }

        public List<AlmStore<A_HAlarm>> AlarmsToSc(List<A_HAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_HAlarm>>();

            var _points = this.AL();
            var _groups = this.Groups();
            return (from alarm in alarms
                    join point in _points on alarm.PointId equals point.Id
                    join sc in _groups on alarm.DeviceId equals sc.Id
                    select new AlmStore<A_HAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        AlarmName = point.Name,
                        DeviceName = sc.Name,
                        DeviceTypeId = SSHSystem.SC.Type.Id,
                        SubDeviceTypeId = SSHSystem.SC.SubType.Id,
                        SubLogicTypeId = SSHSystem.SC.SubLogicType.Id,
                        RoomName = SSHSystem.Room.Name,
                        RoomTypeId = SSHSystem.Room.Type.Id,
                        StationName = SSHSystem.Station.Name,
                        StationTypeId = SSHSystem.Station.Type.Id,
                        AreaName = SSHSystem.Area.Name,
                        SubCompany = "--"
                    }).ToList();
        }

        public List<AlmStore<A_AAlarm>> AlarmsToFsu(List<A_AAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_AAlarm>>();

            var _points = this.AL();
            var _fsus = this.Fsus();
            return (from alarm in alarms
                    join point in _points on alarm.PointId equals point.Id
                    join fsu in _fsus on alarm.DeviceId equals fsu.Current.Id
                    select new AlmStore<A_AAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        AlarmName = point.Name,
                        DeviceName = fsu.Current.Name,
                        DeviceTypeId = SSHSystem.FSU.Type.Id,
                        SubDeviceTypeId = SSHSystem.FSU.SubType.Id,
                        SubLogicTypeId = SSHSystem.FSU.SubLogicType.Id,
                        RoomName = fsu.Current.RoomName,
                        RoomTypeId = fsu.Current.RoomTypeId,
                        StationName = fsu.Current.StationName,
                        StationTypeId = fsu.Current.StaTypeId,
                        AreaName = fsu.Current.AreaName,
                        SubCompany = "--"
                    }).ToList();
        }

        public List<AlmStore<A_HAlarm>> AlarmsToFsu(List<A_HAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_HAlarm>>();

            var _points = this.AL();
            var _fsus = this.Fsus();
            return (from alarm in alarms
                    join point in _points on alarm.PointId equals point.Id
                    join fsu in _fsus on alarm.DeviceId equals fsu.Current.Id
                    select new AlmStore<A_HAlarm> {
                        Current = alarm,
                        PointName = point.Name,
                        AlarmName = point.Name,
                        DeviceName = fsu.Current.Name,
                        DeviceTypeId = SSHSystem.FSU.Type.Id,
                        SubDeviceTypeId = SSHSystem.FSU.SubType.Id,
                        SubLogicTypeId = SSHSystem.FSU.SubLogicType.Id,
                        RoomName = fsu.Current.RoomName,
                        RoomTypeId = fsu.Current.RoomTypeId,
                        StationName = fsu.Current.StationName,
                        StationTypeId = fsu.Current.StaTypeId,
                        AreaName = fsu.Current.AreaName,
                        SubCompany = "--"
                    }).ToList();
        }

        public Dictionary<string, AlmStore<A_AAlarm>> AlarmsToDictionary(List<AlmStore<A_AAlarm>> alarms, bool primaryKey = true) {
            var dictionary = new Dictionary<string, AlmStore<A_AAlarm>>();
            if (alarms == null || alarms.Count == 0)
                return dictionary;

            foreach (var alarm in alarms) {
                dictionary[primaryKey ? alarm.Current.Id : Common.JoinKeys(alarm.Current.DeviceId, alarm.Current.PointId)] = alarm;
            }

            return dictionary;
        }

        public Dictionary<string, AlmStore<A_HAlarm>> AlarmsToDictionary(List<AlmStore<A_HAlarm>> alarms, bool primaryKey = true) {
            var dictionary = new Dictionary<string, AlmStore<A_HAlarm>>();
            if (alarms == null || alarms.Count == 0)
                return dictionary;

            foreach (var alarm in alarms) {
                dictionary[primaryKey ? alarm.Current.Id : Common.JoinKeys(alarm.Current.DeviceId, alarm.Current.PointId)] = alarm;
            }

            return dictionary;
        }

        #endregion

    }
}