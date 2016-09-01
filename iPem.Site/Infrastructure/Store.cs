using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Site.Models;
using System;
using System.Collections.Generic;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// Represents a store
    /// </summary>
    [Serializable]
    public partial class Store {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the current role
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the current employee
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the current user profile
        /// </summary>
        public ProfileValues Profile { get; set; }

        /// <summary>
        /// Gets or sets the webservice values
        /// </summary>
        public WsValues WsValues { get; set; }

        /// <summary>
        /// Gets or sets the expire utc time
        /// </summary>
        public DateTime ExpireUtc { get; set; }

        /// <summary>
        /// Gets or sets the created utc time
        /// </summary>
        public DateTime CreatedUtc { get; set; }
    }
}
