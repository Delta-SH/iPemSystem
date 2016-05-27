using iPem.Core;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    public partial interface IPointsInProtcolRepository {

        List<IdValuePair<string, string>> GetEntities();

        List<IdValuePair<string, string>> GetRelation();

    }
}
