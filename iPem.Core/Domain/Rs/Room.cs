using System;

namespace iPem.Core.Domain.Rs {
    /// <summary>
    /// Represents an room
    /// </summary>
    [Serializable]
    public partial class Room : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the room
        /// </summary>
        public RoomType Type { get; set; }

        /// <summary>
        /// Gets or sets the property identifier
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the floor
        /// </summary>
        public int? Floor { get; set; }

        /// <summary>
        /// Gets or sets the length
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// Gets or sets the heigth
        /// </summary>
        public string Heigth { get; set; }

        /// <summary>
        /// Gets or sets the floor load
        /// </summary>
        public string FloorLoad { get; set; }

        /// <summary>
        /// Gets or sets the line heigth
        /// </summary>
        public string LineHeigth { get; set; }

        /// <summary>
        /// Gets or sets the square
        /// </summary>
        public string Square { get; set; }

        /// <summary>
        /// Gets or sets the effect square
        /// </summary>
        public string EffeSquare { get; set; }

        /// <summary>
        /// Gets or sets the fire figh equipment
        /// </summary>
        public string FireFighEuip { get; set; }

        /// <summary>
        /// Gets or sets the owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the query phone
        /// </summary>
        public string QueryPhone { get; set; }

        /// <summary>
        /// Gets or sets the power sub main
        /// </summary>
        public string PowerSubMain { get; set; }

        /// <summary>
        /// Gets or sets the transmission sub main
        /// </summary>
        public string TranSubMain { get; set; }

        /// <summary>
        /// Gets or sets the environment sub main
        /// </summary>
        public string EnviSubMain { get; set; }

        /// <summary>
        /// Gets or sets the fire sub main
        /// </summary>
        public string FireSubMain { get; set; }

        /// <summary>
        /// Gets or sets the air sub main
        /// </summary>
        public string AirSubMain { get; set; }

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
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
