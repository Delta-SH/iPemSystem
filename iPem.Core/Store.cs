using iPem.Core.Domain.Master;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Core {
    /// <summary>
    /// Represents a store
    /// </summary>
    [Serializable]
    public partial class Store : BaseEntity {
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
        /// Gets or sets the expire utc time
        /// </summary>
        public DateTime ExpireUtc { get; set; }

        /// <summary>
        /// Gets or sets the created utc time
        /// </summary>
        public DateTime CreatedUtc { get; set; }
    }
}
