var now = new Date();

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    fields: [],
    proxy: {
        type: 'ajax',
        url: '/Report/RequestCustom400404',
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
    title: '列头柜分路功率信息',
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
                xtype: 'DevicePicker',
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
                value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.MONTH, -1),
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
        url: '/Report/GetFields400404',
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
                            var field = {
                                text: item.column,
                                dataIndex: item.name,
                                width: item.width,
                                align: item.align,
                                renderer: false
                            };

                            if(item.type === "float") {
                                field.renderer = function (val, meta) {
                                    if (val >= 5) {
                                        meta.style = 'background:#ffc7ce;color:#9c0006;';
                                    }
                                    return val;
                                };
                            }

                            fields.push(field);
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
        url: '/Report/DownloadCustom400404',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});