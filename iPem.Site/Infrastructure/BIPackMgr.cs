using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Site.Models;
using iPem.Site.Models.BInterface;
using System;

namespace iPem.Site.Infrastructure {
    public abstract class BIPackMgr {
        public static GetDataAckPackage GetData(FsuExt fsu, WsValues ws, GetDataPackage package) {
            if(fsu == null) throw new iPemException("Fsu通信未配置");
            if(ws == null) throw new iPemException("WebService通信未配置");
            if(package == null) throw new iPemException("数据包不能为空");
            return GetData(new UriBuilder("http", fsu.IP, fsu.Port, ws.fsuPath ?? "").ToString(), package);
        }

        public static GetDataAckPackage GetData(string url, GetDataPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetDataAckPackage(xmlData);
        }

        public static SetPointActPackage SetPoint(FsuExt fsu, WsValues ws, SetPointPackage package) {
            if(fsu == null) throw new iPemException("Fsu通信未配置");
            if(ws == null) throw new iPemException("WebService通信未配置");
            if(package == null) throw new iPemException("数据包不能为空");
            return SetPoint(new UriBuilder("http", fsu.IP, fsu.Port, ws.fsuPath ?? "").ToString(), package);
        }

        public static SetPointActPackage SetPoint(string url, SetPointPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetPointActPackage(xmlData);
        }

        public static GetThresholdAckPackage GetThreshold(FsuExt fsu, WsValues ws, GetThresholdPackage package) {
            if(fsu == null) throw new iPemException("Fsu通信未配置");
            if(ws == null) throw new iPemException("WebService通信未配置");
            if(package == null) throw new iPemException("数据包不能为空");
            return GetThreshold(new UriBuilder("http", fsu.IP, fsu.Port, ws.fsuPath ?? "").ToString(), package);
        }

        public static GetThresholdAckPackage GetThreshold(string url, GetThresholdPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetThresholdAckPackage(xmlData);
        }

        public static SetThresholdAckPackage SetThreshold(FsuExt fsu, WsValues ws, SetThresholdPackage package) {
            if(fsu == null) throw new iPemException("Fsu通信未配置");
            if(ws == null) throw new iPemException("WebService通信未配置");
            if(package == null) throw new iPemException("数据包不能为空");
            return SetThreshold(new UriBuilder("http", fsu.IP, fsu.Port, ws.fsuPath ?? "").ToString(), package);
        }

        public static SetThresholdAckPackage SetThreshold(string url, SetThresholdPackage package) {
            var service = new FSUServiceService() { Url = url, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetThresholdAckPackage(xmlData);
        }
    }
}