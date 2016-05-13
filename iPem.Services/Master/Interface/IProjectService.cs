using System;
using iPem.Core;
using iPem.Core.Domain.Master;

namespace iPem.Services.Master {
    /// <summary>
    /// ProjectService interface
    /// </summary>
    public partial interface IProjectService {

        IPagedList<Project> GetAllProjects(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Project> GetProjects(DateTime starttime, DateTime endtime, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Project> GetProjectsByName(string[] names, DateTime starttime, DateTime endtime, int pageIndex = 0, int pageSize = int.MaxValue);

        Project GetProject(Guid Id);

        void Insert(Project project);

        void Update(Project project);

    }
}