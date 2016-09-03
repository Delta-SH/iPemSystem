(function () {
    var pieChart = null,
        lineChart = null,
        pieOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                y: 'center',
                data: ['一级告警', '二级告警', '三级告警', '四级告警']
            },
            series: [
                {
                    name: '实时告警',
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
        lineOption = {
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
            { name: 'start', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'value', type: 'string' },
            { name: 'frequency', type: 'int' },
            { name: 'interval', type: 'string' },
            { name: 'confirmed', type: 'string' },
            { name: 'confirmer', type: 'string' },
            { name: 'confirmedtime', type: 'string' },
            { name: 'project', type: 'string' }
        ],
        idProperty: 'index'
    });

    var change = function (node, pagingtoolbar) {
        var me = pagingtoolbar.store;
        me.proxy.extraParams.node = node.getId();
        me.loadPage(1);
    };

    var filter = function (pagingtoolbar) {
        var me = pagingtoolbar.store;

        me.proxy.extraParams.statype = Ext.getCmp('station-type-multicombo').getSelectedValues();
        me.proxy.extraParams.roomtype = Ext.getCmp('room-type-multicombo').getSelectedValues();
        me.proxy.extraParams.devtype = Ext.getCmp('device-type-multicombo').getSelectedValues();
        me.proxy.extraParams.almlevel = Ext.getCmp('alarm-level-multicombo').getSelectedValues();
        me.proxy.extraParams.logictype = Ext.getCmp('logic-type-multipicker').getValue();
        me.proxy.extraParams.pointname = Ext.getCmp('point-name-textfield').getRawValue();

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
            url: '/Home/DownloadActAlms',
            params: store.proxy.extraParams
        });
    };

    var confirm = function (pagingtoolbar) {
        var grid = Ext.getCmp('alarms-grid'),
            selection = grid.getSelectionModel();

        if (!selection.hasSelection()) {
            Ext.Msg.show({ title: '系统警告', msg: '请勾选需要确认的告警', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        var models = selection.getSelection();
        Ext.Msg.confirm('确认对话框', Ext.String.format('共{0}条告警将被确认，您确定吗？', models.length), function (buttonId, text) {
            if (buttonId === 'yes') {
                var keys = [];
                Ext.Array.each(models, function (item, index, allItems) {
                    keys.push(Ext.String.format('{0}{1}{2}', item.get('fsuid'), $$iPems.Separator, item.get('id')));
                });

                Ext.Ajax.request({
                    url: '/Home/ConfirmAlarms',
                    params: { keys: keys },
                    mask: new Ext.LoadMask(grid, { msg: '正在处理，请稍后...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success)
                            pagingtoolbar.doRefresh();
                        else
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                });
            }
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
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Home/RequestActAlarms',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                node: 'root',
                statype: [],
                roomtype: [],
                devtype: [],
                almlevel: [],
                logictype: [],
                pointname: '',
                confirm: 'all',
                project: 'all'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful && pieChart && lineChart) {
                    var data = me.proxy.reader.jsonData;
                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length == 2) {
                        if (!Ext.isEmpty(data.chart[0])) {
                            pieOption.series[0].data[0].value = 0;
                            pieOption.series[0].data[1].value = 0;
                            pieOption.series[0].data[2].value = 0;
                            pieOption.series[0].data[3].value = 0;
                            Ext.Array.each(data.chart[0], function (item, index) {
                                if (item.index == $$iPems.AlmLevel.Level1)
                                    pieOption.series[0].data[0].value = item.value;
                                else if(item.index == $$iPems.AlmLevel.Level2)
                                    pieOption.series[0].data[1].value = item.value;
                                else if (item.index == $$iPems.AlmLevel.Level3)
                                    pieOption.series[0].data[2].value = item.value;
                                else if (item.index == $$iPems.AlmLevel.Level4)
                                    pieOption.series[0].data[3].value = item.value;
                            });
                            pieChart.setOption(pieOption);
                        }
                            
                        if (!Ext.isEmpty(data.chart[1])) {
                            var xaxis = [],
                                series1 = [],
                                series2 = [],
                                series3 = [],
                                series4 = [],
                                groups = {};

                            
                            Ext.Array.each(data.chart[1], function (item, index) {
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
                            
                            lineOption.xAxis[0].data = xaxis;
                            lineOption.series[0].data = series1;
                            lineOption.series[1].data = series2;
                            lineOption.series[2].data = series3;
                            lineOption.series[3].data = series4;
                            lineChart.setOption(lineOption);
                        }
                    }

                    $$iPems.Tasks.actAlmTask.fireOnStart = false;
                    $$iPems.Tasks.actAlmTask.restart();
                }
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    Ext.onReady(function () {
        var currentLayout = Ext.create('Ext.panel.Panel', {
            id: 'currentLayout',
            region: 'center',
            layout: 'border',
            border: false,
            items: [{
                id: 'organization',
                region: 'west',
                xtype: 'treepanel',
                title: '系统层级列表',
                glyph: 0xf011,
                width: 220,
                split: true,
                collapsible: true,
                collapsed: false,
                autoScroll: true,
                useArrows: false,
                rootVisible: true,
                root: {
                    id: 'root',
                    text: '全部',
                    expanded: true,
                    icon: $$iPems.icons.Home
                },
                viewConfig: {
                    loadMask: true
                },
                store: Ext.create('Ext.data.TreeStore', {
                    autoLoad: false,
                    nodeParam: 'node',
                    proxy: {
                        type: 'ajax',
                        url: '/Component/GetDevices',
                        reader: {
                            type: 'json',
                            successProperty: 'success',
                            messageProperty: 'message',
                            totalProperty: 'total',
                            root: 'data'
                        }
                    }
                }),
                listeners:{
                    select: function (me, record, item, index) {
                        change(record, currentPagingToolbar);
                    }
                },
                tbar: [
                    {
                        id: 'organization-search-field',
                        xtype: 'textfield',
                        emptyText: '请输入筛选条件...',
                        flex: 1,
                        listeners: {
                            change: function (me, newValue, oldValue, eOpts) {
                                delete me._filterData;
                                delete me._filterIndex;
                            }
                        }
                    },
                    {
                        id: 'alarm-search-button',
                        xtype: 'button',
                        glyph: 0xf005,
                        handler: function () {
                            var tree = Ext.getCmp('organization'),
                                search = Ext.getCmp('organization-search-field'),
                                text = search.getRawValue();

                            if (Ext.isEmpty(text, false)) {
                                return;
                            }

                            if (text.length < 2) {
                                return;
                            }

                            if (search._filterData != null
                                && search._filterIndex != null) {
                                var index = search._filterIndex + 1;
                                var paths = search._filterData;
                                if (index >= paths.length) {
                                    index = 0;
                                    Ext.Msg.show({ title: '系统提示', msg: '搜索完毕', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                }

                                $$iPems.selectNodePath(tree, paths[index]);
                                search._filterIndex = index;
                            } else {
                                Ext.Ajax.request({
                                    url: '/Component/FilterRoomPath',
                                    params: { text: text },
                                    mask: new Ext.LoadMask({ target: tree, msg: '正在处理，请稍后...' }),
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data.success) {
                                            var len = data.data.length;
                                            if (len > 0) {
                                                $$iPems.selectNodePath(tree, data.data[0]);
                                                search._filterData = data.data;
                                                search._filterIndex = 0;
                                            } else {
                                                Ext.Msg.show({ title: '系统提示', msg: Ext.String.format('未找到指定内容:<br/>{0}', text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                            }
                                        } else {
                                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                        }
                                    }
                                });
                            }
                        }
                    }
                ]
            }, {
                region: 'center',
                xtype: 'panel',
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
                            contentEl: 'line-chart'
                        }
                    ]
                }, {
                    xtype: 'panel',
                    glyph: 0xf029,
                    title: '告警详细信息',
                    collapsible: true,
                    collapseFirst: false,
                    layout: 'fit',
                    margin: '5 0 0 0',
                    flex: 2,
                    tools: [
                        {
                            type: 'refresh',
                            tooltip: '刷新',
                            handler: function (event, toolEl, panelHeader) {
                                currentPagingToolbar.doRefresh();
                            }
                        },
                        {
                            type: 'print',
                            tooltip: '数据导出',
                            handler: function (event, toolEl, panelHeader) {
                                print(currentStore);
                            }
                        }
                    ],
                    items: [{
                        id: 'alarms-grid',
                        xtype: 'grid',
                        selType: 'checkboxmodel',
                        border: false,
                        store: currentStore,
                        viewConfig: {
                            loadMask: false,
                            stripeRows: true,
                            trackOver: true,
                            preserveScrollOnRefresh: true,
                            emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                            getRowClass: function (record, rowIndex, rowParams, store) {
                                return $$iPems.GetAlmLevelCls(record.get("levelid"));
                            }
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
                                text: '告警级别',
                                dataIndex: 'level',
                                align: 'center',
                                tdCls: 'x-level-cell'
                            },
                            {
                                text: '告警时间',
                                dataIndex: 'start',
                                width: 150
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
                                text: '信号名称',
                                dataIndex: 'point'
                            },
                            {
                                text: '告警描述',
                                dataIndex: 'comment'
                            },
                            {
                                text: '触发值',
                                dataIndex: 'value'
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
                    }]
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
                                    xtype: 'splitbutton',
                                    glyph: 0xf005,
                                    text: '确定',
                                    handler: function (me, event) {
                                        filter(currentPagingToolbar);
                                    },
                                    menu: [
                                        {
                                            text: '告警确认',
                                            glyph: 0xf035,
                                            hidden: !$$iPems.ConfirmOperation,
                                            handler: function (me, event) {
                                                confirm(currentPagingToolbar);
                                            }
                                        },
                                        {
                                            xtype: 'menuseparator',
                                            hidden: !$$iPems.ConfirmOperation
                                        },
                                        {
                                            text: '数据导出',
                                            glyph: 0xf010,
                                            handler: function (me, event) {
                                                print(currentStore);
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
                                    id: 'logic-type-multipicker',
                                    xtype: 'LogicTypeMultiPicker',
                                    emptyText: '默认全部',
                                    width: 220
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
                                        },
                                    ]
                                }
                            ]
                        }
                    ]
                }]
            }]
        });

        $$iPems.Tasks.actAlmTask.run = function () {
            currentPagingToolbar.doRefresh();
        };
        $$iPems.Tasks.actAlmTask.start();

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        pieChart = echarts.init(document.getElementById("pie-chart"), 'shine');
        lineChart = echarts.init(document.getElementById("line-chart"), 'shine');

        //init charts
        pieChart.setOption(pieOption);
        lineChart.setOption(lineOption);
    });
})();