using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 工程信息API
    /// </summary>
    public partial interface IProjectService {
        /// <summary>
        /// 获得指定的工程信息
        /// </summary>
        M_Project GetProject(string id);

        /// <summary>
        /// 获得所有的工程信息
        /// </summary>
        List<M_Project> GetProjects();

        /// <summary>
        /// 获得指定时间内的工程信息
        /// </summary>
        List<M_Project> GetProjectsInSpan(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定名称的工程信息
        /// </summary>
        List<M_Project> GetProjectsInNames(string[] names, DateTime start, DateTime end);

        /// <summary>
        /// 获得所有的工程信息（分页）
        /// </summary>
        IPagedList<M_Project> GetPagedProjects(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定时间内的工程信息（分页）
        /// </summary>
        IPagedList<M_Project> GetPagedProjectsInSpan(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定名称的工程信息（分页）
        /// </summary>
        IPagedList<M_Project> GetPagedProjectsInNames(string[] names, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增工程信息
        /// </summary>
        void Add(M_Project project);

        /// <summary>
        /// 更新工程信息
        /// </summary>
        void Update(M_Project project);
    }
}