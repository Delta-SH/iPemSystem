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
                data: ['超短告警', '正常告警']
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
                    data: [
                        {
                            value: 0,
                            name: '超短告警',
                            itemStyle: {
                                normal: {
                                    color: '#c12e34'
                                }
                            }
                        },
                        {
                            value: 0,
                            name: '正常告警',
                            itemStyle: {
                                normal: {
                                    color: '#48ac2e'
                                }
                            }
                        }
                    ]
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
                    data: ['一级告警', '二级告警', '三级告警', '四级告警'],
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
                    name: '超短告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#c12e34'
                        }
                    },
                    data: [0, 0, 0, 0]
                },
                {
                    name: '正常告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#48ac2e'
                        }
                    },
                    data: [0, 0, 0, 0]
                }
            ]
        };

    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'id', type: 'string' },
            { name: 'index', type: 'int' },
			{ name: 'level', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'starttime', type: 'string' },
            { name: 'endtime', type: 'string' },
            { name: 'nmalarmid', type: 'string' },
            { name: 'interval', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'device', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'supporter', type: 'string' },
            { name: 'manager', type: 'string' },
            { name: 'confirmed', type: 'string' },
            { name: 'confirmer', type: 'string' },
            { name: 'confirmedtime', type: 'string' },
            { name: 'reservation', type: 'string' },
            { name: 'reversalcount', type: 'int' },
            { name: 'areaid', type: 'string' },
            { name: 'stationid', type: 'string' },
            { name: 'roomid', type: 'string' },
            { name: 'fsuid', type: 'string' },
            { name: 'deviceid', type: 'string' },
            { name: 'pointid', type: 'string' },
            { name: 'levelid', type: 'int' },
            { name: 'reversalid', type: 'string' }
        ],
        idProperty: 'id'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestCustom400402',
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
                    chart(me.proxy.reader.jsonData);
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
            title: '告警占比',
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
            xtype: 'grid',
            glyph: 0xf029,
            title: '告警信息',
            collapsible: true,
            collapseFirst: false,
            margin: '5 0 0 0',
            flex: 3,
            store: currentStore,
            bbar: currentPagingToolbar,
            viewConfig: {
                loadMask: true,
                stripeRows: true,
                trackOver: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                getRowClass: function (record, rowIndex, rowParams, store) {
                    return $$iPems.GetLevelCls(record.get("levelid"));
                }
            },
            columns: [
                {
                    text: '序号',
                    dataIndex: 'index',
                    width: 60
                },
                {
                    text: '告警级别',
                    dataIndex: 'level',
                    align: 'center',
                    tdCls: 'x-level-cell'
                },
                {
                    text: '开始时间',
                    dataIndex: 'starttime',
                    align: 'center',
                    width: 150
                },
                {
                    text: '结束时间',
                    dataIndex: 'endtime',
                    align: 'center',
                    width: 150
                },
                {
                    text: '告警标准化名称',
                    dataIndex: 'name',
                    width: 150
                },
                {
                    text: '管理编号',
                    dataIndex: 'nmalarmid',
                    align: 'center',
                    width: 150
                },
                {
                    text: '告警历时',
                    dataIndex: 'interval',
                    align: 'center',
                    width: 120
                },
                {
                    text: '信号名称',
                    dataIndex: 'point'
                },
                {
                    text: '所属设备',
                    dataIndex: 'device'
                },
                {
                    text: '所属机房',
                    dataIndex: 'room'
                },
                {
                    text: '所属站点',
                    dataIndex: 'station'
                },
                {
                    text: '所属区域',
                    dataIndex: 'area'
                },
                {
                    text: '代维公司',
                    dataIndex: 'supporter'
                },
                {
                    text: '代维负责人',
                    dataIndex: 'manager'
                },
                {
                    text: '确认状态',
                    dataIndex: 'confirmed',
                    align: 'center'
                },
                {
                    text: '确认人员',
                    dataIndex: 'confirmer',
                    align: 'center'
                },
                {
                    text: '确认时间',
                    dataIndex: 'confirmedtime',
                    align: 'center'
                },
                {
                    text: '工程状态',
                    dataIndex: 'reservation',
                    align: 'center',
                    renderer: function (value, p, record) {
                        if (Ext.isEmpty(value)) return Ext.emptyString;
                        return '<a class="grid-link" href="javascript:void(0);">查看</a>';
                    }
                },
                {
                    text: '告警翻转',
                    dataIndex: 'reversalcount',
                    align: 'center'
                }
            ]
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
                            id: 'rangePicker',
                            xtype: 'DevicePicker',
                            fieldLabel: '查询范围',
                            emptyText: '默认全部',
                            labelWidth: 60,
                            width: 220,
                        },
                        {
                            id: 'startField',
                            xtype: 'datefield',
                            fieldLabel: '开始时间',
                            labelWidth: 60,
                            width: 220,
                            value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                            editable: false,
                            allowBlank: false
                        },
                        {
                            id: 'endField',
                            xtype: 'datefield',
                            fieldLabel: '结束时间',
                            labelWidth: 60,
                            width: 220,
                            value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                            editable: false,
                            allowBlank: false
                        },
                        {
                            xtype: 'button',
                            glyph: 0xf005,
                            text: '数据查询',
                            handler: function (me, event) {
                                query();
                            }
                        }
                    ]
                },
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'station-type-multicombo',
                            xtype: 'StationTypeMultiCombo',
                            emptyText: '默认全部'
                        },
                        {
                            id: 'room-type-multicombo',
                            xtype: 'RoomTypeMultiCombo',
                            emptyText: '默认全部'
                        },
                        {
                            id: 'subdevice-type-multipicker',
                            xtype: 'SubDeviceTypeMultiPicker',
                            emptyText: '默认全部',
                            width: 220
                        },
                        {
                            id: 'exportButton',
                            xtype: 'button',
                            glyph: 0xf010,
                            text: '数据导出',
                            disabled: true,
                            handler: function (me, event) {
                                print();
                            }
                        }
                    ]
                },
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'sublogic-type-multipicker',
                            xtype: 'SubLogicTypeMultiPicker',
                            emptyText: '默认全部',
                            width: 220
                        },
                        {
                            id: 'point-multipicker',
                            xtype: 'PointMultiPicker',
                            emptyText: '默认全部',
                            width: 220
                        },
                        {
                            id: 'alarm-level-multicombo',
                            xtype: 'AlarmLevelMultiCombo',
                            emptyText: '默认全部'
                        },
                        {
                            id: 'other-option-button',
                            xtype: 'button',
                            text: '其他选项',
                            menu: [
                                {
                                    id: 'confirm-menu',
                                    xtype: 'menucheckitem',
                                    text: '已确认告警',
                                    checked: false,
                                    checkHandler: function (me, checked) {
                                        if (checked) {
                                            Ext.getCmp('unconfirm-menu').setChecked(false);
                                        }
                                    }
                                },
                                {
                                    id: 'unconfirm-menu',
                                    xtype: 'menucheckitem',
                                    text: '未确认告警',
                                    checked: false,
                                    checkHandler: function (me, checked) {
                                        if (checked) {
                                            Ext.getCmp('confirm-menu').setChecked(false);
                                        }
                                    }
                                },
                                '-',
                                {
                                    id: 'project-menu',
                                    xtype: 'menucheckitem',
                                    text: '工程告警',
                                    checked: false,
                                    checkHandler: function (me, checked) {
                                        if (checked) {
                                            Ext.getCmp('unproject-menu').setChecked(false);
                                        }
                                    }
                                },
                                {
                                    id: 'unproject-menu',
                                    xtype: 'menucheckitem',
                                    text: '非工程告警',
                                    checked: false,
                                    checkHandler: function (me, checked) {
                                        if (checked) {
                                            Ext.getCmp('project-menu').setChecked(false);
                                        }
                                    }
                                }
                            ]
                        }
                    ]
                }
            ]
        }]
    });

    var query = function () {
        var parent = Ext.getCmp('rangePicker').getValue(),
            startDate = Ext.getCmp('startField').getRawValue(),
            endDate = Ext.getCmp('endField').getRawValue(),
            staTypes = Ext.getCmp('station-type-multicombo').getSelectedValues(),
            roomTypes = Ext.getCmp('room-type-multicombo').getSelectedValues(),
            subDeviceTypes = Ext.getCmp('subdevice-type-multipicker').getValue(),
            subLogicTypes = Ext.getCmp('sublogic-type-multipicker').getValue(),
            points = Ext.getCmp('point-multipicker').getValue(),
            levels = Ext.getCmp('alarm-level-multicombo').getSelectedValues(),
            comfirmMenu = Ext.getCmp('confirm-menu'),
            unconfirmMenu = Ext.getCmp('unconfirm-menu'),
            projectMenu = Ext.getCmp('project-menu'),
            unprojectMenu = Ext.getCmp('unproject-menu'),
            proxy = currentStore.getProxy();

        proxy.extraParams.parent = parent;
        proxy.extraParams.startDate = startDate;
        proxy.extraParams.endDate = endDate;
        proxy.extraParams.staTypes = staTypes;
        proxy.extraParams.roomTypes = roomTypes;
        proxy.extraParams.subDeviceTypes = subDeviceTypes;
        proxy.extraParams.subLogicTypes = subLogicTypes;
        proxy.extraParams.points = points;
        proxy.extraParams.levels = levels;
        proxy.extraParams.cache = false;

        proxy.extraParams.confirm = -1;
        if (comfirmMenu.checked)
            proxy.extraParams.confirm = 1;
        if (unconfirmMenu.checked)
            proxy.extraParams.confirm = 0;

        proxy.extraParams.project = -1;
        if (projectMenu.checked)
            proxy.extraParams.project = 1;
        if (unprojectMenu.checked)
            proxy.extraParams.project = 0;

        currentStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    };

    var print = function () {
        $$iPems.download({
            url: '/Report/DownloadCustom400402',
            params: currentStore.getProxy().extraParams
        });
    };

    var chart = function (data) {
        var total0 = 0, total1 = 0, data0 = [0, 0, 0, 0], data1 = [0, 0, 0, 0];
        if (!Ext.isEmpty(data) && !Ext.isEmpty(data.chart) && Ext.isArray(data.chart)) {
            Ext.Array.each(data.chart, function (item, index) {
                total0 += item.models[0].value;
                total1 += item.models[1].value;

                if (item.index == $$iPems.Level.Level1) {
                    data0[0] = item.models[0].value;
                    data1[0] = item.models[1].value;
                } else if (item.index == $$iPems.Level.Level2) {
                    data0[1] = item.models[0].value;
                    data1[1] = item.models[1].value;
                } else if (item.index == $$iPems.Level.Level3) {
                    data0[2] = item.models[0].value;
                    data1[2] = item.models[1].value;
                } else if (item.index == $$iPems.Level.Level4) {
                    data0[3] = item.models[0].value;
                    data1[3] = item.models[1].value;
                }
            });
        }

        pieOption.series[0].data[0].value = total0;
        pieOption.series[0].data[1].value = total1;
        pieChart.setOption(pieOption);

        barOption.series[0].data = data0;
        barOption.series[1].data = data1;
        barChart.setOption(barOption);
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
        pieChart.setOption(pieOption);
        barChart.setOption(barOption);
    });
})();