using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHStation {
        private readonly Lazy<List<S_Room>> _rooms;
        private readonly Lazy<List<D_Device>> _devices;

        public SSHStation() {
            //延迟加载属性
            this._rooms = new Lazy<List<S_Room>>(() => {
                return EngineContext.Current.Resolve<IRoomService>().GetRoomsInStation(this.Current.Id);
            });

            this._devices = new Lazy<List<D_Device>>(() => {
                return EngineContext.Current.Resolve<IDeviceService>().GetDevicesInStation(this.Current.Id);
            });
        }

        public S_Station Current { get; set; }

        [JsonIgnore]
        public List<S_Room> Rooms {
            get { return this._rooms.Value; }
        }

        [JsonIgnore]
        public List<D_Device> Devices {
            get { return this._devices.Value; }
        }
    }
}