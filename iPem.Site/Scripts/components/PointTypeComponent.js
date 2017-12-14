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