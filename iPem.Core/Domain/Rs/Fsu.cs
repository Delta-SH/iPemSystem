using System;

namespace iPem.Core.Domain.Rs {
    [Serializable]
    public partial class Fsu : Device {
        /// <summary>
        /// Gets or sets the uid
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// Gets or sets the ftp uid
        /// </summary>
        public string FtpUid { get; set; }

        /// <summary>
        /// Gets or sets the ftp password
        /// </summary>
        public string FtpPwd { get; set; }

        /// <summary>
        /// Gets or sets the ftp file path
        /// </summary>
        public string FtpFilePath { get; set; }

        /// <summary>
        /// Gets or sets the ftp authority
        /// </summary>
        public int FtpAuthority { get; set; }
    }
}
