(function () {

    //#region Chart
    var almChart = null,
        cpuChart = null,
        memoryChart = null,
        energybarChart = null,
        energypieChart = null,
        cuttingChart = null,
        powerChart = null,
        fsuChart = null,
        almOption = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                }
            },
            legend: {
                data: ['一级告警', '二级告警', '三级告警', '四级告警'],
                align: 'left'
            },
            grid: {
                top: 25,
                left: 5,
                right: 5,
                bottom: 5,
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
                    type: 'value'
                }
            ],
            series: [
                {
                    name: '一级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#f04b51'
                        }
                    },
                    data: [0]
                },
                {
                    name: '二级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#efa91f'
                        }
                    },
                    data: [0]
                },
                {
                    name: '三级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#f5d313'
                        }
                    },
                    data: [0]
                },
                {
                    name: '四级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#0892cd'
                        }
                    },
                    data: [0]
                }
            ]
        },
        cpuOption = {
            tooltip: {
                trigger: 'axis',
                formatter: '{b}<br/>{c}%'
            },
            grid: {
                top: 10,
                left: 0,
                right: 15,
                bottom: 0,
                containLabel: true
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                splitLine: { show: false },
                data: []
            },
            yAxis: {
                type: 'value',
                max: 100,
                min: 0
            },
            series: [
                {
                    name: 'CPU',
                    type: 'line',
                    smooth: true,
                    symbol: 'none',
                    sampling: 'average',
                    itemStyle: {
                        normal: {
                            color: '#0892cd'
                        }
                    },
                    areaStyle: { normal: {} },
                    data: []
                }
            ]
        },
        memoryOption = {
            tooltip: {
                trigger: 'axis',
                formatter: '{b}<br/>{c}%'
            },
            grid: {
                top: 10,
                left: 0,
                right: 15,
                bottom: 0,
                containLabel: true
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                splitLine: { show: false },
                data: []
            },
            yAxis: {
                type: 'value',
                max: 100,
                min: 0
            },
            series: [
                {
                    name: 'Memory',
                    type: 'line',
                    smooth: true,
                    symbol: 'none',
                    sampling: 'average',
                    itemStyle: {
                        normal: {
                            color: '#b198dc'
                        }
                    },
                    areaStyle: { normal: {} },
                    data: []
                }
            ]
        },
        energypieOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            series: [
                {
                    name: '能耗分类占比',
                    type: 'pie',
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false,
                            position: 'center'
                        },
                        emphasis: {
                            show: true,
                            textStyle: {
                                fontSize: '20',
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
                        { value: 0, name: '空调' },
                        { value: 0, name: '照明' },
                        { value: 0, name: '办公' },
                        { value: 0, name: 'IT设备' },
                        { value: 0, name: '开关电源' },
                        { value: 0, name: 'UPS' },
                        { value: 0, name: '其他' }
                    ]
                }
            ]
        },
        energybarOption = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                }
            },
            legend: {
                data: ['空调', '照明', '办公', 'IT设备', '开关电源', 'UPS', '其他'],
                align: 'left'
            },
            grid: {
                top: 30,
                left: 5,
                right: 5,
                bottom: 5,
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
                    name: '能耗',
                    axisLabel: {
                        formatter: '{value} kW·h'
                    }
                },
                {
                    type: 'value',
                    name: 'PUE',
                    interval: 1
                }
            ],
            series: [
                {
                    name: '空调',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: '照明',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: '办公',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: 'IT设备',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: '开关电源',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: 'UPS',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: '其他',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: 'PUE',
                    type: 'line',
                    smooth: true,
                    yAxisIndex: 1,
                    data: [0]
                },
                {
                    name: '能效',
                    type: 'line',
                    smooth: true,
                    yAxisIndex: 1,
                    data: [0]
                }
            ]
        },
        cuttingOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: 'bottom',
                data: ['正常站点', '停电站点']
            },
            series: [
                {
                    name: '站点停电率',
                    type: 'pie',
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false,
                            position: 'center'
                        },
                        emphasis: {
                            show: true,
                            textStyle: {
                                fontSize: '20',
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
                        {
                            value: 0, name: '正常站点', itemStyle: {
                                normal: {
                                    color: '#48ac2e'
                                }
                            }
                        },
                        {
                            value: 0, name: '停电站点', itemStyle: {
                                normal: {
                                    color: '#c12e34'
                                }
                            }
                        }
                    ]
                }
            ]
        },
        powerOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: 'bottom',
                data: ['正常站点', '发电站点']
            },
            series: [
                {
                    name: '站点发电率',
                    type: 'pie',
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false,
                            position: 'center'
                        },
                        emphasis: {
                            show: true,
                            textStyle: {
                                fontSize: '20',
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
                        {
                            value: 0, name: '正常站点', itemStyle: {
                                normal: {
                                    color: '#48ac2e'
                                }
                            }
                        },
                        {
                            value: 0, name: '发电站点', itemStyle: {
                                normal: {
                                    color: '#c12e34'
                                }
                            }
                        }
                    ]
                }
            ]
        },
        fsuOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: 'bottom',
                data: ['正常FSU', '离线FSU']
            },
            series: [
                {
                    name: 'Fsu离线率',
                    type: 'pie',
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false,
                            position: 'center'
                        },
                        emphasis: {
                            show: true,
                            textStyle: {
                                fontSize: '20',
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
                        {
                            value: 0,
                            name: '正常FSU',
                            itemStyle: {
                                normal: {
                                    color: '#48ac2e'
                                }
                            }
                        },
                        {
                            value: 0,
                            name: '离线FSU',
                            itemStyle: {
                                normal: {
                                    color: '#c12e34'
                                }
                            }
                        }
                    ]
                }
            ]
        };
    //#endregion

    //#region Model
    Ext.define('CuttingModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'station', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'interval', type: 'string' }
        ],
        idProperty: 'index'
    });

    Ext.define('PowerModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'station', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'interval', type: 'string' }
        ],
        idProperty: 'index'
    });

    Ext.define('FsuModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'room', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'vendor', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'interval', type: 'string' }
        ],
        idProperty: 'index'
    });
    //#endregion

    //#region Store
    var cuttingStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'CuttingModel',
        proxy: {
            type: 'ajax',
            url: '/Home/RequestHomeCutting',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length == 2
                        && cuttingChart !== null) {
                        if (!Ext.isEmpty(data.chart[0])) {
                            cuttingOption.series[0].data[0].value = data.chart[0].value;
                        }

                        if (!Ext.isEmpty(data.chart[1])) {
                            cuttingOption.series[0].data[1].value = data.chart[1].value;
                        }

                        cuttingChart.setOption(cuttingOption);
                    }

                    $$iPems.Tasks.homeTasks.cuttingTask.fireOnStart = false;
                    $$iPems.Tasks.homeTasks.cuttingTask.restart();
                }
            }
        }
    });
    var powerStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PowerModel',
        proxy: {
            type: 'ajax',
            url: '/Home/RequestHomePower',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length == 2
                        && powerChart !== null) {
                        if (!Ext.isEmpty(data.chart[0])) {
                            powerOption.series[0].data[0].value = data.chart[0].value;
                        }

                        if (!Ext.isEmpty(data.chart[1])) {
                            powerOption.series[0].data[1].value = data.chart[1].value;
                        }

                        powerChart.setOption(powerOption);
                    }

                    $$iPems.Tasks.homeTasks.powerTask.fireOnStart = false;
                    $$iPems.Tasks.homeTasks.powerTask.restart();
                }
            }
        }
    });
    var fsuStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'FsuModel',
        proxy: {
            type: 'ajax',
            url: '/Home/RequestHomeFsu',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length == 2
                        && fsuChart !== null) {
                        if (!Ext.isEmpty(data.chart[0])) {
                            fsuOption.series[0].data[0].value = data.chart[0].value;
                        }

                        if (!Ext.isEmpty(data.chart[1])) {
                            fsuOption.series[0].data[1].value = data.chart[1].value;
                        }

                        fsuChart.setOption(fsuOption);
                    }

                    $$iPems.Tasks.homeTasks.offTask.fireOnStart = false;
                    $$iPems.Tasks.homeTasks.offTask.restart();
                }
            }
        }
    });
    var cuttingPagingToolbar = $$iPems.clonePagingToolbar(cuttingStore);
    var powerPagingToolbar = $$iPems.clonePagingToolbar(powerStore);
    var fsuPagingToolbar = $$iPems.clonePagingToolbar(fsuStore);
    //#endregion

    //#region Layout
    var currentLayout = Ext.create('Ext.panel.Panel', {
        region: 'center',
        header: false,
        border: false,
        overflowY: 'auto',
        defaults: {
            margin: '0 22 0 0'
        },
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        items: [{
            xtype: 'container',
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'alm-0-container'
                }, {
                    xtype: 'component',
                    width: 10
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'alm-1-container'
                }, {
                    xtype: 'component',
                    width: 10
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'alm-2-container'
                }, {
                    xtype: 'component',
                    width: 10
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'alm-3-container'
                }, {
                    xtype: 'component',
                    width: 10
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'alm-4-container'
                }
            ]
        }, {
            xtype: 'component',
            height: 10
        }, {
            xtype: 'container',
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'panel',
                    glyph: 0xf031,
                    title: '告警分布图',
                    collapsible: true,
                    collapseFirst: false,
                    flex: 3,
                    contentEl: 'alm-chart'
                },
                {
                    xtype: 'component',
                    width: 5
                },
                {
                    xtype: 'container',
                    flex: 1,
                    layout: {
                        type: 'vbox',
                        align: 'stretch'
                    },
                    items: [
                        {
                            xtype: 'panel',
                            glyph: 0xf038,
                            title: 'CPU利用率',
                            collapsible: true,
                            collapseFirst: false,
                            flex: 1,
                            contentEl: 'cpu-chart'
                        }, {
                            xtype: 'component',
                            height: 5
                        }, {
                            xtype: 'panel',
                            glyph: 0xf038,
                            title: '内存利用率',
                            collapsible: true,
                            collapseFirst: false,
                            flex: 1,
                            contentEl: 'memory-chart'
                        }
                    ]
                }
            ]
        }, {
            xtype: 'component',
            height: 10
        }, {
            xtype: 'panel',
            glyph: 0xf031,
            title: '能耗分布图(kW·h)',
            collapsible: true,
            collapseFirst: false,
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'container',
                    flex: 3,
                    contentEl: 'energy-bar'
                }, {
                    xtype: 'component',
                    height: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'energy-pie'
                }
            ]
        }, {
            xtype: 'component',
            height: 10
        }, {
            xtype: 'panel',
            glyph: 0xf030,
            title: '站点停电列表',
            collapsible: true,
            collapseFirst: false,
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            tools: [{
                type: 'print',
                tooltip: '数据导出',
                handler: function (event, toolEl, panelHeader) {
                    $$iPems.download({
                        url: '/Home/DownloadHomeCutting',
                        params: cuttingStore.proxy.extraParams
                    });
                }
            }],
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'cutting-pie'
                }, {
                    xtype: 'component',
                    height: 5
                }, {
                    xtype: 'grid',
                    flex: 3,
                    store: cuttingStore,
                    border: false,
                    style: {
                        'border-left': '1px solid #c0c0c0'
                    },
                    viewConfig: {
                        loadMask: false,
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
                        sortable: true
                    }, {
                        text: '站点名称',
                        dataIndex: 'station',
                        align: 'left',
                        flex: 1,
                        sortable: true
                    }, {
                        text: '停电时间',
                        dataIndex: 'time',
                        width: 150,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '停电时长',
                        dataIndex: 'interval',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }],
                    bbar: cuttingPagingToolbar
                }
            ]
        }, {
            xtype: 'component',
            height: 10
        },
        {
            xtype: 'panel',
            glyph: 0xf030,
            title: '油机发电列表',
            collapsible: true,
            collapseFirst: false,
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            tools: [{
                type: 'print',
                tooltip: '数据导出',
                handler: function (event, toolEl, panelHeader) {
                    $$iPems.download({
                        url: '/Home/DownloadHomePower',
                        params: powerStore.proxy.extraParams
                    });
                }
            }],
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'power-pie'
                }, {
                    xtype: 'component',
                    height: 5
                }, {
                    xtype: 'grid',
                    flex: 3,
                    store: powerStore,
                    border: false,
                    style: {
                        'border-left': '1px solid #c0c0c0'
                    },
                    viewConfig: {
                        loadMask: false,
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
                        sortable: true
                    }, {
                        text: '所属站点',
                        dataIndex: 'station',
                        align: 'left',
                        flex: 1,
                        sortable: true
                    }, {
                        text: '设备名称',
                        dataIndex: 'device',
                        align: 'left',
                        width: 150,
                        sortable: true
                    }, {
                        text: '发电时间',
                        dataIndex: 'time',
                        width: 150,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '发电时长',
                        dataIndex: 'interval',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }],
                    bbar: powerPagingToolbar
                }
            ]
        },
        {
            xtype: 'component',
            height: 10
        },
        {
            xtype: 'panel',
            glyph: 0xf030,
            title: 'Fsu离线列表',
            collapsible: true,
            collapseFirst: false,
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            tools: [{
                type: 'print',
                tooltip: '数据导出',
                handler: function (event, toolEl, panelHeader) {
                    $$iPems.download({
                        url: '/Home/DownloadHomeFsu',
                        params: fsuStore.proxy.extraParams
                    });
                }
            }],
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'fsu-pie'
                }, {
                    xtype: 'component',
                    height: 5
                }, {
                    xtype: 'grid',
                    flex: 3,
                    store: fsuStore,
                    border: false,
                    style: {
                        'border-left': '1px solid #c0c0c0'
                    },
                    viewConfig: {
                        loadMask: false,
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
                        sortable: true
                    }, {
                        text: '所属机房',
                        dataIndex: 'room',
                        align: 'left',
                        flex: 1,
                        sortable: true
                    }, {
                        text: 'Fsu名称',
                        dataIndex: 'name',
                        align: 'left',
                        width: 120,
                        sortable: true
                    }, {
                        text: '所属厂家',
                        dataIndex: 'vendor',
                        align: 'left',
                        width: 120,
                        sortable: true
                    }, {
                        text: '离线时间',
                        dataIndex: 'time',
                        width: 150,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '离线时长',
                        dataIndex: 'interval',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }],
                    bbar: fsuPagingToolbar
                }
            ]
        }],
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                if (!Ext.isEmpty(almChart)) almChart.resize();
                if (!Ext.isEmpty(cpuChart)) cpuChart.resize();
                if (!Ext.isEmpty(memoryChart)) memoryChart.resize();
                if (!Ext.isEmpty(energybarChart)) energybarChart.resize();
                if (!Ext.isEmpty(energypieChart)) energypieChart.resize();
                if (!Ext.isEmpty(cuttingChart)) cuttingChart.resize();
                if (!Ext.isEmpty(powerChart)) powerChart.resize();
                if (!Ext.isEmpty(fsuChart)) fsuChart.resize();
            }
        }
    });
    //#endregion

    //#region Ready
    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        almChart = echarts.init(document.getElementById("alm-chart"), 'shine');
        cpuChart = echarts.init(document.getElementById("cpu-chart"), 'shine');
        memoryChart = echarts.init(document.getElementById("memory-chart"), 'shine');
        energybarChart = echarts.init(document.getElementById("energy-bar"), 'shine');
        energypieChart = echarts.init(document.getElementById("energy-pie"), 'shine');
        cuttingChart = echarts.init(document.getElementById("cutting-pie"), 'shine');
        powerChart = echarts.init(document.getElementById("power-pie"), 'shine');
        fsuChart = echarts.init(document.getElementById("fsu-pie"), 'shine');

        //init charts
        almChart.setOption(almOption);
        cpuChart.setOption(cpuOption);
        memoryChart.setOption(memoryOption);
        energybarChart.setOption(energybarOption);
        energypieChart.setOption(energypieOption);
        cuttingChart.setOption(cuttingOption);
        powerChart.setOption(powerOption);
        fsuChart.setOption(fsuOption);

        $$iPems.Tasks.homeTasks.almTask.run = function () {
            Ext.Ajax.request({
                url: '/Home/RequestHomeAlm',
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success){
                        if (!Ext.isEmpty(data.data)) {
                            var me = data.data,
                                alarms = me.alarms;

                            $$iPems.animateNumber('alm-0-count', me.total);
                            $$iPems.animateNumber('alm-1-count', me.total1);
                            $$iPems.animateNumber('alm-2-count', me.total2);
                            $$iPems.animateNumber('alm-3-count', me.total3);
                            $$iPems.animateNumber('alm-4-count', me.total4);

                            if (!Ext.isEmpty(alarms) && Ext.isArray(alarms)) {
                                almOption.xAxis[0].data = [];
                                almOption.series[0].data = [];
                                almOption.series[1].data = [];
                                almOption.series[2].data = [];
                                almOption.series[3].data = [];
                                Ext.Array.each(alarms, function (item, index) {
                                    almOption.xAxis[0].data.push(item.name);
                                    almOption.series[0].data.push(item.level1);
                                    almOption.series[1].data.push(item.level2);
                                    almOption.series[2].data.push(item.level3);
                                    almOption.series[3].data.push(item.level4);
                                });
                                almChart.setOption(almOption, true);
                            }
                        }

                        $$iPems.Tasks.homeTasks.almTask.fireOnStart = false;
                        $$iPems.Tasks.homeTasks.almTask.restart();
                    } else {
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                }
            });
        };
        $$iPems.Tasks.homeTasks.almTask.start();

        $$iPems.Tasks.homeTasks.svrTask.run = function () {
            Ext.Ajax.request({
                url: '/Home/RequestHomeSvr',
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        if (!Ext.isEmpty(data.data)) {
                            var me = data.data;
                            if (cpuOption.xAxis.data.length > 60)
                                cpuOption.xAxis.data.shift();

                            if (cpuOption.series[0].data.length > 60)
                                cpuOption.series[0].data.shift();

                            if (memoryOption.xAxis.data.length > 60)
                                memoryOption.xAxis.data.shift();

                            if (memoryOption.series[0].data.length > 60)
                                memoryOption.series[0].data.shift();

                            cpuOption.xAxis.data.push(me.time);
                            cpuOption.series[0].data.push(me.cpu);
                            memoryOption.xAxis.data.push(me.time);
                            memoryOption.series[0].data.push(me.memory);

                            cpuChart.setOption(cpuOption);
                            memoryChart.setOption(memoryOption);
                        }

                        $$iPems.Tasks.homeTasks.svrTask.fireOnStart = false;
                        $$iPems.Tasks.homeTasks.svrTask.restart();
                    } else {
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                }
            });
        };
        $$iPems.Tasks.homeTasks.svrTask.start();

        $$iPems.Tasks.homeTasks.energyTask.run = function () {
            Ext.Ajax.request({
                url: '/Home/RequestHomeEnergies',
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        if (!Ext.isEmpty(data.data) && Ext.isArray(data.data)) {
                            var xaxis = [], kt = [], zm = [], bg = [], it = [],dy = [], ups = [], qt = [], pue = [], eer = [],ttkt = 0, ttzm = 0, ttbg = 0, ttit = 0, ttdy = 0,ttups = 0, ttqt = 0;
                            Ext.Array.each(data.data, function (item, index) {
                                xaxis.push(item.name);
                                kt.push(item.kt);
                                ttkt += item.kt;

                                zm.push(item.zm);
                                ttzm += item.zm;

                                bg.push(item.bg);
                                ttbg += item.bg;

                                it.push(item.it);
                                ttit += item.it;

                                dy.push(item.dy);
                                ttdy += item.dy;

                                ups.push(item.ups);
                                ttups += item.ups;

                                qt.push(item.qt);
                                ttqt += item.qt;

                                pue.push(item.pue);
                                eer.push(item.eer);
                            });

                            energybarOption.xAxis[0].data = xaxis;
                            energybarOption.series[0].data = kt;
                            energybarOption.series[1].data = zm;
                            energybarOption.series[2].data = bg;
                            energybarOption.series[3].data = it;
                            energybarOption.series[4].data = dy;
                            energybarOption.series[5].data = ups;
                            energybarOption.series[6].data = qt;
                            energybarOption.series[7].data = pue;
                            energybarOption.series[8].data = eer;
                            energybarChart.setOption(energybarOption, true);

                            energypieOption.series[0].data[0].value = ttkt;
                            energypieOption.series[0].data[1].value = ttzm;
                            energypieOption.series[0].data[2].value = ttbg;
                            energypieOption.series[0].data[3].value = ttit;
                            energypieOption.series[0].data[4].value = ttdy;
                            energypieOption.series[0].data[5].value = ttups;
                            energypieOption.series[0].data[6].value = ttqt;
                            energypieChart.setOption(energypieOption, true);
                        }

                        $$iPems.Tasks.homeTasks.energyTask.fireOnStart = false;
                        $$iPems.Tasks.homeTasks.energyTask.restart();
                    } else {
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                }
            });
        };
        $$iPems.Tasks.homeTasks.energyTask.start();

        $$iPems.Tasks.homeTasks.cuttingTask.run = function () {
            cuttingPagingToolbar.doRefresh();
        };
        $$iPems.Tasks.homeTasks.cuttingTask.start();

        $$iPems.Tasks.homeTasks.powerTask.run = function () {
            powerPagingToolbar.doRefresh();
        };
        $$iPems.Tasks.homeTasks.powerTask.start();

        $$iPems.Tasks.homeTasks.offTask.run = function () {
            fsuPagingToolbar.doRefresh();
        };
        $$iPems.Tasks.homeTasks.offTask.start();
    });
    //#endregion

})();