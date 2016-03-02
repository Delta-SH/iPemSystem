using System;
using System.Collections.Generic;
using iPem.Core.Enum;

namespace iPem.Core.Data {
    public class DbTypeConverter {
        /// <summary>
        /// DBNull String Handler
        /// </summary>
        /// <param name="val">val</param>
        public static string DBNullStringHandler(object val) {
            if (val == DBNull.Value) { return default(String); }
            return val.ToString();
        }

        /// <summary>
        /// DBNull String Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullStringChecker(string val) {
            if (val == default(String)) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Int32 Handler
        /// </summary>
        /// <param name="val">val</param>
        public static int DBNullInt32Handler(object val) {
            if (val == DBNull.Value) { return default(Int32); }
            return Convert.ToInt32(val);
        }

        /// <summary>
        /// DBNull Int32 Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullInt32Checker(int val) {
            if (val == default(Int32)) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Int64 Handler
        /// </summary>
        /// <param name="val">val</param>
        public static long DBNullInt64Handler(object val) {
            if (val == DBNull.Value) { return default(Int64); }
            return Convert.ToInt64(val);
        }

        /// <summary>
        /// DBNull Int64 Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullInt64Checker(long val) {
            if (val == default(Int64)) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Float Handler
        /// </summary>
        /// <param name="val">val</param>
        public static float DBNullFloatHandler(object val) {
            if (val == DBNull.Value) { return default(Single); }
            return Convert.ToSingle(val);
        }

        /// <summary>
        /// DBNull Float Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullFloatChecker(float val) {
            if (val == default(Single)) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Double Handler
        /// </summary>
        /// <param name="val">val</param>
        public static double DBNullDoubleHandler(object val) {
            if (val == DBNull.Value) { return default(Double); }
            return Convert.ToDouble(val);
        }

        /// <summary>
        /// DBNull Double Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullDoubleChecker(double val) {
            if (val == default(Double)) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull DateTime Handler
        /// </summary>
        /// <param name="val">val</param>
        public static DateTime DBNullDateTimeHandler(object val) {
            if (val == DBNull.Value) { return default(DateTime); }
            return Convert.ToDateTime(val);
        }

        /// <summary>
        /// DBNull DateTime Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullDateTimeChecker(DateTime val) {
            if (val == default(DateTime)) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Boolean Handler
        /// </summary>
        /// <param name="val">val</param>
        public static bool DBNullBooleanHandler(object val) {
            if (val == DBNull.Value) { return default(Boolean); }
            return Convert.ToBoolean(val);
        }

        /// <summary>
        /// DBNull Guid Handler
        /// </summary>
        /// <param name="val">val</param>
        public static Guid DBNullGuidHandler(object val) {
            if (val == DBNull.Value) { return default(Guid); }
            return Guid.Parse(val.ToString());
        }

        /// <summary>
        /// DBNull Data Provider Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmDbProvider DBNullDataProviderHandler(object val) {
            if(val == DBNull.Value) { return EnmDbProvider.SqlServer; }
            var em = Convert.ToInt32(val);
            return System.Enum.IsDefined(typeof(EnmDbProvider), em) ? (EnmDbProvider)em : EnmDbProvider.SqlServer;
        }

        /// <summary>
        /// DBNull Database Type Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmDatabaseType DBNullDatabaseTypeHandler(object val) {
            if(val == DBNull.Value) { return EnmDatabaseType.Master; }
            var em = Convert.ToInt32(val);
            return System.Enum.IsDefined(typeof(EnmDatabaseType), em) ? (EnmDatabaseType)em : EnmDatabaseType.Master;
        }
    }
}
