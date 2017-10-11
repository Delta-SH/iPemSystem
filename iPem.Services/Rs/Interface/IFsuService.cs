using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// Fsu信息API
    /// </summary>
    public partial interface IFsuService {
        /// <summary>
        /// 获得指定的FSU
        /// </summary>
        D_Fsu GetFsu(string id);

        /// <summary>
        /// 获得指定机房的FSU
        /// </summary>
        List<D_Fsu> GetFsusInRoom(string id);

        /// <summary>
        /// 获得所有的FSU
        /// </summary>
        List<D_Fsu> GetFsus();

        /// <summary>
        /// 获得指定的FSU扩展信息
        /// </summary>
        D_ExtFsu GetExtFsu(string id);

        /// <summary>
        /// 获得所有的FSU扩展信息
        /// </summary>
        List<D_ExtFsu> GetExtFsus();

        /// <summary>
        /// 更新FSU所属的机房
        /// </summary>
        void UpdateFsus();

        /// <summary>
        /// 获得所有的FSU(分页)
        /// </summary>
        IPagedList<D_Fsu> GetPagedFsus(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得所有的FSU扩展信息(分页)
        /// </summary>
        IPagedList<D_ExtFsu> GetPagedExtFsus(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
