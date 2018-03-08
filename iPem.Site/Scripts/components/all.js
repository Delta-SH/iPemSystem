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
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all }
        });
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
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.AlarmTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmTypeMultiCombo",
    fieldLabel: '告警类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    store: Ext.create('Ext.data.Store', {
        fields: [
             { name: 'id', type: 'int' },
             { name: 'text', type: 'string' }
        ],
        data: [
            { "id": 1, "text": '包含系统告警' },
            { "id": 2, "text": '包含屏蔽告警' }
        ]
    })
});

Ext.define("Ext.ux.AlarmTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmTypeCombo",
    fieldLabel: '告警类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    store: Ext.create('Ext.data.Store', {
        fields: [
             { name: 'id', type: 'int' },
             { name: 'text', type: 'string' }
        ],
        data: [
            { "id": 1, "text": '包含系统告警' },
            { "id": 2, "text": '包含屏蔽告警' }
        ]
    })
});

Ext.define("Ext.ux.BIAlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "BIAlarmLevelMultiCombo",
    fieldLabel: '告警等级',
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
        me.storeUrl = '/Component/GetBIAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.BIAlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "BIAlarmLevelCombo",
    fieldLabel: '告警等级',
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
        me.storeUrl = '/Component/GetBIAlarmLevels';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.ConfirmMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "ConfirmMultiCombo",
    fieldLabel: '确认状态',
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
        me.storeUrl = '/Component/GetConfirms';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.ConfirmComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ConfirmCombo",
    fieldLabel: '确认状态',
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
        me.storeUrl = '/Component/GetConfirms';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.ReservationMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "ReservationMultiCombo",
    fieldLabel: '工程状态',
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
        me.storeUrl = '/Component/GetReservations';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.ReservationComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ReservationCombo",
    fieldLabel: '工程状态',
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
        me.storeUrl = '/Component/GetReservations';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.SeniorConditionMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "SeniorConditionMultiCombo",
    fieldLabel: '订制条件',
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
        me.storeUrl = '/Component/GetSeniorConditionCombo';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.SeniorConditionComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "SeniorConditionCombo",
    fieldLabel: '订制条件',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: true,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetSeniorConditionCombo';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.MaskingMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "MaskingMultiCombo",
    fieldLabel: '屏蔽类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetMaskingTypes';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all }
        });
    }
});

Ext.define("Ext.ux.MaskingComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "MaskingCombo",
    fieldLabel: '屏蔽类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetMaskingTypes';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
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
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

/* ========================================================================
 * Components: DepartmentComponent.js
 * /Scripts/components/DepartmentComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DepartmentMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DepartmentMultiCombo",
    fieldLabel: '所属部门',
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
        me.storeUrl = '/Component/GetDepartments';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DepartmentComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "DepartmentCombo",
    fieldLabel: '所属部门',
    valueField: 'id',
    displayField: 'text',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDepartments';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
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
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.SubDeviceTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "SubDeviceTypeMultiPicker",
    fieldLabel: '设备类型',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetSubDeviceTypes';
        me.searchUrl = '/Component/FilterSubDeviceTypesPath';
        me.queryUrl = '/Component/GetSubDeviceTypesPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.SubDeviceTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "SubDeviceTypePicker",
    fieldLabel: '设备类型',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetSubDeviceTypes';
        me.searchUrl = '/Component/FilterSubDeviceTypesPath';
        me.queryUrl = '/Component/GetSubDeviceTypesPath';

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
 * Components: FormulaComponent.js
 * /Scripts/components/FormulaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.ComputeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "ComputeMultiCombo",
    fieldLabel: '运算方式',
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
        me.storeUrl = '/Component/GetComputes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.ComputeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ComputeCombo",
    fieldLabel: '运算方式',
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
        me.storeUrl = '/Component/GetComputes';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
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
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
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

Ext.define("Ext.ux.SubLogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "SubLogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetSubLogicTree';
        me.searchUrl = '/Component/FilterSubLogicTreePath';
        me.queryUrl = '/Component/GetSubLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.SubLogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "SubLogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetSubLogicTree';
        me.searchUrl = '/Component/FilterSubLogicTreePath';
        me.queryUrl = '/Component/GetSubLogicTreePath';

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
        me.storeUrl = '/Component/GetPoints';
        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, _ai, _ao, _di, _do, _al) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams._ai = _ai || false;
        me.store.proxy.extraParams._ao = _ao || false;
        me.store.proxy.extraParams._di = _di || false;
        me.store.proxy.extraParams._do = _do || false;
        me.store.proxy.extraParams._al = _al || false;
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
        me.storeUrl = '/Component/GetPoints';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    },
    bind: function (device, _ai, _ao, _di, _do, _al) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams._ai = _ai || false;
        me.store.proxy.extraParams._ao = _ao || false;
        me.store.proxy.extraParams._di = _di || false;
        me.store.proxy.extraParams._do = _do || false;
        me.store.proxy.extraParams._al = _al || false;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.ControlComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ControlCombo",
    fieldLabel: '控制参数',
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
        me.storeUrl = '/Component/GetControls';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    },
    bind: function (point) {
        var me = this;
        if (Ext.isEmpty(point)) return false;

        me.store.proxy.extraParams.point = point;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.PointMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "PointMultiPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    AI: true,
    AO: true,
    DI: true,
    DO: true,
    AL: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetPointTree';
        me.searchUrl = '/Component/FilterPointTreePath';
        me.queryUrl = '/Component/GetPointTreePath';

        me.callParent(arguments);
        me.store.proxy.extraParams.AI = me.AI;
        me.store.proxy.extraParams.AO = me.AO;
        me.store.proxy.extraParams.DI = me.DI;
        me.store.proxy.extraParams.DO = me.DO;
        me.store.proxy.extraParams.AL = me.AL;
        me.store.load();
    }
});

Ext.define("Ext.ux.PointTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "PointPicker",
    fieldLabel: '信号名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    AI: true,
    AO: true,
    DI: true,
    DO: true,
    AL: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetPointTree';
        me.searchUrl = '/Component/FilterPointTreePath';
        me.queryUrl = '/Component/GetPointTreePath';

        me.callParent(arguments);
        me.store.proxy.extraParams.AI = me.AI;
        me.store.proxy.extraParams.AO = me.AO;
        me.store.proxy.extraParams.DI = me.DI;
        me.store.proxy.extraParams.DO = me.DO;
        me.store.proxy.extraParams.AL = me.AL;
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
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.PointParamMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointParamMultiCombo",
    fieldLabel: '信号参数',
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
        me.storeUrl = '/Component/GetPointParams';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.PointParamComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointParamCombo",
    fieldLabel: '信号参数',
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
        me.storeUrl = '/Component/GetPointParams';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
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
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
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
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

/* ========================================================================
 * Components: VendorComponent.js
 * /Scripts/components/VendorComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.VendorMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "VendorMultiCombo",
    fieldLabel: 'FSU厂家',
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
        me.storeUrl = '/Component/GetVendors';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.VendorComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "VendorCombo",
    fieldLabel: 'FSU厂家',
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
        me.storeUrl = '/Component/GetVendors';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.FsuEventMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "FsuEventMultiCombo",
    fieldLabel: '日志类型',
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
        me.storeUrl = '/Component/GetFsuEvents';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.FsuEventComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "FsuEventCombo",
    fieldLabel: '日志类型',
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
        me.storeUrl = '/Component/GetFsuEvents';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});