Ext.define('FsuModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'code', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'vendor', type: 'string' },
        { name: 'ip', type: 'string' },
        { name: 'port', type: 'int' },
        { name: 'change', type: 'string' },
        { name: 'last', type: 'string' },
        { name: 'status', type: 'string' },
        { name: 'comment', type: 'string' }
    ],
    idProperty: 'id'
});

Ext.define('FileModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'size', type: 'double' },
        { name: 'path', type: 'string' },
        { name: 'time', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'FsuModel',
    proxy: {
        type: 'ajax',
        url: '/Fsu/RequestFsu',
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

var fileStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'FileModel',
    proxy: {
        type: 'ajax',
        url: '/Fsu/RequestFtpFiles',
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
var filePagingToolbar = $$iPems.clonePagingToolbar(fileStore);

var currentLayout = Ext.create('Ext.grid.Panel', {
    glyph: 0xf029,
    title: 'FSU信息',
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
        align: 'center',
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
        text: '所属厂家',
        dataIndex: 'vendor',
        align: 'left',
        sortable: true
    }, {
        text: 'IP',
        dataIndex: 'ip',
        align: 'center',
        width: 120,
        sortable: true
    }, {
        text: '端口',
        dataIndex: 'port',
        align: 'center',
        sortable: true
    }, {
        text: '注册时间',
        dataIndex: 'change',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '离线时间',
        dataIndex: 'last',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '状态',
        dataIndex: 'status',
        align: 'center',
        sortable: true
    }, {
        text: '备注',
        dataIndex: 'comment',
        align: 'left',
        sortable: true
    }, {
        text: '日志',
        align: 'center',
        dataIndex: 'id',
        renderer: function (value, p, record) {
            if (Ext.isEmpty(value)) return Ext.emptyString;
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">查看</a>', value);
        }
    }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var columns = view.getGridColumns(),
                fieldName = columns[cellIndex].dataIndex;

            if (fieldName !== 'id')
                return;

            var fieldValue = record.get(fieldName);
            if (Ext.isEmpty(fieldValue))
                return;

            showFileDetail(fieldValue, record.get('ip'));
        }
    },
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
                width: 220,
                emptyText: '默认全部'
            }, {
                id: 'statusField',
                xtype: 'multicombo',
                fieldLabel: 'FSU状态',
                valueField: 'id',
                displayField: 'text',
                delimiter: $$iPems.Delimiter,
                queryMode: 'local',
                triggerAction: 'all',
                selectionMode: 'all',
                emptyText: '默认全部',
                forceSelection: true,
                labelWidth: 60,
                width: 220,
                store: Ext.create('Ext.data.Store', {
                    fields: [
                        { name: 'id', type: 'int' },
                        { name: 'text', type: 'string' },
                    ],
                    data: [
                        { id: 1, text: '在线' },
                        { id: 0, text: '离线' },
                    ]
                })
            }, {
                id: 'vendorField',
                xtype: 'VendorMultiCombo',
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
                id: 'filterField',
                xtype: 'combobox',
                fieldLabel: '筛选类型',
                labelWidth: 60,
                width: 220,
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
                width: 448,
                maxLength: 100,
                emptyText: '多条件请以;分隔，例: A;B;C',
            }, {
                xtype: 'button',
                text: '数据导出',
                glyph: 0xf010,
                handler: function (el, e) {
                    print();
                }
            }]
        }]
    }],
    bbar: currentPagingToolbar
});

var fileGrid = Ext.create('Ext.grid.Panel', {
    region: 'center',
    border: false,
    store: fileStore,
    bbar: filePagingToolbar,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        loadMask: true,
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
            text: '文件名称',
            dataIndex: 'name',
            flex: 1
        },
        {
            text: '文件大小',
            dataIndex: 'size',
            width: 120
        },
        {
            text: '创建时间',
            dataIndex: 'time',
            align: 'center',
            width: 150
        },
        {
            text: '详情',
            dataIndex: 'path',
            align: 'center',
            renderer: function (value, p, record) {
                if (Ext.isEmpty(value)) return Ext.emptyString;
                return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">下载</a>', value);
            }
        }
    ],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var columns = view.getGridColumns(),
                fieldName = columns[cellIndex].dataIndex;

            if (fieldName !== 'path')
                return;

            var fieldValue = record.get(fieldName);
            if (Ext.isEmpty(fieldValue))
                return;

            downloadFile(fieldValue);
        }
    }
});

var fileWnd = Ext.create('Ext.window.Window', {
    title: '日志列表',
    glyph: 0xf029,
    height: 500,
    width: 800,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    layout: 'border',
    items: [fileGrid],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '关闭',
        handler: function (el, e) {
            fileWnd.hide();
        }
    }]
});

var showFileDetail = function (id, ip) {
    if (Ext.isEmpty(id)) return false;
    fileStore.removeAll();
    fileStore.getProxy().extraParams.id = id;
    fileStore.loadPage(1);

    fileWnd.fsu = id;
    fileWnd.setTitle(Ext.String.format("日志列表[{0}]", ip || "--"));
    fileWnd.show();
};

var downloadFile = function (path) {
    if (Ext.isEmpty(path)) return false;
    if (Ext.isEmpty(fileWnd.fsu)) return false;

    $$iPems.download({
        url: '/Fsu/DownloadFtpFile',
        params: {
            fsu: fileWnd.fsu,
            path: path
        }
    });
};

var query = function () {
    var rangeField = Ext.getCmp('rangeField'),
        statusField = Ext.getCmp('statusField'),
        vendorField = Ext.getCmp('vendorField'),
        filterField = Ext.getCmp('filterField'),
        keywordsField = Ext.getCmp('keywordsField'),
        parent = rangeField.getValue(),
        status = statusField.getValue(),
        vendors = vendorField.getValue(),
        filter = filterField.getValue(),
        keywords = keywordsField.getRawValue();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.status = status;
    proxy.extraParams.vendors = vendors;
    proxy.extraParams.filter = filter;
    proxy.extraParams.keywords = keywords;
    me.loadPage(1);
};

var print = function () {
    $$iPems.download({
        url: '/Fsu/DownloadFsu',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentLayout);

        //load data
        Ext.defer(query, 500);
    }
});