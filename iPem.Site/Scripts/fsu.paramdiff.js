Ext.define('DiffModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'device', type: 'string' },
        { name: 'point', type: 'string' },
        { name: 'threshold', type: 'string' },
        { name: 'level', type: 'string' },
        { name: 'nmid', type: 'string' },
        { name: 'absolute', type: 'string' },
        { name: 'relative', type: 'string' },
        { name: 'interval', type: 'string' },
        { name: 'reftime', type: 'string' },
        { name: 'masked', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'DiffModel',
    proxy: {
        type: 'ajax',
        url: '/Fsu/RequestParamDiff',
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
    title: '参数巡检信息',
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
        sortable: true
    }, {
        text: '所属站点',
        dataIndex: 'station',
        align: 'left',
        sortable: true
    }, {
        text: '所属机房',
        dataIndex: 'room',
        align: 'left',
        sortable: true
    }, {
        text: '所属设备',
        dataIndex: 'device',
        align: 'left',
        sortable: true
    }, {
        text: '信号名称',
        dataIndex: 'point',
        align: 'left',
        sortable: true
    }, {
        text: '告警门限值',
        dataIndex: 'threshold',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '告警等级',
        dataIndex: 'level',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '网管告警编号',
        dataIndex: 'nmid',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '绝对阀值',
        dataIndex: 'absolute',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '百分比阀值',
        dataIndex: 'relative',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '存储时间间隔',
        dataIndex: 'interval',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '存储参考时间',
        dataIndex: 'reftime',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '屏蔽信号',
        dataIndex: 'masked',
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
                id: 'point-multipicker',
                xtype: 'PointMultiPicker',
                emptyText: '默认全部'
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
                id: 'dateField',
                xtype: 'datetimepicker',
                fieldLabel: '查询月份',
                labelWidth: 60,
                width: 280,
                value: Ext.ux.DateTime.todayString('Ym'),
                format:'yyyyMM'
            }, {
                id: 'keywordsField',
                xtype: 'textfield',
                fieldLabel: '关键字',
                emptyText: '多关键字请以;分隔，例: A;B;C',
                labelWidth: 60,
                width: 280
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
        points = Ext.getCmp('point-multipicker').getValue(),
        date = Ext.getCmp('dateField').getRawValue(),
        keywords = Ext.getCmp('keywordsField').getRawValue();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.points = points;
    proxy.extraParams.date = date;
    proxy.extraParams.keywords = keywords;
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
        url: '/Fsu/DownloadParamDiff',
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