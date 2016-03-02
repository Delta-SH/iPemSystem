using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class AjaxDataModel<T> {
        public bool success { get; set; }

        public string message { get; set; }

        public int total { get; set; }

        public T data { get; set; }
    }
}