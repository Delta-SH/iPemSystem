using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class SetThresholdPackage {
        public string FsuId { get; set; }

        public List<SetThresholdDevice> DeviceList { get; set; }

        public virtual string ToXml() {
            var xmlDoc = new XmlDocument();
            var node = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(node);

            var root = xmlDoc.CreateElement("Request");
            xmlDoc.AppendChild(root);

            var PK_Type = xmlDoc.CreateElement("PK_Type");
            root.AppendChild(PK_Type);

            var Name = xmlDoc.CreateElement("Name");
            Name.InnerText = EnmBIPackType.SET_THRESHOLD.ToString();
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

            if(this.DeviceList != null) {
                foreach(var device in this.DeviceList) {
                    var Device = xmlDoc.CreateElement("Device");
                    Device.SetAttribute("ID", device.Id ?? "");
                    if(device.Values != null) {
                        foreach(var tThreshold in device.Values) {
                            var TThreshold = xmlDoc.CreateElement("TThreshold");
                            TThreshold.SetAttribute("ID", tThreshold.Id ?? "");
                            TThreshold.SetAttribute("SignalNumber", tThreshold.SignalNumber ?? "");
                            TThreshold.SetAttribute("Type", ((int)tThreshold.Type).ToString());
                            TThreshold.SetAttribute("Threshold", tThreshold.Threshold);
                            TThreshold.SetAttribute("AlarmLevel", ((int)tThreshold.AlarmLevel).ToString());
                            TThreshold.SetAttribute("NMAlarmID", tThreshold.NMAlarmID);
                            Device.AppendChild(TThreshold);
                        }
                    }

                    DeviceList.AppendChild(Device);
                }
            }

            return xmlDoc.OuterXml;
        }
    }

    public partial class SetThresholdDevice {
        public string Id { get; set; }

        public List<TThreshold> Values { get; set; }
    }
}