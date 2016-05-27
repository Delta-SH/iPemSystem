using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Services.History {
    public partial interface IAlmExtendService {

        void Update(List<AlmExtend> entities);

        void UpdateConfirm(List<AlmExtend> entities);

    }
}
