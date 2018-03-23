using System;

namespace iPem.Core {
    /// <summary>
    /// 应用程序相关信息
    /// </summary>
    [Serializable]
    public partial class iPemStore {
        /// <summary>
        /// 应用程序标识
        /// </summary>
        public Guid Id {
            get { return new Guid("cbc23c46-d14d-4b05-99db-243826013069"); }
        }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Name {
            get { return "动力环境集中监控管理平台"; }
        }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string Version {
            get { return "V1.3.0 Build180310"; }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        public string CompanyName {
            get { return "中达电通股份有限公司"; }
        }

        /// <summary>
        /// 中文版权
        /// </summary>
        public string Copyright {
            get { return "中达电通股份有限公司｜台达集团 版权所有 ©2011-2018"; }
        }

        /// <summary>
        /// 英文版权
        /// </summary>
        public string Copyright_Us {
            get { return "Delta GreenTech(China) Co., Ltd. All Rights Reserved"; }
        }

        /// <summary>
        /// 浏览器要求
        /// </summary>
        public string Requirements {
            get { return "浏览器：IE10+、Firefox、Chrome"; }
        }

        /// <summary>
        /// 本地URL
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 本地主机名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 本地端口
        /// </summary>
        public int Port { get; set; }
    }
}
