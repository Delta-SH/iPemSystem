Ext.define('ParamModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'device', type: 'string' },
        { name: 'point', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'current', type: 'string' },
        { name: 'normal', type: 'string' },
        { name: 'diff', type: 'boolean' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ParamModel',
    proxy: {
        type: 'ajax',
        url: '/Maintenance/RequestPointParams',
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
    title: '信号参数信息',
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
        preserveScrollOnRefresh: true,
        getRowClass: function (record, rowIndex, rowParams, store) {
            return record.get("diff") === true ? 'alm-level1' : '';
        }
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
        text: '所属设备',
        dataIndex: 'device',
        align: 'left',
        sortable: true
    }, {
        text: '信号名称',
        dataIndex: 'point',
        align: 'left',
        sortable: true
    }, {
        text: '参数类型',
        dataIndex: 'type',
        align: 'center',
        sortable: true
    }, {
        text: '当前参数',
        dataIndex: 'current',
        align: 'left',
        tdCls: 'x-level-cell',
        width: 150
    }, {
        text: '标准参数',
        dataIndex: 'normal',
        align: 'left',
        width: 150
    }],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [{
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'rangePicker',
                xtype: 'DevicePicker',
                fieldLabel: '查询范围',
                emptyText: '默认全部',
                labelWidth: 60,
                width: 280
            }, {
                id: 'point-param-combo',
                xtype: 'PointParamCombo',
                allowBlank: false,
                width: 280
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
                id: 'point-multipicker',
                xtype: 'PointMultiPicker',
                emptyText: '默认全部',
                width: 280
            }, {
                id: 'point-type-multicombo',
                xtype: 'PointTypeMultiCombo',
                emptyText: '默认全部',
                width: 280
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
        }]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var parent = Ext.getCmp('rangePicker').getValue(),
        pmtype = Ext.getCmp('point-param-combo').getValue(),
        points = Ext.getCmp('point-multipicker').getValue(),
        types = Ext.getCmp('point-type-multicombo').getSelectedValues();

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = parent;
    proxy.extraParams.pmtype = pmtype;
    proxy.extraParams.points = points;
    proxy.extraParams.types = types;
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
        url: '/Maintenance/DownloadPointParams',
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