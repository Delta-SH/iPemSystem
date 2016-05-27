using iPem.Core;
using iPem.Site.Models;
using iPem.Site.Models.BI;
using System;
using System.Collections.Generic;

namespace iPem.Site.Infrastructure {
    public abstract class BIPack {
        public static ActDataAckTemplate RequestActiveData(WsValues value, ActDataTemplate template) {
            if(value == null) return null;
            return RequestActiveData(GetWsDataUrl(value), template);
        }

        public static ActDataAckTemplate RequestActiveData(string url, ActDataTemplate template) {
            var service = new FSUServiceService();
            service.Url = url;
            var xmlData = service.invoke(template.ToXml());
            return new ActDataAckTemplate(xmlData);
        }

        public static SetDataAckTemplate SetPointData(WsValues value, SetDataTemplate template) {
            if(value == null) return null;
            return SetPointData(GetWsDataUrl(value), template);
        }

        public static SetDataAckTemplate SetPointData(string url, SetDataTemplate template) {
            var service = new FSUServiceService();
            service.Url = url;
            var xmlData = service.invoke(template.ToXml());
            return new SetDataAckTemplate(xmlData);
        }

        public static string GetWsDataUrl(WsValues value) {
            var uri = new UriBuilder("http", value.ip, value.port, value.data);
            return uri.ToString();
        }

        public static string GetWsOrderUrl(WsValues value) {
            var uri = new UriBuilder("http", value.ip, value.port, value.order);
            return uri.ToString();
        }
    }
}