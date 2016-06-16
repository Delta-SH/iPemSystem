using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace iPem.Core.NPOI {
    public class ExcelConverter {

        #region Field 
        private readonly XSSFWorkbook _workbook;
        private readonly Dictionary<int,XSSFSheet> _worksheets;

        private readonly XSSFColor _ffffff;
        private readonly XSSFColor _fafafa;
        private readonly XSSFColor _f5f5f5;
        private readonly XSSFColor _c0c0c0;
        private readonly XSSFColor _666666;
        private readonly XSSFColor _157fcc;
        private readonly XSSFColor _000000;

        
        private XSSFCellStyle _default;
        private XSSFCellStyle _title;
        private XSSFCellStyle _subtitle;
        private XSSFCellStyle _celltitle;
        private XSSFCellStyle _date;
        private XSSFCellStyle _red;
        private XSSFCellStyle _orange;
        private XSSFCellStyle _yellow;
        private XSSFCellStyle _skyblue;
        private XSSFCellStyle _blue;
        private XSSFCellStyle _limegreen;
        private XSSFCellStyle _lightgray;
        private XSSFCellStyle _datewithred;
        private XSSFCellStyle _datewithorange;
        private XSSFCellStyle _datewithyellow;
        private XSSFCellStyle _datewithskyblue;
        private XSSFCellStyle _datewithblue;
        private XSSFCellStyle _datewithlimegreen;
        private XSSFCellStyle _datewithlightgray;
        private XSSFFont _font;
        private IDataFormat _format;

        #endregion

        #region Ctor

        public ExcelConverter() {
            this._workbook = new XSSFWorkbook();
            var props = _workbook.GetProperties();
            props.CoreProperties.Title = "iPemSystem";
            props.CoreProperties.Subject = "iPemSystem";
            props.CoreProperties.Creator = "Steven H";
            props.CoreProperties.Created = DateTime.Now;
            props.CoreProperties.Description = "Delta GreenTech(China) Co., Ltd. All Rights Reserved";

            this._worksheets = new Dictionary<int, XSSFSheet>();

            this._ffffff = new XSSFColor(new byte[3] { 254, 254, 254 });
            this._fafafa = new XSSFColor(new byte[3] { 249, 249, 249 });
            this._f5f5f5 = new XSSFColor(new byte[3] { 244, 244, 244 });
            this._c0c0c0 = new XSSFColor(new byte[3] { 191, 191, 191 });
            this._666666 = new XSSFColor(new byte[3] { 101, 101, 101 });
            this._157fcc = new XSSFColor(new byte[3] { 20, 126, 203 });
            this._000000 = new XSSFColor(new byte[3] { 0, 0, 0 });
        }

        #endregion

        #region Properties

        public XSSFWorkbook Workbook {
            get { return _workbook; }
        }

        public XSSFFont Font {
            get {
                if(_font != null)
                    return _font;

                _font = (XSSFFont)_workbook.CreateFont();
                _font.Boldweight = (short)FontBoldWeight.None;
                _font.FontHeightInPoints = 10;
                return _font;
            }
        }

        public IDataFormat DataFormat {
            get {
                if(_format != null)
                    return _format;

                _format = _workbook.CreateDataFormat();
                return _format;
            }
        }

        public short DateFormat {
            get { return this.DataFormat.GetFormat("yyyy-MM-dd HH:mm:ss"); }
        }

        public XSSFCellStyle Default {
            get {
                if(_default != null)
                    return _default;

                _default = (XSSFCellStyle)_workbook.CreateCellStyle();
                _default.Alignment = HorizontalAlignment.Left;
                _default.VerticalAlignment = VerticalAlignment.Center;
                _default.BorderBottom = BorderStyle.Thin;
                _default.BorderLeft = BorderStyle.Thin;
                _default.BorderRight = BorderStyle.Thin;
                _default.BorderTop = BorderStyle.Thin;
                _default.SetBottomBorderColor(_c0c0c0);
                _default.SetLeftBorderColor(_c0c0c0);
                _default.SetRightBorderColor(_c0c0c0);
                _default.SetTopBorderColor(_c0c0c0);
                _default.SetFont(this.Font);

                return _default;
            }
        }

        public XSSFCellStyle Red {
            get {
                if(_red != null)
                    return _red;

                _red = (XSSFCellStyle)_workbook.CreateCellStyle();
                _red.Alignment = HorizontalAlignment.Left;
                _red.VerticalAlignment = VerticalAlignment.Center;
                _red.BorderBottom = BorderStyle.Thin;
                _red.BorderLeft = BorderStyle.Thin;
                _red.BorderRight = BorderStyle.Thin;
                _red.BorderTop = BorderStyle.Thin;
                _red.SetBottomBorderColor(_c0c0c0);
                _red.SetLeftBorderColor(_c0c0c0);
                _red.SetRightBorderColor(_c0c0c0);
                _red.SetTopBorderColor(_c0c0c0);
                _red.SetFillForegroundColor(new XSSFColor(Color.Red));
                _red.FillPattern = FillPattern.SolidForeground;
                _red.SetFillBackgroundColor(new XSSFColor(Color.Red));
                _red.SetFont(this.Font);

                return _red;
            }
        }

        public XSSFCellStyle Orange {
            get {
                if(_orange != null)
                    return _orange;

                _orange = (XSSFCellStyle)_workbook.CreateCellStyle();
                _orange.Alignment = HorizontalAlignment.Left;
                _orange.VerticalAlignment = VerticalAlignment.Center;
                _orange.BorderBottom = BorderStyle.Thin;
                _orange.BorderLeft = BorderStyle.Thin;
                _orange.BorderRight = BorderStyle.Thin;
                _orange.BorderTop = BorderStyle.Thin;
                _orange.SetBottomBorderColor(_c0c0c0);
                _orange.SetLeftBorderColor(_c0c0c0);
                _orange.SetRightBorderColor(_c0c0c0);
                _orange.SetTopBorderColor(_c0c0c0);
                _orange.SetFillForegroundColor(new XSSFColor(Color.Orange));
                _orange.FillPattern = FillPattern.SolidForeground;
                _orange.SetFillBackgroundColor(new XSSFColor(Color.Orange));
                _orange.SetFont(this.Font);

                return _orange;
            }
        }

        public XSSFCellStyle Yellow {
            get {
                if(_yellow != null)
                    return _yellow;

                _yellow = (XSSFCellStyle)_workbook.CreateCellStyle();
                _yellow.Alignment = HorizontalAlignment.Left;
                _yellow.VerticalAlignment = VerticalAlignment.Center;
                _yellow.BorderBottom = BorderStyle.Thin;
                _yellow.BorderLeft = BorderStyle.Thin;
                _yellow.BorderRight = BorderStyle.Thin;
                _yellow.BorderTop = BorderStyle.Thin;
                _yellow.SetBottomBorderColor(_c0c0c0);
                _yellow.SetLeftBorderColor(_c0c0c0);
                _yellow.SetRightBorderColor(_c0c0c0);
                _yellow.SetTopBorderColor(_c0c0c0);
                _yellow.SetFillForegroundColor(new XSSFColor(Color.Yellow));
                _yellow.FillPattern = FillPattern.SolidForeground;
                _yellow.SetFillBackgroundColor(new XSSFColor(Color.Yellow));
                _yellow.SetFont(this.Font);

                return _yellow;
            }
        }

        public XSSFCellStyle SkyBlue {
            get {
                if(_skyblue != null)
                    return _skyblue;

                _skyblue = (XSSFCellStyle)_workbook.CreateCellStyle();
                _skyblue.Alignment = HorizontalAlignment.Left;
                _skyblue.VerticalAlignment = VerticalAlignment.Center;
                _skyblue.BorderBottom = BorderStyle.Thin;
                _skyblue.BorderLeft = BorderStyle.Thin;
                _skyblue.BorderRight = BorderStyle.Thin;
                _skyblue.BorderTop = BorderStyle.Thin;
                _skyblue.SetBottomBorderColor(_c0c0c0);
                _skyblue.SetLeftBorderColor(_c0c0c0);
                _skyblue.SetRightBorderColor(_c0c0c0);
                _skyblue.SetTopBorderColor(_c0c0c0);
                _skyblue.SetFillForegroundColor(new XSSFColor(Color.SkyBlue));
                _skyblue.FillPattern = FillPattern.SolidForeground;
                _skyblue.SetFillBackgroundColor(new XSSFColor(Color.SkyBlue));
                _skyblue.SetFont(this.Font);

                return _skyblue;
            }
        }

        public XSSFCellStyle Blue {
            get {
                if(_blue != null)
                    return _blue;

                _blue = (XSSFCellStyle)_workbook.CreateCellStyle();
                _blue.Alignment = HorizontalAlignment.Left;
                _blue.VerticalAlignment = VerticalAlignment.Center;
                _blue.BorderBottom = BorderStyle.Thin;
                _blue.BorderLeft = BorderStyle.Thin;
                _blue.BorderRight = BorderStyle.Thin;
                _blue.BorderTop = BorderStyle.Thin;
                _blue.SetBottomBorderColor(_c0c0c0);
                _blue.SetLeftBorderColor(_c0c0c0);
                _blue.SetRightBorderColor(_c0c0c0);
                _blue.SetTopBorderColor(_c0c0c0);
                _blue.SetFillForegroundColor(new XSSFColor(Color.Blue));
                _blue.FillPattern = FillPattern.SolidForeground;
                _blue.SetFillBackgroundColor(new XSSFColor(Color.Blue));
                _blue.SetFont(this.Font);

                return _blue;
            }
        }

        public XSSFCellStyle LimeGreen {
            get {
                if(_limegreen != null)
                    return _limegreen;

                _limegreen = (XSSFCellStyle)_workbook.CreateCellStyle();
                _limegreen.Alignment = HorizontalAlignment.Left;
                _limegreen.VerticalAlignment = VerticalAlignment.Center;
                _limegreen.BorderBottom = BorderStyle.Thin;
                _limegreen.BorderLeft = BorderStyle.Thin;
                _limegreen.BorderRight = BorderStyle.Thin;
                _limegreen.BorderTop = BorderStyle.Thin;
                _limegreen.SetBottomBorderColor(_c0c0c0);
                _limegreen.SetLeftBorderColor(_c0c0c0);
                _limegreen.SetRightBorderColor(_c0c0c0);
                _limegreen.SetTopBorderColor(_c0c0c0);
                _limegreen.SetFillForegroundColor(new XSSFColor(Color.LimeGreen));
                _limegreen.FillPattern = FillPattern.SolidForeground;
                _limegreen.SetFillBackgroundColor(new XSSFColor(Color.LimeGreen));
                _limegreen.SetFont(this.Font);

                return _limegreen;
            }
        }

        public XSSFCellStyle LightGray {
            get {
                if(_lightgray != null)
                    return _lightgray;

                _lightgray = (XSSFCellStyle)_workbook.CreateCellStyle();
                _lightgray.Alignment = HorizontalAlignment.Left;
                _lightgray.VerticalAlignment = VerticalAlignment.Center;
                _lightgray.BorderBottom = BorderStyle.Thin;
                _lightgray.BorderLeft = BorderStyle.Thin;
                _lightgray.BorderRight = BorderStyle.Thin;
                _lightgray.BorderTop = BorderStyle.Thin;
                _lightgray.SetBottomBorderColor(_c0c0c0);
                _lightgray.SetLeftBorderColor(_c0c0c0);
                _lightgray.SetRightBorderColor(_c0c0c0);
                _lightgray.SetTopBorderColor(_c0c0c0);
                _lightgray.SetFillForegroundColor(new XSSFColor(Color.LightGray));
                _lightgray.FillPattern = FillPattern.SolidForeground;
                _lightgray.SetFillBackgroundColor(new XSSFColor(Color.LightGray));
                _lightgray.SetFont(this.Font);

                return _lightgray;
            }
        }

        public XSSFCellStyle Date {
            get {
                if(_date != null)
                    return _date;

                _date = (XSSFCellStyle)_workbook.CreateCellStyle();
                _date.Alignment = HorizontalAlignment.Left;
                _date.VerticalAlignment = VerticalAlignment.Center;
                _date.BorderBottom = BorderStyle.Thin;
                _date.BorderLeft = BorderStyle.Thin;
                _date.BorderRight = BorderStyle.Thin;
                _date.BorderTop = BorderStyle.Thin;
                _date.SetBottomBorderColor(_c0c0c0);
                _date.SetLeftBorderColor(_c0c0c0);
                _date.SetRightBorderColor(_c0c0c0);
                _date.SetTopBorderColor(_c0c0c0);
                _date.DataFormat = this.DateFormat;
                _date.SetFont(this.Font);

                return _date;
            }
        }

        public XSSFCellStyle DateWithRed {
            get {
                if(_datewithred != null)
                    return _datewithred;

                _datewithred = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithred.Alignment = HorizontalAlignment.Left;
                _datewithred.VerticalAlignment = VerticalAlignment.Center;
                _datewithred.BorderBottom = BorderStyle.Thin;
                _datewithred.BorderLeft = BorderStyle.Thin;
                _datewithred.BorderRight = BorderStyle.Thin;
                _datewithred.BorderTop = BorderStyle.Thin;
                _datewithred.SetBottomBorderColor(_c0c0c0);
                _datewithred.SetLeftBorderColor(_c0c0c0);
                _datewithred.SetRightBorderColor(_c0c0c0);
                _datewithred.SetTopBorderColor(_c0c0c0);
                _datewithred.DataFormat = this.DateFormat;
                _datewithred.SetFillForegroundColor(new XSSFColor(Color.Red));
                _datewithred.FillPattern = FillPattern.SolidForeground;
                _datewithred.SetFillBackgroundColor(new XSSFColor(Color.Red));
                _datewithred.SetFont(this.Font);

                return _datewithred;
            }
        }

        public XSSFCellStyle DateWithOrange {
            get {
                if(_datewithorange != null)
                    return _datewithorange;

                _datewithorange = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithorange.Alignment = HorizontalAlignment.Left;
                _datewithorange.VerticalAlignment = VerticalAlignment.Center;
                _datewithorange.BorderBottom = BorderStyle.Thin;
                _datewithorange.BorderLeft = BorderStyle.Thin;
                _datewithorange.BorderRight = BorderStyle.Thin;
                _datewithorange.BorderTop = BorderStyle.Thin;
                _datewithorange.SetBottomBorderColor(_c0c0c0);
                _datewithorange.SetLeftBorderColor(_c0c0c0);
                _datewithorange.SetRightBorderColor(_c0c0c0);
                _datewithorange.SetTopBorderColor(_c0c0c0);
                _datewithorange.DataFormat = this.DateFormat;
                _datewithorange.SetFillForegroundColor(new XSSFColor(Color.Orange));
                _datewithorange.FillPattern = FillPattern.SolidForeground;
                _datewithorange.SetFillBackgroundColor(new XSSFColor(Color.Orange));
                _datewithorange.SetFont(this.Font);

                return _datewithorange;
            }
        }

        public XSSFCellStyle DateWithYellow {
            get {
                if(_datewithyellow != null)
                    return _datewithyellow;

                _datewithyellow = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithyellow.Alignment = HorizontalAlignment.Left;
                _datewithyellow.VerticalAlignment = VerticalAlignment.Center;
                _datewithyellow.BorderBottom = BorderStyle.Thin;
                _datewithyellow.BorderLeft = BorderStyle.Thin;
                _datewithyellow.BorderRight = BorderStyle.Thin;
                _datewithyellow.BorderTop = BorderStyle.Thin;
                _datewithyellow.SetBottomBorderColor(_c0c0c0);
                _datewithyellow.SetLeftBorderColor(_c0c0c0);
                _datewithyellow.SetRightBorderColor(_c0c0c0);
                _datewithyellow.SetTopBorderColor(_c0c0c0);
                _datewithyellow.DataFormat = this.DateFormat;
                _datewithyellow.SetFillForegroundColor(new XSSFColor(Color.Yellow));
                _datewithyellow.FillPattern = FillPattern.SolidForeground;
                _datewithyellow.SetFillBackgroundColor(new XSSFColor(Color.Yellow));
                _datewithyellow.SetFont(this.Font);

                return _datewithyellow;
            }
        }

        public XSSFCellStyle DateWithSkyBlue {
            get {
                if(_datewithskyblue != null)
                    return _datewithskyblue;

                _datewithskyblue = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithskyblue.Alignment = HorizontalAlignment.Left;
                _datewithskyblue.VerticalAlignment = VerticalAlignment.Center;
                _datewithskyblue.BorderBottom = BorderStyle.Thin;
                _datewithskyblue.BorderLeft = BorderStyle.Thin;
                _datewithskyblue.BorderRight = BorderStyle.Thin;
                _datewithskyblue.BorderTop = BorderStyle.Thin;
                _datewithskyblue.SetBottomBorderColor(_c0c0c0);
                _datewithskyblue.SetLeftBorderColor(_c0c0c0);
                _datewithskyblue.SetRightBorderColor(_c0c0c0);
                _datewithskyblue.SetTopBorderColor(_c0c0c0);
                _datewithskyblue.DataFormat = this.DateFormat;
                _datewithskyblue.SetFillForegroundColor(new XSSFColor(Color.SkyBlue));
                _datewithskyblue.FillPattern = FillPattern.SolidForeground;
                _datewithskyblue.SetFillBackgroundColor(new XSSFColor(Color.SkyBlue));
                _datewithskyblue.SetFont(this.Font);

                return _datewithskyblue;
            }
        }

        public XSSFCellStyle DateWithBlue {
            get {
                if(_datewithblue != null)
                    return _datewithblue;

                _datewithblue = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithblue.Alignment = HorizontalAlignment.Left;
                _datewithblue.VerticalAlignment = VerticalAlignment.Center;
                _datewithblue.BorderBottom = BorderStyle.Thin;
                _datewithblue.BorderLeft = BorderStyle.Thin;
                _datewithblue.BorderRight = BorderStyle.Thin;
                _datewithblue.BorderTop = BorderStyle.Thin;
                _datewithblue.SetBottomBorderColor(_c0c0c0);
                _datewithblue.SetLeftBorderColor(_c0c0c0);
                _datewithblue.SetRightBorderColor(_c0c0c0);
                _datewithblue.SetTopBorderColor(_c0c0c0);
                _datewithblue.DataFormat = this.DateFormat;
                _datewithblue.SetFillForegroundColor(new XSSFColor(Color.Blue));
                _datewithblue.FillPattern = FillPattern.SolidForeground;
                _datewithblue.SetFillBackgroundColor(new XSSFColor(Color.Blue));
                _datewithblue.SetFont(this.Font);

                return _datewithblue;
            }
        }

        public XSSFCellStyle DateWithLimeGreen {
            get {
                if(_datewithlimegreen != null)
                    return _datewithlimegreen;

                _datewithlimegreen = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithlimegreen.Alignment = HorizontalAlignment.Left;
                _datewithlimegreen.VerticalAlignment = VerticalAlignment.Center;
                _datewithlimegreen.BorderBottom = BorderStyle.Thin;
                _datewithlimegreen.BorderLeft = BorderStyle.Thin;
                _datewithlimegreen.BorderRight = BorderStyle.Thin;
                _datewithlimegreen.BorderTop = BorderStyle.Thin;
                _datewithlimegreen.SetBottomBorderColor(_c0c0c0);
                _datewithlimegreen.SetLeftBorderColor(_c0c0c0);
                _datewithlimegreen.SetRightBorderColor(_c0c0c0);
                _datewithlimegreen.SetTopBorderColor(_c0c0c0);
                _datewithlimegreen.DataFormat = this.DateFormat;
                _datewithlimegreen.SetFillForegroundColor(new XSSFColor(Color.LimeGreen));
                _datewithlimegreen.FillPattern = FillPattern.SolidForeground;
                _datewithlimegreen.SetFillBackgroundColor(new XSSFColor(Color.LimeGreen));
                _datewithlimegreen.SetFont(this.Font);

                return _datewithlimegreen;
            }
        }

        public XSSFCellStyle DateWithLightGray {
            get {
                if(_datewithlightgray != null)
                    return _datewithlightgray;

                _datewithlightgray = (XSSFCellStyle)_workbook.CreateCellStyle();
                _datewithlightgray.Alignment = HorizontalAlignment.Left;
                _datewithlightgray.VerticalAlignment = VerticalAlignment.Center;
                _datewithlightgray.BorderBottom = BorderStyle.Thin;
                _datewithlightgray.BorderLeft = BorderStyle.Thin;
                _datewithlightgray.BorderRight = BorderStyle.Thin;
                _datewithlightgray.BorderTop = BorderStyle.Thin;
                _datewithlightgray.SetBottomBorderColor(_c0c0c0);
                _datewithlightgray.SetLeftBorderColor(_c0c0c0);
                _datewithlightgray.SetRightBorderColor(_c0c0c0);
                _datewithlightgray.SetTopBorderColor(_c0c0c0);
                _datewithlightgray.DataFormat = this.DateFormat;
                _datewithlightgray.SetFillForegroundColor(new XSSFColor(Color.LightGray));
                _datewithlightgray.FillPattern = FillPattern.SolidForeground;
                _datewithlightgray.SetFillBackgroundColor(new XSSFColor(Color.LightGray));
                _datewithlightgray.SetFont(this.Font);

                return _datewithlightgray;
            }
        }

        public XSSFCellStyle Title {
            get {
                if(_title != null)
                    return _title;

                _title = (XSSFCellStyle)_workbook.CreateCellStyle();
                _title.Alignment = HorizontalAlignment.Center;
                _title.VerticalAlignment = VerticalAlignment.Center;
                _title.BorderBottom = BorderStyle.Thin;
                _title.BorderLeft = BorderStyle.Thin;
                _title.BorderRight = BorderStyle.Thin;
                _title.BorderTop = BorderStyle.Thin;
                _title.SetBottomBorderColor(_c0c0c0);
                _title.SetLeftBorderColor(_c0c0c0);
                _title.SetRightBorderColor(_c0c0c0);
                _title.SetTopBorderColor(_c0c0c0);
                _title.SetFillForegroundColor(_157fcc);
                _title.FillPattern = FillPattern.SolidForeground;
                _title.SetFillBackgroundColor(_157fcc);

                var ttfont = (XSSFFont)_workbook.CreateFont();
                ttfont.Boldweight = (short)FontBoldWeight.Bold;
                ttfont.FontHeightInPoints = 12;
                ttfont.SetColor(_ffffff);
                _title.SetFont(ttfont);

                return _title;
            }
        }

        public XSSFCellStyle SubTitle {
            get {
                if(_subtitle != null)
                    return _subtitle;

                _subtitle = (XSSFCellStyle)_workbook.CreateCellStyle();
                _subtitle.Alignment = HorizontalAlignment.Right;
                _subtitle.VerticalAlignment = VerticalAlignment.Center;
                _subtitle.BorderBottom = BorderStyle.Thin;
                _subtitle.BorderLeft = BorderStyle.Thin;
                _subtitle.BorderRight = BorderStyle.Thin;
                _subtitle.BorderTop = BorderStyle.Thin;
                _subtitle.SetBottomBorderColor(_c0c0c0);
                _subtitle.SetLeftBorderColor(_c0c0c0);
                _subtitle.SetRightBorderColor(_c0c0c0);
                _subtitle.SetTopBorderColor(_c0c0c0);
                _subtitle.SetFillForegroundColor(_fafafa);
                _subtitle.FillPattern = FillPattern.SolidForeground;
                _subtitle.SetFillBackgroundColor(_fafafa);

                var stfont = (XSSFFont)_workbook.CreateFont();
                stfont.Boldweight = (short)FontBoldWeight.None;
                stfont.FontHeightInPoints = 10;
                stfont.SetColor(_666666);
                _subtitle.SetFont(stfont);

                return _subtitle;
            }
        }

        public XSSFCellStyle CellTitle {
            get {
                if(_celltitle != null)
                    return _celltitle;

                _celltitle = (XSSFCellStyle)_workbook.CreateCellStyle();
                _celltitle.Alignment = HorizontalAlignment.Center;
                _celltitle.VerticalAlignment = VerticalAlignment.Center;
                _celltitle.BorderBottom = BorderStyle.Thin;
                _celltitle.BorderLeft = BorderStyle.Thin;
                _celltitle.BorderRight = BorderStyle.Thin;
                _celltitle.BorderTop = BorderStyle.Thin;
                _celltitle.SetBottomBorderColor(_c0c0c0);
                _celltitle.SetLeftBorderColor(_c0c0c0);
                _celltitle.SetRightBorderColor(_c0c0c0);
                _celltitle.SetTopBorderColor(_c0c0c0);
                _celltitle.SetFillForegroundColor(_f5f5f5);
                _celltitle.FillPattern = FillPattern.SolidForeground;
                _celltitle.SetFillBackgroundColor(_f5f5f5);

                var ctfont = (XSSFFont)_workbook.CreateFont();
                ctfont.Boldweight = (short)FontBoldWeight.Bold;
                ctfont.FontHeightInPoints = 11;
                ctfont.SetColor(_666666);
                _celltitle.SetFont(ctfont);

                return _celltitle;
            }
        }

        #endregion

        #region Methods

        public XSSFCellStyle GetCellStyle(Color color, bool draw = true) {
            if(!draw)
                return this.Default;

            if(color == Color.Red)
                return this.Red;

            if(color == Color.Orange)
                return this.Orange;

            if(color == Color.Yellow)
                return this.Yellow;

            if(color == Color.SkyBlue)
                return this.SkyBlue;

            if(color == Color.Blue)
                return this.Blue;

            if(color == Color.LimeGreen)
                return this.LimeGreen;

            if(color == Color.LightGray)
                return this.LightGray;

            return this.Default;
        }

        public XSSFCellStyle GetDateCellStyle(Color color, bool draw = true) {
            if(!draw)
                return this.Date;

            if(color == Color.Red)
                return this.DateWithRed;

            if(color == Color.Orange)
                return this.DateWithOrange;

            if(color == Color.Yellow)
                return this.DateWithYellow;

            if(color == Color.SkyBlue)
                return this.DateWithSkyBlue;

            if(color == Color.Blue)
                return this.DateWithBlue;

            if(color == Color.LimeGreen)
                return this.DateWithLimeGreen;

            if(color == Color.LightGray)
                return this.DateWithLightGray;

            return this.Date;
        }

        public XSSFSheet CreateSheet() {
            var id = _worksheets.Keys.Count + 1;
            var worksheet = (XSSFSheet)_workbook.CreateSheet(string.Format("Sheet{0}", id));
            worksheet.DefaultColumnWidth = 25;
            worksheet.Header.Left = "iPemSystem";
            worksheet.Header.Right = "Delta GreenTech(China) Co., Ltd.";
            worksheet.Footer.Center = "&P/&N";
            _worksheets[id] = worksheet;

            return worksheet;
        }

        #endregion

    }
}