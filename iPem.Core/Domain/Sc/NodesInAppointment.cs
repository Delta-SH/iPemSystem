using iPem.Core.Enum;
using System;

namespace iPem.Core.Domain.Sc {
    [Serializable]
    public partial class NodesInAppointment {
        /// <summary>
        /// Gets or sets the appointment id
        /// </summary>
        public Guid AppointmentId { get; set; }

        /// <summary>
        ///  Gets or sets the node id
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// Gets or sets the node type
        /// </summary>
        public EnmOrganization NodeType { get; set; }
    }
}