Ext.define("Ext.ux.PointTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "point.type.multicombo",
    fieldLabel: $$iPems.lang.Component.PointType,
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
            url: '/Component/GetPointTypes',
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

Ext.define("Ext.ux.PointTypeComboBox", {
    extend: "Ext.form.ComboBox",
    xtype: "point.type.combo",
    fieldLabel: $$iPems.lang.Component.PointType,
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
            url: '/Component/GetPointTypes',
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