using iPem.Core.Enum;
using System;

namespace iPem.Data.Common {
    /// <summary>
    /// Sql类型适配器
    /// </summary>
    public static partial class SqlTypeConverter {
        public static string DBNullStringHandler(object val) {
            if (val == DBNull.Value) { return default(String); }
            return val.ToString();
        }

        public static object DBNullStringChecker(string val) {
            if (val == default(String)) { return DBNull.Value; }
            return val;
        }

        public static int DBNullInt32Handler(object val) {
            if (val == DBNull.Value) { return int.MinValue; }
            return (Int32)val;
        }

        public static object DBNullInt32Checker(int val) {
            if(val == int.MinValue) { return DBNull.Value; }
            return val;
        }

        public static long DBNullInt64Handler(object val) {
            if (val == DBNull.Value) { return long.MinValue; }
            return (Int64)val;
        }

        public static object DBNullInt64Checker(long val) {
            if(val == long.MinValue) { return DBNull.Value; }
            return val;
        }

        public static float DBNullFloatHandler(object val) {
            if (val == DBNull.Value) { return float.MinValue; }
            return (Single)val;
        }

        public static object DBNullFloatChecker(float val) {
            if(val == float.MinValue) { return DBNull.Value; }
            return val;
        }

        public static double DBNullDoubleHandler(object val) {
            if (val == DBNull.Value) { return double.MinValue; }
            return (Double)val;
        }

        public static object DBNullDoubleChecker(double val) {
            if(val == double.MinValue) { return DBNull.Value; }
            return val;
        }

        public static DateTime DBNullDateTimeHandler(object val) {
            if (val == DBNull.Value) { return default(DateTime); }
            return (DateTime)val;
        }

        public static object DBNullDateTimeChecker(DateTime val) {
            if (val == default(DateTime)) { return DBNull.Value; }
            return val;
        }

        public static DateTime? DBNullDateTimeNullableHandler(object val) {
            if(val == DBNull.Value) { return null; }
            return (DateTime)val;
        }

        public static object DBNullDateTimeNullableChecker(DateTime? val) {
            if(!val.HasValue) { return DBNull.Value; }
            return val.Value;
        }

        public static bool DBNullBooleanHandler(object val) {
            if (val == DBNull.Value) { return default(Boolean); }
            return (Boolean)val;
        }

        public static Guid DBNullGuidHandler(object val) {
            if (val == DBNull.Value) { return default(Guid); }
            return new Guid(val.ToString());
        }

        public static object DBNullGuidChecker(Guid val) {
            if (val == default(Guid)) { return DBNull.Value; }
            return val.ToString();
        }

        public static byte[] DBNullBytesHandler(object val) {
            if(val == DBNull.Value) { return null; }
            return (byte[])val;
        }

        public static object DBNullBytesChecker(object val) {
            if(val == null) { return DBNull.Value; }
            return val;
        }

        public static EnmEventLevel DBNullEventLevelHandler(object val) {
            if(val == DBNull.Value) { return EnmEventLevel.Information; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmEventLevel), v) ? (EnmEventLevel)v : EnmEventLevel.Information;
        }

        public static EnmEventType DBNullEventTypeHandler(object val) {
            if(val == DBNull.Value) { return EnmEventType.Other; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmEventType), v) ? (EnmEventType)v : EnmEventType.Other;
        }

        public static EnmPasswordFormat DBNullEnmPasswordFormatHandler(object val) {
            if(val == DBNull.Value) { return EnmPasswordFormat.Hashed; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmPasswordFormat), v) ? (EnmPasswordFormat)v : EnmPasswordFormat.Hashed;
        }

        public static EnmSex DBNullEnmSexHandler(object val) {
            if(val == DBNull.Value) { return EnmSex.Male; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmSex), v) ? (EnmSex)v : EnmSex.Male;
        }

        public static EnmDegree DBNullEnmDegreeHandler(object val) {
            if(val == DBNull.Value) { return EnmDegree.Other; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmDegree), v) ? (EnmDegree)v : EnmDegree.Other;
        }

        public static EnmMarriage DBNullEnmMarriageHandler(object val) {
            if(val == DBNull.Value) { return EnmMarriage.Other; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmMarriage), v) ? (EnmMarriage)v : EnmMarriage.Other;
        }

        public static EnmPermission DBNullEnmPermissionHandler(object val) {
            if(val == DBNull.Value) { return EnmPermission.Confirm; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmPermission), v) ? (EnmPermission)v : EnmPermission.Confirm;
        }

        public static EnmPoint DBNullEnmPointHandler(object val) {
            if(val == DBNull.Value) { return EnmPoint.AI; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmPoint), v) ? (EnmPoint)v : EnmPoint.AI;
        }

        public static EnmAlarm DBNullEnmLevelHandler(object val) {
            if(val == DBNull.Value) { return EnmAlarm.Level1; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmAlarm), v) ? (EnmAlarm)v : EnmAlarm.Level1;
        }

        public static EnmConfirm DBNullEnmConfirmStatusHandler(object val) {
            if(val == DBNull.Value) { return EnmConfirm.Unconfirmed; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmConfirm), v) ? (EnmConfirm)v : EnmConfirm.Unconfirmed;
        }

        public static EnmSSH DBNullEnmSSHHandler(object val) {
            if(val == DBNull.Value) { return EnmSSH.Area; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmSSH), v) ? (EnmSSH)v : EnmSSH.Area;
        }

        public static EnmState DBNullEnmStateHandler(object val) {
            if(val == DBNull.Value) { return EnmState.Invalid; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmState), v) ? (EnmState)v : EnmState.Invalid;
        }

        public static EnmFormula DBNullEnmFormulaHandler(object val) {
            if(val == DBNull.Value) { return EnmFormula.KT; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmFormula), v) ? (EnmFormula)v : EnmFormula.KT;
        }

        public static EnmFsuEvent DBNullEnmFsuEventHandler(object val) {
            if(val == DBNull.Value) { return EnmFsuEvent.Undefined; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmFsuEvent), v) ? (EnmFsuEvent)v : EnmFsuEvent.Undefined;
        }
    }
}
