using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Sc {
    [Serializable]
    public partial class Formula : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public EnmOrganization Type { get; set; }

        /// <summary>
        /// Gets or sets the formula type
        /// </summary>
        public EnmFormula FormulaType { get; set; }

        /// <summary>
        /// Gets or sets the formula
        /// </summary>
        public string FormulaText { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the created datetime
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
