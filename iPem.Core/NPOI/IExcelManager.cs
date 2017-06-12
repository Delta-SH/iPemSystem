using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace iPem.Core.NPOI {
    /// <summary>
    /// Excel管理器接口
    /// </summary>
    public interface IExcelManager {
        MemoryStream Export<T>(List<T> data, string title = "No title", string subtitle = "No subtitle");

        MemoryStream Export(DataTable data, string title = "No title", string subtitle = "No subtitle");

        void Save<T>(List<T> data, string fullPath, string title = "No title", string subtitle = "No subtitle");

        void Save(DataTable data, string fullPath, string title = "No title", string subtitle = "No subtitle");

        void Send<T>(List<T> data, HttpContextBase httpContext, string fileName, string title = "No title", string subtitle = "No subtitle");

        void Send(DataTable data, HttpContextBase httpContext, string fileName = "", string title = "No title", string subtitle = "No subtitle");

        string ContentType { get; }

        string RandomFileName { get; }
    }
}
