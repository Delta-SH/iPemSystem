using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class SetStorageRulePackage {
        public string FsuId { get; set; }

        public List<SetStorageRuleDevice> DeviceList { get; set; }

        public virtual string ToXml() {
            var xmlDoc = new XmlDocument();
            var node = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(node);

            var root = xmlDoc.CreateElement("Request");
            xmlDoc.AppendChild(root);

            var PK_Type = xmlDoc.CreateElement("PK_Type");
            root.AppendChild(PK_Type);

            var Name = xmlDoc.CreateElement("Name");
            Name.InnerText = EnmBIPackType.SET_STORAGERULE.ToString();
            PK_Type.AppendChild(Name);

            var Info = xmlDoc.CreateElement("Info");
            root.AppendChild(Info);

            var FsuID = xmlDoc.CreateElement("FSUID");
            FsuID.InnerText = this.FsuId ?? "";
            Info.AppendChild(FsuID);

            var Value = xmlDoc.CreateElement("Value");
            Info.AppendChild(Value);

            var DeviceList = xmlDoc.CreateElement("DeviceList");
            Value.AppendChild(DeviceList);

            if (this.DeviceList != null) {
                foreach (var device in this.DeviceList) {
                    var Device = xmlDoc.CreateElement("Device");
                    Device.SetAttribute("ID", device.Id ?? "");
                    if (device.Rules != null) {
                        foreach (var rule in device.Rules) {
                            var tStorageRule = xmlDoc.CreateElement("TStorageRule");
                            tStorageRule.SetAttribute("ID", rule.Id);
                            tStorageRule.SetAttribute("SignalNumber", rule.SignalNumber);
                            tStorageRule.SetAttribute("Type", ((int)rule.Type).ToString());
                            tStorageRule.SetAttribute("AbsoluteVal", rule.AbsoluteVal);
                            tStorageRule.SetAttribute("RelativeVal", rule.RelativeVal);
                            tStorageRule.SetAttribute("StorageInterval", rule.StorageInterval);
                            tStorageRule.SetAttribute("StorageRefTime", rule.StorageRefTime);
                            Device.AppendChild(tStorageRule);
                        }
                    }

                    DeviceList.AppendChild(Device);
                }
            }

            return xmlDoc.OuterXml;
        }
    }

    public partial class SetStorageRuleDevice {
        public string Id { get; set; }

        public List<TStorageRule> Rules { get; set; }
    }
}