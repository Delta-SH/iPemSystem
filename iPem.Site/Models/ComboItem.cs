using System;

namespace iPem.Site.Models {
    [Serializable]
    public class ComboItem<T1, T2> {
        public T1 id { get; set; }

        public T2 text { get; set; }

        public string comment { get; set; }
    }
}