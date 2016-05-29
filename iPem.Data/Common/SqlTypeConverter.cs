﻿using System;
using System.Collections.Generic;
using iPem.Core;
using iPem.Core.Enum;

namespace iPem.Data.Common {
    public class SqlTypeConverter {
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
            if (val == DBNull.Value) { return int.MinValue; }
            return (Int32)val;
        }

        /// <summary>
        /// DBNull Int32 Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullInt32Checker(int val) {
            if(val == int.MinValue) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Int64 Handler
        /// </summary>
        /// <param name="val">val</param>
        public static long DBNullInt64Handler(object val) {
            if (val == DBNull.Value) { return long.MinValue; }
            return (Int64)val;
        }

        /// <summary>
        /// DBNull Int64 Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullInt64Checker(long val) {
            if(val == long.MinValue) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Float Handler
        /// </summary>
        /// <param name="val">val</param>
        public static float DBNullFloatHandler(object val) {
            if (val == DBNull.Value) { return float.MinValue; }
            return (Single)val;
        }

        /// <summary>
        /// DBNull Float Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullFloatChecker(float val) {
            if(val == float.MinValue) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Double Handler
        /// </summary>
        /// <param name="val">val</param>
        public static double DBNullDoubleHandler(object val) {
            if (val == DBNull.Value) { return double.MinValue; }
            return (Double)val;
        }

        /// <summary>
        /// DBNull Double Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullDoubleChecker(double val) {
            if(val == double.MinValue) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull DateTime Handler
        /// </summary>
        /// <param name="val">val</param>
        public static DateTime DBNullDateTimeHandler(object val) {
            if (val == DBNull.Value) { return default(DateTime); }
            return (DateTime)val;
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
        /// DBNull DateTime Nullable Handler
        /// </summary>
        /// <param name="val">val</param>
        public static DateTime? DBNullDateTimeNullableHandler(object val) {
            if(val == DBNull.Value) { return null; }
            return (DateTime)val;
        }

        /// <summary>
        /// DBNull DateTime Nullable Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullDateTimeNullableChecker(DateTime? val) {
            if(!val.HasValue) { return DBNull.Value; }
            return val.Value;
        }

        /// <summary>
        /// DBNull Boolean Handler
        /// </summary>
        /// <param name="val">val</param>
        public static bool DBNullBooleanHandler(object val) {
            if (val == DBNull.Value) { return default(Boolean); }
            return (Boolean)val;
        }

        /// <summary>
        /// DBNull Guid Handler
        /// </summary>
        /// <param name="val">val</param>
        public static Guid DBNullGuidHandler(object val) {
            if (val == DBNull.Value) { return default(Guid); }
            return new Guid(val.ToString());
        }

        /// <summary>
        /// DBNull Guid Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullGuidChecker(Guid val) {
            if (val == default(Guid)) { return DBNull.Value; }
            return val.ToString();
        }

        /// <summary>
        /// DBNull Bytes Handler
        /// </summary>
        /// <param name="val">val</param>
        public static byte[] DBNullBytesHandler(object val) {
            if(val == DBNull.Value) { return null; }
            return (byte[])val;
        }

        /// <summary>
        /// DBNull Bytes Checker
        /// </summary>
        /// <param name="val">val</param>
        public static object DBNullBytesChecker(object val) {
            if(val == null) { return DBNull.Value; }
            return val;
        }

        /// <summary>
        /// DBNull Event Level Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmEventLevel DBNullEventLevelHandler(object val) {
            if(val == DBNull.Value) { return EnmEventLevel.Information; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmEventLevel), v) ? (EnmEventLevel)v : EnmEventLevel.Information;
        }

        /// <summary>
        /// DBNull Event Type Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmEventType DBNullEventTypeHandler(object val) {
            if(val == DBNull.Value) { return EnmEventType.Other; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmEventType), v) ? (EnmEventType)v : EnmEventType.Other;
        }

        /// <summary>
        /// DBNull Password Format Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmPasswordFormat DBNullEnmPasswordFormatHandler(object val) {
            if(val == DBNull.Value) { return EnmPasswordFormat.Hashed; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmPasswordFormat), v) ? (EnmPasswordFormat)v : EnmPasswordFormat.Hashed;
        }

        /// <summary>
        /// DBNull Sex Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmSex DBNullEnmSexHandler(object val) {
            if(val == DBNull.Value) { return EnmSex.Male; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmSex), v) ? (EnmSex)v : EnmSex.Male;
        }

        /// <summary>
        /// DBNull Degree Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmDegree DBNullEnmDegreeHandler(object val) {
            if(val == DBNull.Value) { return EnmDegree.Other; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmDegree), v) ? (EnmDegree)v : EnmDegree.Other;
        }

        /// <summary>
        /// DBNull Marriage Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmMarriage DBNullEnmMarriageHandler(object val) {
            if(val == DBNull.Value) { return EnmMarriage.Other; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmMarriage), v) ? (EnmMarriage)v : EnmMarriage.Other;
        }

        /// <summary>
        /// DBNull Operation Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmOperation DBNullEnmOperationHandler(object val) {
            if(val == DBNull.Value) { return EnmOperation.Confirm; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmOperation), v) ? (EnmOperation)v : EnmOperation.Confirm;
        }

        /// <summary>
        /// DBNull Node Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmPoint DBNullEnmNodeHandler(object val) {
            if(val == DBNull.Value) { return EnmPoint.AI; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmPoint), v) ? (EnmPoint)v : EnmPoint.AI;
        }

        /// <summary>
        /// DBNull Level Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmAlarmLevel DBNullEnmLevelHandler(object val) {
            if(val == DBNull.Value) { return EnmAlarmLevel.Level1; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmAlarmLevel), v) ? (EnmAlarmLevel)v : EnmAlarmLevel.Level1;
        }

        /// <summary>
        /// DBNull Flag Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmAlarmFlag DBNullEnmFlagHandler(object val) {
            if(val == DBNull.Value) { return EnmAlarmFlag.Begin; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmAlarmFlag), v) ? (EnmAlarmFlag)v : EnmAlarmFlag.Begin;
        }

        /// <summary>
        /// DBNull Confirm Status Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmConfirmStatus DBNullEnmConfirmStatusHandler(object val) {
            if(val == DBNull.Value) { return EnmConfirmStatus.Unconfirmed; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmConfirmStatus), v) ? (EnmConfirmStatus)v : EnmConfirmStatus.Unconfirmed;
        }

        /// <summary>
        /// DBNull Organisation Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmOrganization DBNullEnmOrganizationHandler(object val) {
            if(val == DBNull.Value) { return EnmOrganization.Area; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmOrganization), v) ? (EnmOrganization)v : EnmOrganization.Area;
        }

        /// <summary>
        /// DBNull PointStatus Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmPointStatus DBNullEnmPointStatusHandler(object val) {
            if(val == DBNull.Value) { return EnmPointStatus.Invalid; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmPointStatus), v) ? (EnmPointStatus)v : EnmPointStatus.Invalid;
        }

        /// <summary>
        /// DBNull EndType Handler
        /// </summary>
        /// <param name="val">val</param>
        public static EnmAlarmEndType DBNullEnmEndTypeHandler(object val) {
            if(val == DBNull.Value) { return EnmAlarmEndType.Normal; }

            var v = (Int32)val;
            return Enum.IsDefined(typeof(EnmAlarmEndType), v) ? (EnmAlarmEndType)v : EnmAlarmEndType.Normal;
        }
    }
}