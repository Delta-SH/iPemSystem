using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 信号参数API
    /// </summary>
    public partial interface ISignalService {
        /// <summary>
        /// 获得设置了绝对阈值的信号
        /// </summary>
        List<D_Signal> GetAbsThresholds();

        /// <summary>
        /// 获得设置了百分比阈值的信号
        /// </summary>
        List<D_Signal> GetPerThresholds();

        /// <summary>
        /// 获得设置了存储周期的信号
        /// </summary>
        List<D_Signal> GetSavedPeriods();

        /// <summary>
        /// 获得设置了存储参考时间的信号
        /// </summary>
        List<D_Signal> GetStorageRefTimes();

        /// <summary>
        /// 获得设置了告警门限值的信号
        /// </summary>
        List<D_Signal> GetAlarmLimits();

        /// <summary>
        /// 获得设置了告警等级的信号
        /// </summary>
        List<D_Signal> GetAlarmLevels();

        /// <summary>
        /// 获得设置了告警延迟的信号
        /// </summary>
        List<D_Signal> GetAlarmDelays();

        /// <summary>
        /// 获得设置了告警恢复延迟的信号
        /// </summary>
        List<D_Signal> GetAlarmRecoveryDelays();

        /// <summary>
        /// 获得设置了告警过滤的信号
        /// </summary>
        List<D_Signal> GetAlarmFilterings();

        /// <summary>
        /// 获得设置了主次告警的信号
        /// </summary>
        List<D_Signal> GetAlarmInferiors();

        /// <summary>
        /// 获得设置了关联告警的信号
        /// </summary>
        List<D_Signal> GetAlarmConnections();

        /// <summary>
        /// 获得设置了翻转告警的信号
        /// </summary>
        List<D_Signal> GetAlarmReversals();

        /// <summary>
        /// 获得简单信号对象
        /// </summary>
        D_SimpleSignal GetSimpleSignal(string device, string point);

        /// <summary>
        /// 获得简单信号对象
        /// </summary>
        List<D_SimpleSignal> GetSimpleSignals(IEnumerable<Kv<string, string>> keys);

        /// <summary>
        /// 获得指定设备的简单信号对象
        /// </summary>
        List<D_SimpleSignal> GetSimpleSignalsInDevice(string device);

        /// <summary>
        /// 获得指定设备的简单信号对象
        /// </summary>
        List<D_SimpleSignal> GetSimpleSignalsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do, bool _al);

        /// <summary>
        /// 获得指定设备的简单信号对象
        /// </summary>
        List<D_SimpleSignal> GetSimpleSignalsInDevices(IEnumerable<string> devices);

        /// <summary>
        /// 获得指定设备的简单信号对象
        /// </summary>
        List<D_SimpleSignal> GetSimpleSignalsInDevices(IEnumerable<string> devices, bool _ai, bool _ao, bool _di, bool _do, bool _al);

        /// <summary>
        /// 获得指定的虚拟信号
        /// </summary>
        D_VSignal GetVSignal(string device, string point);

        /// <summary>
        /// 获得所有的虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals();

        /// <summary>
        /// 获得虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals(IEnumerable<Kv<string, string>> pairs);

        /// <summary>
        /// 获得指定设备的虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals(string device);

        /// <summary>
        /// 获得指定设备的虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals(string device, bool _ai, bool _ao, bool _di, bool _do, bool _al);

        /// <summary>
        /// 获得指定设备，信号名称的虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals(string device, string[] names);

        /// <summary>
        /// 获得指定分类的虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals(string device, EnmVSignalCategory category);

        /// <summary>
        /// 获得指定分类的虚拟信号
        /// </summary>
        List<D_VSignal> GetVSignals(EnmVSignalCategory category);

        /// <summary>
        /// 添加虚拟信号
        /// </summary>
        void AddVSignals(params D_VSignal[] entities);

        /// <summary>
        /// 更新虚拟信号
        /// </summary>
        void UpdateVSignals(params D_VSignal[] entities);

        /// <summary>
        /// 删除虚拟信号
        /// </summary>
        void RemoveVSignals(params D_VSignal[] entities);

        /// <summary>
        /// 获得指定设备的所有信号(包括真实信号和虚拟信号)
        /// </summary>
        List<D_SimpleSignal> GetAllSignals(string device);

        /// <summary>
        /// 获得指定设备的信号(包括真实信号和虚拟信号)
        /// </summary>
        List<D_SimpleSignal> GetAllSignals(string device, bool _ai, bool _ao, bool _di, bool _do, bool _al);

        /// <summary>
        /// 获得指定设备的信号(包括真实信号和虚拟信号)
        /// </summary>
        List<D_SimpleSignal> GetAllSignals(IEnumerable<Kv<string, string>> pairs);
    }
}