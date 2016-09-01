using System;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents a notice
    /// </summary>
    [Serializable]
    public class NoticeInUser : BaseEntity {
        /// <summary>
        /// Gets or sets the notice identifier
        /// </summary>
        public Guid NoticeId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been readed
        /// </summary>
        public bool Readed { get; set; }

        /// <summary>
        /// Gets or sets the read time
        /// </summary>
        public DateTime ReadTime { get; set; }
    }
}
