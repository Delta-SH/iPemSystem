using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents the areas in role
    /// </summary>
    [Serializable]
    public partial class AreasInRole : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the areas in role
        /// </summary>
        public List<string> AreaIds { get; set; }
    }
}