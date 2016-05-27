using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.History {
    public partial interface IAlmExtendRepository {
        /// <summary>
        /// Update the entities
        /// </summary>
        /// <param name="entities">entities</param>
        void Update(List<AlmExtend> entities);

        /// <summary>
        /// Update the confirm fields
        /// </summary>
        /// <param name="entities">entities</param>
        void UpdateConfirm(List<AlmExtend> entities);
    }
}
