using iPem.Core.Domain.Rs;
using iPem.Services.Rs;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHFsu {
        private readonly Lazy<List<D_Device>> _devices;

        public SSHFsu() {
            //延迟加载属性
            this._devices = new Lazy<List<D_Device>>(() => {
                return EngineContext.Current.Resolve<IDeviceService>().GetDevicesInFsu(this.Current.Id);
            });
        }

        public D_Fsu Current { get; set; }

        [JsonIgnore]
        public List<D_Device> Devices {
            get { return this._devices.Value; }
        }
    }
}