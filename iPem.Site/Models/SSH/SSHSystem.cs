using iPem.Core;
using iPem.Core.Domain.Rs;
using System;

namespace iPem.Site.Models.SSH {
    public abstract class SSHSystem {
        public static readonly A_Area Area = new A_Area {
            Id = "-1",
            Code = "-1",
            Name = "--",
            Type = new Kv<int, string> { Key = -1, Value = "系统区域" },
            ParentId = "0",
            Enabled = true
        };

        public static readonly S_Station Station = new S_Station {
            Id = "-1",
            Code = "-1",
            Name = "--",
            Type = new C_StationType { Id = "-1", Name = "系统站点" },
            AreaId = Area.Id,
            Enabled = true
        };

        public static readonly S_Room Room = new S_Room {
            Id = "-1",
            Code = "-1",
            Name = "--",
            Type = new C_RoomType { Id = "-1", Name = "系统机房" },
            AreaId = Area.Id,
            StationId = Station.Id,
            StationName = Station.Name,
            Enabled = true
        };

        public static readonly D_Fsu Fsu = new D_Fsu {
            Id = "-1",
            Code = "-1",
            Name = "--",
            AreaId = Area.Id,
            StationId = Station.Id,
            StationName = Station.Name,
            StaTypeId = Station.Type.Id,
            RoomId = Room.Id,
            RoomName = Room.Name,
            VendorId = "-1",
            VendorName = "系统厂家"
        };

        public static readonly D_Device SC = new D_Device {
            Id = "-1",
            Code = "-1",
            Name = "SC通信采集前置机",
            Type = new C_DeviceType { Id = "-1", Name = "SC通信采集设备" },
            SubType = new C_SubDeviceType { Id = "-1", Name = "SC通信采集设备" },
            SubLogicType = new C_SubLogicType { Id = "-1", Name = "SC通信采集设备" },
            AreaId = Area.Id,
            StationId = Station.Id,
            StationName = Station.Name,
            StaTypeId = Station.Type.Id,
            RoomId = Room.Id,
            RoomName = Room.Name,
            FsuId = Fsu.Id,
            Enabled = true
        };

        public static readonly D_Device FSU = new D_Device {
            Id = "-1",
            Code = "-1",
            Name = "FSU监控采集设备",
            Type = new C_DeviceType { Id = "-2", Name = "FSU监控采集设备" },
            SubType = new C_SubDeviceType { Id = "-2", Name = "FSU监控采集设备" },
            SubLogicType = new C_SubLogicType { Id = "-2", Name = "FSU监控采集设备" },
            AreaId = Area.Id,
            StationId = Station.Id,
            StationName = Station.Name,
            StaTypeId = Station.Type.Id,
            RoomId = Room.Id,
            RoomName = Room.Name,
            FsuId = Fsu.Id,
            Enabled = true
        };
    }
}