using System;

namespace iPem.Core {
    /// <summary>
    /// ID Value Pair
    /// </summary>
    [Serializable]
    public class IdValuePair<T1, T2> {
        /// <summary>
        /// Class Constructor
        /// </summary>
        public IdValuePair() { 
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="value">Value</param>
        public IdValuePair(T1 id, T2 value) {
            this.Id = id;
            this.Value = value;
        }

        /// <summary>
        /// ID
        /// </summary>
        public T1 Id { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public T2 Value { get; set; }
    }
}
