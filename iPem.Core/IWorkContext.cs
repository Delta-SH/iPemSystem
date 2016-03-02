using iPem.Core.Domain.Master;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Core {
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext {
        /// <summary>
        /// Gets or sets whether the user has been authenticated
        /// </summary>
        Boolean IsAuthenticated { get; }
        
        /// <summary>
        /// Gets or sets the current identifier
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        Store Store { get; }

        /// <summary>
        /// Gets or sets the current role
        /// </summary>
        Role CurrentRole { get; }

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        User CurrentUser { get; }

        /// <summary>
        /// Gets or sets the current employee
        /// </summary>
        Employee CurrentEmployee { get; }
    }
}
