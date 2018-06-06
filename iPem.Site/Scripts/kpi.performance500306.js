var now = new Date();

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    fields: [],
    proxy: {
        type: 'ajax',
        url: '/KPI/Request500306',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        },
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf029,
    title: '变压器能耗信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    viewConfig: {
        forceFit: false,
        trackOver: true,
        stripeRows: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
    },
    columns: [],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [{
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'rangePicker',
                xtype: 'RoomPicker',
                fieldLabel: '查询范围',
                width: 280
            },
            {
                id: 'periodCombo',
                xtype: 'PeriodCombo',
                year: false,
                week: false,
                width: 280
            },
            {
                xtype: 'button',
                glyph: 0xf005,
                text: '数据查询',
                handler: function (me, event) {
                    query();
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'startField',
                xtype: 'datefield',
                fieldLabel: '开始时间',
                labelWidth: 60,
                width: 280,
                value: new Date(now.getFullYear(), 0, 1),
                editable: false,
                allowBlank: false
            },
            {
                id: 'endField',
                xtype: 'datefield',
                fieldLabel: '结束时间',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.SECOND, -1),
                editable: false,
                allowBlank: false
            },
            {
                id: 'exportButton',
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                disabled: true,
                handler: function (me, event) {
                    print();
                }
            }]
        }]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        period = Ext.getCmp('periodCombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField'),
        grid = currentPanel,
        view = grid.getView();

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.period = period.getValue();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;

    Ext.Ajax.request({
        url: '/KPI/GetFields500306',
        params: proxy.extraParams,
        mask: new Ext.LoadMask({ target: view, msg: '获取列名...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                me.model.prototype.fields.clear();
                me.removeAll();
                var fields = [];
                if (data.data && Ext.isArray(data.data)) {
                    Ext.Array.each(data.data, function (item, index) {
                        me.model.prototype.fields.replace({ name: item.name, type: item.type });
                        if (!Ext.isEmpty(item.column)) {
                            fields.push({
                                text: item.column,
                                dataIndex: item.name,
                                width: item.width,
                                align: item.align
                            });
                        }
                    });
                }

                grid.reconfigure(me, fields);
                me.loadPage(1, {
                    callback: function (records, operation, success) {
                        proxy.extraParams.cache = success;
                        Ext.getCmp('exportButton').setDisabled(success === false);
                    }
                });
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/KPI/Download500306',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});