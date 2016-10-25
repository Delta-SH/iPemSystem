(function () {
    var now = new Date(),
        barChart = null,
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
                    data: [],
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
                    name: '当前能耗',
                    type: 'bar',
                    data: []
                },
                {
                    name: '去年同期',
                    type: 'bar',
                    data: []
                }
            ]
        };

    Ext.define('ReportModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'name', type: 'string' },
            { name: 'period', type: 'string' },
            { name: 'current', type: 'float' },
            { name: 'last', type: 'float' },
            { name: 'increase', type: 'float' },
            { name: 'rate', type: 'string' }
        ],
        idProperty: 'index'
    });

    var query = function (store) {
        var range = Ext.getCmp('rangePicker'),
            size = Ext.getCmp('sizeCombo'),
            start = Ext.getCmp('startField'),
            end = Ext.getCmp('endField');

        if (!range.isValid()) return;
        if (!start.isValid()) return;
        if (!end.isValid()) return;

        store.proxy.extraParams.parent = range.getValue();
        store.proxy.extraParams.size = size.getValue();
        store.proxy.extraParams.startDate = start.getRawValue();
        store.proxy.extraParams.endDate = end.getRawValue();
        store.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/KPI/Download500303',
            params: store.proxy.extraParams
        });
    };

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ReportModel',
        proxy: {
            type: 'ajax',
            url: '/KPI/Request500303',
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
                    var xaxis = [], series0 = [], series1 = [];
                    if (!Ext.isEmpty(data) && !Ext.isEmpty(data.chart)) {
                        Ext.Array.each(data.chart, function (item) {
                            xaxis.push(item.name);
                            series0.push(item.value);
                            series1.push(parseFloat(item.comment));
                        });
                    }

                    barOption.xAxis[0].data = xaxis;
                    barOption.series[0].data = series0;
                    barOption.series[1].data = series1;
                    barChart.setOption(barOption, true);
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
            bodyCls: 'x-border-body-panel',
            layout: {
                type: 'vbox',
                align: 'stretch',
                pack: 'start'
            },
            items: [{
                xtype: 'panel',
                glyph: 0xf031,
                title: '能耗同比',
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
                xtype: 'grid',
                glyph: 0xf029,
                title: '能耗同址同比',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                store: currentStore,
                tools: [{
                    type: 'print',
                    tooltip: '数据导出',
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
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
                    width: 150,
                    sortable: true
                }, {
                    text: '时段',
                    dataIndex: 'period',
                    width: 150,
                    align: 'left',
                    sortable: true
                }, {
                    text: '当前能耗(kW·h)',
                    dataIndex: 'current',
                    width: 150,
                    align: 'left',
                    sortable: true
                }, {
                    text: '去年同期(kW·h)',
                    dataIndex: 'last',
                    width: 150,
                    align: 'left',
                    sortable: true
                }, {
                    text: '同比增量(kW·h)',
                    dataIndex: 'increase',
                    width: 150,
                    align: 'left',
                    sortable: true
                }, {
                    text: '同比增幅',
                    dataIndex: 'rate',
                    width: 150,
                    align: 'left',
                    sortable: true
                }],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '能耗筛选条件',
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
                        value: $$iPems.Organization.Area,
                        store: Ext.create('Ext.data.Store', {
                            fields: ['id', 'text'],
                            data: [
                                { id: $$iPems.Organization.Area, text: '区域' },
                                { id: $$iPems.Organization.Station, text: '站点' },
                                { id: $$iPems.Organization.Room, text: '机房' },
                            ]
                        })
                    }, {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query(currentStore);
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
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        handler: function (me, event) {
                            print(currentStore);
                        }
                    }]
                }]
            }]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');

        //init charts
        barChart.setOption(barOption);
    });
})();