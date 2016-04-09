using System;
using System.Collections.Generic;

namespace iPem.Core.Data {
    /// <summary>
    /// Data provider interface
    /// </summary>
    public interface IDataProvider {
        /// <summary>
        /// Gets database entities.
        /// </summary>
        /// <returns>all the database entities.</returns>
        IList<DbEntity> GetEntites();

        /// <summary>
        /// Save database entities.
        /// </summary>
        void SaveEntites(IList<DbEntity> entities);

        /// <summary>
        /// Delete database entities.
        /// </summary>
        void DelEntites(IList<DbEntity> entities);

        /// <summary>
        /// Clean database entities.
        /// </summary>
        void CleanEntites();
    }
}