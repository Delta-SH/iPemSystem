using iPem.Core.Domain.Rs;
using iPem.Services.Rs;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHRoom {
        private readonly Lazy<List<D_Fsu>> _fsus;
        private readonly Lazy<List<D_Device>> _devices;

        public SSHRoom() {
            //延迟加载属性
            this._fsus = new Lazy<List<D_Fsu>>(() => {
                return EngineContext.Current.Resolve<IFsuService>().GetFsusInRoom(this.Current.Id);
            });
            this._devices = new Lazy<List<D_Device>>(() => {
                return EngineContext.Current.Resolve<IDeviceService>().GetDevicesInRoom(this.Current.Id);
            });
        }

        public S_Room Current { get; set; }

        [JsonIgnore]
        public List<D_Fsu> Fsus {
            get { return this._fsus.Value; }
        }

        [JsonIgnore]
        public List<D_Device> Devices {
            get { return this._devices.Value; }
        }
    }
}