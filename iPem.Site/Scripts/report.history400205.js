//Ext.define('ProjectModel', {
//    extend: 'Ext.data.Model',
//    belongsTo: 'ReportModel',
//    fields: [
//        { name: 'Index', type: 'int' },
//        { name: 'Id', type: 'string' },
//        { name: 'Name', type: 'string' },
//        { name: 'StartTime', type: 'string' },
//        { name: 'EndTime', type: 'string' },
//        { name: 'Responsible', type: 'string' },
//        { name: 'ContactPhone', type: 'string' },
//        { name: 'Company', type: 'string' },
//        { name: 'Creator', type: 'string' },
//        { name: 'CreatedTime', type: 'string' },
//        { name: 'Comment', type: 'string' },
//        { name: 'Enabled', type: 'boolean' }
//    ]
//});

Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    //hasMany: { model: 'ProjectModel', name: 'projects', associationKey: 'projects' },
    fields: [
        { name: 'index', type: 'int' },
        { name: 'type', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' },
        { name: 'timeout', type: 'int' },
        { name: 'rate', type: 'string' },
        { name: 'projects', type: 'auto' }
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
        url: '/Report/DownloadHistory400205',
        params: store.proxy.extraParams
    });
};

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400205',
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
    glyph: 0xf046,
    title: '工程项目统计信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    cls: 'x-grid-expander',
    plugins: [{
        ptype: 'rowexpander',
        rowBodyTpl: new Ext.XTemplate(
            '<table class="row-table" cellspacing="0" cellpadding="0" border="0" style="width:100%;">',
            '<tpl if="this.isEmpty(projects)">',
                '<tbody><tr><td>没有数据记录</td><tr/></tbody>',
            '<tpl else>',
                '<thead>',
                    '<tr>',
                        '<td>#</td><td>工程名称</td><td>开始时间</td><td>结束时间</td><td>负责人员</td><td>联系电话</td><td>施工公司</td><td>超时工程</td>',
                    '<tr/>',
                '</thead>',
                '<tbody>',
                    '<tpl for="projects">',
                        '<tr><td>{#}</td><td>{Name}</td><td>{StartTime}</td><td>{EndTime}</td><td>{Responsible}</td><td>{ContactPhone}</td><td>{Company}</td><td>{Enabled:this.isTimeout}</td><tr/>',
                    '</tpl>',
                '</tbody>',
            '</tpl>',
            '</table>',
            {
                isEmpty: function (values) {
                    return !(Ext.isArray(values) && values.length > 0);
                },
                isTimeout: function (timeout) {
                    return timeout === true ? '是' : '否';
                }
            })
    }],
    viewConfig: {
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
        align: 'left',
        sortable: true
    }, {
        text: '名称',
        dataIndex: 'name',
        align: 'left',
        flex: 1,
        sortable: true
    }, {
        text: '工程数量',
        dataIndex: 'count',
        align: 'left',
        sortable: true
    }, {
        text: '平均历时',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '超时工程数量',
        dataIndex: 'timeout',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '超时工程占比',
        dataIndex: 'rate',
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
                        xtype: 'StationPicker',
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