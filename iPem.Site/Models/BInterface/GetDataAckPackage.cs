using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetDataAckPackage {
        public GetDataAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.GET_DATA_ACK.ToString()))
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
                this.DeviceList = new List<GetDataAckDevice>();
                foreach(XmlNode node in DeviceList) {
                    var device = new GetDataAckDevice();
                    device.Id = node.Attributes["ID"].InnerText;
                    device.Values = new List<TSemaphore>();

                    if(node.HasChildNodes) {
                        foreach(XmlNode child in node.ChildNodes) {
                            var tSemaphore = new TSemaphore();
                            var type = child.Attributes["Type"] != null ? int.Parse(child.Attributes["Type"].InnerText) : (int)EnmBIPoint.AL;
                            var status = child.Attributes["Status"] != null ? int.Parse(child.Attributes["Status"].InnerText) : (int)EnmState.Invalid;

                            tSemaphore.Id = child.Attributes["ID"].InnerText;
                            tSemaphore.SignalNumber = child.Attributes["SignalNumber"].InnerText;
                            tSemaphore.Type = Enum.IsDefined(typeof(EnmBIPoint), type) ? (EnmBIPoint)type : EnmBIPoint.AL;
                            tSemaphore.MeasuredVal = child.Attributes["MeasuredVal"] != null ? child.Attributes["MeasuredVal"].InnerText : "NULL";
                            tSemaphore.SetupVal = child.Attributes["SetupVal"] != null ? child.Attributes["SetupVal"].InnerText : "NULL";
                            tSemaphore.Status = Enum.IsDefined(typeof(EnmBIState), status) ? (EnmBIState)status : EnmBIState.INVALID;
                            tSemaphore.Time = DateTime.Parse(child.Attributes["Time"].InnerText);
                            device.Values.Add(tSemaphore);
                        }
                    }

                    this.DeviceList.Add(device);
                }
            }
        }

        public string FsuId { get; set; }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public List<GetDataAckDevice> DeviceList { get; set; }
    }

    public partial class GetDataAckDevice {
        public string Id { get; set; }

        public List<TSemaphore> Values { get; set; }
    }
}