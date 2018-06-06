using iPem.Core.Domain.Rs;
using iPem.Services.Rs;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHDevice {
        private readonly Lazy<List<D_SimpleSignal>> _signals;

        public SSHDevice() {
            //延迟加载属性
            this._signals = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetAllSignals(this.Current.Id);
            });
        }

        public D_Device Current { get; set; }

        [JsonIgnore]
        public List<D_SimpleSignal> Signals {
            get { return this._signals.Value; }
        }
    }
}