using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetStorageRulePackage {
        public string FsuId { get; set; }

        public List<GetStorageDevice> DeviceList { get; set; }

        public virtual string ToXml() {
            var xmlDoc = new XmlDocument();
            var node = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(node);

            var root = xmlDoc.CreateElement("Request");
            xmlDoc.AppendChild(root);

            var PK_Type = xmlDoc.CreateElement("PK_Type");
            root.AppendChild(PK_Type);

            var Name = xmlDoc.CreateElement("Name");
            Name.InnerText = EnmBIPackType.GET_STORAGERULE.ToString();
            PK_Type.AppendChild(Name);

            var Info = xmlDoc.CreateElement("Info");
            root.AppendChild(Info);

            var FSUID = xmlDoc.CreateElement("FSUID");
            FSUID.InnerText = this.FsuId ?? "";
            Info.AppendChild(FSUID);

            var DeviceList = xmlDoc.CreateElement("DeviceList");
            Info.AppendChild(DeviceList);

            if (this.DeviceList != null) {
                foreach (var device in this.DeviceList) {
                    var Device = xmlDoc.CreateElement("Device");
                    Device.SetAttribute("ID", device.Id ?? "");

                    if (device.Signals != null) {
                        foreach (var signal in device.Signals) {
                            var ID = xmlDoc.CreateElement("TSignalMeasurementId");
                            ID.SetAttribute("ID", signal.Id ?? "");
                            ID.SetAttribute("SignalNumber", signal.SignalNumber ?? "");
                            Device.AppendChild(ID);
                        }
                    }
                    DeviceList.AppendChild(Device);
                }
            }

            return xmlDoc.OuterXml;
        }
    }

    public partial class GetStorageDevice {
        public string Id { get; set; }

        public List<TSignalMeasurementId> Signals { get; set; }
    }
}