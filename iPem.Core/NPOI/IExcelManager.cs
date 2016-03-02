using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace iPem.Core.NPOI {
    /// <summary>
    /// Excel manager interface
    /// </summary>
    public interface IExcelManager {
        MemoryStream Export<T>(List<T> data, string title = "No title", string subtitle = "No subtitle");

        T Import<T>(string fullPath);

        void Save<T>(List<T> data, string fullPath, string title = "No title", string subtitle = "No subtitle");

        void Send<T>(List<T> data, HttpContextBase httpContext, string fileName, string title = "No title", string subtitle = "No subtitle");

        string ContentType { get; }

        string RandomFileName { get; }
    }
}
