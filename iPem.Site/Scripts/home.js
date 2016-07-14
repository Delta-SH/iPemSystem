(function () {
    Ext.define('UnconnectedModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'interval', type: 'float' }
        ],
        idProperty: 'index'
    });

    Ext.define('OffModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'room', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'interval', type: 'float' }
        ],
        idProperty: 'index'
    });

    var unconnectedStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'UnconnectedModel',
        proxy: {
            type: 'ajax',
            url: '/Home/GetNotices',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        }
    });

    var offStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'OffModel',
        proxy: {
            type: 'ajax',
            url: '/Home/GetNotices',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        }
    });

    var unconnectedPagingToolbar = $$iPems.clonePagingToolbar(unconnectedStore);

    var offPagingToolbar = $$iPems.clonePagingToolbar(offStore);

    var almOption = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                animation: false
            }
        },
        toolbox: {
            show: true,
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar', 'tiled'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        grid: {
            top: 40,
            left: 5,
            right: 5,
            bottom: 5,
            containLabel: true
        },
        xAxis: [
            {
                type: 'category',
                data: [],
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
                stack: 'alarm',
                data: []
            },
            {
                name: '二级告警',
                type: 'bar',
                itemStyle: {
                    normal: {
                        color: '#efa91f'
                    }
                },
                stack: 'alarm',
                data: []
            },
            {
                name: '三级告警',
                type: 'bar',
                itemStyle: {
                    normal: {
                        color: '#f5d313'
                    }
                },
                stack: 'alarm',
                data: []
            },
            {
                name: '四级告警',
                type: 'bar',
                itemStyle: {
                    normal: {
                        color: '#0892cd'
                    }
                },
                stack: 'alarm',
                data: []
            }
        ]
    };

    var cpuOption = {
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
    };

    var memoryOption = {
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
    };

    var energypieOption = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            x: 'center',
            y: 'bottom',
            data: ['空调', '照明', '办公', '其他']
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
                    { value: 895, name: '空调' },
                    { value: 221, name: '照明' },
                    { value: 152, name: '办公' },
                    { value: 335, name: '其他' }
                ]
            }
        ]
    };

    var energybarOption = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                animation: false
            }
        },
        toolbox: {
            show: true,
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar', 'stack'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        grid: {
            top: 40,
            left: 5,
            right: 5,
            bottom: 5,
            containLabel: true
        },
        xAxis: [
            {
                type: 'category',
                data: ['北京市', '上海市', '浙江省', '江苏省', '河北省', '广东省', '吉林省'],
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
                name: '空调',
                type: 'bar',
                data: [120, 132, 101, 134, 90, 230, 210]
            },
            {
                name: '照明',
                type: 'bar',
                data: [220, 182, 191, 234, 290, 330, 310]
            },
            {
                name: '办公',
                type: 'bar',
                data: [150, 232, 201, 154, 190, 330, 410]
            },
            {
                name: '其他',
                type: 'bar',
                data: [220, 182, 191, 234, 290, 330, 310]
            }
        ]
    };

    var unconnectedOption = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            x: 'center',
            y: 'bottom',
            data: ['正常', '断站']
        },
        series: [
            {
                name: '站点断站率',
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
                        value: 286, name: '正常', itemStyle: {
                            normal: {
                                color: '#48ac2e'
                            }
                        }
                    },
                    {
                        value: 392, name: '断站', itemStyle: {
                            normal: {
                                color: '#c12e34'
                            }
                        }
                    }
                ]
            }
        ]
    };

    var offlineOption = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            x: 'center',
            y: 'bottom',
            data: ['正常', '离线']
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
                        value: 679, name: '正常', itemStyle: {
                            normal: {
                                color: '#48ac2e'
                            }
                        }
                    },
                    {
                        value: 335, name: '离线', itemStyle: {
                            normal: {
                                color: '#c12e34'
                            }
                        }
                    }
                ]
            }
        ]
    };

    Ext.onReady(function () {
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
                        title: '区域告警分布图',
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
                title: '区域能耗分布图',
                collapsible: true,
                collapseFirst: false,
                layout: {
                    type: 'hbox',
                    align: 'stretch'
                },
                cls: 'energyview',
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
                title: '站点断站列表',
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
                    }
                }],
                cls: 'unconnectedview',
                items: [
                    {
                        xtype: 'container',
                        flex: 1,
                        contentEl: 'unconnected-pie'
                    }, {
                        xtype: 'component',
                        height: 5
                    }, {
                        xtype: 'grid',
                        flex: 3,
                        store: unconnectedStore,
                        loadMask: false,
                        forceFit: false,
                        border: false,
                        style: {
                            'border-left': '1px solid #c0c0c0'
                        },
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
                            width: 80,
                            align: 'left',
                            sortable: true
                        }, {
                            text: '所属区域',
                            dataIndex: 'area',
                            align: 'left',
                            flex: 1,
                            sortable: true
                        }, {
                            text: '站点名称',
                            dataIndex: 'station',
                            align: 'left',
                            flex: 1,
                            sortable: true
                        }, {
                            text: '断站时长(分钟)',
                            dataIndex: 'interval',
                            width: 120,
                            align: 'left',
                            sortable: true
                        }],
                        bbar: unconnectedPagingToolbar
                    }
                ]
            }, {
                xtype: 'component',
                height: 10
            }, {
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
                    }
                }],
                cls: 'offlineview',
                items: [
                    {
                        xtype: 'container',
                        flex: 1,
                        contentEl: 'offline-pie'
                    }, {
                        xtype: 'component',
                        height: 5
                    }, {
                        xtype: 'grid',
                        flex: 3,
                        store: offStore,
                        loadMask: false,
                        forceFit: false,
                        border: false,
                        style: {
                            'border-left': '1px solid #c0c0c0'
                        },
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
                            width: 80,
                            align: 'left',
                            sortable: true
                        }, {
                            text: '所属区域',
                            dataIndex: 'area',
                            align: 'left',
                            flex: 1,
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
                            flex: 1,
                            sortable: true
                        }, {
                            text: 'Fsu名称',
                            dataIndex: 'name',
                            align: 'left',
                            flex: 1,
                            sortable: true
                        }, {
                            text: '离线时长(分钟)',
                            dataIndex: 'interval',
                            width: 120,
                            align: 'left',
                            sortable: true
                        }],
                        bbar: offPagingToolbar
                    }
                ]
            }]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        var almChart = echarts.init(document.getElementById("alm-chart"), 'shine');
        var cpuChart = echarts.init(document.getElementById("cpu-chart"), 'shine');
        var memoryChart = echarts.init(document.getElementById("memory-chart"), 'shine');
        var energybarChart = echarts.init(document.getElementById("energy-bar"), 'shine');
        var energypieChart = echarts.init(document.getElementById("energy-pie"), 'shine');
        var unconnectedChart = echarts.init(document.getElementById("unconnected-pie"), 'shine');
        var offlineChart = echarts.init(document.getElementById("offline-pie"), 'shine');

        almChart.on('magictypechanged', function (param) {
            $$iPems.Tasks.homeTasks.almTask.stop();
        });
        almChart.on('restore', function (param) {
            $$iPems.Tasks.homeTasks.almTask.start();
        });

        //init charts
        energybarChart.setOption(energybarOption);
        energypieChart.setOption(energypieOption);
        unconnectedChart.setOption(unconnectedOption);
        offlineChart.setOption(offlineOption);

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
    });
})();