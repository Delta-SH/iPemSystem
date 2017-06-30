using iPem.Site.Models.BInterface;
using System;

namespace iPem.Site.Infrastructure {
    public interface IPackMgr {
        GetDataAckPackage GetData(string uri, GetDataPackage package);

        SetPointActPackage SetPoint(string uri, SetPointPackage package);

        GetThresholdAckPackage GetThreshold(string uri, GetThresholdPackage package);

        SetThresholdAckPackage SetThreshold(string uri, SetThresholdPackage package);

        SetFsuRebootAckPackage SetFsuReboot(string uri, SetFsuRebootPackage package);

        GetDevConfAckPackage GetDevConf(string uri, GetDevConfPackage package);

        GetStorageRuleAckPackage GetStorageRule(string uri, GetStorageRulePackage package);

        SetStorageRuleAckPackage SetStorageRule(string uri, SetStorageRulePackage package);
    }
}