using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 节点(区域、站点、机房、设备)预约映射表
    /// </summary>
    public partial interface IM_NodesInReservationRepository {
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
        /// 新增节点预约映射关系
        /// </summary>
        void Insert(IList<M_NodeInReservation> entities);

        /// <summary>
        /// 删除节点预约映射关系
        /// </summary>
        /// <param name="entities">预约编号集合</param>
        void Delete(IList<string> entities);
    }
}