﻿Ext.define('ReportModel', {
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

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/KPI/Request500401',
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
    title: '系统设备完好率',
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
        text: '区域名称',
        dataIndex: 'name',
        align: 'left',
        flex: 1,
        sortable: true
    }, {
        text: '区域类型',
        dataIndex: 'type',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '设备数量',
        dataIndex: 'devCount',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '告警时长',
        dataIndex: 'almTime',
        width: 150,
        align: 'left',
        sortable: true
    }, {
        text: '统计时长',
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
                xtype: 'AreaPicker',
                selectAll: true,
                allowBlank: false,
                emptyText: '请选择查询范围...',
                fieldLabel: '查询范围',
                width: 568,
                pickerWidth: 503
            }, {
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
                id: 'areaTypeCombo',
                xtype: 'AreaTypeCombo',
                fieldLabel: '统计粒度',
                width: 280
            }, {
                id: 'deviceTypeMulticombo',
                xtype: 'DeviceTypeMultiCombo',
                emptyText: '默认全部',
                width: 280
            }, {
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                handler: function (me, event) {
                    print();
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
            }]
        }]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        size = Ext.getCmp('areaTypeCombo'),
        type = Ext.getCmp('deviceTypeMulticombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!size.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.size = size.getValue();
    proxy.extraParams.types = type.getValue();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    me.loadPage(1);
};

var print = function () {
    $$iPems.download({
        url: '/KPI/Download500401',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }

    Ext.defer(query, 2000);
});