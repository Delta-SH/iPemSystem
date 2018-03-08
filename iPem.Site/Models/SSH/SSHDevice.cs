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
        private readonly Lazy<List<D_SimpleSignal>> _ai;
        private readonly Lazy<List<D_SimpleSignal>> _ao;
        private readonly Lazy<List<D_SimpleSignal>> _di;
        private readonly Lazy<List<D_SimpleSignal>> _do;
        private readonly Lazy<List<D_SimpleSignal>> _al;

        public SSHDevice() {
            //延迟加载属性
            this._signals = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetSimpleSignalsInDevice(this.Current.Id);
            });
            this._ai = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetSimpleSignalsInDevice(this.Current.Id, true, false, false, false, false);
            });
            this._ao = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetSimpleSignalsInDevice(this.Current.Id, false, true, false, false, false);
            });
            this._di = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetSimpleSignalsInDevice(this.Current.Id, false, false, true, false, false);
            });
            this._do = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetSimpleSignalsInDevice(this.Current.Id, false, false, false, true, false);
            });
            this._al = new Lazy<List<D_SimpleSignal>>(() => {
                return EngineContext.Current.Resolve<ISignalService>().GetSimpleSignalsInDevice(this.Current.Id, false, false, false, false, true);
            });
        }

        public D_Device Current { get; set; }

        [JsonIgnore]
        public List<D_SimpleSignal> Signals {
            get { return this._signals.Value; }
        }

        [JsonIgnore]
        public List<D_SimpleSignal> AI {
            get { return this._ai.Value; }
        }

        [JsonIgnore]
        public List<D_SimpleSignal> AO {
            get { return this._ao.Value; }
        }

        [JsonIgnore]
        public List<D_SimpleSignal> DI {
            get { return this._di.Value; }
        }

        [JsonIgnore]
        public List<D_SimpleSignal> DO {
            get { return this._do.Value; }
        }

        [JsonIgnore]
        public List<D_SimpleSignal> AL {
            get { return this._al.Value; }
        }
    }
}