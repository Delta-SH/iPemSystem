using iPem.Core;
using iPem.Core.Domain.Rs;
using System;

namespace iPem.Site.Models.SSH {
    public abstract class SSHSystem {
        public static readonly A_Area Area = new A_Area {
            Id = "-1",
            Code = "-1",
            Name = "系统区域",
            Type = new IdValuePair<int, string>() { Id = -1, Value = "系统区域" },
            ParentId = "0",
            Enabled = true
        };

        public static readonly S_Station Station = new S_Station {
            Id = "-1",
            Code = "-1",
            Name = "系统站点",
            Type = new C_StationType { Id = "-1", Name = "系统站点" },
            AreaId = Area.Id,
            Enabled = true
        };

        public static readonly S_Room Room = new S_Room {
            Id = "-1",
            Code = "-1",
            Name = "系统机房",
            Type = new C_RoomType { Id = "-1", Name = "系统设备" },
            AreaId = Area.Id,
            StationId = Station.Id,
            StationName = Station.Name,
            Enabled = true
        };

        public static readonly D_Fsu Fsu = new D_Fsu {
            Id = "-1",
            Code = "-1",
            Name = "系统FSU",
            Type = new C_DeviceType { Id = "-1", Name = "系统FSU" },
            SubType = new C_SubDeviceType { Id = "-1", Name = "系统FSU" },
            SubLogicType = new C_SubLogicType { Id = "-1", Name = "系统FSU" },
            AreaId = Area.Id,
            StationId = Station.Id,
            StationName = Station.Name,
            StaTypeId = Station.Type.Id,
            RoomId = Room.Id,
            RoomName = Room.Name,
            VendorId = "-1",
            VendorName = "系统厂家",
            Enabled = true
        };

        public static D_Device SC(string id, string name) {
            return new D_Device {
                Id = id,
                Code = id,
                Name = name,
                Type = new C_DeviceType { Id = "-1", Name = "SC采集设备" },
                SubType = new C_SubDeviceType { Id = "-1", Name = "SC采集设备" },
                SubLogicType = new C_SubLogicType { Id = "-1", Name = "SC采集设备" },
                AreaId = Area.Id,
                StationId = Station.Id,
                StationName = Station.Name,
                StaTypeId = Station.Type.Id,
                RoomId = Room.Id,
                RoomName = Room.Name,
                FsuId = Fsu.Id,
                FsuCode = Fsu.Code,
                Enabled = true
            };
        }
    }
}