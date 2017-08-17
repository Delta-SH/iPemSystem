(function () {
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
            { name: 'property', type: 'string' },
            { name: 'address', type: 'string' },
            { name: 'floor', type: 'string' },
            { name: 'length', type: 'string' },
            { name: 'width', type: 'string' },
            { name: 'height', type: 'string' },
            { name: 'floorLoad', type: 'string' },
            { name: 'lineHeigth', type: 'string' },
            { name: 'square', type: 'string' },
            { name: 'effeSquare', type: 'string' },
            { name: 'fireFighEuip', type: 'string' },
            { name: 'owner', type: 'string' },
            { name: 'queryPhone', type: 'string' },
            { name: 'powerSubMain', type: 'string' },
            { name: 'tranSubMain', type: 'string' },
            { name: 'enviSubMain', type: 'string' },
            { name: 'fireSubMain', type: 'string' },
            { name: 'airSubMain', type: 'string' },
            { name: 'contact', type: 'string' },
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
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Report/RequestBase400103',
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
                        id: 'stationfield',
                        xtype: 'StationPicker',
                        emptyText: '默认全部'
                    }, {
                        id: 'typesfield',
                        xtype: 'RoomTypeMultiCombo',
                        emptyText: '默认全部'
                    }, {
                        xtype: 'button',
                        text: '数据查询',
                        glyph: 0xf005,
                        handler: function (el, e) {
                            query();
                        }
                    }, '-', {
                        xtype: 'button',
                        text: '数据导出',
                        glyph: 0xf010,
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
                title: '机房信息',
                store: currentStore,
                columnLines: true,
                disableSelection: false,
                tools: [{
                    type: 'print',
                    tooltip: '数据导出',
                    handler: function (event, toolEl, panelHeader) {
                        print();
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
                    width: 60,
                    align: 'left',
                    sortable: true
                }, {
                    text: '编号',
                    dataIndex: 'id',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '名称',
                    dataIndex: 'name',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '类型',
                    dataIndex: 'type',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '产权',
                    dataIndex: 'property',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '地址',
                    dataIndex: 'address',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '楼层',
                    dataIndex: 'floor',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '长度',
                    dataIndex: 'length',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '宽度',
                    dataIndex: 'width',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '高度',
                    dataIndex: 'height',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '楼面荷载',
                    dataIndex: 'floorLoad',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '走线架高度',
                    dataIndex: 'lineHeigth',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '机房面积',
                    dataIndex: 'square',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '可使用面积',
                    dataIndex: 'effeSquare',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '消防设备',
                    dataIndex: 'fireFighEuip',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '业主联系人',
                    dataIndex: 'owner',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '查询电话',
                    dataIndex: 'queryPhone',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '动力代维',
                    dataIndex: 'powerSubMain',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '传输代维',
                    dataIndex: 'tranSubMain',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '环境代维',
                    dataIndex: 'enviSubMain',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '消防代维',
                    dataIndex: 'fireSubMain',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '空调代维',
                    dataIndex: 'airSubMain',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '维护负责人',
                    dataIndex: 'contact',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '描述',
                    dataIndex: 'comment',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '状态',
                    dataIndex: 'enabled',
                    align: 'center',
                    width: 100,
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
        var stationfield = Ext.getCmp('stationfield'),
            typesfield = Ext.getCmp('typesfield'),
            parent = stationfield.getValue(),
            types = typesfield.getValue();

        var me = currentStore, proxy = me.getProxy();
        proxy.extraParams.parent = parent;
        proxy.extraParams.types = types;
        me.loadPage(1);
    };

    var print = function () {
        $$iPems.download({
            url: '/Report/DownloadBase400103',
            params: currentStore.getProxy().extraParams
        });
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
            
            //load data
            Ext.defer(query, 2000);
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