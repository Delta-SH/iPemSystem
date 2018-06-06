var lineChart = null,
    lineOption = {
        tooltip: {
            trigger: 'item',
            axisPointer: {
                type: 'cross'
            },
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
        legend: {
            type: 'scroll',
            left: 'center',
            top: 48,
            data: []
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
            top: 80,
            left: 10,
            right: 15,
            bottom: 45,
            containLabel: true
        },
        dataZoom: [
            {
                type: 'inside',
                start: 0,
                end: 100
            },
            {
                type: 'slider',
                show: true,
                start: 0,
                end: 100
            }
        ],
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
            min: function (value) {
                return (value.min - (value.max - value.min) / 3).toFixed(2);
            }
        }],
        series: []
    };

Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'area', type: 'string' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'id', type: 'string' },
        { name: 'pack', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'interval', type: 'string' },
        { name: 'proctime', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    DownloadURL: '/Report/DownloadChart400303',
    proxy: {
        type: 'ajax',
        url: '/Report/RequestChart400303',
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
    glyph: 0xf029,
    title: '电池放电曲线',
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
            width: 60
        }, {
            text: '放电曲线',
            dataIndex: 'id',
            align: 'center',
            width: 200,
            renderer: function (value, p, record) {
                return '<a data="zdy" class="grid-link" href="javascript:void(0);">总电压</a><span class="grid-link-split">|</span><a data="zdl" class="grid-link" href="javascript:void(0);">总电流</a><span class="grid-link-split">|</span><a data="dy" class="grid-link" href="javascript:void(0);">电压</a><span class="grid-link-split">|</span><a data="wd" class="grid-link" href="javascript:void(0);">温度</a>';
            }
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
            text: '电池组名',
            dataIndex: 'name',
            align: 'left'
        },
        {
            text: '电池组号',
            dataIndex: 'pack',
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
            text: '放电时长',
            dataIndex: 'interval',
            align: 'center',
            width: 150
        }],
    listeners: {
        cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            var columns = view.getGridColumns();
            if (columns.length == 0 || columns.length <= cellIndex) return false;
            var colname = columns[cellIndex].dataIndex;
            if (colname === 'id') {
                var target = e.getTarget('a.grid-link');
                if (!Ext.isEmpty(target)) {
                    var operate = target.getAttribute("data");
                    if (!Ext.isEmpty(operate)) {
                        var id = record.get('id');
                        var pack = record.get('pack');
                        var proc = record.get('proctime');
                        var start = record.get('start');
                        var end = record.get('end');
                        var name = record.get('name');

                        if (operate === 'zdy') {
                            dochart(id, pack, proc, operate, start, end, Ext.String.format('{0}-总电压放电曲线', name), '电池组总电压(V)');
                        } else if (operate === 'zdl') {
                            dochart(id, pack, proc, operate, start, end, Ext.String.format('{0}-总电流放电曲线', name), '电池组总电流(A)');
                        } else if (operate === 'dy') {
                            dochart(id, pack, proc, operate, start, end, Ext.String.format('{0}-单体电压放电曲线', name), '电池组单体电压(V)');
                        } else if (operate === 'wd') {
                            dochart(id, pack, proc, operate, start, end, Ext.String.format('{0}-单体温度放电曲线', name), '电池组单体温度(℃)');
                        }
                    }
                }
            }
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
                        xtype: 'DevicePicker',
                        fieldLabel: '查询范围',
                        width: 448,
                        pickerWidth: 383
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

var lineWnd = Ext.create('Ext.window.Window', {
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
            lineWnd.hide();
        }
    }]
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
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

var dochart = function (id, pack, proc, action, start, end, title, yaxis) {
    if (Ext.isEmpty(id)) return false;
    if (Ext.isEmpty(pack)) return false;
    if (Ext.isEmpty(proc)) return false;
    if (Ext.isEmpty(start)) return false;
    if (Ext.isEmpty(end)) return false;

    lineOption.title.text = title;
    lineOption.title.subtext = Ext.String.format('{0} ~ {1}', start, end);
    lineOption.series = [];
    lineOption.yAxis[0].name = yaxis;
    lineChart.setOption(lineOption, true);
    lineWnd.show();

    Ext.Ajax.request({
        url: '/Report/RequestBatteryChart',
        params: { id: id, pack: pack, proc: proc, action: action },
        mask: new Ext.LoadMask(lineWnd, { msg: '正在处理...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                if (!Ext.isEmpty(lineChart)
                    && !Ext.isEmpty(data.data)
                    && Ext.isArray(data.data)) {
                    var series = [];
                    var legends = [];
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
                            showSymbol: true,
                            symbolSize: 1,
                            data: models
                        });

                        legends.push(item.name);
                    });

                    lineOption.series = series;
                    lineOption.legend.data = legends;
                    lineChart.setOption(lineOption, true);
                }
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
}

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