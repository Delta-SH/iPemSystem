using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400206 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("类型")]
        public string type { get; set; }

        [ExcelDisplayName("名称")]
        public string name { get; set; }

        [ExcelDisplayName("预约数量")]
        public int count { get; set; }

        [ExcelDisplayName("预约时长")]
        public string interval { get; set; }

        [ExcelIgnore]
        public List<ReservationModel> appointments { get; set; }
    }
}