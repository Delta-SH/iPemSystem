using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Site.Models.BInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;

namespace iPem.Site.Infrastructure {
    public class Common {
        public static string CaptchaId {
            get { return "ipems.login.captcha"; }
        }

        public static byte[] CreateCaptcha(string code, int width = 100, int height = 30) {
            if (string.IsNullOrWhiteSpace(code))
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
                } while (size.Width > rect.Width);

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
                for (var i = 0; i < ct; i++) {
                    var x = random.Next(rect.Width);
                    var y = random.Next(rect.Height);
                    var w = random.Next(m / 30);
                    var h = random.Next(m / 30);
                    g.FillEllipse(hatchBrush, x, y, w, h);
                }

                var bzct = random.Next(0, 3);
                for (int i = 0; i < bzct; i++) {
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
                if (font != null) font.Dispose();
                if (hatchBrush != null) hatchBrush.Dispose();
                if (g != null) g.Dispose();
            }
        }

        public static string GenerateCode(int length) {
            var chars = "abcdefghjkmnpqrstuvwxyz23456789ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

            var code = "";
            var index = 0;
            for (var i = 0; i < length; i++) {
                index = CommonHelper.GenerateRandomInteger(0, chars.Length - 1);
                code += chars[index].ToString();
            }
            return code;
        }

        public static byte[] MergeSvgXml(string[] svgs) {
            if (svgs == null || svgs.Length == 0)
                return null;

            var images = new Bitmap[svgs.Length];
            for (var i = 0; i < svgs.Length; i++) {
                var xml = new XmlDocument();
                xml.XmlResolver = null;
                xml.LoadXml(HttpUtility.HtmlDecode(svgs[i]));
                var svgGraph = Svg.SvgDocument.Open(xml);
                var image = svgGraph.Draw();
                images[i] = image;
            }

            int width = 0, height = 0;
            foreach (var image in images) {
                width = width + image.Width;
                if (image.Height > height)
                    height = image.Height;
            }

            using (var result = new Bitmap(width, height)) {
                using (var canvas = Graphics.FromImage(result)) {
                    var start = 0;
                    canvas.Clear(Color.White);
                    for (var i = 0; i < images.Length; i++) {
                        canvas.DrawImage(images[i], new Rectangle(start, 0, images[i].Width, images[i].Height));
                        start += images[i].Width;
                    }
                    canvas.Save();
                }

                using (var ms = new MemoryStream()) {
                    result.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        public static string GetCachedKey(string pattern, IWorkContext workContext) {
            if (!workContext.IsAuthenticated()) throw new iPemException("Unauthorized");
            return string.Format(pattern, workContext.Identifier());
        }

        public static string GetSexDisplay(EnmSex sex) {
            if (sex == EnmSex.Female)
                return "女";
            else
                return "男";
        }

        public static string GetDictionaryDisplay(EnmDictionary dic) {
            switch (dic) {
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
            switch (level) {
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
            switch (type) {
                case EnmEventType.Login:
                    return "登录系统";
                case EnmEventType.Logout:
                    return "登出系统";
                case EnmEventType.Control:
                    return "信号遥控";
                case EnmEventType.Adjust:
                    return "信号遥调";
                case EnmEventType.Other:
                    return "其他操作";
                default:
                    return "未定义";
            }
        }

        public static string GetPermissionDisplay(EnmPermission op) {
            switch (op) {
                case EnmPermission.Control:
                    return "信号遥控";
                case EnmPermission.Adjust:
                    return "信号遥调";
                case EnmPermission.Confirm:
                    return "告警确认";
                case EnmPermission.Threshold:
                    return "门限设置";
                case EnmPermission.Check:
                    return "预约审核";
                default:
                    return "其他";
            }
        }

        public static string GetPointTypeDisplay(EnmPoint type) {
            switch (type) {
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
            switch (status) {
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

        public static string GetMaskTypeDisplay(EnmMaskType type) {
            switch (type) {
                case EnmMaskType.Area:
                    return "区域";
                case EnmMaskType.Station:
                    return "站点";
                case EnmMaskType.Room:
                    return "机房";
                case EnmMaskType.Fsu:
                    return "FSU";
                case EnmMaskType.Device:
                    return "设备";
                case EnmMaskType.Point:
                    return "信号";
                default:
                    return "未定义";
            }
        }

        public static string GetAlarmDisplay(EnmAlarm level) {
            switch (level) {
                case EnmAlarm.Level0:
                    return "正常";
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

        public static string GetPeriodDisplay(EnmPDH period) {
            switch (period) {
                case EnmPDH.Year:
                    return "按年统计";
                case EnmPDH.Month:
                    return "按月统计";
                case EnmPDH.Week:
                    return "按周统计";
                case EnmPDH.Day:
                    return "按日统计";
                case EnmPDH.Hour:
                    return "按时统计";
                default:
                    return "未定义";
            }
        }

        public static string GetEnergyDisplay(EnmFormula formula) {
            switch (formula) {
                case EnmFormula.KT:
                    return "空调";
                case EnmFormula.ZM:
                    return "照明";
                case EnmFormula.BG:
                    return "办公";
                case EnmFormula.DY:
                    return "开关电源";
                case EnmFormula.UPS:
                    return "UPS";
                case EnmFormula.IT:
                    return "IT设备";
                case EnmFormula.QT:
                    return "其他";
                case EnmFormula.TT:
                    return "总计";
                case EnmFormula.PUE:
                    return "PUE";
                case EnmFormula.WD:
                    return "温度";
                case EnmFormula.SD:
                    return "湿度";
                default:
                    return "未定义";
            }
        }

        public static string GetEnergyIconCls(EnmFormula formula) {
            switch (formula) {
                case EnmFormula.KT:
                    return "ipems-icon-font-kongtiao";
                case EnmFormula.ZM:
                    return "ipems-icon-font-zhaoming";
                case EnmFormula.BG:
                    return "ipems-icon-font-computer";
                case EnmFormula.DY:
                    return "ipems-icon-font-dianyuan";
                case EnmFormula.UPS:
                    return "ipems-icon-font-ups";
                case EnmFormula.IT:
                    return "ipems-icon-font-vdevice";
                case EnmFormula.QT:
                    return "ipems-icon-font-nenghao";
                case EnmFormula.TT:
                    return "ipems-icon-font-jifang";
                case EnmFormula.PUE:
                    return "ipems-icon-font-zhishu";
                default:
                    return "ipems-icon-font-nenghao";
            }
        }

        public static string GetEnergyColorCls(EnmFormula formula) {
            switch (formula) {
                case EnmFormula.KT:
                    return "indicator-kt";
                case EnmFormula.ZM:
                    return "indicator-zm";
                case EnmFormula.BG:
                    return "indicator-bg";
                case EnmFormula.DY:
                    return "indicator-dy";
                case EnmFormula.UPS:
                    return "indicator-ups";
                case EnmFormula.IT:
                    return "indicator-it";
                case EnmFormula.QT:
                    return "indicator-qt";
                case EnmFormula.TT:
                    return "indicator-tt";
                case EnmFormula.PUE:
                    return "indicator-pue";
                default:
                    return "indicator-qt";
            }
        }

        public static string GetConfirmDisplay(EnmConfirm status) {
            switch (status) {
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

        public static string GetVSignalCategoryDisplay(EnmVSignalCategory ext) {
            switch (ext) {
                case EnmVSignalCategory.Category01:
                    return "普通虚拟信号";
                case EnmVSignalCategory.Category02:
                    return "能耗虚拟信号";
                case EnmVSignalCategory.Category03:
                    return "列头柜分路功率";
                case EnmVSignalCategory.Category04:
                    return "列头柜分路电流";
                case EnmVSignalCategory.Category05:
                    return "列头柜分路电量";
                default:
                    return "未定义";
            }
        }

        public static string GetComputeDisplay(EnmCompute compute) {
            switch (compute) {
                case EnmCompute.Kwh:
                    return "电表电度运算";
                case EnmCompute.Power:
                    return "功率时间运算";
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

        public static string GetExeDisplay(EnmUpgradeStatus status) {
            switch (status) {
                case EnmUpgradeStatus.Ready:
                    return "准备就绪";
                case EnmUpgradeStatus.Running:
                    return "正在执行";
                case EnmUpgradeStatus.Success:
                    return "执行成功";
                case EnmUpgradeStatus.Failure:
                    return "执行失败";
                default:
                    return "未定义";
            }
        }

        public static string GetValueDisplay(EnmPoint type, string value, string unit) {
            switch (type) {
                case EnmPoint.DI:
                case EnmPoint.DO:
                    var result = string.Empty;
                    var units = (unit ?? string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var u in units) {
                        var vs = u.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                        if (vs.Length != 2) continue;

                        if (vs[0].Trim() == value) {
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
            switch (type) {
                case EnmPoint.DI:
                case EnmPoint.DO:
                case EnmPoint.AL:
                    var result = string.Empty;
                    var units = (unit ?? string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var u in units) {
                        var vs = u.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                        if (vs.Length != 2) continue;

                        if (vs[0].Trim() == value) {
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

        public static List<Kv<int, string>> GetDIStatus(string status) {
            var result = new List<Kv<int, string>>();
            var units = (status ?? string.Empty).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var unit in units) {
                var vs = unit.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                if (vs.Length != 2) continue;

                result.Add(new Kv<int, string>(int.Parse(vs[0].Trim()), vs[1].Trim()));
            }
            return result;
        }

        public static string GetEmployeeTypeDisplay(EnmEmpType type) {
            switch (type) {
                case EnmEmpType.Employee:
                    return "正式员工";
                case EnmEmpType.OutEmployee:
                    return "外协人员";
                default:
                    return "未定义";
            }
        }

        public static string GetDegreeDisplay(EnmDegree degree) {
            switch (degree) {
                case EnmDegree.High:
                    return "高中";
                case EnmDegree.College:
                    return "大专";
                case EnmDegree.Bachelor:
                    return "本科";
                case EnmDegree.Master:
                    return "硕士";
                case EnmDegree.Doctor:
                    return "博士";
                default:
                    return "其他";
            }
        }

        public static string GetMarriageDisplay(EnmMarriage marriage) {
            switch (marriage) {
                case EnmMarriage.Single:
                    return "单身";
                case EnmMarriage.Married:
                    return "已婚";
                default:
                    return "其他";
            }
        }

        public static string GetRecRemarkDisplay(EnmRecRemark remark) {
            switch (remark) {
                case EnmRecRemark.Remark0:
                    return "刷卡开门记录";
                case EnmRecRemark.Remark1:
                    return "键入用户ID及个人密码开门的记录";
                case EnmRecRemark.Remark2:
                    return "远程(由SU)开门记录";
                case EnmRecRemark.Remark3:
                    return "手动开门记录";
                case EnmRecRemark.Remark4:
                    return "联动开门记录";
                case EnmRecRemark.Remark5:
                    return "报警 (或报警取消) 记录";
                case EnmRecRemark.Remark6:
                    return "SM掉电记录";
                case EnmRecRemark.Remark7:
                    return "内部控制参数被修改的记录";
                case EnmRecRemark.Remark8:
                    return "无效的用户卡刷卡记录";
                case EnmRecRemark.Remark9:
                    return "用户卡的有效期已过";
                case EnmRecRemark.Remark10:
                    return "当前时间该用户卡无进入权限";
                case EnmRecRemark.Remark11:
                    return "用户在个人密码确认时，三次全部不正确";
                case EnmRecRemark.Remark34:
                    return "有效的消防联动输入";
                default:
                    return "未定义";
            }
        }

        public static string GetRecTypeDisplay(EnmRecType type) {
            switch (type) {
                case EnmRecType.Normal:
                    return "正常开门";
                case EnmRecType.Illegal:
                    return "非法开门";
                case EnmRecType.Remote:
                    return "远程开门";
                default:
                    return "未定义";
            }
        }

        public static string GetPointParamDisplay(EnmPointParam param) {
            switch (param) {
                case EnmPointParam.AbsThreshold:
                    return "绝对阈值";
                case EnmPointParam.PerThreshold:
                    return "百分比阈值";
                case EnmPointParam.SavedPeriod:
                    return "存储周期";
                case EnmPointParam.StorageRefTime:
                    return "存储参考时间";
                case EnmPointParam.AlarmLimit:
                    return "告警门限值";
                case EnmPointParam.AlarmLevel:
                    return "告警等级";
                case EnmPointParam.AlarmDelay:
                    return "告警延迟";
                case EnmPointParam.AlarmRecoveryDelay:
                    return "告警恢复延迟";
                case EnmPointParam.AlarmFiltering:
                    return "告警过滤";
                case EnmPointParam.AlarmInferior:
                    return "主次告警";
                case EnmPointParam.AlarmConnection:
                    return "关联告警";
                case EnmPointParam.AlarmReversal:
                    return "告警翻转";
                default:
                    return "未定义";
            }
        }

        public static Color GetAlarmColor(EnmAlarm level) {
            switch (level) {
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
            switch (status) {
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
            return CommonHelper.SplitCondition(conditions);
        }

        public static string JoinCondition(params string[] conditions) {
            return CommonHelper.JoinCondition(conditions);
        }

        public static string[] SplitKeys(string key) {
            return CommonHelper.SplitKeys(key);
        }

        public static string JoinKeys(params object[] keys) {
            return CommonHelper.JoinKeys(keys);
        }

        public static Kv<EnmSSH, string> ParseNode(string node) {
            if ("root".Equals(node, StringComparison.CurrentCultureIgnoreCase))
                return new Kv<EnmSSH, string>(EnmSSH.Root, node);

            var keys = Common.SplitKeys(node);
            if (keys.Length != 2) throw new iPemException("参数格式错误");
            if (!Enum.IsDefined(typeof(EnmSSH), int.Parse(keys[0]))) throw new iPemException("无效的参数");
            return new Kv<EnmSSH, string>((EnmSSH)(int.Parse(keys[0])), keys[1]);
        }

        public static string GetNodeName(string node, string vendor) {
            if (string.IsNullOrWhiteSpace(node)) node = "未知";
            if (string.IsNullOrWhiteSpace(vendor)) return node;
            return string.Format("{0}[{1}]", node, vendor);
        }

        public static bool SetAllowUnsafeHeaderParsing() {
            //Get the assembly that contains the internal class
            var aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            if (aNetAssembly != null) {
                //Use the assembly in order to get the internal type for the internal class
                var aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
                if (aSettingsType != null) {
                    //Use the internal static property to get an instance of the internal settings class.
                    //If the static instance isn't created allready the property will create it for us.
                    var anInstance = aSettingsType.InvokeMember("Section", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });
                    if (anInstance != null) {
                        //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
                        var aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null) {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsSystemAlarm(string flag) {
            return IsSystemSCAlarm(flag) || IsSystemFSUAlarm(flag);
        }

        public static bool IsSystemSCAlarm(string flag) {
            return "-1".Equals(flag);
        }

        public static bool IsSystemFSUAlarm(string flag) {
            return "-2".Equals(flag);
        }

        public static string GetResStatusDisplay(EnmResult status) {
            switch (status) {
                case EnmResult.Failure:
                    return "未通过";
                case EnmResult.Success:
                    return "已通过";
                default:
                    return "未审核";
            }
        }

        public static string GetSSHDisplay(EnmSSH type) {
            switch (type) {
                case EnmSSH.Root:
                    return "全部";
                case EnmSSH.Area:
                    return "区域";
                case EnmSSH.Station:
                    return "站点";
                case EnmSSH.Room:
                    return "机房";
                case EnmSSH.Fsu:
                    return "FSU";
                case EnmSSH.Device:
                    return "设备";
                case EnmSSH.Point:
                    return "信号";
                default:
                    return "未定义";
            }
        }

        public static bool ValidateFormula(string formula) {
            if (string.IsNullOrWhiteSpace(formula)) return false;
            formula = Regex.Replace(formula, @"\s+", "");

            if (Regex.IsMatch(formula, @"[\+\-\*\/]{2,}")) return false;
            if (Regex.IsMatch(formula, @"[\+\-\*\/]{2,}")) return false;

            var stack = new Stack<char>();
            foreach (var letter in formula) {
                if (letter == '(') {
                    stack.Push('(');
                } else if (letter == ')') {
                    if (stack.Count == 0) return false;
                    stack.Pop();
                }
            }

            if (stack.Count != 0) return false;
            if (Regex.IsMatch(formula, @"\([\+\-\*\/]")) return false;
            if (Regex.IsMatch(formula, @"[\+\-\*\/]\)")) return false;
            if (Regex.IsMatch(formula, @"[^\+\-\*\/\(]\(")) return false;
            if (Regex.IsMatch(formula, @"\)[^\+\-\*\/\)]")) return false;

            formula = Regex.Replace(formula, @"\(|\)", "");
            formula = Regex.Replace(formula, @"[\+\-\*\/]", CommonHelper.GlobalSeparator);
            var variables = SplitKeys(formula);
            foreach (var variable in variables) {
                if (Regex.IsMatch(variable, @"^\d+(\.\d+)?$")) continue;
                if (!Regex.IsMatch(variable, @"^@.+>>.+>>.+$")) return false;
                var starts = Regex.Matches(variable, @"@");
                if (starts.Count != 1) return false;
                var separators = Regex.Matches(variable, @">>");
                if (separators.Count != 2) return false;
            }

            return true;
        }

        public static List<string> GetFormulaVariables(string formula) {
            var result = new List<string>();
            if (string.IsNullOrWhiteSpace(formula)) 
                return result;

            formula = Regex.Replace(formula, @"\s+", "");
            formula = Regex.Replace(formula, @"\(|\)", "");
            formula = Regex.Replace(formula, @"[\+\-\*\/]", CommonHelper.GlobalSeparator);
            var variables = SplitKeys(formula);
            foreach (var variable in variables) {
                if (Regex.IsMatch(variable, @"^\d+(\.\d+)?$")) continue;
                if (result.Contains(variable)) continue;
                result.Add(variable);
            }

            return result;
        }

        public static void CheckPeriods(DateTime start, DateTime end, EnmPDH period) {
            if (end < start)
                throw new ArgumentException("结束时间必须大于开始时间");

            if (period == EnmPDH.Hour && end.Subtract(start).TotalDays > 1)
                throw new ArgumentException("按时统计最大支持1天");

            if (period == EnmPDH.Day && end.Subtract(start).TotalDays > 31)
                throw new ArgumentException("按日统计最大支持1个月");

            if (period == EnmPDH.Week && end.Subtract(start).TotalDays > 186)
                throw new ArgumentException("按周统计最大支持6个月");

            if (period == EnmPDH.Month && end.Subtract(start).TotalDays > 366)
                throw new ArgumentException("按月统计最大支持12个月");

            if (period == EnmPDH.Year && end.Subtract(start).TotalDays > 732)
                throw new ArgumentException("按月统计最大支持2年");
        }
    }
}