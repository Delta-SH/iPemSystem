using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.Security;
using iPem.Core;
using iPem.Core.Enum;

namespace iPem.Site.Infrastructure {
    public class Common {
        /// <summary>
        /// Captcha cookie id
        /// </summary>
        public static string CaptchaId {
            get { return "ipems.login.captcha"; }
        }

        /// <summary>
        /// Generating captcha image
        /// </summary>
        /// <param name="code">captcha code</param>
        /// <param name="width">image width</param>
        /// <param name="height">image width</param>
        /// <returns>image bytes</returns>
        public static byte[] CreateCaptcha(string code, int width = 100, int height = 30) {
            if(string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException("code");

            Graphics g = null;
            Font font = null;
            HatchBrush hatchBrush = null;
            try {
                var image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                g = Graphics.FromImage(image);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = new Rectangle(0, 0, width, height);
                hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
                g.FillRectangle(hatchBrush, rect);

                SizeF size;
                var fontSize = rect.Height + 1f;
                do {
                    fontSize--;
                    font = new Font("Arial", fontSize, FontStyle.Bold);
                    size = g.MeasureString(code, font);
                } while(size.Width > rect.Width);

                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                var path = new GraphicsPath();
                path.AddString(code, font.FontFamily, (int)font.Style, font.Size + 5, rect, format);

                var v = 4f;
                var random = new Random();
                var points = new PointF[] {
                    new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
                    new PointF(rect.Width - random.Next(rect.Width) / v, random.Next(rect.Height) / v),
                    new PointF(random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v),
                    new PointF(rect.Width - random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v)
                };

                var matrix = new Matrix(); matrix.Translate(0F, 0F);
                path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

                hatchBrush.Dispose();
                hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.DarkGray);
                g.FillPath(hatchBrush, path);

                var m = Math.Max(rect.Width, rect.Height);
                var ct = (int)(rect.Width * rect.Height / 8F);
                for(var i = 0; i < ct; i++) {
                    var x = random.Next(rect.Width);
                    var y = random.Next(rect.Height);
                    var w = random.Next(m / 30);
                    var h = random.Next(m / 30);
                    g.FillEllipse(hatchBrush, x, y, w, h);
                }

                var bzct = random.Next(0, 3);
                for(int i = 0; i < bzct; i++) {
                    var p1 = new PointF(random.Next(rect.Width / 8), random.Next(rect.Height));
                    var p2 = new PointF(random.Next(rect.Width / 8), random.Next(rect.Height / 4));
                    var p3 = new PointF(random.Next(rect.Width - rect.Width / 2, rect.Width), random.Next(rect.Height));
                    var p4 = new PointF(random.Next(rect.Width - rect.Width / 2, rect.Width), random.Next(rect.Height));
                    g.DrawBezier(new Pen(Color.DarkGray, 1.6f), p1, p2, p3, p4);
                }

                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            } finally {
                if(font != null) font.Dispose();
                if(hatchBrush != null) hatchBrush.Dispose();
                if(g != null) g.Dispose();
            }
        }

        /// <summary>
        /// Generating random code string.
        /// </summary>
        /// <param name="len">code length</param>
        public static string GenerateCode(int length) {
            var chars = "abcdefghjkmnpqrstuvwxyz23456789ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

            var code = "";
            var index = 0;
            for(var i = 0; i < length; i++) {
                index = CommonHelper.GenerateRandomInteger(0, chars.Length - 1);
                code += chars[index].ToString();
            }
            return code;
        }

        public static string GetCachedKey(string pattern, IWorkContext workContext) {
            if(!workContext.IsAuthenticated)
                throw new iPemException("Unauthorized");

            return string.Format(pattern, workContext.Identifier);
        }

        public static string GetSexDisplayName(EnmSex sex) {
            if(sex == EnmSex.Female)
                return "女";
            else
                return "男";
        }

        public static string GetEventLevelDisplayName(EnmEventLevel level) {
            switch(level) {
                case EnmEventLevel.Debug:
                    return "调试";
                case EnmEventLevel.Information:
                    return "信息";
                case EnmEventLevel.Warning:
                    return "警告";
                case EnmEventLevel.Error:
                    return "错误";
                case EnmEventLevel.Fatal:
                    return "致命";
                default:
                    return "";
            }
        }

        public static string GetEventTypeDisplayName(EnmEventType type) {
            switch(type) {
                case EnmEventType.Exception:
                    return "异常";
                case EnmEventType.Operating:
                    return "操作";
                case EnmEventType.Login:
                    return "登录";
                case EnmEventType.Logout:
                    return "登出";
                case EnmEventType.Other:
                default:
                    return "其他";
            }
        }

        public static string GetOperationDisplayName(EnmOperation op) {
            switch(op) {
                case EnmOperation.Control:
                    return "测点遥控";
                case EnmOperation.Adjust:
                    return "测点遥调";
                case EnmOperation.Confirm:
                    return "告警确认";
                default:
                    return "其他";
            }
        }
    }
}