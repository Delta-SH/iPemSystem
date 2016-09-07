using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    public class ProfileValues {
        public List<RssPoint> RssPoints { get; set; }

        public HashSet<string> ToRssHashSet() {
            var hashset = new HashSet<string>();
            foreach(var rss in this.RssPoints) {
                var key = string.Format("{0}-{1}", rss.device, rss.point);
                hashset.Add(key);
            }
            return hashset;
        }
    }
}