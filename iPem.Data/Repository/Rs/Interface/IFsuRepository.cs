using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IFsuRepository {

        Fsu GetEntity(string id);

        List<Fsu> GetEntities(string parent);

        List<Fsu> GetEntities();

        List<FsuExt> GetExtends();

    }
}
