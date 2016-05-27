Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmLevelMultiCombo",
    fieldLabel: $$iPems.lang.Component.AlarmLevel,
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
        this.callParent(arguments);
        this.store.load();
    },
    store: Ext.create('Ext.data.Store', {
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'int' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Component/GetAlarmLevels',
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
    xtype: "AlarmLevelCombo",
    fieldLabel: $$iPems.lang.Component.AlarmLevel,
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
        this.callParent(arguments);
        this.store.load();
    },
    store: Ext.create('Ext.data.Store', {
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'int' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Component/GetAlarmLevels',
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