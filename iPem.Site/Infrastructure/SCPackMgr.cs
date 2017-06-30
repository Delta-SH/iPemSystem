using iPem.Site.Models.BInterface;
using System;

namespace iPem.Site.Infrastructure {
    public class SCPackMgr : IPackMgr {
        public GetDataAckPackage GetData(string uri, GetDataPackage package) {
            throw new NotImplementedException();
        }

        public SetPointActPackage SetPoint(string uri, SetPointPackage package) {
            throw new NotImplementedException();
        }

        public GetThresholdAckPackage GetThreshold(string uri, GetThresholdPackage package) {
            throw new NotImplementedException();
        }

        public SetThresholdAckPackage SetThreshold(string uri, SetThresholdPackage package) {
            throw new NotImplementedException();
        }

        public SetFsuRebootAckPackage SetFsuReboot(string uri, SetFsuRebootPackage package) {
            throw new NotImplementedException();
        }

        public GetDevConfAckPackage GetDevConf(string uri, GetDevConfPackage package) {
            throw new NotImplementedException();
        }

        public GetStorageRuleAckPackage GetStorageRule(string uri, GetStorageRulePackage package) {
            throw new NotImplementedException();
        }

        public SetStorageRuleAckPackage SetStorageRule(string uri, SetStorageRulePackage package) {
            throw new NotImplementedException();
        }
    }
}