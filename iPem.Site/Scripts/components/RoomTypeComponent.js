Ext.define("Ext.ux.RoomTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "RoomTypeMultiCombo",
    fieldLabel: $$iPems.lang.Component.RoomType,
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
    fieldLabel: $$iPems.lang.Component.RoomType,
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