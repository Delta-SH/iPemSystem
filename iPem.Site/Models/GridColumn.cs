using System;

namespace iPem.Site.Models {
    [Serializable]
    public class GridColumn {
        public string name { get; set; }

        public string type { get; set; }

        public string column { get; set; }

        public int width { get; set; }
    }
}