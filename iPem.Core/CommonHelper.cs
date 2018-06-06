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
    /// ����������
    /// </summary>
    public partial class CommonHelper {
        public static string GlobalSeparator {
            get { return "��"; }
        }

        /// <summary>
        /// ȫ�ֱ���
        /// </summary>
        private static PerformanceCounter cpuCounter;
        private static GregorianCalendar calendar;
        private static AspNetHostingPermissionLevel? _trustLevel;

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static CommonHelper() {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            calendar = new GregorianCalendar();
        }

        /// <summary>
        /// ����ָ����Χ�������
        /// </summary>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue) {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// ��ȡӦ�ó�������Ȩ��
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
        /// ����ת��
        /// </summary>
        public static T To<T>(object value) {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Ӧ�ó��򱾵ػ�
        /// </summary>
        public static void SetCulture(CultureInfo culture = null) {
            if (culture == null) culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// �����·��ת��Ϊ����·��
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
        /// ����ָ�������ڵ���ĵڼ���
        /// </summary>
        public static string WeekConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return string.Format("{0}��{1}��", date.Year, calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday));
        }

        /// <summary>
        /// ����ָ����ʽ�������ַ���
        /// yyyy-MM
        /// </summary>
        public static string MonthConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("yyyy-MM");
        }

        /// <summary>
        /// ����ָ����ʽ�������ַ���
        /// yyyy-MM
        /// </summary>
        public static string DayConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("MM-dd");
        }

        /// <summary>
        /// ����ָ����ʽ�������ַ���
        /// yyyy-MM-dd
        /// </summary>
        public static string DateConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// ����ָ����ʽ������ʱ���ַ���
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string DateTimeConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ����ָ����ʽ��ʱ���ַ���
        /// HH:mm:ss
        /// </summary>
        public static string TimeConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("HH:mm:ss");
        }

        /// <summary>
        /// ����ָ����ʽ��ʱ���ַ���
        /// mm��ss��
        /// </summary>
        public static string ShortTimeConverter(DateTime date) {
            if (!IsValidDateTime(date))
                return string.Empty;

            return date.ToString("mm��ss��");
        }

        /// <summary>
        /// ����ָ����ʽ��ʱ����ַ���
        /// dddd.HH:mm:ss
        /// </summary>
        public static string IntervalConverter(DateTime start, DateTime? end = null) {
            if (start == default(DateTime)) { return String.Empty; }
            if (!end.HasValue) { end = DateTime.Now; }
            var ts = end.Value.Subtract(start);
            return String.Format("{0:0000}.{1:00}:{2:00}:{3:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
        }

        /// <summary>
        /// ����ָ����ʽ��ʱ����ַ���
        /// dddd.HH:mm:ss
        /// </summary>
        public static string IntervalConverter(TimeSpan span) {
            return String.Format("{0:0000}.{1:00}:{2:00}:{3:00}", span.Days, span.Hours, span.Minutes, span.Seconds);
        }

        /// <summary>
        /// ����ָ����ʽ�����ڶ��ַ���
        /// yyyy-MM-dd ~ yyyy-MM-dd
        /// </summary>
        public static string PeriodConverter(DateTime start, DateTime end) {
            if (start == end) return DateConverter(start);
            return string.Format("{0} ~ {1}", DateConverter(start), DateConverter(end));
        }

        /// <summary>
        /// ����ָ����ʽ������ʱ����ַ���
        /// yyyy-MM-dd HH:mm:ss ~ yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string PeriodWithTimeConverter(DateTime start, DateTime end) {
            if (start == end) return DateTimeConverter(start);
            return string.Format("{0} ~ {1}", DateTimeConverter(start), DateTimeConverter(end));
        }

        /// <summary>
        /// ���ʱ������
        /// </summary>
        public static List<Period> GetPeriods(DateTime start, DateTime end, EnmPDH period) {
            var dates = new List<Period>();
            if (period == EnmPDH.Month) {
                while (start <= end) {
                    var date = new DateTime(start.Year, start.Month, 1).AddMonths(1).AddSeconds(-1);
                    if (date > end) date = end;
                    dates.Add(new Period { Name = MonthConverter(start), Start = start, End = date });
                    start = date.AddSeconds(1);
                }
            } else if (period == EnmPDH.Week) {
                while (start <= end) {
                    var date = start.Date.AddSeconds((8 - (int)start.DayOfWeek) * 86400 - 1);
                    if (date > end) date = end;
                    dates.Add(new Period { Name = WeekConverter(start), Start = start, End = date });
                    start = date.AddSeconds(1);
                }
            } else if (period == EnmPDH.Day) {
                while (start <= end) {
                    var date = start.Date.AddSeconds(86399);
                    if (date > end) date = end;
                    dates.Add(new Period { Name = DateConverter(start), Start = start, End = date });
                    start = date.AddSeconds(1);
                }
            } else if (period == EnmPDH.Hour) {
                while (start <= end) {
                    var date = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0).AddSeconds(3599);
                    if (date > end) date = end;
                    dates.Add(new Period { Name = TimeConverter(start), Start = start, End = date });
                    start = date.AddSeconds(1);
                }
            }

            return dates;
        }

        /// <summary>
        /// ʱ����֤
        /// </summary>
        private static bool IsValidDateTime(DateTime date) {
            if (date == default(DateTime))
                return false;

            /*
             * ���JsonSerializer.DeserializeFromString����ת��Ĭ��ʱ��[default(DateTime)]���Զ�����ʱ��(+8H)
             * ��ʹ��Redis����ʱ��NServiceKitĬ�ϻ�ʹ��JsonSerializer.SerializeToString������Ҫ��������ݽ���Json���л�
             * �ڻ�ȡ����ʱ��NServiceKitĬ�ϻ�ʹ��JsonSerializer.DeserializeFromString�������ѻ�������ݽ���Json�����л�
             * ���Ὣ0001-01-01 00:00:00 �����к�Ϊ 0001-01-01 08:00:00
             */
            //if (date == new DateTime(1, 1, 1, 8, 0, 0))
            //    return false;

            return true;
        }

        /// <summary>
        /// ����ָ�����ַ���
        /// </summary>
        /// <param name="plainText">��Ҫ���ܵ��ַ���</param>
        /// <param name="saltkey">ɢ����</param>
        /// <param name="format">���ܷ�ʽ(SHA1)</param>
        /// <returns>text hash</returns>
        public static string CreateHash(string plainText, string saltkey = "", string format = "SHA1") {
            var text = String.Concat(plainText, saltkey);

            if (String.IsNullOrWhiteSpace(format))
                format = "SHA1";

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(text, format);
            var algorithm = HashAlgorithm.Create(format);
            if (algorithm == null) throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// ���ָ�����������Ƿ����ָ�����ַ���
        /// </summary>
        /// <param name="target">��������ַ���</param>
        /// <param name="source">�����������</param>
        /// <returns>true/false</returns>
        public static bool ConditionContain(string target, string[] source) {
            if (target == null || source == null)
                return false;

            foreach (var src in source) {
                if (target.ToLowerInvariant().Contains(src.ToLowerInvariant()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// ����ȷ������
        /// </summary>
        public static string CreateDynamicKeys(int length = 6) {
            if (length > 16) throw new ArgumentException("���ȳ�����Χ�����֧��16���ַ�����");
            var today = DateTime.Today;
            var even = today.Day % 2 == 0;
            var factor = today.Year * 10000 + today.Month * 100 + today.Day;
            var salt = even ? 621 : 325;
            var key = string.Format("TKac4SRTQi%rsDixVlfM3w{0}PVF+t^0iNOonZUyTZ5Hcxg=={2}==g+T3Cpu/FDpUjxOsUrt&xw{1}CuCEh2cH#kFqetg9Z+1wUw", even ? today.Day : today.Month, even ? today.Month : today.Day, factor + salt);
            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key));
            if (bytes == null) throw new Exception("������Կʱ��������");
            key = string.Join("", bytes.Reverse());
            return key.Substring(key.Length > (today.Day + today.Month + length) ? (today.Day + today.Month) : (even ? 0 : (key.Length - length)), length);
        }

        /// <summary>
        /// ��ָ�����ַ���ת����������
        /// </summary>
        public static byte[] ConvertTextToSpeech(string word) {
            byte[] bytes = null;
            var task = new Thread(() => {
                try {
                    using (var speaker = new SpeechSynthesizer()) {
                        using (var stream = new MemoryStream()) {
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
        /// ��ȡCPUʹ����
        /// </summary>
        public static double GetCpuUsage() {
            var rate = cpuCounter.NextValue();
            return Math.Round(rate, 2);
        }

        /// <summary>
        /// ��ȡ�ڴ�ʹ����
        /// </summary>
        public static double GetMemoryUsage() {
            var computer = new Microsoft.VisualBasic.Devices.ComputerInfo();
            var rate = (double)computer.AvailablePhysicalMemory / (double)computer.TotalPhysicalMemory;
            return Math.Round((1d - rate) * 100, 2);
        }

        /// <summary>
        /// �����ˮ��
        /// </summary>
        public static string GetIdAsString() {
            return GetIdAsLong().ToString();
        }

        /// <summary>
        /// �����ˮ��
        /// </summary>
        public static long GetIdAsLong() {
            return Math.Abs(DateTime.Now.Subtract(new DateTime(2017, 6, 21)).Ticks);
        }

        /// <summary>
        /// ���ˢ��ˢ������
        /// </summary>
        public static EnmRecType GetRecType(EnmRecRemark remark) {
            //ˢ�����ż�¼�������û�ID���������뿪�ŵļ�¼
            var normals = new EnmRecRemark[] { EnmRecRemark.Remark0, EnmRecRemark.Remark1 };

            //�ֶ����ż�¼���������ż�¼����Ч���û���ˢ����¼���û�������Ч���ѹ�����ǰʱ����û����޽���Ȩ�ޡ��û��ڸ�������ȷ��ʱ������ȫ������ȷ
            var illegals = new EnmRecRemark[] { EnmRecRemark.Remark3, EnmRecRemark.Remark4, EnmRecRemark.Remark8, EnmRecRemark.Remark9, EnmRecRemark.Remark10, EnmRecRemark.Remark11 };

            //Զ��(��SU)���ż�¼
            var remotes = new EnmRecRemark[] { EnmRecRemark.Remark2 };

            if (normals.Contains(remark)) return EnmRecType.Normal;
            if (illegals.Contains(remark)) return EnmRecType.Illegal;
            if (remotes.Contains(remark)) return EnmRecType.Remote;
            return EnmRecType.Other;
        }

        /// <summary>
        /// ���л�����
        /// </summary>
        public static byte[] ObjectToBytes(object obj) {
            using (var memory = new MemoryStream()) {
                new BinaryFormatter().Serialize(memory, obj);
                return memory.GetBuffer();
            }
        }

        /// <summary>
        /// �����л�����
        /// </summary>
        public static T BytesToObject<T>(byte[] bytes) where T : class {
            using (var memory = new MemoryStream(bytes)) {
                memory.Position = 0;
                return new BinaryFormatter().Deserialize(memory) as T;
            }
        }

        /// <summary>
        /// ��DataTableת��ΪByte����
        /// </summary>
        public static byte[] DtToXml(DataTable dt) {
            using (var memory = new MemoryStream()) {
                dt.WriteXml(memory, XmlWriteMode.WriteSchema, true);
                return memory.GetBuffer();
            }
        }

        /// <summary>
        /// ��Byte����ת��ΪDataTable
        /// </summary>
        public static DataTable XmlToDt(byte[] bytes) {
            var dt = new DataTable();
            using (var memory = new MemoryStream()) {
                memory.Write(bytes, 0, bytes.Length);
                memory.Position = 0;
                dt.ReadXml(memory);
            }
            return dt;
        }

        /// <summary>
        /// ����Key
        /// </summary>
        public static string[] SplitKeys(string key) {
            if (string.IsNullOrWhiteSpace(key))
                return new string[0];

            return key.Split(new string[] { GlobalSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// ����Key
        /// </summary>
        public static string JoinKeys(params object[] keys) {
            if (keys == null || keys.Length == 0)
                return string.Empty;

            return string.Join(GlobalSeparator, keys);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public static string[] SplitCondition(string conditions) {
            if (string.IsNullOrWhiteSpace(conditions))
                return new string[0];

            return conditions.Split(new char[] { ';', '��' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public static string JoinCondition(params string[] conditions) {
            if (conditions == null || conditions.Length == 0)
                return string.Empty;

            return string.Join(";", conditions);
        }

        /// <summary>
        /// ����ļ�����
        /// </summary>
        public static string GetFileType(string file) {
            if (string.IsNullOrWhiteSpace(file)) return FileType.Undefined.Key;

            var ext = Path.GetExtension(file).ToLower();
            if (FileType.ZIP.Value.Contains(ext)) return FileType.ZIP.Key;
            if (FileType.IMG.Value.Contains(ext)) return FileType.IMG.Key;
            if (FileType.MUSIC.Value.Contains(ext)) return FileType.MUSIC.Key;
            if (FileType.VIDEO.Value.Contains(ext)) return FileType.VIDEO.Key;
            if (FileType.WORD.Value.Contains(ext)) return FileType.WORD.Key;
            if (FileType.EXCEL.Value.Contains(ext)) return FileType.EXCEL.Key;
            if (FileType.POWER.Value.Contains(ext)) return FileType.POWER.Key;
            if (FileType.EXE.Value.Contains(ext)) return FileType.EXE.Key;
            if (FileType.PDF.Value.Contains(ext)) return FileType.PDF.Key;
            if (FileType.TXT.Value.Contains(ext)) return FileType.TXT.Key;
            if (FileType.XML.Value.Contains(ext)) return FileType.XML.Key;
            if (FileType.HTML.Value.Contains(ext)) return FileType.HTML.Key;
            return FileType.Undefined.Key;
        }
    }
}