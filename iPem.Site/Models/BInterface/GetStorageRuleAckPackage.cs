using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetStorageRuleAckPackage {
        public GetStorageRuleAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.GET_STORAGERULE_ACK.ToString())) 
                return;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmBIResult), result) ? (EnmBIResult)result : EnmBIResult.FAILURE;
            }

            var FailureCause = xmlDoc.SelectSingleNode("/Response/Info/FailureCause");
            if(FailureCause != null) this.FailureCause = FailureCause.InnerText;

            var DeviceList = xmlDoc.SelectNodes("/Response/Info/DeviceList/Device");
            if(DeviceList != null && DeviceList.Count > 0) {
                this.DeviceList = new List<GetStorageRuleAckDevice>();
                foreach(XmlNode node in DeviceList) {
                    var device = new GetStorageRuleAckDevice();
                    device.Id = node.Attributes["ID"].InnerText;
                    device.Rules = new List<TStorageRule>();

                    if (node.HasChildNodes) {
                        foreach (XmlNode child in node.ChildNodes) {
                            var tStorageRule = new TStorageRule();
                            var type = child.Attributes["Type"] != null ? int.Parse(child.Attributes["Type"].InnerText) : (int)EnmBIPoint.AI;

                            tStorageRule.Id = child.Attributes["ID"].InnerText;
                            tStorageRule.SignalNumber = child.Attributes["SignalNumber"].InnerText;
                            tStorageRule.Type = Enum.IsDefined(typeof(EnmBIPoint), type) ? (EnmBIPoint)type : EnmBIPoint.AL;
                            tStorageRule.AbsoluteVal = child.Attributes["AbsoluteVal"].InnerText;
                            tStorageRule.RelativeVal = child.Attributes["RelativeVal"].InnerText;
                            tStorageRule.StorageInterval = child.Attributes["StorageInterval"].InnerText;
                            tStorageRule.StorageRefTime = child.Attributes["StorageRefTime"].InnerText;
                            device.Rules.Add(tStorageRule);
                        }
                    }

                    this.DeviceList.Add(device);
                }
            }
        }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public List<GetStorageRuleAckDevice> DeviceList { get; set; }
    }

    public partial class GetStorageRuleAckDevice {
        public string Id { get; set; }

        public List<TStorageRule> Rules { get; set; }
    }
}