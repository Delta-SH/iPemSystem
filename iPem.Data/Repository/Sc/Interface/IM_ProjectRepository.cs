using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 工程信息表
    /// </summary>
    public partial interface IM_ProjectRepository {
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
        /// 新增工程信息
        /// </summary>
        void Insert(M_Project entity);

        /// <summary>
        /// 更新工程信息
        /// </summary>
        void Update(M_Project entity);
    }
}