using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 区域信息表
    /// </summary>
    public partial interface IC_FtpRepository {
        /// <summary>
        /// 获得所有的FTP信息
        /// </summary>
        List<C_Ftp> GetEntities();

        /// <summary>
        /// 获得指定类型的FTP信息
        /// </summary>
        List<C_Ftp> GetEntities(EnmFtp type);
    }
}
