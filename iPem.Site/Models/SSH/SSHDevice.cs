using iPem.Core.Domain.Rs;
using iPem.Services.Rs;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHDevice {
        private readonly Lazy<List<P_Point>> _points;

        public SSHDevice() {
            //延迟加载属性
            this._points = new Lazy<List<P_Point>>(() => {
                return EngineContext.Current.Resolve<IPointService>().GetPointsInDevice(this.Current.Id);
            });
        }

        public D_Device Current { get; set; }

        [JsonIgnore]
        public List<P_Point> Points {
            get { return this._points.Value; }
        }
    }
}