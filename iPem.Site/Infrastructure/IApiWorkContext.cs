using iPem.Core.Domain.Common;
using iPem.Core.Domain.Cs;
using iPem.Core.Domain.Rs;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Site.Models;
using iPem.Site.Models.SSH;
using System.Collections.Generic;

namespace iPem.Site.Infrastructure {
    public interface IApiWorkContext {
        /// <summary>
        /// 获得指定的角色对象
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>角色对象</returns>
        U_Role GetRole(string id);

        /// <summary>
        /// 获得指定的用户对象
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns>用户对象</returns>
        U_User GetUser(string name);

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>验证结果</returns>
        EnmLoginResults Login(string name, string password);

        /// <summary>
        /// 获得角色的权限对象
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>权限对象</returns>
        U_EntitiesInRole GetAuth(string id);

        /// <summary>
        /// 获得站点类型集合
        /// </summary>
        /// <returns>站点类型集合</returns>
        List<C_StationType> GetStationTypes();

        /// <summary>
        /// 获得机房类型集合
        /// </summary>
        /// <returns>机房类型集合</returns>
        List<C_RoomType> GetRoomTypes();

        /// <summary>
        /// 获得设备类型集合
        /// </summary>
        /// <returns>设备类型集合</returns>
        List<C_DeviceType> GetDeviceTypes();

        /// <summary>
        /// 获得设备子类型集合
        /// </summary>
        /// <returns>设备子类型集合</returns>
        List<C_SubDeviceType> GetSubDeviceTypes();

        /// <summary>
        /// 获得逻辑分类集合
        /// </summary>
        /// <returns>逻辑分类集合</returns>
        List<C_LogicType> GetLogicTypes();

        /// <summary>
        /// 获得逻辑子分类集合
        /// </summary>
        /// <returns>逻辑子分类集合</returns>
        List<C_SubLogicType> GetSubLogicTypes();

        /// <summary>
        /// 获得动环厂家集合
        /// </summary>
        /// <returns>动环厂家集合</returns>
        List<C_SCVendor> GetVendors();

        /// <summary>
        /// 获得区域集合
        /// </summary>
        /// <returns>区域集合</returns>
        List<SSHArea> GetAllAreas();

        /// <summary>
        /// 获得指定角色下的区域集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>区域集合</returns>
        List<SSHArea> GetAreas(string id);

        /// <summary>
        /// 获得站点集合
        /// </summary>
        /// <returns>站点集合</returns>
        List<S_Station> GetAllStations();

        /// <summary>
        /// 获得指定角色下的站点集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>站点集合</returns>
        List<S_Station> GetStations(string id);

        /// <summary>
        /// 获得机房集合
        /// </summary>
        /// <returns>机房集合</returns>
        List<S_Room> GetAllRooms();

        /// <summary>
        /// 获得指定角色下的机房集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>机房集合</returns>
        List<S_Room> GetRooms(string id);

        /// <summary>
        /// 获得FSU集合
        /// </summary>
        /// <returns>FSU集合</returns>
        List<D_Fsu> GetAllFsus();

        /// <summary>
        /// 获得指定角色下的FSU集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>FSU集合</returns>
        List<D_Fsu> GetFsus(string id);

        /// <summary>
        /// 获得设备集合
        /// </summary>
        /// <returns>设备集合</returns>
        List<D_Device> GetAllDevices();

        /// <summary>
        /// 获得指定角色下的设备集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>设备集合</returns>
        List<D_Device> GetDevices(string id);

        /// <summary>
        /// 获得指定角色下的设备HASH
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>设备HASH</returns>
        HashSet<string> GetDeviceKeys(string id);

        /// <summary>
        /// 获得信号集合
        /// </summary>
        /// <returns>信号集合</returns>
        List<P_Point> GetPoints();

        /// <summary>
        /// 获得子信号集合
        /// </summary>
        /// <returns>子信号集合</returns>
        List<P_SubPoint> GetSubPoints();

        /// <summary>
        /// 获得告警信号集合
        /// </summary>
        /// <returns>告警信号集合</returns>
        List<P_Point> GetALPoints();

        /// <summary>
        /// 判断信号是否为告警信号
        /// </summary>
        /// <param name="point">信号对象</param>
        /// <returns>true/false</returns>
        EnmPoint GetPointType(P_Point point);

        /// <summary>
        /// 获得SC采集组集合
        /// </summary>
        /// <returns>SC采集组集合</returns>
        List<C_Group> GetGroups();

        /// <summary>
        /// 获得指定角色下的实时告警集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>实时告警集合</returns>
        List<AlmStore<A_AAlarm>> ActAlarms(string id);

        /// <summary>
        /// 获得实时告警集合
        /// </summary>
        /// <returns>实时告警集合</returns>
        List<AlmStore<A_AAlarm>> AllAlarms();

        /// <summary>
        /// 计算关联指定角色下的实时告警集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <param name="alarms">实时告警集合</param>
        /// <returns>实时告警集合</returns>
        List<AlmStore<A_AAlarm>> AlarmsToStore(string id, List<A_AAlarm> alarms);

        /// <summary>
        /// 计算关联指定角色下的历史告警集合
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <param name="alarms">历史告警集合</param>
        /// <returns>历史告警集合</returns>
        List<AlmStore<A_HAlarm>> AlarmsToStore(string id, List<A_HAlarm> alarms);

        /// <summary>
        /// 判断信号点是否为告警信号
        /// </summary>
        /// <param name="point">信号对象</param>
        /// <returns>是否为告警信号</returns>
        bool IsAlarmPoint(P_Point point);
    }
}
