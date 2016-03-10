using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Core {
    public class StationAttributes {
        public StationAttributes(List<Station> source, Station current) {
            this.Current = current;
            this.Parents = new List<Station>();
            this.Children = new List<Station>();
            this.SetParents(source, current);
            this.SetChildren(source, current);
            this.FirstChildren = this.Children.FindAll(c => c.ParentId == current.Id);
        }

        public Station Current { get; set; }

        public List<Station> Parents { get; private set; }

        public List<Station> Children { get; private set; }

        public List<Station> FirstChildren { get; private set; }

        public virtual bool HasParents {
            get { return (this.Parents.Count > 0); }
        }

        public virtual bool HasChildren {
            get { return (this.Children.Count > 0); }
        }

        private void SetParents(List<Station> source, Station current) {
            var parent = source.Find(a => a.Id == current.ParentId);
            if(parent != null) {
                SetParents(source, parent);
                this.Parents.Add(parent);
            }
        }

        private void SetChildren(List<Station> source, Station current) {
            var children = source.FindAll(a => a.ParentId == current.Id);
            if(children.Count > 0) {
                this.Children.AddRange(children);
                foreach(var child in children) {
                    SetChildren(source, child);
                }
            }
        }
    }
}