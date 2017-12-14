using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 机房信息表
    /// </summary>
    [Serializable]
    public partial class S_Room : BaseEntity {
        /// <summary>
        /// 机房编码
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外部编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 机房名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机房类型
        /// </summary>
        public C_RoomType Type { get; set; }

        /// <summary>
        /// 所属厂家
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 产权属性
        /// </summary>
        /// <remarks>
        /// 枚举值：自建、购买、租用
        /// </remarks>
        public int PropertyId { get; set; }

        /// <summary>
        /// 机房地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? Floor { get; set; }

        /// <summary>
        /// 长度（米）
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// 宽度（米）
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// 高度（米）
        /// </summary>
        public string Heigth { get; set; }

        /// <summary>
        /// 楼面荷载
        /// </summary>
        public string FloorLoad { get; set; }

        /// <summary>
        /// 走线架高度
        /// </summary>
        public string LineHeigth { get; set; }

        /// <summary>
        /// 机房面积（m2）
        /// </summary>
        public string Square { get; set; }

        /// <summary>
        /// 可使用面积（m2）
        /// </summary>
        public string EffeSquare { get; set; }

        /// <summary>
        /// 消防设备
        /// </summary>
        public string FireFighEuip { get; set; }

        /// <summary>
        /// 业主联系人
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 查询电话
        /// </summary>
        public string QueryPhone { get; set; }

        /// <summary>
        /// 动力代维
        /// </summary>
        public string PowerSubMain { get; set; }

        /// <summary>
        /// 传输代维
        /// </summary>
        public string TranSubMain { get; set; }

        /// <summary>
        /// 环境代维
        /// </summary>
        public string EnviSubMain { get; set; }

        /// <summary>
        /// 消防代维
        /// </summary>
        public string FireSubMain { get; set; }

        /// <summary>
        /// 空调代维
        /// </summary>
        public string AirSubMain { get; set; }

        /// <summary>
        /// 维护负责人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
