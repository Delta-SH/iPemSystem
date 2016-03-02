using System;
using iPem.Core.Enum;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents a web event record
    /// </summary>
    [Serializable]
    public partial class WebEvent : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the level
        /// </summary>
        public EnmEventLevel Level { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public EnmEventType Type { get; set; }

        /// <summary>
        /// Gets or sets the short message
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// Gets or sets the full exception
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the page URL
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the referrer URL
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}