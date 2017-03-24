using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IExtAlarmRepository {
        /// <summary>
        /// Gets the entities
        /// </summary>
        /// <returns>entities</returns>
        List<ExtAlarm> GetEntities();

        /// <summary>
        /// Update the entities
        /// </summary>
        /// <param name="entities">entities</param>
        void Update(List<ExtAlarm> entities);

        /// <summary>
        /// Gets the history entities
        /// </summary>
        /// <returns>entities</returns>
        List<ExtAlarm> GetHisEntities(DateTime start, DateTime end);
    }
}
