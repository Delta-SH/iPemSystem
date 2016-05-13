using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Resource {
    /// <summary>
    /// Represents an employee
    /// </summary>
    [Serializable]
    public partial class Employee : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the number
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the english name
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// Gets or sets the used name
        /// </summary>
        public string UsedName { get; set; }

        /// <summary>
        /// Gets or sets the sex
        /// </summary>
        public EnmSex Sex { get; set; }

        /// <summary>
        /// Gets or sets the department identifier
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// Gets or sets the duty identifier
        /// </summary>
        public string DutyId { get; set; }

        /// <summary>
        /// Gets or sets the card identifier
        /// </summary>
        public string ICardId { get; set; }

        /// <summary>
        /// Gets or sets the birthday
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the degree
        /// </summary>
        public EnmDegree Degree { get; set; }

        /// <summary>
        /// Gets or sets the marriage
        /// </summary>
        public EnmMarriage Marriage { get; set; }

        /// <summary>
        /// Gets or sets the nation
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// Gets or sets the provinces
        /// </summary>
        public string Provinces { get; set; }

        /// <summary>
        /// Gets or sets the native
        /// </summary>
        public string Native { get; set; }

        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the postal code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the home phone
        /// </summary>
        public string AddrPhone { get; set; }

        /// <summary>
        /// Gets or sets the work phone
        /// </summary>
        public string WorkPhone { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the employee photo
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the employee has already left.
        /// </summary>
        public bool IsLeft { get; set; }

        /// <summary>
        /// Gets or sets the entry time
        /// </summary>
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// Gets or sets the retire time
        /// </summary>
        public DateTime RetireTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the employee is formal.
        /// </summary>
        public bool IsFormal { get; set; }

        /// <summary>
        /// Gets or sets the remarks
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the employee is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
