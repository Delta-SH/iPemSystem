var lineChart = null,
    lineOption = {
        tooltip: {
            trigger: 'axis',
            formatter: function (params) {
                if (lineOption.series.length > 0) {
                    if (!Ext.isArray(params))
                        params = [params];

                    var tips = [];
                    Ext.Array.each(params, function (item, index) {
                        tips.push(Ext.String.format('<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:{0}"></span>{1}<br/>电池电压：{2} {3}<br/>放电时间：{4} min', item.color, item.seriesName, item.value[1], item.data.unit, item.value[0]));
                    });

                    return tips.join('<br/>');
                }

                return '无数据';
            }
        },
        title: {
            text: '蓄电池组放电曲线',
            subtext: '2017-01-01 00:00:00 ~ 2017-12-31 23:59:59',
            left: 'center',
            textStyle: {
                fontSize: 14
            }
        },
        grid: {
            top: 45,
            left: 10,
            right: 15,
            bottom: 10,
            containLabel: true
        },
        xAxis: [{
            name: '放电时间(min)',
            nameLocation: 'middle',
            nameGap: -15,
            type: 'value',
            splitLine: { show: false }
        }],
        yAxis: [{
            name: '电池电压(V)',
            nameLocation: 'middle',
            nameGap: -18,
            type: 'value',

        }],
        series: []
    };

Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'count', type: 'int' },
        { name: 'interval', type: 'string' },
        { name: 'stationid', type: 'string' }
    ],
    idProperty: 'index'
});

Ext.define('DetailModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'device', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'interval', type: 'string' },
        { name: 'deviceid', type: 'string' },
        { name: 'proctime', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    DownloadURL: '/Report/DownloadHistory400211',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistory400211',
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

var detailStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'DetailModel',
    DownloadURL: '/Report/DownloadHistoryDetail400211',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestHistoryDetail400211',
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
var detailPagingToolbar = $$iPems.clonePagingToolbar(detailStore);

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf029,
    title: '放电次数统计',
    region: 'center',
    store: currentStore,
    bbar: currentPagingToolbar,
    selType: 'cellmodel',
    forceFit: false,
    viewConfig: {
        forceFit: true,
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
        text: '站点类型',
        dataIndex: 'type',
        align: 'left',
        width: 150,
        sortable: true
    }, {
        text: '放电次数',
        dataIndex: 'count',
        width: 150,
        align: 'center',
        renderer: function (value, p, record) {
            return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', record.get('stationid'), value);
        }
    }, {
        text: '放电时长',
        dataIndex: 'interval',
        align: 'center',
        width: 150,
        sortable: true
    }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var elements = Ext.fly(td).select('a.grid-link');
            if (elements.getCount() == 0) return false;
            detail(elements.first().getAttribute('data'));
        }
    },
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
                        xtype: 'AreaPicker',
                        fieldLabel: '查询范围',
                        width: 220
                    },
                    {
                        id: 'station-type-multicombo',
                        xtype: 'StationTypeMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query();
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
                        id: 'exportButton',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (me, event) {
                            print(currentStore);
                        }
                    }
                ]
            }
        ]
    }]
});

var detailGrid = Ext.create('Ext.grid.Panel', {
    region: 'center',
    border: false,
    store: detailStore,
    bbar: detailPagingToolbar,
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
            align: 'left'
        },
        {
            text: '所属站点',
            dataIndex: 'station',
            align: 'left'
        },
        {
            text: '所属机房',
            dataIndex: 'room',
            align: 'left'
        },
        {
            text: '所属设备',
            dataIndex: 'device',
            align: 'left'
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
            text: '时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        }, {
            text: '曲线',
            dataIndex: 'deviceid',
            align: 'center',
            renderer: function (value, p, record) {
                return Ext.String.format('<a data1="{0}" data2="{1}" class="grid-link" href="javascript:void(0);">查看</a>', value, record.get('proctime'));
            }
        }
    ],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var elements = Ext.fly(td).select('a.grid-link');
            if (elements.getCount() == 0) return false;
            var first = elements.first();
            ddetail(first.getAttribute('data1'), first.getAttribute('data2'), record.get('device'), record.get('start'), record.get('end'));
        }
    }
});

var detailWnd = Ext.create('Ext.window.Window', {
    title: '电池放电详情',
    glyph: 0xf029,
    height: 600,
    width: 800,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    layout: 'border',
    items: [detailGrid],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '导出',
        handler: function (el, e) {
            print(detailStore);
        }
    }, {
        xtype: 'button',
        text: '关闭',
        handler: function (el, e) {
            detailWnd.hide();
        }
    }]
});

var ddetailWnd = Ext.create('Ext.window.Window', {
    title: '电池放电曲线',
    glyph: 0xf031,
    height: 600,
    width: 800,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    layout: 'border',
    items: [{
        xtype: 'panel',
        region: 'center',
        border: false,
        contentEl: 'line-chart',
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                Ext.get('line-chart').setHeight(height);
                if (lineChart) lineChart.resize();
            }
        }
    }],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '关闭',
        handler: function (el, e) {
            ddetailWnd.hide();
        }
    }]
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        types = Ext.getCmp('station-type-multicombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getSelectedValues();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton').setDisabled(success === false);
        }
    });
};

var print = function (store) {
    $$iPems.download({
        url: store.DownloadURL,
        params: store.getProxy().extraParams
    });
};

var detail = function (station) {
    if (Ext.isEmpty(station)) return false;

    var proxy = detailStore.getProxy();
    proxy.extraParams.station = station;

    detailStore.removeAll();
    detailStore.loadPage(1);
    detailWnd.show();
};

var ddetail = function (device, proc, title, start, end) {
    if (Ext.isEmpty(device)) return false;
    if (Ext.isEmpty(proc)) return false;

    lineOption.title.text = Ext.String.format('{0}放电曲线', title);
    lineOption.title.subtext = Ext.String.format('{0} ~ {1}', start, end);
    lineOption.series = [];
    lineChart.setOption(lineOption, true);
    ddetailWnd.show();

    Ext.Ajax.request({
        url: '/Report/RequestChart400211',
        params: { device: device, proctime: proc },
        mask: new Ext.LoadMask(ddetailWnd, { msg: '正在处理...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                if (!Ext.isEmpty(lineChart)
                    && !Ext.isEmpty(data.data)
                    && Ext.isArray(data.data)) {
                    var series = [];
                    Ext.Array.each(data.data, function (item, index) {
                        var models = [];
                        Ext.Array.each(item.models, function (model) {
                            models.push({
                                value: [parseFloat(model.name), model.value],
                                unit: model.comment
                            });
                        });

                        series.push({
                            name: item.name,
                            type: 'line',
                            showSymbol: false,
                            data: models
                        });
                    });

                    lineOption.series = series;
                    lineChart.setOption(lineOption, true);
                }
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});

Ext.onReady(function () {
    lineChart = echarts.init(document.getElementById("line-chart"), 'shine');
    lineChart.setOption(lineOption);
});