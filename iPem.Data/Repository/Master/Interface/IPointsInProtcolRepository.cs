using iPem.Core;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    public partial interface IPointsInProtcolRepository {

        List<IdValuePair<int,string>> GetEntities();

    }
}
