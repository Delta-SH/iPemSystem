using System;
using System.Collections.Generic;

namespace iPem.Core.Domain.Sc {
    /// <summary>
    /// Represents the menus in role
    /// </summary>
    [Serializable]
    public partial class MenusInRole : BaseEntity {
        /// <summary>
        /// Gets or sets the role identifier
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the menus in role
        /// </summary>
        public List<Menu> Menus { get; set; }
    }
}
