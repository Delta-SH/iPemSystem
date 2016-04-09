using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    public partial interface IProfileRepository {

        UserProfile GetEntity(Guid id);

        void Save(UserProfile entity);

        void Delete(Guid id);

    }
}
