Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'type', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'devCount', type: 'int' },
        { name: 'almTime', type: 'string' },
        { name: 'cntTime', type: 'string' },
        { name: 'rate', type: 'string' }
    ],
    idProperty: 'index'
});

var query = function (store) {
    var range = Ext.getCmp('rangePicker'),
        type = Ext.getCmp('deviceTypeMulticombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    store.proxy.extraParams.parent = range.getValue();
    store.proxy.extraParams.types = type.getValue();
    store.proxy.extraParams.startDate = start.getRawValue();
    store.proxy.extraParams.endDate = end.getRawValue();
    store.loadPage(1);
};

var print = function (store) {
    $$iPems.download({
        url: '/KPI/Download500101',
        params: store.proxy.extraParams
    });
};

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/KPI/Request500101',
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
    title: '系统设备完好率信息',
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
    columns: [{
        text: '序号',
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: '类型',
        dataIndex: 'type',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '名称',
        dataIndex: 'name',
        align: 'left',
        flex: 1,
        sortable: true
    }, {
        text: '设备数量',
        dataIndex: 'devCount',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '设备告警时长',
        dataIndex: 'almTime',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '设备统计时长',
        dataIndex: 'cntTime',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '设备完好率',
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
                xtype: 'StationPicker',
                selectAll: false,
                allowBlank: false,
                emptyText: '请选择查询范围...',
                fieldLabel: '查询范围',
                width: 280,
            }, {
                id: 'deviceTypeMulticombo',
                xtype: 'DeviceTypeMultiCombo',
                width: 280,
                emptyText: '默认全部'
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
                id: 'startField',
                xtype: 'datefield',
                fieldLabel: '开始时间',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                editable: false,
                allowBlank: false
            }, {
                id: 'endField',
                xtype: 'datefield',
                fieldLabel: '结束时间',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                editable: false,
                allowBlank: false
            }, {
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                handler: function (me, event) {
                    print(currentStore);
                }
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