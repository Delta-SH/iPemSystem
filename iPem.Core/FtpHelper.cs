using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace iPem.Core {
    public partial class FtpHelper {
        /// <summary>
        /// 默认构造
        /// </summary>
        /// <param name="ip">FTP服务器地址</param>
        /// <param name="path">初始化目录</param>
        /// <param name="uid">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="passive">FTP主动/被动模式（主动:TRUE,被动：FALSE,默认值：FALSE）</param>
        public FtpHelper(string ip, string path, string uid, string password, bool passive = false) {
            this.IP = ip;
            this.Path = path;
            this.Uid = uid;
            this.Password = password;
            this.UsePassive = passive;
            this.URI = string.IsNullOrWhiteSpace(path) ? string.Format(@"ftp://{0}/", ip) : string.Format(@"ftp://{0}/{1}/", ip, path);
        }

        /// <summary>  
        /// FTP服务器地址  
        /// </summary>  
        public string URI { get; private set; }

        /// <summary>  
        /// FTP服务器IP  
        /// </summary>  
        public string IP { get; private set; }

        /// <summary>  
        /// FTP服务器默认目录  
        /// </summary>  
        public string Path { get; private set; }

        /// <summary>  
        /// FTP服务器登录用户名  
        /// </summary>  
        public string Uid { get; private set; }

        /// <summary>  
        /// FTP服务器登录密码  
        /// </summary>  
        public string Password { get; private set; }

        /// <summary>  
        /// 主动/被动模式
        /// </summary>  
        public bool UsePassive { get; private set; }

        /// <summary>  
        /// 建立FTP链接,返回响应对象  
        /// </summary>  
        /// <param name="uri">FTP地址</param>  
        /// <param name="method">操作命令</param>  
        /// <returns></returns>  
        private FtpWebResponse Open(string uri, string method) {
            var request = (FtpWebRequest)FtpWebRequest.Create(uri);
            request.UseBinary = true;
            request.UsePassive = this.UsePassive;
            request.KeepAlive = false;
            request.Method = method;
            request.Credentials = new NetworkCredential(this.Uid, this.Password);
            return (FtpWebResponse)request.GetResponse();
        }

        /// <summary>         
        /// 建立FTP链接,返回请求对象         
        /// </summary>        
        /// <param name="uri">FTP地址</param>         
        /// <param name="method">操作命令</param>         
        private FtpWebRequest OpenRequest(string uri, string method) {
            var request = (FtpWebRequest)FtpWebRequest.Create(uri);
            request.UseBinary = true;
            request.UsePassive = this.UsePassive;
            request.KeepAlive = false;
            request.Method = method;
            request.Credentials = new NetworkCredential(this.Uid, this.Password);
            return request;
        }

        /// <summary>
        /// 获取所有文件和文件夹信息
        /// </summary>
        /// <param name="directory">FTP当前目录的子目录，路径可以多级。例如：Web/Logs/xxx</param>
        public List<FtpItem> GetFtpItems(string directory = null) {
            var items = new List<FtpItem>();
            var url = string.IsNullOrWhiteSpace(directory) ? this.URI : this.URI + directory;
            using (var response = this.Open(url, WebRequestMethods.Ftp.ListDirectoryDetails)) {
                using (var reader = new StreamReader(response.GetResponseStream())) {
                    string line;
                    var windowsRegex = new Regex(@"^(\d+-\d+-\d+\s+\d+:\d+(?:AM|PM|上午|下午))\s+(<DIR>|\d+)\s+(.+)$", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    while ((line = reader.ReadLine()) != null) {
                        var wmatch = windowsRegex.Match(line);
                        if (wmatch.Groups.Count >= 4) {
                            var culture = CultureInfo.GetCultureInfo(wmatch.Groups[1].Value.Contains("午") ? "zh-CN" : "en-US");
                            
                            var item = new FtpItem();
                            item.IsDirectory = line.Contains("<DIR>");
                            item.CreatedTime = DateTime.ParseExact(wmatch.Groups[1].Value, "MM-dd-yy hh:mmtt", culture, DateTimeStyles.AllowWhiteSpaces);
                            item.Size = item.IsDirectory ? 0 : long.Parse(wmatch.Groups[2].Value);
                            item.Path = string.IsNullOrWhiteSpace(directory) ? wmatch.Groups[3].Value : string.Format(@"{0}/{1}", directory, wmatch.Groups[3].Value);
                            item.Name = wmatch.Groups[3].Value;
                            items.Add(item);
                            continue;
                        }

                        var linuxRegex = new Regex(@"^([d-])([rwxt-]{3}){3}\s+\d{1,}\s+.*?(\d{1,})\s+(\w+\s+\d{1,2}\s+(?:\d{4})?)(\d{1,2}:\d{2})?\s+(.+?)\s?$", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                        var lmatch = linuxRegex.Match(line);
                        if (lmatch.Groups.Count >= 7) {
                            var time = string.IsNullOrWhiteSpace(lmatch.Groups[5].Value) ? "00:00" : lmatch.Groups[5].Value;
                            var dates = lmatch.Groups[4].Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (dates.Length == 2) dates = new string[] { dates[0], dates[1], DateTime.Today.Year.ToString() };

                            var item = new FtpItem();
                            item.IsDirectory = lmatch.Groups[1].Value.Trim().Equals("d", StringComparison.CurrentCultureIgnoreCase);
                            item.CreatedTime = DateTime.ParseExact(string.Format("{0} {1}", string.Join(" ", dates), time), "MMM dd yyyy HH:mm", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.AllowWhiteSpaces);
                            item.Size = item.IsDirectory ? 0 : long.Parse(lmatch.Groups[3].Value);
                            item.Path = string.IsNullOrWhiteSpace(directory) ? lmatch.Groups[6].Value : string.Format(@"{0}/{1}", directory, lmatch.Groups[6].Value);
                            item.Name = lmatch.Groups[6].Value;
                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>         
        /// 获取指定目录的所有文件         
        /// </summary>
        /// <param name="directory">FTP当前目录的子目录，路径可以多级。例如：Web/Logs/xxx</param>
        public List<FtpItem> GetFtpFiles(string directory = null) {
            return this.GetFtpItems(directory).FindAll(f => !f.IsDirectory);
        }

        /// <summary>         
        /// 获取指定目录的一级子目录         
        /// </summary>
        /// <param name="directory">FTP当前目录的子目录，路径可以多级。例如：Web/Logs/xxx</param>
        public List<FtpItem> GetFtpDirectories(string directory = null) {
            return this.GetFtpItems(directory).FindAll(f => f.IsDirectory);
        }

        /// <summary>
        /// 指定目录是否存在指定的子目录或文件
        /// </summary>
        /// <param name="item">目录或文件名称</param>
        /// <param name="directory">FTP当前目录的子目录，路径可以多级。例如：Web/Logs/xxx</param>
        public bool Exist(string item, string directory = null) {
            return this.GetFtpItems(directory).Any(f => f.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>         
        /// 指定目录是否存在指定的一级子目录
        /// </summary>   
        /// <param name="item">子目录名称</param>
        /// <param name="directory">FTP当前目录的子目录，路径可以多级。例如：Web/Logs/xxx</param>
        public bool ExistDirectory(string item, string directory = null) {
            return this.GetFtpDirectories(directory).Any(f => f.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>         
        /// 当前目录是否存在指定的文件       
        /// </summary>      
        /// <param name="item">文件名称</param>
        /// <param name="directory">FTP当前目录的子目录，路径可以多级。例如：Web/Logs/xxx</param>
        public bool ExistFile(string item, string directory = null) {
            return this.GetFtpFiles(directory).Any(f => f.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>  
        /// 创建目录  
        /// </summary>
        public void CreateDirectory(string directory) {
            Open(this.URI + directory, WebRequestMethods.Ftp.MakeDirectory);
        }

        /// <summary>  
        /// 更改目录或文件名  
        /// </summary>  
        /// <param name="current">当前名称</param>  
        /// <param name="rename">修改后新名称</param>  
        public void ReName(string current, string rename) {
            var request = OpenRequest(this.URI + current, WebRequestMethods.Ftp.Rename);
            request.RenameTo = rename;
            var response = (FtpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// 删除目录(包括下面所有子目录和子文件) 
        /// </summary>
        public void RemoveDirectory(string directory) {
            var items = this.GetFtpItems(directory);
            foreach (var item in items) {
                if (item.IsDirectory)
                    RemoveDirectory(item.Path);
                else
                    DeleteFile(item.Path);
            }
            Open(this.URI + directory, WebRequestMethods.Ftp.RemoveDirectory);
        }

        /// <summary>    
        /// 删除文件    
        /// </summary>
        /// <param name="item">文件虚拟路径</param>
        public void DeleteFile(string item) {
            Open(this.URI + item, WebRequestMethods.Ftp.DeleteFile);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="localFile">本地文件路径</param>
        public void Upload(string localFile, string directory = null) {
            var file = new FileInfo(localFile);
            var request = OpenRequest(string.IsNullOrWhiteSpace(directory) ? this.URI + file.Name : string.Format("{0}{1}/{2}", this.URI, directory, file.Name), WebRequestMethods.Ftp.UploadFile);
            request.ContentLength = file.Length;

            var bufferSize = 2048;
            var buffer = new byte[bufferSize];
            int dataRead;
            using (var fs = file.OpenRead()) {
                using (var stream = request.GetRequestStream()) {
                    while ((dataRead = fs.Read(buffer, 0, bufferSize)) > 0) {
                        stream.Write(buffer, 0, dataRead);
                    }
                }
            }
        }

        /// <summary>  
        /// 文件下载
        /// </summary>
        public MemoryStream Download(string file) {
            var response = Open(this.URI + file, WebRequestMethods.Ftp.DownloadFile);
            var outStream = new MemoryStream();
            using (var ftpStream = response.GetResponseStream()) {
                var bufferSize = 2048;
                var buffer = new byte[bufferSize];
                int dataRead;
                while ((dataRead = ftpStream.Read(buffer, 0, bufferSize)) > 0) {
                    outStream.Write(buffer, 0, dataRead);
                }
            }
            return outStream;
        }
    }

    public partial class FtpItem {
        /// <summary>  
        /// 文件或目录名称  
        /// </summary>  
        public string Name { get; set; }

        /// <summary>  
        /// 是否为目录  
        /// </summary>  
        public bool IsDirectory { get; set; }

        /// <summary>  
        /// 文件大小  
        /// </summary>  
        public long Size { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>  
        /// 创建时间  
        /// </summary>  
        public DateTime CreatedTime { get; set; }
    }
}