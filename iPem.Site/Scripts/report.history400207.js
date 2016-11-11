//Ext.define('DetailModel', {
//    extend: 'Ext.data.Model',
//    belongsTo: 'ReportModel',
//    fields: [
//        { name: 'area', type: 'string' },
//        { name: 'station', type: 'string' },
//        { name: 'room', type: 'string' },
//        { name: 'device', type: 'string' },
//        { name: 'point', type: 'string' },
//        { name: 'start', type: 'string' },
//        { name: 'end', type: 'string' },
//        { name: 'interval', type: 'string' }
//    ]
//});

Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    //hasMany: { model: 'DetailModel', name: 'details', associationKey: 'details' },
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' },
        { name: 'details', type: 'auto' }
    ]
});

var query = function (pagingtoolbar) {
    var range = Ext.getCmp('rangePicker'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = pagingtoolbar.store;
    me.proxy.extraParams.parent = range.getValue();
    me.proxy.extraParams.starttime = start.getRawValue();
    me.proxy.extraParams.endtime = end.getRawValue();
    me.loadPage(1);
};

var print = function (store) {
    $$iPems.download({
        url: '/Report/DownloadHistory400207',
        params: store.proxy.extraParams
    });
};

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
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

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf029,
    title: '市电停电信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    cls: 'x-grid-expander',
    plugins: [{
        ptype: 'rowexpander',
        rowBodyTpl: new Ext.XTemplate(
            '<table class="row-table" cellspacing="0" cellpadding="0" border="0" style="width:100%;">',
            '<tpl if="this.isEmpty(details)">',
                '<tbody><tr><td>没有数据记录</td><tr/></tbody>',
            '<tpl else>',
                '<thead>',
                    '<tr>',
                        '<td>#</td><td>停电时间</td><td>来电时间</td><td>停电时长</td>',
                    '<tr/>',
                '</thead>',
                '<tbody>',
                    '<tpl for="details">',
                        '<tr><td>{#}</td><td>{start}</td><td>{end}</td><td>{timespan}</td><tr/>',
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
        align: 'left',
        sortable: true
    }, {
        text: '所属区域',
        dataIndex: 'area',
        align: 'left',
        flex: 1,
        sortable: true
    }, {
        text: '站点名称',
        dataIndex: 'name',
        align: 'left',
        width: 150,
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
        sortable: true
    }, {
        text: '停电时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
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
                        selectAll: false,
                        allowBlank: false,
                        emptyText: '请选择查询范围...',
                        fieldLabel: '查询范围',
                        labelWidth: 60,
                        width: 448,
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query(currentPagingToolbar);
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
    }],
    bbar: currentPagingToolbar
})

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});