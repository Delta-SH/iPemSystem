//#region Global
var now = new Date();
//#endregion

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
        { name: 'interval', type: 'string' },
        { name: 'value', type: 'string' }
    ]
});

Ext.define('ReportModel2', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' },
        { name: 'value', type: 'string' }
    ]
});

Ext.define('DetailModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'interval', type: 'string' },
        { name: 'value', type: 'string' }
    ],
    idProperty: 'index'
});
//#endregion

//#region Store
var currentStore1 = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel1',
    DownloadURL: '/Report/DownloadHistory400208_1',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400208_1',
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
    DownloadURL: '/Report/DownloadHistory400208_2',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400208_2',
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
var currentStore3 = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    fields: [],
    DownloadURL: '/Report/DownloadHistory400208_3',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400208_3',
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
    model: 'DetailModel',
    DownloadURL: '/Report/DownloadHistoryDetail400208_1',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistoryDetail400208_1',
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
    model: 'DetailModel',
    DownloadURL: '/Report/DownloadHistoryDetail400208_2',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistoryDetail400208_2',
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
var currentPagingToolbar3 = $$iPems.clonePagingToolbar(currentStore3);
var detailPagingToolbar1 = $$iPems.clonePagingToolbar(detailStore1);
var detailPagingToolbar2 = $$iPems.clonePagingToolbar(detailStore2);
//#endregion

//#region Grid
var GridPanel1 = Ext.create("Ext.grid.Panel", {
    glyph: 0xf018,
    title: '站点发电次数报表',
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
        width: 150,
        sortable: true
    }, {
        text: '站点类型',
        dataIndex: 'type',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '发电次数',
        dataIndex: 'count',
        align: 'left',
        width: 150,
        renderer: function (value, p, record) {
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', record.get('id'), value);
        }
    }, {
        text: '发电时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '发电量',
        dataIndex: 'value',
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
    glyph: 0xf037,
    title: '油机发电次数报表',
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
        width: 150,
        sortable: true
    }, {
        text: '所属机房',
        dataIndex: 'room',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '设备名称',
        dataIndex: 'name',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '设备类型',
        dataIndex: 'type',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '发电次数',
        dataIndex: 'count',
        align: 'left',
        width: 150,
        renderer: function (value, p, record) {
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', record.get('id'), value);
        }
    }, {
        text: '发电时长',
        dataIndex: 'interval',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '发电量',
        dataIndex: 'value',
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
                        id: 'roomPicker',
                        xtype: 'RoomPicker',
                        fieldLabel: '查询范围',
                        width: 280
                    },
                    {
                        id: 'subDeviceTypePicker',
                        xtype: 'SubDeviceTypeMultiPicker',
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
                        id: 'startField2',
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

var GridPanel3 = Ext.create("Ext.grid.Panel", {
    glyph: 0xf092,
    title: '油机发电量报表',
    selType: 'cellmodel',
    viewConfig: {
        loadMask: true,
        trackOver: false,
        stripeRows: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
    },
    store: currentStore3,
    bbar: currentPagingToolbar3,
    columns: [],
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
                        id: 'roomPicker3',
                        xtype: 'RoomPicker',
                        fieldLabel: '查询范围',
                        width: 280
                    },
                    {
                        id: 'periodCombo3',
                        xtype: 'PeriodCombo',
                        year: false,
                        hour: false,
                        width: 280
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query3();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'startField3',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: new Date(now.getFullYear(), 0, 1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField3',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.SECOND, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'exportButton3',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (me, event) {
                            print(currentStore3);
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
            text: '所属站点',
            dataIndex: 'station',
            align: 'left',
            width: 150,
            sortable: true
        }, {
            text: '所属机房',
            dataIndex: 'room',
            align: 'left',
            width: 150,
            sortable: true
        }, {
            text: '设备名称',
            dataIndex: 'name',
            align: 'left',
            width: 150,
            sortable: true
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
            text: '发电时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        },
        {
            text: '发电量',
            dataIndex: 'value',
            align: 'left',
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
            width: 150,
        },
        {
            text: '所属站点',
            dataIndex: 'station',
            align: 'left',
            width: 150,
            sortable: true
        }, {
            text: '所属机房',
            dataIndex: 'room',
            align: 'left',
            width: 150,
            sortable: true
        }, {
            text: '设备名称',
            dataIndex: 'name',
            align: 'left',
            width: 150,
            sortable: true
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
            text: '发电时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        },
        {
            text: '发电量',
            dataIndex: 'value',
            align: 'left',
            width: 150
        }
    ]
});

var currentPanel = Ext.create('Ext.tab.Panel', {
    border: true,
    region: 'center',
    cls: 'x-custom-panel',
    plain: true,
    items: [GridPanel1, GridPanel2, GridPanel3]
});
//#endregion

//#region Window
var detailWnd1 = Ext.create('Ext.window.Window', {
    title: '油机发电详情',
    glyph: 0xf037,
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
    title: '油机发电详情',
    glyph: 0xf037,
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
    var range = Ext.getCmp('roomPicker'),
        types = Ext.getCmp('subDeviceTypePicker'),
        start = Ext.getCmp('startField2'),
        end = Ext.getCmp('endField2');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore2, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getValue();
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

var query3 = function () {
    var range = Ext.getCmp('roomPicker3'),
        period = Ext.getCmp('periodCombo3'),
        start = Ext.getCmp('startField3'),
        end = Ext.getCmp('endField3'),
        grid = GridPanel3,
        view = grid.getView();

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore3, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.period = period.getValue();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;

    Ext.Ajax.request({
        url: '/Report/GetFields400208_3',
        params: proxy.extraParams,
        mask: new Ext.LoadMask({ target: view, msg: '获取列名...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                me.model.prototype.fields.clear();
                me.removeAll();
                var fields = [];
                if (data.data && Ext.isArray(data.data)) {
                    Ext.Array.each(data.data, function (item, index) {
                        me.model.prototype.fields.replace({ name: item.name, type: item.type });
                        if (!Ext.isEmpty(item.column)) {
                            fields.push({
                                text: item.column,
                                dataIndex: item.name,
                                width: item.width,
                                align: item.align
                            });
                        }
                    });
                }

                grid.reconfigure(me, fields);
                me.loadPage(1, {
                    callback: function (records, operation, success) {
                        proxy.extraParams.cache = success;
                        Ext.getCmp('exportButton3').setDisabled(success === false);
                    }
                });
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
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