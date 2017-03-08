using iPem.Core.Domain.Rs;
using iPem.Site.Models.BInterface;
using System;

namespace iPem.Site.Infrastructure {
    public abstract class BIPackMgr {
        public static GetDataAckPackage GetData(FsuExt fsu, GetDataPackage package) {
            if(fsu == null) return null;
            return GetData(new UriBuilder("http", fsu.IP, fsu.Port, "").ToString(), package);
        }

        public static GetDataAckPackage GetData(string url, GetDataPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetDataAckPackage(xmlData);
        }

        public static SetPointActPackage SetPoint(FsuExt fsu, SetPointPackage package) {
            if(fsu == null) return null;
            return SetPoint(new UriBuilder("http", fsu.IP, fsu.Port, "").ToString(), package);
        }

        public static SetPointActPackage SetPoint(string url, SetPointPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetPointActPackage(xmlData);
        }

        public static GetThresholdAckPackage GetThreshold(FsuExt fsu, GetDataPackage package) {
            if(fsu == null) return null;
            return GetThreshold(new UriBuilder("http", fsu.IP, fsu.Port, "").ToString(), package);
        }

        public static GetThresholdAckPackage GetThreshold(string url, GetDataPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetThresholdAckPackage(xmlData);
        }

        public static SetThresholdAckPackage SetThreshold(FsuExt fsu, GetDataPackage package) {
            if(fsu == null) return null;
            return SetThreshold(new UriBuilder("http", fsu.IP, fsu.Port, "").ToString(), package);
        }

        public static SetThresholdAckPackage SetThreshold(string url, GetDataPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetThresholdAckPackage(xmlData);
        }
    }
}