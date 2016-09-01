using iPem.Core.Domain.Sc;
using System;

namespace iPem.Services.Sc {
    public partial interface IProfileService {
        UserProfile GetProfile(Guid uid);

        void Save(UserProfile profile);

        void Remove(Guid uid);
    }
}