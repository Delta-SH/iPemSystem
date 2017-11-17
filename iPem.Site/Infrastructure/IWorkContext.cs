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
        Boolean IsAuthenticated();

        Guid Identifier();

        U_Role Role();

        U_User User();

        U_Employee Employee();

        iProfile Profile();

        iAuth Authorizations();

        DateTime GetLastNoticeTime();

        void SetLastNoticeTime(DateTime value);

        DateTime GetLastSpeechTime();

        void SetLastSpeechTime(DateTime value);

        DateTime LastLoginTime();

        WsValues WsValues();

        TsValues TsValues();

        RtValues RtValues();

        List<C_LogicType> LogicTypes();

        List<C_SubLogicType> SubLogicTypes();

        List<C_DeviceType> DeviceTypes();

        List<C_SubDeviceType> SubDeviceTypes();

        List<C_RoomType> RoomTypes();

        List<C_StationType> StationTypes();

        List<C_EnumMethod> AreaTypes();

        List<C_SCVendor> Vendors();

        List<C_Department> Departments();

        List<SSHArea> AllAreas();

        List<SSHStation> AllStations();

        List<SSHRoom> AllRooms();

        List<SSHFsu> AllFsus();

        List<SSHDevice> AllDevices();

        List<SSHCamera> AllCameras();

        List<SSHArea> Areas();

        List<SSHStation> Stations();

        List<SSHRoom> Rooms();

        List<SSHFsu> Fsus();

        List<SSHDevice> Devices();

        List<SSHCamera> Cameras();

        HashSet<string> DeviceKeys();

        List<C_Group> Groups();

        List<P_Point> Points();

        List<P_SubPoint> SubPoints();

        List<P_Point> AL();

        List<AlmStore<A_AAlarm>> ActAlarms();

        List<AlmStore<A_AAlarm>> AllAlarms();

        void ResetRole();

        void ResetUser();

        void ResetEmployee();

        void ResetProfile();

        List<iSSHDevice> iDevices(DateTime date);

        List<iSSHStation> iStations(DateTime date);

        List<iSSHArea> iAreas(DateTime date);

        List<P_Point> GetPoints(bool _ai, bool _ao, bool _di, bool _do, bool _al);

        EnmPoint GetPointType(P_Point point);

        List<S_Station> GetStationsWithPoints(IList<string> points);

        List<AlmStore<A_AAlarm>> AlarmsToStore(List<A_AAlarm> alarms);

        List<AlmStore<A_HAlarm>> AlarmsToStore(List<A_HAlarm> alarms);

        Dictionary<string, AlmStore<A_AAlarm>> AlarmsToDictionary(List<AlmStore<A_AAlarm>> alarms, bool primaryKey = true);

        Dictionary<string, AlmStore<A_HAlarm>> AlarmsToDictionary(List<AlmStore<A_HAlarm>> alarms, bool primaryKey = true);
    }
}
