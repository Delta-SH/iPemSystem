using iPem.Core;
using System;
using System.Runtime.CompilerServices;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// Provides access to the singleton instance of the engine.
    /// </summary>
    public class EngineContext {

        #region Methods

        /// <summary>
        /// Initializes a static instance of the factory.
        /// </summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate = false) {
            if(Singleton<IEngine>.Instance == null || forceRecreate) {
                Singleton<IEngine>.Instance = new iPemEngine();
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine) {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton engine used to access services.
        /// </summary>
        public static IEngine Current {
            get {
                if(Singleton<IEngine>.Instance == null) {
                    Initialize();
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion

    }
}