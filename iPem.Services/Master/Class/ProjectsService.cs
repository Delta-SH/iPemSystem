using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Master {
    /// <summary>
    /// Projects service
    /// </summary>
    public partial class ProjectsService : IProjectService {

        #region Fields

        private readonly IProjectRepository _projectsRepository;

        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProjectsService(IProjectRepository projectsRepository, ICacheManager cacheManager) {
            this._projectsRepository = projectsRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<Project> GetAllProjects(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _projectsRepository.GetEntities();
            return new PagedList<Project>(result, pageIndex, pageSize);
        }

        public virtual IPagedList<Project> GetProjects(DateTime starttime, DateTime endtime, int pageIndex = 0, int pageSize = int.MaxValue) {
            var projects = _projectsRepository.GetEntities(starttime, endtime);
            return new PagedList<Project>(projects, pageIndex, pageSize);
        }

        public virtual IPagedList<Project> GetProjectsByName(string[] names, DateTime starttime, DateTime endtime, int pageIndex = 0, int pageSize = int.MaxValue) {
            var projects = _projectsRepository.GetEntities(starttime, endtime);
            var result = projects.FindAll(r => CommonHelper.ConditionContain(r.Name, names));
            return new PagedList<Project>(result, pageIndex, pageSize);
        }

        public virtual Project GetProject(Guid Id) {
            return _projectsRepository.GetEntity(Id);
        }

        public virtual void Insert(Project project) {
            if(project == null)
                throw new ArgumentNullException("Project");

            _projectsRepository.Insert(project);
        }

        public virtual void Update(Project project) {
            if(project == null)
                throw new ArgumentNullException("Project");

            _projectsRepository.Update(project);
        }

        #endregion

    }
}