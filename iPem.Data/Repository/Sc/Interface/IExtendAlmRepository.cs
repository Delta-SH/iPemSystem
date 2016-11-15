using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IExtendAlmRepository {
        /// <summary>
        /// Gets the entities
        /// </summary>
        /// <returns>entities</returns>
        List<ExtAlm> GetEntities();

        /// <summary>
        /// Update the entities
        /// </summary>
        /// <param name="entities">entities</param>
        void Update(List<ExtAlm> entities);

        /// <summary>
        /// Gets the history entities
        /// </summary>
        /// <returns>entities</returns>
        List<ExtAlm> GetHisEntities(DateTime start, DateTime end);
    }
}
