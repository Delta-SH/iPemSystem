using iPem.Core.NPOI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    [Serializable]
    public class Model400206 {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("所属区域")]
        public string area { get; set; }

        [ExcelIgnore]
        public string stationid { get; set; }

        [ExcelDisplayName("所属站点")]
        public string station { get; set; }

        [ExcelDisplayName("预约数量")]
        public int count { get; set; }

        [ExcelDisplayName("预约时长")]
        public string interval { get; set; }

        [ExcelIgnore]
        public List<ReservationModel> reservations { get; set; }
    }
}