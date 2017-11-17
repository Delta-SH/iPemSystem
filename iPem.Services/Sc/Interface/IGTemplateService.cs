using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 组态模版API
    /// </summary>
    public partial interface IGTemplateService {
        /// <summary>
        /// 获得指定模板名称的组态模板对象
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <returns>组态模板对象</returns>
        G_Template GetTemplate(string name);

        /// <summary>
        /// 判断指定模板名称的组态模板对象是否存在
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <returns>true/false</returns>
        Boolean Exist(string name);

        /// <summary>
        /// 获得所有的组态模板对象
        /// </summary>
        /// <returns>组态模板对象</returns>
        List<G_Template> GetTemplates();

        /// <summary>
        /// 获得所有的组态模板名录
        /// </summary>
        /// <returns>组态模板名录</returns>
        List<String> GetNames();

        /// <summary>
        /// 新增组态模板
        /// </summary>
        /// <param name="entities">需要新增的组态模板对象</param>
        void Add(params G_Template[] entities);

        /// <summary>
        /// 更新组态模板
        /// </summary>
        /// <param name="entities">需要更新的组态模板对象</param>
        void Update(params G_Template[] entities);

        /// <summary>
        /// 删除组态模板
        /// </summary>
        /// <param name="names">需要删除的组态模板名录</param>
        void Remove(params string[] names);

        /// <summary>
        /// 删除所有的组态模板
        /// </summary>
        void Clear();
    }
}
