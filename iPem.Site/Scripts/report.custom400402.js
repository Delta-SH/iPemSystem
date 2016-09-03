(function () {
    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
			{ name: 'id', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'devType', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'logic', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'levelValue', type: 'int' },
            { name: 'levelDisplay', type: 'string' },
            { name: 'startTime', type: 'string' },
            { name: 'endTime', type: 'string' },
            { name: 'interval', type: 'float' },
            { name: 'startValue', type: 'string' },
            { name: 'endValue', type: 'string' },
            { name: 'almComment', type: 'string' },
            { name: 'normalComment', type: 'string' },
            { name: 'frequency', type: 'int' },
            { name: 'endType', type: 'string' },
            { name: 'project', type: 'string' },
            { name: 'confirmedStatus', type: 'string' },
            { name: 'confirmedTime', type: 'string' },
            { name: 'confirmer', type: 'string' }
        ],
        idProperty: 'id'
    });

    var query = function (pagingtoolbar) {
        var me = pagingtoolbar.store;

        me.proxy.extraParams.parent = Ext.getCmp('rangePicker').getValue();
        me.proxy.extraParams.starttime = Ext.getCmp('startField').getRawValue();
        me.proxy.extraParams.endtime = Ext.getCmp('endField').getRawValue();
        me.proxy.extraParams.statypes = Ext.getCmp('station-type-multicombo').getSelectedValues();
        me.proxy.extraParams.roomtypes = Ext.getCmp('room-type-multicombo').getSelectedValues();
        me.proxy.extraParams.devtypes = Ext.getCmp('device-type-multicombo').getSelectedValues();
        me.proxy.extraParams.almlevels = Ext.getCmp('alarm-level-multicombo').getSelectedValues();
        me.proxy.extraParams.logictypes = Ext.getCmp('logic-type-multicombo').getSelectedValues();
        me.proxy.extraParams.point = Ext.getCmp('point-name-textfield').getRawValue();

        me.proxy.extraParams.confirm = 'all';
        if (Ext.getCmp('show-confirm-menu').checked)
            me.proxy.extraParams.confirm = 'confirm';
        if (Ext.getCmp('show-unconfirm-menu').checked)
            me.proxy.extraParams.confirm = 'unconfirm';

        me.proxy.extraParams.project = 'all';
        if (Ext.getCmp('show-project-menu').checked)
            me.proxy.extraParams.project = 'project';
        if (Ext.getCmp('show-unproject-menu').checked)
            me.proxy.extraParams.project = 'unproject';

        me.loadPage(1);
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadCustom400402',
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
            fieldLabel: '备注信息'
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

    var chartPie1 = Ext.create('Ext.chart.Chart', {
        id: 'chartPie1',
        xtype: 'chart',
        animate: true,
        shadow: false,
        flex: 1,
        insetPadding: 5,
        theme: 'Base:gradients',
        legend: {
            position: 'right',
            itemSpacing: 3,
            boxStrokeWidth: 1,
            boxStroke: '#c0c0c0'
        },
        series: [{
            type: 'pie',
            field: 'value',
            showInLegend: true,
            donut: false,
            highlight: true,
            highlightCfg: {
                segment: { margin: 5 }
            },
            label: {
                display: 'rotate',
                field: 'name',
                contrast: true
            },
            tips: {
                trackMouse: true,
                minWidth: 120,
                minHeight: 60,
                renderer: function (storeItem, item) {
                    var total = 0;
                    chartPie1.store.each(function (rec) {
                        total += rec.get('value');
                    });

                    //this.setTitle('');
                    this.update(
                        Ext.String.format('{0}: {1}<br/>{2}: {3}<br/>{4}: {5}%',
                        '告警总量',
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        '告警占比',
                        (storeItem.get('value') / total * 100).toFixed(2))
                    );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment']
        })
    });

    var chartPie2 = Ext.create('Ext.chart.Chart', {
        id: 'chartPie2',
        xtype: 'chart',
        animate: true,
        shadow: false,
        flex: 1,
        insetPadding: 5,
        theme: 'Base:gradients',
        legend: {
            position: 'right',
            itemSpacing: 3,
            boxStrokeWidth: 1,
            boxStroke: '#c0c0c0'
        },
        series: [{
            type: 'pie',
            field: 'value',
            showInLegend: true,
            donut: false,
            highlight: true,
            highlightCfg: {
                segment: { margin: 5 }
            },
            label: {
                display: 'rotate',
                field: 'name',
                contrast: true
            },
            tips: {
                trackMouse: true,
                minWidth: 120,
                minHeight: 60,
                renderer: function (storeItem, item) {
                    var total = 0;
                    chartPie2.store.each(function (rec) {
                        total += rec.get('value');
                    });

                    //this.setTitle('');
                    this.update(
                        Ext.String.format('{0}: {1}<br/>{2}: {3}<br/>{4}: {5}%',
                        '告警总量',
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        '告警占比',
                        (storeItem.get('value') / total * 100).toFixed(2))
                    );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment']
        })
    });

    var chartPie3 = Ext.create('Ext.chart.Chart', {
        id: 'chartPie3',
        xtype: 'chart',
        animate: true,
        shadow: false,
        flex: 1,
        insetPadding: 5,
        theme: 'Base:gradients',
        legend: {
            position: 'right',
            itemSpacing: 3,
            boxStrokeWidth: 1,
            boxStroke: '#c0c0c0'
        },
        series: [{
            type: 'pie',
            field: 'value',
            showInLegend: true,
            donut: false,
            highlight: true,
            highlightCfg: {
                segment: { margin: 5 }
            },
            label: {
                display: 'rotate',
                field: 'name',
                contrast: true
            },
            tips: {
                trackMouse: true,
                minWidth: 120,
                minHeight: 60,
                renderer: function (storeItem, item) {
                    var total = 0;
                    chartPie3.store.each(function (rec) {
                        total += rec.get('value');
                    });

                    //this.setTitle('');
                    this.update(
                        Ext.String.format('{0}: {1}<br/>{2}: {3}<br/>{4}: {5}%',
                        '告警总量',
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        '告警占比',
                        (storeItem.get('value') / total * 100).toFixed(2))
                    );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment']
        })
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Report/RequestCustom400402',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    var data = me.proxy.reader.jsonData,
                        chartData1 = $$iPems.ChartEmptyDataPie,
                        chartData2 = $$iPems.ChartEmptyDataPie,
                        chartData3 = $$iPems.ChartEmptyDataPie;

                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length == 3) {
                        if (!Ext.isEmpty(data.chart[0]))
                            chartData1 = data.chart[0];

                        if (!Ext.isEmpty(data.chart[1]))
                            chartData2 = data.chart[1];

                        if (!Ext.isEmpty(data.chart[2]))
                            chartData3 = data.chart[2];
                    }

                    chartPie1.getStore().loadData(chartData1, false);
                    chartPie2.getStore().loadData(chartData2, false);
                    chartPie3.getStore().loadData(chartData3, false);
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
                flex: 1,
                tools: [
                    //{
                    //    type: 'print',
                    //    tooltip: '数据导出',
                    //    handler: function (event, toolEl, panelHeader) {
                    //        Ext.ux.ImageExporter.save([chartPie1, chartPie2, chartPie3]);
                    //    }
                    //}
                ],
                layout: {
                    type: 'hbox',
                    align: 'stretch',
                    pack: 'start'
                },
                items: [chartPie1, chartPie2, chartPie3]
            }, {
                id: 'history-alarm-grid',
                xtype: 'grid',
                glyph: 0xf029,
                title: '告警详细信息',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 2,
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
                        text: '设备类型',
                        dataIndex: 'devType'
                    },
                    {
                        text: '设备名称',
                        dataIndex: 'device'
                    },
                    {
                        text: '逻辑分类',
                        dataIndex: 'logic'
                    },
                    {
                        text: '信号名称',
                        dataIndex: 'point'
                    },
                    {
                        text: '告警级别',
                        dataIndex: 'levelDisplay'
                    },
                    {
                        text: '开始时间',
                        dataIndex: 'startTime'
                    },
                    {
                        text: '结束时间',
                        dataIndex: 'endTime'
                    },
                    {
                        text: '告警历时(分钟)',
                        dataIndex: 'interval'
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
                        dataIndex: 'almComment'
                    },
                    {
                        text: '正常描述',
                        dataIndex: 'normalComment'
                    },
                    {
                        text: '触发频次',
                        dataIndex: 'frequency'
                    },
                    {
                        text: '结束方式',
                        dataIndex: 'endType'
                    },
                    {
                        text: '工程预约',
                        dataIndex: 'project',
                        renderer: function (value, p, record) {
                            return Ext.String.format('<a href="javascript:void(0)" style="color:#157fcc;">{0}</a>', value);
                        }
                    },
                    {
                        text: '确认状态',
                        dataIndex: 'confirmedStatus'
                    },
                    {
                        text: '确认时间',
                        dataIndex: 'confirmedTime'
                    },
                    {
                        text: '确认人员',
                        dataIndex: 'confirmer'
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
                                    query(currentPagingToolbar);
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
                                id: 'alarm-level-multicombo',
                                xtype: 'AlarmLevelMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'logic-type-multicombo',
                                xtype: 'LogicTypeMultiPicker',
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
            query(currentPagingToolbar);
        }
    });
})();