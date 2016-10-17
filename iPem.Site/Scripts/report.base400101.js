(function () {
    var pieChart = null,
        barChart = null,
        pieOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                y: 'center',
                data: []
            },
            series: [
                {
                    name: '区域分类',
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
                    data: []
                }
            ]
        },
        barOption = {
            tooltip: {
                trigger: 'axis',
                formatter: "{a} <br/>{b}: {c}",
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
                    name: '区域分类',
                    type: 'bar',
                    data: []
                }
            ]
        };

    Ext.define('ReportModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'enabled', type: 'boolean' }
        ],
        idProperty: 'id'
    });

    var query = function (store) {
        var areasfield = Ext.getCmp('areasfield'),
            typesfield = Ext.getCmp('typesfield'),
            parent = areasfield.getValue(),
            types = typesfield.getValue();

        var proxy = store.getProxy();
        proxy.extraParams.parent = parent;
        proxy.extraParams.types = types;
        store.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadBase400101',
            params: store.proxy.extraParams
        });
    };

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ReportModel',
        groupField: 'type',
        //禁用自动排序，使用后台的排序方式
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Report/RequestBase400101',
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
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length > 0) {
                        var legend = [], pseries = [], xaxis = [], bseries = [];
                        Ext.Array.each(data.chart, function (item, index) {
                            legend.push(item.name);
                            pseries.push({
                                value: item.value,
                                name: item.name
                            });
                            xaxis.push(item.name);
                            bseries.push(item.value);
                        });

                        pieOption.legend.data = legend;
                        pieOption.series[0].data = pseries;

                        barOption.xAxis[0].data = xaxis;
                        barOption.series[0].data = bseries;

                        pieChart.setOption(pieOption);
                        barChart.setOption(barOption);
                    }
                }
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    Ext.onReady(function () {
        var currentLayout = Ext.create('Ext.panel.Panel', {
            id: 'currentLayout',
            region: 'center',
            border: false,
            layout: {
                type: 'vbox',
                align: 'stretch',
                pack: 'start'
            },
            dockedItems: [
                {
                    xtype: 'panel',
                    glyph: 0xf034,
                    title: '区域统计条件',
                    bodyCls: 'x-docked-top-with-bottom',
                    collapsible: true,
                    collapsed: false,
                    dock: 'top',
                    items: [{
                        xtype: 'toolbar',
                        border: false,
                        items: [{
                            id: 'areasfield',
                            xtype: 'AreaPicker',
                            emptyText: '默认全部'
                        }, {
                            id: 'typesfield',
                            xtype: 'AreaTypeMultiCombo',
                            emptyText: '默认全部'
                        }, {
                            xtype: 'button',
                            text: '数据查询',
                            glyph: 0xf005,
                            handler: function (el, e) {
                                query(currentStore);
                            }
                        }, '-', {
                            xtype: 'button',
                            text: '数据导出',
                            glyph: 0xf010,
                            handler: function (el, e) {
                                print(currentStore);
                            }
                        }]
                    }]
                }
            ],
            items: [
                {
                    xtype: 'panel',
                    glyph: 0xf030,
                    title: '区域统计图表',
                    collapsible: true,
                    collapseFirst: false,
                    margin: '5 0 0 0',
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
                    ]
                }, {
                    xtype: 'gridpanel',
                    glyph: 0xf029,
                    flex: 1,
                    margin: '5 0 0 0',
                    collapsible: true,
                    collapseFirst: false,
                    title: '区域统计信息',
                    store: currentStore,
                    columnLines: true,
                    disableSelection: false,
                    tools: [{
                        type: 'print',
                        tooltip: '数据导出',
                        handler: function (event, toolEl, panelHeader) {
                            print(currentStore);
                        }
                    }],
                    viewConfig: {
                        loadMask: true,
                        trackOver: true,
                        stripeRows: true,
                        emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
                    },
                    features: [{
                        ftype: 'grouping',
                        groupHeaderTpl: '{columnName}: {name} ({rows.length}条)',
                        hideGroupedHeader: false,
                        startCollapsed: true
                    }],
                    columns: [{
                        text: '序号',
                        dataIndex: 'index',
                        width: 80,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '编号',
                        dataIndex: 'id',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '名称',
                        dataIndex: 'name',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }, {
                        text: '类型',
                        dataIndex: 'type',
                        align: 'left',
                        width: 120,
                        sortable: true
                    }, {
                        text: '备注',
                        dataIndex: 'comment',
                        align: 'left',
                        width: 120,
                        sortable: true
                    },  {
                        text: '状态',
                        dataIndex: 'enabled',
                        align: 'center',
                        width: 120,
                        sortable: true,
                        renderer: function (value) {
                            return value ? '有效' : '禁用';
                        }
                    }],
                    bbar: currentPagingToolbar
                }
            ]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);

            //load data
            query(currentStore);
        }
    });

    Ext.onReady(function () {
        pieChart = echarts.init(document.getElementById("pie-chart"), 'shine');
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');

        //init charts
        pieChart.setOption(pieOption);
        barChart.setOption(barOption);
    });
})();