(function () {
    var barChart = null,
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
            { name: 'subType', type: 'string' },
            { name: 'sysName', type: 'string' },
            { name: 'sysCode', type: 'string' },
            { name: 'model', type: 'string' },
            { name: 'productor', type: 'string' },
            { name: 'brand', type: 'string' },
            { name: 'supplier', type: 'string' },
            { name: 'subCompany', type: 'string' },
            { name: 'startTime', type: 'string' },
            { name: 'scrapTime', type: 'string' },
            { name: 'status', type: 'string' },
            { name: 'contact', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'enabled', type: 'boolean' }
        ],
        idProperty: 'id'
    });

    var query = function (store) {
        var roomfield = Ext.getCmp('roomfield'),
            typesfield = Ext.getCmp('typesfield'),
            parent = roomfield.getValue(),
            types = typesfield.getValue();

        var proxy = store.getProxy();
        proxy.extraParams.parent = parent;
        proxy.extraParams.types = types;
        store.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadBase400104',
            params: store.proxy.extraParams
        });
    };

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ReportModel',
        groupField: 'type',
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Report/RequestBase400104',
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
                if (successful && barChart) {
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data) && Ext.isArray(data.chart)) {
                        var xaxis = [], series = [];
                        Ext.Array.each(data.chart, function (item, index) {
                            xaxis.push(item.name);
                            series.push(item.value);
                        });

                        if (xaxis.length == 0) xaxis.push('无数据');
                        if (series.length == 0) series.push(0);

                        barOption.xAxis[0].data = xaxis;
                        barOption.series[0].data = series;
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
            dockedItems: [{
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
                        id: 'roomfield',
                        xtype: 'RoomPicker',
                        emptyText: '默认全部'
                    }, {
                        id: 'typesfield',
                        xtype: 'DeviceTypeMultiCombo',
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
            }],
            items: [{
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
                        contentEl: 'bar-chart'
                    }
                ],
                listeners: {
                    resize: function (me, width, height, oldWidth, oldHeight) {
                        var barContainer = Ext.get('bar-chart');
                        barContainer.setHeight(height - 40);
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
                title: '设备信息',
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
                    text: '设备子类',
                    dataIndex: 'subType',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '系统名称',
                    dataIndex: 'sysName',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '系统编号',
                    dataIndex: 'sysCode',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '型号',
                    dataIndex: 'model',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '生产厂家',
                    dataIndex: 'productor',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '品牌',
                    dataIndex: 'brand',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '供应商',
                    dataIndex: 'supplier',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '维护厂家',
                    dataIndex: 'subCompany',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '开始使用时间',
                    dataIndex: 'startTime',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '预计报废时间',
                    dataIndex: 'scrapTime',
                    align: 'left',
                    width: 100,
                    sortable: true
                }, {
                    text: '使用状态',
                    dataIndex: 'status',
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

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
            
            //load data
            query(currentStore);
        }
    });

    Ext.onReady(function () {
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');

        //init charts
        barChart.setOption(barOption);
    });
})();