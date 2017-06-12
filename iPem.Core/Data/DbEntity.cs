using iPem.Core.Enum;
using System;

namespace iPem.Core.Data {
    /// <summary>
    /// Represents a database entity
    /// </summary>
    public partial class DbEntity : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the provider
        /// </summary>
        public EnmDbProvider Provider { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public EnmDbType Type { get; set; }

        /// <summary>
        /// Gets or sets the ip
        /// </summary>
        public String IP { get; set; }

        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public Int32 Port { get; set; }

        /// <summary>
        /// Gets or sets the uid
        /// </summary>
        public String Uid { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public String Name { get; set; }
    }
}