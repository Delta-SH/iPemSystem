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
        { name: 'type', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'float' },
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
        url: '/Report/DownloadHistory400208',
        params: store.proxy.extraParams
    });
};

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400208',
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
    glyph: 0xf029,
    title: $$iPems.lang.Report400208.Title,
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
            '<tpl if="this.isEmpty(details)">',
                Ext.String.format('<tbody><tr><td>{0}</td><tr/></tbody>', $$iPems.lang.RowEmptyText),
            '<tpl else>',
                '<thead>',
                    '<tr>',
                    Ext.String.format('<td>#</td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td>'
                    , $$iPems.lang.Report400208.Columns.Rows.Area
                    , $$iPems.lang.Report400208.Columns.Rows.Station
                    , $$iPems.lang.Report400208.Columns.Rows.Room
                    , $$iPems.lang.Report400208.Columns.Rows.Device
                    , $$iPems.lang.Report400208.Columns.Rows.Point
                    , $$iPems.lang.Report400208.Columns.Rows.Start
                    , $$iPems.lang.Report400208.Columns.Rows.End
                    , $$iPems.lang.Report400208.Columns.Rows.Interval),
                    '<tr/>',
                '</thead>',
                '<tbody>',
                    '<tpl for="details">',
                        '<tr><td>{#}</td><td>{area}</td><td>{station}</td><td>{room}</td><td>{device}</td><td>{point}</td><td>{start}</td><td>{end}</td><td>{interval}</td><tr/>',
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
        text: $$iPems.lang.Report400208.Columns.Index,
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400208.Columns.Type,
        dataIndex: 'type',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400208.Columns.Name,
        dataIndex: 'name',
        align: 'left',
        flex: 1,
        sortable: true
    }, {
        text: $$iPems.lang.Report400208.Columns.Count,
        dataIndex: 'count',
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Report400208.Columns.Interval,
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
                        emptyText: $$iPems.lang.Report400208.ToolBar.RangeEmptyText,
                        fieldLabel: $$iPems.lang.Report400208.ToolBar.Range,
                        labelWidth: 60,
                        width: 448,
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: $$iPems.lang.Query,
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
                        fieldLabel: $$iPems.lang.Report400208.ToolBar.Start,
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Report400208.ToolBar.End,
                        labelWidth: 60,
                        width: 220,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf010,
                        text: $$iPems.lang.Import,
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