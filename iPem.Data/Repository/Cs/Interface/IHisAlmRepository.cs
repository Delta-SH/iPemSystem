using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisAlmRepository {
        List<HisAlm> GetEntitiesInArea(string area, DateTime start, DateTime end);

        List<HisAlm> GetEntitiesInStation(string station, DateTime start, DateTime end);

        List<HisAlm> GetEntitiesInRoom(string room, DateTime start, DateTime end);

        List<HisAlm> GetEntitiesInDevice(string device, DateTime start, DateTime end);

        List<HisAlm> GetEntities(string point, DateTime start, DateTime end);

        List<HisAlm> GetEntities(DateTime start, DateTime end);
    }
}
