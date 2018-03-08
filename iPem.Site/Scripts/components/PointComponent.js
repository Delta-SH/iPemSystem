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