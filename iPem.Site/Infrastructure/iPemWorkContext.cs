using iPem.Core;
using iPem.Core.Caching;
using iPem.Services.Common;
using MsDomain = iPem.Core.Domain.Master;
using RsDomain = iPem.Core.Domain.Resource;
using MsSrv = iPem.Services.Master;
using RsSrv = iPem.Services.Resource;
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
        private readonly RsSrv.IAreaService _rsAreaService;
        private readonly RsSrv.IDeviceService _rsDeviceService;
        private readonly RsSrv.IEmployeeService _rsEmployeeService;
        private readonly RsSrv.IRoomService _rsRoomService;
        private readonly RsSrv.IStationService _rsStationService;

        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private MsDomain.Role _cachedRole;
        private MsDomain.User _cachedUser;
        private RsDomain.Employee _cachedEmployee;
        private List<RsDomain.Area> _cachedAssociatedAreas;
        private List<RsDomain.Station> _cachedAssociatedStations;
        private List<RsDomain.Room> _cachedAssociatedRooms;
        private List<RsDomain.Device> _cachedAssociatedDevices;
        private Dictionary<string, AreaAttributes> _cachedAssociatedAreaAttributes;
        private Dictionary<string, StationAttributes> _cachedAssociatedStationAttributes;

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
            RsSrv.IAreaService rsAreaService,
            RsSrv.IDeviceService rsDeviceService,
            RsSrv.IEmployeeService rsEmployeeService,
            RsSrv.IRoomService rsRoomService,
            RsSrv.IStationService rsStationService) {
            this._httpContext = httpContext;
            this._cacheManager = cacheManager;
            this._msAreaService = msAreaService;
            this._msDeviceService = msDeviceService;
            this._msRoleService = msRoleService;
            this._msRoomService = msRoomService;
            this._msStationService = msStationService;
            this._msUserService = msUserService;
            this._rsAreaService = rsAreaService;
            this._rsDeviceService = rsDeviceService;
            this._rsEmployeeService = rsEmployeeService;
            this._rsRoomService = rsRoomService;
            this._rsStationService = rsStationService;
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
                    _cachedStore.ExpireUtc = now.Add(CachedIntervals.AppStoreIntervals);
                    return _cachedStore;
                }
                
                if(!EngineContext.Current.WorkStores.ContainsKey(Identifier)) {
                    _cachedStore = new Store {
                        Id = Identifier,
                        ExpireUtc = now.Add(CachedIntervals.AppStoreIntervals),
                        CreatedUtc = now
                    };

                    EngineContext.Current.WorkStores[Identifier] = _cachedStore;
                } else {
                    _cachedStore = EngineContext.Current.WorkStores[Identifier];
                    _cachedStore.ExpireUtc = now.Add(CachedIntervals.AppStoreIntervals);
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

                _cacheManager.Set<List<RsDomain.Area>>(key, _cachedAssociatedAreas, CachedIntervals.AppRoleIntervals);
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
                                             join area in this.AssociatedAreas on rs.AreaId equals area.AreaId
                                             join ms in msStations on rs.Id equals ms.Id
                                             select rs).ToList();

                _cacheManager.Set<List<RsDomain.Station>>(key, _cachedAssociatedStations, CachedIntervals.AppRoleIntervals);
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
                                          join sta in this.AssociatedStations on rs.StationId equals sta.Id
                                          join ms in msRooms on rs.Id equals ms.Id
                                          select rs).ToList();

                _cacheManager.Set<List<RsDomain.Room>>(key, _cachedAssociatedRooms, CachedIntervals.AppRoleIntervals);
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
                                            join room in this.AssociatedRooms on rs.RoomId equals room.Id
                                            join ms in msDevices on rs.Id equals ms.Id
                                            select rs).ToList();

                _cacheManager.Set<List<RsDomain.Device>>(key, _cachedAssociatedDevices, CachedIntervals.AppRoleIntervals);
                return _cachedAssociatedDevices;
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

                _cacheManager.Set<Dictionary<string, AreaAttributes>>(key, _cachedAssociatedAreaAttributes, CachedIntervals.AppRoleIntervals);
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

                _cacheManager.Set<Dictionary<string, StationAttributes>>(key, _cachedAssociatedStationAttributes, CachedIntervals.AppRoleIntervals);
                return _cachedAssociatedStationAttributes;
            }
        }

        #endregion

    }
}