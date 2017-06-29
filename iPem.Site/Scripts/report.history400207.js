Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'stationid', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' }
    ]
});

Ext.define('ShiDianModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'timespan', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    DownloadURL: '/Report/DownloadHistory400207',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400207',
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

var detailStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ShiDianModel',
    DownloadURL: '/Report/DownloadHistoryDetail400207',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistoryDetail400207',
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
var detailPagingToolbar = $$iPems.clonePagingToolbar(detailStore);

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf029,
    title: '市电停电信息',
    region: 'center',
    store: currentStore,
    bbar: currentPagingToolbar,
    selType: 'cellmodel',
    forceFit: false,
    viewConfig: {
        forceFit: true,
        loadMask: true,
        trackOver: false,
        stripeRows: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
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
        flex: 1,
        sortable: true
    }, {
        text: '所属站点',
        dataIndex: 'station',
        align: 'left',
        width: 200,
        sortable: true
    }, {
        text: '站点类型',
        dataIndex: 'type',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '停电次数',
        dataIndex: 'count',
        align: 'left',
        width: 150,
        renderer: function (value, p, record) {
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>',record.get('stationid') , value);
        }
    }, {
        text: '停电时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
    }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var elements = Ext.fly(td).select('a.grid-link');
            if (elements.getCount() == 0) return false;
            detail(elements.first().getAttribute('data'));
        }
    },
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'rangePicker',
                        xtype: 'AreaPicker',
                        fieldLabel: '查询范围',
                        width: 220
                    },
                    {
                        id: 'station-type-multicombo',
                        xtype: 'StationTypeMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'startField',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        handler: function (me, event) {
                            print(currentStore);
                        }
                    }
                ]
            }
        ]
    }]
});

var detailGrid = Ext.create('Ext.grid.Panel', {
    region: 'center',
    border: false,
    store: detailStore,
    bbar: detailPagingToolbar,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        loadMask: false,
        stripeRows: true,
        trackOver: true,
        preserveScrollOnRefresh: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
    },
    columns: [
        {
            text: '序号',
            dataIndex: 'index',
            width: 60
        },
        {
            text: '所属区域',
            dataIndex: 'area',
            flex:1
        },
        {
            text: '所属站点',
            dataIndex: 'station'
        },
        {
            text: '开始时间',
            dataIndex: 'start',
            align: 'center',
            width: 150
        },
        {
            text: '结束时间',
            dataIndex: 'end',
            align: 'center',
            width: 150
        },
        {
            text: '时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        }
    ]
});

var detailWnd = Ext.create('Ext.window.Window', {
    title: '市电停电详情',
    glyph: 0xf029,
    height: 500,
    width: 800,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    layout: 'border',
    items: [detailGrid],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '导出',
        handler: function (el, e) {
            print(detailStore);
        }
    }, {
        xtype: 'button',
        text: '关闭',
        handler: function (el, e) {
            detailWnd.hide();
        }
    }]
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        types = Ext.getCmp('station-type-multicombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getSelectedValues();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;

    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
        }
    });
};

var print = function (store) {
    $$iPems.download({
        url: store.DownloadURL,
        params: store.getProxy().extraParams
    });
};

var detail = function (station) {
    if (Ext.isEmpty(station)) return false;

    var proxy = detailStore.getProxy();
    proxy.extraParams.station = station;

    detailStore.removeAll();
    detailStore.loadPage(1);
    detailWnd.show();
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }

    query();
});