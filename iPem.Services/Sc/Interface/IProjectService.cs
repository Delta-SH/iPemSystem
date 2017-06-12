using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IProjectService {
        M_Project GetProject(Guid id);

        IPagedList<M_Project> GetAllProjects(int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Project> GetAllProjectsAsList();

        IPagedList<M_Project> GetProjects(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Project> GetProjectsAsList(DateTime start, DateTime end);

        IPagedList<M_Project> GetProjects(string[] names, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Project> GetProjectsAsList(string[] names, DateTime start, DateTime end);

        void Add(M_Project project);

        void Update(M_Project project);
    }
}