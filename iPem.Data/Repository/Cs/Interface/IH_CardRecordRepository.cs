using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 刷卡记录表
    /// </summary>
    public partial interface IH_CardRecordRepository {
        /// <summary>
        /// 获取指定时间范围的刷卡记录
        /// </summary>
        List<H_CardRecord> GetEntities(DateTime start, DateTime end);

        /// <summary>
        /// 获取指定卡号的刷卡记录
        /// </summary>
        List<H_CardRecord> GetEntitiesInCard(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定设备的刷卡记录
        /// </summary>
        List<H_CardRecord> GetEntitiesInDevice(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定机房的刷卡记录
        /// </summary>
        List<H_CardRecord> GetEntitiesInRoom(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定站点的刷卡记录
        /// </summary>
        List<H_CardRecord> GetEntitiesInStation(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定区域的刷卡记录
        /// </summary>
        List<H_CardRecord> GetEntitiesInArea(DateTime start, DateTime end, string id);
    }
}
