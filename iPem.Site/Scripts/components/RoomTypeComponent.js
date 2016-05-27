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
            url: '/Component/GetRoomTypes',
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

Ext.define("Ext.ux.RoomTypeComboBox", {
    extend: "Ext.form.ComboBox",
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
            url: '/Component/GetRoomTypes',
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