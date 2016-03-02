using System;
using System.Collections.Generic;

namespace iPem.Core {
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the 
    /// various services composing the engine. Edit functionality, modules
    /// and implementations access most functionality through this 
    /// interface.
    /// </summary>
    public interface IEngine {
        /// <summary>
        /// Initialize components and plugins in the environment.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// Gets or sets the container manager
        /// </summary>
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Gets or sets the application store
        /// </summary>
        iPemStore AppStore { get; }

        /// <summary>
        /// Gets or sets the work stores
        /// </summary>
        IDictionary<Guid, Store> WorkStores { get; }
    }
}
