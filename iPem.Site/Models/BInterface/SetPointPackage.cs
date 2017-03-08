using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class SetPointPackage {
        public string FsuId { get; set; }

        public List<SetPointDevice> DeviceList { get; set; }

        public virtual string ToXml() {
            var xmlDoc = new XmlDocument();
            var node = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(node);

            var root = xmlDoc.CreateElement("Request");
            xmlDoc.AppendChild(root);

            var PK_Type = xmlDoc.CreateElement("PK_Type");
            root.AppendChild(PK_Type);

            var Name = xmlDoc.CreateElement("Name");
            Name.InnerText = EnmPackType.SET_POINT.ToString();
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
                        foreach(var tSemaphore in device.Values) {
                            var TSemaphore = xmlDoc.CreateElement("TSemaphore");
                            TSemaphore.SetAttribute("ID", tSemaphore.Id ?? "");
                            TSemaphore.SetAttribute("SignalNumber", tSemaphore.SignalNumber ?? "");
                            TSemaphore.SetAttribute("Type", ((int)tSemaphore.Type).ToString());
                            TSemaphore.SetAttribute("MeasuredVal", tSemaphore.MeasuredVal);
                            TSemaphore.SetAttribute("SetupVal", tSemaphore.SetupVal);
                            TSemaphore.SetAttribute("Status", ((int)tSemaphore.Status).ToString());
                            TSemaphore.SetAttribute("Time", tSemaphore.Time.ToString("yyyy-MM-dd HH:mm:ss"));
                            Device.AppendChild(TSemaphore);
                        }
                    }

                    DeviceList.AppendChild(Device);
                }
            }

            return xmlDoc.OuterXml;
        }
    }

    public partial class SetPointDevice {
        public string Id { get; set; }

        public List<TSemaphore> Values { get; set; }
    }
}