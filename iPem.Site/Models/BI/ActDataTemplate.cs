using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BI {
    public partial class ActDataTemplate {
        public string Id { get; set; }

        public string Code { get; set; }

        public List<ActDataDeviceTemplate> Values { get; set; }

        public virtual string ToXml() {
            var xmlDoc = new XmlDocument();
            var node = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(node);

            var root = xmlDoc.CreateElement("Request");
            xmlDoc.AppendChild(root);

            var PK_Type = xmlDoc.CreateElement("PK_Type");
            root.AppendChild(PK_Type);

            var Name = xmlDoc.CreateElement("Name");
            Name.InnerText = EnmBIPackCode.GET_DATA.ToString();
            PK_Type.AppendChild(Name);

            var Code = xmlDoc.CreateElement("Code");
            Code.InnerText = ((int)EnmBIPackCode.GET_DATA).ToString();
            PK_Type.AppendChild(Code);

            var Info = xmlDoc.CreateElement("Info");
            root.AppendChild(Info);

            var FsuID = xmlDoc.CreateElement("FsuID");
            FsuID.InnerText = this.Id ?? "";
            Info.AppendChild(FsuID);

            var FsuCode = xmlDoc.CreateElement("FsuCode");
            FsuCode.InnerText = this.Code ?? "";
            Info.AppendChild(FsuCode);

            var DeviceList = xmlDoc.CreateElement("DeviceList");
            Info.AppendChild(DeviceList);

            if(this.Values != null) {
                foreach(var value in this.Values) {
                    var Device = xmlDoc.CreateElement("Device");
                    Device.SetAttribute("Id", value.Id ?? "");
                    Device.SetAttribute("Code", value.Code ?? "");
                    foreach(var id in value.Values) {
                        var Id = xmlDoc.CreateElement("Id");
                        Id.InnerText = id ?? "";
                        Device.AppendChild(Id);
                    }
                    DeviceList.AppendChild(Device);
                }
            }

            return xmlDoc.OuterXml;
        }
    }
}