(function () {
    var lineChart = null,
        lineOption = {
            tooltip: {
                trigger: 'axis',
                formatter: function (params) {
                    var xaxis = lineOption.xAxis[0].data;
                    if (xaxis.length > 0) {
                        if (!Ext.isArray(params)) params = [params];

                        var tips = [Ext.String.format('开始时间：{0}<br/>结束时间：{1}', xaxis[params[0].dataIndex].start, xaxis[params[0].dataIndex].end)];
                        Ext.Array.each(params, function (item, index) {
                            tips.push(Ext.String.format('<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:{0}"></span>{1}：{2} {3}({4})', item.color, item.seriesName, item.value, item.data.unit, item.data.time));
                        });

                        return tips.join('<br/>');
                    }

                    return 'No Data';
                }
            },
            legend: {
                data: ['最大测值', '平均测值', '最小测值']
            },
            grid: {
                top: 20,
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
                type: 'category',
                boundaryGap: false,
                splitLine: {show: false},
                data:[]
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [
                {
                    name: '最大测值',
                    type: 'line',
                    smooth: true,
                    data: []
                }, {
                    name: '平均测值',
                    type: 'line',
                    smooth: true,
                    data: []
                }, {
                    name: '最小测值',
                    type: 'line',
                    smooth: true,
                    data: []
                }
            ]
        };

    var query = function (target) {
        var device = Ext.getCmp('devicePicker'),
            point = Ext.getCmp('pointCombo'),
            start = Ext.getCmp('startField'),
            end = Ext.getCmp('endField');

        if (!device.isValid()) return;
        if (!point.isValid()) return;

        var device = device.getValue(),
            point = point.getValue(),
            starttime = start.getRawValue(),
            endtime = end.getRawValue();

        Ext.Ajax.request({
            url: '/Report/RequestChart400302',
            params: { device: device, point: point, starttime: starttime, endtime: endtime },
            mask: new Ext.LoadMask(target, { msg: '正在处理，请稍后...' }),
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    if (lineChart) {
                        var xaxis = [], series0 = [], series1 = [], series2 = [];
                        if (data.data && Ext.isArray(data.data)) {
                            Ext.Array.each(data.data, function (item, index) {
                                xaxis.push({
                                    value: item.name.replace(' ', '\n'),
                                    start: item.name,
                                    end: item.comment
                                });

                                series0.push({
                                    value: item.models[0].value,
                                    time: item.models[0].name,
                                    unit: item.models[0].comment
                                });
                                series1.push({
                                    value: item.models[1].value,
                                    time: item.models[1].name,
                                    unit: item.models[1].comment
                                });
                                series2.push({
                                    value: item.models[2].value,
                                    time: item.models[2].name,
                                    unit: item.models[2].comment
                                });
                            });
                        }

                        lineOption.xAxis[0].data = xaxis;
                        lineOption.series[0].data = series0;
                        lineOption.series[1].data = series1;
                        lineOption.series[2].data = series2;
                        lineChart.setOption(lineOption, true);
                    }
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    Ext.onReady(function () {
        var currentLayout = Ext.create('Ext.panel.Panel', {
            id: 'currentLayout',
            region: 'center',
            border: false,
            bodyCls: 'x-border-body-panel',
            layout: 'fit',
            items: [{
                xtype: 'panel',
                glyph: 0xf031,
                title: '测值统计曲线',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                layout: 'fit',
                items: [{
                    xtype: 'container',
                    contentEl: 'line-chart'
                }]
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '信号筛选条件',
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
                                            pointCombo.bind(keys[1], true, false, true, false);
                                        }
                                    }
                                }
                            },
                            {
                                id: 'pointCombo',
                                xtype: 'PointCombo',
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
                                value: Ext.ux.DateTime.addDays(Ext.ux.DateTime.today(),-1),
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

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        lineChart = echarts.init(document.getElementById("line-chart"), 'shine');

        //init charts
        lineChart.setOption(lineOption);
    });
})();