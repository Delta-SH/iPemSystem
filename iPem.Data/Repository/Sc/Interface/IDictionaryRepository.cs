using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IDictionaryRepository {

        M_Dictionary GetEntity(int id);

        List<M_Dictionary> GetEntities();

        void Update(M_Dictionary entity);

        void Update(List<M_Dictionary> entities);

    }
}