using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 组态配置表
    /// </summary>
    public partial interface IG_PageRepository {
        /// <summary>
        /// 获得指定组态名称的组态对象
        /// </summary>
        /// <param name="name">组态名称</param>
        /// <returns>组态对象</returns>
        G_Page GetEntity(string name);

        /// <summary>
        /// 判断指定组态对象是否存在
        /// </summary>
        /// <param name="name">组态名称</param>
        /// <returns>true/false</returns>
        Boolean ExistEntity(string name);

        /// <summary>
        /// 获得所有的组态对象集合
        /// </summary>
        /// <returns>组态对象集合</returns>
        List<G_Page> GetEntities();

        /// <summary>
        /// 获得指定角色下的组态对象集合
        /// </summary>
        /// <param name="role">角色编号</param>
        /// <returns>组态对象集合</returns>
        List<G_Page> GetEntities(string role);

        /// <summary>
        /// 获得指定监控对象所有的组态对象集合
        /// </summary>
        /// <param name="role">角色编号</param>
        /// <param name="id">监控对象编号</param>
        /// <param name="type">监控对象类型</param>
        /// <returns>组态对象集合</returns>
        List<G_Page> GetEntities(string role, string id, int type);

        /// <summary>
        /// 获得指定监控对象所有的组态名录
        /// </summary>
        /// <param name="role">角色编号</param>
        /// <param name="id">监控对象编号</param>
        /// <param name="type">监控对象类型</param>
        /// <returns>组态名录</returns>
        List<String> GetNames(string role, string id, int type);

        /// <summary>
        /// 新增组态对象
        /// </summary>
        /// <param name="entities">需要新增的组态对象集合</param>
        void Insert(IList<G_Page> entities);

        /// <summary>
        /// 更新组态对象
        /// </summary>
        /// <param name="entities">需要更新的组态对象集合</param>
        void Update(IList<G_Page> entities);

        /// <summary>
        /// 删除组态对象
        /// </summary>
        /// <param name="names">需要删除的组态名录</param>
        void Delete(IList<string> names);

        /// <summary>
        /// 删除指定角色下的所有组态对象
        /// </summary>
        /// <param name="role">角色编号</param>
        void Clear(string role);

        /// <summary>
        /// 删除所有组态对象
        /// </summary>
        void Clear();
    }
}
