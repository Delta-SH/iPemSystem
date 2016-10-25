(function () {
    var lineChart = null,
        lineOption = {
            tooltip: {
                trigger: 'axis',
                formatter: '{b}<br/>{c} {a}'
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
                splitLine: { show: false },
                data: []
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [
                {
                    name: '',
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
                    data: [ ]
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
            url: '/Report/RequestChart400301',
            params: { device: device, point: point, starttime: starttime, endtime: endtime },
            mask: new Ext.LoadMask(target, { msg: '正在处理...' }),
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    if (lineChart) {
                        var xaxis = [], series = [], unit = '';
                        if (data.data && Ext.isArray(data.data)) {
                            Ext.Array.each(data.data, function (item, index) {
                                xaxis.push(item.name);
                                series.push(item.value);
                                unit = item.comment;
                            });
                        }

                        lineOption.series[0].name = unit;
                        lineOption.series[0].data = series;
                        lineOption.xAxis[0].data = xaxis.map(function (str) {
                            return str.replace(' ', '\n');
                        });
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
                title: '信号测值曲线',
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