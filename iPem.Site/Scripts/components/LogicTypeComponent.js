Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "logic.type.multicombo",
    fieldLabel: $$iPems.lang.LogicType,
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
            url: '../Component/GetLogicTypes',
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

Ext.define("Ext.ux.LogicTypeComboBox", {
    extend: "Ext.form.ComboBox",
    xtype: "logic.type.combo",
    fieldLabel: $$iPems.lang.LogicType,
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
            url: '../Component/GetLogicTypes',
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