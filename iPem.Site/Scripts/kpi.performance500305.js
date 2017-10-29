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
                    name: '站点A',
                    type: 'bar',
                    data: [0]
                },
                {
                    name: '站点B',
                    type: 'bar',
                    data: [0]
                }
            ]
        };

    Ext.define('ReportModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'period', type: 'string' },
            { name: 'Aname', type: 'string' },
            { name: 'Bname', type: 'string' },
            { name: 'Avalue', type: 'float' },
            { name: 'Bvalue', type: 'float' },
            { name: 'increase', type: 'float' },
            { name: 'rate', type: 'string' }
        ],
        idProperty: 'index'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ReportModel',
        proxy: {
            type: 'ajax',
            url: '/KPI/Request500305',
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
                    if (!Ext.isEmpty(data) && Ext.isArray(data.chart)) {
                        Ext.Array.each(data.chart, function (item) {
                            xaxis.push(item.name);
                            series0.push(item.models[0].value);
                            series1.push(item.models[1].value);
                        });
                    }

                    if (xaxis.length == 0) xaxis.push('无数据');
                    if (series0.length == 0) series0.push(0);
                    if (series1.length == 0) series1.push(0);

                    barOption.xAxis[0].data = xaxis;
                    barOption.series[0].data = series0;
                    barOption.series[1].data = series1;
                    barChart.setOption(barOption, true);
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
            glyph: 0xf031,
            title: '对比图表',
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
            title: '能耗信息',
            collapsible: true,
            collapseFirst: false,
            margin: '5 0 0 0',
            flex: 1,
            store: currentStore,
            forceFit: false,
            viewConfig: {
                forceFit: true,
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
                text: '时段',
                dataIndex: 'period',
                flex: 1,
                align: 'center',
                sortable: true
            }, {
                text: '站点A',
                dataIndex: 'Aname',
                align: 'left',
                width: 150,
                sortable: true
            }, {
                text: '站点B',
                dataIndex: 'Bname',
                align: 'left',
                width: 150,
                sortable: true
            }, {
                text: '站点A能耗(kW·h)',
                dataIndex: 'Avalue',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '站点B能耗(kW·h)',
                dataIndex: 'Bvalue',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '对比值A-B(kW·h)',
                dataIndex: 'increase',
                width: 150,
                align: 'left',
                sortable: true
            }, {
                text: '对比幅度(A-B)/B',
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
            title: '筛选条件',
            collapsible: true,
            collapsed: false,
            dock: 'top',
            items: [{
                xtype: 'toolbar',
                border: false,
                items: [{
                    id: 'rangePicker',
                    xtype: 'StationMultiPicker',
                    allowBlank: false,
                    selectOnLeaf: true,
                    emptyText: '请勾选两个对比站点...',
                    fieldLabel: '对比站点',
                    width: 280
                }, {
                    id: 'periodCombo',
                    xtype: 'combobox',
                    fieldLabel: '统计周期',
                    displayField: 'text',
                    valueField: 'id',
                    typeAhead: true,
                    queryMode: 'local',
                    triggerAction: 'all',
                    selectOnFocus: true,
                    forceSelection: true,
                    labelWidth: 60,
                    width: 280,
                    value: $$iPems.Period.Month,
                    store: Ext.create('Ext.data.Store', {
                        fields: ['id', 'text'],
                        data: [
                            { id: $$iPems.Period.Month, text: '按月统计' },
                            { id: $$iPems.Period.Week, text: '按周统计' },
                            { id: $$iPems.Period.Day, text: '按日统计' },
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
                    value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.YEAR, -1),
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
            period = Ext.getCmp('periodCombo'),
            start = Ext.getCmp('startField'),
            end = Ext.getCmp('endField');

        if (!range.isValid()) return;
        if (!start.isValid()) return;
        if (!end.isValid()) return;

        var ranges = range.getValue();
        if (ranges.length < 2) {
            Ext.Msg.show({ title: '系统提示', msg: '请勾选两个对比站点', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
            return false;
        }

        if (ranges.length > 2) {
            Ext.Msg.show({ title: '系统提示', msg: '仅支持两个对比站点', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
            return false;
        }

        var me = currentStore, proxy = me.getProxy();
        proxy.extraParams.parents = range.getValue();
        proxy.extraParams.period = period.getValue();
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
            url: '/KPI/Download500305',
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
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');
        barChart.setOption(barOption);
    });
})();