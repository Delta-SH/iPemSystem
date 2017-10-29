Ext.define('FsuEventModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'code', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'vendor', type: 'string' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'message', type: 'string' },
        { name: 'time', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'FsuEventModel',
    proxy: {
        type: 'ajax',
        url: '/Fsu/RequestFsuEvents',
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

var currentLayout = Ext.create('Ext.grid.Panel', {
    glyph: 0xf029,
    title: 'FSU日志信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    viewConfig: {
        forceFit: true,
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
        locked: true,
        sortable: true
    }, {
        text: '编号',
        dataIndex: 'code',
        align: 'left',
        width: 120,
        locked: true,
        sortable: true
    }, {
        text: '名称',
        dataIndex: 'name',
        align: 'left',
        width: 120,
        locked: true,
        sortable: true
    }, {
        text: '所属厂家',
        dataIndex: 'vendor',
        align: 'left',
        sortable: true
    }, {
        text: '所属区域',
        dataIndex: 'area',
        align: 'left',
        sortable: true
    }, {
        text: '所属站点',
        dataIndex: 'station',
        align: 'left',
        sortable: true
    }, {
        text: '所属机房',
        dataIndex: 'room',
        align: 'left',
        sortable: true
    }, {
        text: '日志类型',
        dataIndex: 'type',
        align: 'center',
        sortable: true
    },
    {
        text: '日志信息',
        dataIndex: 'message',
        flex: 1,
        align: 'left',
        sortable: true,
        renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
            metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
            return value;
        }
    }, {
        text: '日志时间',
        dataIndex: 'time',
        align: 'center',
        width: 150,
        sortable: true
    }],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [{
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'rangeField',
                xtype: 'RoomPicker',
                fieldLabel: '查询范围',
                emptyText: '默认全部'
            }, {
                id: 'typesField',
                xtype: 'FsuEventMultiCombo',
                width: 280,
                emptyText: '默认全部'
            }, {
                xtype: 'button',
                text: '数据查询',
                glyph: 0xf005,
                handler: function (el, e) {
                    query();
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'startField',
                xtype: 'datefield',
                fieldLabel: '开始日期',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                editable: false,
                allowBlank: false
            }, {
                id: 'endField',
                xtype: 'datefield',
                fieldLabel: '结束日期',
                labelWidth: 60,
                width: 280,
                value: new Date(),
                editable: false,
                allowBlank: false
            }, {
                id: 'exportButton',
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                disabled: true,
                handler: function (el, e) {
                    print();
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'filterField',
                xtype: 'combobox',
                fieldLabel: '筛选类型',
                labelWidth: 60,
                width: 280,
                store: Ext.create('Ext.data.Store', {
                    fields: [
                         { name: 'id', type: 'int' },
                         { name: 'text', type: 'string' }
                    ],
                    data: [
                        { "id": 1, "text": '按FSU编号' },
                        { "id": 2, "text": '按FSU名称' }
                    ]
                }),
                align: 'center',
                value: 1,
                editable: false,
                displayField: 'text',
                valueField: 'id',
            }, {
                id: 'keywordsField',
                xtype: 'textfield',
                width: 280,
                maxLength: 100,
                emptyText: '多条件请以;分隔，例: A;B;C',
            }]
        }]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var rangeField = Ext.getCmp('rangeField'),
        typesField = Ext.getCmp('typesField'),
        filterField = Ext.getCmp('filterField'),
        keywordsField = Ext.getCmp('keywordsField'),
        startField = Ext.getCmp('startField'),
        endField = Ext.getCmp('endField'),
        parent = rangeField.getValue(),
        types = typesField.getValue(),
        filter = filterField.getValue(),
        keywords = keywordsField.getRawValue(),
        startDate = startField.getRawValue(),
        endDate = endField.getRawValue();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.types = types;
    proxy.extraParams.filter = filter;
    proxy.extraParams.keywords = keywords;
    proxy.extraParams.startDate = startDate;
    proxy.extraParams.endDate = endDate;
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton').setDisabled(success === false);
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/Fsu/DownloadFsuEvents',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentLayout);
    }
});