﻿using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Common;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Data.Installation;
using iPem.Services.Common;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace iPem.Site.Controllers {
    public class AccountController : JsonNetController {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IDictionaryService _dictionaryService;
        private readonly IEmployeeService _employeeService;
        private readonly IEntitiesInRoleService _entitiesInRoleService;
        private readonly IFormulaService _formulaService;
        private readonly IFsuService _fsuService;
        private readonly ISignalService _signalService;
        private readonly IMenuService _menuService;
        private readonly INoticeService _noticeService;
        private readonly INoticeInUserService _noticeInUserService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly INoteService _noteService;
        private readonly IScExecutor _scExecutor;
        private readonly ICsExecutor _csExecutor;
        private readonly IRsExecutor _rsExecutor;
        private readonly IHDBScriptService _hdbScriptService;
        private readonly IRDBScriptService _rdbScriptService;
        private readonly ISDBScriptService _sdbScriptService;
        private const string _captchaSalt = "w9hRaAIX+tRJ4GD4wnVkVQ==";

        #endregion

        #region Ctor

        public AccountController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IDictionaryService dictionaryService,
            IEmployeeService employeeService,
            IEntitiesInRoleService entitiesInRoleService,
            IFormulaService formulaService,
            IFsuService fsuService,
            ISignalService signalService,
            IMenuService menuService,
            INoticeService noticeService,
            INoticeInUserService noticeInUserService,
            IRoleService roleService,
            IUserService userService,
            INoteService noteService,
            IScExecutor scExecutor,
            ICsExecutor csExecutor,
            IRsExecutor rsExecutor,
            IHDBScriptService hdbScriptService,
            IRDBScriptService rdbScriptService,
            ISDBScriptService sdbScriptService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._dictionaryService = dictionaryService;
            this._employeeService = employeeService;
            this._entitiesInRoleService = entitiesInRoleService;
            this._formulaService = formulaService;
            this._fsuService = fsuService;
            this._signalService = signalService;
            this._menuService = menuService;
            this._noticeService = noticeService;
            this._noticeInUserService = noticeInUserService;
            this._roleService = roleService;
            this._userService = userService;
            this._noteService = noteService;
            this._scExecutor = scExecutor;
            this._csExecutor = csExecutor;
            this._rsExecutor = rsExecutor;
            this._hdbScriptService = hdbScriptService;
            this._rdbScriptService = rdbScriptService;
            this._sdbScriptService = sdbScriptService;
        }

        #endregion

        #region Actions

        public ActionResult Login(string returnUrl) {
            Session.RemoveAll();
            Response.Cookies.Clear();
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string uid, string pwd, string captcha, string returnUrl) {
            try {
                if (string.IsNullOrWhiteSpace(uid))
                    ModelState.AddModelError("", "用户名不能为空。");

                uid = uid.Trim(); ViewBag.Uid = uid;

                if (ModelState.IsValid && string.IsNullOrWhiteSpace(pwd))
                    ModelState.AddModelError("", "密码不能为空。");

                if (ModelState.IsValid && string.IsNullOrWhiteSpace(captcha))
                    ModelState.AddModelError("", "验证码不能为空。");

                var captchaCookie = Request.Cookies[Common.CaptchaId];
                if (ModelState.IsValid && captchaCookie == null)
                    ModelState.AddModelError("", "您的浏览器禁用了JavaScript，启用后才能使用本系统。");

                if (ModelState.IsValid) {
                    captcha = CommonHelper.CreateHash(captcha.ToLowerInvariant().Trim(), _captchaSalt);
                    if (captcha != captchaCookie.Value) ModelState.AddModelError("", "验证码错误。");
                }

                if (ModelState.IsValid) {
                    var loginResult = _userService.Validate(uid, pwd);
                    switch (loginResult) {
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

                if (ModelState.IsValid) {
                    var _userEntity = _userService.GetUserByName(uid);
                    var loginResult = _roleService.Validate(_userEntity.RoleId);
                    switch (loginResult) {
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

                if (ModelState.IsValid) {
                    var store = Store.CreateInstance();
                    var ticket = new FormsAuthenticationTicket(
                        1,
                        uid,
                        DateTime.Now.ToLocalTime(),
                        DateTime.Now.ToLocalTime().Add(FormsAuthentication.Timeout),
                        false,
                        store.Id.ToString(),
                        FormsAuthentication.FormsCookiePath);

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    authCookie.HttpOnly = true;
                    authCookie.Path = FormsAuthentication.FormsCookiePath;
                    if (ticket.IsPersistent)
                        authCookie.Expires = ticket.Expiration;

                    if (FormsAuthentication.CookieDomain != null)
                        authCookie.Domain = FormsAuthentication.CookieDomain;

                    Response.Cookies.Add(authCookie);
                    _webLogger.Information(EnmEventType.Login, string.Format("{0} 登录系统", uid), null);

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToRoute("HomePage");
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, null, exc);
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
                if (hc != null) {
                    hc.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(hc);
                }

                var captcha = CommonHelper.CreateHash(code, _captchaSalt);
                var cookie = new HttpCookie(Common.CaptchaId, captcha);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return File(image, @"image/png");
            } catch (Exception exc) {
                return Content(exc.Message);
            }
        }

        [Authorize]
        public ActionResult GetUserPhoto() {
            var employee = _workContext.Employee();
            if (employee != null && employee.Photo != null)
                return File(employee.Photo, @"image/png");

            if (employee != null && employee.Sex == EnmSex.Female) {
                var female = Convert.FromBase64String(@"iVBORw0KGgoAAAANSUhEUgAAABwAAAAcCAIAAAD9b0jDAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MTFEOEQ4NTY5MjdCMTFFNThCQ0RDMzY1NUJFQjdBQTkiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MTFEOEQ4NTc5MjdCMTFFNThCQ0RDMzY1NUJFQjdBQTkiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDoxMUQ4RDg1NDkyN0IxMUU1OEJDREMzNjU1QkVCN0FBOSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDoxMUQ4RDg1NTkyN0IxMUU1OEJDREMzNjU1QkVCN0FBOSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PuDyG6MAAAQISURBVHjanFRJbBtlFJ75x+OZ8TjjLa7rxCUloUXETZQuWYRKxRIVxFYVRA8cuVZCVEhFHLj0CKpYhMQBTlWvLKJSJUgjSkMR2ZoqaWPHtHZSY9fx7vFsnhnPz+8FN1i202Q0h/+993+f3nvf+x8uZmNYx68sxozcHaOUwoDZxB0knEMYTnSGmDrE9PScsvZduRDY6gSMlx58n/Qc7wDE22VaundJCX3bDsYMnTfve2NnpFp8Srp9oVMRGonRT3UNv2f2TjweqS7y189AlW/b5RIm5WAVjbme/5h58rWmC6BF4dGrWxlNnJ/qOU153ySsB5AJDUwuwHoMYvm/vngsofSH040z1XOKtB+rnUnHqM7fFe5fMVtJo6zqkgghVpZlQ/gHWH2dSGFZ0QvBesw20mBsZG0/7K+dlcRsPnAF8arZVfr/pM3lG8IGIq60i2Cpva900IreO05y7gpEzm5TvlFKVxUgKN87uc1iSU4ig7LQDq8HGvBheF0WRKQP53Ls8flM3CGV/03NPLB0JtWSdyCG49SwiR1wWKAglUS5RJsBZsD4vYjFZgU2ZxdLswxVFY3WSxAz0dupT+0RU4YulSrp4vjP15e+vDxl4bpwACSh6PR4Ll765erMMgqhC6qYk3OQsI9spz7pQC3VxM2a9e6rE/C/+cGxCtGn585UCSufmt9s2W7Q+sFIGUOTa8kCUOfACaBrGjJraWIQKm2eeGtSBFDzkSafd3+fIkoNEzEamrITUoRJBZo8rI2z2m0NU4wFdrz6lNRdQ38dmCoqFzJ8IlYZNZfH3u1xViuBfORWO2zbTGFZTa+v1Bedlcml8rl0wWpja57MelCTCrtZ0mpiUe4dYhjKTJETLx155Fe1bPDGLje/mVD4vIAoOI6tKY6mSxAkQRBNuqjvjhSnHQzLSJLMFwWKMhM4rigaIHAry2oWuy6kd0aq0f0Wzk31HkcJsqyF0suqphkQWliGNFUg9sOn+LU/hGwS8JFOm59PJUNzc6H5+Vgk+cmPP6zeuOYEUbzxeprnGObgE4MnJi+cftvX5z4wPvr02FhXt7ue6fL0VGhxObwaTicebbC/F28feuHl1d+v2Y11HDTzGhDjQb//uRdDi0upRBb9S7NrGHbZ7XX2+wcOHhkh6I1sLBKXBHkrTBaKx05Ouvf35yVKSMcpEjZCvAhI37MDR0fR+fvPv97ceNAIIZJYOL5881brnq7M/BmYnX9mfHTfoL/o7fnp4mdlKYNqBmz3Wx+etzrs6E5wbmFl5mZLODHW29cysDa3cPTkJErkq7Pn7q8E49F0PJqJhaMLv04PjAyjVn/zwUeKJLUem7NjJ9pNhqvHy6czmqo2b0eKsnW70rF4O+C/AgwABxjY7MvCiokAAAAASUVORK5CYII=");
                return File(female, @"image/png");
            }

            var male = Convert.FromBase64String(@"iVBORw0KGgoAAAANSUhEUgAAABwAAAAcCAIAAAD9b0jDAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MDhDNDdFNEQ5MjdCMTFFNTgwNkM5MDlCQUE5OEEyNkYiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MDhDNDdFNEU5MjdCMTFFNTgwNkM5MDlCQUE5OEEyNkYiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDowOEM0N0U0QjkyN0IxMUU1ODA2QzkwOUJBQTk4QTI2RiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDowOEM0N0U0QzkyN0IxMUU1ODA2QzkwOUJBQTk4QTI2RiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PuuUA5kAAAPCSURBVHjarFbdbxRVFL93Pnd2Z3bodtrurmUL/ZBCC0aqtqWgAXmAhBAeNDGx8mBIiD6gf4BP+PEiEH3ynRC0MSFBI2iIbEtti0p3t7RAwKK2ARbW/f7o7p2dmeudThCKnXGXeHMnOXPuzO/c8zu/c2eYUvou+L8H47CGq8Xy9c8odxAyEoAY6CrFK7TSR7manhoU69k5LR7GhrrCDWl+3Wuu7ncBpOoDxdV8KfKBnoquGgv9MQIYl6vrkB0oXJXT4i/v6cmIfRJUFXkxH/K+8A7f3LvK8r9dWvLXfxAh42GkbsjKj63T5YxeyWTQ/Znk+SNabqGm9LXEpGWwDf2cfw+EDDBUlPhRy88CKAC2Q1TaDLVYWriko1xp/nu57/B/gxqVhLkgdfOBfQ/z4Xj/XjJX5Mi5s3MjeiFeU/oQsuY2fYPOuuHktmUApjZQzzOmcIRWZ1CalyBFMVKwJk5pbydkRJJeIZ0pZXMkAMuxvqAfA3Dv9u8qQoaB1yiNSjAAaDfr66oJlGkeYpRRDDAwsKelpVRWPS66itCDxTsN/uaCiiUPr+YLGGOhtd/VOlBb+qQs7W9AACXF981o7PNTF1yiyAkCQkiU5eMnfzg3PuvzN0MIpefehDRbq/iBoeuL42b7mANQFCT2/NW5zi29JHdIApoXRYe2r9qsNv1L0VZZycsWosk1bT5Mbk1E02Lt2t/2UID0k3Sv79m0sqCsbRc7SMZZUpDm6j76ICfiCtETyKXy0alrhF2rMbYObfY2iMvqF+sHlQI4f5eQJzd6e/o2xBcThMC164MWIolAS/76D2lWzBVV2cOSsjQFfGQ+OlExzi1pjYy7fk7JZt3K/XhSVauPO8ltPJ6EbqcvCnT48GHajTJ3ErNhQWB43iwLqqClitayZTcnt1AGqmOnpHN+Cl/8+OiH6ULZ19a7btfbGq+kUlkydVdT+6uHfKFNyVz5k6MfTYyGVaTa7pTQNH/r5lg4PHE5Ertxu4I04jz91ZdDgy8ur4KTY9eJ6odf3mgJf2x86uDwW8QQePb5nvZt/X2v7NzZ0fWs1RfMd2fPXBq//HP0xoNU7omA0WjMAp24p33651pidHTp24I0Maanp61nyqg6GblJ5rEvTvsVeWBrz47tgzAU6rCj5qWBga9HTmXypfcvliZTAvHsaFw6sUtc4/XsP/D6TDT6ND8TkStXEsnsrYX4wSDOp72EhuFA4dpv6c6Q/2os5vCik6Q0TTt77oJhGH8VwExsaSZWTheBbhhnvj2PrQazGX8LMAA0pn/L4p+vyAAAAABJRU5ErkJggg==");
            return File(male, @"image/png");
        }

        /**
         *TODO:跳转权限判断
         *在跳转时需要判断一下该角色下是否拥有这个菜单权限，每个Action跳转时都需要判断
         */

        [Authorize]
        public ActionResult Roles() {
            if (!_workContext.Authorizations().Menus.Contains(1001))
                throw new HttpException(404, "Page not found.");

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
                    _roleService.GetPagedRoles(start / limit, limit) :
                    _roleService.GetPagedRoleByNames(names, start / limit, limit);

                if (roles.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = roles.TotalCount;
                    for (var i = 0; i < roles.Count; i++) {
                        data.data.Add(new RoleModel {
                            index = start + i + 1,
                            id = roles[i].Id,
                            name = roles[i].Name,
                            type = roles[i].Type,
                            typeName = Common.GetSSHDisplay(roles[i].Type),
                            comment = roles[i].Comment,
                            enabled = roles[i].Enabled
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, _workContext.User().Id, exc.Message, exc);
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
                data = new RoleModel { index = 1, id = Guid.NewGuid().ToString(), name = "", comment = "", enabled = true, menus = new string[] { }, permissions = new string[] { }, authorizations = new string[] { } }

            };

            try {
                if (action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                var role = _roleService.GetRoleById(id);
                if (role == null) throw new iPemException("未找到数据对象");

                var values = new RoleModel() { };
                if (!string.IsNullOrWhiteSpace(role.ValuesJson))
                    values = JsonConvert.DeserializeObject<RoleModel>(role.ValuesJson);

                var auth = _entitiesInRoleService.GetEntitiesInRole(role.Id);
                data.data.id = role.Id;
                data.data.name = role.Name;
                data.data.type = role.Type;
                data.data.comment = role.Comment;
                data.data.enabled = role.Enabled;
                data.data.menus = auth.Menus.Select(m => m.ToString()).ToArray();
                data.data.permissions = auth.Permissions.Select(o => ((int)o).ToString()).ToArray();
                data.data.authorizations = auth.Authorizations.Select(a => Common.JoinKeys((int)role.Type, a)).ToArray();
                data.data.sms = (role.Config & (int)EnmRoleConfig.SMS) > 0 ? true : false;
                data.data.voice = (role.Config & (int)EnmRoleConfig.Voice) > 0 ? true : false;
                data.data.SMSLevels = values.SMSLevels;
                data.data.SMSDevices = values.SMSDevices;
                data.data.SMSSignals = values.SMSSignals;
                data.data.voiceLevels = values.voiceLevels;
                data.data.voiceDevices = values.voiceDevices;
                data.data.voiceSignals = values.voiceSignals;
                return Json(data, JsonRequestBehavior.AllowGet);

            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, _workContext.User().Id, exc.Message, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult GetAllMenus() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var menus = _menuService.GetMenus();
                if (menus.Count > 0) {
                    var roots = new List<U_Menu>();
                    foreach (var menu in menus) {
                        if (!menus.Any(m => m.Id == menu.LastId))
                            roots.Add(menu);
                    }

                    var _menus = roots.OrderBy(m => m.Index).ToList();
                    if (_menus.Count > 0) {
                        data.success = true;
                        data.message = "200 Ok";
                        data.total = menus.Count;
                        for (var i = 0; i < _menus.Count; i++) {
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
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        private void MenusRecursion(List<U_Menu> menus, int pid, TreeModel node) {
            var _menus = menus.FindAll(m => m.LastId == pid).OrderBy(m => m.Index).ToList();
            if (_menus.Count > 0) {
                node.data = new List<TreeModel>();
                for (var i = 0; i < _menus.Count; i++) {
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
        public JsonResult GetAllPermissions() {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                var result = Enum.GetValues(typeof(EnmPermission));
                if (result.Length > 0) {
                    data.message = "200 Ok";
                    data.total = result.Length;
                    foreach (EnmPermission op in result) {
                        var root = new TreeModel {
                            id = ((int)op).ToString(),
                            text = Common.GetPermissionDisplay(op),
                            selected = false,
                            icon = Icons.Junheng,
                            leaf = true
                        };

                        data.data.Add(root);
                    }
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        [AjaxAuthorize]
        public JsonResult GetAllAuthorizations(EnmSSH roleType, string node, bool? multiselect, bool? leafselect) {
            var data = new AjaxDataModel<List<TreeModel>> {
                success = false,
                message = "无数据",
                total = 0,
                data = new List<TreeModel>()
            };

            try {
                if (node == "root") {
                    #region root organization
                    var areas = _workContext.AllAreas();
                    if (areas.Count > 0) {
                        var roots = areas.FindAll(m => !m.HasParents);
                        if (roots.Count > 0) {
                            data.success = true;
                            data.message = "200 Ok";
                            data.total = areas.Count;
                            for (var i = 0; i < roots.Count; i++) {
                                var current = roots[i];
                                var root = new TreeModel {
                                    id = Common.JoinKeys((int)EnmSSH.Area, current.Current.Id),
                                    text = current.Current.Name,
                                    selected = false,
                                    icon = Icons.Diqiu,
                                    expanded = false,
                                    leaf = current.HasChildren ? false : true
                                };

                                if (multiselect.HasValue && multiselect.Value) {
                                    if (!leafselect.HasValue || !leafselect.Value)
                                        root.selected = false;
                                }

                                data.data.Add(root);
                            }
                        }
                    }
                    #endregion
                } else if (!string.IsNullOrWhiteSpace(node)) {
                    var keys = Common.SplitKeys(node);
                    if (keys.Length == 2) {
                        var type = int.Parse(keys[0]);
                        var id = keys[1];
                        var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                        if (nodeType == EnmSSH.Area) {
                            #region area organization
                            var current = _workContext.AllAreas().Find(a => a.Current.Id == id);
                            if (current != null) {
                                if (current.HasChildren) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = current.ChildRoot.Count;
                                    for (var i = 0; i < current.ChildRoot.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Area, current.ChildRoot[i].Current.Id),
                                            text = current.ChildRoot[i].Current.Name,
                                            icon = Icons.Diqiu,
                                            expanded = false,
                                            leaf = current.ChildRoot[i].HasChildren ? false : true
                                        };

                                        if (roleType != EnmSSH.Area)
                                            root.leaf = false;

                                        if (multiselect.HasValue && multiselect.Value) {
                                            if (!leafselect.HasValue || !leafselect.Value)
                                                root.selected = false;
                                        }

                                        data.data.Add(root);
                                    }
                                } else {
                                    if (roleType != EnmSSH.Area) {
                                        var stations = _workContext.AllStations().FindAll(s => s.Current.AreaId == id);
                                        if (stations != null && stations.Count > 0) {
                                            data.success = true;
                                            data.message = "200 Ok";
                                            data.total = stations.Count;
                                            for (var i = 0; i < stations.Count; i++) {
                                                var root = new TreeModel {
                                                    id = Common.JoinKeys((int)EnmSSH.Station, stations[i].Current.Id),
                                                    text = stations[i].Current.Name,
                                                    icon = Icons.Juzhan,
                                                    expanded = false,
                                                    leaf = roleType == EnmSSH.Station ? true : false
                                                };

                                                if (multiselect.HasValue && multiselect.Value) {
                                                    if (!leafselect.HasValue || !leafselect.Value)
                                                        root.selected = false;
                                                }

                                                data.data.Add(root);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        } else if (nodeType == EnmSSH.Station) {
                            #region station organization
                            if (roleType != EnmSSH.Station) {
                                var rooms = _workContext.AllRooms().FindAll(r => r.Current.StationId == id);
                                if (rooms != null && rooms.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = rooms.Count;
                                    for (var i = 0; i < rooms.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Room, rooms[i].Current.Id),
                                            text = rooms[i].Current.Name,
                                            icon = Icons.Room,
                                            expanded = false,
                                            leaf = roleType == EnmSSH.Room ? true : false
                                        };

                                        if (multiselect.HasValue && multiselect.Value)
                                            root.selected = false;

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        } else if (nodeType == EnmSSH.Room) {
                            #region room organization
                            if (roleType != EnmSSH.Room) {
                                var devices = _workContext.AllDevices().FindAll(d => d.Current.RoomId == id);
                                if (devices != null && devices.Count > 0) {
                                    data.success = true;
                                    data.message = "200 Ok";
                                    data.total = devices.Count;
                                    for (var i = 0; i < devices.Count; i++) {
                                        var root = new TreeModel {
                                            id = Common.JoinKeys((int)EnmSSH.Device, devices[i].Current.Id),
                                            text = devices[i].Current.Name,
                                            icon = Icons.Device,
                                            expanded = false,
                                            leaf = roleType == EnmSSH.Device ? true : false
                                        };

                                        if (multiselect.HasValue && multiselect.Value)
                                            root.selected = false;

                                        data.data.Add(root);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }

            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return new JsonNetResult {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveRole(RoleModel role, RoleValues values, int action) {
            try {

                #region 异常操作处理
                if (role == null) throw new ArgumentException("role");

                if (role.menus == null || role.menus.Length == 0)
                    throw new iPemException("尚未选择菜单权限");

                if (role.authorizations == null || role.authorizations.Length == 0) {
                    if (EnmSSH.Area == role.type)
                        throw new iPemException("尚未选择区域权限");
                    if (EnmSSH.Station == role.type)
                        throw new iPemException("尚未选择站点权限");
                    if (EnmSSH.Room == role.type)
                        throw new iPemException("尚未选择机房权限");
                    if (EnmSSH.Device == role.type)
                        throw new iPemException("尚未选择设备权限");
                }
                #endregion

                var auth = new U_EntitiesInRole {
                    RoleId = role.id,
                    Type = role.type,
                    Menus = role.menus.Select(i => int.Parse(i)).ToList(),
                    Permissions = role.permissions != null && role.permissions.Length > 0 ? role.permissions.Select(o => (EnmPermission)(int.Parse(o))).ToList() : new List<EnmPermission>()
                };

                #region Authorizations 的处理
                HashSet<string> authorizations = new HashSet<string>();

                var allAreas = _workContext.AllAreas();
                var allStations = _workContext.AllStations();
                var allRooms = _workContext.AllRooms();
                var allDevices = _workContext.AllDevices();

                var nodes = role.authorizations.ToList();
                foreach (var node in nodes) {
                    var keys = Common.SplitKeys(node);
                    if (keys.Length == 2) {
                        var nodeType = int.Parse(keys[0]);
                        var nodeId = keys[1];
                        if (role.type == EnmSSH.Area) {
                            #region 区域类型角色
                            var current = allAreas.Find(a => a.Current.Id == nodeId);
                            if (current != null) {
                                if (current.HasChildren && current.Children.Count > 0) {
                                    var areaIds = current.Children.FindAll(c => !c.HasChildren).Select(a => a.Current.Id);
                                    if (areaIds != null) {
                                        foreach (var areaId in areaIds) {
                                            authorizations.Add(areaId);
                                        }
                                    }
                                } else {
                                    authorizations.Add(nodeId);
                                }
                            }
                            #endregion
                        } else if (role.type == EnmSSH.Station) {
                            #region 站点类型角色
                            if (nodeType == (int)EnmSSH.Area) {
                                #region 选择了区域
                                var current = allAreas.Find(a => a.Current.Id == nodeId);
                                if (current != null) {
                                    if (current.HasChildren && current.Children.Count > 0) {
                                        var areaIds = current.Children.FindAll(c => !c.HasChildren).Select(a => a.Current.Id);
                                        var stations = allStations.FindAll(s => areaIds.Contains(s.Current.AreaId));
                                        if (stations != null) {
                                            foreach (var station in stations) {
                                                authorizations.Add(station.Current.Id);
                                            }
                                        }
                                    } else {
                                        var stations = allStations.FindAll(s => s.Current.AreaId == nodeId);
                                        if (stations != null) {
                                            foreach (var station in stations) {
                                                authorizations.Add(station.Current.Id);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            } else if (nodeType == (int)EnmSSH.Station) {
                                #region 选择了站点
                                authorizations.Add(nodeId);
                                #endregion
                            }
                            #endregion
                        } else if (role.type == EnmSSH.Room) {
                            #region 机房类型角色
                            if (nodeType == (int)EnmSSH.Area) {
                                #region 选择了区域
                                var current = allAreas.Find(a => a.Current.Id == nodeId);
                                if (current != null) {
                                    if (current.HasChildren && current.Children.Count > 0) {
                                        var areaIds = current.Children.FindAll(c => !c.HasChildren).Select(a => a.Current.Id);
                                        var rooms = allRooms.FindAll(r => areaIds.Contains(r.Current.AreaId));
                                        if (rooms != null) {
                                            foreach (var room in rooms) {
                                                authorizations.Add(room.Current.Id);
                                            }
                                        }
                                    } else {
                                        var rooms = allRooms.FindAll(r => r.Current.AreaId == nodeId);
                                        if (rooms != null) {
                                            foreach (var room in rooms) {
                                                authorizations.Add(room.Current.Id);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            } else if (nodeType == (int)EnmSSH.Station) {
                                #region 选择了站点
                                var rooms = allRooms.FindAll(r => r.Current.StationId == nodeId);
                                if (rooms != null) {
                                    foreach (var room in rooms) {
                                        authorizations.Add(room.Current.Id);
                                    }
                                }
                                #endregion
                            } else if (nodeType == (int)EnmSSH.Room) {
                                #region 选择了机房
                                authorizations.Add(nodeId);
                                #endregion
                            }
                            #endregion
                        } else if (role.type == EnmSSH.Device) {
                            #region 设备类型角色
                            if (nodeType == (int)EnmSSH.Area) {
                                #region 选择了区域
                                var current = allAreas.Find(a => a.Current.Id == nodeId);
                                if (current != null) {
                                    if (current.HasChildren && current.Children.Count > 0) {
                                        var areaIds = current.Children.FindAll(c => !c.HasChildren).Select(a => a.Current.Id);
                                        var devices = allDevices.FindAll(d => areaIds.Contains(d.Current.AreaId));
                                        if (devices != null) {
                                            foreach (var device in devices) {
                                                authorizations.Add(device.Current.Id);
                                            }
                                        }
                                    } else {
                                        var devices = allDevices.FindAll(d => d.Current.AreaId == nodeId);
                                        if (devices != null) {
                                            foreach (var device in devices) {
                                                authorizations.Add(device.Current.Id);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            } else if (nodeType == (int)EnmSSH.Station) {
                                #region 选择了站点
                                var devices = allDevices.FindAll(d => d.Current.StationId == nodeId);
                                if (devices != null) {
                                    foreach (var device in devices) {
                                        authorizations.Add(device.Current.Id);
                                    }
                                }
                                #endregion
                            } else if (nodeType == (int)EnmSSH.Room) {
                                #region 选择了机房
                                var devices = allDevices.FindAll(d => d.Current.RoomId == nodeId);
                                if (devices != null) {
                                    foreach (var device in devices) {
                                        authorizations.Add(device.Current.Id);
                                    }
                                }
                                #endregion
                            } else if (nodeType == (int)EnmSSH.Device) {
                                #region 选择了设备
                                authorizations.Add(nodeId);
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
                #endregion

                auth.Authorizations = authorizations.ToList();

                var sms = role.sms == true ? (int)EnmRoleConfig.SMS : 0;
                var voice = role.voice == true ? (int)EnmRoleConfig.Voice : 0;

                if (action == (int)EnmAction.Add) {
                    #region 新增角色操作
                    var existed = _roleService.GetRoleById(role.id);
                    //连续新增角色时更换角色编号
                    if (existed != null) role.id = Guid.NewGuid().ToString();
                    auth.RoleId = role.id;

                    existed = _roleService.GetRoleByName(role.name);
                    if (existed != null) throw new iPemException("角色已存在");

                    var newRole = new U_Role {
                        Id = role.id,
                        Name = role.name,
                        Type = role.type,
                        Comment = role.comment,
                        Enabled = role.enabled,
                        Config = sms + voice,
                        ValuesJson = JsonConvert.SerializeObject(values)
                    };

                    _roleService.Add(newRole);
                    _entitiesInRoleService.Add(auth);
                    _webLogger.Information(EnmEventType.Other, string.Format("新增角色[{0}]", newRole.Name), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "角色保存成功" });
                    #endregion
                } else if (action == (int)EnmAction.Edit) {
                    #region 编辑角色操作
                    var existed = _roleService.GetRoleByName(role.name);
                    if (existed == null) throw new iPemException("角色不存在");

                    existed.Name = role.name;
                    existed.Type = role.type;
                    existed.Comment = role.comment;
                    existed.Enabled = role.enabled;
                    existed.Config = sms + voice;
                    existed.ValuesJson = JsonConvert.SerializeObject(values);

                    _roleService.Update(existed);
                    _entitiesInRoleService.Remove(existed.Id);
                    _entitiesInRoleService.Add(auth);
                    _webLogger.Information(EnmEventType.Other, string.Format("更新角色[{0}]", existed.Name), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "角色保存成功" });
                    #endregion
                }

                throw new ArgumentException("action");
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteRole(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var existed = _roleService.GetRoleById(id);
                if (existed == null) throw new iPemException("角色不存在");

                var usersInRole = _userService.GetUsers(existed.Id, false, 0, 5);
                if (usersInRole.TotalCount > 0) throw new iPemException("存在隶属该角色的用户，禁止删除");

                _entitiesInRoleService.Remove(existed.Id);
                _roleService.Remove(existed);
                _webLogger.Information(EnmEventType.Other, string.Format("删除角色[{0}]", existed.Name), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "角色删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadRoles(string condition) {
            try {
                var names = Common.SplitCondition(condition);
                var roles = names.Length == 0 ?
                    _roleService.GetRoles() :
                    _roleService.GetRoleByNames(names);

                var models = new List<RoleModel>();
                for (var i = 0; i < roles.Count; i++) {
                    models.Add(new RoleModel {
                        index = i + 1,
                        id = roles[i].Id,
                        name = roles[i].Name,
                        typeName = Common.GetSSHDisplay(roles[i].Type),
                        comment = roles[i].Comment,
                        enabled = true,
                        sms = (roles[i].Config & (int)EnmRoleConfig.SMS) > 0 ? true : false,
                        voice = (roles[i].Config & (int)EnmRoleConfig.Voice) > 0 ? true : false
                    });
                }

                using (var ms = _excelManager.Export<RoleModel>(models, "角色信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [Authorize]
        public ActionResult Users() {
            if (!_workContext.Authorizations().Menus.Contains(1002))
                throw new HttpException(404, "Page not found.");

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
                var users = _userService.GetUsersInRole(_workContext.Role().Id);
                if (roles != null && roles.Length > 0)
                    users = users.FindAll(u => roles.Contains(u.RoleId.ToString()));

                if (names != null && names.Length > 0)
                    users = users.FindAll(u => CommonHelper.ConditionContain(u.Uid, names));

                if (users.Count > 0) {
                    var allRoles = _roleService.GetRolesByRole(_workContext.Role().Id);
                    var employees = _employeeService.GetEmployees();
                    var models = (from user in users
                                  join role in allRoles on user.RoleId equals role.Id into lt2
                                  from def2 in lt2.DefaultIfEmpty()
                                  join emp in employees on user.EmployeeId equals emp.Id into lt
                                  from def in lt.DefaultIfEmpty()
                                  select new UserModel {
                                      id = user.Id.ToString(),
                                      uid = user.Uid,
                                      roleId = def2 != null ? def2.Id : "",
                                      roleName = def2 != null ? def2.Name : "",
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

                    if (models.Count > 0) {
                        var result = new PagedList<UserModel>(models, start / limit, limit);
                        if (result.Count > 0) {
                            data.message = "200 Ok";
                            data.total = result.TotalCount;
                            for (var i = 0; i < result.Count; i++) {
                                result[i].index = start + 1 + i;
                                data.data.Add(result[i]);
                            }
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (action == (int)EnmAction.Add) {
                    data.data.roleId = _workContext.Role().Id;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                var user = _userService.GetUserById(id);
                if (user == null) throw new iPemException("未找到数据对象");

                data.data.id = user.Id.ToString();
                data.data.uid = user.Uid;
                data.data.roleId = user.RoleId;
                data.data.empId = user.EmployeeId;
                data.data.limited = CommonHelper.DateConverter(user.LimitedDate);
                data.data.isLockedOut = user.IsLockedOut;
                data.data.comment = user.Comment;
                data.data.enabled = user.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                var user = _workContext.User();
                var role = _workContext.Role();
                var employee = _workContext.Employee();

                data.data.id = user.Id.ToString();
                data.data.uid = user.Uid;
                data.data.roleId = role.Id;
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
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveUser(UserModel user, int action) {
            try {
                if (user == null) throw new ArgumentException("user");

                if (action == (int)EnmAction.Add) {
                    var existed = _userService.GetUserById(user.id);
                    if (existed != null) user.id = Guid.NewGuid().ToString();

                    existed = _userService.GetUserByName(user.uid);
                    if (existed != null) throw new iPemException("用户已存在");

                    var newUser = new U_User {
                        Id = user.id,
                        RoleId = user.roleId,
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
                    _webLogger.Information(EnmEventType.Other, string.Format("新增用户[{0}]", newUser.Uid), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                } else if (action == (int)EnmAction.Edit) {
                    var existed = _userService.GetUserByName(user.uid);
                    if (existed == null) throw new iPemException("用户不存在");

                    existed.RoleId = user.roleId;
                    existed.LimitedDate = DateTime.Parse(user.limited);
                    existed.FailedPasswordAttemptCount = 0;
                    existed.IsLockedOut = user.isLockedOut;
                    if (user.isLockedOut) existed.LastLockoutDate = DateTime.Now;
                    existed.Comment = user.comment;
                    existed.EmployeeId = user.empId;
                    existed.Enabled = user.enabled;

                    _userService.Update(existed);
                    _webLogger.Information(EnmEventType.Other, string.Format("更新用户[{0}]", existed.Uid), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
                }

                throw new ArgumentException("action");
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteUser(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                var existed = _userService.GetUserById(id);
                if (existed == null)
                    throw new iPemException("用户不存在");

                if (_workContext.User().Uid.Equals(existed.Uid, StringComparison.CurrentCultureIgnoreCase))
                    throw new iPemException("无法删除当前用户");

                _userService.Remove(existed);
                _webLogger.Information(EnmEventType.Other, string.Format("删除用户[{0}]", existed.Uid), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ResetPassword(string id, string password) {
            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("password");

                var existed = _userService.GetUserById(id);
                if (existed == null) throw new iPemException("用户不存在");

                _userService.ForcePassword(existed, password);
                _webLogger.Information(EnmEventType.Other, string.Format("重置用户密码[{0}]", existed.Uid), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "密码重置成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult ChangePassword(string id, string origin, string password) {
            try {
                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (string.IsNullOrWhiteSpace(origin))
                    throw new ArgumentException("origin");

                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("password");

                var existed = _userService.GetUserById(id);
                if (existed == null) throw new iPemException("用户不存在");

                var result = _userService.ChangePassword(existed, origin, password);
                if (result == EnmChangeResults.Successful) {
                    _webLogger.Information(EnmEventType.Other, string.Format("修改用户密码[{0}]", existed.Uid), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "密码修改成功" });
                } else if (result == EnmChangeResults.WrongPassword) {
                    throw new iPemException("原始密码错误");
                } else {
                    throw new iPemException("未知错误");
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadUsers(string[] roles, string[] names) {
            try {
                var users = _userService.GetUsersInRole(_workContext.Role().Id);
                if (roles != null && roles.Length > 0)
                    users = users.FindAll(u => roles.Contains(u.RoleId.ToString()));

                if (names != null && names.Length > 0)
                    users = users.FindAll(u => CommonHelper.ConditionContain(u.Uid, names));

                var models = new List<UserModel>();
                if (users.Count > 0) {
                    var allRoles = _roleService.GetRolesByRole(_workContext.Role().Id);
                    var employees = _employeeService.GetEmployees();
                    models = (from user in users
                              join role in allRoles on user.RoleId equals role.Id
                              join employee in employees on user.EmployeeId equals employee.Id into lt
                              from def in lt.DefaultIfEmpty()
                              select new UserModel {
                                  id = user.Id.ToString(),
                                  uid = user.Uid,
                                  roleId = role.Id,
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

                for (var i = 0; i < models.Count; i++)
                    models[i].index = i + 1;

                using (var ms = _excelManager.Export<UserModel>(models.ToList(), "用户信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                var models = _roleService.GetRolesByRole(_workContext.Role().Id).Select(r => new ComboItem<string, string> { id = r.Id, text = r.Name });
                var result = new PagedList<ComboItem<string, string>>(models, start / limit, limit, models.Count());
                if (result.Count > 0) {
                    data.message = "200 Ok";
                    data.total = result.TotalCount;
                    for (var i = 0; i < result.Count; i++) {
                        data.data.Add(result[i]);
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Events() {
            if (!_workContext.Authorizations().Menus.Contains(1003))
                throw new HttpException(404, "Page not found.");

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
                if (result.Length > 0) {
                    data.message = "200 Ok";
                    data.total = result.Length;
                    foreach (EnmEventLevel level in result) {
                        data.data.Add(new ComboItem<int, string>() {
                            id = (int)level,
                            text = Common.GetEventLevelDisplay(level)
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (result.Length > 0) {
                    data.message = "200 Ok";
                    data.total = result.Length;
                    foreach (EnmEventType type in result) {
                        data.data.Add(new ComboItem<int, string>() {
                            id = (int)type,
                            text = Common.GetEventTypeDisplay(type)
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (levels != null && levels.Length > 0) {
                    foreach (var level in levels) {
                        if (Enum.IsDefined(typeof(EnmEventLevel), level))
                            _levels.Add((EnmEventLevel)level);
                    }
                }

                var _types = new List<EnmEventType>();
                if (types != null && types.Length > 0) {
                    foreach (var type in types) {
                        if (Enum.IsDefined(typeof(EnmEventType), type))
                            _types.Add((EnmEventType)type);
                    }
                }

                var events = _webLogger.GetWebEvents(startDate, endDate, _levels.Count > 0 ? _levels.ToArray() : null, _types.Count > 0 ? _types.ToArray() : null, start / limit, limit);
                var users = _userService.GetPagedUsers().ToDictionary(k => k.Id, v => v.Uid);
                if (events.TotalCount > 0) {
                    data.message = "200 Ok";
                    data.total = events.TotalCount;
                    if (events.Count > 0) {
                        for (var i = 0; i < events.Count; i++) {
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
                                user = (!string.IsNullOrWhiteSpace(events[i].UserId)) && users.ContainsKey(events[i].UserId) ? users[events[i].UserId] : "",
                                created = CommonHelper.DateTimeConverter(events[i].CreatedTime)
                            });
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (levels != null && levels.Length > 0) {
                    foreach (var level in levels) {
                        if (Enum.IsDefined(typeof(EnmEventLevel), level))
                            _levels.Add((EnmEventLevel)level);
                    }
                }

                var _types = new List<EnmEventType>();
                if (types != null && types.Length > 0) {
                    foreach (var type in types) {
                        if (Enum.IsDefined(typeof(EnmEventType), type))
                            _types.Add((EnmEventType)type);
                    }
                }

                var events = _webLogger.GetWebEvents(startDate, endDate, _levels.Count > 0 ? _levels.ToArray() : null, _types.Count > 0 ? _types.ToArray() : null);
                if (events.Count == 0)
                    throw new iPemException("无数据");

                var users = _userService.GetPagedUsers().ToDictionary(k => k.Id, v => v.Uid);
                var models = new List<EventModel>();
                for (var i = 0; i < events.Count; i++) {
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
                        user = (!string.IsNullOrWhiteSpace(events[i].UserId)) && users.ContainsKey(events[i].UserId) ? users[events[i].UserId] : "",
                        created = CommonHelper.DateTimeConverter(events[i].CreatedTime)
                    });
                }

                using (var ms = _excelManager.Export<EventModel>(models, "日志信息列表", string.Format("操作人员：{0}  操作日期：{1}", _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name, CommonHelper.DateTimeConverter(DateTime.Now)))) {
                    return File(ms.ToArray(), _excelManager.ContentType, _excelManager.RandomFileName);
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearEvents() {
            try {
                var startDate = new DateTime(2017, 1, 1);
                var endDate = DateTime.Today.AddMonths(-3);
                _webLogger.Clear(startDate, endDate);
                _webLogger.Information(EnmEventType.Other, string.Format("清理{0}之前的系统日志信息", CommonHelper.DateConverter(endDate)), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "日志清理完成" }, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Notice() {
            if (!_workContext.Authorizations().Menus.Contains(1004))
                throw new HttpException(404, "Page not found.");

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

                var notices = _noticeService.GetPagedNoticesInSpan(startDate, endDate, start / limit, limit);
                if (notices.Count > 0) {
                    data.message = "200 Ok";
                    data.total = notices.TotalCount;
                    for (var i = 0; i < notices.Count; i++) {
                        data.data.Add(new NoticeModel {
                            index = start + i + 1,
                            id = notices[i].Id.ToString(),
                            title = notices[i].Title,
                            content = notices[i].Content,
                            created = CommonHelper.DateTimeConverter(notices[i].CreatedTime),
                            enabled = notices[i].Enabled
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                if (action == (int)EnmAction.Add)
                    return Json(data, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException("id");

                if (action != (int)EnmAction.Edit)
                    throw new ArgumentException("action");

                var notice = _noticeService.GetNotice(new Guid(id));
                if (notice == null) throw new iPemException("未找到数据对象");

                data.data.id = notice.Id.ToString();
                data.data.title = notice.Title;
                data.data.content = notice.Content;
                data.data.created = CommonHelper.DateConverter(notice.CreatedTime);
                data.data.enabled = notice.Enabled;
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveNotice(NoticeModel notice, string[] roles, int action) {
            try {
                if (notice == null) throw new ArgumentException("notice");
                if (action == (int)EnmAction.Add) {
                    var existed = _noticeService.GetNotice(new Guid(notice.id));
                    if (existed != null) throw new iPemException("消息已存在，保存失败。");

                    var newNotice = new H_Notice {
                        Id = new Guid(notice.id),
                        Title = notice.title,
                        Content = notice.content,
                        CreatedTime = DateTime.Now,
                        Enabled = notice.enabled
                    };

                    var toUsers = new List<U_User>();
                    var allUsers = _userService.GetUsers();
                    if (roles != null && roles.Length > 0) {
                        foreach (var role in roles) {
                            var usersInRole = allUsers.FindAll(u => u.RoleId == role);
                            if (usersInRole.Count > 0) toUsers.AddRange(usersInRole);
                        }
                    } else {
                        toUsers.AddRange(allUsers);
                    }

                    var noticesInUsers = new List<H_NoticeInUser>();
                    foreach (var user in toUsers) {
                        noticesInUsers.Add(new H_NoticeInUser {
                            NoticeId = newNotice.Id,
                            UserId = user.Id,
                            Readed = false,
                            ReadTime = DateTime.MinValue
                        });
                    }

                    _noticeService.Add(newNotice);
                    _noticeInUserService.Add(noticesInUsers.ToArray());
                    _webLogger.Information(EnmEventType.Other, string.Format("新增消息[{0}]", newNotice.Title), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "消息保存成功" });
                } else if (action == (int)EnmAction.Edit) {
                    var existed = _noticeService.GetNotice(new Guid(notice.id));
                    if (existed == null) throw new iPemException("消息不存在，保存失败。");

                    existed.Title = notice.title;
                    existed.Content = notice.content;
                    existed.CreatedTime = DateTime.Now;
                    existed.Enabled = notice.enabled;

                    var toUsers = new List<U_User>();
                    var allUsers = _userService.GetUsers();
                    if (roles != null && roles.Length > 0) {
                        foreach (var role in roles) {
                            var usersInRole = allUsers.FindAll(u => u.RoleId == role);
                            if (usersInRole.Count > 0) toUsers.AddRange(usersInRole);
                        }
                    } else {
                        toUsers.AddRange(allUsers);
                    }

                    var noticesInUsers = new List<H_NoticeInUser>();
                    foreach (var user in toUsers) {
                        noticesInUsers.Add(new H_NoticeInUser {
                            NoticeId = existed.Id,
                            UserId = user.Id,
                            Readed = false,
                            ReadTime = DateTime.MinValue
                        });
                    }

                    _noticeService.Remove(existed);
                    _noticeService.Add(existed);
                    _noticeInUserService.Add(noticesInUsers.ToArray());
                    _webLogger.Information(EnmEventType.Other, string.Format("更新消息[{0}]", existed.Title), _workContext.User().Id, null);
                    return Json(new AjaxResultModel { success = true, code = 200, message = "消息保存成功" });
                }

                throw new ArgumentException("action");
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult DeleteNotice(string id) {
            try {
                if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");
                var existed = _noticeService.GetNotice(new Guid(id));
                if (existed == null) throw new iPemException("消息不存在，删除失败");

                _noticeService.Remove(existed);
                _webLogger.Information(EnmEventType.Other, string.Format("删除系统消息[{0}]", existed.Title), _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "消息删除成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [Authorize]
        public ActionResult Dictionary() {
            if (!_workContext.Authorizations().Menus.Contains(1005))
                throw new HttpException(404, "Page not found.");

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
                    port = 8000,
                    fsuPath = "",
                    uid = "",
                    password = ""
                }
            };

            try {

                var ws = _dictionaryService.GetDictionary((int)EnmDictionary.Ws);
                if (ws != null && !string.IsNullOrWhiteSpace(ws.ValuesJson))
                    data.data = JsonConvert.DeserializeObject<WsValues>(ws.ValuesJson);

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveWs(WsValues values) {
            try {
                if (values == null) throw new ArgumentException("values");
                _dictionaryService.Update(new M_Dictionary {
                    Id = (int)EnmDictionary.Ws,
                    Name = Common.GetDictionaryDisplay(EnmDictionary.Ws),
                    ValuesJson = JsonConvert.SerializeObject(values),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });
                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                    bases = new int[0],
                    levels = new int[0],
                    contents = new int[0],
                    stationTypes = new string[0],
                    roomTypes = new string[0],
                    subDeviceTypes = new string[0],
                    subLogicTypes = new string[0],
                    points = new string[0],
                    keywords = ""
                }
            };

            try {
                var ts = _dictionaryService.GetDictionary((int)EnmDictionary.Ts);
                if (ts != null && !string.IsNullOrWhiteSpace(ts.ValuesJson))
                    data.data = JsonConvert.DeserializeObject<TsValues>(ts.ValuesJson);

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveTs(TsValues values) {
            try {
                if (values == null) throw new ArgumentException("values");
                _dictionaryService.Update(new M_Dictionary {
                    Id = (int)EnmDictionary.Ts,
                    Name = Common.GetDictionaryDisplay(EnmDictionary.Ts),
                    ValuesJson = JsonConvert.SerializeObject(values),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });

                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
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
                    whlHuLue = 0,
                    jslGuiDing = 0,
                    jslHuLue = 0,
                    jslQueRen = 0,
                    indicator01 = (int)EnmFormula.KT,
                    indicator02 = (int)EnmFormula.KT,
                    indicator03 = (int)EnmFormula.KT,
                    indicator04 = (int)EnmFormula.KT,
                    indicator05 = (int)EnmFormula.KT,
                    indicator = (int)EnmFormula.TT
                }
            };

            try {
                var rt = _dictionaryService.GetDictionary((int)EnmDictionary.Report);
                if (rt != null && !string.IsNullOrWhiteSpace(rt.ValuesJson))
                    data.data = JsonConvert.DeserializeObject<RtValues>(rt.ValuesJson);

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult SaveRt(RtValues values) {
            try {
                if (values == null) throw new ArgumentException("values");
                _dictionaryService.Update(new M_Dictionary {
                    Id = (int)EnmDictionary.Report,
                    Name = Common.GetDictionaryDisplay(EnmDictionary.Report),
                    ValuesJson = JsonConvert.SerializeObject(values),
                    ValuesBinary = null,
                    LastUpdatedDate = DateTime.Now
                });

                return Json(new AjaxResultModel { success = true, code = 200, message = "保存成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearCache() {
            try {
                this.UpdateFsuRelation();
                this.ClearGlobalCache();
                this.ClearUserCache();
                return Json(new AjaxResultModel { success = true, code = 200, message = "所有缓存清除成功" }, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearGlobalCache() {
            try {
                //_cacheManager.RemoveByPattern(@"ipems:global:.+");
                _cacheManager.Clear();
                return Json(new AjaxResultModel { success = true, code = 200, message = "全局缓存清除成功" }, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult ClearUserCache() {
            try {
                _workContext.ResetRole();
                _workContext.ResetUser();
                _workContext.ResetEmployee();
                _workContext.ResetProfile(EnmProfile.Condition);
                _workContext.ResetProfile(EnmProfile.Follow);
                _workContext.ResetProfile(EnmProfile.Matrix);
                _workContext.ResetProfile(EnmProfile.Speech);
                return Json(new AjaxResultModel { success = true, code = 200, message = "用户缓存清除成功" }, JsonRequestBehavior.AllowGet);
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AjaxAuthorize]
        public JsonResult UpdateFsuRelation() {
            try {
                _fsuService.UpdateFsus();
                _cacheManager.Remove(GlobalCacheKeys.Rs_FsusRepository);
                _noteService.Add(new H_Note { SysType = 2, GroupID = "-1", Name = "D_FSU", DtType = 0, OpType = 0, Time = DateTime.Now, Desc = "同步FSU数据" });
                _webLogger.Information(EnmEventType.Other, "更新FSU关联信息", _workContext.User().Id, null);
                return Json(new AjaxResultModel { success = true, code = 200, message = "更新关联成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult UpdateScScript(string password) {
            try {
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("鉴权密码验证失败。");
                if (CommonHelper.CreateDynamicKeys() != password) throw new ArgumentException("鉴权密码验证失败。");

                var current = Request.Files[0];
                if (!Path.GetExtension(current.FileName).Equals(".sql", StringComparison.CurrentCultureIgnoreCase)) throw new iPemException("请选择脚本文件（*.sql）");
                var fileName = Path.GetFileNameWithoutExtension(current.FileName);
                if (!fileName.StartsWith("P2S_", StringComparison.CurrentCultureIgnoreCase)) throw new iPemException("请选择P2S脚本文件");

                var scripts = _sdbScriptService.GetEntities();
                var script = scripts.Find(s => s.Id.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
                if (script != null) throw new iPemException("脚本已存在，无需执行。");

                _scExecutor.Execute(current.InputStream);
                _sdbScriptService.Update(new S_DBScript { Id = fileName, Executor = _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name });
                return Json(new AjaxResultModel { success = true, code = 200, message = "脚本执行成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetScScripts(int start, int limit) {
            var data = new AjaxDataModel<List<ScriptModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ScriptModel>()
            };

            try {
                var scripts = _sdbScriptService.GetPagedDBScripts(start / limit, limit);
                if (scripts.Count > 0) {
                    data.message = "200 Ok";
                    data.total = scripts.TotalCount;
                    for (var i = 0; i < scripts.Count; i++) {
                        data.data.Add(new ScriptModel {
                            id = scripts[i].Id,
                            name = scripts[i].Name,
                            creator = scripts[i].Creator,
                            createdtime = CommonHelper.DateTimeConverter(scripts[i].CreatedTime),
                            executor = scripts[i].Executor,
                            executedtime = CommonHelper.DateTimeConverter(scripts[i].ExecutedTime),
                            comment = scripts[i].Comment
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult UpdateCsScript(string password) {
            try {
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("鉴权密码验证失败。");
                if (CommonHelper.CreateDynamicKeys() != password) throw new ArgumentException("鉴权密码验证失败。");

                var current = Request.Files[0];
                if (!Path.GetExtension(current.FileName).Equals(".sql", StringComparison.CurrentCultureIgnoreCase)) throw new iPemException("请选择脚本文件（*.sql）");
                var fileName = Path.GetFileNameWithoutExtension(current.FileName);
                if (!fileName.StartsWith("P2H_", StringComparison.CurrentCultureIgnoreCase)) throw new iPemException("请选择P2H脚本文件");

                var scripts = _hdbScriptService.GetEntities();
                var script = scripts.Find(s => s.Id.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
                if (script != null) throw new iPemException("脚本已存在，无需执行。");

                _csExecutor.Execute(current.InputStream);
                _hdbScriptService.Update(new H_DBScript { Id = fileName, Executor = _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name });
                return Json(new AjaxResultModel { success = true, code = 200, message = "脚本执行成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetCsScripts(int start, int limit) {
            var data = new AjaxDataModel<List<ScriptModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ScriptModel>()
            };

            try {
                var scripts = _hdbScriptService.GetPagedDBScripts(start / limit, limit);
                if (scripts.Count > 0) {
                    data.message = "200 Ok";
                    data.total = scripts.TotalCount;
                    for (var i = 0; i < scripts.Count; i++) {
                        data.data.Add(new ScriptModel {
                            id = scripts[i].Id,
                            name = scripts[i].Name,
                            creator = scripts[i].Creator,
                            createdtime = CommonHelper.DateTimeConverter(scripts[i].CreatedTime),
                            executor = scripts[i].Executor,
                            executedtime = CommonHelper.DateTimeConverter(scripts[i].ExecutedTime),
                            comment = scripts[i].Comment
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult UpdateRsScript(string password) {
            try {
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("鉴权密码验证失败。");
                if (CommonHelper.CreateDynamicKeys() != password) throw new ArgumentException("鉴权密码验证失败。");

                var current = Request.Files[0];
                if (!Path.GetExtension(current.FileName).Equals(".sql", StringComparison.CurrentCultureIgnoreCase)) throw new iPemException("请选择脚本文件（*.sql）");
                var fileName = Path.GetFileNameWithoutExtension(current.FileName);
                if (!fileName.StartsWith("P2R_", StringComparison.CurrentCultureIgnoreCase)) throw new iPemException("请选择P2R脚本文件");

                var scripts = _rdbScriptService.GetEntities();
                var script = scripts.Find(s => s.Id.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
                if (script != null) throw new iPemException("脚本已存在，无需执行。");

                _rsExecutor.Execute(current.InputStream);
                _rdbScriptService.Update(new R_DBScript { Id = fileName, Executor = _workContext.Employee() != null ? _workContext.Employee().Name : User.Identity.Name });
                return Json(new AjaxResultModel { success = true, code = 200, message = "脚本执行成功" });
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetRsScripts(int start, int limit) {
            var data = new AjaxDataModel<List<ScriptModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<ScriptModel>()
            };

            try {
                var scripts = _rdbScriptService.GetPagedDBScripts(start / limit, limit);
                if (scripts.Count > 0) {
                    data.message = "200 Ok";
                    data.total = scripts.TotalCount;
                    for (var i = 0; i < scripts.Count; i++) {
                        data.data.Add(new ScriptModel {
                            id = scripts[i].Id,
                            name = scripts[i].Name,
                            creator = scripts[i].Creator,
                            createdtime = CommonHelper.DateTimeConverter(scripts[i].CreatedTime),
                            executor = scripts[i].Executor,
                            executedtime = CommonHelper.DateTimeConverter(scripts[i].ExecutedTime),
                            comment = scripts[i].Comment
                        });
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult GetNodeIcons(string[] nodes) {
            var data = new AjaxDataModel<List<NodeIcon>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<NodeIcon>()
            };

            try {
                if (nodes != null && nodes.Length > 0) {
                    data.message = "200 Ok";
                    data.total = nodes.Length;

                    var alarms = _workContext.ActAlarms();
                    if (alarms.Count > 0) {
                        var areaIcons = alarms.GroupBy(a => a.Current.AreaId).Select(g => new NodeIcon { id = g.Key, level = g.Min(a => (int)a.Current.AlarmLevel) }).ToList();
                        data.data.Add(new NodeIcon { id = "root", level = areaIcons.Min(a => a.level), type = (int)EnmSSH.Root });
                        foreach (var node in nodes) {
                            var keys = Common.SplitKeys(node);
                            if (keys.Length != 2) continue;
                            var type = int.Parse(keys[0]); var id = keys[1];
                            var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                            var target = new NodeIcon { id = node, level = (int)EnmAlarm.Level0, type = (int)nodeType };

                            if (nodeType == EnmSSH.Area) {
                                var current = _workContext.Areas().Find(a => a.Current.Id == id);
                                if (current != null) {
                                    if (current.HasChildren) {
                                        var icons = areaIcons.FindAll(i => current.Keys.Contains(i.id));
                                        if (icons.Count > 0) target.level = icons.Min(i => i.level);
                                    } else {
                                        var icon = areaIcons.Find(i => i.id == current.Current.Id);
                                        if (icon != null) target.level = icon.level;
                                    }
                                }
                            } else if (nodeType == EnmSSH.Station) {
                                var icons = alarms.FindAll(a => a.Current.StationId == id);
                                if (icons.Count > 0) target.level = icons.Min(i => (int)i.Current.AlarmLevel);
                            } else if (nodeType == EnmSSH.Room) {
                                var icons = alarms.FindAll(a => a.Current.RoomId == id);
                                if (icons.Count > 0) target.level = icons.Min(i => (int)i.Current.AlarmLevel);
                            } else if (nodeType == EnmSSH.Device) {
                                var icons = alarms.FindAll(a => a.Current.DeviceId == id);
                                if (icons.Count > 0) target.level = icons.Min(i => (int)i.Current.AlarmLevel);
                            }

                            data.data.Add(target);
                        }
                    } else {
                        data.data.Add(new NodeIcon { id = "root", level = (int)EnmAlarm.Level0, type = (int)EnmSSH.Root });
                        foreach (var node in nodes) {
                            var keys = Common.SplitKeys(node);
                            if (keys.Length != 2) continue;
                            var type = int.Parse(keys[0]); var id = keys[1];
                            var nodeType = Enum.IsDefined(typeof(EnmSSH), type) ? (EnmSSH)type : EnmSSH.Area;
                            var target = new NodeIcon { id = node, level = (int)EnmAlarm.Level0, type = (int)nodeType };
                            data.data.Add(target);
                        }
                    }
                }
            } catch (Exception exc) {
                _webLogger.Error(EnmEventType.Other, exc.Message, _workContext.User().Id, exc);
                data.success = false; data.message = exc.Message;
            }

            return Json(data);
        }

        #endregion

    }
}