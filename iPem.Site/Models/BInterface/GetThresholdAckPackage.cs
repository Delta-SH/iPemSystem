using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetThresholdAckPackage {
        public GetThresholdAckPackage(string xmlData) {
            this.Result = EnmResult.Failure;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmPackType.GET_THRESHOLD_ACK.ToString()))
                return;

            var FsuId = xmlDoc.SelectSingleNode("/Response/Info/FSUID");
            if(FsuId != null) this.FsuId = FsuId.InnerText;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmResult), result) ? (EnmResult)result : EnmResult.Failure;
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
                            tThreshold.Id = child.Attributes["ID"] != null ? child.Attributes["ID"].InnerText : string.Empty;
                            tThreshold.SignalNumber = child.Attributes["SignalNumber"] != null ? child.Attributes["SignalNumber"].InnerText : string.Empty;
                            var type = child.Attributes["Type"] != null ? int.Parse(child.Attributes["Type"].InnerText) : (int)EnmBIPoint.AL;
                            tThreshold.Type = Enum.IsDefined(typeof(EnmBIPoint), type) ? (EnmBIPoint)type : EnmBIPoint.AL;
                            tThreshold.Threshold = child.Attributes["Threshold"] != null ? child.Attributes["Threshold"].InnerText : "";
                            var level = child.Attributes["AlarmLevel"] != null ? int.Parse(child.Attributes["AlarmLevel"].InnerText) : (int)EnmLevel.Level0;
                            tThreshold.AlarmLevel = Enum.IsDefined(typeof(EnmLevel), level) ? (EnmLevel)level : EnmLevel.Level0;
                            tThreshold.NMAlarmID = child.Attributes["NMAlarmID"] != null ? child.Attributes["NMAlarmID"].InnerText : "";
                            device.Values.Add(tThreshold);
                        }
                    }

                    this.DeviceList.Add(device);
                }
            }
        }

        public string FsuId { get; set; }

        public EnmResult Result { get; set; }

        public string FailureCause { get; set; }

        public List<GetThresholdAckDevice> DeviceList { get; set; }
    }

    public partial class GetThresholdAckDevice {
        public string Id { get; set; }

        public List<TThreshold> Values { get; set; }
    }
}