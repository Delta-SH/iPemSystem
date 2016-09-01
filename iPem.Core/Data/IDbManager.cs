using System;
using System.Collections.Generic;
using iPem.Core.Enum;

namespace iPem.Core.Data {
    /// <summary>
    /// Database manager interface
    /// </summary>
    public interface IDbManager {
        /// <summary>
        /// Initialize database
        /// </summary>
        void Initializer();

        /// <summary>
        /// Clean database
        /// </summary>
        void Clean();

        /// <summary>
        /// A value indicating whether all databases has been installed
        /// </summary>
        bool DatabaseIsInstalled();

        /// <summary>
        /// A value indicating whether all databases has been configured
        /// </summary>
        bool IsValid(EnmDbType dt);

        /// <summary>
        /// Gets or sets the current databases
        /// </summary>
        Dictionary<EnmDbType, DbEntity> CurrentDbSets { get; }

        /// <summary>
        /// Gets or sets the current connection
        /// </summary>
        Dictionary<EnmDbType, string> CurrentConnetions { get; }
    }
}
