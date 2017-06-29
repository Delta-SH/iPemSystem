using iPem.Core.NPOI;
using System;

namespace iPem.Site.Models {
    [Serializable]
    public class FtpFileModel {
        [ExcelDisplayName("序号")]
        public int index { get; set; }

        [ExcelDisplayName("文件名称")]
        public string name { get; set; }

        [ExcelDisplayName("文件大小")]
        public long size { get; set; }

        [ExcelDisplayName("创建时间")]
        public string time { get; set; }

        [ExcelIgnore]
        public string path { get; set; }
    }
}