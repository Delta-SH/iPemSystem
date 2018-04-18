using FSUService;
using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Site.Models;
using iPem.Site.Models.BInterface;
using System;

namespace iPem.Site.Infrastructure {
    public class BIPackMgr : IPackMgr {
        public GetFsuLoginAckPackage GetFsuLogin(string uri, GetFsuLoginPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetFsuLoginAckPackage(xmlData);
        }

        public SetFsuLoginAckPackage SetFsuLogin(string uri, SetFsuLoginPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetFsuLoginAckPackage(xmlData);
        }

        public GetFtpLoginAckPackage GetFtpLogin(string uri, GetFtpLoginPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetFtpLoginAckPackage(xmlData);
        }

        public SetFtpLoginAckPackage SetFtpLogin(string uri, SetFtpLoginPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetFtpLoginAckPackage(xmlData);
        }

        public GetDataAckPackage GetData(string uri, GetDataPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetDataAckPackage(xmlData);
        }

        public SetPointActPackage SetPoint(string uri, SetPointPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetPointActPackage(xmlData);
        }

        public GetThresholdAckPackage GetThreshold(string uri, GetThresholdPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetThresholdAckPackage(xmlData);
        }

        public SetThresholdAckPackage SetThreshold(string uri, SetThresholdPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetThresholdAckPackage(xmlData);
        }

        public SetFsuRebootAckPackage SetFsuReboot(string uri, SetFsuRebootPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetFsuRebootAckPackage(xmlData);
        }

        public GetDevConfAckPackage GetDevConf(string uri, GetDevConfPackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetDevConfAckPackage(xmlData);
        }

        public GetStorageRuleAckPackage GetStorageRule(string uri, GetStorageRulePackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetStorageRuleAckPackage(xmlData);
        }

        public SetStorageRuleAckPackage SetStorageRule(string uri, SetStorageRulePackage package) {
            var service = new FSUServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetStorageRuleAckPackage(xmlData);
        }
    }
}