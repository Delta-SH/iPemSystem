using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 工程预约表
    /// </summary>
    public partial interface IM_ReservationRepository {
        /// <summary>
        /// 获得指定的工程预约
        /// </summary>
        M_Reservation GetReservation(string id);

        /// <summary>
        /// 获得所有的工程预约
        /// </summary>
        List<M_Reservation> GetReservations();

        /// <summary>
        /// 获得指定时间内的工程预约
        /// </summary>
        List<M_Reservation> GetReservationsInSpan(DateTime start, DateTime end);

        /// <summary>
        /// 新增工程预约
        /// </summary>
        void Insert(IList<M_Reservation> entities);

        /// <summary>
        /// 更新工程预约
        /// </summary>
        void Update(IList<M_Reservation> entities);

        /// <summary>
        /// 删除工程预约
        /// </summary>
        void Delete(IList<M_Reservation> entities);

        /// <summary>
        /// 审核工程预约
        /// </summary>
        void Check(string id, DateTime start, EnmResult status, string common);
    }
}
