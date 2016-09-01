using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents a station
    /// </summary>
    [Serializable]
    public partial class Station : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the station
        /// </summary>
        public StationType Type { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude
        /// </summary>
        public string Altitude { get; set; }

        /// <summary>
        /// Gets or sets the city electric load type
        /// </summary>
        public int CityElecLoadTypeId { get; set; }

        /// <summary>
        /// Gets or sets the city electric capacity
        /// </summary>
        public string CityElecCap { get; set; }

        /// <summary>
        /// Gets or sets the city electric load
        /// </summary>
        public string CityElecLoad { get; set; }

        /// <summary>
        /// Gets or sets the contact
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Gets or sets the line radius size
        /// </summary>
        public string LineRadiusSize { get; set; }

        /// <summary>
        /// Gets or sets the line length
        /// </summary>
        public string LineLength { get; set; }

        /// <summary>
        /// Gets or sets the supply power type
        /// </summary>
        public int SuppPowerTypeId { get; set; }

        /// <summary>
        /// Gets or sets the transmission information
        /// </summary>
        public string TranInfo { get; set; }

        /// <summary>
        /// Gets or sets the transmission contact number
        /// </summary>
        public string TranContNo { get; set; }

        /// <summary>
        /// Gets or sets the transmission phone
        /// </summary>
        public string TranPhone { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the area
        /// </summary>
        public string AreaId { get; set; }

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
