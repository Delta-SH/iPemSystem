using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.Organization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace iPem.Site.Controllers {
    public class AccountController : Controller {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebLogger _webLogger;
        private readonly IAreasInRoleService _areaInRoleService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IEmployeeService _employeeService;
        private readonly IFormulaService _formulaService;
        private readonly IMenuService _menuService;
        private readonly IMenusInRoleService _menusInRoleService;
        private readonly INoticeService _noticeService;
        private readonly INoticeInUserService _noticeInUserService;
        private readonly IOperateInRoleService _operateInRoleService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private const string _captchaSalt = "w9hRaAIX+tRJ4GD4wnVkVQ==";

        #endregion

        #region Ctor

        public AccountController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebLogger webLogger,
            IAreasInRoleService areaInRoleService,
            IDictionaryService dictionaryService,
            IEmployeeService employeeService,
            IFormulaService formulaService,
            IMenuService menuService,
            IMenusInRoleService menusInRoleService,
            INoticeService noticeService,
            INoticeInUserService noticeInUserService,
            IOperateInRoleService operateInRoleService,
            IRoleService roleService,
            IUserService userService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._areaInRoleService = areaInRoleService;
            this._dictionaryService = dictionaryService;
            this._employeeService = employeeService;
            this._formulaService = formulaService;
            this._menuService = menuService;
            this._menusInRoleService = menusInRoleService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._operateInRoleService = operateInRoleService;
            this._roleService = roleService;
            this._userService = userService;
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
                        ExpireUtc = now.Add(CachedIntervals.Store_Intervals),
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
        public ActionResult GetCurrentPhoto() {
            if(_workContext.Employee != null && _workContext.Employee.Photo != null) {
                return File(_workContext.Employee.Photo, @"image/png");
            } else if(_workContext.Employee.Sex == EnmSex.Female) {
                var photo = Convert.FromBase64String(@"iVBORw0KGgoAAAANSUhEUgAAABwAAAAcCAIAAAD9b0jDAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MTFEOEQ4NTY5MjdCMTFFNThCQ0RDMzY1NUJFQjdBQTkiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MTFEOEQ4NTc5MjdCMTFFNThCQ0RDMzY1NUJFQjdBQTkiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDoxMUQ4RDg1NDkyN0IxMUU1OEJDREMzNjU1QkVCN0FBOSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDoxMUQ4RDg1NTkyN0IxMUU1OEJDREMzNjU1QkVCN0FBOSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PuDyG6MAAAQISURBVHjanFRJbBtlFJ75x+OZ8TjjLa7rxCUloUXETZQuWYRKxRIVxFYVRA8cuVZCVEhFHLj0CKpYhMQBTlWvLKJSJUgjSkMR2ZoqaWPHtHZSY9fx7vFsnhnPz+8FN1i202Q0h/+993+f3nvf+x8uZmNYx68sxozcHaOUwoDZxB0knEMYTnSGmDrE9PScsvZduRDY6gSMlx58n/Qc7wDE22VaundJCX3bDsYMnTfve2NnpFp8Srp9oVMRGonRT3UNv2f2TjweqS7y189AlW/b5RIm5WAVjbme/5h58rWmC6BF4dGrWxlNnJ/qOU153ySsB5AJDUwuwHoMYvm/vngsofSH040z1XOKtB+rnUnHqM7fFe5fMVtJo6zqkgghVpZlQ/gHWH2dSGFZ0QvBesw20mBsZG0/7K+dlcRsPnAF8arZVfr/pM3lG8IGIq60i2Cpva900IreO05y7gpEzm5TvlFKVxUgKN87uc1iSU4ig7LQDq8HGvBheF0WRKQP53Ls8flM3CGV/03NPLB0JtWSdyCG49SwiR1wWKAglUS5RJsBZsD4vYjFZgU2ZxdLswxVFY3WSxAz0dupT+0RU4YulSrp4vjP15e+vDxl4bpwACSh6PR4Ll765erMMgqhC6qYk3OQsI9spz7pQC3VxM2a9e6rE/C/+cGxCtGn585UCSufmt9s2W7Q+sFIGUOTa8kCUOfACaBrGjJraWIQKm2eeGtSBFDzkSafd3+fIkoNEzEamrITUoRJBZo8rI2z2m0NU4wFdrz6lNRdQ38dmCoqFzJ8IlYZNZfH3u1xViuBfORWO2zbTGFZTa+v1Bedlcml8rl0wWpja57MelCTCrtZ0mpiUe4dYhjKTJETLx155Fe1bPDGLje/mVD4vIAoOI6tKY6mSxAkQRBNuqjvjhSnHQzLSJLMFwWKMhM4rigaIHAry2oWuy6kd0aq0f0Wzk31HkcJsqyF0suqphkQWliGNFUg9sOn+LU/hGwS8JFOm59PJUNzc6H5+Vgk+cmPP6zeuOYEUbzxeprnGObgE4MnJi+cftvX5z4wPvr02FhXt7ue6fL0VGhxObwaTicebbC/F28feuHl1d+v2Y11HDTzGhDjQb//uRdDi0upRBb9S7NrGHbZ7XX2+wcOHhkh6I1sLBKXBHkrTBaKx05Ouvf35yVKSMcpEjZCvAhI37MDR0fR+fvPv97ceNAIIZJYOL5881brnq7M/BmYnX9mfHTfoL/o7fnp4mdlKYNqBmz3Wx+etzrs6E5wbmFl5mZLODHW29cysDa3cPTkJErkq7Pn7q8E49F0PJqJhaMLv04PjAyjVn/zwUeKJLUem7NjJ9pNhqvHy6czmqo2b0eKsnW70rF4O+C/AgwABxjY7MvCiokAAAAASUVORK5CYII=");
                return File(photo, @"image/png");
            } else {
                var photo = Convert.FromBase64String(@"iVBORw0KGgoAAAANSUhEUgAAABwAAAAcCAIAAAD9b0jDAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MDhDNDdFNEQ5MjdCMTFFNTgwNkM5MDlCQUE5OEEyNkYiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MDhDNDdFNEU5MjdCMTFFNTgwNkM5MDlCQUE5OEEyNkYiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDowOEM0N0U0QjkyN0IxMUU1ODA2QzkwOUJBQTk4QTI2RiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDowOEM0N0U0QzkyN0IxMUU1ODA2QzkwOUJBQTk4QTI2RiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PuuUA5kAAAPCSURBVHjarFbdbxRVFL93Pnd2Z3bodtrurmUL/ZBCC0aqtqWgAXmAhBAeNDGx8mBIiD6gf4BP+PEiEH3ynRC0MSFBI2iIbEtti0p3t7RAwKK2ARbW/f7o7p2dmeudThCKnXGXeHMnOXPuzO/c8zu/c2eYUvou+L8H47CGq8Xy9c8odxAyEoAY6CrFK7TSR7manhoU69k5LR7GhrrCDWl+3Wuu7ncBpOoDxdV8KfKBnoquGgv9MQIYl6vrkB0oXJXT4i/v6cmIfRJUFXkxH/K+8A7f3LvK8r9dWvLXfxAh42GkbsjKj63T5YxeyWTQ/Znk+SNabqGm9LXEpGWwDf2cfw+EDDBUlPhRy88CKAC2Q1TaDLVYWriko1xp/nu57/B/gxqVhLkgdfOBfQ/z4Xj/XjJX5Mi5s3MjeiFeU/oQsuY2fYPOuuHktmUApjZQzzOmcIRWZ1CalyBFMVKwJk5pbydkRJJeIZ0pZXMkAMuxvqAfA3Dv9u8qQoaB1yiNSjAAaDfr66oJlGkeYpRRDDAwsKelpVRWPS66itCDxTsN/uaCiiUPr+YLGGOhtd/VOlBb+qQs7W9AACXF981o7PNTF1yiyAkCQkiU5eMnfzg3PuvzN0MIpefehDRbq/iBoeuL42b7mANQFCT2/NW5zi29JHdIApoXRYe2r9qsNv1L0VZZycsWosk1bT5Mbk1E02Lt2t/2UID0k3Sv79m0sqCsbRc7SMZZUpDm6j76ICfiCtETyKXy0alrhF2rMbYObfY2iMvqF+sHlQI4f5eQJzd6e/o2xBcThMC164MWIolAS/76D2lWzBVV2cOSsjQFfGQ+OlExzi1pjYy7fk7JZt3K/XhSVauPO8ltPJ6EbqcvCnT48GHajTJ3ErNhQWB43iwLqqClitayZTcnt1AGqmOnpHN+Cl/8+OiH6ULZ19a7btfbGq+kUlkydVdT+6uHfKFNyVz5k6MfTYyGVaTa7pTQNH/r5lg4PHE5Ertxu4I04jz91ZdDgy8ur4KTY9eJ6odf3mgJf2x86uDwW8QQePb5nvZt/X2v7NzZ0fWs1RfMd2fPXBq//HP0xoNU7omA0WjMAp24p33651pidHTp24I0Maanp61nyqg6GblJ5rEvTvsVeWBrz47tgzAU6rCj5qWBga9HTmXypfcvliZTAvHsaFw6sUtc4/XsP/D6TDT6ND8TkStXEsnsrYX4wSDOp72EhuFA4dpv6c6Q/2os5vCik6Q0TTt77oJhGH8VwExsaSZWTheBbhhnvj2PrQazGX8LMAA0pn/L4p+vyAAAAABJRU5ErkJggg==");
                return File(photo, @"image/png");
            }
        }

        /**
         *TODO:跳转权限判断
         *在跳转时需要判断一下该角色下是否拥有这个菜单权限，每个Action跳转时都需要判断
         */
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
                var names = Common.SplitCondition(condition);
                var roles = names.Length == 0 ?
                    _roleService.GetAllRoles(start / limit, limit) :
                    _roleService.GetRoles(names, start / limit, limit);

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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                data = new RoleModel { index = 1, id = Guid.NewGuid().ToString(), name = "", comment = "", enabled = true, menus = new string[] { }, areas = new string[] { }, operates = new string[] { } }
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

                var menus = _menuService.GetMenusAsList(role.Id);
                var areas = _areaInRoleService.GetKeysAsList(role.Id);
                var operate = _operateInRoleService.GetOperates(role.Id);
                data.data.id = role.Id.ToString();
                data.data.name = role.Name;
                data.data.comment = role.Comment;
                data.data.enabled = role.Enabled;
                data.data.menus = menus.Select(m => m.Id.ToString()).ToArray();
                data.data.areas = areas.ToArray();
                data.data.operates = operate.Operates.Select(o => ((int)o).ToString()).ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetAllMenus() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var menus = _menuService.GetAllMenusAsList();
                if(menus.Count > 0) {
                    var roots = new List<Menu>();
                    foreach(var menu in menus) {
                        if(!menus.Any(m => m.Id == menu.LastId))
                            roots.Add(menu);
                    }

                    var _menus = roots.OrderBy(m => m.Index).ToList();
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

        private void MenusRecursion(List<Menu> menus, int pid, TreeModel node) {
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
        public JsonNetResult GetAllAreas() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(_workContext.Areas.Count > 0) {
                    var roots = _workContext.Areas.FindAll(m => !m.HasParents);
                    if(roots.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = _workContext.Areas.Count;
                        for(var i = 0; i < roots.Count; i++) {
                            var current = roots[i];
                            var root = new TreeModel {
                                id = current.Current.Id,
                                text = current.Current.Name,
                                selected = false,
                                icon = Icons.Diqiu,
                                expanded = false,
                                leaf = false
                            };

                            AreasRecursion(current, root);
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

        private void AreasRecursion(OrgArea parent, TreeModel node) {
            if(parent.HasChildren) {
                node.data = new List<TreeModel>();
                for(var i = 0; i < parent.ChildRoot.Count; i++) {
                    var current = parent.ChildRoot[i];
                    var child = new TreeModel {
                        id = current.Current.Id,
                        text = current.Current.Name,
                        selected = false,
                        icon = Icons.Diqiu,
                        expanded = false,
                        leaf = false
                    };

                    AreasRecursion(current, child);
                    node.data.Add(child);
                }
            } else {
                node.leaf = true;
                node.icon = Icons.Dingwei;
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetAllOperates() {
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

                if(role.menus == null || role.menus.Length == 0)
                    throw new iPemException("无效的菜单权限，保存失败。");

                if(role.areas == null || role.areas.Length == 0)
                    throw new iPemException("无效的区域权限，保存失败。");

                var menus = new MenusInRole {
                    RoleId = new Guid(role.id),
                    Menus = role.menus.Select(i => new Menu { Id = int.Parse(i) }).ToList()
                };

                var areas = role.areas.ToList();

                var operates = new OperateInRole {
                    RoleId = new Guid(role.id),
                    Operates = role.operates != null && role.operates.Length > 0 ? role.operates.Select(o => (EnmOperation)(int.Parse(o))).ToList() : new List<EnmOperation>()
                };

                if(action == (int)EnmAction.Add) {
                    var existedRole = _roleService.GetRole(role.name);
                    if(existedRole != null)
                        throw new iPemException("角色已存在，保存失败。");

                    var newRole = new Role {
                        Id = new Guid(role.id),
                        Name = role.name,
                        Comment = role.comment,
                        Enabled = role.enabled
                    };

                    _roleService.Add(newRole);
                    _menusInRoleService.AddMenusInRole(menus);
                    _areaInRoleService.Add(new Guid(role.id), areas);
                    _operateInRoleService.Add(operates);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增角色[{0}]", newRole.Name), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "角色保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existedRole = _roleService.GetRole(role.name);
                    if(existedRole == null)
                        throw new iPemException("角色不存在，保存失败。");

                    //existedRole.Id = new Guid(role.id);
                    existedRole.Name = role.name;
                    existedRole.Comment = role.comment;
                    existedRole.Enabled = role.enabled;

                    _roleService.Update(existedRole);
                    _menusInRoleService.DeleteMenusInRole(existedRole.Id);
                    _areaInRoleService.Remove(existedRole.Id);
                    _operateInRoleService.Remove(existedRole.Id);
                    _menusInRoleService.AddMenusInRole(menus);
                    _areaInRoleService.Add(new Guid(role.id), areas);
                    _operateInRoleService.Add(operates);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新角色[{0}]", existedRole.Name), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "角色保存成功" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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

                _roleService.Remove(existedRole);
                _menusInRoleService.DeleteMenusInRole(existedRole.Id);
                _areaInRoleService.Remove(existedRole.Id);
                _operateInRoleService.Remove(existedRole.Id);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除角色[{0}]", existedRole.Name), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "角色删除成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadRoles(string condition) {
            try {
                var names = Common.SplitCondition(condition);
                var roles = names.Length == 0 ?
                    _roleService.GetAllRolesAsList() :
                    _roleService.GetRolesAsList(names);

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

                using(var ms = _excelManager.Export<RoleModel>(models, "角色信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [Authorize]
        public ActionResult Users() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetUsers(int start, int limit, string[] roles, string[] names) {
            var data = new AjaxDataModel<List<UserModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<UserModel>()
            };

            try {
                var users = _userService.GetUsersAsList(_workContext.Role.Id);
                if(roles != null && roles.Length > 0)
                    users = users.FindAll(u => roles.Contains(u.RoleId.ToString()));

                if(names != null && names.Length > 0)
                    users = users.FindAll(u => CommonHelper.ConditionContain(u.Uid, names));

                if(users.Count > 0) {
                    var allRoles = _roleService.GetRolesAsList(_workContext.Role.Id);
                    var employees = _employeeService.GetAllEmployeesAsList();
                    var models = (from user in users
                                  join role in allRoles on user.RoleId equals role.Id
                                  join emp in employees on user.EmployeeId equals emp.Id into lt
                                  from def in lt.DefaultIfEmpty()
                                  select new UserModel {
                                      id = user.Id.ToString(),
                                      uid = user.Uid,
                                      roleId = role.Id.ToString(),
                                      roleName = role.Name,
                                      empId = user.EmployeeId,
                                      empName = def != null ? def.Name : "",
                                      empNo = def != null ? def.Code : "",
                                      sex = (int)(def != null ? def.Sex : EnmSex.Male),
                                      sexName = def != null ? Common.GetSexDisplay(def.Sex) : Common.GetSexDisplay(EnmSex.Male),
                                      mobile = def != null ? def.MobilePhone : "",
                                      email = def != null ? def.Email : "",
                                      created = CommonHelper.DateTimeConverter(user.CreatedDate),
                                      limited = CommonHelper.DateConverter(user.LimitedDate),
                                      lastLogined = CommonHelper.DateTimeConverter(user.LastLoginDate),
                                      lastPasswordChanged = CommonHelper.DateTimeConverter(user.LastPasswordChangedDate),
                                      isLockedOut = user.IsLockedOut,
                                      lastLockedout = CommonHelper.DateTimeConverter(user.LastLockoutDate),
                                      comment = user.Comment,
                                      enabled = user.Enabled
                                  }).ToList();

                    if(models.Count > 0) {
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
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; 
                data.message = exc.Message;
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
                    data.data.roleId = _workContext.Role.Id.ToString();
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
                data.data.limited = CommonHelper.DateConverter(user.LimitedDate);
                data.data.isLockedOut = user.IsLockedOut;
                data.data.comment = user.Comment;
                data.data.enabled = user.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; 
                data.message = exc.Message;
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
                var user = _workContext.User;
                var role = _workContext.Role;
                var employee = _workContext.Employee;

                data.data.id = user.Id.ToString();
                data.data.uid = user.Uid;
                data.data.roleId = role.Id.ToString();
                data.data.roleName = role.Name;
                data.data.empId = user.EmployeeId;
                data.data.empName = employee != null ? employee.Name : "";
                data.data.empNo = employee != null ? employee.Code : "";
                data.data.sex = (int)(employee != null ? employee.Sex : EnmSex.Male);
                data.data.sexName = employee != null ? Common.GetSexDisplay(employee.Sex) : Common.GetSexDisplay(EnmSex.Male);
                data.data.mobile = employee != null ? employee.MobilePhone : "";
                data.data.email = employee != null ? employee.Email : "";
                data.data.created = CommonHelper.DateTimeConverter(user.CreatedDate);
                data.data.limited = CommonHelper.DateConverter(user.LimitedDate);
                data.data.lastLogined = CommonHelper.DateTimeConverter(user.LastLoginDate);
                data.data.lastPasswordChanged = CommonHelper.DateTimeConverter(user.LastPasswordChangedDate);
                data.data.isLockedOut = user.IsLockedOut;
                data.data.lastLockedout = CommonHelper.DateTimeConverter(user.LastLockoutDate);
                data.data.comment = user.Comment;
                data.data.enabled = user.Enabled;

                data.message = "200 Ok";
                data.total = 1;
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; 
                data.message = exc.Message;
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

                    var newUser = new User {
                        Id = new Guid(user.id),
                        RoleId = new Guid(user.roleId),
                        Uid = user.uid,
                        Password = user.password,
                        CreatedDate = DateTime.Now,
                        LimitedDate = DateTime.Parse(user.limited),
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

                    _userService.Add(newUser);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增用户[{0}]", newUser.Uid), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existedUser = _userService.GetUser(user.uid);
                    if(existedUser == null)
                        throw new iPemException("用户不存在，保存失败。");

                    existedUser.RoleId = new Guid(user.roleId);
                    existedUser.LimitedDate = DateTime.Parse(user.limited);
                    existedUser.FailedPasswordAttemptCount = 0;
                    existedUser.IsLockedOut = user.isLockedOut;
                    if(user.isLockedOut) existedUser.LastLockoutDate = DateTime.Now;
                    existedUser.Comment = user.comment;
                    existedUser.EmployeeId = user.empId;
                    existedUser.Enabled = user.enabled;

                    _userService.Update(existedUser);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新用户[{0}]", existedUser.Uid), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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

                if(_workContext.User.Uid.Equals(existedUser.Uid, StringComparison.CurrentCultureIgnoreCase))
                    throw new iPemException("无法删除当前用户");

                _userService.Remove(existedUser);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除用户[{0}]", existedUser.Uid), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                _webLogger.Information(EnmEventType.Operating, string.Format("重置用户密码[{0}]", existedUser.Uid), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "密码重置成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                    _webLogger.Information(EnmEventType.Operating, string.Format("修改用户密码[{0}]", existedUser.Uid), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "密码修改成功" });
                } else if(result == EnmChangeResults.WrongPassword) {
                    throw new iPemException("原始密码错误");
                } else {
                    throw new iPemException("未知错误");
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadUsers(string[] roles, string[] names) {
            try {
                var users = _userService.GetUsersAsList(_workContext.Role.Id);
                if(roles != null && roles.Length > 0)
                    users = users.FindAll(u => roles.Contains(u.RoleId.ToString()));

                if(names != null && names.Length > 0)
                    users = users.FindAll(u => CommonHelper.ConditionContain(u.Uid, names));

                var models = new List<UserModel>();
                if(users.Count > 0) {
                    var allRoles = _roleService.GetRolesAsList(_workContext.Role.Id);
                    var employees = _employeeService.GetAllEmployeesAsList();
                    models = (from user in users
                              join role in allRoles on user.RoleId equals role.Id
                              join employee in employees on user.EmployeeId equals employee.Id into lt
                              from def in lt.DefaultIfEmpty()
                              select new UserModel {
                                  id = user.Id.ToString(),
                                  uid = user.Uid,
                                  roleId = role.Id.ToString(),
                                  roleName = role.Name,
                                  empId = user.EmployeeId,
                                  empName = def != null ? def.Name : "",
                                  empNo = def != null ? def.Code : "",
                                  sex = (int)(def != null ? def.Sex : EnmSex.Male),
                                  sexName = def != null ? Common.GetSexDisplay(def.Sex) : Common.GetSexDisplay(EnmSex.Male),
                                  mobile = def != null ? def.MobilePhone : "",
                                  email = def != null ? def.Email : "",
                                  created = CommonHelper.DateTimeConverter(user.CreatedDate),
                                  limited = CommonHelper.DateConverter(user.LimitedDate),
                                  lastLogined = CommonHelper.DateTimeConverter(user.LastLoginDate),
                                  lastPasswordChanged = CommonHelper.DateTimeConverter(user.LastPasswordChangedDate),
                                  isLockedOut = user.IsLockedOut,
                                  lastLockedout = CommonHelper.DateTimeConverter(user.LastLockoutDate),
                                  comment = user.Comment,
                                  enabled = user.Enabled
                              }).ToList();
                }

                for(var i = 0; i < models.Count; i++)
                    models[i].index = i + 1;

                using(var ms = _excelManager.Export<UserModel>(models.ToList(), "用户信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                var models = _roleService.GetRolesAsList(_workContext.Role.Id).Select(r => new ComboItem<string, string> { id = r.Id.ToString(), text = r.Name });
                var result = new PagedList<ComboItem<string, string>>(models, start / limit, limit, models.Count());
                if(result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for(var i = 0; i < result.Count; i++) {
                        data.data.Add(result[i]);
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult GetEvents(int start, int limit, int[] levels, int[] types, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<EventModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<EventModel>()
            };

            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

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

                var events = _webLogger.GetAllLogs(startDate, endDate, _levels.Count > 0 ? _levels.ToArray() : null, _types.Count > 0 ? _types.ToArray() : null, start / limit, limit);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadEvents(int[] levels, int[] types, DateTime startDate, DateTime endDate) {
            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

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

                var events = _webLogger.GetAllLogs(startDate, endDate, _levels.Count > 0 ? _levels.ToArray() : null, _types.Count > 0 ? _types.ToArray() : null);
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

                using(var ms = _excelManager.Export<EventModel>(models, "日志信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee != null ? _workContext.Employee.Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearEvents() {
            try {
                var startDate = new DateTime(2016, 1, 1);
                var endDate = DateTime.Today.AddMonths(-3);
                _webLogger.Clear(startDate, endDate);
                _webLogger.Information(EnmEventType.Operating, string.Format("清理{0}之前的系统日志信息", CommonHelper.DateConverter(endDate)), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "日志清理完成" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Notice() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetNotices(int start, int limit, DateTime startDate, DateTime endDate) {
            var data = new AjaxDataModel<List<NoticeModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<NoticeModel>()
            };

            try {
                startDate = startDate.Date;
                endDate = endDate.Date.AddSeconds(86399);

                var notices = _noticeService.GetNotices(startDate, endDate, start / limit, limit);
                if(notices.Count > 0) {
                    data.message = "200 Ok";
                    data.total = notices.TotalCount;
                    for(var i = 0; i < notices.Count; i++) {
                        data.data.Add(new NoticeModel {
                            index = start+i+1,
                            id = notices[i].Id.ToString(),
                            title = notices[i].Title,
                            content = notices[i].Content,
                            created = CommonHelper.DateTimeConverter(notices[i].CreatedTime),
                            enabled = notices[i].Enabled
                        });
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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

                    var one = new Notice {
                        Id = new Guid(notice.id),
                        Title = notice.title,
                        Content = notice.content,
                        CreatedTime = DateTime.Now,
                        Enabled = notice.enabled
                    };

                    var toUsers = new List<User>();
                    var allUsers = _userService.GetUsersAsList();
                    if(roles != null && roles.Length > 0) {
                        foreach(var role in roles) {
                            var usersInRole = allUsers.FindAll(u => u.RoleId == new Guid(role));
                            if(usersInRole.Count > 0) toUsers.AddRange(usersInRole);
                        }
                    } else {
                        toUsers.AddRange(allUsers);
                    }

                    var noticesInUsers = new List<NoticeInUser>();
                    foreach(var user in toUsers) {
                        noticesInUsers.Add(new NoticeInUser {
                            NoticeId = one.Id,
                            UserId = user.Id,
                            Readed = false,
                            ReadTime = DateTime.MinValue
                        });
                    }

                    _noticeService.Add(one);
                    _noticeInUserService.AddRange(noticesInUsers);
                    _webLogger.Information(EnmEventType.Operating, string.Format("新增消息[{0}]", one.Title), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "消息保存成功" });
                } else if(action == (int)EnmAction.Edit) {
                    var existed = _noticeService.GetNotice(new Guid(notice.id));
                    if(existed == null)
                        throw new iPemException("消息不存在，保存失败。");

                    existed.Title = notice.title;
                    existed.Content = notice.content;
                    existed.CreatedTime = DateTime.Now;
                    existed.Enabled = notice.enabled;

                    var toUsers = new List<User>();
                    var allUsers = _userService.GetUsersAsList();
                    if(roles != null && roles.Length > 0) {
                        foreach(var role in roles) {
                            var usersInRole = allUsers.FindAll(u => u.RoleId == new Guid(role));
                            if(usersInRole.Count > 0) toUsers.AddRange(usersInRole);
                        }
                    } else {
                        toUsers.AddRange(allUsers);
                    }

                    var noticesInUsers = new List<NoticeInUser>();
                    foreach(var user in toUsers) {
                        noticesInUsers.Add(new NoticeInUser {
                            NoticeId = existed.Id,
                            UserId = user.Id,
                            Readed = false,
                            ReadTime = DateTime.MinValue
                        });
                    }

                    _noticeService.Remove(existed);
                    _noticeService.Add(existed);
                    _noticeInUserService.AddRange(noticesInUsers);
                    _webLogger.Information(EnmEventType.Operating, string.Format("更新消息[{0}]", existed.Title), null, _workContext.User.Id);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "消息保存成功" });
                }

                throw new ArgumentException("参数无效 action");
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
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

                _noticeService.Remove(existed);
                _webLogger.Information(EnmEventType.Operating, string.Format("删除系统消息[{0}]", existed.Title), null, _workContext.User.Id);
                return Json(new AjaxResultModel { success = true, code = 200, message = "消息删除成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [Authorize]
        public ActionResult Dictionary() {
            return View();
        }

        [AjaxAuthorize]
        public JsonResult GetWs() {
            var data = new AjaxDataModel<WsValues> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new WsValues {
                    ip = "",
                    port = 8080,
                    uid = "",
                    password = "",
                    dataPath = "",
                    orderPath = ""
                }
            };

            try {

                var ws = _dictionaryService.GetDictionary((int)EnmDictionary.Ws);
                if(ws != null && !string.IsNullOrWhiteSpace(ws.ValuesJson))
                    data.data = JsonConvert.DeserializeObject<WsValues>(ws.ValuesJson);

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveWs(WsValues values) {
            try {
                if(values == null)
                    throw new ArgumentException("参数无效 values");

                _dictionaryService.Update(new Dictionary {
                    Id = (int)EnmDictionary.Ws,
                    Name = Common.GetDictionaryDisplay(EnmDictionary.Ws),
                    ValuesJson = JsonConvert.SerializeObject(values),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });
                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetTs() {
            var data = new AjaxDataModel<TsValues> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new TsValues {
                    basic = new int[] { },
                    levels = new int[] { },
                    contents = new int[] { },
                    stationTypes = new string[] { },
                    roomTypes = new string[] { },
                    deviceTypes = new string[] { },
                    logicTypes = new string[] { },
                    pointNames = "",
                    pointExtset = ""
                }
            };

            try {
                var ts = _dictionaryService.GetDictionary((int)EnmDictionary.Ts);
                if(ts != null && !string.IsNullOrWhiteSpace(ts.ValuesJson))
                    data.data = JsonConvert.DeserializeObject<TsValues>(ts.ValuesJson);

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveTs(TsValues values) {
            try {
                if(values == null)
                    throw new ArgumentException("参数无效 values");

                _dictionaryService.Update(new Dictionary {
                    Id = (int)EnmDictionary.Ts,
                    Name = Common.GetDictionaryDisplay(EnmDictionary.Ts),
                    ValuesJson = JsonConvert.SerializeObject(values),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });

                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetRt() {
            var data = new AjaxDataModel<RtValues> {
                success = true,
                message = "200 Ok",
                total = 0,
                data = new RtValues() {
                    chaoPin = 1,
                    chaoDuan = 1,
                    chaoChang = 1,
                    weiTingDian = 0,
                    tingDian = 1,
                    weiFaDian = 0,
                    faDian = 1,
                    whlHuLue = 0,
                    jslGuiDing = 0,
                    jslHuLue = 0,
                    jslQueRen = 0
                }
            };

            try {
                var rt = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
                if(rt != null && !string.IsNullOrWhiteSpace(rt.ValuesJson))
                    data.data = JsonConvert.DeserializeObject<RtValues>(rt.ValuesJson);

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveRt(RtValues values) {
            try {
                if(values == null)
                    throw new ArgumentException("参数无效 values");

                _dictionaryService.Update(new Dictionary {
                    Id = (int)EnmDictionary.Report,
                    Name = Common.GetDictionaryDisplay(EnmDictionary.Report),
                    ValuesJson = JsonConvert.SerializeObject(values),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });

                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetFormula(string current) {
            var data = new AjaxDataModel<FormulaModel> {
                success = true,
                message = "200 OK",
                total = 0,
                data = new FormulaModel()
            };

            try {
                var keys = Common.SplitKeys(current);
                if(keys.Length == 2) {
                    var type = int.Parse(keys[0]);
                    var id = keys[1];
                    var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                    if (nodeType == EnmOrganization.Station || nodeType == EnmOrganization.Room){
                        var formulas = _formulaService.GetFormulasAsList(id, nodeType);
                        foreach(var formula in formulas) {
                            switch(formula.FormulaType) {
                                case EnmFormula.KT:
                                    data.data.ktFormulas = formula.FormulaText;
                                    data.data.ktRemarks = formula.Comment;
                                    break;
                                case EnmFormula.ZM:
                                    data.data.zmFormulas = formula.FormulaText;
                                    data.data.zmRemarks = formula.Comment;
                                    break;
                                case EnmFormula.BG:
                                    data.data.bgFormulas = formula.FormulaText;
                                    data.data.bgRemarks = formula.Comment;
                                    break;
                                case EnmFormula.SB:
                                    data.data.sbFormulas = formula.FormulaText;
                                    data.data.sbRemarks = formula.Comment;
                                    break;
                                case EnmFormula.KGDY:
                                    data.data.kgdyFormulas = formula.FormulaText;
                                    data.data.kgdyRemarks = formula.Comment;
                                    break;
                                case EnmFormula.UPS:
                                    data.data.upsFormulas = formula.FormulaText;
                                    data.data.upsRemarks = formula.Comment;
                                    break;
                                case EnmFormula.QT:
                                    data.data.qtFormulas = formula.FormulaText;
                                    data.data.qtRemarks = formula.Comment;
                                    break;
                                case EnmFormula.ZL:
                                    data.data.zlFormulas = formula.FormulaText;
                                    data.data.zlRemarks = formula.Comment;
                                    break;
                                case EnmFormula.PUE:
                                    data.data.pueFormulas = formula.FormulaText;
                                    data.data.pueRemarks = formula.Comment;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        /**
         *TODO:判断变量中设备和信号的有效性
         *在保存公式之前，需要判断每个公式中变量里的设备、信号是否存在
         */
        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveFormula(string current, FormulaModel formula) {
            try {
                var keys = Common.SplitKeys(current);
                if(keys.Length != 2)
                    throw new ArgumentException("参数无效 current");

                if(formula == null)
                    throw new ArgumentException("参数无效 formula");

                var type = int.Parse(keys[0]);
                var id = keys[1];
                var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                if(nodeType != EnmOrganization.Station && nodeType != EnmOrganization.Room)
                    throw new ArgumentException("能耗对象仅支持站点、机房");

                var formulas = new List<Formula>();
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.KT, FormulaText = formula.ktFormulas, Comment = formula.ktRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.ZM, FormulaText = formula.zmFormulas, Comment = formula.zmRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.BG, FormulaText = formula.bgFormulas, Comment = formula.bgRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.SB, FormulaText = formula.sbFormulas, Comment = formula.sbRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.KGDY, FormulaText = formula.kgdyFormulas, Comment = formula.kgdyRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.UPS, FormulaText = formula.upsFormulas, Comment = formula.upsRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.QT, FormulaText = formula.qtFormulas, Comment = formula.qtRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.ZL, FormulaText = formula.zlFormulas, Comment = formula.zlRemarks, CreatedTime = DateTime.Now });
                formulas.Add(new Formula { Id = id, Type = nodeType, FormulaType = EnmFormula.PUE, FormulaText = formula.pueFormulas, Comment = formula.pueRemarks, CreatedTime = DateTime.Now });
                
                _formulaService.SaveRange(formulas);
                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        /**
         *TODO:判断变量中设备和信号的有效性
         *在保存公式之前，需要判断每个公式中变量里的设备、信号是否存在
         */
        [AjaxAuthorize]
        public JsonResult PasteFormula(string source, string target) {
            try {
                var srcKeys = Common.SplitKeys(source);
                if(srcKeys.Length != 2)
                    throw new ArgumentException("参数无效 source");

                var tgtKeys = Common.SplitKeys(target);
                if(tgtKeys.Length != 2)
                    throw new ArgumentException("参数无效 target");

                var srcType = int.Parse(srcKeys[0]);
                var srcId = srcKeys[1];
                var srcEnmType = Enum.IsDefined(typeof(EnmOrganization), srcType) ? (EnmOrganization)srcType : EnmOrganization.Area;
                if(srcEnmType != EnmOrganization.Station && srcEnmType != EnmOrganization.Room)
                    throw new ArgumentException("能耗对象仅支持站点、机房");

                var tgtType = int.Parse(tgtKeys[0]);
                var tgtId = tgtKeys[1];
                var tgtEnmType = Enum.IsDefined(typeof(EnmOrganization), tgtType) ? (EnmOrganization)tgtType : EnmOrganization.Area;
                if(tgtEnmType != EnmOrganization.Station && tgtEnmType != EnmOrganization.Room)
                    throw new ArgumentException("能耗对象仅支持站点、机房");

                if(srcEnmType != tgtEnmType)
                    throw new ArgumentException("能耗对象类型不匹配");

                var formulas = _formulaService.GetFormulasAsList(srcId, srcEnmType);
                if(formulas.Count == 0)
                    throw new ArgumentException("复制的能耗对象未配置公式");

                for(var i = 0; i < formulas.Count; i++) {
                    formulas[i].Id = tgtId;
                }
                
                _formulaService.SaveRange(formulas);
                return Json(new AjaxResultModel { success = true, code = 200, message = "粘贴成功" });
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonNetResult GetFormulaDevices(string node, string[] devTypes) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(node)) {
                    var keys = Common.SplitKeys(node);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                        if(nodeType == EnmOrganization.Area) {
                            #region area organization
                            var current = _workContext.RoleAreas.Find(a => a.Current.Id == id);
                            if(current != null) {
                                if(current.HasChildren) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.ChildRoot.Count;
                                    for(var i = 0; i < current.ChildRoot.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmOrganization.Area, current.ChildRoot[i].Current.Id),
                                            text = current.ChildRoot[i].Current.Name,
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = false
                                        };

                                        data.data.Add(root);
                                    }
                                } else {
                                    if(current.Stations.Count > 0) {
                                        data.success = true;
                                        data.message = "200 Ok";
                                        data.total = current.Stations.Count;
                                        for(var i = 0; i < current.Stations.Count; i++) {
                                            var root = new TreeModel {
                                                id = Common.JoinKeys((int)EnmOrganization.Station, current.Stations[i].Current.Id),
                                                text = current.Stations[i].Current.Name,
                                                icon = Icons.Juzhan,
                                                expanded = false,
                                                leaf = false
                                            };

                                            data.data.Add(root);
                                        }
                                    }
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Station) {
                            #region station organization
                            var current = _workContext.RoleStations.Find(s => s.Current.Id == id);
                            if(current != null) {
                                if(current.Rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Rooms.Count;
                                    for(var i = 0; i < current.Rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmOrganization.Room, current.Rooms[i].Current.Id),
                                            text = current.Rooms[i].Current.Name,
                                            icon = Icons.Room,
                                            expanded = false,
                                            leaf = false
                                        };

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        } else if(nodeType == EnmOrganization.Room) {
                            #region room organization
                            var current = _workContext.RoleRooms.Find(d => d.Current.Id == id);
                            if(current != null) {
                                if(current.Devices.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.Devices.Count;
                                    for(var i = 0; i < current.Devices.Count; i++) {
                                        if(devTypes != null && devTypes.Length > 0 && !devTypes.Contains(current.Devices[i].Current.Type.Id))
                                            continue;

                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmOrganization.Device, current.Devices[i].Current.Id),
                                            text = current.Devices[i].Current.Name,
                                            icon = Icons.Device,
                                            expanded = false,
                                            leaf = true
                                        };

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            } catch(Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Include }
            };
        }

        [AjaxAuthorize]
        public JsonResult GetFormulaPoints(int start, int limit, string parent) {
            var data = new AjaxDataModel<List<IdValuePair<int,string>>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<IdValuePair<int, string>>()
            };

            try {
                if(!string.IsNullOrWhiteSpace(parent)) {
                    var keys = Common.SplitKeys(parent);
                    if(keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmOrganization), type) ? (EnmOrganization)type : EnmOrganization.Area;
                        if(nodeType == EnmOrganization.Device) {
                            var current = _workContext.RoleDevices.Find(d => d.Current.Id == id);
                            if(current != null && current.Protocol != null) {
                                var points = current.Protocol.Points.FindAll(p => p.Type == EnmPoint.AI);
                                for(var i = 0; i < points.Count; i++) {
                                    data.data.Add(new IdValuePair<int, string> {
                                        Id = i + 1,
                                        Value = string.Format("@{0}>>{1}", current.Current.Name, points[i].Name)
                                    });
                                }
                            }
                        }
                    }
                }
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        public JsonResult ClearCache() {
            try {
                this.ClearGlobalCache();
                this.ClearUserCache();
                return Json(new AjaxResultModel { success = true, code = 200, message = "所有缓存清除成功" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearGlobalCache() {
            try {
                //_cacheManager.RemoveByPattern(@"ipems:global:.+");
                _cacheManager.Clear();
                return Json(new AjaxResultModel { success = true, code = 200, message = "全局缓存清除成功" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearUserCache() {
            try {
                _workContext.Store.Role = null;
                _workContext.Store.User = null;
                _workContext.Store.Employee = null;
                _workContext.Store.Profile = null;
                return Json(new AjaxResultModel { success = true, code = 200, message = "用户缓存清除成功" }, JsonRequestBehavior.AllowGet);
            } catch(Exception exc) {
                _webLogger.Error(EnmEventType.Exception, exc.Message, exc, _workContext.User.Id);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}