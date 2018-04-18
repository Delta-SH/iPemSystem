using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Services.Rs {
    /// <summary>
    /// FTP信息API
    /// </summary>
    public partial interface IFtpService {
        /// <summary>
        /// 获得所有的FTP
        /// </summary>
        List<C_Ftp> GetFtps();

        /// <summary>
        /// 获得指定类型的FTP
        /// </summary>
        List<C_Ftp> GetFtps(EnmFtp type);
    }
}
