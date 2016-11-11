var now = new Date();
Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'current', type: 'int' },
        { name: 'last', type: 'int' },
        { name: 'rate', type: 'string' }
    ],
    idProperty: 'index'
});

var query = function (store) {
    var range = Ext.getCmp('rangePicker'),
        types = Ext.getCmp('stationTypeMultiCombo'),
        size = Ext.getCmp('areaTypeCombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!types.isValid()) return;
    if (!size.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    store.proxy.extraParams.parent = range.getValue();
    store.proxy.extraParams.types = types.getValue();
    store.proxy.extraParams.size = size.getValue();
    store.proxy.extraParams.startDate = start.getRawValue();
    store.proxy.extraParams.endDate = end.getRawValue();
    store.loadPage(1);
};

var print = function (store) {
    $$iPems.download({
        url: '/KPI/Download500206',
        params: store.proxy.extraParams
    });
};

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/KPI/Request500206',
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
    title: '温控容量合格率',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    viewConfig: {
        loadMask: true,
        trackOver: true,
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
        text: '区域名称',
        dataIndex: 'name',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '区域类型',
        dataIndex: 'type',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '高温告警站点总数',
        dataIndex: 'current',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '包含温度测点的站点总数',
        dataIndex: 'last',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '温控容量合格率',
        dataIndex: 'rate',
        width: 150,
        align: 'left',
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
                xtype: 'AreaPicker',
                selectAll: true,
                allowBlank: false,
                emptyText: '请选择查询范围...',
                fieldLabel: '查询范围',
                width: 568,
            }, {
                xtype: 'button',
                glyph: 0xf005,
                text: '数据查询',
                handler: function (me, event) {
                    query(currentStore);
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'stationTypeMultiCombo',
                xtype: 'StationTypeMultiCombo',
                emptyText: '默认全部',
                width: 280
            }, {
                id: 'areaTypeCombo',
                xtype: 'AreaTypeCombo',
                fieldLabel: '统计粒度',
                width: 280
            }, {
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                handler: function (me, event) {
                    print(currentStore);
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
            }, {
                id: 'endField',
                xtype: 'datefield',
                fieldLabel: '结束时间',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.SECOND, -1),
                editable: false,
                allowBlank: false
            }]
        }]
    }],
    bbar: currentPagingToolbar
})

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});