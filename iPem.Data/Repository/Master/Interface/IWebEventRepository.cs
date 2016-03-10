using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Event repository interface
    /// </summary>
    public partial interface IWebEventRepository {
        List<WebEvent> GetEntities(DateTime startTime, DateTime endTime);

        List<WebEvent> GetEntities(DateTime? startTime = null, DateTime? endTime = null, EnmEventLevel[] levels = null, EnmEventType[] types = null);

        void Insert(WebEvent entity);

        void Insert(List<WebEvent> entities);

        void Delete(WebEvent entity);

        void Delete(List<WebEvent> entities);

        void Clear(DateTime? startTime = null, DateTime? endTime = null);
    }
}