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
        { name: 'interval', type: 'float' },
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
                Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf046,
    title: $$iPems.lang.Report400205.Title,
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
            '<tpl if="this.isEmpty(projects)">',
                Ext.String.format('<tbody><tr><td>{0}</td><tr/></tbody>', $$iPems.lang.RowEmptyText),
            '<tpl else>',
                '<thead>',
                    '<tr>',
                    Ext.String.format('<td>#</td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td>'
                    , $$iPems.lang.Report400205.Columns.Rows.Name
                    , $$iPems.lang.Report400205.Columns.Rows.StartTime
                    , $$iPems.lang.Report400205.Columns.Rows.EndTime
                    , $$iPems.lang.Report400205.Columns.Rows.Responsible
                    , $$iPems.lang.Report400205.Columns.Rows.ContactPhone
                    , $$iPems.lang.Report400205.Columns.Rows.Company
                    , $$iPems.lang.Report400205.Columns.Rows.Timeout),
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
                    return timeout === true ? $$iPems.lang.YesTrue : $$iPems.lang.YesFalse;
                }
            })
    }],
    viewConfig: {
        forceFit: false,
        trackOver: true,
        stripeRows: true,
        emptyText: $$iPems.lang.GridEmptyText,
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: $$iPems.lang.Report400205.Columns.Index,
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400205.Columns.Type,
        dataIndex: 'type',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400205.Columns.Name,
        dataIndex: 'name',
        align: 'left',
        flex: 1,
        sortable: true
    }, {
        text: $$iPems.lang.Report400205.Columns.Count,
        dataIndex: 'count',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400205.Columns.Interval,
        dataIndex: 'interval',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400205.Columns.Timeout,
        dataIndex: 'timeout',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400205.Columns.Rate,
        dataIndex: 'rate',
        align: 'left',
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
                        fieldLabel: $$iPems.lang.Report400205.ToolBar.Range,
                        labelWidth: 60,
                        width: 448,
                    },
                    {
                        xtype: 'splitbutton',
                        glyph: 0xf005,
                        text: $$iPems.lang.Ok,
                        handler: function (me, event) {
                            query(currentPagingToolbar);
                        },
                        menu: [{
                            text: $$iPems.lang.Import,
                            glyph: 0xf010,
                            handler: function (me, event) {
                                print(currentStore);
                            }
                        }]
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
                        fieldLabel: $$iPems.lang.Report400205.ToolBar.Start,
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Report400205.ToolBar.End,
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
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