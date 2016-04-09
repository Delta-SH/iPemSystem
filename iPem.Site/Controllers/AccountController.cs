using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MsDomain = iPem.Core.Domain.Master;
using MsSrv = iPem.Services.Master;
using RsDomain = iPem.Core.Domain.Resource;
using RsSrv = iPem.Services.Resource;

namespace iPem.Site.Controllers {
    public class AccountController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;

        private readonly MsSrv.IWebLogger _webLogger;
        private readonly MsSrv.IUserService _userService;
        private readonly MsSrv.IRoleService _roleService;
        private readonly MsSrv.IMenuService _menuService;
        private readonly MsSrv.IMenusInRoleService _menusInRoleService;
        private readonly MsSrv.INoticeService _noticeService;
        private readonly MsSrv.INoticeInUserService _noticeInUserService;
        private readonly MsSrv.IAreaService _msAreaService;
        private readonly MsSrv.IAreasInRoleService _areaInRoleService;
        private readonly MsSrv.IOperateInRoleService _operateInRoleService;

        private readonly RsSrv.IEmployeeService _employeeService;
        private readonly RsSrv.IDepartmentService _departmentService;
        private readonly RsSrv.IAreaService _rsAreaService;

        private const string _captchaSalt = "w9hRaAIX+tRJ4GD4wnVkVQ==";

        #endregion

        #region Ctor

        public AccountController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            MsSrv.IWebLogger webLogger,
            MsSrv.IUserService userService,
            MsSrv.IRoleService roleService,
            MsSrv.IMenuService menuService,
            MsSrv.IMenusInRoleService menusInRoleService,
            MsSrv.INoticeService noticeService,
            MsSrv.INoticeInUserService noticeInUserService,
            MsSrv.IAreaService msAreaService,
            MsSrv.IAreasInRoleService areaInRoleService,
            MsSrv.IOperateInRoleService operateInRoleService,
            RsSrv.IEmployeeService employeeService,
            RsSrv.IDepartmentService departmentService,
            RsSrv.IAreaService rsAreaService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._userService = userService;
            this._roleService = roleService;
            this._menuService = menuService;
            this._menusInRoleService = menusInRoleService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._msAreaService = msAreaService;
            this._areaInRoleService = areaInRoleService;
            this._operateInRoleService = operateInRoleService;
            this._employeeService = employeeService;
            this._departmentService = departmentService;
            this._rsAreaService = rsAreaService;
        }

        #endregion

        #region Actions

        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string uid, string pwd, string captcha, string returnUrl) {
            try {
                if(String.IsNullOrWhiteSpace(uid))
                    ModelState.AddModelError("", "用户名不能为空。");

                uid = uid.Trim(); ViewBag.Uid = uid;

                if(ModelState.IsValid && String.IsNullOrWhiteSpace(pwd))
                    ModelState.AddModelError("", "密码不能为空。");

                if(ModelState.IsValid && String.IsNullOrWhiteSpace(captcha))
                    ModelState.AddModelError("", "验证码不能为空。");

                if(ModelState.IsValid && Request.Cookies[Common.CaptchaId] == null)
                    ModelState.AddModelError("", "您的浏览器禁用了JavaScript，启用后才能使用本系统。");

                if(ModelState.IsValid) {
                    var code = Request.Cookies[Common.CaptchaId].Value;
                    captcha = CommonHelper.CreateHash(captcha.ToLowerInvariant().Trim(), _captchaSalt);
                    if(captcha != code)
                        ModelState.AddModelError("", "验证码错误。");
                }

                if(ModelState.IsValid) {
                    var loginResult = _userService.Validate(uid, pwd);
                    switch(loginResult) {
                        case EnmLoginResults.Successful:
                            break;
                        case EnmLoginResults.NotExist:
                            ModelState.AddModelError("", "用户名不存在。");
                            break;
                        case EnmLoginResults.NotEnabled:
                            ModelState.AddModelError("", "用户已禁用，请与管理员联系。");
                            break;
                        case EnmLoginResults.Expired:
                            ModelState.AddModelError("", "用户已过期，请与管理员联系。");
                            break;
                        case EnmLoginResults.Locked:
                            ModelState.AddModelError("", "用户已锁定，请与管理员联系。");
                            break;
                        case EnmLoginResults.WrongPassword:
                        default:
                            ModelState.AddModelError("", "密码错误，登录失败。");
                            break;
                    }
                }

                if(ModelState.IsValid) {
                    var _userEntity = _userService.GetUser(uid);
                    var loginResult = _roleService.Validate(_userEntity.Id);
                    switch(loginResult) {
                        case EnmLoginResults.Successful:
                            break;
                        case EnmLoginResults.RoleNotExist:
                            ModelState.AddModelError("", "角色不存在。");
                            break;
                        case EnmLoginResults.RoleNotEnabled:
                            ModelState.AddModelError("", "角色已禁用，请与管理员联系。");
                            break;
                        default:
                            ModelState.AddModelError("", "角色错误。");
                            break;
                    }
                }

                if(ModelState.IsValid) {
                    var now = DateTime.UtcNow;

                    var store = new Store {
                        Id = Guid.NewGuid(),
                        ActAlmNoticeTime = now,
                        ExpireUtc = now.Add(CachedIntervals.Site_StoreIntervals),
                        CreatedUtc = now
                    };

                    var ticket = new FormsAuthenticationTicket(
                        1, 
                        uid, 
                        now.ToLocalTime(), 
                        now.ToLocalTime().Add(FormsAuthentication.Timeout), 
                        false, 
                        store.Id.ToString(), 
                        FormsAuthentication.FormsCookiePath);

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    authCookie.HttpOnly = true;
                    authCookie.Path = FormsAuthentication.FormsCookiePath;
                    if(ticket.IsPersistent) {
                        authCookie.Expires = ticket.Expiration;
                    }
                    if(FormsAuthentication.CookieDomain != null) {
                        authCookie.Domain = FormsAuthentication.CookieDomain;
                    }
                    
                    Response.Cookies.Add(authCookie);
                    EngineContext.Current.WorkStores[store.Id] = store;
                    _webLogger.Information(EnmEventType.Login, string.Format("{0} 登录系统", uid));

                    if(Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToRoute("HomePage");
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc);
                ModelState.AddModelError("", exc.Message);
            }

            return View();
        }

        public ActionResult LogOut() {
            Session.RemoveAll();
            Response.Cookies.Clear();
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            return RedirectToRoute("HomePage");
        }

        public ActionResult GetCaptcha() {
            try {
                var code = Common.GenerateCode(5).ToLowerInvariant();
                var image = Common.CreateCaptcha(code);

                var hc = Request.Cookies[Common.CaptchaId];
                if(hc != null) {
                    hc.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(hc);
                }

                var captcha = CommonHelper.CreateHash(code, _captchaSalt);
                var cookie = new HttpCookie(Common.CaptchaId, captcha);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return File(image, @"image/png");
            } catch(Exception exc) {
                return Content(exc.Message);
            }
        }

        [Authorize]
        public ActionResult Roles() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetRoles(int start, int limit, string condition) {
            var data = new AjaxDataModel<List<RoleModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<RoleModel>()
            };

            try {
                var names = CommonHelper.ConditionSplit(condition);
                var roles = names.Length == 0 ?
                    _roleService.GetAllRoles(start / limit, limit) :
                    _roleService.GetAllRoles(names, start / limit, limit);

                if(roles.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = roles.TotalCount;
                    for(var i = 0; i < roles.Count; i++) {
                        data.data.Add(new RoleModel {
                            index = start + i + 1,
                            id = roles[i].Id.ToString(),
                            name = roles[i].Name,
                            comment = roles[i].Comment,
                            enabled = roles[i].Enabled
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetRole(string id, int action) {
            var data = new AjaxDataModel<RoleModel> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new RoleModel { index = 1, id = Guid.NewGuid().ToString(), name = "", comment = "", enabled = true, menuIds = new string[] { }, areaIds = new string[] { }, operateIds = new string[] { } }
            };

            try {
                if(action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(action != (int)EnmAction.Edit)
                    throw new ArgumentException("参数无效 action");

                var role = _roleService.GetRole(new Guid(id));
                if(role == null)
                    throw new iPemException("未找到数据对象");

                var menus = _menuService.GetMenus(role.Id, 0, int.MaxValue);
                var areas = _areaInRoleService.GetAreasInRole(role.Id);
                var operate = _operateInRoleService.GetOperateInRole(role.Id);
                data.data.id = role.Id.ToString();
                data.data.name = role.Name;
                data.data.comment = role.Comment;
                data.data.enabled = role.Enabled;
                data.data.menuIds = menus.Select(m => m.Id.ToString()).ToArray();
                data.data.areaIds = areas.AreaIds.ToArray();
                data.data.operateIds = operate.OperateIds.Select(o => ((int)o).ToString()).ToArray();

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetMenusInRole() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var menus = _menuService.GetMenus(_workContext.CurrentRole.Id).ToList();
                if(menus.Count > 0) {
                    var _menus = menus.FindAll(m => m.LastId == 0).OrderBy(m => m.Index).ToList();
                    if(_menus.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = menus.Count;
                        for(var i = 0; i < _menus.Count; i++) {
                            var root = new TreeModel {
                                id = _menus[i].Id.ToString(),
                                text = _menus[i].Name,
                                selected = false,
                                icon = _menus[i].Icon,
                                expanded = false,
                                leaf = false
                            };

                            MenusRecursion(menus, _menus[i].Id, root);
                            data.data.Add(root);
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        private void MenusRecursion(List<MsDomain.Menu> menus, int pid, TreeModel node) {
            var _menus = menus.FindAll(m => m.LastId == pid).OrderBy(m => m.Index).ToList();
            if(_menus.Count > 0) {
                node.data = new List<TreeModel>();
                for(var i = 0; i < _menus.Count; i++) {
                    var children = new TreeModel {
                        id = _menus[i].Id.ToString(),
                        text = _menus[i].Name,
                        selected = false,
                        icon = _menus[i].Icon,
                        expanded = false,
                        leaf = false
                    };

                    MenusRecursion(menus, _menus[i].Id, children);
                    node.data.Add(children);
                }
            } else {
                node.leaf = true;
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetAreasInRole() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var rsAreas = _rsAreaService.GetAreas().ToList();
                var msAreas = _msAreaService.GetAllAreas().ToList();

                var areas = (from ra in rsAreas
                             join ma in msAreas on ra.AreaId equals ma.Id
                             select ra).ToList();

                if(areas.Count > 0) {
                    var _areas = areas.FindAll(m => m.ParentId == "0");
                    if(_areas.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = areas.Count;
                        for(var i = 0; i < _areas.Count; i++) {
                            var root = new TreeModel {
                                id = _areas[i].AreaId,
                                text = _areas[i].Name,
                                selected = false,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            AreasRecursion(areas, _areas[i].AreaId, root);
                            data.data.Add(root);
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        private void AreasRecursion(List<RsDomain.Area> areas, string pid, TreeModel node) {
            var _areas = areas.FindAll(m => m.ParentId.Equals(pid, StringComparison.CurrentCultureIgnoreCase));
            if(_areas.Count > 0) {
                node.data = new List<TreeModel>();
                for(var i = 0; i < _areas.Count; i++) {
                    var children = new TreeModel {
                        id = _areas[i].AreaId,
                        text = _areas[i].Name,
                        selected = false,
                        icon = Icons.Diqiu,
                        expanded = false,
                        leaf = false
                    };

                    AreasRecursion(areas, _areas[i].AreaId, children);
                    node.data.Add(children);
                }
            } else {
                node.leaf = true;
                node.icon = Icons.Dingwei;
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetOperateInRole() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var result = Enum.GetValues(typeof(EnmOperation));
                if(result.Length > 0) {
                    data.message = "200 Ok";
                    data.total = result.Length;
                    foreach(EnmOperation op in result) {
                        var root = new TreeModel {
                            id = ((int)op).ToString(),
                            text = Common.GetOperationDisplay(op),
                            selected = false,
                            icon = Icons.Junheng,
                            leaf = true
                        };

                        data.data.Add(root);
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
            };
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveRole(RoleModel role, int action) {
            try {
                if(role == null)
                    throw new ArgumentException("参数无效 role");

                if(role.menuIds == null || role.menuIds.Length == 0)
                    throw new iPemException("无效的菜单权限，保存失败。");

                if(role.areaIds == null || role.areaIds.Length == 0)
                    throw new iPemException("无效的区域权限，保存失败。");

                var menus = new MsDomain.MenusInRole {
                    RoleId = new Guid(role.id),
                    Menus = role.menuIds.Select(i => new MsDomain.Menu { Id = int.Parse(i) }).ToList()
                };

                var areas = new MsDomain.AreasInRole {
                    RoleId = new Guid(role.id),
                    AreaIds = role.areaIds.ToList()
                };

                var operates = new MsDomain.OperateInRole {
                    RoleId = new Guid(role.id),
                    OperateIds = role.operateIds != null && role.operateIds.Length > 0 ? role.operateIds.Select(o => (EnmOperation)(int.Parse(o))).ToList() : new List<EnmOperation>()
                };

                if(action == (int)EnmAction.Add) {
                    var existedRole = _roleService.GetRole(role.name);
                    if(existedRole != null)
                        throw new iPemException("角色已存在，保存失败。");

                    var newRole = new MsDomain.Role {
                        Id = new Guid(role.id),
                        Name = role.name,
                        Comment = role.comment,
                        Enabled = role.enabled
                    };

                    _roleService.InsertRole(newRole);
                    _menusInRoleService.AddMenusInRole(menus);
                    _areaInRoleService.AddAreasInRole(areas);
                    _operateInRoleService.AddOperateInRole(operates);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增角色[{0}]", newRole.Name), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "角色保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existedRole = _roleService.GetRole(role.name);
                    if(existedRole == null)
                        throw new iPemException("角色不存在，保存失败。");

                    //existedRole.Id = new Guid(role.id);
                    existedRole.Name = role.name;
                    existedRole.Comment = role.comment;
                    existedRole.Enabled = role.enabled;

                    _roleService.UpdateRole(existedRole);
                    _menusInRoleService.DeleteMenusInRole(existedRole.Id);
                    _areaInRoleService.DeleteAreasInRole(existedRole.Id);
                    _operateInRoleService.DeleteOperateInRole(existedRole.Id);
                    _menusInRoleService.AddMenusInRole(menus);
                    _areaInRoleService.AddAreasInRole(areas);
                    _operateInRoleService.AddOperateInRole(operates);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新角色[{0}]", existedRole.Name), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "角色保存成功" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteRole(string id) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var existedRole = _roleService.GetRole(new Guid(id));
                if(existedRole == null)
                    throw new iPemException("角色不存在，删除失败");

                var usersInRole = _userService.GetUsers(existedRole.Id, false, 0, 5);
                if(usersInRole.TotalCount > 0)
                    throw new iPemException("存在隶属该角色的用户，禁止删除");

                _roleService.DeleteRole(existedRole);
                _menusInRoleService.DeleteMenusInRole(existedRole.Id);
                _areaInRoleService.DeleteAreasInRole(existedRole.Id);
                _operateInRoleService.DeleteOperateInRole(existedRole.Id);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除角色[{0}]", existedRole.Name), null, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "角色删除成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadRoles(string condition) {
            try {
                var names = CommonHelper.ConditionSplit(condition);
                var roles = names.Length == 0 ?
                    _roleService.GetAllRoles() :
                    _roleService.GetAllRoles(names);

                var models = new List<RoleModel>();
                for(var i = 0; i < roles.Count; i++) {
                    models.Add(new RoleModel {
                        index = i + 1,
                        id = roles[i].Id.ToString(),
                        name = roles[i].Name,
                        comment = roles[i].Comment,
                        enabled = true
                    });
                }

                using(var ms = _excelManager.Export<RoleModel>(models, "角色信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [Authorize]
        public ActionResult Users() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetUsers(int start, int limit, string[] rids, string[] names) {
            var data = new AjaxDataModel<List<UserModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<UserModel>()
            };

            try {
                var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);

                List<UserModel> models = null;
                if(_cacheManager.IsSet(key)) {
                    models = _cacheManager.Get<List<UserModel>>(key);
                } else {
                    var users = _userService.GetUsers(_workContext.CurrentRole.Id).ToList();

                    if(rids != null && rids.Length > 0)
                        users = users.FindAll(u => rids.Contains(u.RoleId.ToString()));

                    if(names != null && names.Length > 0)
                        users = users.FindAll(u => CommonHelper.ConditionContain(u.Uid, names));

                    if(users.Count > 0) {
                        var roles = _roleService.GetAllRoles(_workContext.CurrentRole.Id).ToList();
                        var employees = _employeeService.GetAllEmployees().ToList();

                        models = (from user in users
                                  join role in roles on user.RoleId equals role.Id
                                  join employee in employees on user.EmployeeId equals employee.Id into lj
                                  from ue in lj.DefaultIfEmpty()
                                  select new UserModel {
                                      id = user.Id.ToString(),
                                      uid = user.Uid,
                                      roleId = role.Id.ToString(),
                                      roleName = role.Name,
                                      empId = user.EmployeeId,
                                      empName = ue != null ? ue.Name : "",
                                      empNo = ue != null ? ue.EmpNo : "",
                                      sex = (int)(ue != null ? ue.Sex : EnmSex.Male),
                                      sexName = ue != null ? Common.GetSexDisplay(ue.Sex) : Common.GetSexDisplay(EnmSex.Male),
                                      mobile = ue != null ? ue.MobilePhone : "",
                                      email = ue != null ? ue.Email : "",
                                      created = CommonHelper.DateTimeConverter(user.CreateDate),
                                      limited = CommonHelper.DateConverter(user.LimitDate),
                                      lastLogined = CommonHelper.DateTimeConverter(user.LastLoginDate),
                                      lastPasswordChanged = CommonHelper.DateTimeConverter(user.LastPasswordChangedDate),
                                      isLockedOut = user.IsLockedOut,
                                      lastLockedout = CommonHelper.DateTimeConverter(user.LastLockoutDate),
                                      comment = user.Comment,
                                      enabled = user.Enabled
                                  }).ToList();

                        if(models.Count > 0)
                            _cacheManager.Set<List<UserModel>>(key, models, CachedIntervals.Site_ResultIntervals);
                    }
                }

                if(models != null && models.Count > 0) {
                    var result = new PagedList<UserModel>(models, start / limit, limit);
                    if(result.Count > 0) {
                        data.message = "200 Ok";
                        data.total = result.TotalCount;
                        for(var i = 0; i < result.Count; i++) {
                            result[i].index = start + 1 + i;
                            data.data.Add(result[i]);
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetUser(string id, int action) {
            var data = new AjaxDataModel<UserModel> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new UserModel { 
                    index = 1, 
                    id = Guid.NewGuid().ToString(),
                    uid = "",
                    roleId = "",
                    empId = "",
                    password = "",
                    limited = CommonHelper.DateConverter(DateTime.Today.AddYears(1)),
                    isLockedOut = false,
                    comment = "", 
                    enabled = true
                }
            };

            try {
                if(action == (int)EnmAction.Add) {
                    data.data.roleId = _workContext.CurrentRole.Id.ToString();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(action != (int)EnmAction.Edit)
                    throw new ArgumentException("参数无效 action");

                var user = _userService.GetUser(new Guid(id));
                if(user == null)
                    throw new iPemException("未找到数据对象");

                data.data.id = user.Id.ToString();
                data.data.uid = user.Uid;
                data.data.roleId = user.RoleId.ToString();
                data.data.empId = user.EmployeeId;
                data.data.limited = CommonHelper.DateConverter(user.LimitDate);
                data.data.isLockedOut = user.IsLockedOut;
                data.data.comment = user.Comment;
                data.data.enabled = user.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetCurrentUser() {
            var data = new AjaxDataModel<UserModel> {
                success = true,
                message = "无数据",
                total = 0,
                data = new UserModel()
            };

            try {
                var user = _workContext.CurrentUser;
                var role = _workContext.CurrentRole;
                var employee = _workContext.AssociatedEmployee;

                data.data.id = user.Id.ToString();
                data.data.uid = user.Uid;
                data.data.roleId = role.Id.ToString();
                data.data.roleName = role.Name;
                data.data.empId = user.EmployeeId;
                data.data.empName = employee != null ? employee.Name : "";
                data.data.empNo = employee != null ? employee.EmpNo : "";
                data.data.sex = (int)(employee != null ? employee.Sex : EnmSex.Male);
                data.data.sexName = employee != null ? Common.GetSexDisplay(employee.Sex) : Common.GetSexDisplay(EnmSex.Male);
                data.data.mobile = employee != null ? employee.MobilePhone : "";
                data.data.email = employee != null ? employee.Email : "";
                data.data.created = CommonHelper.DateTimeConverter(user.CreateDate);
                data.data.limited = CommonHelper.DateConverter(user.LimitDate);
                data.data.lastLogined = CommonHelper.DateTimeConverter(user.LastLoginDate);
                data.data.lastPasswordChanged = CommonHelper.DateTimeConverter(user.LastPasswordChangedDate);
                data.data.isLockedOut = user.IsLockedOut;
                data.data.lastLockedout = CommonHelper.DateTimeConverter(user.LastLockoutDate);
                data.data.comment = user.Comment;
                data.data.enabled = user.Enabled;

                data.message = "200 Ok";
                data.total = 1;
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveUser(UserModel user, int action) {
            try {
                if(user == null)
                    throw new ArgumentException("参数无效 user");

                if(action == (int)EnmAction.Add) {
                    var existedUser = _userService.GetUser(user.uid);
                    if(existedUser != null)
                        throw new iPemException("用户已存在，保存失败。");

                    var newUser = new MsDomain.User {
                        Id = new Guid(user.id),
                        RoleId = new Guid(user.roleId),
                        Uid = user.uid,
                        Password = user.password,
                        CreateDate = DateTime.Now,
                        LimitDate = DateTime.Parse(user.limited),
                        LastLoginDate = DateTime.Now,
                        LastPasswordChangedDate = DateTime.Now,
                        FailedPasswordAttemptCount = 0,
                        FailedPasswordDate = DateTime.Now,
                        IsLockedOut = user.isLockedOut,
                        LastLockoutDate = DateTime.Now,
                        Comment = user.comment,
                        EmployeeId = user.empId,
                        Enabled = user.enabled
                    };

                    _userService.InsertUser(newUser);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增用户[{0}]", newUser.Uid), null, _workContext.CurrentUser.Id);
                    var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);
                    if(_cacheManager.IsSet(key))
                        _cacheManager.Remove(key);

                    return Json(new AjaxResultModel { success = true, code = 200, message = "用户保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existedUser = _userService.GetUser(user.uid);
                    if(existedUser == null)
                        throw new iPemException("用户不存在，保存失败。");

                    existedUser.RoleId = new Guid(user.roleId);
                    existedUser.LimitDate = DateTime.Parse(user.limited);
                    existedUser.FailedPasswordAttemptCount = 0;
                    existedUser.IsLockedOut = user.isLockedOut;
                    if(user.isLockedOut) existedUser.LastLockoutDate = DateTime.Now;
                    existedUser.Comment = user.comment;
                    existedUser.EmployeeId = user.empId;
                    existedUser.Enabled = user.enabled;

                    _userService.UpdateUser(existedUser);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新用户[{0}]", existedUser.Uid), null, _workContext.CurrentUser.Id);
                    var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);
                    if(_cacheManager.IsSet(key))
                        _cacheManager.Remove(key);

                    return Json(new AjaxResultModel { success = true, code = 200, message = "用户保存成功" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteUser(string id) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var existedUser = _userService.GetUser(new Guid(id));
                if(existedUser == null)
                    throw new iPemException("用户不存在，删除失败");

                if(_workContext.CurrentUser.Uid.Equals(existedUser.Uid,StringComparison.CurrentCultureIgnoreCase))
                    throw new iPemException("无法删除当前用户");

                _userService.DeleteUser(existedUser);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除用户[{0}]", existedUser.Uid), null, _workContext.CurrentUser.Id);
                var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);
                if(_cacheManager.IsSet(key))
                    _cacheManager.Remove(key);

                return Json(new AjaxResultModel { success = true, code = 200, message = "用户删除成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ResetPassword(string id, string password) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("参数无效 password");

                var existedUser = _userService.GetUser(new Guid(id));
                if(existedUser == null)
                    throw new iPemException("用户不存在，重置失败");

                _userService.ForcePassword(existedUser, password);
                _webLogger.Information(EnmEventType.Operating, string.Format("重置用户密码[{0}]", existedUser.Uid), null, _workContext.CurrentUser.Id);
                var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);
                if(_cacheManager.IsSet(key))
                    _cacheManager.Remove(key);

                return Json(new AjaxResultModel { success = true, code = 200, message = "密码重置成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ChangePassword(string id, string origin, string password) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(string.IsNullOrWhiteSpace(origin))
                    throw new ArgumentException("参数无效 origin");

                if(string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("参数无效 password");

                var existedUser = _userService.GetUser(new Guid(id));
                if(existedUser == null)
                    throw new iPemException("用户不存在，修改失败");

                var result = _userService.ChangePassword(existedUser, origin, password);
                if(result == EnmChangeResults.Successful) {
                    _webLogger.Information(EnmEventType.Operating, string.Format("修改用户密码[{0}]", existedUser.Uid), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "密码修改成功" });
                } else if(result == EnmChangeResults.WrongPassword) {
                    throw new iPemException("原始密码错误");
                } else {
                    throw new iPemException("未知错误");
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadUsers(string[] rids, string[] names) {
            try {
                var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);

                IList<UserModel> models = null;
                if(_cacheManager.IsSet(key)) {
                    models = _cacheManager.Get<IList<UserModel>>(key);
                } else {
                    var users = _userService.GetUsers(_workContext.CurrentRole.Id).ToList();

                    if(rids != null && rids.Length > 0)
                        users = users.FindAll(u => rids.Contains(u.RoleId.ToString()));

                    if(names != null && names.Length > 0)
                        users = users.FindAll(u => CommonHelper.ConditionContain(u.Uid, names));

                    if(users.Count > 0) {
                        var roles = _roleService.GetAllRoles(_workContext.CurrentRole.Id).ToList();
                        var employees = _employeeService.GetAllEmployees().ToList();

                        models = (from user in users
                                  join role in roles on user.RoleId equals role.Id
                                  join employee in employees on user.EmployeeId equals employee.Id into lj
                                  from ue in lj.DefaultIfEmpty()
                                  select new UserModel {
                                      id = user.Id.ToString(),
                                      uid = user.Uid,
                                      roleId = role.Id.ToString(),
                                      roleName = role.Name,
                                      empId = user.EmployeeId,
                                      empName = ue != null ? ue.Name : "",
                                      empNo = ue != null ? ue.EmpNo : "",
                                      sex = (int)(ue != null ? ue.Sex : EnmSex.Male),
                                      sexName = ue != null ? Common.GetSexDisplay(ue.Sex) : Common.GetSexDisplay(EnmSex.Male),
                                      mobile = ue != null ? ue.MobilePhone : "",
                                      email = ue != null ? ue.Email : "",
                                      created = CommonHelper.DateTimeConverter(user.CreateDate),
                                      limited = CommonHelper.DateConverter(user.LimitDate),
                                      lastLogined = CommonHelper.DateTimeConverter(user.LastLoginDate),
                                      lastPasswordChanged = CommonHelper.DateTimeConverter(user.LastPasswordChangedDate),
                                      isLockedOut = user.IsLockedOut,
                                      lastLockedout = CommonHelper.DateTimeConverter(user.LastLockoutDate),
                                      comment = user.Comment,
                                      enabled = user.Enabled
                                  }).ToList();

                        if(models.Count > 0)
                            _cacheManager.Set<IList<UserModel>>(key, models);
                    }
                }

                if(models == null || models.Count == 0)
                    throw new iPemException("无数据");

                for(var i = 0; i < models.Count; i++)
                    models[i].index = i + 1;

                using(var ms = _excelManager.Export<UserModel>(models.ToList(), "用户信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetComboRoles(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var models = _roleService.GetAllRoles(_workContext.CurrentRole.Id).AsQueryable().Select(r => new ComboItem<string, string> { id = r.Id.ToString(), text = r.Name });
                var result = new PagedList<ComboItem<string, string>>(models, start / limit, limit);
                if(result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for(var i = 0; i < result.Count; i++) {
                        data.data.Add(result[i]);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetEmployees() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var employees = _employeeService.GetAllEmployees();
                var departments = _departmentService.GetAllDepartments();

                foreach(var dept in departments) {
                    var _employees = employees.ToList().FindAll(d => d.DeptId.Equals(dept.Id));
                    if(_employees.Count > 0) {
                        var root = new TreeModel() {
                            id = string.Format("department-{0}", dept.Id),
                            text = dept.Name,
                            icon = Icons.Department,
                            expanded = false,
                            leaf = false,
                            data = _employees.Select(emp => new TreeModel {
                                id = emp.Id,
                                text = emp.Name,
                                icon = Icons.Employee,
                                leaf = true,
                            }).ToList()
                        };

                        data.data.Add(root);
                    }
                }

                if(data.data.Count > 0) {
                    data.message = "200 Ok";
                    data.total = data.data.Count;
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Router() {
            var input = Request.InputStream;
            var buffer = new byte[input.Length];
            input.Read(buffer, 0, (int)input.Length);

            var inputString = Encoding.UTF8.GetString(buffer);
            var args = JsonConvert.DeserializeObject<ExtDirectArgs>(inputString);
            var result = new ExtDirectResult {
                action = args.action,
                method = args.method,
                tid = args.tid,
                type = args.type
            };

            try {
                if("user".Equals(result.action)) {
                    if("query".Equals(result.method)) {
                        var key = Common.GetCachedKey(SiteCacheKeys.Site_UsersResultPattern, _workContext);
                        if(_cacheManager.IsSet(key))
                            _cacheManager.Remove(key);
                    } else
                        throw new ArgumentException("参数无效 method");
                } else if("role".Equals(result.action)) {
                    if("query".Equals(result.method)) {
                        var key = Common.GetCachedKey(SiteCacheKeys.Site_RolesResultPattern, _workContext);
                        if(_cacheManager.IsSet(key))
                            _cacheManager.Remove(key);
                    } else
                        throw new ArgumentException("参数无效 method");
                } else
                    throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                result.type = "exception";
                result.message = exc.Message;
            }

            return Json(result);
        }

        [Authorize]
        public ActionResult Events() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetComboEventLevels(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                var result = Enum.GetValues(typeof(EnmEventLevel));
                if(result.Length > 0) {
                    data.message = "200 Ok";
                    data.total = result.Length;
                    foreach(EnmEventLevel level in result) {
                        data.data.Add(new ComboItem<int, string>() { 
                            id = (int)level, 
                            text = Common.GetEventLevelDisplay(level) 
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetComboEventTypes(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<int, string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ComboItem<int, string>>()
            };

            try {
                var result = Enum.GetValues(typeof(EnmEventType));
                if(result.Length > 0) {
                    data.message = "200 Ok";
                    data.total = result.Length;
                    foreach(EnmEventType type in result) {
                        data.data.Add(new ComboItem<int, string>() {
                            id = (int)type,
                            text = Common.GetEventTypeDisplay(type)
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetEvents(int start, int limit, int[] levels, int[] types, string startDate, string endDate) {
            var data = new AjaxDataModel<List<EventModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<EventModel>()
            };

            try {
                DateTime fromDate, toDate;
                DateTime.TryParse(startDate, out fromDate);
                DateTime.TryParse(endDate, out toDate);

                if(fromDate == DateTime.MinValue)
                    fromDate = DateTime.Today.AddDays(-1);
                if(toDate == DateTime.MinValue)
                    toDate = DateTime.Today;

                fromDate = fromDate.Date;
                toDate = toDate.Date.AddSeconds(86399);

                var _levels = new List<EnmEventLevel>();
                if(levels!=null && levels.Length>0){
                    foreach(var level in levels){
                        if(Enum.IsDefined(typeof(EnmEventLevel), level))
                            _levels.Add((EnmEventLevel)level);
                    }
                }

                var _types = new List<EnmEventType>();
                if(types!=null && types.Length>0){
                    foreach(var type in types){
                        if(Enum.IsDefined(typeof(EnmEventType), type))
                            _types.Add((EnmEventType)type);
                    }
                }

                var events = _webLogger.GetAllLogs(fromDate, toDate, _levels.Count > 0 ? _levels.ToArray() : null, _types.Count > 0 ? _types.ToArray() : null, start / limit, limit);
                var users = _userService.GetUsers().ToDictionary(k => k.Id, v => v.Uid);
                if(events.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = events.TotalCount;
                    if(events.Count > 0) {
                        for(var i = 0; i < events.Count;i++ ) {
                            data.data.Add(new EventModel {
                                index = start + i + 1,
                                id = events[i].Id.ToString(),
                                level = Common.GetEventLevelDisplay(events[i].Level),
                                type = Common.GetEventTypeDisplay(events[i].Type),
                                shortMessage = events[i].ShortMessage,
                                fullMessage = events[i].FullMessage,
                                ip = events[i].IpAddress,
                                page = events[i].PageUrl,
                                referrer = events[i].ReferrerUrl,
                                user = events[i].UserId.HasValue && users.ContainsKey(events[i].UserId.Value) ? users[events[i].UserId.Value] : "",
                                created = CommonHelper.DateTimeConverter(events[i].CreatedTime)
                            });
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadEvents(int[] levels, int[] types, string startDate, string endDate) {
            try {
                DateTime fromDate, toDate;
                DateTime.TryParse(startDate, out fromDate);
                DateTime.TryParse(endDate, out toDate);

                if(fromDate == DateTime.MinValue)
                    fromDate = DateTime.Today.AddDays(-1);
                if(toDate == DateTime.MinValue)
                    toDate = DateTime.Today;

                fromDate = fromDate.Date;
                toDate = toDate.Date.AddSeconds(86399);

                var _levels = new List<EnmEventLevel>();
                if(levels != null && levels.Length > 0) {
                    foreach(var level in levels) {
                        if(Enum.IsDefined(typeof(EnmEventLevel), level))
                            _levels.Add((EnmEventLevel)level);
                    }
                }

                var _types = new List<EnmEventType>();
                if(types != null && types.Length > 0) {
                    foreach(var type in types) {
                        if(Enum.IsDefined(typeof(EnmEventType), type))
                            _types.Add((EnmEventType)type);
                    }
                }

                var events = _webLogger.GetAllLogs(fromDate, toDate, _levels.Count > 0 ? _levels.ToArray() : null, _types.Count > 0 ? _types.ToArray() : null);
                if(events.Count == 0)
                    throw new iPemException("无数据");

                var users = _userService.GetUsers().ToDictionary(k => k.Id, v => v.Uid);
                var models = new List<EventModel>();
                for(var i = 0; i < events.Count; i++) {
                    models.Add(new EventModel {
                        index = i + 1,
                        id = events[i].Id.ToString(),
                        level = Common.GetEventLevelDisplay(events[i].Level),
                        type = Common.GetEventTypeDisplay(events[i].Type),
                        shortMessage = events[i].ShortMessage,
                        fullMessage = events[i].FullMessage,
                        ip = events[i].IpAddress,
                        page = events[i].PageUrl,
                        referrer = events[i].ReferrerUrl,
                        user = events[i].UserId.HasValue && users.ContainsKey(events[i].UserId.Value) ? users[events[i].UserId.Value] : "",
                        created = CommonHelper.DateTimeConverter(events[i].CreatedTime)
                    });
                }

                using(var ms = _excelManager.Export<EventModel>(models, "日志信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.AssociatedEmployee != null ? _workContext.AssociatedEmployee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearEvents() {
            try {
                var startDate = new DateTime(2000, 1, 1);
                var endDate = DateTime.Today.AddMonths(-3);
                _webLogger.Clear(startDate, endDate);
                _webLogger.Information(EnmEventType.Operating, string.Format("清理{0}之前的系统日志信息", CommonHelper.DateConverter(endDate)), null, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "日志清理完成" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Notice() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetNotices(int start, int limit, string begin, string end) {
            var data = new AjaxDataModel<List<NoticeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<NoticeModel>()
            };

            try {
                if(string.IsNullOrWhiteSpace(begin))
                    throw new ArgumentException("参数无效 begin");

                if(string.IsNullOrWhiteSpace(end))
                    throw new ArgumentException("参数无效 end");

                var beginDate = DateTime.Parse(begin);
                var endDate = DateTime.Parse(end).AddSeconds(86399);

                var result = _noticeService.GetAllNotices(beginDate,endDate,start / limit, limit);
                if(result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for(var i = 0; i < result.Count; i++) {
                        data.data.Add(new NoticeModel {
                            index = start+i+1,
                            id = result[i].Id.ToString(),
                            title = result[i].Title,
                            content = result[i].Content,
                            created = CommonHelper.DateTimeConverter(result[i].CreatedTime),
                            enabled = result[i].Enabled
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetNotice(string id, int action) {
            var data = new AjaxDataModel<NoticeModel> {
                success = true,
                message = "200 Ok",
                total = 1,
                data = new NoticeModel {
                    index = 1,
                    id = Guid.NewGuid().ToString(),
                    title = "",
                    content = "",
                    created = CommonHelper.DateTimeConverter(DateTime.Now),
                    enabled = true
                }
            };

            try {
                if(action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                if(action != (int)EnmAction.Edit)
                    throw new ArgumentException("参数无效 action");

                var notice = _noticeService.GetNotice(new Guid(id));
                if(notice == null)
                    throw new iPemException("未找到数据对象");

                data.data.id = notice.Id.ToString();
                data.data.title = notice.Title;
                data.data.content = notice.Content;
                data.data.created = CommonHelper.DateConverter(notice.CreatedTime);
                data.data.enabled = notice.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveNotice(NoticeModel notice, string[] roles, int action) {
            try {
                if(notice == null)
                    throw new ArgumentException("参数无效 notice");

                if(action == (int)EnmAction.Add) {
                    var existed = _noticeService.GetNotice(new Guid(notice.id));
                    if(existed != null)
                        throw new iPemException("消息已存在，保存失败。");

                    var one = new MsDomain.Notice {
                        Id = new Guid(notice.id),
                        Title = notice.title,
                        Content = notice.content,
                        CreatedTime = DateTime.Now,
                        Enabled = notice.enabled
                    };

                    var toUsers = new List<MsDomain.User>();
                    var allUsers = _userService.GetUsers();
                    if(roles != null && roles.Length > 0) {
                        foreach(var id in roles) {
                            var usersInRole = allUsers.Where(u => u.RoleId == new Guid(id));
                            if(usersInRole.Any())
                                toUsers.AddRange(usersInRole);
                        }
                    } else {
                        toUsers.AddRange(allUsers);
                    }

                    var noticesInUsers = new List<MsDomain.NoticeInUser>();
                    foreach(var user in toUsers) {
                        noticesInUsers.Add(new MsDomain.NoticeInUser {
                            NoticeId = one.Id,
                            UserId = user.Id,
                            Readed = false,
                            ReadTime = DateTime.MinValue
                        });
                    }

                    _noticeService.AddNotice(one);
                    _noticeInUserService.AddNoticesInUsers(noticesInUsers);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增消息[{0}]", one.Title), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "消息保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existed = _noticeService.GetNotice(new Guid(notice.id));
                    if(existed == null)
                        throw new iPemException("消息不存在，保存失败。");

                    existed.Title = notice.title;
                    existed.Content = notice.content;
                    existed.CreatedTime = DateTime.Now;
                    existed.Enabled = notice.enabled;

                    var toUsers = new List<MsDomain.User>();
                    var allUsers = _userService.GetUsers();
                    if(roles != null && roles.Length > 0) {
                        foreach(var id in roles) {
                            var usersInRole = allUsers.Where(u => u.RoleId == new Guid(id));
                            if(usersInRole.Any())
                                toUsers.AddRange(usersInRole);
                        }
                    } else {
                        toUsers.AddRange(allUsers);
                    }

                    var noticesInUsers = new List<MsDomain.NoticeInUser>();
                    foreach(var user in toUsers) {
                        noticesInUsers.Add(new MsDomain.NoticeInUser {
                            NoticeId = existed.Id,
                            UserId = user.Id,
                            Readed = false,
                            ReadTime = DateTime.MinValue
                        });
                    }

                    _noticeService.DeleteNotice(existed);
                    _noticeService.AddNotice(existed);
                    _noticeInUserService.AddNoticesInUsers(noticesInUsers);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新消息[{0}]", existed.Title), null, _workContext.CurrentUser.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "消息保存成功" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteNotice(string id) {
            try {
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("参数无效 id");

                var existed = _noticeService.GetNotice(new Guid(id));
                if(existed == null)
                    throw new iPemException("消息不存在，删除失败");

                _noticeService.DeleteNotice(existed);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除系统消息[{0}]", existed.Title), null, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "消息删除成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.CurrentUser.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        public ActionResult Dictionary() {
            return View();
        }

        #endregion

    }
}