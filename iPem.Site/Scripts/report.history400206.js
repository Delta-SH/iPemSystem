//Ext.define('AppointmentModel', {
//    extend: 'Ext.data.Model',
//    belongsTo: 'ReportModel',
//    fields: [
//        { name: 'index', type: 'int' },
//        { name: 'id', type: 'string' },
//        { name: 'startTime', type: 'string' },
//        { name: 'endTime', type: 'string' },
//        { name: 'projectName', type: 'string' },
//        { name: 'projectId', type: 'string' },
//        { name: 'creator', type: 'string' },
//        { name: 'createdTime', type: 'string' },
//        { name: 'comment', type: 'string' },
//        { name: 'enabled', type: 'boolean' }
//    ]
//});

Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    //hasMany: { model: 'AppointmentModel', name: 'appointments', associationKey: 'appointments' },
    fields: [
        { name: 'index', type: 'int' },
        { name: 'type', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' },
        { name: 'appointments', type: 'auto' }
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
        url: '/Report/DownloadHistory400206',
        params: store.proxy.extraParams
    });
};

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
    title: '工程预约统计信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    cls: 'x-grid-expander',
    plugins: [{
        ptype: 'rowexpander',
        rowBodyTpl: new Ext.XTemplate(
            '<table class="row-table" cellspacing="0" cellpadding="0" border="0" style="width:100%;">',
            '<tpl if="this.isEmpty(appointments)">',
                '<tbody><tr><td>没有数据记录</td><tr/></tbody>',
            '<tpl else>',
                '<thead>',
                    '<tr>',
                        '<td>#</td><td>开始时间</td><td>结束时间</td><td>预约工程</td><td>创建人员</td><td>创建时间</td>',
                    '<tr/>',
                '</thead>',
                '<tbody>',
                    '<tpl for="appointments">',
                        '<tr><td>{#}</td><td>{startTime}</td><td>{endTime}</td><td>{projectName}</td><td>{creator}</td><td>{createdTime}</td><tr/>',
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
        forceFit: false,
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
        text: '预约数量',
        dataIndex: 'count',
        align: 'left',
        width: 150,
        sortable: true
    },{
        text: '预约时长',
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