Ext.define('MaskModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'time', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'MaskModel',
    proxy: {
        type: 'ajax',
        url: '/Maintenance/RequestMaskings',
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

var currentLayout = Ext.create('Ext.grid.Panel', {
    glyph: 0xf029,
    title: '告警屏蔽信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        trackOver: true,
        stripeRows: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: '序号',
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: '所属区域',
        dataIndex: 'area',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '屏蔽范围',
        dataIndex: 'name',
        align: 'left',
        width: 200,
        sortable: true
    }, {
        text: '屏蔽类型',
        dataIndex: 'type',
        align: 'center',
        sortable: true
    }, {
        text: '屏蔽时间',
        dataIndex: 'time',
        align: 'center',
        width: 150,
        sortable: true
    }],
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
                emptyText: '默认全部',
                labelWidth: 60,
                width: 280
            }, {
                id: 'masking-multicombo',
                xtype: 'MaskingMultiCombo',
                emptyText: '默认全部',
                width: 280
            }, {
                xtype: 'button',
                text: '数据查询',
                glyph: 0xf005,
                handler: function (el, e) {
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
                    value: Ext.Date.add(new Date(), Ext.Date.MONTH, -1),
                    editable: false,
                    allowBlank: false
                },
                {
                    id: 'endField',
                    xtype: 'datefield',
                    fieldLabel: '结束时间',
                    labelWidth: 60,
                    width: 280,
                    value: Ext.Date.add(new Date(), Ext.Date.DAY, 0),
                    editable: false,
                    allowBlank: false
                }, {
                id: 'exportButton',
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                disabled: true,
                handler: function (el, e) {
                    print();
                }
            }]
        }]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var parent = Ext.getCmp('rangePicker').getValue(),
        types = Ext.getCmp('masking-multicombo').getSelectedValues(),
        startDate = Ext.getCmp('startField').getRawValue(),
        endDate = Ext.getCmp('endField').getRawValue();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.types = types;
    proxy.extraParams.startDate = startDate;
    proxy.extraParams.endDate = endDate;
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton').setDisabled(success === false);
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/Maintenance/DownloadMaskings',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentLayout);
    }
});