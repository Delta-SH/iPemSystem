using System;

namespace iPem.Site.Models {
    [Serializable]
    public class GridColumn {
        public GridColumn() {
            this.width = 100;
            this.align = "left";
            this.detail = false;
        }

        public string name { get; set; }

        public string type { get; set; }

        public string column { get; set; }

        public int width { get; set; }

        public string align { get; set; }

        public bool detail { get; set; }
    }
}