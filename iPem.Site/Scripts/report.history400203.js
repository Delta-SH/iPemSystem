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
                    data: [],
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
                    name: '一级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#f04b51'
                        }
                    },
                    data: []
                },
                {
                    name: '二级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#efa91f'
                        }
                    },
                    data: []
                },
                {
                    name: '三级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#f5d313'
                        }
                    },
                    data: []
                },
                {
                    name: '四级告警',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#0892cd'
                        }
                    },
                    data: []
                }
            ]
        };

    Ext.define('CategoryModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'stationid', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'level1', type: 'int' },
            { name: 'level2', type: 'int' },
            { name: 'level3', type: 'int' },
            { name: 'level4', type: 'int' },
            { name: 'total', type: 'int' }
        ],
        idProperty: 'index'
    });

    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'nmalarmid', type: 'string' },
			{ name: 'level', type: 'string' },
            { name: 'starttime', type: 'string' },
            { name: 'endtime', type: 'string' },
            { name: 'interval', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'startvalue', type: 'string' },
            { name: 'endvalue', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'device', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'confirmed', type: 'string' },
            { name: 'confirmer', type: 'string' },
            { name: 'confirmedtime', type: 'string' },
            { name: 'reservation', type: 'string' },
            { name: 'reversalcount', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'areaid', type: 'string' },
            { name: 'stationid', type: 'string' },
            { name: 'roomid', type: 'string' },
            { name: 'fsuid', type: 'string' },
            { name: 'deviceid', type: 'string' },
            { name: 'pointid', type: 'string' },
            { name: 'levelid', type: 'int' },
            { name: 'reversalid', type: 'string' }
        ],
        idProperty: 'index'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'CategoryModel',
        DownloadURL: '/Report/DownloadHistory400203',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistory400203',
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
                    chart(me.proxy.reader.jsonData)
                }
            }
        }
    });

    var detailStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        DownloadURL: '/Report/DownloadHistoryDetail400203',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistoryDetail400203',
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
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);
    var detailPagingToolbar = $$iPems.clonePagingToolbar(detailStore);

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
            flex: 2,
            layout: {
                type: 'hbox',
                align: 'stretch',
                pack: 'start'
            },
            items: [{
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
            title: '告警分类信息',
            collapsible: true,
            collapseFirst: false,
            margin: '5 0 0 0',
            flex: 3,
            store: currentStore,
            bbar: currentPagingToolbar,
            selType: 'cellmodel',
            forceFit: false,
            tools: [{
                type: 'print',
                tooltip: '数据导出',
                handler: function (event, toolEl, panelHeader) {
                    print(currentStore);
                }
            }],
            viewConfig: {
                forceFit: true,
                stripeRows: true,
                trackOver: false,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
            },
            columns: [
                {
                    text: '序号',
                    dataIndex: 'index',
                    width: 60
                },
                {
                    text: '所属区域',
                    dataIndex: 'area',
                    flex: 1
                },
                {
                    text: '所属站点',
                    dataIndex: 'station',
                    width: 150
                },
                {
                    text: '一级告警',
                    dataIndex: 'level1',
                    align: 'center',
                    renderer: function (value, p, record) {
                        return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', $$iPems.Level.Level1, value);
                    }
                },
                {
                    text: '二级告警',
                    dataIndex: 'level2',
                    align: 'center',
                    renderer: function (value, p, record) {
                        return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', $$iPems.Level.Level2, value);
                    }
                },
                {
                    text: '三级告警',
                    dataIndex: 'level3',
                    align: 'center',
                    renderer: function (value, p, record) {
                        return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', $$iPems.Level.Level3, value);
                    }
                },
                {
                    text: '四级告警',
                    dataIndex: 'level4',
                    align: 'center',
                    renderer: function (value, p, record) {
                        return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', $$iPems.Level.Level4, value);
                    }
                },
                {
                    text: '总计',
                    dataIndex: 'total',
                    align: 'center',
                    renderer: function (value, p, record) {
                        return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', $$iPems.Level.Level0, value);
                    }
                }
            ],
            listeners: {
                cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
                    var elements = Ext.fly(td).select('a.grid-link');
                    if (elements.getCount() == 0) return false;
                    detail(record.get('stationid'), elements.first().getAttribute('data'));
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
                            id: 'rangePicker',
                            xtype: 'AreaPicker',
                            fieldLabel: '查询范围',
                            emptyText: '默认全部',
                            labelWidth: 60,
                            width: 220
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
                            xtype: 'button',
                            glyph: 0xf010,
                            text: '数据导出',
                            handler: function (me, event) {
                                print(currentStore);
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

    var detailGrid = Ext.create('Ext.grid.Panel', {
        region: 'center',
        border: false,
        store: detailStore,
        bbar: detailPagingToolbar,
        viewConfig: {
            loadMask: false,
            stripeRows: true,
            trackOver: true,
            preserveScrollOnRefresh: true,
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
                text: '告警管理编号',
                dataIndex: 'nmalarmid',
                align: 'center',
                width: 150
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
                text: '告警历时',
                dataIndex: 'interval',
                align: 'center',
                width: 120
            },
            {
                text: '告警描述',
                dataIndex: 'comment'
            },
            {
                text: '开始值',
                dataIndex: 'startvalue',
                align: 'center'
            },
            {
                text: '结束值',
                dataIndex: 'endvalue',
                align: 'center'
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
                    return '工程告警';
                }
            },
            {
                text: '告警翻转',
                dataIndex: 'reversalcount',
                align: 'center'
            }
        ]
    });

    var detailWnd = Ext.create('Ext.window.Window', {
        title: '告警详情',
        glyph: 0xf029,
        height: 500,
        width: 800,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        layout: 'border',
        items: [detailGrid],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: '导出',
            handler: function (el, e) {
                print(detailStore);
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                detailWnd.hide();
            }
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
            }
        });
    };

    var print = function (store) {
        $$iPems.download({
            url: store.DownloadURL,
            params: store.getProxy().extraParams
        });
    };

    var chart = function (data) {
        if (!Ext.isEmpty(data) && !Ext.isEmpty(data.data)) {
            barOption.xAxis[0].data = [];
            barOption.series[0].data = [];
            barOption.series[1].data = [];
            barOption.series[2].data = [];
            barOption.series[3].data = [];

            Ext.Array.each(data.data, function (item, index) {
                barOption.xAxis[0].data.push(item.station);
                barOption.series[0].data.push(item.level1);
                barOption.series[1].data.push(item.level2);
                barOption.series[2].data.push(item.level3);
                barOption.series[3].data.push(item.level4);
            });
        } else {
            barOption.xAxis[0].data = ['无数据'];
            barOption.series[0].data = [0];
            barOption.series[1].data = [0];
            barOption.series[2].data = [0];
            barOption.series[3].data = [0];
        }

        barChart.setOption(barOption);
    };

    var detail = function (station, level) {
        if (Ext.isEmpty(station)) return false;
        if (Ext.isEmpty(level)) return false;

        var proxy = detailStore.getProxy();
        proxy.extraParams.station = station;
        proxy.extraParams.level = level;

        detailStore.removeAll();
        detailStore.loadPage(1);
        detailWnd.show();
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);

            //load data
            Ext.defer(query, 500);
        }
    });

    Ext.onReady(function () {
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');
        barChart.setOption(barOption);
    });
})();