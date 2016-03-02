using System;
using iPem.Core.Enum;

namespace iPem.Core.Data {
    /// <summary>
    /// Represents a database
    /// </summary>
    public partial class DbEntity : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Database Provider
        /// </summary>
        public EnmDbProvider Provider { get; set; }

        /// <summary>
        /// Database Type
        /// </summary>
        public EnmDatabaseType Type { get; set; }

        /// <summary>
        /// Database IP
        /// </summary>
        public String IP { get; set; }

        /// <summary>
        /// Database Port
        /// </summary>
        public Int32 Port { get; set; }

        /// <summary>
        /// Database UId
        /// </summary>
        public String UId { get; set; }

        /// <summary>
        /// Database Password
        /// </summary>
        public String Pwd { get; set; }

        /// <summary>
        /// Database Name
        /// </summary>
        public String Name { get; set; }
    }
}