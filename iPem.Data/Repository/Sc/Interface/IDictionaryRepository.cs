using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IDictionaryRepository {

        Dictionary GetEntity(int id);

        List<Dictionary> GetEntities();

        void Update(Dictionary entity);

        void Update(List<Dictionary> entities);

    }
}