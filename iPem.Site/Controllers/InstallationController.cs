﻿using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Data;
using iPem.Core.Domain.Common;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Common;
using iPem.Data.Installation;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.Installation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class InstallationController : JsonNetController {

        #region Fields

        private readonly IDataProvider _dataProvider;
        private readonly IDbManager _dbManager;
        private readonly IDbInstaller _dbInstaller;
        private readonly ICacheManager _cacheManager;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public InstallationController(
            IDataProvider dataProvider,
            IDbManager dbManager,
            IDbInstaller dbInstaller,
            ICacheManager cacheManager,
            IWebHelper webHelper) {
            this._dataProvider = dataProvider;
            this._dbManager = dbManager;
            this._dbInstaller = dbInstaller;
            this._cacheManager = cacheManager;
            this._webHelper = webHelper;
        }

        #endregion

        #region Action

        public ActionResult Index() {
            var key = "ipems:install:installation";
            if (Session[key] == null || DateTime.Now.Ticks > (long)Session[key]) {
                return View("Authentication", new AuthModel {
                    key = key,
                    name = "安装向导鉴权",
                    service = "/Installation"
                });
            }

            if (_dbManager.DatabaseIsInstalled())
                return RedirectToRoute("HomePage");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Success(string data) {
            //restart application
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            webHelper.RestartAppDomain();

            //Redirect to home page
            return RedirectToRoute("HomePage");
        }

        public JsonResult InstallRs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.InstallRs."
            };

            try {
                var database = JsonConvert.DeserializeObject<DbModel>(data);
                var installed = (type == 0);
                var created = (database.crdnew == 0);

                var scripts = new List<string>(){
                        "~/Resources/install/create/create_rs_database.sql",
                        "~/Resources/install/create/create_rs_data.sql"
                    };

                if (!created)
                    database.name = database.oname;

                if (created) {
                    var createdString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, "master", database.uid, database.pwd);
                    _dbInstaller.InstallDatabase(createdString, database.name, database.path);
                } else if (!database.uncheck) {
                    if (!SqlHelper.DatabaseExists(false, database.ipv4, database.port, database.name, database.uid, database.pwd))
                        throw new iPemException(string.Format("数据库 '{0}' 不存在", database.name));
                }

                var connectionString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, database.name, database.uid, database.pwd);
                using (var scope = new System.Transactions.TransactionScope()) {
                    foreach (var file in scripts) {
                        _dbInstaller.ExecuteScript(connectionString, file);
                    }

                    scope.Complete();
                }

                var entity = new DbEntity() {
                    Id = Guid.NewGuid(),
                    Provider = EnmDbProvider.SqlServer,
                    Type = EnmDbType.Rs,
                    IP = database.ipv4,
                    Port = database.port,
                    Uid = database.uid,
                    Password = database.pwd,
                    Name = database.name
                };

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallCs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.InstallCs."
            };

            try {
                var database = JsonConvert.DeserializeObject<DbModel>(data);
                var installed = (type == 0);
                var created = (database.crdnew == 0);

                var scripts = new List<string>(){
                        "~/Resources/install/create/create_cs_database.sql",
                        "~/Resources/install/create/create_cs_data.sql"
                    };

                if (!created)
                    database.name = database.oname;

                if (created) {
                    var createdString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, "master", database.uid, database.pwd);
                    _dbInstaller.InstallDatabase(createdString, database.name, database.path);
                } else if (!database.uncheck) {
                    if (!SqlHelper.DatabaseExists(false, database.ipv4, database.port, database.name, database.uid, database.pwd))
                        throw new iPemException(string.Format("数据库 '{0}' 不存在", database.name));
                }

                var connectionString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, database.name, database.uid, database.pwd);
                using (var scope = new System.Transactions.TransactionScope()) {
                    foreach (var file in scripts) {
                        _dbInstaller.ExecuteScript(connectionString, file);
                    }

                    scope.Complete();
                }

                var entity = new DbEntity() {
                    Id = Guid.NewGuid(),
                    Provider = EnmDbProvider.SqlServer,
                    Type = EnmDbType.Cs,
                    IP = database.ipv4,
                    Port = database.port,
                    Uid = database.uid,
                    Password = database.pwd,
                    Name = database.name
                };

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallSc(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.InstallSc."
            };

            try {
                var database = JsonConvert.DeserializeObject<DbModel>(data);
                var installed = (type == 0);
                var created = (database.crdnew == 0);

                var scripts = new List<string>(){
                        "~/Resources/install/create/create_sc_database.sql",
                        "~/Resources/install/create/create_sc_data.sql"
                    };

                if (!created)
                    database.name = database.oname;

                if (created) {
                    var createdString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, "master", database.uid, database.pwd);
                    _dbInstaller.InstallDatabase(createdString, database.name, database.path);
                } else if (!database.uncheck) {
                    if (!SqlHelper.DatabaseExists(false, database.ipv4, database.port, database.name, database.uid, database.pwd))
                        throw new iPemException(string.Format("数据库 '{0}' 不存在", database.name));
                }

                var connectionString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, database.name, database.uid, database.pwd);
                using (var scope = new System.Transactions.TransactionScope()) {
                    foreach (var file in scripts) {
                        _dbInstaller.ExecuteScript(connectionString, file);
                    }

                    scope.Complete();
                }

                var entity = new DbEntity() {
                    Id = Guid.NewGuid(),
                    Provider = EnmDbProvider.SqlServer,
                    Type = EnmDbType.Sc,
                    IP = database.ipv4,
                    Port = database.port,
                    Uid = database.uid,
                    Password = database.pwd,
                    Name = database.name
                };

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallRl(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.InstallRl."
            };

            try {
                if (!_dbManager.DatabaseIsInstalled())
                    throw new iPemException("数据库尚未配置，请完成配置后重试。");

                var model = JsonConvert.DeserializeObject<iPem.Site.Models.Installation.RoleModel>(data);
                var repository = new U_RoleRepository(_dbManager.CurrentConnetions[EnmDbType.Sc]);
                var service = new RoleService(repository, _cacheManager);
                var installed = (type == 0);

                var entity = service.GetRoleById(U_Role.SuperId);
                if (entity != null) service.Remove(entity);

                entity = new U_Role() {
                    Id = U_Role.SuperId,
                    Name = model.name,
                    Comment = model.comment,
                    Enabled = true
                };

                service.Add(entity);
                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallUe(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.InstallUe."
            };

            try {
                if (!_dbManager.DatabaseIsInstalled())
                    throw new iPemException("数据库尚未配置，请完成配置后重试。");

                var model = JsonConvert.DeserializeObject<iPem.Site.Models.Installation.UserModel>(data);
                var repository = new U_UserRepository(_dbManager.CurrentConnetions[EnmDbType.Sc]);
                var service = new UserService(repository, _cacheManager);
                var installed = (type == 0);

                var entity = service.GetUserByName(model.name);
                if (entity != null) service.Remove(entity);

                service.Add(new U_User() {
                    RoleId = U_Role.SuperId,
                    Id = "0",
                    Uid = model.name,
                    Password = model.pwd,
                    CreatedDate = DateTime.Now,
                    LimitedDate = new DateTime(2099, 12, 31),
                    LastLoginDate = DateTime.Now,
                    LastPasswordChangedDate = DateTime.Now,
                    FailedPasswordAttemptCount = 0,
                    FailedPasswordDate = DateTime.Now,
                    IsLockedOut = false,
                    LastLockoutDate = DateTime.Now,
                    Comment = model.comment,
                    EmployeeId = "00",
                    Enabled = true
                });

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallFs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.InstallFs."
            };

            try {
                _cacheManager.Clear();
                EngineContext.Initialize(true);
                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public ActionResult DbConfiguration() {
            var key = "ipems:install:auth-configuration";
            if (Session[key] == null || DateTime.Now.Ticks > (long)Session[key]) {
                return View("Authentication", new AuthModel {
                    key = key,
                    name = "数据库操作鉴权",
                    service = "/Installation/DbConfiguration"
                });
            }

            if (!_dbManager.DatabaseIsInstalled())
                return RedirectToAction("Index");

            ViewData["RsDbSet"] = _dbManager.CurrentDbSets[EnmDbType.Rs];
            ViewData["CsDbSet"] = _dbManager.CurrentDbSets[EnmDbType.Cs];
            ViewData["ScDbSet"] = _dbManager.CurrentDbSets[EnmDbType.Sc];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveRs(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.SaveRs."
            };

            try {
                if (entity == null) throw new ArgumentException("参数无效 entity");

                entity.Id = Guid.NewGuid();
                entity.Provider = EnmDbProvider.SqlServer;
                entity.Type = EnmDbType.Rs;

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _cacheManager.Clear();
                _dbManager.Initializer();
                EngineContext.Initialize(true);

                result.success = true;
                result.code = 200;
                result.message = "数据保存成功";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveCs(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.SaveCs."
            };

            try {
                if (entity == null) throw new ArgumentException("参数无效 entity");

                entity.Id = Guid.NewGuid();
                entity.Provider = EnmDbProvider.SqlServer;
                entity.Type = EnmDbType.Cs;

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _cacheManager.Clear();
                _dbManager.Initializer();
                EngineContext.Initialize(true);

                result.success = true;
                result.code = 200;
                result.message = "数据保存成功";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveSc(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.SaveSc."
            };

            try {
                if (entity == null) throw new ArgumentException("参数无效 entity");

                entity.Id = Guid.NewGuid();
                entity.Provider = EnmDbProvider.SqlServer;
                entity.Type = EnmDbType.Sc;

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _cacheManager.Clear();
                _dbManager.Initializer();
                EngineContext.Initialize(true);

                result.success = true;
                result.code = 200;
                result.message = "数据保存成功";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DbClean(string password) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.DbClean."
            };

            try {
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("确认密码验证失败，请与管理员联系。");

                if (!string.Format("{0}@10078", CommonHelper.CreateDynamicKeys()).Equals(password))
                    throw new ArgumentException("确认密码验证失败，请与管理员联系。");

                _cacheManager.Clear();
                _dataProvider.CleanEntites();
                _dbManager.Clean();
                EngineContext.Initialize(true);

                result.success = true;
                result.code = 200;
                result.message = "删除成功，页面将在<span id='leftseconds'>5</span>秒后跳转到安装向导。";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DbTest(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Error: Installation.DbTest."
            };

            try {
                if (entity == null) throw new ArgumentException("参数无效 entity");
                SqlHelper.TestConnection(false, entity.IP, entity.Port, entity.Name, entity.Uid, entity.Password);
                result.success = true;
                result.code = 200;
                result.message = "数据库连接成功";
            } catch (Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public ActionResult Authentication() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authentication(AuthModel model, string password) {
            try {
                if (model == null)
                    ModelState.AddModelError("", "应用Model不能为空。");

                if (ModelState.IsValid && String.IsNullOrWhiteSpace(model.key))
                    ModelState.AddModelError("", "应用Key不能为空。");

                if (ModelState.IsValid && String.IsNullOrWhiteSpace(model.service))
                    ModelState.AddModelError("", "无法获取应用地址。");

                if (ModelState.IsValid && !Url.IsLocalUrl(model.service))
                    ModelState.AddModelError("", "应用地址不是有效的URL。");

                if (String.IsNullOrWhiteSpace(password))
                    ModelState.AddModelError("", "鉴权密码不能为空。");

                if (ModelState.IsValid && CommonHelper.CreateDynamicKeys() != password)
                    ModelState.AddModelError("", "密码验证失败，请与管理员联系。");

                if (ModelState.IsValid) {
                    Session[model.key] = DateTime.Now.AddMinutes(5).Ticks;
                    return Redirect(model.service);
                }
            } catch (Exception exc) {
                ModelState.AddModelError("", exc.Message);
            }

            return View(model);
        }

        #endregion

    }
}