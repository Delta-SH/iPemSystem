using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Sc {
    public partial class ProjectsService : IProjectService {

        #region Fields

        private readonly IProjectRepository _projectsRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProjectsService(
            IProjectRepository projectsRepository, 
            ICacheManager cacheManager) {
            this._projectsRepository = projectsRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public virtual M_Project GetProject(Guid id) {
            return _projectsRepository.GetEntity(id);
        }

        public IPagedList<M_Project> GetAllProjects(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Project>(this.GetAllProjectsAsList(), pageIndex, pageSize);
        }

        public List<M_Project> GetAllProjectsAsList() {
            return _projectsRepository.GetEntities();
        }

        public virtual IPagedList<M_Project> GetProjects(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Project>(this.GetProjectsAsList(start, end), pageIndex, pageSize);
        }

        public virtual List<M_Project> GetProjectsAsList(DateTime start, DateTime end) {
            return _projectsRepository.GetEntities(start, end);
        }

        public virtual IPagedList<M_Project> GetProjects(string[] names, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Project>(this.GetProjectsAsList(names, start, end), pageIndex, pageSize);
        }

        public virtual List<M_Project> GetProjectsAsList(string[] names, DateTime start, DateTime end) {
            var result = _projectsRepository.GetEntities(start, end);
            return result.FindAll(r => CommonHelper.ConditionContain(r.Name, names));
        }

        public virtual void Add(M_Project project) {
            if(project == null)
                throw new ArgumentNullException("project");

            _projectsRepository.Insert(project);
        }

        public virtual void Update(M_Project project) {
            if(project == null)
                throw new ArgumentNullException("project");

            _projectsRepository.Update(project);
        }

        #endregion

    }
}