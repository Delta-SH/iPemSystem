using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Site.Models.SSH {
    [Serializable]
    public partial class SSHArea {
        public A_Area Current { get; set; }

        public List<SSHStation> Stations { get; set; }

        public List<SSHArea> Parents { get; private set; }

        public List<SSHArea> Children { get; private set; }

        public List<SSHArea> ChildRoot { get; private set; }

        public HashSet<string> Keys { get; private set; }

        public virtual bool HasParents {
            get { return (this.Parents.Count > 0); }
        }

        public virtual bool HasChildren {
            get { return (this.Children.Count > 0); }
        }

        public virtual void Initializer(List<SSHArea> entities) {
            this.Parents = new List<SSHArea>();
            this.Children = new List<SSHArea>();
            this.ChildRoot = new List<SSHArea>();
            this.Keys = new HashSet<string>();

            this.SetAreaParents(entities, this, this);
            this.SetAreaChildren(entities, this, this);
            this.ChildRoot = this.Children.FindAll(c => c.Current.ParentId == this.Current.Id);
            this.Keys.Add(this.Current.Id);
            foreach(var child in this.Children) {
                this.Keys.Add(child.Current.Id);
            }
        }

        public virtual string[] ToPath() {
            var paths = new List<string>();
            if(this.HasParents)
                paths.AddRange(this.Parents.Select(p => p.Current.Id));

            if(this.Current != null)
                paths.Add(this.Current.Id);

            return paths.ToArray();
        }

        public override string ToString() {
            if(this.Current == null)
                return null;

            if(!this.HasParents)
                return this.Current.Name;

            return string.Format("{0},{1}", string.Join(",", this.Parents.Select(p => p.Current.Name)), this.Current.Name);
        }

        private void SetAreaParents(List<SSHArea> source, SSHArea target, SSHArea current) {
            var parent = source.Find(a => a.Current.Id == current.Current.ParentId);
            if(parent != null) {
                SetAreaParents(source, target, parent);
                target.Parents.Add(parent);
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