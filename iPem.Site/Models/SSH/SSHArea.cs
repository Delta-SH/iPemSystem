using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Services.Rs;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHArea {
        private readonly Lazy<HashSet<string>> _keys;
        private readonly Lazy<List<SSHArea>> _roots;
        private readonly Lazy<List<S_Station>> _stations;

        public SSHArea() {
            //延迟加载属性
            this._keys = new Lazy<HashSet<string>>(() => {
                var __keys = new HashSet<string>();
                __keys.Add(this.Current.Id);
                foreach (var child in this.Children) {
                    __keys.Add(child.Current.Id);
                }

                return __keys;
            });
            this._roots = new Lazy<List<SSHArea>>(() => {
                if (!this.HasChildren) return new List<SSHArea>();
                return this.Children.FindAll(c => c.Current.ParentId == this.Current.Id);
            });
            this._stations = new Lazy<List<S_Station>>(() => {
                if(this.HasChildren) return new List<S_Station>();
                return EngineContext.Current.Resolve<IStationService>().GetStationsInArea(this.Current.Id);
            });
        }

        public A_Area Current { get; set; }

        public List<A_Area> Parents { get; set; }

        public List<SSHArea> Children { get; set; }

        [JsonIgnore]
        public HashSet<string> Keys {
            get { return this._keys.Value; }
        }

        [JsonIgnore]
        public List<SSHArea> ChildRoot {
            get { return this._roots.Value; }
        }

        [JsonIgnore]
        public List<S_Station> Stations {
            get { return this._stations.Value; }
        }

        [JsonIgnore]
        public bool HasParents {
            get { return (this.Parents.Count > 0); }
        }

        [JsonIgnore]
        public bool HasChildren {
            get { return (this.Children.Count > 0); }
        }

        public virtual void Initializer(List<SSHArea> entities) {
            this.Parents = new List<A_Area>();
            this.Children = new List<SSHArea>();
            this.SetAreaParents(entities, this, this);
            this.SetAreaChildren(entities, this, this);
        }

        public virtual string[] ToPath() {
            var paths = new List<string>();
            if(this.HasParents)
                paths.AddRange(this.Parents.Select(p => p.Id));

            if(this.Current != null)
                paths.Add(this.Current.Id);

            return paths.ToArray();
        }

        public override string ToString() {
            if(this.Current == null)
                return null;

            if(!this.HasParents)
                return this.Current.Name;

            return string.Format("{0},{1}", string.Join(",", this.Parents.Select(p => p.Name)), this.Current.Name);
        }

        private void SetAreaParents(List<SSHArea> source, SSHArea target, SSHArea current) {
            var parent = source.Find(a => a.Current.Id == current.Current.ParentId);
            if(parent != null) {
                SetAreaParents(source, target, parent);
                target.Parents.Add(parent.Current);
            }
        }

        private void SetAreaChildren(List<SSHArea> source, SSHArea target, SSHArea current) {
            var children = source.FindAll(a => a.Current.ParentId == current.Current.Id);
            if(children.Count > 0) {
                target.Children.AddRange(children);
                foreach(var child in children) {
                    SetAreaChildren(source, target, child);
                }
            }
        }
    }
}