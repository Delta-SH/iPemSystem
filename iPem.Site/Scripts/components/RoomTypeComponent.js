Ext.define("Ext.ux.RoomTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "room.type.multicombo",
    fieldLabel: $$iPems.lang.RoomType,
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
            { name: 'id', type: 'int' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '../Component/GetRoomTypes',
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
    xtype: "room.type.combo",
    fieldLabel: $$iPems.lang.RoomType,
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
            { name: 'id', type: 'int' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '../Component/GetRoomTypes',
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