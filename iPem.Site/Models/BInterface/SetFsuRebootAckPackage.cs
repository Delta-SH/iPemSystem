using iPem.Core.Enum;
using System;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class SetFsuRebootAckPackage {
        public SetFsuRebootAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.SET_FSUREBOOT_ACK.ToString())) 
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
        }

        public string FsuId { get; set; }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }
    }
}