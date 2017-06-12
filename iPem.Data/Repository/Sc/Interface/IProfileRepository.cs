using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IProfileRepository {

        U_Profile GetEntity(Guid id);

        void Save(U_Profile entity);

        void Delete(Guid id);

    }
}
