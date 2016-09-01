using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IProjectRepository {
        List<Project> GetEntities();

        List<Project> GetEntities(DateTime starttime, DateTime endtime);

        Project GetEntity(Guid id);

        void Insert(Project entity);

        void Update(Project entity);
    }
}