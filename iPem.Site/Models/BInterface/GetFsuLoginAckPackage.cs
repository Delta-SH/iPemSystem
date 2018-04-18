using iPem.Core.Enum;
using System;
using System.Xml;

namespace iPem.Site.Models.BInterface {
    public partial class GetFsuLoginAckPackage {
        public GetFsuLoginAckPackage(string xmlData) {
            this.Result = EnmBIResult.FAILURE;
            this.Origin = xmlData;

            if(string.IsNullOrWhiteSpace(xmlData))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var Name = xmlDoc.SelectSingleNode("/Response/PK_Type/Name");
            if(Name == null) return;

            if(!Name.InnerText.Equals(EnmBIPackType.GET_LOGININFO_ACK.ToString())) 
                return;

            var UserName = xmlDoc.SelectSingleNode("/Response/Info/UserName");
            if (UserName != null) this.Username = UserName.InnerText;

            var PassWord = xmlDoc.SelectSingleNode("/Response/Info/PassWord");
            if (PassWord != null) this.Password = PassWord.InnerText;

            var FSUID = xmlDoc.SelectSingleNode("/Response/Info/FSUID");
            if (FSUID != null) this.FsuId = FSUID.InnerText;

            var FSUIP = xmlDoc.SelectSingleNode("/Response/Info/FSUIP");
            if (FSUIP != null) this.FsuIp = FSUIP.InnerText;

            var FSUMAC = xmlDoc.SelectSingleNode("/Response/Info/FSUMAC");
            if (FSUMAC != null) this.FsuMac = FSUMAC.InnerText;

            var FSUVER = xmlDoc.SelectSingleNode("/Response/Info/FSUVER");
            if (FSUVER != null) this.FsuVer = FSUVER.InnerText;

            var SiteID = xmlDoc.SelectSingleNode("/Response/Info/SiteID");
            if (SiteID != null) this.SiteId = SiteID.InnerText;

            var SiteName = xmlDoc.SelectSingleNode("/Response/Info/SiteName");
            if (SiteName != null) this.SiteName = SiteName.InnerText;

            var RoomID = xmlDoc.SelectSingleNode("/Response/Info/RoomID");
            if (RoomID != null) this.RoomId = RoomID.InnerText;

            var RoomName = xmlDoc.SelectSingleNode("/Response/Info/RoomName");
            if (RoomName != null) this.RoomName = RoomName.InnerText;

            var Result = xmlDoc.SelectSingleNode("/Response/Info/Result");
            if(Result != null) {
                var result = int.Parse(Result.InnerText);
                this.Result = Enum.IsDefined(typeof(EnmBIResult), result) ? (EnmBIResult)result : EnmBIResult.FAILURE;
            }

            var FailureCause = xmlDoc.SelectSingleNode("/Response/Info/FailureCause");
            if(FailureCause != null) this.FailureCause = FailureCause.InnerText;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FsuId { get; set; }

        public string FsuIp { get; set; }

        public string FsuMac { get; set; }

        public string FsuVer { get; set; }

        public string SiteId { get; set; }

        public string SiteName { get; set; }

        public string RoomId { get; set; }

        public string RoomName { get; set; }

        public EnmBIResult Result { get; set; }

        public string FailureCause { get; set; }

        public string Origin { get; set; }
    }
}