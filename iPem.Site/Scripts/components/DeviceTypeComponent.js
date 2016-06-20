Ext.define("Ext.ux.DeviceTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DeviceTypeMultiCombo",
    fieldLabel: $$iPems.lang.Component.DeviceType,
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
    fieldLabel: $$iPems.lang.Component.DeviceType,
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