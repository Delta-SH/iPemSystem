using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BI {
    public partial class ActDataAckTemplate {

        public ActDataAckTemplate(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Code = xmlDoc.SelectSingleNode("/Response/PK_Type/Code");
            if(Code == null)
                return;

            if(!Code.InnerText.Equals(((int)EnmBIPackCode.GET_DATA_ACK).ToString()))
                return;

            var FsuId = xmlDoc.SelectSingleNode("/Response/Info/FsuId");
            if(FsuId != null)
                this.Id = FsuId.InnerText;

            var FsuCode = xmlDoc.SelectSingleNode("/Response/Info/FsuCode");
            if(FsuCode != null)
                this.Code = FsuCode.InnerText;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmBIResult), result) ? (EnmBIResult)result : EnmBIResult.FAILURE;
            }

            var DeviceList = xmlDoc.SelectNodes("/Response/Info/Values/DeviceList/Device");
            if(DeviceList != null && DeviceList.Count > 0) {
                this.Values = new List<ActDataDeviceAckTemplate>();
                foreach(XmlNode node in DeviceList) {
                    var device = new ActDataDeviceAckTemplate();
                    device.Id = node.Attributes["Id"].InnerText;
                    device.Code = node.Attributes["Code"].InnerText;
                    device.Values = new List<TSemaphore>();

                    if(node.HasChildNodes) {
                        foreach(XmlNode child in node.ChildNodes) {
                            var tse = new TSemaphore();
                            tse.Id = child.Attributes["Id"] != null ? child.Attributes["Id"].InnerText : string.Empty;
                            tse.Type = child.Attributes["Type"] != null ? int.Parse(child.Attributes["Type"].InnerText) : 0;
                            tse.MeasuredVal = child.Attributes["MeasuredVal"] != null ? double.Parse(child.Attributes["MeasuredVal"].InnerText) : 0;
                            tse.SetupVal = child.Attributes["SetupVal"] != null ? float.Parse(child.Attributes["SetupVal"].InnerText) : 0;
                            tse.Status = child.Attributes["Status"] != null ? int.Parse(child.Attributes["Status"].InnerText) : 0;
                            tse.RecordTime = DateTime.Now;//child.Attributes["RecordTime"] != null ? DateTime.Parse(child.Attributes["RecordTime"].InnerText) : DateTime.Now;
                            device.Values.Add(tse);
                        }
                    }

                    this.Values.Add(device);
                }
            }
        }

        public string Id { get; private set; }

        public string Code { get; private set; }

        public EnmBIResult Result { get; private set; }

        public List<ActDataDeviceAckTemplate> Values { get; private set; }

    }
}