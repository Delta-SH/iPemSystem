using iPem.Core.Enum;
using System;
using System.Security.Cryptography;
using System.Text;

namespace iPem.Core.Domain.History {
    /// <summary>
    /// Represents an active alarm
    /// </summary>
    [Serializable]
    public partial class ActAlm : BaseEntity {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the device identifier
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device code
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// Gets or sets the point identifier
        /// </summary>
        public string PointId { get; set; }

        /// <summary>
        /// Gets or sets the alarm flag
        /// </summary>
        public EnmAlarmFlag AlmFlag { get; set; }

        /// <summary>
        /// Gets or sets the alarm level
        /// </summary>
        public EnmAlarmLevel AlmLevel { get; set; }

        /// <summary>
        /// Gets or sets the frequency
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// Gets or sets the alarm descripetion
        /// </summary>
        public string AlmDesc { get; set; }

        /// <summary>
        /// Gets or sets the normal descripretion
        /// </summary>
        public string NormalDesc { get; set; }

        /// <summary>
        /// Gets or sets the start datetime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end datetime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the start value
        /// </summary>
        public double StartValue { get; set; }

        /// <summary>
        /// Gets or sets the end value
        /// </summary>
        public double EndValue { get; set; }

        /// <summary>
        /// Gets or sets the value unit
        /// </summary>
        public string ValueUnit { get; set; }

        /// <summary>
        /// Gets or sets the end type
        /// </summary>
        public EnmAlarmEndType EndType { get; set; }

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the confirm status
        /// </summary>
        public EnmConfirmStatus ConfirmedStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirm datetime
        /// </summary>
        public DateTime? ConfirmedTime { get; set; }

        /// <summary>
        /// Gets or sets the confirmer
        /// </summary>
        public string Confirmer { get; set; }

        /// <summary>
        /// Gets the identifier
        /// </summary>
        /// <returns>the identifier</returns>
        public string GetId() {
            var key = string.Format("{0},{1},{2}", this.DeviceId ?? "", this.PointId ?? "", this.StartTime.Ticks);
            using(var md5Provider = new MD5CryptoServiceProvider()) {
                var bytes = Encoding.UTF8.GetBytes(key);
                var hash = md5Provider.ComputeHash(bytes);
                return new Guid(hash).ToString();
            }
        }
    }
}
