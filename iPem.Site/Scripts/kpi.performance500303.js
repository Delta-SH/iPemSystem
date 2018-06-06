var now = new Date(),
    drawDone = false,
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
            data: ['能耗']
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
                    { name: '能耗', value: 0 }
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
                data: ['能耗'],
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
        series: [{
            name: '能耗',
            type: 'bar',
            stack: 'one',
            data: [0]
        }]
    };

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    fields: [],
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
            if (successful === true && drawDone === false) {
                dochart(me.proxy.reader.jsonData);
                drawDone = true;
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
        id: 'energy-grid',
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
        columns: [],
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
                xtype: 'RoomPicker',
                selectAll: true,
                allowBlank: false,
                emptyText: '请选择查询范围...',
                fieldLabel: '查询范围',
                width: 280,
            },
            {
                id: 'room-type-multicombo',
                xtype: 'RoomTypeMultiCombo',
                width: 280,
                emptyText: '默认全部'
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
                id: 'energy-multicombo',
                xtype: 'EnergyMultiCombo',
                width: 280,
                emptyText: '默认全部'
            },
            {
                id: 'periodCombo',
                xtype: 'PeriodCombo',
                year: false,
                week: false,
                width: 280
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
            }]
        }]
    }]
});

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        types = Ext.getCmp('room-type-multicombo'),
        energies = Ext.getCmp('energy-multicombo'),
        period = Ext.getCmp('periodCombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField'),
        grid = Ext.getCmp('energy-grid'),
        view = grid.getView();

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getSelectedValues();
    proxy.extraParams.energies = energies.getSelectedValues();
    proxy.extraParams.period = period.getValue();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;

    drawDone = false;
    Ext.Ajax.request({
        url: '/KPI/GetFields500303',
        params: proxy.extraParams,
        mask: new Ext.LoadMask({ target: view, msg: '获取列名...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                me.model.prototype.fields.clear();
                me.removeAll();
                var fields = [];
                if (data.data && Ext.isArray(data.data)) {
                    Ext.Array.each(data.data, function (item, index) {
                        me.model.prototype.fields.replace({ name: item.name, type: item.type });
                        if (!Ext.isEmpty(item.column)) {
                            fields.push({
                                text: item.column,
                                dataIndex: item.name,
                                width: item.width,
                                align: item.align
                            });
                        }
                    });
                }

                grid.reconfigure(me, fields);
                me.loadPage(1, {
                    callback: function (records, operation, success) {
                        proxy.extraParams.cache = success;
                        Ext.getCmp('exportButton').setDisabled(success === false);
                    }
                });
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/KPI/Download500303',
        params: currentStore.getProxy().extraParams
    });
};

var dochart = function (data) {
    if (pieChart && barChart) {
        var bar_xaxis = [], bar_series = [], pie_series = [], pie_legend = [];
        if (!Ext.isEmpty(data) && Ext.isArray(data.chart)) {
            Ext.Array.each(data.chart, function (item) {
                bar_xaxis.push(item.name);
                Ext.Array.each(item.models, function (model) {
                    var bar_serie = Ext.Array.findBy(bar_series, function (serie) {
                        return (serie.name === model.name)
                    });

                    if (bar_serie == null) {
                        bar_series.push({
                            name: model.name,
                            type: 'bar',
                            stack: 'one',
                            data: [model.value]
                        });
                    } else {
                        bar_serie.data.push(model.value);
                    }

                    var pie_serie = Ext.Array.findBy(pie_series, function (serie) {
                        return (serie.name === model.name)
                    });

                    if (pie_serie == null) {
                        pie_series.push({ name: model.name, value: model.value });
                    } else {
                        pie_serie.value += model.value;
                    }
                });
            });
        }

        if (bar_xaxis.length == 0) {
            bar_xaxis.push('无数据');
        }

        if (bar_series.length == 0) {
            bar_series.push({
                name: '能耗',
                type: 'bar',
                stack: 'one',
                data: [0]
            });
        }

        if (pie_series.length > 0) {
            Ext.Array.each(pie_series, function (serie) {
                pie_legend.push(serie.name);
            });
        } else {
            pie_series.push({ name: '能耗', value: 0 });
            pie_legend.push('能耗');
        }

        barOption.xAxis[0].data = bar_xaxis;
        barOption.series = bar_series;
        barChart.setOption(barOption, true);

        pieOption.series[0].data = pie_series;
        pieOption.legend.data = pie_legend;
        pieChart.setOption(pieOption);
    }
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