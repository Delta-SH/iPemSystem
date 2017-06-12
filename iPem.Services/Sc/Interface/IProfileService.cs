using iPem.Core.Domain.Sc;
using System;

namespace iPem.Services.Sc {
    public partial interface IProfileService {
        U_Profile GetProfile(Guid uid);

        void Save(U_Profile profile);

        void Remove(Guid uid);
    }
}