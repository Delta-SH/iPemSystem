using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 组态模板表
    /// </summary>
    public partial interface IG_TemplateRepository {
        /// <summary>
        /// 获得指定模板名称的组态模板对象
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <returns>组态模板对象</returns>
        G_Template GetEntity(string name);

        /// <summary>
        /// 判断指定模板名称的组态模板对象是否存在
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <returns>true/false</returns>
        Boolean ExistEntity(string name);

        /// <summary>
        /// 获得所有的组态模板对象
        /// </summary>
        /// <returns>组态模板对象</returns>
        List<G_Template> GetEntities();

        /// <summary>
        /// 获得所有的组态模板名录
        /// </summary>
        /// <returns>组态模板名录</returns>
        List<String> GetNames();

        /// <summary>
        /// 新增组态模板
        /// </summary>
        /// <param name="entities">需要新增的组态模板对象</param>
        void Insert(IList<G_Template> entities);

        /// <summary>
        /// 更新组态模板
        /// </summary>
        /// <param name="entities">需要更新的组态模板对象</param>
        void Update(IList<G_Template> entities);

        /// <summary>
        /// 删除组态模板
        /// </summary>
        /// <param name="names">需要删除的组态模板名录</param>
        void Delete(IList<string> names);

        /// <summary>
        /// 删除所有的组态模板
        /// </summary>
        void Clear();
    }
}
