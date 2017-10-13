using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Site.Models.BInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Web;
using System.Xml;

namespace iPem.Site.Infrastructure {
    public class Common {
        public static string GlobalSeparator {
            get { return "┆"; }
        }

        public static string CaptchaId {
            get { return "ipems.login.captcha"; }
        }

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

        public static byte[] MergeSvgXml(string[] svgs) {
            if(svgs == null || svgs.Length == 0)
                return null;

            var images = new Bitmap[svgs.Length];
            for(var i = 0; i < svgs.Length; i++) {
                var xml = new XmlDocument();
                xml.XmlResolver = null;
                xml.LoadXml(HttpUtility.HtmlDecode(svgs[i]));
                var svgGraph = Svg.SvgDocument.Open(xml);
                var image = svgGraph.Draw();
                images[i] = image;
            }

            int width = 0, height = 0;
            foreach(var image in images) {
                width = width + image.Width;
                if(image.Height > height)
                    height = image.Height;
            }

            using(var result = new Bitmap(width, height)) {
                using(var canvas = Graphics.FromImage(result)) {
                    var start = 0;
                    canvas.Clear(Color.White);
                    for(var i = 0; i < images.Length; i++) {
                        canvas.DrawImage(images[i], new Rectangle(start, 0, images[i].Width, images[i].Height));
                        start += images[i].Width;
                    }
                    canvas.Save();
                }

                using(var ms = new MemoryStream()) {
                    result.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        public static string GetCachedKey(string pattern, IWorkContext workContext) {
            if(!workContext.IsAuthenticated())throw new iPemException("Unauthorized");
            return string.Format(pattern, workContext.Identifier());
        }

        public static string GetSexDisplay(EnmSex sex) {
            if(sex == EnmSex.Female)
                return "女";
            else
                return "男";
        }

        public static string GetDictionaryDisplay(EnmDictionary dic) {
            switch(dic) {
                case EnmDictionary.Ws:
                    return "数据通信";
                case EnmDictionary.Ts:
                    return "语音播报";
                case EnmDictionary.Elec:
                    return "能耗公式";
                case EnmDictionary.Report:
                    return "报表参数";
                default:
                    return "未定义";
            }
        }

        public static string GetEventLevelDisplay(EnmEventLevel level) {
            switch(level) {
                case EnmEventLevel.Debug:
                    return "调试信息";
                case EnmEventLevel.Information:
                    return "普通信息";
                case EnmEventLevel.Warning:
                    return "系统警告";
                case EnmEventLevel.Error:
                    return "异常错误";
                case EnmEventLevel.Fatal:
                    return "严重错误";
                default:
                    return "未定义";
            }
        }

        public static string GetEventTypeDisplay(EnmEventType type) {
            switch(type) {
                case EnmEventType.Exception:
                    return "异常操作";
                case EnmEventType.Operating:
                    return "正常操作";
                case EnmEventType.Login:
                    return "登录系统";
                case EnmEventType.Logout:
                    return "登出系统";
                case EnmEventType.Other:
                default:
                    return "其他";
            }
        }

        public static string GetPermissionDisplay(EnmPermission op) {
            switch(op) {
                case EnmPermission.Control:
                    return "信号遥控";
                case EnmPermission.Adjust:
                    return "信号遥调";
                case EnmPermission.Confirm:
                    return "告警确认";
                case EnmPermission.Threshold:
                    return "门限设置";
                default:
                    return "其他";
            }
        }

        public static string GetPointTypeDisplay(EnmPoint type) {
            switch(type) {
                case EnmPoint.AI:
                    return "遥测";
                case EnmPoint.AO:
                    return "遥调";
                case EnmPoint.DI:
                    return "遥信";
                case EnmPoint.DO:
                    return "遥控";
                case EnmPoint.AL:
                    return "告警";
                default:
                    return "未定义";
            }
        }

        public static string GetPointStatusDisplay(EnmState status) {
            switch(status) {
                case EnmState.Normal:
                    return "正常数据";
                case EnmState.Level1:
                    return "一级告警";
                case EnmState.Level2:
                    return "二级告警";
                case EnmState.Level3:
                    return "三级告警";
                case EnmState.Level4:
                    return "四级告警";
                case EnmState.Opevent:
                    return "操作事件";
                case EnmState.Invalid:
                    return "无效数据";
                default:
                    return "未定义";
            }
        }

        public static string GetAlarmDisplay(EnmAlarm level) {
            switch(level) {
                case EnmAlarm.Level0:
                    return "无告警";
                case EnmAlarm.Level1:
                    return "一级告警";
                case EnmAlarm.Level2:
                    return "二级告警";
                case EnmAlarm.Level3:
                    return "三级告警";
                case EnmAlarm.Level4:
                    return "四级告警";
                default:
                    return "未定义";
            }
        }

        public static string GetBIAlarmDisplay(EnmBILevel level) {
            switch (level) {
                case EnmBILevel.CRITICAL:
                    return "一级告警";
                case EnmBILevel.MAJOR:
                    return "二级告警";
                case EnmBILevel.MINOR:
                    return "三级告警";
                case EnmBILevel.HINT:
                    return "四级告警";
                default:
                    return "无告警";
            }
        }

        public static string GetConfirmDisplay(EnmConfirm status) {
            switch(status) {
                case EnmConfirm.Confirmed:
                    return "已确认";
                case EnmConfirm.Unconfirmed:
                    return "未确认";
                default:
                    return "未定义";
            }
        }

        public static string GetReservationDisplay(EnmReservation reservation) {
            switch (reservation) {
                case EnmReservation.UnReservation:
                    return "非工程告警";
                case EnmReservation.Reservation:
                    return "工程告警";
                default:
                    return "未定义";
            }
        }

        public static string GetComputeDisplay(EnmCompute compute) {
            switch (compute) {
                case EnmCompute.Diff:
                    return "电表电度运算";
                case EnmCompute.Avg:
                    return "电压电流运算";
                default:
                    return "未定义";
            }
        }

        public static string GetFsuEventDisplay(EnmFsuEvent evet) {
            switch (evet) {
                case EnmFsuEvent.FTP:
                    return "FTP操作";
                case EnmFsuEvent.FSU:
                    return "FSU日志";
                case EnmFsuEvent.Undefined:
                default:
                    return "未定义";
            }
        }

        public static string GetValueDisplay(EnmPoint type, string value, string unit) {
            switch(type) {
                case EnmPoint.DI:
                case EnmPoint.DO:
                    var result = string.Empty;
                    var units = (unit ?? string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(var u in units) {
                        var vs = u.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                        if(vs.Length != 2) continue;

                        if(vs[0].Trim() == value) {
                            result = vs[1].Trim();
                            break;
                        }
                    }
                    return result;
                case EnmPoint.AI:
                case EnmPoint.AO:
                    return string.Format("{0} {1}", value, unit ?? string.Empty);
                default:
                    return "";
            }
        }

        public static string GetUnitDisplay(EnmPoint type, string value, string unit) {
            switch(type) {
                case EnmPoint.DI:
                case EnmPoint.DO:
                    var result = string.Empty;
                    var units = (unit ?? string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(var u in units) {
                        var vs = u.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                        if(vs.Length != 2) continue;

                        if(vs[0].Trim() == value) {
                            result = vs[1].Trim();
                            break;
                        }
                    }
                    return result;
                case EnmPoint.AI:
                case EnmPoint.AO:
                    return unit;
                default:
                    return "";
            }
        }

        public static string GetEmployeeTypeDisplay(EnmEmpType type) {
            switch (type) {
                case EnmEmpType.Employee:
                    return "正式人员";
                case EnmEmpType.OutEmployee:
                    return "外协人员";
                default:
                    return "未定义";
            }
        }

        public static Color GetAlarmColor(EnmAlarm level) {
            switch(level) {
                case EnmAlarm.Level1:
                    return Color.Red;
                case EnmAlarm.Level2:
                    return Color.Orange;
                case EnmAlarm.Level3:
                    return Color.Yellow;
                case EnmAlarm.Level4:
                    return Color.SkyBlue;
                default:
                    return Color.White;
            }
        }

        public static Color GetPointColor(EnmState status) {
            switch(status) {
                case EnmState.Normal:
                    return Color.LimeGreen;
                case EnmState.Invalid:
                    return Color.LightGray;
                default:
                    return Color.White;
            }
        }

        public static EnmState LevelToState(EnmAlarm level) {
            switch (level) {
                case EnmAlarm.Level0:
                    return EnmState.Normal;
                case EnmAlarm.Level1:
                    return EnmState.Level1;
                case EnmAlarm.Level2:
                    return EnmState.Level2;
                case EnmAlarm.Level3:
                    return EnmState.Level3;
                case EnmAlarm.Level4:
                    return EnmState.Level4;
                default:
                    return EnmState.Invalid;
            }
        }

        public static string[] SplitCondition(string conditions) {
            if(string.IsNullOrWhiteSpace(conditions))
                return new string[0];

            return conditions.Split(new char[] { ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string JoinCondition(params string[] conditions) {
            if (conditions == null || conditions.Length == 0)
                return string.Empty;

            return string.Join(";", conditions);
        }

        public static string[] SplitKeys(string key) {
            if(string.IsNullOrWhiteSpace(key))
                return new string[0];

            return key.Split(new string[] { GlobalSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string JoinKeys(params object[] keys) {
            if(keys == null || keys.Length == 0)
                return string.Empty;

            return string.Join(GlobalSeparator, keys);
        }

        public static Kv<EnmSSH, string> ParseNode(string node) {
            if ("root".Equals(node, StringComparison.CurrentCultureIgnoreCase))
                return new Kv<EnmSSH, string>(EnmSSH.Root, node);

            var keys = Common.SplitKeys(node);
            if (keys.Length != 2) throw new iPemException("参数格式错误");
            if (!Enum.IsDefined(typeof(EnmSSH), int.Parse(keys[0]))) throw new iPemException("无效的参数");
            return new Kv<EnmSSH, string>((EnmSSH)(int.Parse(keys[0])), keys[1]);
        }

        public static string GetNodeName(string node, C_SCVendor vendor) {
            if (string.IsNullOrWhiteSpace(node)) node = "未知";
            if (vendor == null) return node;
            if (string.IsNullOrWhiteSpace(vendor.Name)) return node;
            return string.Format("{0}[{1}]", node, vendor.Name);
        }

        public static bool SetAllowUnsafeHeaderParsing() {
            //Get the assembly that contains the internal class
            var aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            if(aNetAssembly != null) {
                //Use the assembly in order to get the internal type for the internal class
                var aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
                if(aSettingsType != null) {
                    //Use the internal static property to get an instance of the internal settings class.
                    //If the static instance isn't created allready the property will create it for us.
                    var anInstance = aSettingsType.InvokeMember("Section", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });
                    if(anInstance != null) {
                        //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
                        var aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
                        if(aUseUnsafeHeaderParsing != null) {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}