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