using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Services.Sc;
using iPem.Site.Infrastructure;
using System;
using System.Web;

namespace iPem.Site.Extensions {
    public static class LoggerExtensions {
        public static void Debug(this IWebEventService logger, EnmEventType type, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Debug, type, message, userId, exception);
        }
        public static void Information(this IWebEventService logger, EnmEventType type, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Information, type, message, userId, exception);
        }
        public static void Warning(this IWebEventService logger, EnmEventType type, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Warning, type, message, userId, exception);
        }
        public static void Error(this IWebEventService logger, EnmEventType type, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Error, type, message, userId, exception);
        }
        public static void Fatal(this IWebEventService logger, EnmEventType type, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Fatal, type, message, userId, exception);
        }

        public static void Debug(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Debug, type, ip, url, referrer, message, userId, exception);
        }
        public static void Information(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Information, type, ip, url, referrer, message, userId, exception);
        }
        public static void Warning(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Warning, type, ip, url, referrer, message, userId, exception);
        }
        public static void Error(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Error, type, ip, url, referrer, message, userId, exception);
        }
        public static void Fatal(this IWebEventService logger, EnmEventType type, string ip, string url, string referrer, string message, string userId, Exception exception = null) {
            FilteredLog(logger, EnmEventLevel.Fatal, type, ip, url, referrer, message, userId, exception);
        }

        private static void FilteredLog(IWebEventService logger, EnmEventLevel level, EnmEventType type, string message, string userId, Exception exception = null) {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (logger.IsEnabled(level)) {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                FilteredLog(logger, level, type, webHelper.GetCurrentIpAddress(), webHelper.GetThisPageUrl(true), webHelper.GetUrlReferrer(), message, userId, exception);
            }
        }

        private static void FilteredLog(IWebEventService logger, EnmEventLevel level, EnmEventType type, string ip, string url, string referrer, string message, string userId, Exception exception = null) {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (logger.IsEnabled(level)) {
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
                        UserId = userId,
                        CreatedTime = DateTime.Now
                    };

                    logger.Insert(log);
                } catch (Exception ex) {
                    Console.Write(ex.Message);
                }
            }
        }
    }
}
