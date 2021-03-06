﻿var lineChart = null,
    lineOption = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'cross'
            },
            formatter: function (params) {
                if (lineOption.series.length > 0) {
                    if (!Ext.isArray(params))
                        params = [params];

                    var tips = [];
                    Ext.Array.each(params, function (item, index) {
                        tips.push(Ext.String.format('<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:{0}"></span>{1}<br/>信号测值：{2} {3}<br/>测值时间：{4}', item.color, item.seriesName, item.value[1], item.data.unit, item.value[0]));
                    });

                    return tips.join('<br/>');
                }

                return '无数据';
            }
        },
        legend: {
            type: 'scroll',
            left: 'center',
            top: 'top',
            data: []
        },
        grid: {
            top: 40,
            left: 20,
            right: 20,
            bottom: 40,
            containLabel: true
        },
        dataZoom: [
            {
                type: 'inside',
                start: 0,
                end: 100
            },
            {
                type: 'slider',
                show: true,
                start: 0,
                end: 100
            }
        ],
        xAxis: [{
            type: 'time',
            boundaryGap: false,
            splitLine: { show: false }
        }],
        yAxis: [{
            type: 'value',
            min: function (value) {
                return (value.min - (value.max - value.min) / 2).toFixed(2);
            }
        }],
        series: []
    };

var currentLayout = Ext.create('Ext.panel.Panel', {
    id: 'currentLayout',
    region: 'center',
    border: false,
    bodyCls: 'x-border-body-panel',
    layout: 'fit',
    items: [{
        xtype: 'panel',
        glyph: 0xf031,
        title: '测值曲线',
        collapsible: true,
        collapseFirst: false,
        margin: '5 0 0 0',
        layout: 'fit',
        items: [{
            xtype: 'container',
            contentEl: 'line-chart'
        }],
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                var lineContainer = Ext.get('line-chart');
                lineContainer.setHeight(height - 40);
                if (lineChart) lineChart.resize();
            }
        }
    }],
    dockedItems: [{
        xtype: 'panel',
        glyph: 0xf034,
        title: '筛选条件',
        collapsible: true,
        collapsed: false,
        dock: 'top',
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'devicePicker',
                        xtype: 'DevicePicker',
                        allowBlank: false,
                        emptyText: '请选择设备...',
                        selectOnLeaf: true,
                        selectAll: false,
                        listeners: {
                            select: function (me, record) {
                                var keys = $$iPems.SplitKeys(record.data.id);
                                if (keys.length == 2) {
                                    var pointCombo = Ext.getCmp('pointCombo');
                                    pointCombo.bind(keys[1], true, false, true, false, false);
                                }
                            }
                        }
                    },
                    {
                        id: 'pointCombo',
                        xtype: 'PointMultiCombo',
                        allowBlank: false,
                        emptyText: '请选择信号...',
                        labelWidth: 60,
                        width: 280,
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query(currentLayout);
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'startField',
                        xtype: 'datetimepicker',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.ux.DateTime.addDays(Ext.ux.DateTime.today(), -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField',
                        xtype: 'datetimepicker',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.ux.DateTime.addSeconds(Ext.ux.DateTime.today(), -1),
                        editable: false,
                        allowBlank: false
                    }
                ]
            }
        ]
    }]
});

var query = function () {
    var device = Ext.getCmp('devicePicker'),
        points = Ext.getCmp('pointCombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

    if (!device.isValid()) return;
    if (!points.isValid()) return;

    var device = device.getValue(),
        points = points.getValue(),
        startDate = start.getRawValue(),
        endDate = end.getRawValue();

    Ext.Ajax.request({
        url: '/Report/RequestChart400301',
        params: { device: device, points: points, startDate: startDate, endDate: endDate },
        mask: new Ext.LoadMask(currentLayout, { msg: '正在处理...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                if (lineChart) {
                    var legend = [], series = [];
                    if (!Ext.isEmpty(data.data) && Ext.isArray(data.data)) {
                        Ext.Array.each(data.data, function (item, index) {
                            legend.push(item.name);
                            var models = [];
                            Ext.Array.each(item.models, function (model) {
                                models.push({
                                    value: [model.name, model.value],
                                    unit: model.comment
                                });
                            });

                            series.push({
                                name: item.name,
                                type: 'line',
                                smooth: true,
                                showSymbol: false,
                                data: models
                            });
                        });
                    }

                    lineOption.legend.data = legend;
                    lineOption.series = series;
                    lineChart.setOption(lineOption, true);
                }
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
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
    lineChart = echarts.init(document.getElementById("line-chart"), 'shine');
    lineChart.setOption(lineOption);
});