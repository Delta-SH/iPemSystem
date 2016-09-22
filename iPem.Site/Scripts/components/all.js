/* ========================================================================
 * Components: AlarmLevelComponent.js
 * /Scripts/components/AlarmLevelComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmLevelMultiCombo",
    fieldLabel: '告警级别',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmLevelCombo",
    fieldLabel: '告警级别',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AlarmLevelComponent.js
 * /Scripts/components/AlarmLevelComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmLevelMultiCombo",
    fieldLabel: '告警级别',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmLevelCombo",
    fieldLabel: '告警级别',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AlarmLevelComponent.js
 * /Scripts/components/AlarmLevelComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmLevelMultiCombo",
    fieldLabel: '告警级别',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmLevelCombo",
    fieldLabel: '告警级别',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AlarmLevelComponent.js
 * /Scripts/components/AlarmLevelComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmLevelMultiCombo",
    fieldLabel: '告警级别',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmLevelCombo",
    fieldLabel: '告警级别',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AreaComponent.js
 * /Scripts/components/AreaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaMultiPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: AreaTypeComponent.js
 * /Scripts/components/AreaTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AreaTypeMultiCombo",
    fieldLabel: '区域类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AreaTypeCombo",
    fieldLabel: '区域类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: DeviceComponent.js
 * /Scripts/components/DeviceComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DeviceMultiPicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DevicePicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: DeviceTypeComponent.js
 * /Scripts/components/DeviceTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DeviceTypeMultiCombo",
    fieldLabel: '设备类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "DeviceTypeCombo",
    fieldLabel: '设备类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: EmployeeComponent.js
 * /Scripts/components/EmployeeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.EmployeeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeeMultiPicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.EmployeeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeePicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: ImageExporterComponent.js
 * /Scripts/components/ImageExporterComponent.js
 * ========================================================================
 */

Ext.define('Ext.ux.ImageExporter', {

    singleton: true,

    defaultUrl: '/Component/SaveCharts',

    formCls: Ext.baseCSSPrefix + 'hide-display',

    save: function (charts) {
        if (!Ext.isArray(charts))
            charts = [charts];

        var svgs = [];
        Ext.Array.each(charts, function (chart, index) {
            svgs.push(Ext.String.htmlEncode(Ext.draw.engine.SvgExporter.generate(chart.surface)));
        });

        $$iPems.download({
            url: this.defaultUrl,
            params: { svgs: svgs }
        });
    }
});

/* ========================================================================
 * Components: LogicTypeComponent.js
 * /Scripts/components/LogicTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "LogicTypeMultiCombo",
    fieldLabel: '逻辑分类',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "LogicTypeCombo",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointComponent.js
 * /Scripts/components/PointComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointMultiCombo",
    fieldLabel: '信号名称',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load();
    }
});

Ext.define("Ext.ux.PointComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointCombo",
    fieldLabel: '信号名称',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success) {
                    if (records.length > 0) {
                        me.select(records[0]);
                    }
                }
            }
        });
    }
});

Ext.define("Ext.ux.LogicPointMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointMultiPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicPointTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointTypeComponent.js
 * /Scripts/components/PointTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointTypeMultiCombo",
    fieldLabel: '信号类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.PointTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointTypeCombo",
    fieldLabel: '信号类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: RoomComponent.js
 * /Scripts/components/RoomComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomMultiPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: RoomTypeComponent.js
 * /Scripts/components/RoomTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "RoomTypeMultiCombo",
    fieldLabel: '机房类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "RoomTypeCombo",
    fieldLabel: '机房类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: StationComponent.js
 * /Scripts/components/StationComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationMultiPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: StationTypeComponent.js
 * /Scripts/components/StationTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "StationTypeMultiCombo",
    fieldLabel: '站点类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "StationTypeCombo",
    fieldLabel: '站点类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AreaComponent.js
 * /Scripts/components/AreaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaMultiPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: AreaTypeComponent.js
 * /Scripts/components/AreaTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AreaTypeMultiCombo",
    fieldLabel: '区域类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AreaTypeCombo",
    fieldLabel: '区域类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: DeviceComponent.js
 * /Scripts/components/DeviceComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DeviceMultiPicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DevicePicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: DeviceTypeComponent.js
 * /Scripts/components/DeviceTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DeviceTypeMultiCombo",
    fieldLabel: '设备类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "DeviceTypeCombo",
    fieldLabel: '设备类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: EmployeeComponent.js
 * /Scripts/components/EmployeeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.EmployeeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeeMultiPicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.EmployeeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeePicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: ImageExporterComponent.js
 * /Scripts/components/ImageExporterComponent.js
 * ========================================================================
 */

Ext.define('Ext.ux.ImageExporter', {

    singleton: true,

    defaultUrl: '/Component/SaveCharts',

    formCls: Ext.baseCSSPrefix + 'hide-display',

    save: function (charts) {
        if (!Ext.isArray(charts))
            charts = [charts];

        var svgs = [];
        Ext.Array.each(charts, function (chart, index) {
            svgs.push(Ext.String.htmlEncode(Ext.draw.engine.SvgExporter.generate(chart.surface)));
        });

        $$iPems.download({
            url: this.defaultUrl,
            params: { svgs: svgs }
        });
    }
});

/* ========================================================================
 * Components: LogicTypeComponent.js
 * /Scripts/components/LogicTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "LogicTypeMultiCombo",
    fieldLabel: '逻辑分类',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "LogicTypeCombo",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointComponent.js
 * /Scripts/components/PointComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointMultiCombo",
    fieldLabel: '信号名称',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load();
    }
});

Ext.define("Ext.ux.PointComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointCombo",
    fieldLabel: '信号名称',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success) {
                    if (records.length > 0) {
                        me.select(records[0]);
                    }
                }
            }
        });
    }
});

Ext.define("Ext.ux.LogicPointMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointMultiPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicPointTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointTypeComponent.js
 * /Scripts/components/PointTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointTypeMultiCombo",
    fieldLabel: '信号类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.PointTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointTypeCombo",
    fieldLabel: '信号类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: RoomComponent.js
 * /Scripts/components/RoomComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomMultiPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: RoomTypeComponent.js
 * /Scripts/components/RoomTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "RoomTypeMultiCombo",
    fieldLabel: '机房类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "RoomTypeCombo",
    fieldLabel: '机房类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: StationComponent.js
 * /Scripts/components/StationComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationMultiPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: StationTypeComponent.js
 * /Scripts/components/StationTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "StationTypeMultiCombo",
    fieldLabel: '站点类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "StationTypeCombo",
    fieldLabel: '站点类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AreaComponent.js
 * /Scripts/components/AreaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaMultiPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: AreaTypeComponent.js
 * /Scripts/components/AreaTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AreaTypeMultiCombo",
    fieldLabel: '区域类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AreaTypeCombo",
    fieldLabel: '区域类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: DeviceComponent.js
 * /Scripts/components/DeviceComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DeviceMultiPicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DevicePicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: DeviceTypeComponent.js
 * /Scripts/components/DeviceTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DeviceTypeMultiCombo",
    fieldLabel: '设备类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "DeviceTypeCombo",
    fieldLabel: '设备类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: EmployeeComponent.js
 * /Scripts/components/EmployeeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.EmployeeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeeMultiPicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.EmployeeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeePicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: ImageExporterComponent.js
 * /Scripts/components/ImageExporterComponent.js
 * ========================================================================
 */

Ext.define('Ext.ux.ImageExporter', {

    singleton: true,

    defaultUrl: '/Component/SaveCharts',

    formCls: Ext.baseCSSPrefix + 'hide-display',

    save: function (charts) {
        if (!Ext.isArray(charts))
            charts = [charts];

        var svgs = [];
        Ext.Array.each(charts, function (chart, index) {
            svgs.push(Ext.String.htmlEncode(Ext.draw.engine.SvgExporter.generate(chart.surface)));
        });

        $$iPems.download({
            url: this.defaultUrl,
            params: { svgs: svgs }
        });
    }
});

/* ========================================================================
 * Components: LogicTypeComponent.js
 * /Scripts/components/LogicTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "LogicTypeMultiCombo",
    fieldLabel: '逻辑分类',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "LogicTypeCombo",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointComponent.js
 * /Scripts/components/PointComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointMultiCombo",
    fieldLabel: '信号名称',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load();
    }
});

Ext.define("Ext.ux.PointComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointCombo",
    fieldLabel: '信号名称',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success) {
                    if (records.length > 0) {
                        me.select(records[0]);
                    }
                }
            }
        });
    }
});

Ext.define("Ext.ux.LogicPointMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointMultiPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicPointTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointTypeComponent.js
 * /Scripts/components/PointTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointTypeMultiCombo",
    fieldLabel: '信号类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.PointTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointTypeCombo",
    fieldLabel: '信号类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: RoomComponent.js
 * /Scripts/components/RoomComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomMultiPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: RoomTypeComponent.js
 * /Scripts/components/RoomTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "RoomTypeMultiCombo",
    fieldLabel: '机房类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "RoomTypeCombo",
    fieldLabel: '机房类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: StationComponent.js
 * /Scripts/components/StationComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationMultiPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: StationTypeComponent.js
 * /Scripts/components/StationTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "StationTypeMultiCombo",
    fieldLabel: '站点类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "StationTypeCombo",
    fieldLabel: '站点类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: AreaComponent.js
 * /Scripts/components/AreaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaMultiPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: AreaTypeComponent.js
 * /Scripts/components/AreaTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AreaTypeMultiCombo",
    fieldLabel: '区域类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AreaTypeCombo",
    fieldLabel: '区域类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: DeviceComponent.js
 * /Scripts/components/DeviceComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DeviceMultiPicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DevicePicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: DeviceTypeComponent.js
 * /Scripts/components/DeviceTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DeviceTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DeviceTypeMultiCombo",
    fieldLabel: '设备类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "DeviceTypeCombo",
    fieldLabel: '设备类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDeviceTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: EmployeeComponent.js
 * /Scripts/components/EmployeeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.EmployeeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeeMultiPicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.EmployeeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeePicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: ImageExporterComponent.js
 * /Scripts/components/ImageExporterComponent.js
 * ========================================================================
 */

Ext.define('Ext.ux.ImageExporter', {

    singleton: true,

    defaultUrl: '/Component/SaveCharts',

    formCls: Ext.baseCSSPrefix + 'hide-display',

    save: function (charts) {
        if (!Ext.isArray(charts))
            charts = [charts];

        var svgs = [];
        Ext.Array.each(charts, function (chart, index) {
            svgs.push(Ext.String.htmlEncode(Ext.draw.engine.SvgExporter.generate(chart.surface)));
        });

        $$iPems.download({
            url: this.defaultUrl,
            params: { svgs: svgs }
        });
    }
});

/* ========================================================================
 * Components: LogicTypeComponent.js
 * /Scripts/components/LogicTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "LogicTypeMultiCombo",
    fieldLabel: '逻辑分类',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "LogicTypeCombo",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointComponent.js
 * /Scripts/components/PointComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointMultiCombo",
    fieldLabel: '信号名称',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load();
    }
});

Ext.define("Ext.ux.PointComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointCombo",
    fieldLabel: '信号名称',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success) {
                    if (records.length > 0) {
                        me.select(records[0]);
                    }
                }
            }
        });
    }
});

Ext.define("Ext.ux.LogicPointMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointMultiPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicPointTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: PointTypeComponent.js
 * /Scripts/components/PointTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.PointTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointTypeMultiCombo",
    fieldLabel: '信号类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.PointTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointTypeCombo",
    fieldLabel: '信号类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPointTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: RoomComponent.js
 * /Scripts/components/RoomComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomMultiPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomPicker",
    fieldLabel: '所属机房',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: RoomTypeComponent.js
 * /Scripts/components/RoomTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.RoomTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "RoomTypeMultiCombo",
    fieldLabel: '机房类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "RoomTypeCombo",
    fieldLabel: '机房类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetRoomTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

/* ========================================================================
 * Components: StationComponent.js
 * /Scripts/components/StationComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationMultiPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});

/* ========================================================================
 * Components: StationTypeComponent.js
 * /Scripts/components/StationTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "StationTypeMultiCombo",
    fieldLabel: '站点类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "StationTypeCombo",
    fieldLabel: '站点类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});