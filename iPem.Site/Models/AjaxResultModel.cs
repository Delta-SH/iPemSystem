using System;

namespace iPem.Site.Models {
    [Serializable]
    public class AjaxResultModel {
        public bool success { get; set; }

        public int code { get; set; }

        public string message { get; set; }
    }
}