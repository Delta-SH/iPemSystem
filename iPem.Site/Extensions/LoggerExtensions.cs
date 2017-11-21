﻿using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using System;
using System.Web;

namespace iPem.Site.Extensions {
    public static class LoggerExtensions {
        public static void Debug(this IWebEventService logger, EnmEventType type, string message, Exception exception = null, Guid? userId = null) {
            FilteredLog(logger, EnmEventLevel.Debug, type, message, exception, userId);
        }
        public static void Information(this IWebEventService logger, EnmEventType type, string message, Exception exception = null, Guid? userId = null) {
            FilteredLog(logger, EnmEventLevel.Information, type, message, exception, userId);
        }
        public static void Warning(this IWebEventService logger, EnmEventType type, string message, Exception exception = null, Guid? userId = null) {
            FilteredLog(logger, EnmEventLevel.Warning, type, message, exception, userId);
        }
        public static void Error(this IWebEventService logger, EnmEventType type, string message, Exception exception = null, Guid? userId = null) {
            FilteredLog(logger, EnmEventLevel.Error, type, message, exception, userId);
        }
        public static void Fatal(this IWebEventService logger, EnmEventType type, string message, Exception exception = null, Guid? userId = null) {
            FilteredLog(logger, EnmEventLevel.Fatal, type, message, exception, userId);
        }

        public static void Debug(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, Exception exception = null, Guid? uid = null) {
            FilteredLog(logger, EnmEventLevel.Debug, type, ip, url, referrer, message, exception, uid);
        }
        public static void Information(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, Exception exception = null, Guid? uid = null) {
            FilteredLog(logger, EnmEventLevel.Information, type, ip, url, referrer, message, exception, uid);
        }
        public static void Warning(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, Exception exception = null, Guid? uid = null) {
            FilteredLog(logger, EnmEventLevel.Warning, type, ip, url, referrer, message, exception, uid);
        }
        public static void Error(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, Exception exception = null, Guid? uid = null) {
            FilteredLog(logger, EnmEventLevel.Error, type, ip, url, referrer, message, exception, uid);
        }
        public static void Fatal(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, Exception exception = null, Guid? uid = null) {
            FilteredLog(logger, EnmEventLevel.Fatal, type, ip, url, referrer, message, exception, uid);
        }

        private static void FilteredLog(IWebEventService logger, EnmEventLevel level, EnmEventType type, string message, Exception exception = null, Guid? uid = null) {
            //don't log thread abort exception
            if(exception is System.Threading.ThreadAbortException)
                return;

            if(logger.IsEnabled(level)) {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                FilteredLog(logger, level, type, webHelper.GetCurrentIpAddress(), webHelper.GetThisPageUrl(true), webHelper.GetUrlReferrer(), message, exception, uid);
            }
        }

        private static void FilteredLog(IWebEventService logger, EnmEventLevel level, EnmEventType type, string ip, string url, string referrer, string message, Exception exception = null, Guid? uid = null) {
            //don't log thread abort exception
            if(exception is System.Threading.ThreadAbortException)
                return;

            if(logger.IsEnabled(level)) {
                try {
                    var log = new H_WebEvent {
                        Id = Guid.NewGuid(),
                        Level = level,
                        Type = type,
                        ShortMessage = message,
                        FullMessage = exception == null ? string.Empty : exception.ToString(),
                        IpAddress = ip,
                        PageUrl = url,
                        ReferrerUrl = referrer,
                        UserId = uid,
                        CreatedTime = DateTime.Now
                    };

                    logger.Insert(log);
                } catch(Exception ex) {
                    Console.Write(ex.Message);
                }
            }
        }
    }
}
