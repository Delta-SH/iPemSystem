using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Services.Common;
using iPem.Site.Models;
using iPem.Site.Extensions;
using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using RsSrv = iPem.Services.Resource;
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
        private readonly MsSrv.IAreaService _msAreaService;
        private readonly MsSrv.IDeviceService _msDeviceService;
        private readonly MsSrv.IRoleService _msRoleService;
        private readonly MsSrv.IRoomService _msRoomService;
        private readonly MsSrv.IStationService _msStationService;
        private readonly MsSrv.IUserService _msUserService;
        private readonly MsSrv.IProfileService _msProfileService;
        private readonly MsSrv.IPointService _msPointService;
        private readonly MsSrv.IMenuService _msMenuService;
        private readonly MsSrv.IOperateInRoleService _msOperateInRoleService;
        private readonly MsSrv.IPointsInProtcolService _msPointsInProtcolService;
        private readonly MsSrv.IDictionaryService _msDictionaryService;
        private readonly RsSrv.IAreaService _rsAreaService;
        private readonly RsSrv.IDeviceService _rsDeviceService;
        private readonly RsSrv.IEmployeeService _rsEmployeeService;
        private readonly RsSrv.IRoomService _rsRoomService;
        private readonly RsSrv.IStationService _rsStationService;
        private readonly RsSrv.IRoomTypeService _rsRoomTypeService;
        private readonly RsSrv.IDeviceTypeService _rsDeviceTypeService;
        private readonly RsSrv.ISubDeviceTypeService _rsSubDeviceTypeService;
        private readonly RsSrv.ILogicTypeService _rsLogicTypeService;
        private readonly RsSrv.IFsuService _rsFsuService;

        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private MsDomain.Role _cachedRole;
        private MsDomain.User _cachedUser;
        private ProfileValues _cachedProfile;
        private RsDomain.Employee _cachedEmployee;
        private WsValues _cachedWsValues;
        private List<RsDomain.Area> _cachedAssociatedAreas;
        private List<RsDomain.Station> _cachedAssociatedStations;
        private List<RsDomain.Room> _cachedAssociatedRooms;
        private List<RsDomain.Device> _cachedAssociatedDevices;
        private List<RsDomain.Fsu> _cachedAssociatedFsus;
        private List<MsDomain.Menu> _cachedAssociatedMenus;
        private Dictionary<EnmOperation, string> _cachedAssociatedOperations;
        private Dictionary<string, AreaAttributes> _cachedAssociatedAreaAttributes;
        private Dictionary<string, StationAttributes> _cachedAssociatedStationAttributes;
        private Dictionary<string, RoomAttributes> _cachedAssociatedRoomAttributes;
        private Dictionary<string, DeviceAttributes> _cachedAssociatedDeviceAttributes;
        private Dictionary<string, PointAttributes> _cachedAssociatedPointAttributes;
        private List<IdValuePair<DeviceAttributes, PointAttributes>> _cachedAssociatedRssPoints;

        #endregion

        #region Ctor

        public iPemWorkContext(
            HttpContextBase httpContext,
            ICacheManager cacheManager,
            MsSrv.IAreaService msAreaService,
            MsSrv.IDeviceService msDeviceService,
            MsSrv.IRoleService msRoleService,
            MsSrv.IRoomService msRoomService,
            MsSrv.IStationService msStationService,
            MsSrv.IUserService msUserService,
            MsSrv.IProfileService msProfileService,
            MsSrv.IPointService msPointService,
            MsSrv.IMenuService msMenuService,
            MsSrv.IOperateInRoleService msOperateInRoleService,
            MsSrv.IPointsInProtcolService msPointsInProtcolService,
            MsSrv.IDictionaryService msDictionaryService,
            RsSrv.IAreaService rsAreaService,
            RsSrv.IDeviceService rsDeviceService,
            RsSrv.IEmployeeService rsEmployeeService,
            RsSrv.IRoomService rsRoomService,
            RsSrv.IStationService rsStationService,
            RsSrv.IRoomTypeService rsRoomTypeService,
            RsSrv.IDeviceTypeService rsDeviceTypeService,
            RsSrv.ISubDeviceTypeService rsSubDeviceTypeService,
            RsSrv.ILogicTypeService rsLogicTypeService,
            RsSrv.IFsuService rsFsuService) {
            this._httpContext = httpContext;
            this._cacheManager = cacheManager;
            this._msAreaService = msAreaService;
            this._msDeviceService = msDeviceService;
            this._msRoleService = msRoleService;
            this._msRoomService = msRoomService;
            this._msStationService = msStationService;
            this._msUserService = msUserService;
            this._msProfileService = msProfileService;
            this._msPointService = msPointService;
            this._msMenuService = msMenuService;
            this._msOperateInRoleService = msOperateInRoleService;
            this._msPointsInProtcolService = msPointsInProtcolService;
            this._msDictionaryService = msDictionaryService;
            this._rsAreaService = rsAreaService;
            this._rsDeviceService = rsDeviceService;
            this._rsEmployeeService = rsEmployeeService;
            this._rsRoomService = rsRoomService;
            this._rsStationService = rsStationService;
            this._rsRoomTypeService = rsRoomTypeService;
            this._rsDeviceTypeService = rsDeviceTypeService;
            this._rsSubDeviceTypeService = rsSubDeviceTypeService;
            this._rsLogicTypeService = rsLogicTypeService;
            this._rsFsuService = rsFsuService;
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
                    _cachedStore.ExpireUtc = now.Add(CachedIntervals.Site_StoreIntervals);
                    return _cachedStore;
                }
                
                if(!EngineContext.Current.WorkStores.ContainsKey(Identifier)) {
                    _cachedStore = new Store {
                        Id = Identifier,
                        ExpireUtc = now.Add(CachedIntervals.Site_StoreIntervals),
                        CreatedUtc = now
                    };

                    EngineContext.Current.WorkStores[Identifier] = _cachedStore;
                } else {
                    _cachedStore = EngineContext.Current.WorkStores[Identifier];
                    _cachedStore.ExpireUtc = now.Add(CachedIntervals.Site_StoreIntervals);
                }

                return _cachedStore;
            }
        }

        public MsDomain.Role CurrentRole {
            get {
                if(_cachedRole != null)
                    return _cachedRole;

                if(Store.Role != null) {
                    _cachedRole = Store.Role;
                    return _cachedRole;
                }

                if(!IsAuthenticated)
                    throw new iPemException("Unauthorized");

                var role = _msRoleService.GetRoleByUid(CurrentUser.Id);
                if(role == null)
                    throw new iPemException("Current role not found.");

                Store.Role = _cachedRole = role;

                return _cachedRole;
            }
        }

        public MsDomain.User CurrentUser {
            get {
                if(_cachedUser != null)
                    return _cachedUser;

                if(Store.User != null) {
                    _cachedUser = Store.User;
                    return _cachedUser;
                }

                if(!IsAuthenticated)
                    throw new iPemException("Unauthorized");

                var user = _msUserService.GetUser(_httpContext.User.Identity.Name);
                if(user == null)
                    throw new iPemException("Current user not found.");

                Store.User = _cachedUser = user;

                return _cachedUser;
            }
        }

        public ProfileValues CurrentProfile {
            get {
                if(_cachedProfile != null)
                    return _cachedProfile;

                if(Store.Profile != null) {
                    _cachedProfile = Store.Profile;
                    return _cachedProfile;
                }

                var _profile = _msProfileService.GetUserProfile(CurrentUser.Id);
                if(_profile != null && !string.IsNullOrWhiteSpace(_profile.ValuesJson)) {
                    Store.Profile = _cachedProfile = JsonConvert.DeserializeObject<ProfileValues>(_profile.ValuesJson);
                }

                return _cachedProfile;
            }

            set {
                _cachedProfile = value;
                Store.Profile = value;
            }
        }

        public WsValues CurrentWsValues {
            get {
                if(_cachedWsValues != null)
                    return _cachedWsValues;

                if(Store.WsValues != null) {
                    _cachedWsValues = Store.WsValues;
                    return _cachedWsValues;
                }

                var ws = _msDictionaryService.GetDictionary((int)EnmDictionary.Ws);
                if(ws != null && !string.IsNullOrWhiteSpace(ws.ValuesJson))
                    Store.WsValues = _cachedWsValues = JsonConvert.DeserializeObject<WsValues>(ws.ValuesJson);

                return _cachedWsValues;
            }

            set {
                _cachedWsValues = value;
                Store.WsValues = value;
            }
        }

        public RsDomain.Employee AssociatedEmployee {
            get {
                if(_cachedEmployee != null)
                    return _cachedEmployee;

                if(Store.Employee != null) {
                    _cachedEmployee = Store.Employee;
                    return _cachedEmployee;
                }

                Store.Employee = _cachedEmployee = _rsEmployeeService.GetEmpolyee(CurrentUser.EmployeeId);

                return _cachedEmployee;
            }
        }

        public List<RsDomain.Area> AssociatedAreas {
            get {
                if(_cachedAssociatedAreas != null)
                    return _cachedAssociatedAreas;

                var key = string.Format(GlobalCacheKeys.Rl_AreasResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<RsDomain.Area>>(key);

                var msAreas = _msAreaService.GetAreasInRole(CurrentRole.Id);
                var rsAreas = _rsAreaService.GetAreas();
                _cachedAssociatedAreas = (from rs in rsAreas
                                          join ms in msAreas on rs.AreaId equals ms.Id
                                          select rs).ToList();

                _cacheManager.Set<List<RsDomain.Area>>(key, _cachedAssociatedAreas, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedAreas;
            }
        }

        public List<RsDomain.Station> AssociatedStations {
            get {
                if(_cachedAssociatedStations != null)
                    return _cachedAssociatedStations;

                var key = string.Format(GlobalCacheKeys.Rl_StationsResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<RsDomain.Station>>(key);

                var msStations = _msStationService.GetAllStations();
                var rsStations = _rsStationService.GetAllStations();
                _cachedAssociatedStations = (from rs in rsStations
                                             join parent in this.AssociatedAreas on rs.AreaId equals parent.AreaId
                                             join ms in msStations on rs.Id equals ms.Id
                                             select rs).ToList();

                _cacheManager.Set<List<RsDomain.Station>>(key, _cachedAssociatedStations, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedStations;
            }
        }

        public List<RsDomain.Room> AssociatedRooms {
            get {
                if(_cachedAssociatedRooms != null)
                    return _cachedAssociatedRooms;

                var key = string.Format(GlobalCacheKeys.Rl_RoomsResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<RsDomain.Room>>(key);

                var msRooms = _msRoomService.GetAllRooms();
                var rsRooms = _rsRoomService.GetAllRooms();
                _cachedAssociatedRooms = (from rs in rsRooms
                                          join parent in this.AssociatedStations on rs.StationId equals parent.Id
                                          join ms in msRooms on rs.Id equals ms.Id
                                          select rs).ToList();

                _cacheManager.Set<List<RsDomain.Room>>(key, _cachedAssociatedRooms, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedRooms;
            }
        }

        public List<RsDomain.Device> AssociatedDevices {
            get {
                if(_cachedAssociatedDevices != null)
                    return _cachedAssociatedDevices;

                var key = string.Format(GlobalCacheKeys.Rl_DevicesResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<RsDomain.Device>>(key);

                var msDevices = _msDeviceService.GetAllDevices();
                var rsDevices = _rsDeviceService.GetAllDevices();
                _cachedAssociatedDevices = (from rs in rsDevices
                                            join parent in this.AssociatedRooms on rs.RoomId equals parent.Id
                                            join ms in msDevices on rs.Id equals ms.Id
                                            select rs).ToList();

                _cacheManager.Set<List<RsDomain.Device>>(key, _cachedAssociatedDevices, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedDevices;
            }
        }

        public List<RsDomain.Fsu> AssociatedFsus {
            get {
                if(_cachedAssociatedFsus != null)
                    return _cachedAssociatedFsus;

                var key = string.Format(GlobalCacheKeys.Rl_FsusResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<RsDomain.Fsu>>(key);

                var fsus = _rsFsuService.GetAllFsus();
                _cachedAssociatedFsus = (from dev in AssociatedDevices
                                         join fsu in fsus on dev.Id equals fsu.Id
                                         select fsu).ToList();

                _cacheManager.Set<List<RsDomain.Fsu>>(key, _cachedAssociatedFsus, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedFsus;
            }
        }

        public List<MsDomain.Menu> AssociatedMenus {
            get {
                if(_cachedAssociatedMenus != null)
                    return _cachedAssociatedMenus;

                _cachedAssociatedMenus = _msMenuService.GetMenus(CurrentRole.Id).ToList();
                return _cachedAssociatedMenus;
            }
        }

        public Dictionary<EnmOperation, string> AssociatedOperations {
            get {
                if(_cachedAssociatedOperations != null)
                    return _cachedAssociatedOperations;

                var key = string.Format(GlobalCacheKeys.Rl_OperationsResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<Dictionary<EnmOperation, string>>(key);

                var operations = _msOperateInRoleService.GetOperateInRole(CurrentRole.Id);
                _cachedAssociatedOperations = new Dictionary<EnmOperation, string>();
                foreach(var entity in operations.OperateIds) {
                    _cachedAssociatedOperations[entity] = Common.GetOperationDisplay(entity);
                }

                _cacheManager.Set<Dictionary<EnmOperation, string>>(key, _cachedAssociatedOperations, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedOperations;
            }
        }

        public Dictionary<string, AreaAttributes> AssociatedAreaAttributes {
            get {
                if(_cachedAssociatedAreaAttributes != null)
                    return _cachedAssociatedAreaAttributes;

                var key = string.Format(GlobalCacheKeys.Rl_AreaAttributesResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<Dictionary<string, AreaAttributes>>(key);

                _cachedAssociatedAreaAttributes = new Dictionary<string, AreaAttributes>();
                foreach(var entity in this.AssociatedAreas) {
                    _cachedAssociatedAreaAttributes[entity.AreaId] = new AreaAttributes(this.AssociatedAreas, entity);
                }

                _cacheManager.Set<Dictionary<string, AreaAttributes>>(key, _cachedAssociatedAreaAttributes, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedAreaAttributes;
            }
        }

        public Dictionary<string, StationAttributes> AssociatedStationAttributes {
            get {
                if(_cachedAssociatedStationAttributes != null)
                    return _cachedAssociatedStationAttributes;

                var key = string.Format(GlobalCacheKeys.Rl_StationAttributesResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<Dictionary<string, StationAttributes>>(key);

                _cachedAssociatedStationAttributes = new Dictionary<string, StationAttributes>();
                foreach(var entity in this.AssociatedStations) {
                    _cachedAssociatedStationAttributes[entity.Id] = new StationAttributes(this.AssociatedStations, entity);
                }

                _cacheManager.Set<Dictionary<string, StationAttributes>>(key, _cachedAssociatedStationAttributes, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedStationAttributes;
            }
        }

        public Dictionary<string, RoomAttributes> AssociatedRoomAttributes {
            get {
                if(_cachedAssociatedRoomAttributes != null)
                    return _cachedAssociatedRoomAttributes;

                var key = string.Format(GlobalCacheKeys.Rl_RoomAttributesResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<Dictionary<string, RoomAttributes>>(key);

                var types = _rsRoomTypeService.GetAllRoomTypes();
                var attributes = from room in AssociatedRooms
                                 join type in types on room.RoomTypeId equals type.Id
                                 join station in AssociatedStations on room.StationId equals station.Id
                                 join area in AssociatedAreas on station.AreaId equals area.AreaId
                                 select new RoomAttributes {
                                     Area = area,
                                     Station = station,
                                     Current = room,
                                     Type = type
                                 };

                _cachedAssociatedRoomAttributes = new Dictionary<string, RoomAttributes>();
                foreach(var entity in attributes) {
                    _cachedAssociatedRoomAttributes[entity.Current.Id] = entity;
                }

                _cacheManager.Set<Dictionary<string, RoomAttributes>>(key, _cachedAssociatedRoomAttributes, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedRoomAttributes;
            }
        }

        public Dictionary<string, DeviceAttributes> AssociatedDeviceAttributes {
            get {
                if(_cachedAssociatedDeviceAttributes != null)
                    return _cachedAssociatedDeviceAttributes;

                var key = string.Format(GlobalCacheKeys.Rl_DeviceAttributesResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<Dictionary<string, DeviceAttributes>>(key);

                var msDevices = _msDeviceService.GetAllDevices();
                var rsDevices = _rsDeviceService.GetAllDevices();
                var types = _rsDeviceTypeService.GetAllDeviceTypes();
                var subtypes = _rsSubDeviceTypeService.GetAllSubDeviceTypes();

                var fuDevices = from device in msDevices
                                join fsu in AssociatedFsus on device.FsuId equals fsu.Id into temp
                                from defaultFsu in temp.DefaultIfEmpty()
                                select new {
                                    Id = device.Id,
                                    Fsu = defaultFsu
                                };

                var attributes = from rsd in rsDevices
                                 join fud in fuDevices on rsd.Id equals fud.Id
                                 join type in types on rsd.DeviceTypeId equals type.Id
                                 join subtype in subtypes on rsd.SubDeviceTypeId equals subtype.Id
                                 join room in AssociatedRooms on rsd.RoomId equals room.Id
                                 join station in AssociatedStations on room.StationId equals station.Id
                                 join area in AssociatedAreas on station.AreaId equals area.AreaId
                                 select new DeviceAttributes {
                                     Area = area,
                                     Station = station,
                                     Room = room,
                                     Fsu = fud.Fsu,
                                     Current = rsd,
                                     Type = type,
                                     SubType = subtype
                                 };

                _cachedAssociatedDeviceAttributes = new Dictionary<string, DeviceAttributes>();
                foreach(var entity in attributes) {
                    _cachedAssociatedDeviceAttributes[entity.Current.Id] = entity;
                }

                _cacheManager.Set<Dictionary<string, DeviceAttributes>>(key, _cachedAssociatedDeviceAttributes, CachedIntervals.Global_RoleIntervals);
                return _cachedAssociatedDeviceAttributes;
            }
        }

        public Dictionary<string, PointAttributes> AssociatedPointAttributes {
            get {
                if(_cachedAssociatedPointAttributes != null)
                    return _cachedAssociatedPointAttributes;

                var points = _msPointService.GetPoints();
                var logictypes = _rsLogicTypeService.GetAllLogicTypes();
                var sublogictypes = _rsLogicTypeService.GetAllSubLogicTypes();
                var attributes = from point in points
                                 join sublogic in sublogictypes on point.SubLogicTypeId equals sublogic.Id
                                 join logic in logictypes on sublogic.LogicTypeId equals logic.Id
                                 select new PointAttributes {
                                     Current = point,
                                     SubLogicType = sublogic,
                                     LogicType = logic
                                 };

                _cachedAssociatedPointAttributes = new Dictionary<string, PointAttributes>();
                foreach(var entity in attributes) {
                    _cachedAssociatedPointAttributes[entity.Current.Id] = entity;
                }

                return _cachedAssociatedPointAttributes;
            }
        }

        public List<IdValuePair<DeviceAttributes, PointAttributes>> AssociatedRssPoints {
            get {
                if(_cachedAssociatedRssPoints != null)
                    return _cachedAssociatedRssPoints;

                var key = string.Format(GlobalCacheKeys.Ur_RssPointsResultPattern, CurrentUser.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key);

                var stationtypes = CurrentProfile.PointRss.stationtypes ?? new string[] { };
                var roomtypes = CurrentProfile.PointRss.roomtypes ?? new string[] { };
                var devicetypes = CurrentProfile.PointRss.devicetypes ?? new string[] { };
                var logictypes = CurrentProfile.PointRss.logictypes ?? new string[] { };
                var pointtypes = CurrentProfile.PointRss.pointtypes ?? new int[] { };
                var pointnames = Common.SplitCondition(CurrentProfile.PointRss.pointnames);

                if(stationtypes.Length == 0 
                    && roomtypes.Length == 0 
                    && devicetypes.Length == 0 
                    && logictypes.Length == 0 
                    && pointtypes.Length == 0 
                    && pointnames.Length == 0)
                    return new List<IdValuePair<DeviceAttributes, PointAttributes>>();

                var relations = _msPointsInProtcolService.GetRelation();
                var devWpot = from device in AssociatedDevices
                              join rlt in relations on device.Id equals rlt.Id
                              select rlt;

                var deviceResult = AssociatedDeviceAttributes.Values.ToList();
                if(stationtypes.Length > 0)
                    deviceResult = deviceResult.FindAll(d => stationtypes.Contains(d.Station.StaTypeId));

                if(roomtypes.Length > 0)
                    deviceResult = deviceResult.FindAll(d => roomtypes.Contains(d.Room.RoomTypeId));

                if(devicetypes.Length > 0)
                    deviceResult = deviceResult.FindAll(d => devicetypes.Contains(d.Current.DeviceTypeId));

                var pointResult = AssociatedPointAttributes.Values.ToList();
                if(pointtypes.Length > 0)
                    pointResult = pointResult.FindAll(p => pointtypes.Contains((int)p.Current.Type));

                if(logictypes.Length > 0)
                    pointResult = pointResult.FindAll(p => logictypes.Contains(p.LogicType.Id));

                if(pointnames.Length > 0)
                    pointResult = pointResult.FindAll(p => CommonHelper.ConditionContain(p.Current.Name, pointnames));

                _cachedAssociatedRssPoints = (from dp in devWpot
                                              join dr in deviceResult on dp.Id equals dr.Current.Id
                                              join pr in pointResult on dp.Value equals pr.Current.Id
                                              select new IdValuePair<DeviceAttributes, PointAttributes> {
                                                  Id = dr,
                                                  Value = pr
                                              }).ToList();

                _cacheManager.Set<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key, _cachedAssociatedRssPoints, CachedIntervals.Global_UserIntervals);
                return _cachedAssociatedRssPoints;
            }
        }

        #endregion

        #region Methods

        public List<RsDomain.Area> GetParentsInArea(RsDomain.Area current, bool include = true) {
            var result = new List<RsDomain.Area>();
            if(this.AssociatedAreaAttributes.ContainsKey(current.AreaId))
                result.AddRange(this.AssociatedAreaAttributes[current.AreaId].Parents);

            if(include) result.Add(current);
            return result;
        }

        public List<RsDomain.Area> GetParentsInArea(string id, bool include = true) {
            if(!this.AssociatedAreaAttributes.ContainsKey(id))
                return new List<RsDomain.Area>();

            return this.GetParentsInArea(this.AssociatedAreaAttributes[id].Current, include);
        }

        public List<RsDomain.Station> GetParentsInStation(RsDomain.Station current, bool include = true) {
            var result = new List<RsDomain.Station>();
            if(this.AssociatedStationAttributes.ContainsKey(current.Id))
                result.AddRange(this.AssociatedStationAttributes[current.Id].Parents);

            if(include) result.Add(current);
            return result;
        }

        public List<RsDomain.Station> GetParentsInStation(string id, bool include = true) {
            if(!this.AssociatedStationAttributes.ContainsKey(id))
                return new List<RsDomain.Station>();

            return this.GetParentsInStation(this.AssociatedStationAttributes[id].Current, include);
        }

        #endregion

    }
}