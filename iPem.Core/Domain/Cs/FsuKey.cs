using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class FsuKey : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the changed time
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// Gets or sets the last time
        /// </summary>
        public DateTime LastTime { get; set; }

        /// <summary>
        ///  Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public bool Status { get; set; }
    }
}