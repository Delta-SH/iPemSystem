using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;

namespace iPem.Core.NPOI {
    /// <summary>
    /// Represents a manager for excel operating.
    /// </summary>
    public partial class ExcelManager : IExcelManager {

        public MemoryStream Export<T>(List<T> data, string title = "No title", string subtitle = "No subtitle") {
            var converter = new ExcelConverter();
            var worksheet = converter.CreateSheet();

            PropertyInfo backgroundAttribute = null;
            var colorAttributes = new List<string>();
            var boolAttributes = new List<IdValuePair<PropertyInfo, ExcelBooleanNameAttribute>>();
            var dataAttributes = new List<IdValuePair<PropertyInfo, string>>();

            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach(var prop in props) {
                if(prop.IsDefined(typeof(ExcelIgnoreAttribute), true))
                    continue;

                if(prop.IsDefined(typeof(ExcelBackgroundAttribute), true)) {
                    if(prop.PropertyType == typeof(Color))
                        backgroundAttribute = prop;

                    continue;
                }

                if(prop.IsDefined(typeof(ExcelColorAttribute), true)) {
                    colorAttributes.Add(prop.Name);
                }

                if(prop.IsDefined(typeof(ExcelBooleanNameAttribute), true)) {
                    if(prop.PropertyType == typeof(Boolean)) {
                        var boolean = (ExcelBooleanNameAttribute)prop.GetCustomAttributes(typeof(ExcelBooleanNameAttribute), true)[0];
                        boolAttributes.Add(new IdValuePair<PropertyInfo, ExcelBooleanNameAttribute> { Id = prop, Value = boolean });
                    }
                }

                if(prop.IsDefined(typeof(ExcelDisplayNameAttribute), true)) {
                    var display = (ExcelDisplayNameAttribute)prop.GetCustomAttributes(typeof(ExcelDisplayNameAttribute), true)[0];
                    dataAttributes.Add(new IdValuePair<PropertyInfo, string>(prop, display.DisplayName));
                } else {
                    dataAttributes.Add(new IdValuePair<PropertyInfo, string>(prop, prop.Name));
                }
            }

            var ttrow = worksheet.CreateRow(0); 
                ttrow.HeightInPoints = 30;
            for(var i = 0; i < dataAttributes.Count; i++) {
                var ttcell = ttrow.CreateCell(i);

                ttcell.SetCellValue(new XSSFRichTextString(i == 0 ? title : ""));
                ttcell.CellStyle = converter.Title;
            }
            worksheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dataAttributes.Count - 1));

            var strow = worksheet.CreateRow(1); 
                strow.HeightInPoints = 20;
            for(var i = 0; i < dataAttributes.Count; i++) {
                var stcell = strow.CreateCell(i);

                stcell.SetCellValue(new XSSFRichTextString(i == 0 ? subtitle : ""));
                stcell.CellStyle = converter.SubTitle;
            }
            worksheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, dataAttributes.Count - 1));

            var ctrow = worksheet.CreateRow(2); 
                ctrow.HeightInPoints = 20;
            for(var i = 0; i < dataAttributes.Count; i++) {
                var cell = ctrow.CreateCell(i);
                cell.SetCellValue(dataAttributes[i].Value);
                cell.CellStyle = converter.CellTitle;

                //set cell column width
                worksheet.SetColumnWidth(i, 25 * 150);
            }

            for(int k = 0; k < data.Count; k++) {
                var row = (XSSFRow)worksheet.CreateRow(3 + k); 
                    row.HeightInPoints = 18;

                var background = Color.Empty;
                if(backgroundAttribute != null) 
                    background = (Color)backgroundAttribute.GetValue(data[k]);

                for(int g = 0; g < dataAttributes.Count; g++) {
                    var cell = row.CreateCell(g);
                    var prop = dataAttributes[g].Id;
                    var draw = colorAttributes.Contains(prop.Name);
                    var name = prop.Name;
                    var type = prop.PropertyType;
                    var value = prop.GetValue(data[k]);

                    if(value == null) {
                        cell.CellStyle = converter.GetCellStyle(background, draw);
                        continue;
                    }

                    if(type == typeof(Int16) ||
                        type == typeof(Int32) ||
                        type == typeof(Int64) ||
                        type == typeof(Single) ||
                        type == typeof(Double)) {
                        cell.CellStyle = converter.GetCellStyle(background, draw);
                        cell.SetCellValue(Convert.ToDouble(value));
                    } else if(type == typeof(Boolean)) {
                        cell.CellStyle = converter.GetCellStyle(background, draw);

                        var boolAttribute = boolAttributes.Find(b => b.Id.Equals(prop));
                        if(boolAttribute != null)
                            cell.SetCellValue(new XSSFRichTextString((Boolean)value ? boolAttribute.Value.True : boolAttribute.Value.False));
                        else
                            cell.SetCellValue((Boolean)value);
                    } else if(type == typeof(DateTime)) {
                        cell.CellStyle = converter.GetDateCellStyle(background, draw);
                        cell.SetCellValue((DateTime)value);
                    } else {
                        cell.CellStyle = converter.GetCellStyle(background, draw);
                        cell.SetCellValue(new XSSFRichTextString(value.ToString()));
                    }
                }
            }

            var ms = new MemoryStream();
            converter.Workbook.Write(ms);
            return ms;
        }

        public T Import<T>(string fullPath) {
            throw new NotImplementedException();
        }

        public void Save<T>(List<T> data, string fullPath, string title = "No title", string subtitle = "No subtitle") {
            using(var ms = Export<T>(data, title, subtitle)) {
                using(var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write)) {
                    ms.WriteTo(fs);
                    fs.Flush();
                }
            }
        }

        public void Send<T>(List<T> data, HttpContextBase httpContext, string fileName = "", string title = "No title", string subtitle = "No subtitle") {
            if(string.IsNullOrWhiteSpace(fileName))
                fileName = this.RandomFileName;
            else {
                fileName = fileName.Trim();
                if(!fileName.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase)) fileName = string.Format("{0}.xlsx", fileName);
            }

            using(var ms = Export<T>(data, title, subtitle)) {
                if(!httpContext.Response.Buffer) {
                    httpContext.Response.Buffer = true;
                    httpContext.Response.Clear();
                }

                httpContext.Response.ContentType = this.ContentType;
                httpContext.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
                httpContext.Response.Flush();
                httpContext.Response.BinaryWrite(ms.ToArray());
            }
        }

        public string ContentType {
            get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public string RandomFileName {
            //get { return string.Format("{0}.xlsx", Guid.NewGuid().ToString("N").ToLower()); }
            get { return string.Format("{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssff")); }
        }
    }
}
