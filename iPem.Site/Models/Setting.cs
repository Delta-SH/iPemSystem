using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    /// <summary>
    /// 用户自定义配置
    /// </summary>
    [Serializable]
    public class Setting {
        public List<SeniorCondition> SeniorConditions { get; set; }
    }
}