using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Cs {
    [Serializable]
    public partial class HisElec : BaseEntity {
        public string Id { get; set; }

        public EnmOrganization Type { get; set; }

        public EnmFormula FormulaType { get; set; }

        public DateTime Period { get; set; }

        public double Value { get; set; }
    }
}
