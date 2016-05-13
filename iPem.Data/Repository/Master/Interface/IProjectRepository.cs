using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Projects repository interface
    /// </summary>
    public partial interface IProjectRepository {
        List<Project> GetEntities();

        List<Project> GetEntities(DateTime starttime, DateTime endtime);

        Project GetEntity(Guid id);

        void Insert(Project entity);

        void Update(Project entity);
    }
}