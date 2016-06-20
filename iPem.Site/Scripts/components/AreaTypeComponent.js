Ext.define("Ext.ux.AreaTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AreaTypeMultiCombo",
    fieldLabel: $$iPems.lang.Component.AreaType,
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
    fieldLabel: $$iPems.lang.Component.AreaType,
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