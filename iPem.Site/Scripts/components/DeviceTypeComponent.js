﻿/* ========================================================================
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