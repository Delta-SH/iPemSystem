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
        private readonly IDeviceService _deviceService;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IEmployeeService _employeeService;
        private readonly IEnumMethodService _enumMethodService;
        private readonly IFsuService _fsuService;
        private readonly IGroupService _groupService;
        private readonly ILogicTypeService _logicTypeService;
        private readonly IPointService _pointService;
        private readonly IProtocolService _protocolService;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly ISCVendorService _scVendorService;
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
        private readonly IFollowPointService _followPointService;
        private readonly IProfileService _profileService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        /// <summary>
        /// 全局变量（用户相关）
        /// </summary>
        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private U_Role _cachedRole;
        private U_EntitiesInRole _cachedAuthorizations;
        private U_User _cachedUser;
        private U_Employee _cachedEmployee;
        private iProfile _cachedProfile;
        private WsValues _cachedWsValues;
        private TsValues _cachedTsValues;
        private RtValues _cachedRtValues;

        /// <summary>
        /// 全局变量（配置相关）
        /// </summary>
        private List<C_LogicType> _cachedLogicTypes;
        private List<C_SubLogicType> _cachedSubLogicTypes;
        private List<C_DeviceType> _cachedDeviceTypes;
        private List<C_SubDeviceType> _cachedSubDeviceTypes;
        private List<C_RoomType> _cachedRoomTypes;
        private List<C_StationType> _cachedStationTypes;
        private List<C_EnumMethod> _cachedAreaTypes;
        private List<C_SCVendor> _cachedVendors;
        private List<SSHProtocol> _cachedAllProtocols;
        private List<SSHDevice> _cachedAllDevices;
        private List<SSHFsu> _cachedAllFsus;
        private List<SSHRoom> _cachedAllRooms;
        private List<SSHStation> _cachedAllStations;
        private List<SSHArea> _cachedAllAreas;
        private List<SSHArea> _cachedAreas;
        private List<SSHStation> _cachedStations;
        private List<SSHRoom> _cachedRooms;
        private List<SSHFsu> _cachedFsus;
        private List<SSHDevice> _cachedDevices;
        private List<C_Group> _cachedGroups;
        private List<P_Point> _cachedPoints;
        private List<P_SubPoint> _cachedSubPoints;
        private List<P_Point> _cachedAI;
        private List<P_Point> _cachedAO;
        private List<P_Point> _cachedDI;
        private List<P_Point> _cachedDO;
        private List<P_Point> _cachedAL;
        private List<AlmStore<A_AAlarm>> _cachedActAlarms;

        #endregion

        #region Ctor

        public iPemWorkContext(
            HttpContextBase httpContext,
            ICacheManager cacheManager,
            IAreaService areaService,
            IDeviceService deviceService,
            IDeviceTypeService deviceTypeService,
            IEmployeeService employeeService,
            IEnumMethodService enumMethodService,
            IFsuService fsuService,
            IGroupService groupService,
            ILogicTypeService rsLogicTypeService,
            IPointService pointService,
            IProtocolService protocolService,
            IRoomService roomService,
            IRoomTypeService roomTypeService,
            ISCVendorService scVendorService,
            IStationService stationService,
            IStationTypeService stationTypeService,
            IAAlarmService actAlarmService,
            IHIDeviceService iDeviceService,
            IHIStationService iStationService,
            IHIAreaService iAreaService,
            IDictionaryService dictionaryService,
            IEntitiesInRoleService entitiesInRoleService,
            IFollowPointService followPointService,
            IProfileService profileService,
            IRoleService roleService,
            IUserService userService) {
            this._httpContext = httpContext;
            this._cacheManager = cacheManager;
            this._areaService = areaService;
            this._deviceService = deviceService;
            this._deviceTypeService = deviceTypeService;
            this._employeeService = employeeService;
            this._enumMethodService = enumMethodService;
            this._fsuService = fsuService;
            this._groupService = groupService;
            this._logicTypeService = rsLogicTypeService;
            this._pointService = pointService;
            this._protocolService = protocolService;
            this._roomService = roomService;
            this._roomTypeService = roomTypeService;
            this._scVendorService = scVendorService;
            this._stationService = stationService;
            this._stationTypeService = stationTypeService;
            this._actAlarmService = actAlarmService;
            this._iDeviceService = iDeviceService;
            this._iStationService = iStationService;
            this._iAreaService = iAreaService;
            this._dictionaryService = dictionaryService;
            this._entitiesInRoleService = entitiesInRoleService;
            this._followPointService = followPointService;
            this._profileService = profileService;
            this._roleService = roleService;
            this._userService = userService;
        }

        #endregion

        #region Properties

        public Boolean IsAuthenticated {
            get {
                return (_httpContext != null &&
                   _httpContext.Request != null &&
                   _httpContext.Request.IsAuthenticated &&
                   (_httpContext.User.Identity is FormsIdentity));
            }
        }

        public Guid Identifier {
            get {
                if(_cachedIdentifier.HasValue)
                    return _cachedIdentifier.Value;

                if(_httpContext == null ||
                   _httpContext.Request == null || 
                   !_httpContext.Request.IsAuthenticated || 
                   !(_httpContext.User.Identity is FormsIdentity)) {
                       throw new iPemException("Unauthorized");
                }

                var authCookie = _httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if(authCookie == null) throw new iPemException("Cookie not found.");

                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if(ticket == null) throw new iPemException("Encrypted ticket is invalid.");

                _cachedIdentifier = new Guid(ticket.UserData);
                return _cachedIdentifier.Value;
            }
        }

        private Store Store {
            get {
                if(_cachedStore != null) {
                    _cachedStore.ResetExpire();
                    return _cachedStore;
                }
                
                if(!EngineContext.Current.WorkStores.ContainsKey(Identifier)) {
                    _cachedStore = Store.CreateInstance(Identifier);
                } else {
                    _cachedStore = EngineContext.Current.WorkStores[Identifier];
                    _cachedStore.ResetExpire();
                }

                return _cachedStore;
            }
        }

        public U_Role Role {
            get {
                if(_cachedRole != null)
                    return _cachedRole;

                if(Store.Role != null) {
                    _cachedRole = Store.Role;
                    return _cachedRole;
                }

                if(!IsAuthenticated)
                    throw new iPemException("Unauthorized");

                var role = _roleService.GetRoleByUid(User.Id);
                if(role == null)
                    throw new iPemException("Current role not found.");

                Store.Role = _cachedRole = role;

                return _cachedRole;
            }
        }

        public U_User User {
            get {
                if(_cachedUser != null)
                    return _cachedUser;

                if(Store.User != null) {
                    _cachedUser = Store.User;
                    return _cachedUser;
                }

                if(!IsAuthenticated)
                    throw new iPemException("Unauthorized");

                var user = _userService.GetUserByName(_httpContext.User.Identity.Name);
                if(user == null)
                    throw new iPemException("Current user not found.");

                Store.User = _cachedUser = user;

                return _cachedUser;
            }
        }

        public U_Employee Employee {
            get {
                if(_cachedEmployee != null)
                    return _cachedEmployee;

                if(Store.Employee != null) {
                    _cachedEmployee = Store.Employee;
                    return _cachedEmployee;
                }

                Store.Employee = _cachedEmployee = _employeeService.GetEmployeeById(User.EmployeeId);

                return _cachedEmployee;
            }
        }

        public iProfile Profile {
            get {
                if(_cachedProfile != null)
                    return _cachedProfile;

                if(Store.Profile != null) {
                    _cachedProfile = Store.Profile;
                    return _cachedProfile;
                }

                var _profile = new iProfile { FollowPoints = _followPointService.GetFollowPointsInUser(User.Id) };
                var _settings = _profileService.GetProfile(User.Id);
                if (_settings != null && !string.IsNullOrWhiteSpace(_settings.ValuesJson))
                    _profile.Settings = JsonConvert.DeserializeObject<Setting>(_settings.ValuesJson);

                Store.Profile = _cachedProfile = _profile;
                return _cachedProfile;
            }
        }

        public U_EntitiesInRole Authorizations {
            get {
                if (_cachedAuthorizations != null)
                    return _cachedAuthorizations;

                if (Store.Authorizations != null) {
                    _cachedAuthorizations = Store.Authorizations;
                    return _cachedAuthorizations;
                }

                Store.Authorizations = _cachedAuthorizations = _entitiesInRoleService.GetEntitiesInRole(Role.Id);
                return _cachedAuthorizations;
            }
        }

        public DateTime LastNoticeTime {
            get { return this.Store.LastNoticeTime; }
            set { this.Store.LastNoticeTime = value; }
        }

        public DateTime LastSpeechTime {
            get { return this.Store.LastSpeechTime; }
            set { this.Store.LastSpeechTime = value; }
        }

        public DateTime LastLoginTime {
            get { return this.Store.CreatedTime; }
        }

        public WsValues WsValues {
            get {
                if(_cachedWsValues != null)
                    return _cachedWsValues;

                var ws = _dictionaryService.GetDictionary((int)EnmDictionary.Ws);
                if(ws != null && !string.IsNullOrWhiteSpace(ws.ValuesJson))
                    _cachedWsValues = JsonConvert.DeserializeObject<WsValues>(ws.ValuesJson);

                return _cachedWsValues;
            }
        }

        public TsValues TsValues {
            get {
                if(_cachedTsValues != null)
                    return _cachedTsValues;

                var ts = _dictionaryService.GetDictionary((int)EnmDictionary.Ts);
                if(ts != null && !string.IsNullOrWhiteSpace(ts.ValuesJson))
                    _cachedTsValues = JsonConvert.DeserializeObject<TsValues>(ts.ValuesJson);

                return _cachedTsValues;
            }
        }

        public RtValues RtValues {
            get {
                if(_cachedRtValues != null)
                    return _cachedRtValues;

                var rt = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
                if(rt != null && !string.IsNullOrWhiteSpace(rt.ValuesJson))
                    _cachedRtValues = JsonConvert.DeserializeObject<RtValues>(rt.ValuesJson);

                return _cachedRtValues;
            }
        }

        public List<C_LogicType> LogicTypes {
            get {
                if(_cachedLogicTypes != null)
                    return _cachedLogicTypes;

                _cachedLogicTypes = _logicTypeService.GetLogicTypes();
                return _cachedLogicTypes;
            }
        }

        public List<C_SubLogicType> SubLogicTypes {
            get {
                if(_cachedSubLogicTypes != null)
                    return _cachedSubLogicTypes;

                _cachedSubLogicTypes = _logicTypeService.GetSubLogicTypes();
                return _cachedSubLogicTypes;
            }
        }

        public List<C_DeviceType> DeviceTypes {
            get {
                if(_cachedDeviceTypes != null)
                    return _cachedDeviceTypes;

                _cachedDeviceTypes = _deviceTypeService.GetDeviceTypes();
                return _cachedDeviceTypes;
            }
        }

        public List<C_SubDeviceType> SubDeviceTypes {
            get {
                if(_cachedSubDeviceTypes != null)
                    return _cachedSubDeviceTypes;

                _cachedSubDeviceTypes = _deviceTypeService.GetSubDeviceTypes();
                return _cachedSubDeviceTypes;
            }
        }

        public List<C_RoomType> RoomTypes {
            get {
                if(_cachedRoomTypes != null)
                    return _cachedRoomTypes;

                _cachedRoomTypes = _roomTypeService.GetRoomTypes();
                return _cachedRoomTypes;
            }
        }

        public List<C_StationType> StationTypes {
            get {
                if(_cachedStationTypes != null)
                    return _cachedStationTypes;

                _cachedStationTypes = _stationTypeService.GetStationTypes();
                return _cachedStationTypes;
            }
        }

        public List<C_EnumMethod> AreaTypes {
            get {
                if(_cachedAreaTypes != null)
                    return _cachedAreaTypes;

                _cachedAreaTypes = _enumMethodService.GetEnumsByType(EnmMethodType.Area, "类型");
                return _cachedAreaTypes;
            }
        }

        public List<C_SCVendor> Vendors {
            get {
                if (_cachedVendors != null)
                    return _cachedVendors;

                _cachedVendors = _scVendorService.GetVendors();
                return _cachedVendors;
            }
        }

        public List<SSHProtocol> AllProtocols {
            get {
                if(_cachedAllProtocols != null)
                    return _cachedAllProtocols;

                if(_cacheManager.IsSet(GlobalCacheKeys.SSH_Protocols))
                    return _cachedAllProtocols = _cacheManager.Get<List<SSHProtocol>>(GlobalCacheKeys.SSH_Protocols);

                var protocols = _protocolService.GetProtocols();
                _cachedAllProtocols = new List<SSHProtocol>();
                foreach(var protocol in protocols) {
                    var points = _pointService.GetPointsInProtocol(protocol.Id);
                    _cachedAllProtocols.Add(new SSHProtocol {
                        Current = protocol,
                        Points = points
                    });
                }

                _cacheManager.Set<List<SSHProtocol>>(GlobalCacheKeys.SSH_Protocols, _cachedAllProtocols, CachedIntervals.Global_Default_Intervals);
                return _cachedAllProtocols;
            }
        }

        public List<SSHDevice> AllDevices {
            get {
                if(_cachedAllDevices != null)
                    return _cachedAllDevices;

                if(_cacheManager.IsSet(GlobalCacheKeys.SSH_Devices))
                    return _cachedAllDevices = _cacheManager.Get<List<SSHDevice>>(GlobalCacheKeys.SSH_Devices);

                _cachedAllDevices = new List<SSHDevice>();
                var devices = _deviceService.GetDevices();
                _cachedAllDevices = (from device in devices
                                     join protocol in this.AllProtocols on device.ProtocolId equals protocol.Current.Id
                                     select new SSHDevice { Current = device, Protocol = protocol }).ToList();

                _cacheManager.Set<List<SSHDevice>>(GlobalCacheKeys.SSH_Devices, _cachedAllDevices, CachedIntervals.Global_Default_Intervals);
                return _cachedAllDevices;
            }
        }

        public List<SSHFsu> AllFsus {
            get {
                if(_cachedAllFsus != null)
                    return _cachedAllFsus;

                if(_cacheManager.IsSet(GlobalCacheKeys.SSH_Fsus))
                    return _cachedAllFsus = _cacheManager.Get<List<SSHFsu>>(GlobalCacheKeys.SSH_Fsus);

                var fsus = _fsuService.GetFsus();
                var devices = from device in this.AllDevices
                              group device by device.Current.FsuId into g
                              select new {
                                  Id = g.Key,
                                  Devices = g
                              };

                _cachedAllFsus = (from fsu in fsus
                               join dev in devices on fsu.Id equals dev.Id into lt
                               from def in lt.DefaultIfEmpty()
                               select new SSHFsu {
                                   Current = fsu,
                                   Devices = def != null ? def.Devices.ToList() : new List<SSHDevice>()
                               }).ToList();

                _cacheManager.Set<List<SSHFsu>>(GlobalCacheKeys.SSH_Fsus, _cachedAllFsus, CachedIntervals.Global_Default_Intervals);
                return _cachedAllFsus;
            }
        }

        public List<SSHRoom> AllRooms {
            get {
                if(_cachedAllRooms != null)
                    return _cachedAllRooms;

                if(_cacheManager.IsSet(GlobalCacheKeys.SSH_Rooms))
                    return _cachedAllRooms = _cacheManager.Get<List<SSHRoom>>(GlobalCacheKeys.SSH_Rooms);

                var rooms = _roomService.GetRooms();
                var dsets = from device in this.AllDevices
                            group device by device.Current.RoomId into g
                            select new {
                                Id = g.Key,
                                Devices = g
                            };

                var fsets = from fsu in this.AllFsus
                            group fsu by fsu.Current.RoomId into g
                            select new {
                                Id = g.Key,
                                Fsus = g
                            };

                _cachedAllRooms = (from room in rooms
                                join ds in dsets on room.Id equals ds.Id into lt1
                                from def1 in lt1.DefaultIfEmpty()
                                join fs in fsets on room.Id equals fs.Id into lt2
                                from def2 in lt2.DefaultIfEmpty()
                                select new SSHRoom {
                                    Current = room,
                                    Devices = def1 != null ? def1.Devices.ToList() : new List<SSHDevice>(),
                                    Fsus = def2 != null ? def2.Fsus.ToList() : new List<SSHFsu>()
                                }).ToList();

                _cacheManager.Set<List<SSHRoom>>(GlobalCacheKeys.SSH_Rooms, _cachedAllRooms, CachedIntervals.Global_Default_Intervals);
                return _cachedAllRooms;
            }
        }

        public List<SSHStation> AllStations {
            get {
                if(_cachedAllStations != null)
                    return _cachedAllStations;

                if(_cacheManager.IsSet(GlobalCacheKeys.SSH_Stations))
                    return _cachedAllStations = _cacheManager.Get<List<SSHStation>>(GlobalCacheKeys.SSH_Stations);

                var stations = _stationService.GetStations();
                var rsets = from room in this.AllRooms
                            group room by room.Current.StationId into g
                            select new {
                                Id = g.Key,
                                Rooms = g
                            };

                _cachedAllStations = (from station in stations
                                   join rs in rsets on station.Id equals rs.Id into lt
                                   from def in lt.DefaultIfEmpty()
                                   select new SSHStation {
                                       Current = station,
                                       Rooms = def != null ? def.Rooms.ToList() : new List<SSHRoom>()
                                   }).ToList();

                _cacheManager.Set<List<SSHStation>>(GlobalCacheKeys.SSH_Stations, _cachedAllStations, CachedIntervals.Global_Default_Intervals);
                return _cachedAllStations;
            }
        }

        public List<SSHArea> AllAreas {
            get {
                if(_cachedAllAreas != null)
                    return _cachedAllAreas;

                if(_cacheManager.IsSet(GlobalCacheKeys.SSH_Areas))
                    return _cachedAllAreas = _cacheManager.Get<List<SSHArea>>(GlobalCacheKeys.SSH_Areas);

                var areas = _areaService.GetAreas();
                var ssets = from station in this.AllStations
                            group station by station.Current.AreaId into g
                            select new {
                                Id = g.Key,
                                Stations = g
                            };

                _cachedAllAreas = (from area in areas
                                join ss in ssets on area.Id equals ss.Id into lt
                                from def in lt.DefaultIfEmpty()
                                select new SSHArea {
                                    Current = area,
                                    Stations = def != null ? def.Stations.ToList() : new List<SSHStation>()
                                }).ToList();

                foreach(var current in _cachedAllAreas) {
                    current.Initializer(_cachedAllAreas);
                }

                _cacheManager.Set<List<SSHArea>>(GlobalCacheKeys.SSH_Areas, _cachedAllAreas, CachedIntervals.Global_Default_Intervals);
                return _cachedAllAreas;
            }
        }

        public List<SSHArea> Areas {
            get {
                if(this.Role.Id == U_Role.SuperId)
                    return this.AllAreas;

                if(_cachedAreas != null)
                    return _cachedAreas;

                var cachedKey = string.Format(GlobalCacheKeys.SSH_AreasPattern, this.Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedAreas = _cacheManager.Get<List<SSHArea>>(cachedKey);

                var authorizations = this.Authorizations.Areas;
                if (authorizations.Count == 0) throw new iPemException("No Authorizations.");

                var areas = _areaService.GetAreas();
                var ssets = from station in this.AllStations
                            group station by station.Current.AreaId into g
                            select new { Id = g.Key, Stations = g };

                _cachedAreas = (from area in areas
                                    join permisson in authorizations on area.Id equals permisson
                                    join ss in ssets on area.Id equals ss.Id into lt
                                    from def in lt.DefaultIfEmpty()
                                    select new SSHArea {
                                        Current = area,
                                        Stations = def != null ? def.Stations.ToList() : new List<SSHStation>()
                                    }).ToList();

                foreach(var current in _cachedAreas) {
                    current.Initializer(_cachedAreas);
                }

                _cacheManager.Set<List<SSHArea>>(cachedKey, _cachedAreas, CachedIntervals.Global_Default_Intervals);
                return _cachedAreas;
            }
        }

        public List<SSHStation> Stations {
            get {
                if(this.Role.Id == U_Role.SuperId)
                    return this.AllStations;

                if(_cachedStations != null)
                    return _cachedStations;

                var cachedKey = string.Format(GlobalCacheKeys.SSH_StationsPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedStations = _cacheManager.Get<List<SSHStation>>(cachedKey);

                _cachedStations = this.Areas.SelectMany(a => a.Stations).ToList();
                _cacheManager.Set<List<SSHStation>>(cachedKey, _cachedStations, CachedIntervals.Global_Default_Intervals);
                return _cachedStations;
            }
        }

        public List<SSHRoom> Rooms {
            get {
                if(this.Role.Id == U_Role.SuperId)
                    return this.AllRooms;

                if(_cachedRooms != null)
                    return _cachedRooms;

                var cachedKey = string.Format(GlobalCacheKeys.SSH_RoomsPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedRooms = _cacheManager.Get<List<SSHRoom>>(cachedKey);

                _cachedRooms = this.Stations.SelectMany(s => s.Rooms).ToList();
                _cacheManager.Set<List<SSHRoom>>(cachedKey, _cachedRooms, CachedIntervals.Global_Default_Intervals);
                return _cachedRooms;
            }
        }

        public List<SSHFsu> Fsus {
            get {
                if(this.Role.Id == U_Role.SuperId)
                    return this.AllFsus;

                if(_cachedFsus != null)
                    return _cachedFsus;

                var cachedKey = string.Format(GlobalCacheKeys.SSH_FsusPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedFsus = _cacheManager.Get<List<SSHFsu>>(cachedKey);

                _cachedFsus = this.Rooms.SelectMany(s => s.Fsus).ToList();
                _cacheManager.Set<List<SSHFsu>>(cachedKey, _cachedFsus, CachedIntervals.Global_Default_Intervals);
                return _cachedFsus;
            }
        }

        public List<SSHDevice> Devices {
            get {
                if(this.Role.Id == U_Role.SuperId)
                    return this.AllDevices;

                if(_cachedDevices != null)
                    return _cachedDevices;

                var cachedKey = string.Format(GlobalCacheKeys.SSH_DevicesPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedDevices = _cacheManager.Get<List<SSHDevice>>(cachedKey);

                _cachedDevices = this.Rooms.SelectMany(s => s.Devices).ToList();
                _cacheManager.Set<List<SSHDevice>>(cachedKey, _cachedDevices, CachedIntervals.Global_Default_Intervals);
                return _cachedDevices;
            }
        }

        public List<C_Group> Groups {
            get {
                if (_cachedGroups != null)
                    return _cachedGroups;

                if (_cacheManager.IsSet(GlobalCacheKeys.SSH_Groups))
                    return _cachedGroups = _cacheManager.Get<List<C_Group>>(GlobalCacheKeys.SSH_Groups);

                _cachedGroups = _groupService.GetGroups();
                _cacheManager.Set<List<C_Group>>(GlobalCacheKeys.SSH_Groups, _cachedGroups, CachedIntervals.Global_Default_Intervals);
                return _cachedGroups;
            }
        }

        public List<P_Point> Points {
            get {
                if (_cachedPoints != null)
                    return _cachedPoints;

                if (_cacheManager.IsSet(GlobalCacheKeys.SSH_Points))
                    return _cachedPoints = _cacheManager.Get<List<P_Point>>(GlobalCacheKeys.SSH_Points);

                _cachedPoints = _pointService.GetPoints();
                _cacheManager.Set<List<P_Point>>(GlobalCacheKeys.SSH_Points, _cachedPoints, CachedIntervals.Global_Default_Intervals);
                return _cachedPoints;
            }
        }

        public List<P_SubPoint> SubPoints {
            get {
                if (_cachedSubPoints != null)
                    return _cachedSubPoints;

                if (_cacheManager.IsSet(GlobalCacheKeys.SSH_SubPoints))
                    return _cachedSubPoints = _cacheManager.Get<List<P_SubPoint>>(GlobalCacheKeys.SSH_SubPoints);

                _cachedSubPoints = _pointService.GetSubPoints();
                _cacheManager.Set<List<P_SubPoint>>(GlobalCacheKeys.SSH_SubPoints, _cachedSubPoints, CachedIntervals.Global_Default_Intervals);
                return _cachedSubPoints;
            }
        }

        public List<P_Point> AI {
            get {
                if (_cachedAI != null)
                    return _cachedAI;

                _cachedAI = this.Points.FindAll(p => p.Type == EnmPoint.AI);
                return _cachedAI;
            }
        }

        public List<P_Point> AO {
            get {
                if (_cachedAO != null)
                    return _cachedAO;

                _cachedAO = this.Points.FindAll(p => p.Type == EnmPoint.AO);
                return _cachedAO;
            }
        }

        public List<P_Point> DI {
            get {
                if (_cachedDI != null)
                    return _cachedDI;

                _cachedDI = this.Points.FindAll(p => p.Type == EnmPoint.DI);
                return _cachedDI;
            }
        }

        public List<P_Point> DO {
            get {
                if (_cachedDO != null)
                    return _cachedDO;

                _cachedDO = this.Points.FindAll(p => p.Type == EnmPoint.DO);
                return _cachedDO;
            }
        }

        public List<P_Point> AL {
            get {
                if (_cachedAL != null)
                    return _cachedAL;

                _cachedAL = this.Points.FindAll(p => p.Type == EnmPoint.DI && !string.IsNullOrWhiteSpace(p.AlarmId));
                return _cachedAL;
            }
        }

        public List<AlmStore<A_AAlarm>> ActAlarms {
            get {
                if(_cachedActAlarms != null)
                    return _cachedActAlarms;

                //实时告警缓存10s，减小服务器压力
                List<AlmStore<A_AAlarm>> _activeAlarms = null;
                if (_cacheManager.IsSet(GlobalCacheKeys.Global_ActiveAlarms)) {
                    _activeAlarms = _cacheManager.Get<List<AlmStore<A_AAlarm>>>(GlobalCacheKeys.Global_ActiveAlarms);
                } else {
                    var allAlarms = _actAlarmService.GetAlarms();
                    var sysAlarms = allAlarms.FindAll(a => a.RoomId == "-1");
                    var nomAlarms = from alarm in allAlarms
                                    join point in this.AL on alarm.PointId equals point.Id
                                    join device in this.AllDevices on alarm.DeviceId equals device.Current.Id
                                    join room in this.AllRooms on device.Current.RoomId equals room.Current.Id
                                    join station in this.AllStations on room.Current.StationId equals station.Current.Id
                                    join area in this.AllAreas on station.Current.AreaId equals area.Current.Id
                                    select new AlmStore<A_AAlarm> {
                                        Current = alarm,
                                        Point = point,
                                        Device = device.Current,
                                        Room = room.Current,
                                        Station = station.Current,
                                        Area = new A_Area {
                                            Id = area.Current.Id,
                                            Code = area.Current.Code,
                                            Name = area.ToString(),
                                            Type = area.Current.Type,
                                            ParentId = area.Current.ParentId,
                                            Comment = area.Current.Comment,
                                            Enabled = area.Current.Enabled
                                        }
                                    };

                    if (sysAlarms.Count > 0) {
                        _activeAlarms = nomAlarms.Concat(from alarm in sysAlarms
                                                         join point in this.AL on alarm.PointId equals point.Id
                                                         join gp in this.Groups on alarm.DeviceId equals gp.Id
                                                         select new AlmStore<A_AAlarm> {
                                                             Current = alarm,
                                                             Point = point,
                                                             Device = SSHSystem.SC(gp.Id, gp.Name),
                                                             Room = SSHSystem.Room,
                                                             Station = SSHSystem.Station,
                                                             Area = SSHSystem.Area
                                                         }).OrderByDescending(a => a.Current.AlarmTime).ToList();
                    } else {
                        _activeAlarms = nomAlarms.OrderByDescending(a => a.Current.AlarmTime).ToList();
                    }
                    
                    _cacheManager.Set<List<AlmStore<A_AAlarm>>>(GlobalCacheKeys.Global_ActiveAlarms, _activeAlarms, CachedIntervals.Global_ActiveAlarm_Intervals);
                }

                if (this.Role.Id == U_Role.SuperId) {
                    return _cachedActAlarms = _activeAlarms;
                } else {
                    var keys = new HashSet<string>(this.Areas.Select(a => a.Current.Id));
                    return _cachedActAlarms = _activeAlarms.FindAll(a => keys.Contains(a.Area.Id));
                }
            }
        }

        #endregion

        #region Methods

        public void ResetRole() {
            this.Store.Role = null;
        }

        public void ResetUser() {
            this.Store.User = null;
        }

        public void ResetEmployee() {
            this.Store.Employee = null;
        }

        public void ResetProfile() {
            this.Store.Profile = null;
        }

        public void ResetAuthorizations() {
            this.Store.Authorizations = null;
        }

        public List<iSSHDevice> iDevices(DateTime date) {
            var key = string.Format(GlobalCacheKeys.SSH_iDevicesPattern, date.ToString("yyyyMM"));
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<iSSHDevice>>(key);

            var iAreas = _iAreaService.GetAreas(date);
            var iStations = _iStationService.GetStations(date);
            var iDevices = _iDeviceService.GetDevices(date);
            var iFull = (from iDevice in iDevices
                         join iStation in iStations on iDevice.StationId equals iStation.Id
                         join iArea in iAreas on iStation.AreaId equals iArea.Id
                         select new iSSHDevice { Current = iDevice, iStation = iStation, iArea = iArea }).ToList();

            _cacheManager.Set<List<iSSHDevice>>(key, iFull, CachedIntervals.Global_Default_Intervals);
            return iFull;
        }

        public List<iSSHStation> iStations(DateTime date) {
            var key = string.Format(GlobalCacheKeys.SSH_iStationsPattern, date.ToString("yyyyMM"));
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<iSSHStation>>(key);

            var iAreas = _iAreaService.GetAreas(date);
            var iStations = _iStationService.GetStations(date);
            var iDevices = _iDeviceService.GetDevices(date);
            var iFull = (from iStation in iStations
                         join iArea in iAreas on iStation.AreaId equals iArea.Id
                         select new iSSHStation { Current = iStation, iArea = iArea }).ToList();

            _cacheManager.Set<List<iSSHStation>>(key, iFull, CachedIntervals.Global_Default_Intervals);
            return iFull;
        }

        public List<iSSHArea> iAreas(DateTime date) {
            var key = string.Format(GlobalCacheKeys.SSH_iAreasPattern, date.ToString("yyyyMM"));
            if (_cacheManager.IsSet(key)) return _cacheManager.Get<List<iSSHArea>>(key);

            var iAreas = _iAreaService.GetAreas(date).Select(a => new iSSHArea { Current = a }).ToList();
            _cacheManager.Set<List<iSSHArea>>(key, iAreas, CachedIntervals.Global_Default_Intervals);
            return iAreas;
        }

        public List<AlmStore<A_AAlarm>> AlarmsToStore(List<A_AAlarm> alarms) {
            if (alarms == null || alarms.Count == 0)
                return new List<AlmStore<A_AAlarm>>();

            return (from alarm in alarms
                    join point in this.AL on alarm.PointId equals point.Id
                    join device in this.Devices on alarm.DeviceId equals device.Current.Id
                    join room in this.Rooms on device.Current.RoomId equals room.Current.Id
                    join station in this.Stations on room.Current.StationId equals station.Current.Id
                    join area in this.Areas on station.Current.AreaId equals area.Current.Id
                    orderby alarm.AlarmTime descending
                    select new AlmStore<A_AAlarm> {
                        Current = alarm,
                        Point = point,
                        Device = device.Current,
                        Room = room.Current,
                        Station = station.Current,
                        Area = new A_Area {
                            Id = area.Current.Id,
                            Code = area.Current.Code,
                            Name = area.ToString(),
                            Type = area.Current.Type,
                            ParentId = area.Current.ParentId,
                            Comment = area.Current.Comment,
                            Enabled = area.Current.Enabled
                        }
                    }).ToList();
        }

        public List<AlmStore<A_HAlarm>> AlarmsToStore(List<A_HAlarm> alarms) {
            if(alarms == null || alarms.Count == 0) 
                return new List<AlmStore<A_HAlarm>>();

            return (from alarm in alarms
                    join point in this.AL on alarm.PointId equals point.Id
                    join device in this.Devices on alarm.DeviceId equals device.Current.Id
                    join room in this.Rooms on device.Current.RoomId equals room.Current.Id
                    join station in this.Stations on room.Current.StationId equals station.Current.Id
                    join area in this.Areas on station.Current.AreaId equals area.Current.Id
                    orderby alarm.StartTime descending
                    select new AlmStore<A_HAlarm> {
                        Current = alarm,
                        Point = point,
                        Device = device.Current,
                        Room = room.Current,
                        Station = station.Current,
                        Area = new A_Area {
                            Id = area.Current.Id,
                            Code = area.Current.Code,
                            Name = area.ToString(),
                            Type = area.Current.Type,
                            ParentId = area.Current.ParentId,
                            Comment = area.Current.Comment,
                            Enabled = area.Current.Enabled
                        }
                    }).ToList();
        }

        public Dictionary<string, AlmStore<A_AAlarm>> AlarmsToDictionary(List<AlmStore<A_AAlarm>> alarms, bool primaryKey = true) {
            var dictionary = new Dictionary<string, AlmStore<A_AAlarm>>();
            if (alarms == null || alarms.Count == 0)
                return dictionary;

            foreach (var alarm in alarms) {
                dictionary[primaryKey ? alarm.Current.Id : Common.JoinKeys(alarm.Device.Id, alarm.Point.Id)] = alarm;
            }

            return dictionary;
        }

        public Dictionary<string, AlmStore<A_HAlarm>> AlarmsToDictionary(List<AlmStore<A_HAlarm>> alarms, bool primaryKey = true) {
            var dictionary = new Dictionary<string, AlmStore<A_HAlarm>>();
            if (alarms == null || alarms.Count == 0)
                return dictionary;

            foreach (var alarm in alarms) {
                dictionary[primaryKey ? alarm.Current.Id : Common.JoinKeys(alarm.Device.Id, alarm.Point.Id)] = alarm;
            }

            return dictionary;
        }

        #endregion

    }
}