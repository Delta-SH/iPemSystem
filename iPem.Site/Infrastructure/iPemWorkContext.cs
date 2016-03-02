using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Domain.Resource;
using iPem.Services.Master;
using iPem.Services.Resource;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// Work context for the application
    /// </summary>
    public class iPemWorkContext : IWorkContext {

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;

        private Guid? _cachedIdentifier;
        private Store _cachedStore;
        private Role _cachedRole;
        private User _cachedUser;
        private Employee _cachedEmployee;

        #endregion

        #region Ctor

        public iPemWorkContext(
            HttpContextBase httpContext,
            IRoleService roleService,
            IUserService userService,
            IEmployeeService employeeService) {
            this._httpContext = httpContext;
            this._roleService = roleService;
            this._userService = userService;
            this._employeeService = employeeService;
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
                    _cachedStore.ExpireUtc = now.Add(EngineContext.Current.AppStore.CachedInterval);
                    return _cachedStore;
                }
                
                if(!EngineContext.Current.WorkStores.ContainsKey(Identifier)) {
                    _cachedStore = new Store {
                        Id = Identifier,
                        ExpireUtc = now.Add(EngineContext.Current.AppStore.CachedInterval),
                        CreatedUtc = now
                    };

                    EngineContext.Current.WorkStores[Identifier] = _cachedStore;
                } else {
                    _cachedStore = EngineContext.Current.WorkStores[Identifier];
                    _cachedStore.ExpireUtc = now.Add(EngineContext.Current.AppStore.CachedInterval);
                }

                return _cachedStore;
            }
        }

        public Role CurrentRole {
            get {
                if(_cachedRole != null)
                    return _cachedRole;

                if(Store.Role != null) {
                    _cachedRole = Store.Role;
                    return _cachedRole;
                }

                if(!IsAuthenticated)
                    throw new iPemException("Unauthorized");

                var role = _roleService.GetRoleByUid(CurrentUser.Id);
                if(role == null)
                    throw new iPemException("Current role not found.");

                Store.Role = _cachedRole = role;

                return _cachedRole;
            }
        }

        public User CurrentUser {
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

        public Employee CurrentEmployee {
            get {
                if(_cachedEmployee != null)
                    return _cachedEmployee;

                if(Store.Employee != null) {
                    _cachedEmployee = Store.Employee;
                    return _cachedEmployee;
                }

                Store.Employee = _cachedEmployee = _employeeService.GetEmpolyee(CurrentUser.EmployeeId);

                return _cachedEmployee;
            }
        }

        #endregion

    }
}