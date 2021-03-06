﻿using iPem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class SetStorageRuleAckPackage {
        public SetStorageRuleAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.SET_STORAGERULE_ACK.ToString())) 
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

            var DeviceList = xmlDoc.SelectNodes("/Response/Info/DeviceList/Device");
            if(DeviceList != null && DeviceList.Count > 0) {
                this.DeviceList = new List<SetStorageRuleAckDevice>();
                foreach(XmlNode node in DeviceList) {
                    var device = new SetStorageRuleAckDevice();
                    device.Id = node.Attributes["ID"].InnerText;
                    device.SuccessList = new List<TSignalMeasurementId>();
                    device.FailList = new List<TSignalMeasurementId>();

                    var success = node.SelectNodes("SuccessList/TSignalMeasurementId");
                    if(success != null && success.Count > 0) {
                        foreach(XmlNode s in success) {
                            device.SuccessList.Add(new TSignalMeasurementId {
                                Id = s.Attributes["ID"].InnerText,
                                SignalNumber = s.Attributes["SignalNumber"].InnerText
                            });
                        }
                    }

                    var failure = node.SelectNodes("FailList/TSignalMeasurementId");
                    if(failure != null && failure.Count > 0) {
                        foreach(XmlNode f in failure) {
                            device.FailList.Add(new TSignalMeasurementId {
                                Id = f.Attributes["ID"].InnerText,
                                SignalNumber = f.Attributes["SignalNumber"].InnerText
                            });
                        }
                    }

                    this.DeviceList.Add(device);
                }
            }
        }

        public string FsuId { get; set; }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public List<SetStorageRuleAckDevice> DeviceList { get; set; }
    }

    public partial class SetStorageRuleAckDevice {
        public string Id { get; set; }

        public List<TSignalMeasurementId> SuccessList { get; set; }

        public List<TSignalMeasurementId> FailList { get; set; }
    }
}