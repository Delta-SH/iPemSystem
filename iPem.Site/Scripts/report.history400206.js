Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'stationid', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' },
        { name: 'reservations', type: 'auto' }
    ]
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400206',
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
    glyph: 0xf045,
    title: '工程预约信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    cls: 'x-grid-expander',
    plugins: [{
        ptype: 'rowexpander',
        rowBodyTpl: new Ext.XTemplate(
            '<table class="gridtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;">',
            '<tpl if="this.isEmpty(reservations)">',
                '<tbody><tr><td>没有数据记录</td><tr/></tbody>',
            '<tpl else>',
                '<thead>',
                    '<tr>',
                        '<th>#</th><th>预约名称</th><th>开始时间</th><th>结束时间</th><th>关联工程</th><th>创建人员</th><th>创建时间</th>',
                    '<tr/>',
                '</thead>',
                '<tbody>',
                    '<tpl for="reservations">',
                        '<tr><td>{#}</td><td>{name}</td><td>{startDate}</td><td>{endDate}</td><td>{projectName}</td><td>{creator}</td><td>{createdTime}</td><tr/>',
                    '</tpl>',
                '</tbody>',
            '</tpl>',
            '</table>',
            {
                isEmpty: function (values) {
                    return !(Ext.isArray(values) && values.length > 0);
                }
            })
    }],
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
        align: 'left'
    }, {
        text: '所属区域',
        dataIndex: 'area',
        align: 'left',
        flex: 1
    }, {
        text: '所属站点',
        dataIndex: 'station',
        align: 'left',
        width: 200
    }, {
        text: '预约数量',
        dataIndex: 'count',
        align: 'left',
        width: 150
    }, {
        text: '预约时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150
    }],
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
                        width: 448,
                        pickerWidth: 383
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
                        id: 'exportButton',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (me, event) {
                            print();
                        }
                    }
                ]
            }
        ]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;
    currentStore.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton').setDisabled(success === false);
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/Report/DownloadHistory400206',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});