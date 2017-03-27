using System;

namespace iPem.Core {
    /// <summary>
    /// Represents an application store
    /// </summary>
    [Serializable]
    public partial class iPemStore {
        /// <summary>
        /// Gets or sets the application identifier
        /// </summary>
        public Guid Id {
            get { return new Guid("CBC23C46-D14D-4B05-99DB-243826013069"); }
        }

        /// <summary>
        /// Gets or sets the application name
        /// </summary>
        public string Name {
            get { return "智能监控管理系统"; }
        }

        /// <summary>
        /// Gets or sets the application version
        /// </summary>
        public string Version {
            get { return "V1.0.0 Build170325"; }
        }

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        public string CompanyName {
            get { return "中达电通股份有限公司"; }
        }

        /// <summary>
        /// Gets or sets the copyright
        /// </summary>
        public string Copyright {
            get { return "中达电通股份有限公司｜台达集团 版权所有 ©2011-2017"; }
        }

        /// <summary>
        /// Gets or sets the copyright
        /// </summary>
        public string Copyright_Us {
            get { return "Delta GreenTech(China) Co., Ltd. All Rights Reserved"; }
        }

        /// <summary>
        /// Gets or sets the browser requirements
        /// </summary>
        public string Requirements {
            get { return "浏览器：IE10+、Firefox、Chrome"; }
        }

        /// <summary>
        /// Gets or sets the application location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the application host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the application port
        /// </summary>
        public int Port { get; set; }
    }
}
