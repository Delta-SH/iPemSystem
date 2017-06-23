using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using System;
using System.Collections.Generic;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public interface IWorkContext {
        Boolean IsAuthenticated { get; }

        Guid Identifier { get; }

        U_Role Role { get; }

        U_User User { get; }

        U_Employee Employee { get; }

        iProfile Profile { get; }

        U_EntitiesInRole Authorizations { get; }

        DateTime LastNoticeTime { get; set; }

        DateTime LastSpeechTime { get; set; }

        DateTime LastLoginTime { get; }

        WsValues WsValues { get; }

        TsValues TsValues { get; }

        RtValues RtValues { get; }

        List<C_LogicType> LogicTypes { get; }

        List<C_SubLogicType> SubLogicTypes { get; }

        List<C_DeviceType> DeviceTypes { get; }

        List<C_SubDeviceType> SubDeviceTypes { get; }

        List<C_RoomType> RoomTypes { get; }

        List<C_StationType> StationTypes { get; }

        List<C_EnumMethod> AreaTypes { get; }

        List<SSHProtocol> AllProtocols { get; }

        List<SSHDevice> AllDevices { get; }

        List<SSHFsu> AllFsus { get; }

        List<SSHRoom> AllRooms { get; }

        List<SSHStation> AllStations { get; }

        List<SSHArea> AllAreas { get; }

        List<SSHArea> Areas { get; }

        List<SSHStation> Stations { get; }

        List<SSHRoom> Rooms { get; }

        List<SSHFsu> Fsus { get; }

        List<SSHDevice> Devices { get; }

        List<P_Point> Points { get; }

        List<P_SubPoint> SubPoints { get; }

        List<P_Point> AI { get; }

        List<P_Point> AO { get; }

        List<P_Point> DI { get; }

        List<P_Point> DO { get; }

        List<P_Point> AL { get; }

        List<AlmStore<A_AAlarm>> ActAlarms { get; }

        void ResetRole();

        void ResetUser();

        void ResetEmployee();

        void ResetProfile();

        void ResetAuthorizations();

        List<AlmStore<A_AAlarm>> AlarmsToStore(List<A_AAlarm> alarms);

        List<AlmStore<A_HAlarm>> AlarmsToStore(List<A_HAlarm> alarms);

        Dictionary<string, AlmStore<A_AAlarm>> AlarmsToDictionary(List<AlmStore<A_AAlarm>> alarms, bool primaryKey = true);

        Dictionary<string, AlmStore<A_HAlarm>> AlarmsToDictionary(List<AlmStore<A_HAlarm>> alarms, bool primaryKey = true);
    }
}
