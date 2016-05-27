using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BI {
    public partial class SetDataTemplate {
        public string Id { get; set; }

        public string Code { get; set; }

        public List<SetDataDeviceTemplate> Values { get; set; }

        public virtual string ToXml() {
            var xmlDoc = new XmlDocument();
            var node = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(node);

            var root = xmlDoc.CreateElement("Request");
            xmlDoc.AppendChild(root);

            var PK_Type = xmlDoc.CreateElement("PK_Type");
            root.AppendChild(PK_Type);

            var Name = xmlDoc.CreateElement("Name");
            Name.InnerText = EnmBIPackCode.SET_POINT.ToString();
            PK_Type.AppendChild(Name);

            var Code = xmlDoc.CreateElement("Code");
            Code.InnerText = ((int)EnmBIPackCode.SET_POINT).ToString();
            PK_Type.AppendChild(Code);

            var Info = xmlDoc.CreateElement("Info");
            root.AppendChild(Info);

            var FsuID = xmlDoc.CreateElement("FsuID");
            FsuID.InnerText = this.Id ?? "";
            Info.AppendChild(FsuID);

            var FsuCode = xmlDoc.CreateElement("FsuCode");
            FsuCode.InnerText = this.Code ?? "";
            Info.AppendChild(FsuCode);

            var Value = xmlDoc.CreateElement("Value");
            Info.AppendChild(Value);

            var DeviceList = xmlDoc.CreateElement("DeviceList");
            Value.AppendChild(DeviceList);

            if(this.Values != null) {
                foreach(var value in this.Values) {
                    var Device = xmlDoc.CreateElement("Device");
                    Device.SetAttribute("Id", value.Id ?? "");
                    Device.SetAttribute("Code", value.Code ?? "");
                    foreach(var tse in value.Values) {
                        var TSemaphore = xmlDoc.CreateElement("TSemaphore");
                        TSemaphore.SetAttribute("Id", tse.Id ?? "");
                        TSemaphore.SetAttribute("Type", tse.Type.ToString());
                        TSemaphore.SetAttribute("MeasuredVal", tse.MeasuredVal.ToString());
                        TSemaphore.SetAttribute("SetupVal", tse.SetupVal.ToString());
                        TSemaphore.SetAttribute("Status", tse.Status.ToString());
                        Device.AppendChild(TSemaphore);
                    }
                    DeviceList.AppendChild(Device);
                }
            }

            return xmlDoc.OuterXml;
        }
    }
}