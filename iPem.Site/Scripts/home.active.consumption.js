//#region Global
var currentNode = null,
    inited = true;
//#endregion

//#region Model
Ext.define('ConsumptionModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'station', type: 'string' },
        { name: 'room', type: 'string' },
        { name: 'device', type: 'string' },
        { name: 'value', type: 'string' },
        { name: 'time', type: 'string' }
    ],
    idProperty: 'index'
});
//#endregion

//#region Store
var consumptionStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ConsumptionModel',
    downloadURL: '/Home/DownloadHomeConsumption',
    proxy: {
        type: 'ajax',
        url: '/Home/RequestHomeConsumption',
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
                $$iPems.Tasks.homeTasks.consumptionTask.fireOnStart = false;
                $$iPems.Tasks.homeTasks.consumptionTask.restart();
            }
        }
    }
});

var consumptionPagingToolbar = $$iPems.clonePagingToolbar(consumptionStore);
//#endregion

//#region Chart
var pieChart = null,
    pueChart = null,
    lineChart = null,
    barChart = null,
    pieOption = {
        title: {
            text: '耗电量分类占比',
            x: 50,
            y: 18,
            textStyle: {
                color: '#535353',
                fontSize: 18,
                fontWeight: 'bold'
            }
        },
        legend: {
            type: 'scroll',
            orient: 'vertical',
            left: 15,
            top: 60,
            data: ['空调', '照明', '办公', 'IT设备', '开关电源', 'UPS', '其他']
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b} <br/>{a}: {c} ({d}%)"
        },
        color: ['#d867b2', '#4bc567', '#26aff0', '#ed5441', '#ff9b13', '#a1b033', '#00a4b1'],
        series: [
            {
                name: '能耗(占比)',
                type: 'pie',
                radius: ['35%', '75%'],
                center: ['60%', '50%'],
                avoidLabelOverlap: false,
                data: [
                    { value: 0, name: '空调' },
                    { value: 0, name: '照明' },
                    { value: 0, name: '办公' },
                    { value: 0, name: 'IT设备' },
                    { value: 0, name: '开关电源' },
                    { value: 0, name: 'UPS' },
                    { value: 0, name: '其他' }
                ],
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
                }
            }
        ]
    },
    pueOption = {
        title: {
            text: 'PUE比值',
            x: 50,
            y: 18,
            textStyle: {
                color: '#535353',
                fontSize: 18,
                fontWeight: 'bold'
            }
        },
        tooltip: {
            formatter: '{b}: {c}'
        },
        series: [
            {
                name: 'PUE',
                type: 'gauge',
                center: ['50%', '60%'],
                min: 1,
                max: 5,
                axisLine: {
                    lineStyle: {
                        width: 15,
                        color: [[0.2, '#4bc567'], [0.8, '#26aff0'], [1, '#ed5441']]
                    }
                },
                axisTick: {
                    length: 20
                },
                splitLine: {
                    length: 25
                },
                title: {
                    offsetCenter: [0, '-30%'],
                    textStyle: {
                        fontWeight: 'bold',
                        fontSize: 18,
                    }
                },
                detail: {
                    offsetCenter: [0, '65%']
                },
                data: [{ name: 'PUE', value: 1 }]
            }
        ]
    },
    lineOption = {
        title: {
            text: '耗电量分时曲线',
            x: 50,
            y: 18,
            textStyle: {
                color: '#535353',
                fontSize: 18,
                fontWeight: 'bold'
            }
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            type: 'scroll',
            right: 15,
            top: 55,
            data: ['今日', '昨日']
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data:['00:00']
        },
        yAxis: {
            type: 'value',
            name: '(kW·h)'
        },
        grid: {
            top: 90,
            left: 5,
            right: 20,
            bottom: 5,
            containLabel: true
        },
        series: [
            {
                name: '今日',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        color: '#26aff0'
                    }
                },
                data: [0],
            },
            {
                name: '昨日',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        color: '#ed5441'
                    }
                },
                data: [0],
            }
        ]
    },
    barOption = {
        title: {
            text: '耗电量日环比',
            fontWeight: 20,
            x: 50,
            y: 18,
            textStyle: {
                color: '#535353',
                fontSize: 18,
                fontWeight: 'bold'
            }
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        legend: {
            type: 'scroll',
            right: 15,
            top: 55,
            data: ['本月', '上月']
        },
        xAxis: {
            data: ['01/01'],
        },
        yAxis: {
            type: 'value',
            name: '(kW·h)'
        },
        grid: {
            top: 90,
            left: 5,
            right: 20,
            bottom: 5,
            containLabel: true
        },
        series: [{
            name: '本月',
            type: 'bar',
            data: [0],
            itemStyle: {
                normal: {
                    color: '#26aff0'
                }
            }
        }, {
            name: '上月',
            type: 'bar',
            data: [0],
            itemStyle: {
                normal: {
                    color: '#ed5441'
                }
            }
        }]
    };
//#endregion

//#region UI
var leftPanel = Ext.create('Ext.tree.Panel', {
    region: 'west',
    title: '系统层级',
    glyph: 0xf011,
    width: 220,
    split: true,
    collapsible: true,
    collapsed: false,
    autoScroll: true,
    useArrows: false,
    rootVisible: true,
    root: {
        id: 'root',
        text: '全部',
        expanded: true,
        icon: $$iPems.icons.All
    },
    viewConfig: {
        loadMask: true
    },
    store: Ext.create('Ext.data.TreeStore', {
        autoLoad: false,
        nodeParam: 'node',
        proxy: {
            type: 'ajax',
            url: '/Component/GetRooms',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            }
        }
    }),
    listeners: {
        select: function (me, record, index) {
            currentNode = record;
            select();
        }
    },
    tbar: [
        {
            id: 'left-search-field',
            xtype: 'textfield',
            emptyText: '请输入筛选条件...',
            flex: 1,
            listeners: {
                change: function (me, newValue, oldValue, eOpts) {
                    delete me._filterData;
                    delete me._filterIndex;
                }
            }
        },
        {
            xtype: 'button',
            glyph: 0xf005,
            handler: function () {
                var tree = leftPanel,
                    search = Ext.getCmp('left-search-field'),
                    text = search.getRawValue();

                if (Ext.isEmpty(text, false)) {
                    return;
                }

                if (text.length < 2) {
                    return;
                }

                if (search._filterData != null
                    && search._filterIndex != null) {
                    var index = search._filterIndex + 1;
                    var paths = search._filterData;
                    if (index >= paths.length) {
                        index = 0;
                        Ext.Msg.show({ title: '系统提示', msg: '搜索完毕', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                    }

                    $$iPems.selectNodePath(tree, paths[index]);
                    search._filterIndex = index;
                } else {
                    Ext.Ajax.request({
                        url: '/Component/FilterRoomPath',
                        params: { text: text },
                        mask: new Ext.LoadMask({ target: tree, msg: '正在处理...' }),
                        success: function (response, options) {
                            var data = Ext.decode(response.responseText, true);
                            if (data.success) {
                                var len = data.data.length;
                                if (len > 0) {
                                    $$iPems.selectNodePath(tree, data.data[0]);
                                    search._filterData = data.data;
                                    search._filterIndex = 0;
                                } else {
                                    Ext.Msg.show({ title: '系统提示', msg: Ext.String.format('未找到指定内容:<br/>{0}', text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                }
                            } else {
                                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                            }
                        }
                    });
                }
            }
        },
        '-',{
            xtype: 'button',
            glyph: 0xf058,
            handler: function () {
                refresh();
            }
        }
    ]
});

var centerPanel = Ext.create('Ext.panel.Panel', {
    header: false,
    border: false,
    region: 'center',
    overflowY: 'auto',
    defaults: {
        margin: '0 22 0 0'
    },
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    items: [
        {
            xtype: 'container',
            layout: {
                type: 'hbox',
                align: 'stretch',

            },
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-01'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-02'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-03'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-04'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-05'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-06'
                }
            ]
        }, {
            xtype: 'component',
            height: 5
        }, {
            xtype: 'container',
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-pie-container'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    items: [
                        {
                            xtype: 'container',
                            layout: {
                                type: 'hbox',
                                align: 'stretch'
                            },
                            items: [
                                {
                                    xtype: 'container',
                                    flex: 2,
                                    contentEl: 'indicator-pue-container'
                                },
                                {
                                    xtype: 'container',
                                    flex: 1,
                                    contentEl: 'indicator-rate-container'
                                }
                            ]
                        }
                    ]
                }
            ]
        }, {
            xtype: 'component',
            height: 5
        }, {
            xtype: 'container',
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-line-container'
                }, {
                    xtype: 'component',
                    width: 5
                }, {
                    xtype: 'container',
                    flex: 1,
                    contentEl: 'indicator-bar-container'
                }
            ]
        }, {
            xtype: 'component',
            height: 5
        }, {
            xtype: 'panel',
            glyph: 0xf029,
            title: '变压器损耗列表',
            collapsible: true,
            collapseFirst: false,
            tools: [{
                type: 'print',
                tooltip: '数据导出',
                handler: function (event, toolEl, panelHeader) {
                    download(consumptionStore);
                }
            }],
            items: [
                {
                    xtype: 'grid',
                    height: 260,
                    store: consumptionStore,
                    border: false,
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
                        text: '所属机房',
                        dataIndex: 'room',
                        align: 'left',
                        width: 200,
                        sortable: true
                    }, {
                        text: '设备名称',
                        dataIndex: 'device',
                        width: 200,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '损耗值',
                        dataIndex: 'value',
                        width: 150,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '测值时间',
                        dataIndex: 'time',
                        width: 150,
                        align: 'left',
                        sortable: true
                    }],
                    bbar: consumptionPagingToolbar
                }
            ]
        }
    ],
    listeners: {
        resize: function (me, width, height, oldWidth, oldHeight) {
            if (!Ext.isEmpty(pieChart)) pieChart.resize();
            if (!Ext.isEmpty(pueChart)) pueChart.resize();
            if (!Ext.isEmpty(lineChart)) lineChart.resize();
            if (!Ext.isEmpty(barChart)) barChart.resize();
        }
    }
});

var currentLayout = Ext.create('Ext.panel.Panel', {
    id: 'currentLayout',
    region: 'center',
    layout: 'border',
    border: false,
    items: [leftPanel, centerPanel]
});
//#endregion

//#region Method
var select = function () {
    if (Ext.isEmpty(currentNode)) return;
    request(true);
};

var refresh = function () {
    if (Ext.isEmpty(currentNode)) return;
    request(false);
};

var request = function (mask) {
    var id = currentNode.getId(),
        keys = $$iPems.SplitKeys(id);

    if (keys.length !== 2)
        return;

    var type = parseInt(keys[0]);
    if (type !== $$iPems.SSH.Station && type !== $$iPems.SSH.Room) {
        init();
        return;
    }

    Ext.Ajax.request({
        url: '/Home/RequestHomeConsumptionChart',
        params: { parent: id },
        mask: mask === true ? new Ext.LoadMask(centerPanel, { msg: '正在处理...' }) : null,
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                if (pieChart && pueChart && lineChart && barChart) {
                    if (!Ext.isEmpty(data.data)) {
                        dochart(data.data);
                    } else {
                        init();
                    }
                }
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }

            if (mask) {
                consumptionStore.getProxy().extraParams.parent = currentNode.getId();
                consumptionStore.loadPage(1);
            } else {
                consumptionPagingToolbar.doRefresh();
            }
        }
    });
};

var download = function (store) {
    if (Ext.isEmpty(store.downloadURL)) return;

    $$iPems.download({
        url: store.downloadURL,
        params: store.getProxy().extraParams
    });
};

var init = function () {
    if (inited === true)
        return;

    //#region dom
    var indicator01 = document.getElementById('indicator-01-value'),
        indicator02 = document.getElementById('indicator-02-value'),
        indicator03 = document.getElementById('indicator-03-value'),
        indicator04 = document.getElementById('indicator-04-value'),
        indicator05 = document.getElementById('indicator-05-value'),
        indicator06l = document.getElementById('indicator-06-lvalue'),
        indicator06r = document.getElementById('indicator-06-rvalue'),
        indicatorrate01 = document.getElementById('indicator-rate-value1'),
        indicatorrate02 = document.getElementById('indicator-rate-value2'),
        indicatorrate03 = document.getElementById('indicator-rate-value3'),
        indicatorrate04 = document.getElementById('indicator-rate-value4');
    //#endregion

    //#region indicator
    indicator01.innerHTML = "--";
    indicator02.innerHTML = "--";
    indicator03.innerHTML = "--";
    indicator04.innerHTML = "--";
    indicator05.innerHTML = "--";
    indicator06l.innerHTML = "--";
    indicator06r.innerHTML = "--";
    indicatorrate01.innerHTML = "--";
    indicatorrate02.innerHTML = "--";
    indicatorrate03.innerHTML = "--";
    indicatorrate04.innerHTML = "--";
    //#endregion

    //#region pie
    pieOption.series[0].data[0].value = 0;
    pieOption.series[0].data[1].value = 0;
    pieOption.series[0].data[2].value = 0;
    pieOption.series[0].data[3].value = 0;
    pieOption.series[0].data[4].value = 0;
    pieOption.series[0].data[5].value = 0;
    pieOption.series[0].data[6].value = 0;
    //#endregion

    //#region pue
    pueOption.series[0].data[0].value = 1;
    //#endregion

    //#region line
    lineOption.xAxis.data = ['00:00'];
    lineOption.series[0].data = [0];
    lineOption.series[1].data = [0];
    //#endregion

    //#region bar
    barOption.xAxis.data = ['01/01'];
    barOption.series[0].data = [0];
    barOption.series[1].data = [0];
    //#endregion

    //#region apply option
    pieChart.setOption(pieOption, true);
    pueChart.setOption(pueOption, true);
    lineChart.setOption(lineOption, true);
    barChart.setOption(barOption, true);
    //#endregion

    //#region store
    consumptionStore.removeAll();
    //#endregion

    inited = true;
};

var dochart = function (data) {
    inited = false;

    //#region dom
    var indicator01 = document.getElementById('indicator-01-value'),
        indicator02 = document.getElementById('indicator-02-value'),
        indicator03 = document.getElementById('indicator-03-value'),
        indicator04 = document.getElementById('indicator-04-value'),
        indicator05 = document.getElementById('indicator-05-value'),
        indicator06l = document.getElementById('indicator-06-lvalue'),
        indicator06r = document.getElementById('indicator-06-rvalue'),
        indicatorrate01 = document.getElementById('indicator-rate-value1'),
        indicatorrate02 = document.getElementById('indicator-rate-value2'),
        indicatorrate03 = document.getElementById('indicator-rate-value3'),
        indicatorrate04 = document.getElementById('indicator-rate-value4'),
        category01 = indicator01.getAttribute("data-category"),
        category02 = indicator02.getAttribute("data-category"),
        category03 = indicator03.getAttribute("data-category"),
        category04 = indicator04.getAttribute("data-category"),
        category05 = indicator05.getAttribute("data-category");
    //#endregion

    //#region indicator
    if (data.kt.index == category01) {
        indicator01.innerHTML = data.kt.value;
    } else if (data.zm.index == category01) {
        indicator01.innerHTML = data.zm.value;
    } else if (data.bg.index == category01) {
        indicator01.innerHTML = data.bg.value;
    } else if (data.dy.index == category01) {
        indicator01.innerHTML = data.dy.value;
    } else if (data.ups.index == category01) {
        indicator01.innerHTML = data.ups.value;
    } else if (data.it.index == category01) {
        indicator01.innerHTML = data.it.value;
    } else if (data.qt.index == category01) {
        indicator01.innerHTML = data.qt.value;
    } else if (data.tt.index == category01) {
        indicator01.innerHTML = data.tt.value;
    }

    if (data.kt.index == category02) {
        indicator02.innerHTML = data.kt.value;
    } else if (data.zm.index == category02) {
        indicator02.innerHTML = data.zm.value;
    } else if (data.bg.index == category02) {
        indicator02.innerHTML = data.bg.value;
    } else if (data.dy.index == category02) {
        indicator02.innerHTML = data.dy.value;
    } else if (data.ups.index == category02) {
        indicator02.innerHTML = data.ups.value;
    } else if (data.it.index == category02) {
        indicator02.innerHTML = data.it.value;
    } else if (data.qt.index == category02) {
        indicator02.innerHTML = data.qt.value;
    } else if (data.tt.index == category02) {
        indicator02.innerHTML = data.tt.value;
    }

    if (data.kt.index == category03) {
        indicator03.innerHTML = data.kt.value;
    } else if (data.zm.index == category03) {
        indicator03.innerHTML = data.zm.value;
    } else if (data.bg.index == category03) {
        indicator03.innerHTML = data.bg.value;
    } else if (data.dy.index == category03) {
        indicator03.innerHTML = data.dy.value;
    } else if (data.ups.index == category03) {
        indicator03.innerHTML = data.ups.value;
    } else if (data.it.index == category03) {
        indicator03.innerHTML = data.it.value;
    } else if (data.qt.index == category03) {
        indicator03.innerHTML = data.qt.value;
    } else if (data.tt.index == category03) {
        indicator03.innerHTML = data.tt.value;
    }

    if (data.kt.index == category04) {
        indicator04.innerHTML = data.kt.value;
    } else if (data.zm.index == category04) {
        indicator04.innerHTML = data.zm.value;
    } else if (data.bg.index == category04) {
        indicator04.innerHTML = data.bg.value;
    } else if (data.dy.index == category04) {
        indicator04.innerHTML = data.dy.value;
    } else if (data.ups.index == category04) {
        indicator04.innerHTML = data.ups.value;
    } else if (data.it.index == category04) {
        indicator04.innerHTML = data.it.value;
    } else if (data.qt.index == category04) {
        indicator04.innerHTML = data.qt.value;
    } else if (data.tt.index == category04) {
        indicator04.innerHTML = data.tt.value;
    }

    if (data.kt.index == category05) {
        indicator05.innerHTML = data.kt.value;
    } else if (data.zm.index == category05) {
        indicator05.innerHTML = data.zm.value;
    } else if (data.bg.index == category05) {
        indicator05.innerHTML = data.bg.value;
    } else if (data.dy.index == category05) {
        indicator05.innerHTML = data.dy.value;
    } else if (data.ups.index == category05) {
        indicator05.innerHTML = data.ups.value;
    } else if (data.it.index == category05) {
        indicator05.innerHTML = data.it.value;
    } else if (data.qt.index == category05) {
        indicator05.innerHTML = data.qt.value;
    } else if (data.tt.index == category05) {
        indicator05.innerHTML = data.tt.value;
    }

    indicator06l.innerHTML = data.wd;
    indicator06r.innerHTML = data.sd;
    indicatorrate01.innerHTML = data.curmonth;
    indicatorrate02.innerHTML = data.lastmonth;
    indicatorrate03.innerHTML = data.curyear;
    indicatorrate04.innerHTML = data.lastyear;
    //#endregion

    //#region pie
    pieOption.series[0].data[0].value = data.kt.value;
    pieOption.series[0].data[1].value = data.zm.value;
    pieOption.series[0].data[2].value = data.bg.value;
    pieOption.series[0].data[3].value = data.it.value;
    pieOption.series[0].data[4].value = data.dy.value;
    pieOption.series[0].data[5].value = data.ups.value;
    pieOption.series[0].data[6].value = data.qt.value;
    //#endregion

    //#region pue
    pueOption.series[0].data[0].value = data.pue;
    //#endregion

    //#region line
    var line_xaxis = [], line_series0 = [], line_series1 = [];
    Ext.Array.each(data.dayline, function (item) {
        line_xaxis.push(item.name);
        Ext.Array.each(item.models, function (model) {
            if (model.index === 0)
                line_series0.push(model.value);
            else
                line_series1.push(model.value);
        });
    });

    if (line_xaxis.length == 0) line_xaxis.push('00:00');
    if (line_series0.length == 0) line_series0.push(0);
    if (line_series1.length == 0) line_series1.push(0);

    lineOption.xAxis.data = line_xaxis;
    lineOption.series[0].data = line_series0;
    lineOption.series[1].data = line_series1;
    //#endregion

    //#region bar
    var bar_xaxis = [], bar_series0 = [], bar_series1 = [];
    Ext.Array.each(data.monthbar, function (item) {
        bar_xaxis.push(item.name);
        Ext.Array.each(item.models, function (model) {
            if (model.index === 0)
                bar_series0.push(model.value);
            else
                bar_series1.push(model.value);
        });
    });

    if (bar_xaxis.length == 0) bar_xaxis.push('01/01');
    if (bar_series0.length == 0) bar_series0.push(0);
    if (bar_series1.length == 0) bar_series1.push(0);

    barOption.xAxis.data = bar_xaxis;
    barOption.series[0].data = bar_series0;
    barOption.series[1].data = bar_series1;
    //#endregion

    //#region apply option
    pieChart.setOption(pieOption, true);
    pueChart.setOption(pueOption, true);
    lineChart.setOption(lineOption, true);
    barChart.setOption(barOption, true);
    //#endregion
};
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
    //init charts
    pieChart = echarts.init(document.getElementById("indicator-pie"), 'shine');
    pueChart = echarts.init(document.getElementById("indicator-pue"), 'shine');
    lineChart = echarts.init(document.getElementById("indicator-line"), 'shine');
    barChart = echarts.init(document.getElementById("indicator-bar"), 'shine');

    //set charts
    pieChart.setOption(pieOption);
    pueChart.setOption(pueOption);
    lineChart.setOption(lineOption);
    barChart.setOption(barOption);

    //task
    $$iPems.Tasks.homeTasks.consumptionTask.run = function () {
        refresh();
    };
    $$iPems.Tasks.homeTasks.consumptionTask.start();
});
//#endregion