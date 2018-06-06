var barChart = null,
    pieChart = null,
    barOption = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            },
            formatter: function (params) {
                if (params === null)
                    return '';

                var tips = [];
                var total = 0;
                params.forEach(function (value, index, array) {
                    if (index === 0) {
                        tips.push(value.name);
                    }

                    total += value.value;
                    tips.push('<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' + value.color + '"></span>' + value.seriesName + '：' + value.value);
                });

                if (tips.length > 0) {
                    tips[0] = tips[0] + ' (' + total + ')';
                }

                return tips.join('<br/>');
            }
        },
        title: [{
            text: '按设备类型分组',
            x: '25%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }, {
            text: '按告警类型分组',
            x: '75%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }, {
            text: '按告警级别分组',
            x: '25%',
            y: '50%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }, {
            text: '按确认类型分组',
            x: '75%',
            y: '50%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }],
        grid: [{
            top: 30,
            width: '48%',
            bottom: '52%',
            left: 0,
            containLabel: true
        }, {
            top: 30,
            width: '48%',
            bottom: '52%',
            left: '52%',
            containLabel: true
        }, {
            top: '58%',
            width: '48%',
            bottom: 0,
            left: 0,
            containLabel: true
        }, {
            top: '58%',
            width: '48%',
            bottom: 0,
            left: '52%',
            containLabel: true
        }],
        xAxis: [{
            type: 'category',
            data: ['无数据']
        }, {
            type: 'category',
            data: ['无数据'],
            gridIndex: 1
        }, {
            type: 'category',
            data: ['无数据'],
            gridIndex: 2
        }, {
            type: 'category',
            data: ['无数据'],
            gridIndex: 3
        }],
        yAxis: [{
            type: 'value'
        }, {
            type: 'value',
            gridIndex: 1
        }, {
            type: 'value',
            gridIndex: 2
        }, {
            type: 'value',
            gridIndex: 3
        }],
        series: [{
            name: '一级告警',
            type: 'bar',
            stack: '1',
            itemStyle: {
                normal: {
                    color: '#f04b51'
                }
            },
            label: {
                normal: {
                    show: true,
                    formatter: function (params) {
                        if (params.value > 0)
                            return params.value;
                        else
                            return '';
                    }
                }
            },
            data: [0]
        },
            {
                name: '二级告警',
                type: 'bar',
                stack: '1',
                itemStyle: {
                    normal: {
                        color: '#efa91f'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '三级告警',
                type: 'bar',
                stack: '1',
                itemStyle: {
                    normal: {
                        color: '#f5d313'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '四级告警',
                type: 'bar',
                stack: '1',
                itemStyle: {
                    normal: {
                        color: '#0892cd'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '一级告警',
                type: 'bar',
                stack: '2',
                xAxisIndex: 1,
                yAxisIndex: 1,
                itemStyle: {
                    normal: {
                        color: '#f04b51'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '二级告警',
                type: 'bar',
                stack: '2',
                xAxisIndex: 1,
                yAxisIndex: 1,
                itemStyle: {
                    normal: {
                        color: '#efa91f'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '三级告警',
                type: 'bar',
                stack: '2',
                xAxisIndex: 1,
                yAxisIndex: 1,
                itemStyle: {
                    normal: {
                        color: '#f5d313'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '四级告警',
                type: 'bar',
                stack: '2',
                xAxisIndex: 1,
                yAxisIndex: 1,
                itemStyle: {
                    normal: {
                        color: '#0892cd'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '一级告警',
                type: 'bar',
                stack: '3',
                xAxisIndex: 2,
                yAxisIndex: 2,
                itemStyle: {
                    normal: {
                        color: '#f04b51'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '二级告警',
                type: 'bar',
                stack: '3',
                xAxisIndex: 2,
                yAxisIndex: 2,
                itemStyle: {
                    normal: {
                        color: '#efa91f'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '三级告警',
                type: 'bar',
                stack: '3',
                xAxisIndex: 2,
                yAxisIndex: 2,
                itemStyle: {
                    normal: {
                        color: '#f5d313'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '四级告警',
                type: 'bar',
                stack: '3',
                xAxisIndex: 2,
                yAxisIndex: 2,
                itemStyle: {
                    normal: {
                        color: '#0892cd'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '一级告警',
                type: 'bar',
                stack: '4',
                xAxisIndex: 3,
                yAxisIndex: 3,
                itemStyle: {
                    normal: {
                        color: '#f04b51'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '二级告警',
                type: 'bar',
                stack: '4',
                xAxisIndex: 3,
                yAxisIndex: 3,
                itemStyle: {
                    normal: {
                        color: '#efa91f'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            },
            {
                name: '三级告警',
                type: 'bar',
                stack: '4',
                xAxisIndex: 3,
                yAxisIndex: 3,
                itemStyle: {
                    normal: {
                        color: '#f5d313'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }, {
                name: '四级告警',
                type: 'bar',
                stack: '4',
                xAxisIndex: 3,
                yAxisIndex: 3,
                itemStyle: {
                    normal: {
                        color: '#0892cd'
                    }
                },
                label: {
                    normal: {
                        show: true,
                        formatter: function (params) {
                            if (params.value > 0)
                                return params.value;
                            else
                                return '';
                        }
                    }
                },
                data: [0]
            }]
    },
    pieOption = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        title: [{
            text: '按设备类型分组',
            x: '25%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }, {
            text: '按告警类型分组',
            x: '75%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }, {
            text: '按告警级别分组',
            x: '25%',
            y: '50%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }, {
            text: '按确认类型分组',
            x: '75%',
            y: '50%',
            textAlign: 'center',
            textStyle: {
                color: '#333',
                fontSize: 15,
                fontWeight: 'bold'
            }
        }],
        legend: [{
            type: 'scroll',
            left: 0,
            top: '5%',
            orient:'vertical',
            height: 220,
            data: ['设备类型']
        },{
            type: 'scroll',
            left: '50%',
            top: '5%',
            height: 220,
            orient:'vertical',
            data:['告警类型']
        },{
            type: 'scroll',
            left: '0',
            top: '55%',
            height: 220,
            orient:'vertical',
            data: ['告警级别']
        },{
            type: 'scroll',
            left: '50%',
            top: '55%',
            height: 220,
            orient:'vertical',
            data: ['确认类型']
        }],
        series: [{
            name:'按设备类型分组',
            type: 'pie',
            radius: ['22%', '40%'],
            center: ['25%', '27%'],
            avoidLabelOverlap: false,
            label: {
                normal: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    show: true,
                    textStyle: {
                        fontSize: '18',
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
                { name: '设备类型', value: 0 }
            ]
        }, {
            name:'按告警类型分组',
            type: 'pie',
            radius: ['22%', '40%'],
            center: ['75%', '27%'],
            avoidLabelOverlap: false,
            label: {
                normal: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    show: true,
                    textStyle: {
                        fontSize: '18',
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
                { name: '告警类型', value: 0 }
            ]
        }, {
            name:'按告警级别分组',
            type: 'pie',
            radius: ['22%', '40%'],
            center: ['25%', '77%'],
            avoidLabelOverlap: false,
            label: {
                normal: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    show: true,
                    textStyle: {
                        fontSize: '18',
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
                { name: '告警级别', value: 0 }
            ]
        }, {
            name:'按确认类型分组',
            type: 'pie',
            radius: ['22%', '40%'],
            center: ['75%', '77%'],
            avoidLabelOverlap: false,
            label: {
                normal: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    show: true,
                    textStyle: {
                        fontSize: '18',
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
                { name: '确认类型', value: 0 }
            ]
        }]
    };

var barLayout = Ext.create('Ext.panel.Panel', {
    glyph: 0xf032,
    title: '柱状图表',
    layout: 'fit',
    items: [{
        xtype: 'panel',
        border: false,
        layout: 'fit',
        items: [{
            xtype: 'container',
            margin: '5 5 5 5',
            contentEl: 'bar-chart'
        }],
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                var barContainer = Ext.get('bar-chart');
                barContainer.setHeight(height - 10);
                if (barChart) barChart.resize();
            }
        }
    }],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        border: false,
        bodyStyle: {
            'border-bottom-width': '1px !important'
        },
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'rangePicker1',
                        xtype: 'DevicePicker',
                        fieldLabel: '查询范围',
                        emptyText: '默认全部',
                        width: 280
                    },
                    {
                        id: 'alarm-level-multicombo1',
                        xtype: 'AlarmLevelMultiCombo',
                        emptyText: '默认全部',
                        width: 280
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query1();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'startField1',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField1',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    }
                ]
            }
        ]
    }]
});

var pieLayout = Ext.create('Ext.panel.Panel', {
    glyph: 0xf030,
    title: '饼状图表',
    layout: 'fit',
    items: [{
        xtype: 'panel',
        border: false,
        layout: 'fit',
        items: [{
            xtype: 'container',
            margin: '5 5 5 5',
            contentEl: 'pie-chart'
        }],
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                var pieContainer = Ext.get('pie-chart');
                pieContainer.setHeight(height - 10);
                if (pieChart) {
                    var legendHeight = height / 2 - 30;
                    pieChart.setOption({
                        legend: [{
                            height: legendHeight,
                        }, {
                            height: legendHeight
                        }, {
                            height: legendHeight
                        }, {
                            height: legendHeight
                        }]
                    });
                    pieChart.resize();
                }
            }
        }
    }],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        border: false,
        bodyStyle: {
            'border-bottom-width': '1px !important'
        },
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'rangePicker2',
                        xtype: 'DevicePicker',
                        fieldLabel: '查询范围',
                        emptyText: '默认全部',
                        width: 280
                    },
                    {
                        id: 'alarm-level-multicombo2',
                        xtype: 'AlarmLevelMultiCombo',
                        emptyText: '默认全部',
                        width: 280
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query2();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'startField2',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    },
                    {
                        id: 'endField2',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    }
                ]
            }
        ]
    }]
});

var currentLayout = Ext.create('Ext.tab.Panel', {
    border: true,
    region: 'center',
    cls: 'x-custom-panel',
    plain: true,
    items: [barLayout, pieLayout]
});

var query1 = function () {
    var parent = Ext.getCmp('rangePicker1').getValue(),
        startDate = Ext.getCmp('startField1').getRawValue(),
        endDate = Ext.getCmp('endField1').getRawValue(),
        levels = Ext.getCmp('alarm-level-multicombo1').getSelectedValues();

    Ext.Ajax.request({
        url: '/Report/RequestChart400304_1',
        params: { parent: parent, levels: levels, startDate: startDate, endDate: endDate },
        mask: new Ext.LoadMask(barLayout, { msg: '正在处理...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                if (barChart) {
                    var xaxis0 = [], xaxis1 = [], xaxis2 = [], xaxis3 = [],
                        series00 = [], series01 = [], series02 = [], series03 = [],
                        series10 = [], series11 = [], series12 = [], series13 = [],
                        series20 = [], series21 = [], series22 = [], series23 = [],
                        series30 = [], series31 = [], series32 = [], series33 = [];
                    if (!Ext.isEmpty(data.data) && Ext.isArray(data.data)) {
                        var models = data.data;
                        if (models.length === 4) {
                            Ext.Array.each(models[0], function (model, index) {
                                xaxis0.push(model.name);
                                var values = model.models;
                                if (values.length === 4) {
                                    series00.push(values[0].value);
                                    series01.push(values[1].value);
                                    series02.push(values[2].value);
                                    series03.push(values[3].value);
                                }
                            });

                            Ext.Array.each(models[1], function (model, index) {
                                xaxis1.push(model.name);
                                var values = model.models;
                                if (values.length === 4) {
                                    series10.push(values[0].value);
                                    series11.push(values[1].value);
                                    series12.push(values[2].value);
                                    series13.push(values[3].value);
                                }
                            });

                            Ext.Array.each(models[2], function (model, index) {
                                xaxis2.push(model.name);
                                var values = model.models;
                                if (values.length === 4) {
                                    series20.push(values[0].value);
                                    series21.push(values[1].value);
                                    series22.push(values[2].value);
                                    series23.push(values[3].value);
                                }
                            });

                            Ext.Array.each(models[3], function (model, index) {
                                xaxis3.push(model.name);
                                var values = model.models;
                                if (values.length === 4) {
                                    series30.push(values[0].value);
                                    series31.push(values[1].value);
                                    series32.push(values[2].value);
                                    series33.push(values[3].value);
                                }
                            });
                        }
                    }

                    //#region chart1
                    if (xaxis0.length == 0) xaxis0.push('无数据')
                    barOption.xAxis[0].data = xaxis0;

                    if (series00.length == 0) series00.push(0);
                    barOption.series[0].data = series00;

                    if (series01.length == 0) series01.push(0);
                    barOption.series[1].data = series01;

                    if (series02.length == 0) series02.push(0);
                    barOption.series[2].data = series02;

                    if (series03.length == 0) series03.push(0);
                    barOption.series[3].data = series03;
                    //#endregion

                    //#region chart2
                    if (xaxis1.length == 0) xaxis1.push('无数据');
                    barOption.xAxis[1].data = xaxis1;

                    if (series10.length == 0) series10.push(0);
                    barOption.series[4].data = series10;

                    if (series11.length == 0) series11.push(0);
                    barOption.series[5].data = series11;

                    if (series12.length == 0) series12.push(0);
                    barOption.series[6].data = series12;

                    if (series13.length == 0) series13.push(0);
                    barOption.series[7].data = series13;
                    //#endregion

                    //#region chart3
                    if (xaxis2.length == 0) xaxis2.push('无数据');
                    barOption.xAxis[2].data = xaxis2;

                    if (series20.length == 0) series20.push(0);
                    barOption.series[8].data = series20;

                    if (series21.length == 0) series21.push(0);
                    barOption.series[9].data = series21;

                    if (series22.length == 0) series22.push(0);
                    barOption.series[10].data = series22;

                    if (series23.length == 0) series23.push(0);
                    barOption.series[11].data = series23;
                    //#endregion

                    //#region chart4
                    if (xaxis3.length == 0) xaxis3.push('无数据');
                    barOption.xAxis[3].data = xaxis3;

                    if (series30.length == 0) series30.push(0);
                    barOption.series[12].data = series30;

                    if (series31.length == 0) series31.push(0);
                    barOption.series[13].data = series31;

                    if (series32.length == 0) series32.push(0);
                    barOption.series[14].data = series32;

                    if (series33.length == 0) series33.push(0);
                    barOption.series[15].data = series33;
                    //#endregion

                    barChart.setOption(barOption, true);
                }
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
};

var query2 = function () {
    var parent = Ext.getCmp('rangePicker2').getValue(),
        startDate = Ext.getCmp('startField2').getRawValue(),
        endDate = Ext.getCmp('endField2').getRawValue(),
        levels = Ext.getCmp('alarm-level-multicombo2').getSelectedValues();

    Ext.Ajax.request({
        url: '/Report/RequestChart400304_2',
        params: { parent: parent, levels: levels, startDate: startDate, endDate: endDate },
        mask: new Ext.LoadMask(pieLayout, { msg: '正在处理...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                if (pieChart) {
                    var legend0 = [], legend1 = [], legend2 = [], legend3 = [],
                        series0 = [], series1 = [], series2 = [], series3 = [];
                    if (!Ext.isEmpty(data.data) && Ext.isArray(data.data)) {
                        var models = data.data;
                        if (models.length === 4) {
                            Ext.Array.each(models[0], function (model, index) {
                                Ext.Array.each(model, function (item, index) {
                                    legend0.push(item.name);
                                    series0.push({ name: item.name, value: item.value });
                                });
                            });

                            Ext.Array.each(models[1], function (model, index) {
                                Ext.Array.each(model, function (item, index) {
                                    legend1.push(item.name);
                                    series1.push({ name: item.name, value: item.value });
                                });
                            });

                            Ext.Array.each(models[2], function (model, index) {
                                Ext.Array.each(model, function (item, index) {
                                    legend2.push(item.name);
                                    series2.push({ name: item.name, value: item.value });
                                });
                            });

                            Ext.Array.each(models[3], function (model, index) {
                                Ext.Array.each(model, function (item, index) {
                                    legend3.push(item.name);
                                    series3.push({ name: item.name, value: item.value });
                                });
                            });
                        }
                    }

                    if (legend0.length === 0) legend0.push('设备类型');
                    if (series0.length === 0) series0.push({ name: '设备类型', value: 0 });
                    if (legend1.length === 0) legend1.push('告警类型');
                    if (series1.length === 0) series1.push({ name: '告警类型', value: 0 });
                    if (legend2.length === 0) legend2.push('告警级别');
                    if (series2.length === 0) series2.push({ name: '告警级别', value: 0 });
                    if (legend3.length === 0) legend3.push('确认类型');
                    if (series3.length === 0) series3.push({ name: '确认类型', value: 0 });

                    pieOption.legend[0].data = legend0;
                    pieOption.legend[1].data = legend1;
                    pieOption.legend[2].data = legend2;
                    pieOption.legend[3].data = legend3;
                    pieOption.series[0].data = series0;
                    pieOption.series[1].data = series1;
                    pieOption.series[2].data = series2;
                    pieOption.series[3].data = series3;
                    pieChart.setOption(pieOption, true);
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
    barChart = echarts.init(document.getElementById("bar-chart"), 'shine');
    barChart.setOption(barOption);

    pieChart = echarts.init(document.getElementById("pie-chart"), 'shine');
    pieChart.setOption(pieOption);
});