using iPem.Core.Domain.Rs;
using iPem.Services.Rs;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHCamera {
        private readonly Lazy<List<V_Channel>> _channels;

        public SSHCamera() {
            //延迟加载属性
            this._channels = new Lazy<List<V_Channel>>(() => {
                return EngineContext.Current.Resolve<IChannelService>().GetChannels(this.Current.Id);
            });
        }

        public V_Camera Current { get; set; }

        [JsonIgnore]
        public List<V_Channel> Channels {
            get { return this._channels.Value; }
        }
    }
}