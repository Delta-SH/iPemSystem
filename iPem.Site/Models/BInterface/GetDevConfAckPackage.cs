using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetDevConfAckPackage {
        public GetDevConfAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.GET_DEV_CONF_ACK.ToString())) 
                return;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmBIResult), result) ? (EnmBIResult)result : EnmBIResult.FAILURE;
            }

            var FailureCause = xmlDoc.SelectSingleNode("/Response/Info/FailureCause");
            if(FailureCause != null) this.FailureCause = FailureCause.InnerText;

            var Devices = xmlDoc.SelectNodes("/Response/Info/Values/Device");
            if (Devices != null && Devices.Count > 0) {
                this.Devices = new List<TDevConf>();
                foreach (XmlNode node in Devices) {
                    var device = new TDevConf();
                    device.DeviceId = node.Attributes["DeviceID"].InnerText;
                    device.DeviceName = node.Attributes["DeviceName"].InnerText;
                    device.SiteId = node.Attributes["SiteID"].InnerText;
                    device.SiteName = node.Attributes["SiteName"].InnerText;
                    device.RoomId = node.Attributes["RoomID"].InnerText;
                    device.RoomName = node.Attributes["RoomName"].InnerText;
                    device.DeviceType = node.Attributes["DeviceType"].InnerText;
                    device.DeviceSubType = node.Attributes["DeviceSubType"].InnerText;
                    device.Model = node.Attributes["Model"].InnerText;
                    device.Brand = node.Attributes["Brand"].InnerText;
                    device.RatedCapacity = node.Attributes["RatedCapacity"].InnerText;
                    device.Version = node.Attributes["Version"].InnerText;
                    device.BeginRunTime = node.Attributes["BeginRunTime"].InnerText;
                    device.DevDescribe = node.Attributes["DevDescribe"].InnerText;
                    device.ConfRemark = node.Attributes["ConfRemark"].InnerText;
                    device.Signals = new List<TSignal>();

                    var Signals = node.SelectNodes("Signals/Signal");
                    if (Signals != null && Signals.Count > 0) {
                        foreach (XmlNode child in Signals) {
                            var tSignal = new TSignal();
                            var type = child.Attributes["Type"] != null ? int.Parse(child.Attributes["Type"].InnerText) : (int)EnmBIPoint.AL;
                            var level = child.Attributes["AlarmLevel"] != null ? int.Parse(child.Attributes["AlarmLevel"].InnerText) : (int)EnmBILevel.HINT;

                            tSignal.TSignalId = new TSignalMeasurementId { Id = child.Attributes["ID"].InnerText, SignalNumber = child.Attributes["SignalNumber"].InnerText };
                            tSignal.Type = Enum.IsDefined(typeof(EnmBIPoint), type) ? (EnmBIPoint)type : EnmBIPoint.AL;
                            tSignal.SignalName = child.Attributes["SignalName"].InnerText;
                            tSignal.AlarmLevel = Enum.IsDefined(typeof(EnmBILevel), level) ? (EnmBILevel)level : EnmBILevel.HINT;
                            tSignal.Threshold = child.Attributes["Threshold"].InnerText;
                            tSignal.NMAlarmID = child.Attributes["NMAlarmID"].InnerText;
                            device.Signals.Add(tSignal);
                        }
                    }

                    this.Devices.Add(device);
                }
            }
        }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public List<TDevConf> Devices { get; set; }
    }
}