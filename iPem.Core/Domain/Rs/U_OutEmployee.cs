using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// 外协人员信息表
    /// </summary>
    [Serializable]
    public partial class U_OutEmployee : BaseEntity {
        /// <summary>
        /// 外协编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外协姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        public EnmEmpType Type {
            get { return EnmEmpType.OutEmployee; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public EnmSex Sex { get; set; }

        /// <summary>
        /// 负责人编号
        /// </summary>
        public string EmpId { get; set; }

        /// <summary>
        /// 负责人工号
        /// </summary>
        public string EmpCode { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 所属部门编号
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string ICardId { get; set; }

        /// <summary>
        /// 身份证地址
        /// </summary>
        public string ICardAddress { get; set; }

        /// <summary>
        /// 居住地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 办公电话
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
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// 十六进制卡号（10位）
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 十进制卡号（10位）
        /// </summary>
        public string DecimalCard {
            get {
                if (string.IsNullOrWhiteSpace(this.CardId)) 
                    return string.Empty;

                return int.Parse(this.CardId, System.Globalization.NumberStyles.HexNumber).ToString("D10");
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
