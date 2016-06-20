Ext.define("Ext.ux.PointMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "PointMultiCombo",
    fieldLabel: $$iPems.lang.Component.PointName,
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

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load();
    }
});

Ext.define("Ext.ux.PointComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PointCombo",
    fieldLabel: $$iPems.lang.Component.PointName,
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

        me.store = Ext.create('Ext.data.Store', {
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Component/GetPoints',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        });

        me.callParent(arguments);
        me.store.load();
    },
    bind: function (device, AI, AO, DI, DO) {
        var me = this;
        if (Ext.isEmpty(device)) return false;

        me.store.proxy.extraParams.device = device;
        me.store.proxy.extraParams.AI = AI;
        me.store.proxy.extraParams.AO = AO;
        me.store.proxy.extraParams.DI = DI;
        me.store.proxy.extraParams.DO = DO;
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success) {
                    if (records.length > 0) {
                        me.select(records[0]);
                    }
                }
            }
        });
    }
});

Ext.define("Ext.ux.LogicPointMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointMultiPicker",
    fieldLabel: $$iPems.lang.Component.PointName,
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicPointTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicPointPicker",
    fieldLabel: $$iPems.lang.Component.PointName,
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicPoints';
        me.searchUrl = '/Component/FilterLogicPointPath';
        me.queryUrl = '/Component/GetLogicPointPath';

        me.callParent(arguments);
        me.store.load();
    }
});