using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 节点(区域、站点、机房、设备)预约映射API
    /// </summary>
    public partial interface INodeInReservationService {
        /// <summary>
        /// 获得所有的节点预约映射关系
        /// </summary>
        List<M_NodeInReservation> GetNodesInReservations();

        /// <summary>
        /// 获得指定类型的节点预约映射关系
        /// </summary>
        List<M_NodeInReservation> GetNodesInReservationsInType(EnmSSH type);

        /// <summary>
        /// 获得指定预约的节点预约映射关系
        /// </summary>
        List<M_NodeInReservation> GetNodesInReservationsInReservation(string id);

        /// <summary>
        /// 获得所有的节点预约映射关系(分页)
        /// </summary>
        IPagedList<M_NodeInReservation> GetPagedNodesInReservations(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增节点预约映射关系
        /// </summary>
        void Add(params M_NodeInReservation[] nodes);

        /// <summary>
        /// 删除节点预约映射关系
        /// </summary>
        /// <param name="entities">预约编号集合</param>
        void Remove(params string[] ids);
    }
}