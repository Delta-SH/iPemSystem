using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BI {
    public partial class SetDataAckTemplate {
        public SetDataAckTemplate(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Code = xmlDoc.SelectSingleNode("/Response/PK_Type/Code");
            if(Code == null)
                return;

            if(!Code.InnerText.Equals(((int)EnmBIPackCode.SET_POINT_ACK).ToString()))
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

            var DeviceList = xmlDoc.SelectNodes("/Response/Info/DeviceList/Device");
            if(DeviceList != null && DeviceList.Count > 0) {
                this.Values = new List<SetDataDeviceAckTemplate>();
                foreach(XmlNode node in DeviceList) {
                    var device = new SetDataDeviceAckTemplate();
                    device.Id = node.Attributes["Id"].InnerText;
                    device.Code = node.Attributes["Code"].InnerText;
                    device.Success = new List<string>();
                    device.Failure = new List<string>();

                    var success = node.SelectNodes("SuccessList/Id");
                    if(success != null && success.Count > 0)
                        foreach(XmlNode s in success)
                            device.Success.Add(s.InnerText);

                    var failure = node.SelectNodes("FailList/Id");
                    if(failure != null && failure.Count > 0)
                        foreach(XmlNode f in failure)
                            device.Failure.Add(f.InnerText);

                    this.Values.Add(device);
                }
            }
        }

        public string Id { get; private set; }

        public string Code { get; private set; }

        public EnmBIResult Result { get; private set; }

        public List<SetDataDeviceAckTemplate> Values { get; private set; }
    }
}