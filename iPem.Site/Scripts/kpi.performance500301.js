var now = new Date(),
    pieChart = null,
    barChart = null,
    pieOption = {
        tooltip: {
            trigger: 'item',
            formatter: "{b} <br/>{a}: {c} ({d}%)"
        },
        legend: {
            type: 'scroll',
            orient: 'vertical',
            left: 'left',
            top: 'middle',
            data: ['空调', '照明', '办公', 'IT设备', '开关电源', 'UPS', '其他']
        },
        series: [
            {
                name: '能耗(占比)',
                type: 'pie',
                radius: ['45%', '85%'],
                center: ['60%', '50%'],
                avoidLabelOverlap: false,
                label: {
                    normal: {
                        show: false,
                        position: 'center'
                    },
                    emphasis: {
                        show: true,
                        textStyle: {
                            fontSize: '13',
                            fontWeight: 'bold'
                        }
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: [
                    { name: '空调', value: 0 },
                    { name: '照明', value: 0 },
                    { name: '办公', value: 0 },
                    { name: 'IT设备', value: 0 },
                    { name: '开关电源', value: 0 },
                    { name: 'UPS', value: 0 },
                    { name: '其他', value: 0 }
                ]
            }
        ]
    },
    barOption = {
        tooltip: {
            trigger: 'item',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            top: 15,
            left: 0,
            right: 5,
            bottom: 0,
            containLabel: true
        },
        xAxis: [
            {
                type: 'category',
                data: ['无数据'],
                splitLine: { show: false }
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    formatter: '{value} kW·h'
                }
            }
        ],
        series: [
            {
                name: '空调',
                type: 'bar',
                stack: 'one',
                data: [0]
            },
            {
                name: '照明',
                type: 'bar',
                stack: 'one',
                data: [0]
            },
            {
                name: '办公',
                type: 'bar',
                stack: 'one',
                data: [0]
            },
            {
                name: 'IT设备',
                type: 'bar',
                stack: 'one',
                data: [0]
            },
            {
                name: '开关电源',
                type: 'bar',
                stack: 'one',
                data: [0]
            },
            {
                name: 'UPS',
                type: 'bar',
                stack: 'one',
                data: [0]
            },
            {
                name: '其他',
                type: 'bar',
                stack: 'one',
                data: [0]
            }
        ]
    };

Ext.define('ReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'kt', type: 'float' },
        { name: 'zm', type: 'float' },
        { name: 'bg', type: 'float' },
        { name: 'it', type: 'float' },
        { name: 'dy', type: 'float' },
        { name: 'ups', type: 'float' },
        { name: 'qt', type: 'float' },
        { name: 'tt', type: 'float' },
        { name: 'ktrate', type: 'string' },
        { name: 'zmrate', type: 'string' },
        { name: 'bgrate', type: 'string' },
        { name: 'itrate', type: 'string' },
        { name: 'dyrate', type: 'string' },
        { name: 'upsrate', type: 'string' },
        { name: 'qtrate', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ReportModel',
    proxy: {
        type: 'ajax',
        url: '/KPI/Request500301',
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
    },
    listeners: {
        load: function (me, records, successful) {
            if (successful && pieChart && barChart) {
                chart(me.proxy.reader.jsonData);
            }
        }
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var currentLayout = Ext.create('Ext.panel.Panel', {
    id: 'currentLayout',
    region: 'center',
    border: false,
    bodyCls: 'x-border-body-panel',
    layout: {
        type: 'vbox',
        align: 'stretch',
        pack: 'start'
    },
    items: [{
        xtype: 'panel',
        glyph: 0xf030,
        title: '分类占比',
        collapsible: true,
        collapseFirst: false,
        margin: '5 0 0 0',
        flex: 1,
        layout: {
            type: 'hbox',
            align: 'stretch',
            pack: 'start'
        },
        items: [
            {
                xtype: 'container',
                flex: 1,
                contentEl: 'pie-chart'
            }, {
                xtype: 'container',
                flex: 2,
                contentEl: 'bar-chart'
            }
        ],
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                var pieContainer = Ext.get('pie-chart'),
                    barContainer = Ext.get('bar-chart');

                pieContainer.setHeight(height - 40);
                barContainer.setHeight(height - 40);
                if (pieChart) pieChart.resize();
                if (barChart) barChart.resize();
            }
        }
    }, {
        xtype: 'grid',
        glyph: 0xf029,
        title: '能耗信息',
        collapsible: true,
        collapseFirst: false,
        margin: '5 0 0 0',
        flex: 1,
        store: currentStore,
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [{
            text: '序号',
            dataIndex: 'index',
            width: 60,
            align: 'left',
            sortable: true
        }, {
            text: '名称',
            dataIndex: 'name',
            align: 'left',
            width: 200,
            sortable: true
        }, {
            text: '空调(kW·h)',
            dataIndex: 'kt',
            align: 'left',
            sortable: true
        }, {
            text: '照明(kW·h)',
            dataIndex: 'zm',
            align: 'left',
            sortable: true
        }, {
            text: '办公(kW·h)',
            dataIndex: 'bg',
            align: 'left',
            sortable: true
        }, {
            text: 'IT设备(kW·h)',
            dataIndex: 'it',
            align: 'left',
            sortable: true
        }, {
            text: '开关电源(kW·h)',
            dataIndex: 'dy',
            align: 'left',
            sortable: true
        }, {
            text: 'UPS(kW·h)',
            dataIndex: 'ups',
            align: 'left',
            sortable: true
        }, {
            text: '其他(kW·h)',
            dataIndex: 'qt',
            align: 'left',
            sortable: true
        }, {
            text: '总计(kW·h)',
            dataIndex: 'tt',
            align: 'left',
            sortable: true
        }, {
            text: '空调占比',
            dataIndex: 'ktrate',
            align: 'left',
            sortable: true
        }, {
            text: '照明占比',
            dataIndex: 'zmrate',
            align: 'left',
            sortable: true
        }, {
            text: '办公占比',
            dataIndex: 'bgrate',
            align: 'left',
            sortable: true
        }, {
            text: 'IT设备占比',
            dataIndex: 'itrate',
            align: 'left',
            sortable: true
        }, {
            text: '开关电源占比',
            dataIndex: 'dyrate',
            align: 'left',
            sortable: true
        }, {
            text: 'UPS占比',
            dataIndex: 'upsrate',
            align: 'left',
            sortable: true
        }, {
            text: '其他占比',
            dataIndex: 'qtrate',
            align: 'left',
            sortable: true
        }],
        bbar: currentPagingToolbar,
    }],
    dockedItems: [{
        xtype: 'panel',
        glyph: 0xf034,
        title: '筛选条件',
        collapsible: true,
        collapsed: false,
        dock: 'top',
        items: [{
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'rangePicker',
                xtype: 'AreaPicker',
                selectAll: true,
                allowBlank: false,
                emptyText: '请选择查询范围...',
                fieldLabel: '查询范围',
                width: 280,
            }, {
                id: 'sizeCombo',
                xtype: 'combobox',
                fieldLabel: '统计粒度',
                displayField: 'text',
                valueField: 'id',
                typeAhead: true,
                queryMode: 'local',
                triggerAction: 'all',
                selectOnFocus: true,
                forceSelection: true,
                labelWidth: 60,
                width: 280,
                value: $$iPems.SSH.Station,
                store: Ext.create('Ext.data.Store', {
                    fields: ['id', 'text'],
                    data: [
                        { id: $$iPems.SSH.Station, text: '站点' },
                        { id: $$iPems.SSH.Room, text: '机房' },
                    ]
                })
            }, {
                xtype: 'button',
                glyph: 0xf005,
                text: '数据查询',
                handler: function (me, event) {
                    query();
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [{
                id: 'startField',
                xtype: 'datefield',
                fieldLabel: '开始时间',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.MONTH, -1),
                editable: false,
                allowBlank: false
            }, {
                id: 'endField',
                xtype: 'datefield',
                fieldLabel: '结束时间',
                labelWidth: 60,
                width: 280,
                value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.SECOND, -1),
                editable: false,
                allowBlank: false
            }, {
                id: 'exportButton',
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                disabled: true,
                handler: function (me, event) {
                    print();
                }
            }]
        }]
    }]
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        size = Ext.getCmp('sizeCombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.size = size.getValue();
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

var print = function () {
    $$iPems.download({
        url: '/KPI/Download500301',
        params: currentStore.getProxy().extraParams
    });
};

var chart = function (data) {
    var xaxis = [], kt = [], zm = [], bg = [], it = [], dy = [], ups = [], qt = [], ttkt = 0, ttzm = 0, ttbg = 0, ttit = 0, ttdy = 0, ttups = 0, ttqt = 0;
    if (!Ext.isEmpty(data) && Ext.isArray(data.chart)) {
        Ext.Array.each(data.chart, function (item) {
            xaxis.push(item.name);
            kt.push(item.kt);
            zm.push(item.zm);
            bg.push(item.bg);
            it.push(item.it);
            dy.push(item.dy);
            ups.push(item.ups);
            qt.push(item.qt);

            ttkt += item.kt;
            ttzm += item.zm;
            ttbg += item.bg;
            ttit += item.it;
            ttdy += item.dy;
            ttups += item.ups;
            ttqt += item.qt;
        });
    }

    pieOption.series[0].data[0].value = ttkt;
    pieOption.series[0].data[1].value = ttzm;
    pieOption.series[0].data[2].value = ttbg;
    pieOption.series[0].data[3].value = ttit;
    pieOption.series[0].data[4].value = ttdy;
    pieOption.series[0].data[5].value = ttups;
    pieOption.series[0].data[6].value = ttqt;
    pieChart.setOption(pieOption);

    barOption.xAxis[0].data = xaxis;
    barOption.series[0].data = kt;
    barOption.series[1].data = zm;
    barOption.series[2].data = bg;
    barOption.series[3].data = it;
    barOption.series[4].data = dy;
    barOption.series[5].data = ups;
    barOption.series[6].data = qt;
    barChart.setOption(barOption);
}

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentLayout);
    }
});

Ext.onReady(function () {
    pieChart = echarts.init(document.getElementById("pie-chart"), 'shine');
    barChart = echarts.init(document.getElementById("bar-chart"), 'shine');

    //init charts
    pieChart.setOption(pieOption);
    barChart.setOption(barOption);
});