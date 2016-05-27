Ext.define("Ext.ux.StationTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "StationTypeMultiCombo",
    fieldLabel: $$iPems.lang.Component.StationType,
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
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Component/GetStationTypes',
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

Ext.define("Ext.ux.StationTypeComboBox", {
    extend: "Ext.form.ComboBox",
    xtype: "StationTypeCombo",
    fieldLabel: $$iPems.lang.Component.StationType,
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
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Component/GetStationTypes',
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