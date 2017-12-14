using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace iPem.Core {
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public partial class CommonHelper {
        public static string GlobalSeparator {
            get { return "┆"; }
        }

        /// <summary>
        /// 全局变量
        /// </summary>
        private static PerformanceCounter cpuCounter;
        private static GregorianCalendar calendar;
        private static AspNetHostingPermissionLevel? _trustLevel;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CommonHelper() {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            calendar = new GregorianCalendar();
        }

        /// <summary>
        /// 生成指定范围的随机数
        /// </summary>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue) {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// 获取应用程序的最高权限
        /// </summary>
        public static AspNetHostingPermissionLevel GetTrustLevel() {
            if (!_trustLevel.HasValue) {
                _trustLevel = AspNetHostingPermissionLevel.None;
                foreach (var trustLevel in new[] { AspNetHostingPermissionLevel.Unrestricted, AspNetHostingPermissionLevel.High, AspNetHostingPermissionLevel.Medium, AspNetHostingPermissionLevel.Low, AspNetHostingPermissionLevel.Minimal }) {
                    try {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break;
                    } catch (System.Security.SecurityException) {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        public static T To<T>(object value) {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 应用程序本地化
        /// </summary>
        public static void SetCulture(CultureInfo culture = null) {
            if (culture == null) culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// 将相对路径转换为绝对路径
        /// </summary>
        public static string MapPath(string path) {
            if (HostingEnvironment.IsHosted) {
                return HostingEnvironment.MapPath(path);
            }

            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        /// <summary>
        /// 返回指定日期在当年的第几周
        /// </summary>
        public static string WeekConverter(DateTime date) {
            if(!IsValidDateTime(date))
                return string.Empty;

            return string.Format("{0}年{1}周", date.Year, calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday));
        }

        /// <summary>
        /// 返回指定格式的日期字符串
        /// yyyy-MM
        /// </summary>
        public static string MonthConverter(DateTime date) {
            if(!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("yyyy-MM");
        }

        /// <summary>
        /// 返回指定格式的日期字符串
        /// yyyy-MM-dd
        /// </summary>
        public static string DateConverter(DateTime date) {
            if(!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定格式的日期时间字符串
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string DateTimeConverter(DateTime date) {
            if(!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回指定格式的时间字符串
        /// HH:mm:ss
        /// </summary>
        public static string TimeConverter(DateTime date) {
            if(!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回指定格式的时间字符串
        /// mm′ss″
        /// </summary>
        public static string ShortTimeConverter(DateTime date) {
            if(!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("mm′ss″");
        }

        /// <summary>
        /// 返回指定格式的时间段字符串
        /// dddd.HH:mm:ss
        /// </summary>
        public static string IntervalConverter(DateTime start, DateTime? end = null) {
            if(start == default(DateTime)) { return String.Empty; }
            if(!end.HasValue) { end = DateTime.Now; }
            var ts = end.Value.Subtract(start);
            return String.Format("{0:0000}.{1:00}:{2:00}:{3:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
        }

        /// <summary>
        /// 返回指定格式的时间段字符串
        /// dddd.HH:mm:ss
        /// </summary>
        public static string IntervalConverter(TimeSpan span) {
            return String.Format("{0:0000}.{1:00}:{2:00}:{3:00}", span.Days, span.Hours, span.Minutes, span.Seconds);
        }

        /// <summary>
        /// 时间验证
        /// </summary>
        private static bool IsValidDateTime(DateTime date) {
            if (date == default(DateTime))
                return false;

            /*
             * 解决JsonSerializer.DeserializeFromString方法转换默认时间[default(DateTime)]会自动加上时区(+8H)
             * 在使用Redis缓存时，NServiceKit默认会使用JsonSerializer.SerializeToString方法对要缓存的数据进行Json序列化
             * 在获取缓存时，NServiceKit默认会使用JsonSerializer.DeserializeFromString方法对已缓存的数据进行Json反序列化
             * 它会将0001-01-01 00:00:00 反序列号为 0001-01-01 08:00:00
             */
            //if (date == new DateTime(1, 1, 1, 8, 0, 0))
            //    return false;

            return true;
        }

        /// <summary>
        /// 加密指定的字符串
        /// </summary>
        /// <param name="plainText">需要加密的字符串</param>
        /// <param name="saltkey">散列盐</param>
        /// <param name="format">加密方式(SHA1)</param>
        /// <returns>text hash</returns>
        public static string CreateHash(string plainText, string saltkey = "", string format = "SHA1") {
            var text = String.Concat(plainText, saltkey);

            if(String.IsNullOrWhiteSpace(format))
                format = "SHA1";

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(text, format);
            var algorithm = HashAlgorithm.Create(format);
            if(algorithm == null) throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// 检查指定的数组中是否包含指定的字符串
        /// </summary>
        /// <param name="target">带检验的字符串</param>
        /// <param name="source">带检验的数组</param>
        /// <returns>true/false</returns>
        public static bool ConditionContain(string target, string[] source) {
            if(target == null || source == null) 
                return false;

            foreach(var src in source) {
                if(target.ToLowerInvariant().Contains(src.ToLowerInvariant()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 生成确认密码
        /// </summary>
        public static string CreateDynamicKeys(int length = 6) {
            if (length > 16) throw new ArgumentException("长度超出范围（最大支持16个字符）。");
            var today = DateTime.Today;
            var even = today.Day % 2 == 0;
            var factor = today.Year * 10000 + today.Month * 100 + today.Day;
            var salt = even ? 621 : 325;
            var key = string.Format("TKac4SRTQi%rsDixVlfM3w{0}PVF+t^0iNOonZUyTZ5Hcxg=={2}==g+T3Cpu/FDpUjxOsUrt&xw{1}CuCEh2cH#kFqetg9Z+1wUw", even ? today.Day : today.Month, even ? today.Month : today.Day, factor + salt);
            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key));
            if (bytes == null) throw new Exception("生成密钥时发生错误。");
            key = string.Join("", bytes.Reverse());
            return key.Substring(key.Length > (today.Day + today.Month + length) ? (today.Day + today.Month) : (even ? 0 : (key.Length - length)), length);
        }

        /// <summary>
        /// 将指定的字符串转换成语音流
        /// </summary>
        public static byte[] ConvertTextToSpeech(string word) {
            byte[] bytes = null;
            var task = new Thread(() => {
                try {
                    using(var speaker = new SpeechSynthesizer()) {
                        using(var stream = new MemoryStream()) {
                            speaker.SetOutputToWaveStream(stream);
                            speaker.Speak(word);
                            bytes = stream.ToArray();
                        }
                    }
                } catch { }
            });

            task.Start();
            task.Join();
            return bytes;
        }

        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        public static double GetCpuUsage() {
            var rate = cpuCounter.NextValue();
            return Math.Round(rate, 2);
        }

        /// <summary>
        /// 获取内存使用率
        /// </summary>
        public static double GetMemoryUsage() {
            var computer = new Microsoft.VisualBasic.Devices.ComputerInfo();
            var rate = (double)computer.AvailablePhysicalMemory / (double)computer.TotalPhysicalMemory;
            return Math.Round((1d - rate) * 100, 2);
        }

        /// <summary>
        /// 获得流水号
        /// </summary>
        public static string GetIdAsString() {
            return GetIdAsLong().ToString();
        }

        /// <summary>
        /// 获得流水号
        /// </summary>
        public static long GetIdAsLong() {
            return Math.Abs(DateTime.Now.Subtract(new DateTime(2017, 6, 21)).Ticks);
        }

        /// <summary>
        /// 获得刷卡刷卡类型
        /// </summary>
        public static EnmRecType GetRecType(EnmRecRemark remark) {
            //刷卡开门记录、键入用户ID及个人密码开门的记录
            var normals = new EnmRecRemark[] { EnmRecRemark.Remark0, EnmRecRemark.Remark1 };

            //手动开门记录、联动开门记录、无效的用户卡刷卡记录、用户卡的有效期已过、当前时间该用户卡无进入权限、用户在个人密码确认时，三次全部不正确
            var illegals = new EnmRecRemark[] { EnmRecRemark.Remark3, EnmRecRemark.Remark4, EnmRecRemark.Remark8, EnmRecRemark.Remark9, EnmRecRemark.Remark10, EnmRecRemark.Remark11 };

            //远程(由SU)开门记录
            var remotes = new EnmRecRemark[] { EnmRecRemark.Remark2 };

            if (normals.Contains(remark)) return EnmRecType.Normal;
            if (illegals.Contains(remark)) return EnmRecType.Illegal;
            if (remotes.Contains(remark)) return EnmRecType.Remote;
            return EnmRecType.Other;
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        public static byte[] ObjectToBytes(object obj){
            using (var memory = new MemoryStream()) {
                new BinaryFormatter().Serialize(memory, obj);
                return memory.GetBuffer();
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        public static T BytesToObject<T>(byte[] bytes) where T : class {
            using (var memory = new MemoryStream(bytes)) {
                memory.Position = 0;
                return new BinaryFormatter().Deserialize(memory) as T;
            }
        }

        /// <summary>
        /// 解析Key
        /// </summary>
        public static string[] SplitKeys(string key) {
            if (string.IsNullOrWhiteSpace(key))
                return new string[0];

            return key.Split(new string[] { GlobalSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 生成Key
        /// </summary>
        public static string JoinKeys(params object[] keys) {
            if (keys == null || keys.Length == 0)
                return string.Empty;

            return string.Join(GlobalSeparator, keys);
        }

        /// <summary>
        /// 解析条件
        /// </summary>
        public static string[] SplitCondition(string conditions) {
            if (string.IsNullOrWhiteSpace(conditions))
                return new string[0];

            return conditions.Split(new char[] { ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 生成条件
        /// </summary>
        public static string JoinCondition(params string[] conditions) {
            if (conditions == null || conditions.Length == 0)
                return string.Empty;

            return string.Join(";", conditions);
        }
    }
}