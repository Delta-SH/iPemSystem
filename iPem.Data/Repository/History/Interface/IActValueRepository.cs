using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.History {
    public partial interface IActValueRepository {

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="device">the device identifier</param>
        /// <returns>active value list</returns>
        List<ActValue> GetEntities(string device);

        /// <summary>
        /// Gets entities from the repository by the specific device identifier
        /// </summary>
        /// <param name="devices">the device identifier array</param>
        /// <returns>active value list</returns>
        List<ActValue> GetEntities(string[] devices);

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>active value list</returns>
        List<ActValue> GetEntities();

        /// <summary>
        /// Add an entity to the repository
        /// </summary>
        /// <param name="entity">entity</param>
        void Insert(ActValue entity); 

        /// <summary>
        /// Add the entities to the repository
        /// </summary>
        /// <param name="entities">entities</param>
        void Insert(List<ActValue> entities); 

        /// <summary>
        /// Clear the repository
        /// </summary>
        void Clear(); 

    }
}
