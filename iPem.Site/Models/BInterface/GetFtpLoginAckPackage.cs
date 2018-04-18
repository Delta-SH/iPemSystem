using iPem.Core.Enum;
using System;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetFtpLoginAckPackage {
        public GetFtpLoginAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;
            this.Origin = xmlData;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.GET_FTP_ACK.ToString())) 
                return;

            var UserName = xmlDoc.SelectSingleNode("/Response/Info/UserName");
            if (UserName != null) this.Username = UserName.InnerText;

            var PassWord = xmlDoc.SelectSingleNode("/Response/Info/PassWord");
            if (PassWord != null) this.Password = PassWord.InnerText;

            var FSUID = xmlDoc.SelectSingleNode("/Response/Info/FSUID");
            if (FSUID != null) this.FsuId = FSUID.InnerText;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmBIResult), result) ? (EnmBIResult)result : EnmBIResult.FAILURE;
            }

            var FailureCause = xmlDoc.SelectSingleNode("/Response/Info/FailureCause");
            if(FailureCause != null) this.FailureCause = FailureCause.InnerText;
        }

        public string FsuId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public string Origin { get; set; }
    }
}