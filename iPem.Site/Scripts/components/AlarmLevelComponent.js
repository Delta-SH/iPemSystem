Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "alarm.level.multicombo",
    fieldLabel: $$iPems.lang.AlarmLevel,
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    emptyText: $$iPems.lang.AllEmptyText,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    store: Ext.create('Ext.data.Store', {
        autoLoad: true,
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '../Component/GetAlarmLevels',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            }
        }
    })
});

Ext.define("Ext.ux.AlarmLevelComboBox", {
    extend: "Ext.form.ComboBox",
    xtype: "alarm.level.combo",
    fieldLabel: $$iPems.lang.AlarmLevel,
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    emptyText: $$iPems.lang.AllEmptyText,
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    store: Ext.create('Ext.data.Store', {
        autoLoad: true,
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '../Component/GetAlarmLevels',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            }
        }
    })
});