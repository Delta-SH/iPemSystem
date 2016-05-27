using System;

namespace iPem.Core.Enum {

    public enum EnmBIResult {
        FAILURE = 0,
        SUCCESS
    }

    public enum EnmBIPackCode {
        FORMAT_ERROR = 0,
        LOGIN = 101,
        LOGIN_ACK = 102,
        LOGOUT = 103,
        LOGOUT_ACK = 104,
        GET_DATA = 401,
        GET_DATA_ACK = 402,
        GET_HISDATA = 403,
        GET_HISDATA_ACK = 404,
        SEND_ALARM = 501,
        SEND_ALARM_ACK = 502,
        SET_POINT = 1001,
        SET_POINT_ACK = 1002,
        TIME_CHECK = 1301,
        TIME_CHECK_ACK = 1302,
        GET_GPS = 1401,
        GET_GPS_ACK = 1402,
        SET_GPS = 1403,
        SET_GPS_ACK = 1404,
        GET_LOGININFO = 1501,
        GET_LOGININFO_ACK = 1502,
        SET_LOGININFO = 1503,
        SET_LOGININFO_ACK = 1504,
        GET_FTP = 1601,
        GET_FTP_ACK = 1602,
        SET_FTP = 1603,
        SET_FTP_ACK = 1604,
        GET_FSUINFO = 1701,
        GET_FSUINFO_ACK = 1702,
        SET_FSUREBOOT = 1801,
        SET_FSUREBOOT_ACK = 1802,
        GET_THRESHOLD = 1901,
        GET_THRESHOLD_ACK = 1902,
        SET_THRESHOLD = 2001,
        SET_THRESHOLD_ACK = 2002
    }

    public enum EnmBIType {
        STATION = 0,
        DEVICE = 1,
        DI = 2,
        AI = 3,
        DO = 4,
        AO = 5,
        AREA = 9
    }
}
