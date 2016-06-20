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
        { name: 'interval', type: 'float' },
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
                Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf045,
    title: $$iPems.lang.Report400206.Title,
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: true,
    cls: 'x-grid-expander',
    plugins: [{
        ptype: 'rowexpander',
        rowBodyTpl: new Ext.XTemplate(
            '<table class="row-table" cellspacing="0" cellpadding="0" border="0" style="width:100%;">',
            '<tpl if="this.isEmpty(appointments)">',
                Ext.String.format('<tbody><tr><td>{0}</td><tr/></tbody>', $$iPems.lang.RowEmptyText),
            '<tpl else>',
                '<thead>',
                    '<tr>',
                    Ext.String.format('<td>#</td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td>'
                    , $$iPems.lang.Report400206.Columns.Rows.Id
                    , $$iPems.lang.Report400206.Columns.Rows.StartTime
                    , $$iPems.lang.Report400206.Columns.Rows.EndTime
                    , $$iPems.lang.Report400206.Columns.Rows.Project
                    , $$iPems.lang.Report400206.Columns.Rows.Creator
                    , $$iPems.lang.Report400206.Columns.Rows.CreatedTime),
                    '<tr/>',
                '</thead>',
                '<tbody>',
                    '<tpl for="appointments">',
                        '<tr><td>{#}</td><td>{id}</td><td>{startTime}</td><td>{endTime}</td><td>{projectName}</td><td>{creator}</td><td>{createdTime}</td><tr/>',
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
        emptyText: $$iPems.lang.GridEmptyText,
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: $$iPems.lang.Report400206.Columns.Index,
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400206.Columns.Type,
        dataIndex: 'type',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400206.Columns.Name,
        dataIndex: 'name',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400206.Columns.Count,
        dataIndex: 'count',
        align: 'left',
        sortable: true
    },{
        text: $$iPems.lang.Report400206.Columns.Interval,
        dataIndex: 'interval',
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
                        fieldLabel: $$iPems.lang.Report400206.ToolBar.Range,
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
                        fieldLabel: $$iPems.lang.Report400206.ToolBar.Start,
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Report400206.ToolBar.End,
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