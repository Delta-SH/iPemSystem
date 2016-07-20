using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Data;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Common;
using iPem.Data.Installation;
using iPem.Data.Repository.Master;
using iPem.Services.Master;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using iPem.Site.Models.Installation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    public class InstallationController : Controller {

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
            if(_dbManager.DatabaseIsInstalled())
                return RedirectToRoute("HomePage");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string data) {
            //restart application
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            webHelper.RestartAppDomain();

            //Redirect to home page
            return RedirectToRoute("HomePage");
        }

        public JsonResult InstallCs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.InstallCs error."
            };

            try {
                var database = JsonConvert.DeserializeObject<DbModel>(data);
                var installed = (type == 0); var created = (database.crdnew == 0);

                var scripts = new List<string>(){
                        "~/Resources/install/create/create_cs_database.sql",
                        "~/Resources/install/create/create_cs_data.sql"
                    };

                if(!created)
                    database.name = database.oname;

                if(created) {
                    var createdString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, "master", database.uid, database.pwd);
                    _dbInstaller.InstallDatabase(createdString, database.name, database.path);
                } else if(!database.uncheck) {
                    if(!SqlHelper.DatabaseExists(false, database.ipv4, database.port, database.name, database.uid, database.pwd))
                        throw new iPemException(string.Format("数据库 '{0}' 不存在", database.name));
                }

                var connectionString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, database.name, database.uid, database.pwd);
                using(var scope = new System.Transactions.TransactionScope()) {
                    foreach(var file in scripts) {
                        _dbInstaller.InstallData(connectionString, file);
                    }

                    scope.Complete();
                }

                var entity = new DbEntity() {
                    Id = Guid.NewGuid(),
                    Provider = EnmDbProvider.SqlServer,
                    Type = EnmDatabaseType.Master,
                    IP = database.ipv4,
                    Port = database.port,
                    UId = database.uid,
                    Pwd = database.pwd,
                    Name = database.name
                };

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallHs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.InstallHs error."
            };

            try {
                var database = JsonConvert.DeserializeObject<DbModel>(data);
                var installed = (type == 0); var created = (database.crdnew == 0);

                var scripts = new List<string>(){
                        "~/Resources/install/create/create_hs_database.sql",
                        "~/Resources/install/create/create_hs_data.sql"
                    };

                if(!created)
                    database.name = database.oname;

                if(created) {
                    var createdString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, "master", database.uid, database.pwd);
                    _dbInstaller.InstallDatabase(createdString, database.name, database.path);
                } else if(!database.uncheck) {
                    if(!SqlHelper.DatabaseExists(false, database.ipv4, database.port, database.name, database.uid, database.pwd))
                        throw new iPemException(string.Format("数据库 '{0}' 不存在", database.name));
                }

                var connectionString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, database.name, database.uid, database.pwd);
                using(var scope = new System.Transactions.TransactionScope()) {
                    foreach(var file in scripts) {
                        _dbInstaller.InstallData(connectionString, file);
                    }

                    scope.Complete();
                }

                var entity = new DbEntity() {
                    Id = Guid.NewGuid(),
                    Provider = EnmDbProvider.SqlServer,
                    Type = EnmDatabaseType.History,
                    IP = database.ipv4,
                    Port = database.port,
                    UId = database.uid,
                    Pwd = database.pwd,
                    Name = database.name
                };

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallRs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.InstallRs error."
            };

            try {
                var database = JsonConvert.DeserializeObject<DbModel>(data);
                var installed = (type == 0); var created = (database.crdnew == 0);

                var scripts = new List<string>(){
                        "~/Resources/install/create/create_rs_database.sql",
                        "~/Resources/install/create/create_rs_data.sql"
                    };

                if(!created)
                    database.name = database.oname;

                if(created) {
                    var createdString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, "master", database.uid, database.pwd);
                    _dbInstaller.InstallDatabase(createdString, database.name, database.path);
                } else if(!database.uncheck) {
                    if(!SqlHelper.DatabaseExists(false, database.ipv4, database.port, database.name, database.uid, database.pwd))
                        throw new iPemException(string.Format("数据库 '{0}' 不存在", database.name));
                }

                var connectionString = SqlHelper.CreateConnectionString(false, database.ipv4, database.port, database.name, database.uid, database.pwd);
                using(var scope = new System.Transactions.TransactionScope()) {
                    foreach(var file in scripts) {
                        _dbInstaller.InstallData(connectionString, file);
                    }

                    scope.Complete();
                }

                var entity = new DbEntity() {
                    Id = Guid.NewGuid(),
                    Provider = EnmDbProvider.SqlServer,
                    Type = EnmDatabaseType.Resource,
                    IP = database.ipv4,
                    Port = database.port,
                    UId = database.uid,
                    Pwd = database.pwd,
                    Name = database.name
                };

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallRl(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.InstallRl error."
            };

            try {
                if(!_dbManager.DatabaseIsInstalled())
                    throw new iPemException("数据库未配置");

                var model = JsonConvert.DeserializeObject<iPem.Site.Models.Installation.RoleModel>(data);
                var repository = new RoleRepository(_dbManager.CurrentConnetions[EnmDatabaseType.Master]);
                var service = new RoleService(repository, _cacheManager);
                var installed = (type == 0);

                var entity = service.GetRole(Role.SuperId);
                if(entity != null) service.DeleteRole(entity);

                entity = new Role() {
                    Id = Role.SuperId,
                    Name = model.name,
                    Comment = model.comment,
                    Enabled = true
                };

                service.InsertRole(entity);

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallUe(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.InstallUe error."
            };

            try {
                if(!_dbManager.DatabaseIsInstalled())
                    throw new iPemException("数据库未配置");

                var model = JsonConvert.DeserializeObject<iPem.Site.Models.Installation.UserModel>(data);
                var repository = new UserRepository(_dbManager.CurrentConnetions[EnmDatabaseType.Master]);
                var service = new UserService(repository, _cacheManager);
                var installed = (type == 0);

                var entity = service.GetUser(model.name);
                if(entity != null) service.DeleteUser(entity);

                service.InsertUser(new User() {
                    RoleId = Role.SuperId,
                    Id = Guid.NewGuid(),
                    Uid = model.name,
                    Password = model.pwd,
                    CreateDate = DateTime.Now,
                    LimitDate = new DateTime(2099, 12, 31),
                    LastLoginDate = DateTime.Now,
                    LastPasswordChangedDate = DateTime.Now,
                    FailedPasswordAttemptCount = 0,
                    FailedPasswordDate = DateTime.Now,
                    IsLockedOut = false,
                    LastLockoutDate = DateTime.Now,
                    Comment = model.comment,
                    EmployeeId = "",
                    Enabled = true
                });

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public JsonResult InstallFs(int type, string data) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.InstallFs error."
            };

            try {
                EngineContext.Initialize(true);

                result.success = true;
                result.code = 200;
                result.message = "OK";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        public ActionResult DbConfiguration() {
            if(!_dbManager.DatabaseIsInstalled())
                return RedirectToAction("Index");

            ViewData["master"] = _dbManager.CurrentDbSets[EnmDatabaseType.Master];
            ViewData["history"] = _dbManager.CurrentDbSets[EnmDatabaseType.History];
            ViewData["resource"] = _dbManager.CurrentDbSets[EnmDatabaseType.Resource];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveCs(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.SaveCs error."
            };

            try {
                if(entity == null)
                    throw new ArgumentException("参数无效 entity");

                entity.Id = Guid.NewGuid();
                entity.Provider = EnmDbProvider.SqlServer;
                entity.Type = EnmDatabaseType.Master;

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();
                //_webHelper.RestartAppDomain();

                result.success = true;
                result.code = 200;
                result.message = "数据保存成功";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveHs(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.SaveHs error."
            };

            try {
                if(entity == null)
                    throw new ArgumentException("参数无效 entity");

                entity.Id = Guid.NewGuid();
                entity.Provider = EnmDbProvider.SqlServer;
                entity.Type = EnmDatabaseType.History;

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();
                //_webHelper.RestartAppDomain();

                result.success = true;
                result.code = 200;
                result.message = "数据保存成功";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveRs(DbEntity entity) {
            var result = new AjaxResultModel {
                success = false,
                code = 400,
                message = "Install.SaveRs error."
            };

            try {
                if(entity == null)
                    throw new ArgumentException("参数无效 entity");

                entity.Id = Guid.NewGuid();
                entity.Provider = EnmDbProvider.SqlServer;
                entity.Type = EnmDatabaseType.Resource;

                _dataProvider.DelEntites(new List<DbEntity>() { entity });
                _dataProvider.SaveEntites(new List<DbEntity>() { entity });
                _dbManager.Initializer();
                //_webHelper.RestartAppDomain();

                result.success = true;
                result.code = 200;
                result.message = "数据保存成功";
            } catch(Exception err) {
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
                message = "Install.DbClean error."
            };

            try {
                if(string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("确认密码验证失败，请与管理员联系。");

                if(CommonHelper.GetCleanKey() != password)
                    throw new ArgumentException("确认密码验证失败，请与管理员联系。");

                _dataProvider.CleanEntites();
                _dbManager.Clean();
                //_webHelper.RestartAppDomain();

                result.success = true;
                result.code = 200;
                result.message = "删除成功，页面将在<span id='leftseconds'>5</span>秒后跳转到安装向导。";
            } catch(Exception err) {
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
                message = "Install.DbTest error."
            };

            try {
                if(entity == null)
                    throw new ArgumentException("参数无效 entity");

                SqlHelper.TestConnection(false, entity.IP, entity.Port, entity.Name, entity.UId, entity.Pwd);
                result.success = true;
                result.code = 200;
                result.message = "数据库连接成功";
            } catch(Exception err) {
                result.message = err.Message;
            }

            return Json(result);
        }

        #endregion

    }
}