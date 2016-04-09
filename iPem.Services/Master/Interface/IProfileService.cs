using iPem.Core.Domain.Master;
using System;

namespace iPem.Services.Master {
    public partial interface IProfileService {

        UserProfile GetUserProfile(Guid userId);

        void SaveUserProfile(UserProfile profile);

        void DeleteUserProfile(Guid userId);
    }
}
