using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 工程预约API
    /// </summary>
    public partial interface IReservationService {
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
        /// 获得所有的工程预约(分页)
        /// </summary>
        IPagedList<M_Reservation> GetPagedReservations(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定时间内的工程预约(分页)
        /// </summary>
        IPagedList<M_Reservation> GetPagedReservationsInSpan(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增工程预约
        /// </summary>
        void Add(params M_Reservation[] reservations);

        /// <summary>
        /// 更新工程预约
        /// </summary>
        void Update(params M_Reservation[] reservations);

        /// <summary>
        /// 删除工程预约
        /// </summary>
        void Delete(params M_Reservation[] reservations);

        /// <summary>
        /// 审核工程预约
        /// </summary>
        void Check(string id, DateTime start, EnmResult status);
    }
}