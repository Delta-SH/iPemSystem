using System;

namespace iPem.Site.Models {
    [Serializable]
    public class GviewModel {
        public string parentId { get; set; }

        public int parentType { get; set; }

        public string parentName { get; set; }

        public string currentId { get; set; }

        public int currentType { get; set; }

        public string currentName { get; set; }
    }
}