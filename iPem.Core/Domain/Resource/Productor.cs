using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents a productor
    /// </summary>
    public partial class Productor : BaseEntity {
        /// <summary>
        ///Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the engname
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// Gets or sets the phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the postalcode
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}