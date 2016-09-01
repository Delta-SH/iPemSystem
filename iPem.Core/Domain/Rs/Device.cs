using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents a device
    /// </summary>
    [Serializable]
    public partial class Device : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device type
        /// </summary>
        public DeviceType Type { get; set; }

        /// <summary>
        /// Gets or sets the sub device type
        /// </summary>
        public SubDeviceType SubType { get; set; }

        /// <summary>
        /// Gets or sets the system name
        /// </summary>
        public string SysName { get; set; }

        /// <summary>
        /// Gets or sets the system code
        /// </summary>
        public string SysCode { get; set; }

        /// <summary>
        /// Gets or sets the model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public string ProdId { get; set; }

        /// <summary>
        /// Gets or sets the brand identifier
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier
        /// </summary>
        public string SuppId { get; set; }

        /// <summary>
        /// Gets or sets the sub company identifier
        /// </summary>
        public string SubCompId { get; set; }

        /// <summary>
        /// Gets or sets the start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the scrap time
        /// </summary>
        public DateTime ScrapTime { get; set; }

        /// <summary>
        /// Gets or sets the status identifier
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the contact
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the area
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the station
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the station
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the room
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Gets or sets the name of the room
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fsu
        /// </summary>
        public string FsuId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fsu
        /// </summary>
        public string FsuName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the protcol
        /// </summary>
        public string ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
