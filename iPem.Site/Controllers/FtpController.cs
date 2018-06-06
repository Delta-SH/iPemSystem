using FluentFTP;
using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Services.Common;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using iPem.Site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Controllers {
    [Authorize]
    public class FtpController : JsonNetController {

        #region Fields

        private readonly IExcelManager _excelManager;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebEventService _webLogger;
        private readonly IFsuService _fsuService;
        private readonly IFtpService _ftpService;

        #endregion

        #region Ctor

        public FtpController(
            IExcelManager excelManager,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IWebEventService webLogger,
            IFsuService fsuService,
            IFtpService ftpService) {
            this._excelManager = excelManager;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._webLogger = webLogger;
            this._fsuService = fsuService;
            this._ftpService = ftpService;
        }

        #endregion

        #region Actions

        public ActionResult Index() {
            return View();
        }

        public ActionResult Update() {
            var ftp = _ftpService.GetFtps(EnmFtp.Master).FirstOrDefault();
            if (ftp != null) {
                ftp.Directory = "Update";
                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromDays(1));
                ViewBag.FTPKey = key;
            }
            return View("Index");
        }

        public ActionResult Config() {
            var ftp = _ftpService.GetFtps(EnmFtp.Master).FirstOrDefault();
            if (ftp != null) {
                ftp.Directory = "Config";
                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromDays(1));
                ViewBag.FTPKey = key;
            }

            return View("Index");
        }

        public ActionResult FsuLog(string fsu) {
            var ext = _fsuService.GetExtFsu(fsu);
            if (ext != null) {
                var ftp = new C_Ftp {
                    IP = ext.IP,
                    Port = 21,
                    User = ext.FtpUid,
                    Password = ext.FtpPwd,
                    Directory = "logs"
                };

                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromHours(1));
                ViewBag.FTPKey = key;
            }
            return View("Index");
        }

        public ActionResult FsuConfig(string fsu) {
            var ext = _fsuService.GetExtFsu(fsu);
            if (ext != null) {
                var ftp = new C_Ftp {
                    IP = ext.IP,
                    Port = 21,
                    User = ext.FtpUid,
                    Password = ext.FtpPwd,
                    Directory = "Config"
                };

                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromHours(1));
                ViewBag.FTPKey = key;
            }
            return View("Index");
        }

        public ActionResult FsuAlarm(string fsu, string dt) {
            var ext = _fsuService.GetExtFsu(fsu);
            if (ext != null) {
                var ftp = new C_Ftp {
                    IP = ext.IP,
                    Port = 21,
                    User = ext.FtpUid,
                    Password = ext.FtpPwd,
                    Directory = string.Format("Alarm/{0}", dt)
                };

                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromHours(1));
                ViewBag.FTPKey = key;
            }
            return View("Index");
        }

        public ActionResult FsuMeasurement(string fsu) {
            var ext = _fsuService.GetExtFsu(fsu);
            if (ext != null) {
                var ftp = new C_Ftp {
                    IP = ext.IP,
                    Port = 21,
                    User = ext.FtpUid,
                    Password = ext.FtpPwd,
                    Directory = "Measurement"
                };

                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromHours(1));
                ViewBag.FTPKey = key;
            }
            return View("Index");
        }

        public ActionResult FsuUpgrade(string fsu) {
            var ext = _fsuService.GetExtFsu(fsu);
            if (ext != null) {
                var ftp = new C_Ftp {
                    IP = ext.IP,
                    Port = 21,
                    User = ext.FtpUid,
                    Password = ext.FtpPwd,
                    Directory = "upgrade"
                };

                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromHours(1));
                ViewBag.FTPKey = key;
            }
            return View("Index");
        }

        public JsonResult GetFiles(int start, int limit, string key, string name, bool cache) {
            var data = new AjaxDataModel<List<FileModel>> {
                success = true,
                message = "无数据",
                total = 0,
                data = new List<FileModel>()
            };

            try {
                var files = this.GetFtpFiles(key, name, cache);
                if (files != null && files.Count > 0) {
                    data.message = "200 OK";
                    data.total = files.Count;

                    var end = start + limit;
                    if (end > files.Count)
                        end = files.Count;

                    for (int i = start; i < end; i++) {
                        data.data.Add(files[i]);
                    }
                }
            } catch (Exception exc) {
                data.success = false; data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult Login(string ip, int port, string user, string password, string directory) {
            try {
                if (string.IsNullOrWhiteSpace(ip))
                    throw new ArgumentNullException("ip");
                if (string.IsNullOrWhiteSpace(user))
                    throw new ArgumentNullException("user");
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(directory))
                    throw new ArgumentNullException("directory");

                using (FtpClient conn = new FtpClient()) {
                    conn.Host = ip;
                    conn.Port = port;
                    conn.Credentials = new NetworkCredential(user, password);
                    conn.SetWorkingDirectory(directory);
                    conn.Connect();
                }

                var ftp = new C_Ftp { Name = "手动认证", IP = ip, Port = port, User = user, Password = password, Directory = directory };
                var key = this.GetFtpKey(ftp);
                _cacheManager.Set(string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key), ftp, TimeSpan.FromDays(1));
                return Json(new AjaxResultModel { success = true, code = 200, message = key });
            } catch (Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        public JsonResult Upload(string key) {
            try {
                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException("key");

                var current = Request.Files[0];
                var fileName = Path.GetFileName(current.FileName);

                using (FtpClient conn = this.GetFtpClient(key)) {
                    if (conn.FileExists(fileName)) throw new iPemException("文件已存在");
                    conn.Upload(current.InputStream, fileName, FluentFTP.FtpExists.Overwrite, true);
                }

                return Json(new AjaxResultModel { success = true, code = 200, message = "文件上传成功" });
            } catch (Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        public ActionResult Download(string key, string name) {
            try {
                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException("key");
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                byte[] file;
                using (FtpClient conn = this.GetFtpClient(key)) {
                    if (!conn.FileExists(name))
                        throw new iPemException("文件不存在");

                    conn.Download(out file, name);
                }

                if (file == null) 
                    throw new iPemException("文件下载失败");

                return File(file, "application/octet-stream", name);
            } catch (Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult Rename(string key, string oldname, string newname) {
            try {
                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException("key");
                if (string.IsNullOrWhiteSpace(oldname)) 
                    throw new ArgumentNullException("oldname");
                if (string.IsNullOrWhiteSpace(newname)) 
                    throw new ArgumentNullException("newname");

                using (FtpClient conn = this.GetFtpClient(key)) {
                    if (!conn.FileExists(oldname))
                        throw new iPemException(string.Format("\"{0}\"文件不存在", oldname));
                    if (conn.FileExists(newname))
                        throw new iPemException(string.Format("\"{0}\"文件已存在", newname));

                    conn.Rename(oldname, newname);
                }

                return Json(new AjaxResultModel { success = true, code = 200, message = "重命名成功" });
            } catch (Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult Delete(string key, string name) {
            try {
                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException("key");
                if (string.IsNullOrWhiteSpace(name)) 
                    throw new ArgumentNullException("name");

                using (FtpClient conn = this.GetFtpClient(key)) {
                    if (!conn.FileExists(name))
                        throw new iPemException(string.Format("\"{0}\"文件不存在", name));

                    conn.DeleteFile(name);
                }

                return Json(new AjaxResultModel { success = true, code = 200, message = "删除成功" });
            } catch (Exception exc) {
                return Json(new AjaxResultModel { success = false, code = 400, message = exc.Message });
            }
        }

        [AjaxAuthorize]
        public JsonResult GetUpgradeFiles(int start, int limit) {
            var data = new AjaxDataModel<List<ComboItem<string, string>>> {
                success = true,
                message = "No data",
                total = 0,
                data = new List<ComboItem<string, string>>()
            };

            try {
                var ftp = _ftpService.GetFtps(EnmFtp.Master).FirstOrDefault();
                if (ftp != null) {
                    ftp.Directory = "Update";
                    var files = this.GetFtpFiles(ftp);
                    foreach (var file in files.OrderByDescending(f => f.date)) {
                        data.data.Add(new ComboItem<string, string>() { id = file.name, text = file.name, comment = file.date });
                    }
                }

                if (data.data.Count > 0) {
                    data.total = data.data.Count;
                    data.message = "200 Ok";
                }
            } catch (Exception exc) {
                data.success = false;
                data.message = exc.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private List<FileModel> GetFtpFiles(string key, string name, bool cache) {
            var cachedKey = string.Format(GlobalCacheKeys.Ftp_Files_List, key);
            if (_cacheManager.IsSet(key) && !cache) _cacheManager.Remove(key);
            if (_cacheManager.IsSet(key)) return _cacheManager.GetItemsFromList<FileModel>(key).ToList();

            var files = new List<FileModel>();
            using (FtpClient conn = this.GetFtpClient(key)) {
                var index = 0;
                foreach (var item in conn.GetListing(conn.GetWorkingDirectory(), FtpListOption.Modify | FtpListOption.Size)) {
                    if (item.Type == FtpFileSystemObjectType.File) {
                        var created = CommonHelper.DateTimeConverter(item.Created);
                        var modified = CommonHelper.DateTimeConverter(item.Modified);
                        var file = new FileModel {
                            index = ++index,
                            name = item.Name,
                            size = string.Format("{0:N0} KB", item.Size / 1024),
                            type = CommonHelper.GetFileType(item.Name),
                            date = "--"
                        };

                        if (!string.IsNullOrWhiteSpace(created)) {
                            file.date = created;
                        } else if (!string.IsNullOrWhiteSpace(modified)) {
                            file.date = modified;
                        }

                        files.Add(file);
                    }
                }
            }

            return files;
        }

        private List<FileModel> GetFtpFiles(C_Ftp ftp) {
            var files = new List<FileModel>();
            using (FtpClient conn = new FtpClient()) {
                conn.Host = ftp.IP;
                conn.Port = ftp.Port;
                conn.Credentials = new NetworkCredential(ftp.User, ftp.Password);
                conn.SetWorkingDirectory(ftp.Directory);

                var index = 0;
                foreach (var item in conn.GetListing(conn.GetWorkingDirectory(), FtpListOption.Modify | FtpListOption.Size)) {
                    if (item.Type == FtpFileSystemObjectType.File) {
                        files.Add(new FileModel {
                            index = ++index,
                            name = item.Name,
                            size = string.Format("{0:N0} KB", item.Size / 1024),
                            type = CommonHelper.GetFileType(item.Name),
                            date = CommonHelper.DateTimeConverter(item.Modified)
                        });
                    }
                }
            }

            return files;
        }

        private FtpClient GetFtpClient(string key) {
            if (string.IsNullOrWhiteSpace(key)) 
                throw new ArgumentNullException("key");

            var cachedKey = string.Format(GlobalCacheKeys.Ftp_Info_Cfg, key);
            if (!_cacheManager.IsSet(cachedKey)) 
                throw new iPemException("未找到FTP信息");

            var ftp = _cacheManager.Get<C_Ftp>(cachedKey);
            FtpClient conn = new FtpClient();
            conn.Host = ftp.IP;
            conn.Port = ftp.Port;
            conn.Credentials = new NetworkCredential(ftp.User, ftp.Password);
            conn.SetWorkingDirectory(ftp.Directory);
            return conn;
        }

        private string GetFtpKey(C_Ftp ftp) {
            if (ftp == null) throw new ArgumentNullException("ftp");
           return string.Format("ftp://{0}:{1}/{2}", ftp.IP, ftp.Port, ftp.Directory);
        }

        #endregion

    }
}
