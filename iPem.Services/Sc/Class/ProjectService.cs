using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Sc {
    public partial class ProjectService : IProjectService {

        #region Fields

        private readonly IM_ProjectRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProjectService(
            IM_ProjectRepository repository, 
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public M_Project GetProject(string id) {
            return _repository.GetProject(id);
        }

        public List<M_Project> GetProjects() {
            return _repository.GetProjects();
        }

        public List<M_Project> GetValidProjects() {
            return _repository.GetValidProjects();
        }

        public List<M_Project> GetProjectsInSpan(DateTime start, DateTime end) {
            return _repository.GetProjectsInSpan(start, end);
        }

        public List<M_Project> GetProjectsInNames(string[] names, DateTime start, DateTime end) {
            return this.GetProjectsInSpan(start, end).FindAll(r => CommonHelper.ConditionContain(r.Name, names));
        }

        public IPagedList<M_Project> GetPagedProjects(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Project>(this.GetProjects(), pageIndex, pageSize);
        }

        public IPagedList<M_Project> GetPagedProjectsInSpan(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Project>(this.GetProjectsInSpan(start, end), pageIndex, pageSize);
        }

        public IPagedList<M_Project> GetPagedProjectsInNames(string[] names, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Project>(this.GetProjectsInNames(names, start, end), pageIndex, pageSize);
        }

        public void Add(M_Project project) {
            if(project == null)
                throw new ArgumentNullException("project");

            _repository.Insert(project);
        }

        public void Update(M_Project project) {
            if(project == null)
                throw new ArgumentNullException("project");

            _repository.Update(project);
        }

        #endregion

    }
}