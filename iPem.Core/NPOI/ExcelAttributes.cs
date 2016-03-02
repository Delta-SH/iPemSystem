using System;

namespace iPem.Core.NPOI {
    /// <summary>
    /// Instructs the <see cref="ExcelManager"/> to display the column name with the specified name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExcelDisplayNameAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the ExcelDisplayNameAttribute class.
        /// </summary>
        public ExcelDisplayNameAttribute() { }

        /// <summary>
        /// Initializes a new instance of the ExcelDisplayNameAttribute class using the display name.
        /// </summary>
        /// <param name="displayName">the display name</param>
        public ExcelDisplayNameAttribute(string displayName) {
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// Instructs the <see cref="ExcelManager"/> to display the boolean name with the specified name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExcelBooleanNameAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the ExcelBooleanNameAttribute class.
        /// </summary>
        public ExcelBooleanNameAttribute() { }

        /// <summary>
        /// Initializes a new instance of the ExcelBooleanNameAttribute class using the boolean name.
        /// </summary>
        /// <param name="trueName">the true name</param>
        /// <param name="falseName">the false name</param>
        public ExcelBooleanNameAttribute(string trueName, string falseName) {
            this.True = trueName;
            this.False = falseName;
        }

        /// <summary>
        /// Gets or sets the true name.
        /// </summary>
        public string True { get; set; }

        /// <summary>
        /// Gets or sets the false name.
        /// </summary>
        public string False { get; set; }
    }

    /// <summary>
    /// Instructs the <see cref="ExcelManager"/> to ignore the property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExcelIgnoreAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the ExcelIgnoreAttribute class.
        /// </summary>
        public ExcelIgnoreAttribute() { }
    }

    /// <summary>
    /// Instructs the <see cref="ExcelManager"/> to fill the background color of each row with a specified color,
    /// the attribute must be set on the <see cref="System.Drawing.Color"/> properties or fields,
    /// currently only supported colors include Red、Orange、Yellow、SkyBlue、Blue.
    /// </summary>
    /// <example>
    /// [ExcelBackground]
    /// public System.Drawing.Color Background { get; set; }
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExcelBackgroundAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the ExcelBackgroundAttribute class.
        /// </summary>
        public ExcelBackgroundAttribute() { }
    }
}