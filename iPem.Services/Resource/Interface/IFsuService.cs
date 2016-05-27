using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    /// <summary>
    /// IFsuService interface
    /// </summary>
    public partial interface IFsuService {

        Fsu GetFsu(string id);

        IPagedList<Fsu> GetAllFsus(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
