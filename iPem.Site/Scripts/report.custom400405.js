Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'last', type: 'float' },
        { name: 'current', type: 'float' },
        { name: 'currentA', type: 'float' },
        { name: 'currentB', type: 'float' },
        { name: 'total', type: 'float' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestCustom400405',
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
    title: '列头柜分路电量信息',
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
        sortable: true
    }, {
        text: '所属区域',
        dataIndex: 'area',
        width: 150,
        sortable: true
    }, {
        text: '所属站点',
        dataIndex: 'station',
        width: 150,
        sortable: true
    }, {
        text: '所属机房',
        dataIndex: 'room',
        width: 150,
        sortable: true
    }, {
        text: '分路编号',
        dataIndex: 'name',
        width: 150,
        sortable: true
    }, {
        text: '上月底数据',
        dataIndex: 'last',
        width: 150,
        sortable: true
    }, {
        text: '本月底数据',
        dataIndex: 'current',
        width: 150,
        sortable: true
    }, {
        text: '本月底数据(备)',
        dataIndex: 'currentB',
        width: 150,
        sortable: true
    }, {
        text: '本月底数据(主)',
        dataIndex: 'currentA',
        width: 150,
        sortable: true
    }, {
        text: '本月用电量(Kwh)',
        dataIndex: 'total',
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
                width: 280
            }, {
                id: 'dateField',
                xtype: 'datetimepicker',
                fieldLabel: '查询月份',
                labelWidth: 60,
                width: 220,
                value: Ext.ux.DateTime.todayString('Ym'),
                format: 'yyyyMM'
            }, {
                xtype: 'button',
                text: '数据查询',
                glyph: 0xf005,
                handler: function (el, e) {
                    query();
                }
            },'-', {
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
        date = Ext.getCmp('dateField').getRawValue();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.date = date;
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
        url: '/Report/DownloadCustom400405',
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