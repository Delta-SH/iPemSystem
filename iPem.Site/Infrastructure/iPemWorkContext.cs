using iPem.Core;
using iPem.Core.Caching;
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
using iPem.Core.Enum;

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
        private readonly MsSrv.IOperateInRoleService _msOperateInRoleService;
        private readonly MsSrv.IPointsInProtcolService _msPointsInProtcolService;
        private readonly RsSrv.IAreaService _rsAreaService;
        private readonly RsSrv.IDeviceService _rsDeviceService;
        private readonly RsSrv.IEmployeeService _rsEmployeeService;
        private readonly RsSrv.IRoomService _rsRoomService;
        private readonly RsSrv.IStationService _rsStationService;
        private readonly RsSrv.IDeviceTypeService _rsDeviceTypeService;
        private readonly RsSrv.ILogicTypeService _rsLogicTypeService;

        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private MsDomain.Role _cachedRole;
        private MsDomain.User _cachedUser;
        private ProfileValues _cachedProfile;
        private RsDomain.Employee _cachedEmployee;
        private List<RsDomain.Area> _cachedAssociatedAreas;
        private List<RsDomain.Station> _cachedAssociatedStations;
        private List<RsDomain.Room> _cachedAssociatedRooms;
        private List<RsDomain.Device> _cachedAssociatedDevices;
        private Dictionary<EnmOperation, string> _cachedAssociatedOperations;
        private Dictionary<string, AreaAttributes> _cachedAssociatedAreaAttributes;
        private Dictionary<string, StationAttributes> _cachedAssociatedStationAttributes;
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
            MsSrv.IOperateInRoleService msOperateInRoleService,
            MsSrv.IPointsInProtcolService msPointsInProtcolService,
            RsSrv.IAreaService rsAreaService,
            RsSrv.IDeviceService rsDeviceService,
            RsSrv.IEmployeeService rsEmployeeService,
            RsSrv.IRoomService rsRoomService,
            RsSrv.IStationService rsStationService,
            RsSrv.IDeviceTypeService rsDeviceTypeService,
            RsSrv.ILogicTypeService rsLogicTypeService) {
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
            this._msOperateInRoleService = msOperateInRoleService;
            this._msPointsInProtcolService = msPointsInProtcolService;
            this._rsAreaService = rsAreaService;
            this._rsDeviceService = rsDeviceService;
            this._rsEmployeeService = rsEmployeeService;
            this._rsRoomService = rsRoomService;
            this._rsStationService = rsStationService;
            this._rsDeviceTypeService = rsDeviceTypeService;
            this._rsLogicTypeService = rsLogicTypeService;
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

        public Dictionary<string, DeviceAttributes> AssociatedDeviceAttributes {
            get {
                if(_cachedAssociatedDeviceAttributes != null)
                    return _cachedAssociatedDeviceAttributes;

                var key = string.Format(GlobalCacheKeys.Rl_DeviceAttributesResultPattern, CurrentRole.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<Dictionary<string, DeviceAttributes>>(key);

                var devTypes = _rsDeviceTypeService.GetAllDeviceTypes();
                var attributes = from device in AssociatedDevices
                                 join devType in devTypes on device.DeviceTypeId equals devType.Id
                                 join room in AssociatedRooms on device.RoomId equals room.Id
                                 join station in AssociatedStations on room.StationId equals station.Id
                                 join area in AssociatedAreas on station.AreaId equals area.AreaId
                                 select new DeviceAttributes {
                                     Area = area,
                                     Station = station,
                                     Room = room,
                                     Current = device,
                                     Type = devType
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
                _cachedAssociatedPointAttributes = (from point in points
                                                    join logic in logictypes on point.LogicTypeId equals logic.Id
                                                    select new PointAttributes {
                                                        Current = point,
                                                        LogicType = logic
                                                    }).ToDictionary(k => k.Current.Id, v => v);

                return _cachedAssociatedPointAttributes;
            }
        }

        public List<IdValuePair<DeviceAttributes,PointAttributes>> AssociatedRssPoints {
            get {
                if(_cachedAssociatedRssPoints != null)
                    return _cachedAssociatedRssPoints;

                var key = string.Format(GlobalCacheKeys.Ur_RssPointsResultPattern, CurrentUser.Id);
                if(_cacheManager.IsSet(key))
                    return _cacheManager.Get<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key);

                var stationtypes = CurrentProfile.PointRss.stationtypes ?? new int[] { };
                var roomtypes = CurrentProfile.PointRss.roomtypes ?? new int[] { };
                var devicetypes = CurrentProfile.PointRss.devicetypes ?? new int[] { };
                var logictypes = CurrentProfile.PointRss.logictypes ?? new int[] { };
                var pointtypes = CurrentProfile.PointRss.pointtypes ?? new int[] { };
                var pointnames = CommonHelper.ConditionSplit(CurrentProfile.PointRss.pointnames);
                if(stationtypes.Length == 0
                    && roomtypes.Length == 0
                    && devicetypes.Length == 0
                    && logictypes.Length == 0
                    && pointtypes.Length == 0
                    && pointnames.Length == 0)
                    return new List<IdValuePair<DeviceAttributes, PointAttributes>>();

                var devices = this.AssociatedDeviceAttributes.Values.ToList().FindAll(d => (stationtypes.Length == 0 || stationtypes.Contains(d.Station.StaTypeId)) && (roomtypes.Length == 0 || roomtypes.Contains(d.Room.RoomTypeId)) && (devicetypes.Length == 0 || devicetypes.Contains(d.Current.DeviceTypeId)));
                var points = (pointtypes.Length == 0 ? this.AssociatedPointAttributes.Values.ToList() : this.GetAssociatedPointAttributes(pointtypes)).FindAll(p => (logictypes.Length == 0 || logictypes.Contains(p.Current.LogicTypeId)) && (pointnames.Length == 0 || CommonHelper.ConditionContain(p.Current.Name, pointnames)));

                var protcolInDevice = _msDeviceService.GetAllDevices();
                var pointsInProtcol = _msPointsInProtcolService.GetAllPointsInProtcol();
                var pointsInDevice = from pid in protcolInDevice
                                     join pip in pointsInProtcol on pid.ProtcolId equals pip.Id
                                     select new {
                                         DeviceId = pid.Id,
                                         PointId = pip.Value
                                     };

                _cachedAssociatedRssPoints = (from p in points
                                              join pid in pointsInDevice on p.Current.Id equals pid.PointId
                                              join d in devices on pid.DeviceId equals d.Current.Id
                                              select new IdValuePair<DeviceAttributes, PointAttributes> {
                                                  Id = d,
                                                  Value = p
                                              }).ToList();

                _cacheManager.Set<List<IdValuePair<DeviceAttributes, PointAttributes>>>(key, _cachedAssociatedRssPoints, CachedIntervals.Global_UserIntervals);
                return _cachedAssociatedRssPoints;
            }
        }

        #endregion

        #region Methods

        public List<PointAttributes> GetAssociatedPointAttributes(int[] types) {
            var logicTypes = _rsLogicTypeService.GetAllLogicTypes();
            var points = _msPointService.GetPointsByType(types);
            var attributes = from point in points
                             join logic in logicTypes on point.LogicTypeId equals logic.Id
                             select new PointAttributes {
                                 Current = point,
                                 LogicType = logic
                             };

            return attributes.ToList();
        }

        #endregion

    }
}