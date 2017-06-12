namespace iPem.Core.Enum {
    /// <summary>
    /// 数据库分类
    /// </summary>
    public enum EnmDbType {
        /// <summary>
        /// 资源数据库
        /// </summary>
        Rs,
        /// <summary>
        /// 历史数据库
        /// </summary>
        Cs,
        /// <summary>
        /// 应用数据库
        /// </summary>
        Sc
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum EnmDbProvider {
        SqlServer,
        Oracle,
        MySql
    }
}
