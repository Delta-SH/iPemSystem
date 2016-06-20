Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "LogicTypeMultiCombo",
    fieldLabel: $$iPems.lang.Component.LogicType,
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
    fieldLabel: $$iPems.lang.Component.LogicType,
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