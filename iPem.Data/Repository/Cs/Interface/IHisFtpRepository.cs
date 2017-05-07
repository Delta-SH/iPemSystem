using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisFtpRepository {
        /// <summary>
        /// Gets entities from the repository by the specific datetime
        /// </summary>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <returns></returns>
        List<HisFtp> GetEntities(DateTime start, DateTime end);

        /// <summary>
        /// Gets entities from the repository by the specific datetime & type
        /// </summary>
        /// <param name="start">the start datetime</param>
        /// <param name="end">the end datetime</param>
        /// <param name="type">the ftp event type</param>
        /// <returns></returns>
        List<HisFtp> GetEntities(DateTime start, DateTime end, EnmFtpEvent type);
    }
}
