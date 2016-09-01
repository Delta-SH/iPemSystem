using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IProfileRepository {

        UserProfile GetEntity(Guid id);

        void Save(UserProfile entity);

        void Delete(Guid id);

    }
}
