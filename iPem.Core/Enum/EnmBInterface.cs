using System;

namespace iPem.Core.Enum {
    /// <summary>
    /// 报文返回结果
    /// </summary>
    /// <remarks>
    /// 失败（FAILURE）
    /// 成功（SUCCESS）
    /// </remarks>
    public enum EnmBIResult {
        FAILURE = 0,
        SUCCESS = 1
    }

    /// <summary>
    /// 信号值的状态
    /// </summary>
    /// <remarks>
    /// 正常数据（NOALARM）
    /// 无效数据（INVALID）
    /// </remarks>
    public enum EnmBIState {
        NOALARM = 0,
        INVALID = 1
    }

    /// <summary>
    /// 告警等级
    /// </summary>
    /// <remarks>
    /// 一级告警（CRITICAL）
    /// 二级告警（MAJOR）
    /// 三级告警（MINOR）
    /// 四级告警（HINT）
    /// </remarks>
    public enum EnmBILevel {
        CRITICAL = 1,
        MAJOR = 2,
        MINOR = 3,
        HINT = 4
    }

    /// <summary>
    /// 告警标志
    /// </summary>
    /// <remarks>
    /// 告警开始（BEGIN）
    /// 告警结束（END）
    /// </remarks>
    public enum EnmBIFlag {
        BEGIN,
        END
    }

    /// <summary>
    /// 报文类型
    /// </summary>
    /// <remarks>
    /// 注册(LOGIN)
    /// 注册响应(LOGIN_ACK)
    /// 实时告警发送(SEND_ALARM)
    /// 实时告警发送确认(SEND_ALARM_ACK)
    /// 监控点数据请求(GET_DATA)
    /// 请求监控点数据响应(GET_DATA_ACK)
    /// 写监控点设置值请求(SET_POINT)
    /// 写监控点设置值响应(SET_POINT_ACK)
    /// 监控点门限数据请求(GET_THRESHOLD)
    /// 请求监控点门限数据响应(GET_THRESHOLD_ACK)
    /// 写监控点门限数据请求(SET_THRESHOLD)
    /// 写监控点门限数据响应(SET_THRESHOLD_ACK)
    /// 获取FSU注册信息请求(GET_LOGININFO)
    /// 获取FSU注册信息响应(GET_LOGININFO_ACK)
    /// 设置FSU注册信息请求(SET_LOGININFO)
    /// 设置FSU注册信息响应(SET_LOGININFO_ACK)
    /// 获取FSU的FTP信息请求(GET_FTP)
    /// 获取FSU的FTP信息响应(GET_FTP_ACK)
    /// 设置FSU的FTP信息请求(SET_FTP)
    /// 设置FSU的FTP信息响应(SET_FTP_ACK)
    /// 时间同步请求(TIME_CHECK)
    /// 时间同步响应(TIME_CHECK_ACK)
    /// 获取FSU的状态参数请求(GET_FSUINFO)
    /// 获取FSU的状态参数响应(GET_FSUINFO_ACK)
    /// 更新FSU状态信息获取周期请求(UPDATE_FSUINFO_INTERVAL)
    /// 更新FSU状态信息获取周期响应(UPDATE_FSUINFO_INTERVAL_ACK)
    /// 重启FSU请求(SET_FSUREBOOT)
    /// 重启FSU响应(SET_FSUREBOOT_ACK)
    /// 监控点存储规则查询请求(GET_STORAGERULE)
    /// 监控点存储规则查询响应(GET_STORAGERULE_ACK)
    /// 动环配置数据请求(GET_DEV_CONF)
    /// 动环配置数据确认(GET_DEV_CONF_ACK)
    /// 上报动环设备配置变更数据请求(SEND_DEV_CONF_DATA)
    /// 上报动环设备配置变更数据响应(SEND_DEV_CONF_DATA_ACK)
    /// 写动环设备配置数据请求(SET_DEV_CONF_DATA)
    /// 写动环设备配置数据响应(SET_DEV_CONF_DATA_ACK)
    /// 写监控点存储规则请求(SET_STORAGERULE)
    /// 写监控点存储规则响应(SET_STORAGERULE_ACK
    /// </remarks>
    public enum EnmBIPackType {
        LOGIN,
        LOGIN_ACK,
        SEND_ALARM,
        SEND_ALARM_ACK,
        GET_DATA,
        GET_DATA_ACK,
        SET_POINT,
        SET_POINT_ACK,
        GET_THRESHOLD,
        GET_THRESHOLD_ACK,
        SET_THRESHOLD,
        SET_THRESHOLD_ACK,
        GET_LOGININFO,
        GET_LOGININFO_ACK,
        SET_LOGININFO,
        SET_LOGININFO_ACK,
        GET_FTP,
        GET_FTP_ACK,
        SET_FTP,
        SET_FTP_ACK,
        TIME_CHECK,
        TIME_CHECK_ACK,
        GET_FSUINFO,
        GET_FSUINFO_ACK,
        UPDATE_FSUINFO_INTERVAL,
        UPDATE_FSUINFO_INTERVAL_ACK,
        SET_FSUREBOOT,
        SET_FSUREBOOT_ACK,
        GET_STORAGERULE,
        GET_STORAGERULE_ACK,
        GET_DEV_CONF,
        GET_DEV_CONF_ACK,
        SEND_DEV_CONF_DATA,
        SEND_DEV_CONF_DATA_ACK,
        SET_DEV_CONF_DATA,
        SET_DEV_CONF_DATA_ACK,
        SET_STORAGERULE,
        SET_STORAGERULE_ACK
    }

    /// <summary>
    /// Represents the point enumeration
    /// </summary>
    /// <remarks>
    /// 4-遥信信号（DI）
    /// 3-遥测信号（AI）
    /// 1-遥控信号（DO）
    /// 2-遥调信号（AO）
    /// 0-告警信号（AL）
    /// </remarks>
    public enum EnmBIPoint {
        DI = 4,
        AI = 3,
        DO = 1,
        AO = 2,
        AL = 0
    }
}
