using System;

namespace iPem.Core {
    public static class FileType {
        public static Kv<string, string[]> ZIP {
            get { return new Kv<string, string[]>("压缩文件", new string[] { ".zip", ".rar", ".7z", ".gz", ".tar", ".jar" }); }
        }

        public static Kv<string, string[]> IMG {
            get { return new Kv<string, string[]>("图片文件", new string[] { ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".ico" }); }
        }

        public static Kv<string, string[]> MUSIC {
            get { return new Kv<string, string[]>("音乐文件", new string[] { ".mp3", ".wav", ".wma", ".ogg", ".m4a", ".mmf", ".amr" }); }
        }

        public static Kv<string, string[]> VIDEO {
            get { return new Kv<string, string[]>("视频文件", new string[] { ".mp4", ".3gp", ".mpg", ".avi", ".wmv", ".flv", ".swf" }); }
        }

        public static Kv<string, string[]> WORD {
            get { return new Kv<string, string[]>("Word文档", new string[] { ".doc", ".docx", ".wps", ".rtf" }); }
        }

        public static Kv<string, string[]> EXCEL {
            get { return new Kv<string, string[]>("Excel工作表", new string[] { ".xls", ".xlsx", ".et", ".csv" }); }
        }

        public static Kv<string, string[]> POWER {
            get { return new Kv<string, string[]>("PowerPoint幻灯片", new string[] { ".ppt", ".pptx", ".dps" }); }
        }

        public static Kv<string, string[]> EXE {
            get { return new Kv<string, string[]>("安装包文件", new string[] { ".exe", ".msi", ".cmd", ".bat" }); }
        }

        public static Kv<string, string[]> PDF {
            get { return new Kv<string, string[]>("PDF文档", new string[] { ".pdf" }); }
        }

        public static Kv<string, string[]> TXT {
            get { return new Kv<string, string[]>("文本文档", new string[] { ".txt", ".log", ".cfg", ".config", ".sql" }); }
        }

        public static Kv<string, string[]> XML {
            get { return new Kv<string, string[]>("编程文档", new string[] { ".xml", ".js", ".css", ".cs" }); }
        }

        public static Kv<string, string[]> HTML {
            get { return new Kv<string, string[]>("HTML文档", new string[] { ".htm", ".html", ".shtml", ".shtm", ".cshtml" }); }
        }

        public static Kv<string, string[]> Undefined {
            get { return new Kv<string, string[]>("未知类型", new string[] { ".*" }); }
        }
    }
}
