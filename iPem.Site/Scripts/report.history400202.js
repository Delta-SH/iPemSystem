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
                data: ['一级告警', '二级告警', '三级告警', '四级告警']
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
                            name: '一级告警',
                            itemStyle: {
                                normal: {
                                    color: '#f04b51'
                                }
                            }
                        },
                        {
                            value: 0,
                            name: '二级告警',
                            itemStyle: {
                                normal: {
                                    color: '#efa91f'
                                }
                            }
                        },
                        {
                            value: 0,
                            name: '三级告警',
                            itemStyle: {
                                normal: {
                                    color: '#f5d313'
                                }
                            }
                        },
                        {
                            value: 0,
                            name: '四级告警',
                            itemStyle: {
                                normal: {
                                    color: '#0892cd'
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

    var reservationWnd = Ext.create('Ext.window.Window', {
        title: '工程状态详情',
        glyph: 0xf045,
        height: 320,
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
            itemId: 'name',
            labelWidth: 60,
            fieldLabel: '预约名称'
        }, {
            itemId: 'start',
            labelWidth: 60,
            fieldLabel: '开始时间'
        }, {
            itemId: 'end',
            labelWidth: 60,
            fieldLabel: '结束时间'
        }, {
            itemId: 'project',
            labelWidth: 60,
            fieldLabel: '关联工程'
        }, {
            itemId: 'creator',
            labelWidth: 60,
            fieldLabel: '创建人员'
        }, {
            itemId: 'time',
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
                reservationWnd.hide();
            }
        }]
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        DownloadURL: '/Report/DownloadHistory400202',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistory400202',
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

    var detailStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        DownloadURL: '/Report/DownloadDetail400202',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Report/RequestDetail400202',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                id: '',
                title: '',
                date: '2017-01-01',
                primary: false,
                related: false,
                filter: false,
                reversal: false
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
            //tools: [
            //    {
            //        type: 'print',
            //        tooltip: '数据导出',
            //        handler: function (event, toolEl, panelHeader) {
            //            Ext.ux.ImageExporter.save([chartPie1, chartPie2, chartPie3]);
            //        }
            //    }
            //],
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
            //tools: [{
            //    type: 'print',
            //    tooltip: '数据导出',
            //    handler: function (event, toolEl, panelHeader) {
            //        print(currentStore);
            //    }
            //}],
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
                        return '<a class="grid-link" href="javascript:void(0);">查看</a>';
                    }
                },
                {
                    text: '告警翻转',
                    dataIndex: 'reversalcount',
                    align: 'center'
                }
            ],
            listeners: {
                cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
                    var columns = view.getGridColumns(),
                        fieldName = columns[cellIndex].dataIndex;

                    if (fieldName !== 'reservation')
                        return;

                    var fieldValue = record.get(fieldName);
                    if (Ext.isEmpty(fieldValue))
                        return;

                    showResDetail(fieldValue, view);
                },
                itemcontextmenu: function (me, record, item, index, e) {
                    showContextMenu(me, record, item, index, e);
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
                },
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'alarm-type-multicombo',
                            xtype: 'AlarmTypeMultiCombo',
                            emptyText: '默认不包含以下告警'
                        },
                        {
                            id: 'confirmer-keywords',
                            xtype: 'textfield',
                            fieldLabel: '确认人员',
                            emptyText: '多条件请以;分隔，例: A;B;C',
                            labelWidth: 60,
                            width: 220
                        },
                        {
                            id: 'point-keywords',
                            xtype: 'textfield',
                            fieldLabel: '关键字',
                            emptyText: '多关键字请以;分隔，例: A;B;C',
                            labelWidth: 60,
                            width: 220
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

    var almContextMenu = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        record: null,
        items: [{
            itemId: 'subalarms',
            glyph: 0xf029,
            text: '查看告警',
            hideOnClick: false,
            menu: [
                {
                    itemId: 'primary',
                    glyph: 0xf029,
                    text: '主次告警',
                    handler: function () {
                        var me = almContextMenu;
                        if (me.record == null) return false;
                        var id = me.record.get('id');
                        if (Ext.isEmpty(id)) return false;
                        var date = me.record.get('starttime');
                        if (Ext.isEmpty(date)) return false;
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showDetail(id, Ext.String.format('主次告警详单({0})', name), date, true, false, false, false);
                    }
                }, {
                    itemId: 'related',
                    glyph: 0xf029,
                    text: '关联告警',
                    handler: function () {
                        var me = almContextMenu;
                        if (me.record == null) return false;
                        var id = me.record.get('id');
                        if (Ext.isEmpty(id)) return false;
                        var date = me.record.get('starttime');
                        if (Ext.isEmpty(date)) return false;
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showDetail(id, Ext.String.format('关联告警详单({0})', name), date, false, true, false, false);
                    }
                }, {
                    itemId: 'filter',
                    glyph: 0xf029,
                    text: '过滤告警',
                    handler: function () {
                        var me = almContextMenu;
                        if (me.record == null) return false;
                        var id = me.record.get('id');
                        if (Ext.isEmpty(id)) return false;
                        var date = me.record.get('starttime');
                        if (Ext.isEmpty(date)) return false;
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showDetail(id, Ext.String.format('过滤告警详单({0})', name), date, false, false, true, false);
                    }
                }, {
                    itemId: 'reversal',
                    glyph: 0xf029,
                    text: '翻转告警',
                    handler: function () {
                        var me = almContextMenu;
                        if (me.record == null) return false;
                        var id = me.record.get('reversalid');
                        if (Ext.isEmpty(id)) return false;
                        var date = me.record.get('starttime');
                        if (Ext.isEmpty(date)) return false;
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showDetail(id, Ext.String.format('翻转告警详单({0})', name), date, false, false, false, true);
                    }
                }
            ]
        }, '-', {
            itemId: 'export',
            glyph: 0xf010,
            text: '数据导出',
            handler: function () {
                print(currentStore);
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
            types = Ext.getCmp('alarm-type-multicombo').getSelectedValues(),
            confirmers = Ext.getCmp('confirmer-keywords').getRawValue(),
            keywords = Ext.getCmp('point-keywords').getRawValue(),
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
        proxy.extraParams.types = types;
        proxy.extraParams.confirmers = confirmers;
        proxy.extraParams.keywords = keywords;
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

    var print = function (store) {
        $$iPems.download({
            url: store.DownloadURL,
            params: store.getProxy().extraParams
        });
    };

    var chart = function (data) {
        if (Ext.isEmpty(data)) return false;
        if (Ext.isEmpty(data.chart)) return false;
        if (!Ext.isArray(data.chart)) return false;
        if (data.chart.length != 2) return false;

        var chart0 = data.chart[0];
        var chart1 = data.chart[1];
        if (!Ext.isEmpty(chart0)) {
            pieOption.series[0].data[0].value = 0;
            pieOption.series[0].data[1].value = 0;
            pieOption.series[0].data[2].value = 0;
            pieOption.series[0].data[3].value = 0;

            Ext.Array.each(chart0, function (item, index) {
                if (item.index == $$iPems.Level.Level1)
                    pieOption.series[0].data[0].value = item.value;
                else if (item.index == $$iPems.Level.Level2)
                    pieOption.series[0].data[1].value = item.value;
                else if (item.index == $$iPems.Level.Level3)
                    pieOption.series[0].data[2].value = item.value;
                else if (item.index == $$iPems.Level.Level4)
                    pieOption.series[0].data[3].value = item.value;
            });
            pieChart.setOption(pieOption);
        }

        if (!Ext.isEmpty(chart1)) {
            var xaxis = [], series1 = [], series2 = [], series3 = [], series4 = [], groups = {};
            Ext.Array.each(chart1, function (item, index) {
                if (!groups[item.name])
                    groups[item.name] = { Key: item.name, L1: 0, L2: 0, L3: 0, L4: 0 };

                if (item.index == $$iPems.Level.Level1)
                    groups[item.name].L1 = item.value;
                else if (item.index == $$iPems.Level.Level2)
                    groups[item.name].L2 = item.value;
                else if (item.index == $$iPems.Level.Level3)
                    groups[item.name].L3 = item.value;
                else if (item.index == $$iPems.Level.Level4)
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
    };

    var showContextMenu = function (me, record, item, index, e) {
        e.stopEvent();
        almContextMenu.record = record;
        almContextMenu.showAt(e.getXY());
    }

    var showDetail = function (id, title, date, primary, related, filter, reversal) {
        var store = detailGrid.getStore(),
            proxy = store.getProxy();

        proxy.extraParams.id = id;
        proxy.extraParams.title = title;
        proxy.extraParams.date = date;
        proxy.extraParams.primary = primary;
        proxy.extraParams.related = related;
        proxy.extraParams.filter = filter;
        proxy.extraParams.reversal = reversal;
        store.loadPage(1);

        detailWnd.setTitle(title);
        detailWnd.show();
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
})();