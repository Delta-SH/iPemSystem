﻿(function () {
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
            series: []
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
        fields: [],
        DownloadURL: '/Report/DownloadHistory400204',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistory400204',
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
        DownloadURL: '/Report/DownloadHistoryDetail400204',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistoryDetail400204',
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

    var chartPanel = Ext.create('Ext.panel.Panel', {
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
    });

    var gridPanel = Ext.create('Ext.grid.Panel', {
        glyph: 0xf029,
        title: '设备告警信息',
        collapsible: true,
        collapseFirst: false,
        margin: '5 0 0 0',
        flex: 3,
        store: currentStore,
        bbar: currentPagingToolbar,
        selType: 'cellmodel',
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: false,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [],
        listeners: {
            cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
                var elements = Ext.fly(td).select('a.grid-link');
                if (elements.getCount() == 0) return false;
                detail(record.get('stationid'), elements.first().getAttribute('data'));
            }
        }
    });

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
        items: [chartPanel, gridPanel],
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
                            id: 'device-type-multicombo',
                            xtype: 'DeviceTypeMultiCombo',
                            emptyText: '默认全部'
                        },
                        {
                            id: 'exportButton',
                            xtype: 'button',
                            glyph: 0xf010,
                            text: '数据导出',
                            disabled: true,
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
            devTypes = Ext.getCmp('device-type-multicombo').getSelectedValues(),
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
        proxy.extraParams.devTypes = devTypes;
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

        Ext.Ajax.request({
            url: '/Report/GetFields400204',
            params: { types: devTypes },
            mask: new Ext.LoadMask({ target: gridPanel.getView(), msg: '获取列名...' }),
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    currentStore.model.prototype.fields.clear();
                    currentStore.removeAll();
                    var columns = [];
                    if (data.data && Ext.isArray(data.data)) {
                        Ext.Array.each(data.data, function (item, index) {
                            currentStore.model.prototype.fields.replace({ name: item.name, type: item.type });
                            if (!Ext.isEmpty(item.column)) {
                                columns.push(
                                    {
                                        text: item.column,
                                        dataIndex: item.name,
                                        width: item.width,
                                        align: item.align,
                                        renderer: function (value, p, record) {
                                            if (item.detail === true) {
                                                return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', item.name, value);
                                            }

                                            return value;
                                        }
                                    }
                                );
                            }
                        });
                    }

                    gridPanel.reconfigure(currentStore, columns);
                    currentStore.loadPage(1, {
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

    var print = function (store) {
        $$iPems.download({
            url: store.DownloadURL,
            params: store.getProxy().extraParams
        });
    };

    var chart = function (data) {
        if (!Ext.isEmpty(data) && !Ext.isEmpty(data.chart)) {
            barOption.xAxis[0].data = [];
            barOption.series = [];
            Ext.Array.each(data.chart, function (item, index) {
                barOption.xAxis[0].data.push(item.name);

                var added = barOption.series.length > 0;
                Ext.Array.each(item.models, function (_item, _index) {
                    if (added === false) {
                        barOption.series.push({
                            name: _item.name,
                            type: 'bar',
                            data: []
                        })
                    }

                    barOption.series[_index].data.push(parseInt(_item.value));
                });
            });
        } else {
            barOption.xAxis[0].data = ["无数据"];
            barOption.series = [{
                name: "无数据",
                type: 'bar',
                data: [0]
            }];
        }

        barChart.setOption(barOption, true);
    };

    var detail = function (station, type) {
        if (Ext.isEmpty(station)) return false;
        if (Ext.isEmpty(type)) return false;

        var proxy = detailStore.getProxy();
        proxy.extraParams.station = station;
        proxy.extraParams.type = type;

        detailStore.removeAll();
        detailStore.loadPage(1);
        detailWnd.show();
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

        //init charts
        barChart.setOption(barOption);
    });
})();