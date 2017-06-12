using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IProjectRepository {
        List<M_Project> GetEntities();

        List<M_Project> GetEntities(DateTime starttime, DateTime endtime);

        M_Project GetEntity(Guid id);

        void Insert(M_Project entity);

        void Update(M_Project entity);
    }
}