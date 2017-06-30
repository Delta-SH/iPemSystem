using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetThresholdAckPackage {
        public GetThresholdAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.GET_THRESHOLD_ACK.ToString()))
                return;

            var FsuId = xmlDoc.SelectSingleNode("/Response/Info/FSUID");
            if(FsuId != null) this.FsuId = FsuId.InnerText;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmBIResult), result) ? (EnmBIResult)result : EnmBIResult.FAILURE;
            }

            var FailureCause = xmlDoc.SelectSingleNode("/Response/Info/FailureCause");
            if(FailureCause != null) this.FailureCause = FailureCause.InnerText;

            var DeviceList = xmlDoc.SelectNodes("/Response/Info/Values/DeviceList/Device");
            if(DeviceList != null && DeviceList.Count > 0) {
                this.DeviceList = new List<GetThresholdAckDevice>();
                foreach(XmlNode node in DeviceList) {
                    var device = new GetThresholdAckDevice();
                    device.Id = node.Attributes["ID"].InnerText;
                    device.Values = new List<TThreshold>();

                    if(node.HasChildNodes) {
                        foreach(XmlNode child in node.ChildNodes) {
                            var tThreshold = new TThreshold();
                            var type = child.Attributes["Type"] != null ? int.Parse(child.Attributes["Type"].InnerText) : (int)EnmBIPoint.AL;
                            var level = child.Attributes["AlarmLevel"] != null ? int.Parse(child.Attributes["AlarmLevel"].InnerText) : (int)EnmBILevel.HINT;

                            tThreshold.Id = child.Attributes["ID"].InnerText;
                            tThreshold.SignalNumber = child.Attributes["SignalNumber"].InnerText;
                            tThreshold.Type = Enum.IsDefined(typeof(EnmBIPoint), type) ? (EnmBIPoint)type : EnmBIPoint.AL;
                            tThreshold.Threshold = child.Attributes["Threshold"].InnerText;
                            tThreshold.AlarmLevel = Enum.IsDefined(typeof(EnmBILevel), level) ? (EnmBILevel)level : EnmBILevel.HINT;
                            tThreshold.NMAlarmID = child.Attributes["NMAlarmID"].InnerText;
                            device.Values.Add(tThreshold);
                        }
                    }

                    this.DeviceList.Add(device);
                }
            }
        }

        public string FsuId { get; set; }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public List<GetThresholdAckDevice> DeviceList { get; set; }
    }

    public partial class GetThresholdAckDevice {
        public string Id { get; set; }

        public List<TThreshold> Values { get; set; }
    }
}