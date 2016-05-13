using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    public partial interface IDictionaryRepository {

        Dictionary GetEntity(int id);

        List<Dictionary> GetEntities();

        void UpdateEntity(Dictionary entity);

        void UpdateEntities(List<Dictionary> entities);

    }
}