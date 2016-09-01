using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IProjectService {
        Project GetProject(Guid id);

        IPagedList<Project> GetAllProjects(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Project> GetAllProjectsAsList();

        IPagedList<Project> GetProjects(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Project> GetProjectsAsList(DateTime start, DateTime end);

        IPagedList<Project> GetProjects(string[] names, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Project> GetProjectsAsList(string[] names, DateTime start, DateTime end);

        void Add(Project project);

        void Update(Project project);
    }
}