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
using iPem.Site.Models.Organization;
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

        private readonly HttpContextBase _httpContext;
        private readonly ICacheManager _cacheManager;
        //rs repository
        private readonly IAreaService _areaService;
        private readonly IDeviceService _deviceService;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IEmployeeService _employeeService;
        private readonly IEnumMethodsService _enumMethodsService;
        private readonly IExtendAlmService _extendAlmService;
        private readonly IFsuService _fsuService;
        private readonly ILogicTypeService _logicTypeService;
        private readonly IPointService _pointService;
        private readonly IProtocolService _protocolService;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IStationService _stationService;
        private readonly IStationTypeService _stationTypeService;
        //cs repository
        private readonly IFsuKeyService _fsuKeyService;
        private readonly IActAlmService _actAlmService;
        //sc repository
        private readonly IAreasInRoleService _areasInRoleService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IMenuService _menuService;
        private readonly IOperateInRoleService _operateService;
        private readonly IProfileService _profileService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private Role _cachedRole;
        private User _cachedUser;
        private Employee _cachedEmployee;
        private ProfileValues _cachedProfile;
        private WsValues _cachedWsValues;
        private TsValues _cachedTsValues;
        private RtValues _cachedRtValues;
        private List<Menu> _cachedMenus;
        private HashSet<EnmOperation> _cachedOperations;

        private List<LogicType> _cachedLogicTypes;
        private List<SubLogicType> _cachedSubLogicTypes;
        private List<DeviceType> _cachedDeviceTypes;
        private List<SubDeviceType> _cachedSubDeviceTypes;
        private List<RoomType> _cachedRoomTypes;
        private List<StationType> _cachedStationTypes;
        private List<EnumMethods> _cachedAreaTypes;
        private List<Point> _cachedPoints;
        private List<OrgProtocol> _cachedProtocols;
        private List<OrgDevice> _cachedDevices;
        private List<OrgFsu> _cachedFsus;
        private List<OrgRoom> _cachedRooms;
        private List<OrgStation> _cachedStations;
        private List<OrgArea> _cachedAreas;
        private List<OrgArea> _cachedRoleAreas;
        private List<OrgStation> _cachedRoleStations;
        private List<OrgRoom> _cachedRoleRooms;
        private List<OrgFsu> _cachedRoleFsus;
        private List<OrgDevice> _cachedRoleDevices;
        private List<AlmStore<ActAlm>> _cachedAlmStore;

        #endregion

        #region Ctor

        public iPemWorkContext(
        HttpContextBase httpContext,
        ICacheManager cacheManager,
        //rs repository
        IAreaService areaService,
        IDeviceService deviceService,
        IDeviceTypeService deviceTypeService,
        IEmployeeService employeeService,
        IEnumMethodsService enumMethodsService,
        IExtendAlmService extendAlmService,
        IFsuService fsuService,
        ILogicTypeService rsLogicTypeService,
        IPointService pointService,
        IProtocolService protocolService,
        IRoomService roomService,
        IRoomTypeService roomTypeService,
        IStationService stationService,
        IStationTypeService stationTypeService,
        //cs repository
        IFsuKeyService fsuKeyService,
        IActAlmService actAlmService,
        //sc repository
        IAreasInRoleService areasInRoleService,
        IDictionaryService dictionaryService,
        IMenuService menuService,
        IOperateInRoleService operateService,
        IProfileService profileService,
        IRoleService roleService,
        IUserService userService) {
            this._httpContext = httpContext;
            this._cacheManager = cacheManager;
            //rs repository
            this._areaService = areaService;
            this._deviceService = deviceService;
            this._deviceTypeService = deviceTypeService;
            this._employeeService = employeeService;
            this._enumMethodsService = enumMethodsService;
            this._extendAlmService = extendAlmService;
            this._fsuService = fsuService;
            this._logicTypeService = rsLogicTypeService;
            this._pointService = pointService;
            this._protocolService = protocolService;
            this._roomService = roomService;
            this._roomTypeService = roomTypeService;
            this._stationService = stationService;
            this._stationTypeService = stationTypeService;
            //cs repository
            this._fsuKeyService = fsuKeyService;
            this._actAlmService = actAlmService;
            //sc repository
            this._areasInRoleService = areasInRoleService;
            this._dictionaryService = dictionaryService;
            this._menuService = menuService;
            this._operateService = operateService;
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
                if(authCookie == null)
                    throw new iPemException("Cookie not found.");

                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if(ticket == null)
                    throw new iPemException("Encrypted ticket is invalid.");

                _cachedIdentifier = new Guid(ticket.UserData);
                return _cachedIdentifier.Value;
            }
        }

        public Store Store {
            get {
                var now = DateTime.UtcNow;
                if(_cachedStore != null) {
                    _cachedStore.ExpireUtc = now.Add(CachedIntervals.Store_Intervals);
                    return _cachedStore;
                }
                
                if(!EngineContext.Current.WorkStores.ContainsKey(Identifier)) {
                    _cachedStore = new Store {
                        Id = Identifier,
                        ExpireUtc = now.Add(CachedIntervals.Store_Intervals),
                        CreatedUtc = now
                    };

                    EngineContext.Current.WorkStores[Identifier] = _cachedStore;
                } else {
                    _cachedStore = EngineContext.Current.WorkStores[Identifier];
                    _cachedStore.ExpireUtc = now.Add(CachedIntervals.Store_Intervals);
                }

                return _cachedStore;
            }
        }

        public Role Role {
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

        public User User {
            get {
                if(_cachedUser != null)
                    return _cachedUser;

                if(Store.User != null) {
                    _cachedUser = Store.User;
                    return _cachedUser;
                }

                if(!IsAuthenticated)
                    throw new iPemException("Unauthorized");

                var user = _userService.GetUser(_httpContext.User.Identity.Name);
                if(user == null)
                    throw new iPemException("Current user not found.");

                Store.User = _cachedUser = user;

                return _cachedUser;
            }
        }

        public Employee Employee {
            get {
                if(_cachedEmployee != null)
                    return _cachedEmployee;

                if(Store.Employee != null) {
                    _cachedEmployee = Store.Employee;
                    return _cachedEmployee;
                }

                Store.Employee = _cachedEmployee = _employeeService.GetEmpolyee(User.EmployeeId);

                return _cachedEmployee;
            }
        }

        public ProfileValues Profile {
            get {
                if(_cachedProfile != null)
                    return _cachedProfile;

                if(Store.Profile != null) {
                    _cachedProfile = Store.Profile;
                    return _cachedProfile;
                }

                var _profile = _profileService.GetProfile(User.Id);
                if(_profile != null && !string.IsNullOrWhiteSpace(_profile.ValuesJson))
                    Store.Profile = _cachedProfile = JsonConvert.DeserializeObject<ProfileValues>(_profile.ValuesJson);

                return _cachedProfile;
            }
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

        public List<Menu> Menus {
            get {
                if(_cachedMenus != null)
                    return _cachedMenus;

                return _cachedMenus = _menuService.GetMenusAsList(Role.Id);
            }
        }

        public HashSet<EnmOperation> Operations {
            get {
                if(_cachedOperations != null)
                    return _cachedOperations;

                var operations = _operateService.GetOperates(Role.Id);
                _cachedOperations = new HashSet<EnmOperation>();
                foreach(var entity in operations.Operates) {
                    _cachedOperations.Add(entity);
                }

                return _cachedOperations;
            }
        }

        public List<LogicType> LogicTypes {
            get {
                if(_cachedLogicTypes != null)
                    return _cachedLogicTypes;

                _cachedLogicTypes = _logicTypeService.GetAllLogicTypesAsList();
                return _cachedLogicTypes;
            }
        }

        public List<SubLogicType> SubLogicTypes {
            get {
                if(_cachedSubLogicTypes != null)
                    return _cachedSubLogicTypes;

                _cachedSubLogicTypes = _logicTypeService.GetAllSubLogicTypesAsList();
                return _cachedSubLogicTypes;
            }
        }

        public List<DeviceType> DeviceTypes {
            get {
                if(_cachedDeviceTypes != null)
                    return _cachedDeviceTypes;

                _cachedDeviceTypes = _deviceTypeService.GetAllDeviceTypesAsList();
                return _cachedDeviceTypes;
            }
        }

        public List<SubDeviceType> SubDeviceTypes {
            get {
                if(_cachedSubDeviceTypes != null)
                    return _cachedSubDeviceTypes;

                _cachedSubDeviceTypes = _deviceTypeService.GetAllSubDeviceTypesAsList();
                return _cachedSubDeviceTypes;
            }
        }

        public List<RoomType> RoomTypes {
            get {
                if(_cachedRoomTypes != null)
                    return _cachedRoomTypes;

                _cachedRoomTypes = _roomTypeService.GetAllRoomTypesAsList();
                return _cachedRoomTypes;
            }
        }

        public List<StationType> StationTypes {
            get {
                if(_cachedStationTypes != null)
                    return _cachedStationTypes;

                _cachedStationTypes = _stationTypeService.GetAllStationTypesAsList();
                return _cachedStationTypes;
            }
        }

        public List<EnumMethods> AreaTypes {
            get {
                if(_cachedAreaTypes != null)
                    return _cachedAreaTypes;

                _cachedAreaTypes = _enumMethodsService.GetValuesAsList(EnmMethodType.Area, "类型");
                return _cachedAreaTypes;
            }
        }

        public List<Point> Points {
            get {
                if(_cachedPoints != null)
                    return _cachedPoints;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Points))
                    return _cachedPoints = _cacheManager.Get<List<Point>>(GlobalCacheKeys.Og_Points);

                _cachedPoints = _pointService.GetAllPointsAsList();
                _cacheManager.Set<List<Point>>(GlobalCacheKeys.Og_Points, _cachedPoints, CachedIntervals.Global_Intervals);
                return _cachedPoints;
            }
        }

        public List<OrgProtocol> Protocols {
            get {
                if(_cachedProtocols != null)
                    return _cachedProtocols;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Protocols))
                    return _cachedProtocols = _cacheManager.Get<List<OrgProtocol>>(GlobalCacheKeys.Og_Protocols);

                var protocols = _protocolService.GetAllProtocolsAsList();
                _cachedProtocols = new List<OrgProtocol>();
                foreach(var protocol in protocols) {
                    var points = _pointService.GetPointsInProtocolAsList(protocol.Id);
                    _cachedProtocols.Add(new OrgProtocol {
                        Current = protocol,
                        Points = points
                    });
                }

                _cacheManager.Set<List<OrgProtocol>>(GlobalCacheKeys.Og_Protocols, _cachedProtocols, CachedIntervals.Global_Intervals);
                return _cachedProtocols;
            }
        }

        public List<OrgDevice> Devices {
            get {
                if(_cachedDevices != null)
                    return _cachedDevices;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Devices))
                    return _cachedDevices = _cacheManager.Get<List<OrgDevice>>(GlobalCacheKeys.Og_Devices);

                var devices = _deviceService.GetAllDevicesAsList();
                _cachedDevices = (from device in devices
                                  join protocol in this.Protocols on device.ProtocolId equals protocol.Current.Id
                                  select new OrgDevice {
                                      Current = device,
                                      Protocol = protocol
                                  }).ToList();

                _cacheManager.Set<List<OrgDevice>>(GlobalCacheKeys.Og_Devices, _cachedDevices, CachedIntervals.Global_Intervals);
                return _cachedDevices;
            }
        }

        public List<OrgFsu> Fsus {
            get {
                if(_cachedFsus != null)
                    return _cachedFsus;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Fsus))
                    return _cachedFsus = _cacheManager.Get<List<OrgFsu>>(GlobalCacheKeys.Og_Fsus);

                var fsus = _fsuService.GetAllFsusAsList();
                var devices = from device in this.Devices
                              group device by device.Current.FsuId into g
                              select new {
                                  Id = g.Key,
                                  Devices = g
                              };

                _cachedFsus = (from fsu in fsus
                               join dev in devices on fsu.Id equals dev.Id into lt
                               from def in lt.DefaultIfEmpty()
                               select new OrgFsu {
                                   Current = fsu,
                                   Devices = def != null ? def.Devices.ToList() : new List<OrgDevice>()
                               }).ToList();

                _cacheManager.Set<List<OrgFsu>>(GlobalCacheKeys.Og_Fsus, _cachedFsus, CachedIntervals.Global_Intervals);
                return _cachedFsus;
            }
        }

        public List<OrgRoom> Rooms {
            get {
                if(_cachedRooms != null)
                    return _cachedRooms;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Rooms))
                    return _cachedRooms = _cacheManager.Get<List<OrgRoom>>(GlobalCacheKeys.Og_Rooms);

                var rooms = _roomService.GetAllRoomsAsList();
                var dsets = from device in this.Devices
                            group device by device.Current.RoomId into g
                            select new {
                                Id = g.Key,
                                Devices = g
                            };

                var fsets = from fsu in this.Fsus
                            group fsu by fsu.Current.RoomId into g
                            select new {
                                Id = g.Key,
                                Fsus = g
                            };

                _cachedRooms = (from room in rooms
                                join ds in dsets on room.Id equals ds.Id into lt1
                                from def1 in lt1.DefaultIfEmpty()
                                join fs in fsets on room.Id equals fs.Id into lt2
                                from def2 in lt2.DefaultIfEmpty()
                                select new OrgRoom {
                                    Current = room,
                                    Devices = def1 != null ? def1.Devices.ToList() : new List<OrgDevice>(),
                                    Fsus = def2 != null ? def2.Fsus.ToList() : new List<OrgFsu>()
                                }).ToList();

                _cacheManager.Set<List<OrgRoom>>(GlobalCacheKeys.Og_Rooms, _cachedRooms, CachedIntervals.Global_Intervals);
                return _cachedRooms;
            }
        }

        public List<OrgStation> Stations {
            get {
                if(_cachedStations != null)
                    return _cachedStations;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Stations))
                    return _cachedStations = _cacheManager.Get<List<OrgStation>>(GlobalCacheKeys.Og_Stations);

                var stations = _stationService.GetAllStationsAsList();
                var rsets = from room in this.Rooms
                            group room by room.Current.StationId into g
                            select new {
                                Id = g.Key,
                                Rooms = g
                            };

                _cachedStations = (from station in stations
                                   join rs in rsets on station.Id equals rs.Id into lt
                                   from def in lt.DefaultIfEmpty()
                                   select new OrgStation {
                                       Current = station,
                                       Rooms = def != null ? def.Rooms.ToList() : new List<OrgRoom>()
                                   }).ToList();

                _cacheManager.Set<List<OrgStation>>(GlobalCacheKeys.Og_Stations, _cachedStations, CachedIntervals.Global_Intervals);
                return _cachedStations;
            }
        }

        public List<OrgArea> Areas {
            get {
                if(_cachedAreas != null)
                    return _cachedAreas;

                if(_cacheManager.IsSet(GlobalCacheKeys.Og_Areas))
                    return _cachedAreas = _cacheManager.Get<List<OrgArea>>(GlobalCacheKeys.Og_Areas);

                var areas = _areaService.GetAreasAsList();
                var ssets = from station in this.Stations
                            group station by station.Current.AreaId into g
                            select new {
                                Id = g.Key,
                                Stations = g
                            };

                _cachedAreas = (from area in areas
                                join ss in ssets on area.Id equals ss.Id into lt
                                from def in lt.DefaultIfEmpty()
                                select new OrgArea {
                                    Current = area,
                                    Stations = def != null ? def.Stations.ToList() : new List<OrgStation>()
                                }).ToList();

                foreach(var current in _cachedAreas) {
                    current.Initializer(_cachedAreas);
                }

                _cacheManager.Set<List<OrgArea>>(GlobalCacheKeys.Og_Areas, _cachedAreas, CachedIntervals.Global_Intervals);
                return _cachedAreas;
            }
        }

        public List<OrgArea> RoleAreas {
            get {
                if(this.Role.Id == Role.SuperId)
                    return this.Areas;

                if(_cachedRoleAreas != null)
                    return _cachedRoleAreas;

                var cachedKey = string.Format(GlobalCacheKeys.Og_RoleAreasPattern, this.Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedRoleAreas = _cacheManager.Get<List<OrgArea>>(cachedKey);

                var areas = _areaService.GetAreasAsList();
                var keys = _areasInRoleService.GetKeysAsList(this.Role.Id);
                var ssets = from station in this.Stations
                            group station by station.Current.AreaId into g
                            select new {
                                Id = g.Key,
                                Stations = g
                            };

                _cachedRoleAreas = (from area in areas
                                    join key in keys on area.Id equals key
                                    join ss in ssets on area.Id equals ss.Id into lt
                                    from def in lt.DefaultIfEmpty()
                                    select new OrgArea {
                                        Current = area,
                                        Stations = def != null ? def.Stations.ToList() : new List<OrgStation>()
                                    }).ToList();

                foreach(var current in _cachedRoleAreas) {
                    current.Initializer(_cachedRoleAreas);
                }

                _cacheManager.Set<List<OrgArea>>(cachedKey, _cachedRoleAreas, CachedIntervals.Global_Intervals);
                return _cachedRoleAreas;
            }
        }

        public List<OrgStation> RoleStations {
            get {
                if(this.Role.Id == Role.SuperId)
                    return this.Stations;

                if(_cachedRoleStations != null)
                    return _cachedRoleStations;

                var cachedKey = string.Format(GlobalCacheKeys.Og_RoleStationsPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedRoleStations = _cacheManager.Get<List<OrgStation>>(cachedKey);

                _cachedRoleStations = this.RoleAreas.SelectMany(a => a.Stations).ToList();
                _cacheManager.Set<List<OrgStation>>(cachedKey, _cachedRoleStations, CachedIntervals.Global_Intervals);
                return _cachedRoleStations;
            }
        }

        public List<OrgRoom> RoleRooms {
            get {
                if(this.Role.Id == Role.SuperId)
                    return this.Rooms;

                if(_cachedRoleRooms != null)
                    return _cachedRoleRooms;

                var cachedKey = string.Format(GlobalCacheKeys.Og_RoleRoomsPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedRoleRooms = _cacheManager.Get<List<OrgRoom>>(cachedKey);

                _cachedRoleRooms = this.RoleStations.SelectMany(s => s.Rooms).ToList();
                _cacheManager.Set<List<OrgRoom>>(cachedKey, _cachedRoleRooms, CachedIntervals.Global_Intervals);
                return _cachedRoleRooms;
            }
        }

        public List<OrgFsu> RoleFsus {
            get {
                if(this.Role.Id == Role.SuperId)
                    return this.Fsus;

                if(_cachedRoleFsus != null)
                    return _cachedRoleFsus;

                var cachedKey = string.Format(GlobalCacheKeys.Og_RoleFsusPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedRoleFsus = _cacheManager.Get<List<OrgFsu>>(cachedKey);

                _cachedRoleFsus = this.RoleRooms.SelectMany(s => s.Fsus).ToList();
                _cacheManager.Set<List<OrgFsu>>(cachedKey, _cachedRoleFsus, CachedIntervals.Global_Intervals);
                return _cachedRoleFsus;
            }
        }

        public List<OrgDevice> RoleDevices {
            get {
                if(this.Role.Id == Role.SuperId)
                    return this.Devices;

                if(_cachedRoleDevices != null)
                    return _cachedRoleDevices;

                var cachedKey = string.Format(GlobalCacheKeys.Og_RoleDevicesPattern, Role.Id);
                if(_cacheManager.IsSet(cachedKey))
                    return _cachedRoleDevices = _cacheManager.Get<List<OrgDevice>>(cachedKey);

                _cachedRoleDevices = this.RoleRooms.SelectMany(s => s.Devices).ToList();
                _cacheManager.Set<List<OrgDevice>>(cachedKey, _cachedRoleDevices, CachedIntervals.Global_Intervals);
                return _cachedRoleDevices;
            }
        }

        public List<AlmStore<ActAlm>> ActAlmStore {
            get {
                if(_cachedAlmStore != null)
                    return _cachedAlmStore;
                
                var alarms = _actAlmService.GetAllAlmsAsList();
                var extsets = _extendAlmService.GetAllExtAlmsAsList();
                var points = this.Points.FindAll(p => p.Type == EnmPoint.DI);
                _cachedAlmStore = (from alarm in alarms
                                   join point in points on alarm.PointId equals point.Id
                                   join device in this.RoleDevices on alarm.DeviceId equals device.Current.Id
                                   join room in this.RoleRooms on alarm.RoomId equals room.Current.Id
                                   join station in this.RoleStations on alarm.StationId equals station.Current.Id
                                   join area in this.RoleAreas on alarm.AreaId equals area.Current.Id
                                   join ext in extsets on new { alarm.Id, alarm.FsuId } equals new { ext.Id, ext.FsuId } into lt
                                   from def in lt.DefaultIfEmpty()
                                   orderby alarm.StartTime descending
                                   select new AlmStore<ActAlm> {
                                       Current = alarm,
                                       ExtSet = def,
                                       Point = point,
                                       Device = device.Current,
                                       Room = room.Current,
                                       Station = station.Current,
                                       Area = new Area {
                                           Id = area.Current.Id,
                                           Code = area.Current.Code,
                                           Name = area.ToString(),
                                           Type = area.Current.Type,
                                           ParentId = area.Current.ParentId,
                                           Comment = area.Current.Comment,
                                           Enabled = area.Current.Enabled
                                       }
                                   }).ToList();

                return _cachedAlmStore;
            }
        }

        #endregion

        #region Methods

        public List<AlmStore<ActAlm>> GetActAlmStore(List<ActAlm> alarms) {
            if(alarms == null || alarms.Count == 0) 
                return new List<AlmStore<ActAlm>>();

            var extsets = _extendAlmService.GetAllExtAlmsAsList();
            var points = this.Points.FindAll(p => p.Type == EnmPoint.DI);
            return (from alarm in alarms
                    join point in points on alarm.PointId equals point.Id
                    join device in this.RoleDevices on alarm.DeviceId equals device.Current.Id
                    join room in this.RoleRooms on alarm.RoomId equals room.Current.Id
                    join station in this.RoleStations on alarm.StationId equals station.Current.Id
                    join area in this.RoleAreas on alarm.AreaId equals area.Current.Id
                    join ext in extsets on new { alarm.Id, alarm.FsuId } equals new { ext.Id, ext.FsuId } into lt
                    from def in lt.DefaultIfEmpty()
                    orderby alarm.StartTime descending
                    select new AlmStore<ActAlm> {
                        Current = alarm,
                        ExtSet = def,
                        Point = point,
                        Device = device.Current,
                        Room = room.Current,
                        Station = station.Current,
                        Area = new Area {
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

        public List<AlmStore<HisAlm>> GetHisAlmStore(List<HisAlm> alarms, DateTime start, DateTime end) {
            if(alarms == null || alarms.Count == 0) 
                return new List<AlmStore<HisAlm>>();

            var extsets = _extendAlmService.GetHisExtAlmsAsList(start, end);
            var points = this.Points.FindAll(p => p.Type == EnmPoint.DI);
            return (from alarm in alarms
                    join point in points on alarm.PointId equals point.Id
                    join device in this.RoleDevices on alarm.DeviceId equals device.Current.Id
                    join room in this.RoleRooms on alarm.RoomId equals room.Current.Id
                    join station in this.RoleStations on alarm.StationId equals station.Current.Id
                    join area in this.RoleAreas on alarm.AreaId equals area.Current.Id
                    join ext in extsets on new { alarm.Id, alarm.FsuId } equals new { ext.Id, ext.FsuId } into lt
                    from def in lt.DefaultIfEmpty()
                    orderby alarm.StartTime descending
                    select new AlmStore<HisAlm> {
                        Current = alarm,
                        ExtSet = def,
                        Point = point,
                        Device = device.Current,
                        Room = room.Current,
                        Station = station.Current,
                        Area = new Area {
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

        #endregion

    }
}