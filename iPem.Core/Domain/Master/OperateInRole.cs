using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Core.Domain.Master {
    /// <summary>
    /// Represents the operate in role
    /// </summary>
    [Serializable]
    public partial class OperateInRole : BaseEntity {
        /// <summary>
        /// Gets or sets the role identifier
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the operate in role
        /// </summary>
        public List<EnmOperation> OperateIds { get; set; }
    }
}
