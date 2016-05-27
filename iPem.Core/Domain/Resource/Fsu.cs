using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents an fsu
    /// </summary>
    [Serializable]
    public partial class Fsu : Device {
        /// <summary>
        /// Gets or sets the ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public int Port { get; set; }

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

        /// <summary>
        /// Gets or sets the fsu comment
        /// </summary>
        public string FsuComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is online.
        /// </summary>
        public bool IsOnLine { get; set; }
    }
}
