﻿(function () {
    var pieChart = null,
        barChart = null,
        pieOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{b} <br/>{a}: {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                y: 'center',
                data: ['无数据']
            },
            series: [
                {
                    name: '数量(占比)',
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
                    data: [0]
                }
            ]
        },
        barOption = {
            tooltip: {
                trigger: 'axis',
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
                    type: 'value'
                }
            ],
            series: [
                {
                    name: '数量',
                    type: 'bar',
                    data: [0]
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
                    if (!Ext.isEmpty(data) && Ext.isArray(data.chart)) {
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

                        if (legend.length == 0) legend.push('无数据');
                        if (pseries.length == 0) pseries.push({ value: 0, name: '无数据' });
                        if (xaxis.length == 0) xaxis.push('无数据');
                        if (bseries.length == 0) bseries.push(0);

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
                title: '筛选条件',
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
                            query();
                        }
                    }, '-', {
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
            }
        ],
        items: [
            {
                xtype: 'panel',
                glyph: 0xf030,
                title: '分类占比',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 2,
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
                xtype: 'gridpanel',
                glyph: 0xf029,
                flex: 3,
                margin: '5 0 0 0',
                collapsible: true,
                collapseFirst: false,
                title: '区域信息',
                store: currentStore,
                columnLines: true,
                disableSelection: false,
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
                }, {
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

    var query = function () {
        var areasfield = Ext.getCmp('areasfield'),
            typesfield = Ext.getCmp('typesfield'),
            parent = areasfield.getValue(),
            types = typesfield.getValue();

        var me = currentStore, proxy = me.getProxy();
        proxy.extraParams.parent = parent;
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
            url: '/Report/DownloadBase400101',
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

    Ext.onReady(function () {
        pieChart = echarts.init(document.getElementById("pie-chart"), 'shine');
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');

        //init charts
        pieChart.setOption(pieOption);
        barChart.setOption(barOption);
    });
})();