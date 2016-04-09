using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Site.Infrastructure {
    public class AreaAttributes {
        public AreaAttributes(List<Area> source, Area current) {
            this.Current = current;
            this.Parents = new List<Area>();
            this.Children = new List<Area>();
            this.SetParents(source, current);
            this.SetChildren(source, current);
            this.FirstChildren = this.Children.FindAll(c => c.ParentId == current.AreaId);
        }

        public Area Current { get; set; }

        public List<Area> Parents { get; private set; }

        public List<Area> Children { get; private set; }

        public List<Area> FirstChildren { get; private set; }

        public virtual bool HasParents {
            get { return (this.Parents.Count > 0); }
        }

        public virtual bool HasChildren {
            get { return (this.Children.Count > 0); }
        }

        private void SetParents(List<Area> source, Area current) {
            var parent = source.Find(a => a.AreaId == current.ParentId);
            if(parent != null) {
                SetParents(source, parent);
                this.Parents.Add(parent);
            }
        }

        private void SetChildren(List<Area> source, Area current) {
            var children = source.FindAll(a => a.ParentId == current.AreaId);
            if(children.Count > 0) {
                this.Children.AddRange(children);
                foreach(var child in children) {
                    SetChildren(source, child);
                }
            }
        }
    }
}