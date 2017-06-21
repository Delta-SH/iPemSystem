using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Site.Models {
    public class iProfile {
        /// <summary>
        /// 用户关注的信号
        /// </summary>
        public List<U_FollowPoint> FollowPoints { get; set; }

        /// <summary>
        /// 用户自定义配置
        /// </summary>
        public Setting Settings { get; set; }
    }
}