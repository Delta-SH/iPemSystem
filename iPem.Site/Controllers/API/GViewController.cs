using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using iPem.Site.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iPem.Site.Controllers {
    public class GViewController : ApiController {

        #region Fields

        private readonly IApiWorkContext _workContext;
        private readonly IStationService _stationService;
        private readonly IRoomService _roomService;
        private readonly IFsuService _fsuService;
        private readonly IDeviceService _deviceService;
        private readonly IPointService _pointService;
        private readonly IGImageService _gimageService;
        private readonly IGPageService _gpageService;
        private readonly IGTemplateService _gtemplateService;

        #endregion

        #region Ctor

        public GViewController(
            IApiWorkContext workContext,
            IStationService stationService,
            IRoomService roomService,
            IFsuService fsuService,
            IDeviceService deviceService,
            IPointService pointService,
            IGImageService gimageService,
            IGPageService gpageService,
            IGTemplateService gtemplateService) {
            this._workContext = workContext;
            this._stationService = stationService;
            this._roomService = roomService;
            this._fsuService = fsuService;
            this._deviceService = deviceService;
            this._pointService = pointService;
            this._gimageService = gimageService;
            this._gpageService = gpageService;
            this._gtemplateService = gtemplateService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public List<String> GetGVPageNames(string role, string id, int type) {
            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                return _gpageService.GetNames((int)EnmAPISCObj.Device == type ? U_Role.SuperId.ToString() : role, id, type);
            } catch (Exception exc) {
            }

            return new List<String>();
        }

        [HttpGet]
        public List<API_GV_Page> GetGVPages(string role, string id, int type) {
            var data = new List<API_GV_Page>();

            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                foreach (var page in _gpageService.GetPages((int)EnmAPISCObj.Device == type ? U_Role.SuperId.ToString() : role, id, type)) {
                    data.Add(new API_GV_Page {
                        Name = page.Name,
                        IsHome = page.IsHome,
                        Content = page.Content,
                        SCObjID = page.ObjId,
                        SCObjType = page.ObjType
                    });
                }
            } catch (Exception exc) {
            }

            return data;
        }

        [HttpGet]
        public API_GV_Page GetGVPage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                var page = _gpageService.GetPage(name);
                if (page != null) {
                    return new API_GV_Page {
                        Name = page.Name,
                        IsHome = page.IsHome,
                        Content = page.Content,
                        SCObjID = page.ObjId,
                        SCObjType = page.ObjType
                    };
                }
            } catch (Exception exc) {
            }

            return null;
        }

        [HttpGet]
        public Boolean CheckGVPageName(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                return _gpageService.Exist(name);
            } catch (Exception exc) {
            }

            return true;
        }

        [HttpPost]
        public String SaveGVPage(string role, [FromBody]API_GV_Page page) {
            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (page == null)
                    throw new ArgumentNullException("value");

                var target = new G_Page {
                    RoleId = (int)EnmAPISCObj.Device == page.SCObjType ? U_Role.SuperId.ToString() : role,
                    Name = page.Name,
                    IsHome = page.IsHome,
                    Content = page.Content,
                    ObjId = page.SCObjID,
                    ObjType = page.SCObjType
                };

                if (_gpageService.Exist(page.Name)) {
                    _gpageService.Update(target);
                } else {
                    _gpageService.Add(target);
                }
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpGet]
        public List<String> GetGVTemplateNames() {
            try {
                return _gtemplateService.GetNames();
            } catch (Exception exc) {
            }

            return new List<String>();
        }

        [HttpGet]
        public API_GV_Template GetGVTemplate(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                var template = _gtemplateService.GetTemplate(name);
                if (template != null) {
                    return new API_GV_Template {
                        Name = template.Name,
                        Content = template.Content
                    };
                }
            } catch (Exception exc) {
            }

            return null;
        }

        [HttpGet]
        public Boolean CheckGVTemplateName(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                return _gtemplateService.Exist(name);
            } catch (Exception exc) {
            }

            return true;
        }

        [HttpPost]
        public String SaveGVTemplate([FromBody]API_GV_Template template) {
            try {
                if (template == null)
                    throw new ArgumentNullException("template");

                var target = new G_Template {
                    Name = template.Name,
                    Content = template.Content
                };

                if (_gtemplateService.Exist(template.Name)) {
                    _gtemplateService.Update(target);
                } else {
                    _gtemplateService.Add(target);
                }
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String ClearGVTemplates() {
            try {
                _gtemplateService.Clear();
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpGet]
        public List<API_GV_ImageInfo> GetGVImageInfos() {
            var data = new List<API_GV_ImageInfo>();

            try {
                foreach (var image in _gimageService.GetNames()) {
                    data.Add(new API_GV_ImageInfo {
                        Name = image.Name,
                        Type = image.Type,
                        UpdateMark = image.UpdateMark
                    });
                }
            } catch (Exception exc) {
            }

            return data;
        }

        [HttpGet]
        public List<API_GV_Image> GetMinGVImages() {
            var data = new List<API_GV_Image>();

            try {
                foreach (var image in _gimageService.GetThumbnails()) {
                    data.Add(new API_GV_Image {
                        Name = image.Name,
                        Type = image.Type,
                        Content = CommonHelper.BytesToString(image.Thumbnail)
                    });
                }
            } catch (Exception exc) {
            }

            return data;
        }

        [HttpPost]
        public List<API_GV_Image> GetGVImages([FromBody]List<string> names) {
            var data = new List<API_GV_Image>();

            try {
                if (names == null)
                    throw new ArgumentNullException("names");

                foreach (var image in _gimageService.GetContents(names)) {
                    data.Add(new API_GV_Image {
                        Name = image.Name,
                        Type = image.Type,
                        Content = CommonHelper.BytesToString(image.Content)
                    });
                }
            } catch (Exception exc) {
            }

            return data;
        }

        [HttpGet]
        public API_GV_Image GetGVImage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                var image = _gimageService.GetImage(name);
                if (image != null) {
                    return new API_GV_Image {
                        Name = image.Name,
                        Type = image.Type,
                        Content = CommonHelper.BytesToString(image.Content)
                    };
                }
            } catch (Exception exc) {
            }

            return null;
        }

        [HttpGet]
        public Boolean CheckGVImageName(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                return _gimageService.Exist(name);
            } catch (Exception exc) {
            }

            return true;
        }

        [HttpPost]
        public String SaveGVImage([FromBody]API_GV_ImagePair image) {
            try {
                if (image == null)
                    throw new ArgumentNullException("image");

                var target = new G_Image {
                    Name = image.Name,
                    Type = image.Type,
                    Content = CommonHelper.StringToBytes(image.Source),
                    Thumbnail = CommonHelper.StringToBytes(image.Min),
                    UpdateMark = image.UpdateMark
                };

                if (_gimageService.Exist(image.Name)) {
                    _gimageService.Update(target);
                } else {
                    _gimageService.Add(target);
                }
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String DeleteGVImage(string name) {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name");

                _gimageService.Remove(name);
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpDelete]
        public String ClearGVImages() {
            try {
                _gimageService.Clear();
            } catch (Exception exc) {
                return exc.Message;
            }

            return null;
        }

        [HttpGet]
        public List<API_GV_SCObj> GetSubSCObjs(string role, string id, int type) {
            var data = new List<API_GV_SCObj>();
 
            try {
                if (string.IsNullOrWhiteSpace(role))
                    throw new ArgumentNullException("role");

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException("id");

                if ((int)EnmAPISCObj.Area == type) {
                    var current = _workContext.GetAreas(role).Find(a => a.Current.Id.Equals(id));
                    if (current != null) {
                        if (current.HasChildren) {
                            foreach (var child in current.ChildRoot) {
                                data.Add(new API_GV_SCObj {
                                    ID = child.Current.Id,
                                    Name = child.Current.Name,
                                    Type = (int)EnmAPISCObj.Area
                                });
                            }
                        } else {
                            var stations = _workContext.GetStations(role).FindAll(s => s.AreaId.Equals(id));
                            foreach (var child in stations) {
                                data.Add(new API_GV_SCObj {
                                    ID = child.Id,
                                    Name = child.Name,
                                    Type = (int)EnmAPISCObj.Station
                                });
                            }
                        }
                    }
                } else if ((int)EnmAPISCObj.Station == type) {
                    var rooms = _workContext.GetRooms(role).FindAll(s => s.StationId.Equals(id));
                    foreach (var child in rooms) {
                        data.Add(new API_GV_SCObj {
                            ID = child.Id,
                            Name = child.Name,
                            Type = (int)EnmAPISCObj.Room
                        });
                    }
                } else if ((int)EnmAPISCObj.Room == type) {
                    var devices = _workContext.GetDevices(role).FindAll(s => s.RoomId.Equals(id));
                    foreach (var child in devices) {
                        data.Add(new API_GV_SCObj {
                            ID = child.Id,
                            Name = child.Name,
                            Type = (int)EnmAPISCObj.Device
                        });
                    }
                } else if ((int)EnmAPISCObj.Device == type) {
                    var points = _pointService.GetPointsInDevice(id);
                    foreach (var child in points) {
                        data.Add(new API_GV_SCObj {
                            ID = Common.JoinKeys(id, child.Id),
                            Name = child.Name,
                            Type = (int)EnmAPISCObj.Signal
                        });
                    }
                }
            } catch (Exception exc) {
            }

            return data;
        }

        [HttpPost]
        public List<API_GV_SCValue> GetSCValues(string role, [FromBody]List<API_GV_Key> keys) {
        }

        [HttpPost]
        public String OpSignal([FromBody]API_GV_OpSignal signal) {
        }

        #endregion

    }
}