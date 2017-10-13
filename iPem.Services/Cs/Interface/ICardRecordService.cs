using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 刷卡记录API
    /// </summary>
    public partial interface ICardRecordService {
        /// <summary>
        /// 获取指定时间范围的刷卡记录
        /// </summary>
        List<H_CardRecord> GetRecords(DateTime start, DateTime end);

        /// <summary>
        /// 获取指定卡号的刷卡记录
        /// </summary>
        List<H_CardRecord> GetRecordsInCard(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定设备的刷卡记录
        /// </summary>
        List<H_CardRecord> GetRecordsInDevice(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定机房的刷卡记录
        /// </summary>
        List<H_CardRecord> GetRecordsInRoom(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定站点的刷卡记录
        /// </summary>
        List<H_CardRecord> GetRecordsInStation(DateTime start, DateTime end, string id);

        /// <summary>
        /// 获取指定区域的刷卡记录
        /// </summary>
        List<H_CardRecord> GetRecordsInArea(DateTime start, DateTime end, string id);
    }
}
