using System;

namespace iPem.Site.Models {
    public class RoleValues {
        /// <summary>
        /// 短信告警等级
        /// </summary>
        public int[] SMSLevels { get; set; }

        /// <summary>
        /// 短信告警设备
        /// </summary>
        public string SMSDevices { get; set; }

        /// <summary>
        /// 短信告警信号
        /// </summary>
        public string SMSSignals { get; set; }

        /// <summary>
        /// 语音告警等级
        /// </summary>
        public int[] voiceLevels { get; set; }

        /// <summary>
        /// 语音告警设备
        /// </summary>
        public string voiceDevices { get; set; }

        /// <summary>
        /// 语音告警信号
        /// </summary>
        public string voiceSignals { get; set; }
    }
}