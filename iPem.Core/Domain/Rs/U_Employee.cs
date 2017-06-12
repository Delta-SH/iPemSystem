using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 员工信息表
    /// </summary>
    [Serializable]
    public partial class U_Employee : BaseEntity {
        /// <summary>
        /// 员工编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 员工代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// 曾用名
        /// </summary>
        public string UsedName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public EnmSex Sex { get; set; }

        /// <summary>
        /// 所属部门编号
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 所属职位编号
        /// </summary>
        public string DutyId { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string ICardId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public EnmDegree Degree { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public EnmMarriage Marriage { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Provinces { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 地址电话
        /// </summary>
        public string AddrPhone { get; set; }

        /// <summary>
        /// 工作电话
        /// </summary>
        public string WorkPhone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        public bool IsLeft { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// 退休时间
        /// </summary>
        public DateTime RetireTime { get; set; }

        /// <summary>
        /// 是否正式员工
        /// </summary>
        public bool IsFormal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
