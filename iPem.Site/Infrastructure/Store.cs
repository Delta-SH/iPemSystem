﻿using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Services.Common;
using iPem.Site.Models;
using System;
using System.Collections.Generic;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// 登录用户的相关信息
    /// </summary>
    [Serializable]
    public partial class Store {
        /// <summary>
        /// 登录标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public U_Role Role { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public U_User User { get; set; }

        /// <summary>
        /// 关联员工
        /// </summary>
        public U_Employee Employee { get; set; }

        /// <summary>
        /// 用户自定义信息
        /// </summary>
        public iProfile Profile { get; set; }

        /// <summary>
        /// 所属角色权限
        /// </summary>
        public U_EntitiesInRole Authorizations { get; set; }

        /// <summary>
        /// 上次消息查询时间
        /// </summary>
        public DateTime LastNoticeTime { get; set; }

        /// <summary>
        /// 上次语音查询时间
        /// </summary>
        public DateTime LastSpeechTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expire { get; private set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedTime { get; private set; }

        /// <summary>
        /// 重置过期时间
        /// </summary>
        public void ResetExpire() {
            this.Expire = DateTime.Now.Add(CachedIntervals.Store_Intervals);
        }

        /// <summary>
        /// 创建新的Store对象
        /// </summary>
        public static Store CreateInstance(Guid? id = null) {
            var store = new Store {
                Id = id.HasValue ? id.Value : Guid.NewGuid(),
                LastNoticeTime = DateTime.Now,
                LastSpeechTime = DateTime.Now,
                Expire = DateTime.Now.Add(CachedIntervals.Store_Intervals),
                CreatedTime = DateTime.Now
            };

            EngineContext.Current.WorkStores[store.Id] = store;
            return store;
        }
    }
}
