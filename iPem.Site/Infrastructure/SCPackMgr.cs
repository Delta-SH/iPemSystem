using iPem.Site.Models.BInterface;
using LSCService;
using System;

namespace iPem.Site.Infrastructure {
    public class SCPackMgr : IPackMgr {
        public GetDataAckPackage GetData(string uri, GetDataPackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetDataAckPackage(xmlData);
        }

        public SetPointActPackage SetPoint(string uri, SetPointPackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetPointActPackage(xmlData);
        }

        public GetThresholdAckPackage GetThreshold(string uri, GetThresholdPackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetThresholdAckPackage(xmlData);
        }

        public SetThresholdAckPackage SetThreshold(string uri, SetThresholdPackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetThresholdAckPackage(xmlData);
        }

        public SetFsuRebootAckPackage SetFsuReboot(string uri, SetFsuRebootPackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetFsuRebootAckPackage(xmlData);
        }

        public GetDevConfAckPackage GetDevConf(string uri, GetDevConfPackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetDevConfAckPackage(xmlData);
        }

        public GetStorageRuleAckPackage GetStorageRule(string uri, GetStorageRulePackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 10000 };
            var xmlData = service.invoke(package.ToXml());
            return new GetStorageRuleAckPackage(xmlData);
        }

        public SetStorageRuleAckPackage SetStorageRule(string uri, SetStorageRulePackage package) {
            var service = new LSCServiceService() { Url = uri, Timeout = 15000 };
            var xmlData = service.invoke(package.ToXml());
            return new SetStorageRuleAckPackage(xmlData);
        }
    }
}