//#region Model
Ext.define('ReportModel1', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' }
    ]
});

Ext.define('ReportModel2', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' }
    ]
});

Ext.define('DetailModel1', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'interval', type: 'string' }
    ],
    idProperty: 'index'
});

Ext.define('DetailModel2', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'interval', type: 'string' }
    ],
    idProperty: 'index'
});
//#endregion

//#region Store
var currentStore1 = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel1',
    DownloadURL: '/Report/DownloadHistory400207_1',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400207_1',
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
var currentStore2 = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel2',
    DownloadURL: '/Report/DownloadHistory400207_2',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400207_2',
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
var detailStore1 = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'DetailModel1',
    DownloadURL: '/Report/DownloadHistoryDetail400207_1',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistoryDetail400207_1',
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
var detailStore2 = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'DetailModel2',
    DownloadURL: '/Report/DownloadHistoryDetail400207_2',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistoryDetail400207_2',
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

var currentPagingToolbar1 = $$iPems.clonePagingToolbar(currentStore1);
var currentPagingToolbar2 = $$iPems.clonePagingToolbar(currentStore2);
var detailPagingToolbar1 = $$iPems.clonePagingToolbar(detailStore1);
var detailPagingToolbar2 = $$iPems.clonePagingToolbar(detailStore2);
//#endregion

//#region Grid
var GridPanel1 = Ext.create("Ext.grid.Panel", {
    glyph: 0xf018,
    title: '站点停电报表',
    store: currentStore1,
    bbar: currentPagingToolbar1,
    selType: 'cellmodel',
    viewConfig: {
        loadMask: true,
        trackOver: false,
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
        width: 150,
        sortable: true
    }, {
        text: '站点名称',
        dataIndex: 'name',
        align: 'left',
        width: 200,
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
        renderer: function (value, p, record) {
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>',record.get('id') , value);
        }
    }, {
        text: '停电时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
    }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var elements = Ext.fly(td).select('a.grid-link');
            if (elements.getCount() == 0) return false;
            detail1(elements.first().getAttribute('data'));
        }
    },
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        border: false,
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'areaPicker',
                        xtype: 'AreaPicker',
                        fieldLabel: '查询范围',
                        width: 280
                    },
                    {
                        id: 'station-type-multicombo',
                        xtype: 'StationTypeMultiCombo',
                        width: 280,
                        emptyText: '默认全部'
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query1();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'startField1',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField1',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'exportButton1',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (me, event) {
                            print(currentStore1);
                        }
                    }
                ]
            }
        ]
    }]
});

var GridPanel2 = Ext.create("Ext.grid.Panel", {
    glyph: 0xf036,
    title: '机房停电报表',
    store: currentStore2,
    bbar: currentPagingToolbar2,
    selType: 'cellmodel',
    viewConfig: {
        loadMask: true,
        trackOver: false,
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
        width: 150,
        sortable: true
    }, {
        text: '所属站点',
        dataIndex: 'station',
        align: 'left',
        width: 200,
        sortable: true
    }, {
        text: '机房名称',
        dataIndex: 'name',
        align: 'left',
        width: 200,
        sortable: true
    }, {
        text: '机房类型',
        dataIndex: 'type',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '停电次数',
        dataIndex: 'count',
        align: 'left',
        width: 150,
        renderer: function (value, p, record) {
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', record.get('id'), value);
        }
    }, {
        text: '停电时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
    }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var elements = Ext.fly(td).select('a.grid-link');
            if (elements.getCount() == 0) return false;
            detail2(elements.first().getAttribute('data'));
        }
    },
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        border: false,
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'stationPicker',
                        xtype: 'StationPicker',
                        fieldLabel: '查询范围',
                        width: 280
                    },
                    {
                        id: 'room-type-multicombo',
                        xtype: 'RoomTypeMultiCombo',
                        width: 280,
                        emptyText: '默认全部'
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query2();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id:'startField2',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField2',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'exportButton2',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (me, event) {
                            print(currentStore2);
                        }
                    }
                ]
            }
        ]
    }]
});

var detailGrid1 = Ext.create('Ext.grid.Panel', {
    region: 'center',
    border: false,
    store: detailStore1,
    bbar: detailPagingToolbar1,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        loadMask: false,
        stripeRows: true,
        trackOver: true,
        preserveScrollOnRefresh: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
    },
    columns: [
        {
            text: '序号',
            dataIndex: 'index',
            width: 60
        },
        {
            text: '所属区域',
            dataIndex: 'area',
            width: 150,
        },
        {
            text: '站点名称',
            dataIndex: 'name',
            width: 200
        },
        {
            text: '开始时间',
            dataIndex: 'start',
            align: 'center',
            width: 150
        },
        {
            text: '结束时间',
            dataIndex: 'end',
            align: 'center',
            width: 150
        },
        {
            text: '停电时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        }
    ]
});

var detailGrid2 = Ext.create('Ext.grid.Panel', {
    region: 'center',
    border: false,
    store: detailStore2,
    bbar: detailPagingToolbar2,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        loadMask: false,
        stripeRows: true,
        trackOver: true,
        preserveScrollOnRefresh: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
    },
    columns: [
        {
            text: '序号',
            dataIndex: 'index',
            width: 60
        },
        {
            text: '所属区域',
            dataIndex: 'area',
            width: 150
        },
        {
            text: '所属站点',
            dataIndex: 'station',
            width: 200
        },
        {
            text: '机房名称',
            dataIndex: 'name',
            width: 200
        },
        {
            text: '开始时间',
            dataIndex: 'start',
            align: 'center',
            width: 150
        },
        {
            text: '结束时间',
            dataIndex: 'end',
            align: 'center',
            width: 150
        },
        {
            text: '停电时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        }
    ]
});

var currentPanel = Ext.create('Ext.tab.Panel', {
    border: true,
    region: 'center',
    cls: 'x-custom-panel',
    plain: true,
    items: [GridPanel1, GridPanel2]
});
//#endregion

//#region Window
var detailWnd1 = Ext.create('Ext.window.Window', {
    title: '站点停电详情',
    glyph: 0xf018,
    height: 500,
    width: 800,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    layout: 'border',
    items: [detailGrid1],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '导出',
        handler: function (el, e) {
            print(detailStore1);
        }
    }, {
        xtype: 'button',
        text: '关闭',
        handler: function (el, e) {
            detailWnd1.hide();
        }
    }]
});

var detailWnd2 = Ext.create('Ext.window.Window', {
    title: '机房停电详情',
    glyph: 0xf036,
    height: 500,
    width: 800,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    layout: 'border',
    items: [detailGrid2],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '导出',
        handler: function (el, e) {
            print(detailStore2);
        }
    }, {
        xtype: 'button',
        text: '关闭',
        handler: function (el, e) {
            detailWnd2.hide();
        }
    }]
});
//#endregion

//#region Method
var query1 = function () {
    var range = Ext.getCmp('areaPicker'),
        types = Ext.getCmp('station-type-multicombo'),
        start = Ext.getCmp('startField1'),
        end = Ext.getCmp('endField1');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore1, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getSelectedValues();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton1').setDisabled(success === false);
        }
    });
};

var query2 = function () {
    var range = Ext.getCmp('stationPicker'),
        types = Ext.getCmp('room-type-multicombo'),
        start = Ext.getCmp('startField2'),
        end = Ext.getCmp('endField2');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore2, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getSelectedValues();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton2').setDisabled(success === false);
        }
    });
};

var print = function (store) {
    $$iPems.download({
        url: store.DownloadURL,
        params: store.getProxy().extraParams
    });
};

var detail1 = function (id) {
    if (Ext.isEmpty(id)) return false;

    var store = detailStore1,
        proxy = store.getProxy();

    proxy.extraParams.id = id;

    store.removeAll();
    store.loadPage(1);
    detailWnd1.show();
};

var detail2 = function (id) {
    if (Ext.isEmpty(id)) return false;

    var store = detailStore2,
        proxy = store.getProxy();

    proxy.extraParams.id = id;

    store.removeAll();
    store.loadPage(1);
    detailWnd2.show();
};
//#endregion

//#region Ready
Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});
//#endregion