using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IWebEventRepository {
        List<H_WebEvent> GetEntities(DateTime startTime, DateTime endTime);

        List<H_WebEvent> GetEntities(DateTime? startTime = null, DateTime? endTime = null, EnmEventLevel[] levels = null, EnmEventType[] types = null);

        void Insert(H_WebEvent entity);

        void Insert(List<H_WebEvent> entities);

        void Delete(H_WebEvent entity);

        void Delete(List<H_WebEvent> entities);

        void Clear(DateTime? startTime = null, DateTime? endTime = null);
    }
}