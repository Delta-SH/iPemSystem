using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 卡片信息API
    /// </summary>
    public partial interface IMCardService {
        /// <summary>
        /// 获得指定门禁卡片
        /// </summary>
        /// <param name="id">十六进制卡号（10位）</param>
        M_Card GetCard(string id);

        /// <summary>
        /// 获得所有门禁卡片
        /// </summary>
        List<M_Card> GetCards();
    }
}
