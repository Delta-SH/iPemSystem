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

    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'fsuid', type: 'string' },
			{ name: 'id', type: 'string' },
            { name: 'level', type: 'string' },
            { name: 'levelid', type: 'int' },
            { name: 'startDate', type: 'string' },
            { name: 'endDate', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'deviceType', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'startValue', type: 'string' },
            { name: 'endValue', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'frequency', type: 'int' },
            { name: 'interval', type: 'string' },
            { name: 'endType', type: 'string' },
            { name: 'confirmed', type: 'string' },
            { name: 'confirmer', type: 'string' },
            { name: 'confirmedtime', type: 'string' },
            { name: 'project', type: 'string' }
        ],
        idProperty: 'index'
    });

    var query = function (store) {
        store.proxy.extraParams.parent = Ext.getCmp('rangePicker').getValue();
        store.proxy.extraParams.startDate = Ext.getCmp('startField').getRawValue();
        store.proxy.extraParams.endDate = Ext.getCmp('endField').getRawValue();
        store.proxy.extraParams.staTypes = Ext.getCmp('station-type-multicombo').getSelectedValues();
        store.proxy.extraParams.roomTypes = Ext.getCmp('room-type-multicombo').getSelectedValues();
        store.proxy.extraParams.devTypes = Ext.getCmp('device-type-multicombo').getSelectedValues();
        store.proxy.extraParams.levels = Ext.getCmp('alarm-level-multicombo').getSelectedValues();
        store.proxy.extraParams.logicTypes = Ext.getCmp('logic-type-multicombo').getValue();
        store.proxy.extraParams.point = Ext.getCmp('point-name-textfield').getRawValue();

        store.proxy.extraParams.confirm = 'all';
        if (Ext.getCmp('show-confirm-menu').checked)
            store.proxy.extraParams.confirm = 'confirm';
        if (Ext.getCmp('show-unconfirm-menu').checked)
            store.proxy.extraParams.confirm = 'unconfirm';

        store.proxy.extraParams.project = 'all';
        if (Ext.getCmp('show-project-menu').checked)
            store.proxy.extraParams.project = 'project';
        if (Ext.getCmp('show-unproject-menu').checked)
            store.proxy.extraParams.project = 'unproject';

        store.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadHistory400204',
            params: store.proxy.extraParams
        });
    };

    var projectWnd = Ext.create('Ext.window.Window', {
        title: '工程预约',
        glyph: 0xf045,
        height: 300,
        width: 400,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        bodyPadding: 10,
        layout: 'form',
        defaultType: 'displayfield',
        items: [{
            itemId: 'id',
            labelWidth: 60,
            fieldLabel: '预约编号'
        }, {
            itemId: 'startTime',
            labelWidth: 60,
            fieldLabel: '开始时间'
        }, {
            itemId: 'endTime',
            labelWidth: 60,
            fieldLabel: '结束时间'
        }, {
            itemId: 'projectName',
            labelWidth: 60,
            fieldLabel: '工程名称'
        }, {
            itemId: 'creator',
            labelWidth: 60,
            fieldLabel: '创建人员'
        }, {
            itemId: 'createdTime',
            labelWidth: 60,
            fieldLabel: '创建时间'
        }, {
            itemId: 'comment',
            labelWidth: 60,
            fieldLabel: '预约备注'
        }],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                projectWnd.hide();
            }
        }]
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        groupField: 'deviceType',
        //禁用自动排序，使用后台的排序方式
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
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
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data) && !Ext.isEmpty(data.chart)) {
                        var xaxis = [],
                            series1 = [],
                            series2 = [],
                            series3 = [],
                            series4 = [],
                            groups = {};

                        Ext.Array.each(data.chart, function (item, index) {
                            if (!groups[item.name]) {
                                groups[item.name] = {
                                    Key: item.name,
                                    L1: 0,
                                    L2: 0,
                                    L3: 0,
                                    L4: 0
                                };
                            }

                            if (item.index == $$iPems.AlmLevel.Level1)
                                groups[item.name].L1 = item.value;
                            else if (item.index == $$iPems.AlmLevel.Level2)
                                groups[item.name].L2 = item.value;
                            else if (item.index == $$iPems.AlmLevel.Level3)
                                groups[item.name].L3 = item.value;
                            else if (item.index == $$iPems.AlmLevel.Level4)
                                groups[item.name].L4 = item.value;
                        });

                        for (var x in groups) {
                            if (Object.prototype.hasOwnProperty.call(groups, x)) {
                                xaxis.push(groups[x].Key);
                                series1.push(groups[x].L1);
                                series2.push(groups[x].L2);
                                series3.push(groups[x].L3);
                                series4.push(groups[x].L4);
                            }
                        }

                        barOption.xAxis[0].data = xaxis;
                        barOption.series[0].data = series1;
                        barOption.series[1].data = series2;
                        barOption.series[2].data = series3;
                        barOption.series[3].data = series4;
                        barChart.setOption(barOption);
                    }
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
                glyph: 0xf030,
                title: '告警分类占比',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
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
                ]
            }, {
                id: 'history-alarm-grid',
                xtype: 'grid',
                glyph: 0xf029,
                title: '告警分类信息',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                store: currentStore,
                loadMask: true,
                tools: [{
                    type: 'print',
                    tooltip: '数据导出',
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
                viewConfig: {
                    loadMask: false,
                    preserveScrollOnRefresh: true,
                    stripeRows: true,
                    trackOver: true,
                    emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
                },
                features: [{
                    ftype: 'grouping',
                    groupHeaderTpl: '{columnName}: {name} ({rows.length}条)',
                    hideGroupedHeader: false,
                    startCollapsed: true
                }],
                listeners: {
                    cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
                        var fieldName = view.ownerCt.columns[cellIndex].dataIndex;
                        if (fieldName !== 'project')
                            return;

                        var fieldValue = record.get(fieldName);
                        if (Ext.isEmpty(fieldValue))
                            return;

                        Ext.Ajax.request({
                            url: '/Home/GetAppointmentDetail',
                            Method: 'POST',
                            params: { id: fieldValue },
                            mask: new Ext.LoadMask(view.ownerCt, { msg: '正在处理，请稍后...' }),
                            success: function (response, options) {
                                var data = Ext.decode(response.responseText, true);
                                if (data.success) {
                                    var id = projectWnd.getComponent('id');
                                    var startTime = projectWnd.getComponent('startTime');
                                    var endTime = projectWnd.getComponent('endTime');
                                    var projectName = projectWnd.getComponent('projectName');
                                    var creator = projectWnd.getComponent('creator');
                                    var createdTime = projectWnd.getComponent('createdTime');
                                    var comment = projectWnd.getComponent('comment');

                                    id.setValue(data.data.id);
                                    startTime.setValue(data.data.startTime);
                                    endTime.setValue(data.data.endTime);
                                    projectName.setValue(data.data.projectName);
                                    creator.setValue(data.data.creator);
                                    createdTime.setValue(data.data.createdTime);
                                    comment.setValue(data.data.comment);
                                    projectWnd.show();
                                } else {
                                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                }
                            }
                        });
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
                        dataIndex: 'startDate'
                    },
                    {
                        text: '结束时间',
                        dataIndex: 'endDate'
                    },
                    {
                        text: '所属区域',
                        dataIndex: 'area'
                    },
                    {
                        text: '所属站点',
                        dataIndex: 'station'
                    },
                    {
                        text: '所属机房',
                        dataIndex: 'room'
                    },
                    {
                        text: '所属设备',
                        dataIndex: 'device'
                    },
                    {
                        text: '设备类型',
                        dataIndex: 'deviceType'
                    },
                    {
                        text: '信号名称',
                        dataIndex: 'point'
                    },
                    {
                        text: '触发值',
                        dataIndex: 'startValue'
                    },
                    {
                        text: '结束值',
                        dataIndex: 'endValue'
                    },
                    {
                        text: '告警描述',
                        dataIndex: 'comment'
                    },
                    {
                        text: '触发频次',
                        dataIndex: 'frequency'
                    },
                    {
                        text: '告警历时',
                        dataIndex: 'interval'
                    },
                    {
                        text: '结束方式',
                        dataIndex: 'endType'
                    },
                    {
                        text: '确认状态',
                        dataIndex: 'confirmed'
                    },
                    {
                        text: '确认人员',
                        dataIndex: 'confirmer'
                    },
                    {
                        text: '确认时间',
                        dataIndex: 'confirmedtime'
                    },
                    {
                        text: '工程预约',
                        dataIndex: 'project',
                        renderer: function (value, p, record) {
                            return Ext.String.format('<a href="javascript:void(0)" style="color:#157fcc;">{0}</a>', value);
                        }
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '告警筛选条件',
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
                                    query(currentStore);
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
                                id: 'logic-type-multicombo',
                                xtype: 'LogicTypeMultiPicker',
                                width: 220,
                                emptyText: '默认全部'
                            },
                            {
                                id: 'point-name-textfield',
                                xtype: 'textfield',
                                fieldLabel: '信号名称',
                                emptyText: '多条件请以;分隔，例: A;B;C',
                                labelWidth: 60,
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
                                        id: 'show-confirm-menu',
                                        xtype: 'menucheckitem',
                                        text: '已确认告警',
                                        checked: false,
                                        checkHandler: function (me, checked) {
                                            if (checked) {
                                                Ext.getCmp('show-unconfirm-menu').setChecked(false);
                                            }
                                        }
                                    },
                                    {
                                        id: 'show-unconfirm-menu',
                                        xtype: 'menucheckitem',
                                        text: '未确认告警',
                                        checked: false,
                                        checkHandler: function (me, checked) {
                                            if (checked) {
                                                Ext.getCmp('show-confirm-menu').setChecked(false);
                                            }
                                        }
                                    },
                                    '-',
                                    {
                                        id: 'show-project-menu',
                                        xtype: 'menucheckitem',
                                        text: '工程告警',
                                        checked: false,
                                        checkHandler: function (me, checked) {
                                            if (checked) {
                                                Ext.getCmp('show-unproject-menu').setChecked(false);
                                            }
                                        }
                                    },
                                    {
                                        id: 'show-unproject-menu',
                                        xtype: 'menucheckitem',
                                        text: '非工程告警',
                                        checked: false,
                                        checkHandler: function (me, checked) {
                                            if (checked) {
                                                Ext.getCmp('show-project-menu').setChecked(false);
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

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
            
            //load data
            query(currentStore);
        }
    });

    Ext.onReady(function () {
        barChart = echarts.init(document.getElementById("bar-chart"), 'shine');

        //init charts
        barChart.setOption(barOption);
    });
})();