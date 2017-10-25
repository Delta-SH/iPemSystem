(function () {

    //#region Model
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
			{ name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'value', type: 'string' },
            { name: 'unit', type: 'string' },
            { name: 'status', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'deviceid', type: 'string' },
            { name: 'pointid', type: 'string' },
            { name: 'typeid', type: 'int' },
            { name: 'statusid', type: 'int' },
            { name: 'followed', type: 'boolean' },
            { name: 'followedOnly', type: 'boolean' },
            { name: 'timestamp', type: 'string' }
        ],
        idProperty: 'index'
    });

    Ext.define('ActAlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'nmalarmid', type: 'string' },
			{ name: 'level', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'interval', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'value', type: 'string' },
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

    Ext.define('TemplateModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'text', type: 'string' },
            { name: 'custom', type: 'auto' }
        ]
    });

    Ext.define("CardRecordModel", {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'recType', type: 'string' },
            { name: 'cardId', type: 'string' },
            { name: 'decimalCard', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'remark', type: 'string' },
            { name: 'employeeCode', type: 'string' },
            { name: 'employeeName', type: 'string' },
            { name: 'employeeType', type: 'string' },
            { name: 'department', type: 'string' }
        ],
        idProperty: 'index'
    });
    //#endregion

    //#region Store
    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        groupField: 'type',
        groupDir: 'undefined',
        sortOnLoad: false,
        downloadURL: '',
        selectPoint: null,
        proxy: {
            type: 'ajax',
            url: '/Home/RequestActPoints',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                node: 'root',
                normal: false,
                types: [$$iPems.Point.DI, $$iPems.Point.AI, $$iPems.Point.AO, $$iPems.Point.DO, $$iPems.Point.AL]
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    if (records.length > 0) {
                        var grid = pointGrid,
                            selection = grid.getSelectionModel();

                        if (!Ext.isEmpty(me.selectPoint)) {
                            var record = me.findRecord('pointid', me.selectPoint);
                            if (!Ext.isEmpty(record)) selection.select(record);
                            me.selectPoint = null;
                        }

                        if (!Ext.isEmpty(gaugeChart) && !Ext.isEmpty(lineChart)) {
                            if (selection.hasSelection()) {
                                var index = selection.getSelection()[0].get('index');
                                var record = me.findRecord('index', index);
                                if (!Ext.isEmpty(record)) loadChart(record);
                            }
                        }
                    }

                    $$iPems.Tasks.actPointTask.fireOnStart = false;
                    $$iPems.Tasks.actPointTask.restart();
                }
            }
        }
    });

    var matrixStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        fields: [],
        proxy: {
            type: 'ajax',
            url: '/Home/RequestMatrixValues',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                node: 'root'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    $$iPems.Tasks.actPointTask.fireOnStart = false;
                    $$iPems.Tasks.actPointTask.restart();
                }
            }
        }
    });

    var alarmStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ActAlarmModel',
        downloadURL: '/Home/DownloadActAlms',
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
                baseNode: 'root',
                seniorNode: 'root',
                stationTypes: [],
                roomTypes: [],
                subDeviceTypes: [],
                subLogicTypes: [],
                points: [],
                levels: [],
                confirms: [],
                reservations: [],
                keywords: Ext.emptyString,
                onlyConfirms: false,
                onlyReservations: false,
                onlySystem: false
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    $$iPems.Tasks.actPointTask.fireOnStart = false;
                    $$iPems.Tasks.actPointTask.restart();
                }
            }
        }
    });

    var columnStore = Ext.create('Ext.data.Store',{
        autoLoad: false,
        fields: ['id','text'],
        proxy: {
            type: 'ajax',
            url: '/Component/GetPointInDevType',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                node: ''
            },
            simpleSortMode: true
        }
    });

    var templateStore = Ext.create('Ext.data.Store', {
        autoLoad: true,
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'auto' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Component/GetMatrixTemplates',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            }
        },
        listeners: {
            load: function (me, records, success) {
                if (success && records.length > 0) {
                    var templates = Ext.getCmp('matrixTemplates'),
                        tempcookie = Ext.util.Cookies.get('ipems.matrix.template');
                    
                    var template = null;
                    if (Ext.isEmpty(tempcookie)) {
                        templates.select(records[0]);
                        template = records[0].getId();
                    } else {
                        templates.select(tempcookie);
                        template = tempcookie;
                    }

                    if (!Ext.isEmpty(template)) {
                        loadMatrixColumn(template);
                    }
                }
            }
        }
    });

    var recordStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'CardRecordModel',
        downloadURL: '/Home/DownloadCardRecords',
        proxy: {
            type: 'ajax',
            url: '/Home/RequestCardRecords',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: { node: 'root' },
            simpleSortMode: true
        },
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var matrixPagingToolbar = $$iPems.clonePagingToolbar(matrixStore);

    var alarmPagingToolbar = $$iPems.clonePagingToolbar(alarmStore);

    var recordPagingToolbar = $$iPems.clonePagingToolbar(recordStore);
    //#endregion

    //#region Chart
    var gaugeChart = null,
        lineChart = null,
        gaugeOption = {
            tooltip: {
                formatter: '{b}: {c} {a}'
            },
            series: [
                {
                    name: '',
                    type: 'gauge',
                    center: ['50%', 100],
                    radius: '100%',
                    title: {
                        offsetCenter: [0, 60],
                        textStyle: {
                            color: '#005eaa',
                            fontWeight: 'bolder',
                            fontSize: 12
                        }
                    },
                    detail: {
                        offsetCenter: [0, -30],
                        textStyle: {
                            fontSize: 16
                        }
                    },
                    data: [{ value: 0, name: '' }]
                }
            ]
        },
        lineOption = {
            tooltip: {
                trigger: 'axis',
                formatter: '{b}: {c} {a}'
            },
            grid: {
                top: 15,
                left: 0,
                right: 5,
                bottom: 0,
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                splitLine: { show: false },
                data: ['00′00″']
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
                    data: [0]
                }
            ]
        };
    //#endregion

    //#region UI
    var leftPanel = Ext.create('Ext.tree.Panel', {
        region: 'west',
        title: '系统层级',
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
            icon: $$iPems.icons.All
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
            },
            listeners: {
                load: function (me, node, records, successful) {
                    if (successful) {
                        var nodes = [];
                        Ext.Array.each(records, function (item, index, allItems) {
                            nodes.push(item.getId());
                        });

                        if (nodes.length > 0) {
                            $$iPems.UpdateIcons(leftPanel, nodes);
                        }
                    }
                }
            }
        }),
        listeners: {
            select: function (me, record, index) {
                select(record);
                videor(record);
            }
        },
        tbar: [
            {
                id: 'left-search-field',
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
                xtype: 'button',
                glyph: 0xf005,
                handler: function () {
                    var tree = leftPanel,
                        search = Ext.getCmp('left-search-field'),
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
                            mask: new Ext.LoadMask({ target: tree, msg: '正在处理...' }),
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
    });

    var chartPanel = Ext.create('Ext.panel.Panel', {
        border: false,
        layout: {
            type: 'hbox',
            align: 'stretch',
            pack: 'start'
        },
        items: [
            {
                xtype: 'container',
                flex: 1,
                contentEl: 'gauge-chart'
            }, {
                xtype: 'container',
                flex: 3,
                contentEl: 'line-chart'
            }
        ],
        listeners: {
            resize: function (me, width, height, oldWidth, oldHeight) {
                if (gaugeChart) gaugeChart.resize();
                if (lineChart) lineChart.resize();
            }
        }
    });

    var pointGrid = Ext.create('Ext.grid.Panel', {
        border: false,
        flex: 2,
        header: {
            items: [
                {
                    xtype: 'toolbar',
                    border: false,
                    padding: 0,
                    style: {
                        padding: 0,
                        background: 'transparent none repeat scroll 0 0'
                    },
                    items: [
                        {
                            xtype: 'checkboxgroup',
                            items: [
                                { xtype: 'checkboxfield', width: 50, boxLabel: '遥信', inputValue: $$iPems.Point.DI, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                { xtype: 'checkboxfield', width: 50, boxLabel: '遥测', inputValue: $$iPems.Point.AI, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                { xtype: 'checkboxfield', width: 50, boxLabel: '遥调', inputValue: $$iPems.Point.AO, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                { xtype: 'checkboxfield', width: 50, boxLabel: '遥控', inputValue: $$iPems.Point.DO, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                { xtype: 'checkboxfield', width: 50, boxLabel: '告警', inputValue: $$iPems.Point.AL, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' }
                            ],
                            listeners: {
                                change: function (me, newValue, oldValue) {
                                    var types = [];
                                    Ext.Object.each(newValue, function (key, value, myself) {
                                        types.push(value);
                                    });

                                    currentStore.proxy.extraParams.types = types;
                                    currentStore.loadPage(1);
                                }
                            }
                        },'-',
                        {
                            width: 150,
                            xtype: 'checkbox',
                            boxLabel: '仅显示有效数据',
                            inputValue: false,
                            checked: false,
                            boxLabelCls: 'x-label-header x-form-cb-label',
                            listeners: {
                                change: function (me, newValue, oldValue) {
                                    currentStore.proxy.extraParams.normal = newValue;
                                    currentStore.loadPage(1);
                                }
                            }
                        }
                    ]
                }
            ]
        },
        tools: [
            {
                type: 'maximize',
                tooltip: '最大化/还原',
                handler: function (event, toolEl, owner, tool) {
                    chartPanel.setVisible(!chartPanel.isVisible());
                    tool.setType(chartPanel.isVisible() ? 'maximize' : 'restore');
                }
            }
        ],
        store: currentStore,
        bbar: currentPagingToolbar,
        viewConfig: {
            loadMask: false,
            preserveScrollOnRefresh: true,
            stripeRows: true,
            trackOver: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
            getRowClass: function (record, rowIndex, rowParams, store) {
                return $$iPems.GetStateCls(record.get("statusid"));
            }
        },
        features: [{
            ftype: 'grouping',
            groupHeaderTpl: '{columnName}: {name} ({rows.length}条)',
            hideGroupedHeader: false,
            startCollapsed: false
        }],
        columns: [
            { text: '序号', dataIndex: 'index', width: 60 },
            { text: '所属区域', dataIndex: 'area' },
            { text: '所属站点', dataIndex: 'station' },
            { text: '所属机房', dataIndex: 'room' },
            { text: '所属设备', dataIndex: 'device' },
            { text: '信号名称', dataIndex: 'point' },
            { text: '信号类型', dataIndex: 'type' },
            { text: '信号测值', dataIndex: 'value' },
            { text: '单位/描述', dataIndex: 'unit' },
            { text: '信号状态', dataIndex: 'status', tdCls: 'x-status-cell', align: 'center' },
            { text: '测值时间', dataIndex: 'time', width: 150 },
            {
                xtype: 'actioncolumn',
                width: 100,
                align: 'center',
                menuDisabled: true,
                menuText: '操作',
                text: '操作',
                items: [{
                    tooltip: '遥控',
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('typeid') === $$iPems.Point.DO && $$iPems.ControlOperation) ? 'x-cell-icon x-icon-remote-control' : 'x-cell-icon x-icon-hidden';
                    },
                    handler: function (view, rowIndex, colIndex, item, event, record) {
                        view.getSelectionModel().select(record);
                        var form = controlWnd.getComponent('controlForm'),
                            device = form.getComponent('device'),
                            point = form.getComponent('point'),
                            result = Ext.getCmp('controlResult');

                        device.setValue(record.get('deviceid'));
                        point.setValue(record.get('pointid'));
                        result.setTextWithIcon('', '')
                        controlWnd.show();
                    }
                }, {
                    tooltip: '遥调',
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('typeid') === $$iPems.Point.AO && $$iPems.AdjustOperation) ? 'x-cell-icon x-icon-remote-setting' : 'x-cell-icon x-icon-hidden';
                    },
                    handler: function (view, rowIndex, colIndex, item, event, record) {
                        view.getSelectionModel().select(record);
                        var form = adjustWnd.getComponent('adjustForm'),
                            device = form.getComponent('device'),
                            point = form.getComponent('point'),
                            result = Ext.getCmp('adjustResult');

                        device.setValue(record.get('deviceid'));
                        point.setValue(record.get('pointid'));
                        result.setTextWithIcon('', '')
                        adjustWnd.show();
                    }
                }, {
                    getTip: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('followed') === true) ? '已关注' : '关注';
                    },
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('followedOnly') === false) ? ((r.get('followed') === true) ? 'x-cell-icon x-icon-tick' : 'x-cell-icon x-icon-add') : 'x-cell-icon x-icon-hidden';
                    },
                    handler: function (view, rowIndex, colIndex, item, event, record) {
                        if (record.get('followed')) return false;

                        Ext.Ajax.request({
                            url: '/Home/AddFollowPoint',
                            params: { device: record.get('deviceid'), point: record.get('pointid') },
                            mask: new Ext.LoadMask({ target: view, msg: '正在处理...' }),
                            success: function (response, options) {
                                var data = Ext.decode(response.responseText, true);
                                if (data.success) {
                                    currentPagingToolbar.doRefresh();
                                } else {
                                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                }
                            }
                        });
                    }
                }, {
                    tooltip: '取消关注',
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('followedOnly') === true) ? 'x-cell-icon x-icon-delete' : 'x-cell-icon x-icon-hidden';
                    },
                    handler: function (view, rowIndex, colIndex, item, event, record) {
                        Ext.Ajax.request({
                            url: '/Home/RemoveFollowPoint',
                            params: { device: record.get('deviceid'), point: record.get('pointid') },
                            mask: new Ext.LoadMask({ target: view, msg: '正在处理...' }),
                            success: function (response, options) {
                                var data = Ext.decode(response.responseText, true);
                                if (data.success) {
                                    currentPagingToolbar.doRefresh();
                                } else {
                                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                }
                            }
                        });
                    }
                }]
            }
        ],
        listeners: {
            selectionchange: function (model, selected) {
                resetChart();
                if (selected.length > 0) {
                    loadChart(selected[0]);
                }
            }
        }
    });

    var pointPanel = Ext.create('Ext.panel.Panel', {
        xtype: 'panel',
        title: '实时测值',
        glyph: 0xf039,
        pager: currentPagingToolbar,
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        items: [chartPanel, pointGrid]
    });

    var matrixPanel = Ext.create('Ext.grid.Panel', {
        title: '综合测值',
        glyph: 0xf055,
        border: false,
        selType: 'cellmodel',
        store: matrixStore,
        bbar: matrixPagingToolbar,
        pager: matrixPagingToolbar,
        tbar: [
            {
                xtype: 'button',
                glyph: 0xf008,
                text: '模版设置',
                handler: function () {
                    resetMatrixTree();
                    matrixWnd.show();
                }
            }, '-',
            {
                id: 'matrixTemplates',
                xtype: "combo",
                fieldLabel: '测值模版',
                displayField: 'text',
                valueField: 'id',
                typeAhead: true,
                queryMode: 'local',
                triggerAction: 'all',
                selectOnFocus: true,
                forceSelection: true,
                labelWidth: 60,
                width: 365,
                store: templateStore
            },
            {
                xtype: 'button',
                glyph: 0xf005,
                text: '应用模版',
                handler: function () {
                    var me = Ext.getCmp('matrixTemplates'),
                        template = me.getValue();

                    Ext.util.Cookies.set('ipems.matrix.template', template);
                    loadMatrixColumn(template);
                }
            }, '-',
            {
                xtype: 'button',
                glyph: 0xf058,
                text: '刷新数据',
                handler: function () {
                    refresh(matrixPanel);
                }
            }
        ],
        viewConfig: {
            loadMask: false,
            stripeRows: true,
            trackOver: false,
            preserveScrollOnRefresh: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [],
        listeners: {
            cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
            }
        }
    });

    var videoIFrame = Ext.create('Ext.ux.IFrame', {
        flex: 1,
        loadMask: '正在处理...',
        src: '/Home/Videor?view=videor'
    });

    var videoPanel = Ext.create('Ext.panel.Panel', {
        title: '实时视频',
        glyph: 0xf066,
        border: false,
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        items: [videoIFrame]
    });

    var alarmToolbar = Ext.create('Ext.panel.Panel', {
        border: false,
        dock: 'top',
        items: [
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'alarm-station-type-multicombo',
                        xtype: 'StationTypeMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        id: 'alarm-room-type-multicombo',
                        xtype: 'RoomTypeMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        id: 'alarm-subdevice-type-multipicker',
                        xtype: 'SubDeviceTypeMultiPicker',
                        emptyText: '默认全部',
                        width: 220
                    },
                    {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '应用条件',
                        handler: function (me, event) {
                            var stationTypes = Ext.getCmp('alarm-station-type-multicombo').getValue(),
                                roomTypes = Ext.getCmp('alarm-room-type-multicombo').getValue(),
                                subDeviceTypes = Ext.getCmp('alarm-subdevice-type-multipicker').getValue(),
                                subLogicTypes = Ext.getCmp('alarm-sublogic-type-multipicker').getValue(),
                                points = Ext.getCmp('alarm-point-multipicker').getValue(),
                                levels = Ext.getCmp('alarm-level-multicombo').getValue(),
                                confirms = Ext.getCmp('alarm-confirm-multicombo').getValue(),
                                reservations = Ext.getCmp('alarm-reservation-multicombo').getValue(),
                                seniorNode = Ext.getCmp('alarm-senior-condition').getValue();

                            var proxy = alarmStore.getProxy();
                            proxy.extraParams.stationTypes = stationTypes;
                            proxy.extraParams.roomTypes = roomTypes;
                            proxy.extraParams.subDeviceTypes = subDeviceTypes;
                            proxy.extraParams.subLogicTypes = subLogicTypes;
                            proxy.extraParams.points = points;
                            proxy.extraParams.levels = levels;
                            proxy.extraParams.confirms = confirms;
                            proxy.extraParams.reservations = reservations;
                            proxy.extraParams.seniorNode = seniorNode;
                            alarmStore.loadPage(1);
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'alarm-sublogic-type-multipicker',
                        xtype: 'SubLogicTypeMultiPicker',
                        emptyText: '默认全部',
                        width: 220
                    },
                    {
                        id: 'alarm-point-multipicker',
                        xtype: 'PointMultiPicker',
                        emptyText: '默认全部',
                        width: 220
                    },
                    {
                        id: 'alarm-level-multicombo',
                        xtype: 'AlarmLevelMultiCombo',
                        emptyText: '默认全部'
                    }, {
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        handler: function (me, event) {
                            download();
                        }
                    }
                ]
            },
            {
                xtype: 'toolbar',
                border: false,
                items: [
                    {
                        id: 'alarm-confirm-multicombo',
                        xtype: 'ConfirmMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        id: 'alarm-reservation-multicombo',
                        xtype: 'ReservationMultiCombo',
                        emptyText: '默认全部'
                    },
                    {
                        id: 'alarm-senior-condition',
                        xtype: 'SeniorConditionCombo'
                    }
                ]
            }
        ]
    });

    var alarmGrid = Ext.create('Ext.grid.Panel', {
        title: '实时告警信息',
        glyph: 0xf029,
        selType: 'checkboxmodel',
        border: false,
        flex: 1,
        store: alarmStore,
        bbar: alarmPagingToolbar,
        tools: [
            {
                type: 'maximize',
                tooltip: '最大化/还原',
                handler: function (event, toolEl, owner, tool) {
                    alarmToolbar.setVisible(!alarmToolbar.isVisible());
                    tool.setType(alarmToolbar.isVisible() ? 'maximize' : 'restore');
                }
            }
        ],
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
                text: '告警时间',
                dataIndex: 'time',
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
                text: '触发值',
                dataIndex: 'value',
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
                e.stopEvent();
                almContextMenu.record = record;
                almContextMenu.showAt(e.getXY());
            }
        }
    });

    var alarmPanel = Ext.create('Ext.panel.Panel', {
        xtype: 'panel',
        title: '实时告警',
        glyph: 0xf065,
        pager: alarmPagingToolbar,
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        dockedItems: [alarmToolbar],
        items: [alarmGrid]
    });

    var recordPanel = Ext.create('Ext.grid.Panel', {
        title: '最近刷卡',
        glyph: 0xf020,
        border: false,
        store: recordStore,
        bbar: recordPagingToolbar,
        pager: recordPagingToolbar,
        viewConfig: {
            loadMask: false,
            stripeRows: true,
            trackOver: false,
            preserveScrollOnRefresh: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [
            { text: '序号', dataIndex: 'index', width: 60 },
            { text: '所属区域', dataIndex: 'area' },
            { text: '所属站点', dataIndex: 'station' },
            { text: '所属机房', dataIndex: 'room' },
            { text: '设备名称', dataIndex: 'device' },
            { text: '刷卡类型', dataIndex: 'recType' },
            { text: '刷卡描述', dataIndex: 'remark' },
            { text: '刷卡卡号', dataIndex: 'decimalCard' },
            { text: '刷卡时间', dataIndex: 'time' },
            { text: '刷卡人员', dataIndex: 'employeeName' },
            { text: '人员类型', dataIndex: 'employeeType' },
            { text: '所属部门', dataIndex: 'department' }
        ],
        listeners: {
            itemcontextmenu: function (me, record, item, index, e) {
                e.stopEvent();
                recordContextMenu.record = record;
                recordContextMenu.showAt(e.getXY());
            }
        }
    });

    var centerPanel = Ext.create('Ext.tab.Panel', {
        xtype: 'tabpanel',
        region: 'center',
        items: [pointPanel, matrixPanel, alarmPanel, recordPanel, videoPanel],
        listeners: {
            tabchange: function (me, newCard, oldCard) {
                refresh(newCard);
            }
        }
    });

    var currentLayout = Ext.create('Ext.panel.Panel', {
        id: 'currentLayout',
        region: 'center',
        layout: 'border',
        border: false,
        items: [leftPanel, centerPanel]
    });
    //#endregion

    //#region Windows
    var controlWnd = Ext.create('Ext.window.Window', {
        title: '信号遥控',
        height: 250,
        width: 400,
        glyph: 0xf040,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        items: [{
            xtype: 'form',
            itemId: 'controlForm',
            border: false,
            padding: 10,
            items: [
                {
                    itemId: 'device',
                    xtype: 'hiddenfield',
                    name: 'device'
                },
                {
                    itemId: 'point',
                    xtype: 'hiddenfield',
                    name: 'point'
                },
                {
                    itemId: 'controlradio',
                    xtype: 'radiogroup',
                    columns: 1,
                    vertical: true,
                    items: [
                        { boxLabel: '常开控制(0)', name: 'ctrl', inputValue: 0, checked: true },
                        { boxLabel: '常闭控制(1)', name: 'ctrl', inputValue: 1 },
                        { boxLabel: '脉冲控制(2)', name: 'ctrl', inputValue: 2 }
                    ]
                }]
        }],
        buttons: [
          { id: 'controlResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '遥控',
              handler: function (el, e) {
                  var form = controlWnd.getComponent('controlForm'),
                      baseForm = form.getForm(),
                      result = Ext.getCmp('controlResult');

                  result.setTextWithIcon('', '');
                  if (baseForm.isValid()) {
                      result.setTextWithIcon('正在处理...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/ControlPoint',
                          success: function (form, action) {
                              result.setTextWithIcon(action.result.message, 'x-icon-accept');
                          },
                          failure: function (form, action) {
                              var message = 'undefined error.';
                              if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                  message = action.result.message;

                              result.setTextWithIcon(message, 'x-icon-error');
                          }
                      });
                  } else {
                      result.setTextWithIcon('表单填写错误', 'x-icon-error');
                  }
              }
          }, {
              xtype: 'button',
              text: '关闭',
              handler: function (el, e) {
                  var form = controlWnd.getComponent('controlForm'),
                      baseForm = form.getForm();

                  baseForm.reset();
                  controlWnd.close();
              }
          }
        ]
    });

    var adjustWnd = Ext.create('Ext.window.Window', {
        title: '信号遥调',
        height: 250,
        width: 400,
        glyph: 0xf028,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        items: [{
            xtype: 'form',
            itemId: 'adjustForm',
            border: false,
            padding: 10,
            items: [
                {
                    itemId: 'device',
                    xtype: 'hiddenfield',
                    name: 'device'
                },
                {
                    itemId: 'point',
                    xtype: 'hiddenfield',
                    name: 'point'
                },
                {
                    itemId: 'adjust',
                    xtype: 'numberfield',
                    name: 'adjust',
                    fieldLabel: '模拟量输出值',
                    value: 0,
                    width: 280,
                    allowOnlyWhitespace: false
                }]
        }],
        buttons: [
          { id: 'adjustResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '遥调',
              handler: function (el, e) {
                  var form = adjustWnd.getComponent('adjustForm'),
                      baseForm = form.getForm(),
                      result = Ext.getCmp('adjustResult');

                  result.setTextWithIcon('', '');
                  if (baseForm.isValid()) {
                      result.setTextWithIcon('正在处理...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/AdjustPoint',
                          success: function (form, action) {
                              result.setTextWithIcon(action.result.message, 'x-icon-accept');
                          },
                          failure: function (form, action) {
                              var message = 'undefined error.';
                              if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                  message = action.result.message;

                              result.setTextWithIcon(message, 'x-icon-error');
                          }
                      });
                  } else {
                      result.setTextWithIcon('表单填写错误', 'x-icon-error');
                  }
              }
          }, {
              xtype: 'button',
              text: '关闭',
              handler: function (el, e) {
                  var form = adjustWnd.getComponent('adjustForm'),
                      baseForm = form.getForm();

                  baseForm.reset();
                  adjustWnd.close();
              }
          }
        ]
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

    var matrixWnd = Ext.create('Ext.window.Window', {
        title: '模版设置',
        height: 500,
        width: 800,
        glyph: 0xf008,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        layout: 'border',
        currentNode: null,
        createCount: 1,
        items: [
            {
                id: 'templatePanel',
                xtype: 'treepanel',
                region: 'west',
                margin: '5 0 5 5',
                width: 200,
                autoScroll: true,
                useArrows: false,
                rootVisible: false,
                root: {
                    id: 'root',
                    text: '全部',
                    expanded: true,
                    icon: $$iPems.icons.All
                },
                viewConfig: {
                    loadMask: true
                },
                store: Ext.create('Ext.data.TreeStore', {
                    model: 'TemplateModel',
                    autoLoad: false,
                    nodeParam: 'node',
                    proxy: {
                        type: 'ajax',
                        url: '/Component/GetMatrixTemplates',
                        reader: {
                            type: 'json',
                            successProperty: 'success',
                            messageProperty: 'message',
                            totalProperty: 'total',
                            root: 'data'
                        }
                    }
                }),
                listeners: {
                    select: function (me, record, index) {
                        matrixWnd.currentNode = record;
                        var values = record.get('custom'),
                            basic = Ext.getCmp('templateForm').getForm();

                        if (!Ext.isEmpty(values)) {
                            basic.setValues(values);
                        }
                    }
                },
                tbar: [
                        {
                            id: 'tbar-add',
                            xtype: 'button',
                            glyph: 0xf001,
                            tooltip: '新增模版',
                            handler: function () {
                                var template = Ext.getCmp('templatePanel');
                                Ext.Ajax.request({
                                    url: '/Home/CreateMatrixTemplate',
                                    params: { index: matrixWnd.createCount++ },
                                    mask: new Ext.LoadMask(template.getView(), { msg: '正在处理...' }),
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data.success){
                                            var root = template.getRootNode(),
                                                current = root.createNode(data.data);

                                            root.appendChild(current);
                                            template.getSelectionModel().select(current);
                                        } else {
                                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                        }
                                    }
                                });
                            }
                        }, '-',
                        {
                            id: 'tbar-delete',
                            xtype: 'button',
                            glyph: 0xf003,
                            tooltip: '删除模版',
                            handler: function () {
                                if (matrixWnd.currentNode == null) {
                                    Ext.Msg.show({ title: '系统警告', msg: '请选择需要删除的模版', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
                                    return false;
                                }

                                Ext.Msg.confirm('确认对话框', '您确定要删除模版吗？', function (buttonId, text) {
                                    if (buttonId === 'yes') {
                                        var template = Ext.getCmp('templatePanel'),
                                            root = template.getRootNode();

                                        if (root.hasChildNodes()) {
                                            var current = matrixWnd.currentNode,
                                                next = current.nextSibling || current.previousSibling;

                                            root.removeChild(current);
                                            matrixWnd.currentNode = null;

                                            if (next !== null) {
                                                template.getSelectionModel().select(next);
                                            } else {
                                                resetMatrixForm();
                                            }
                                        }
                                    }
                                });
                            }
                        }, '-',
                        {
                            id: 'tbar-refresh',
                            xtype: 'button',
                            glyph: 0xf058,
                            tooltip: '重置模版',
                            handler: function () {
                                resetMatrixTree();
                                resetMatrixForm();
                            }
                        }
                ]
            },
            {
                id: 'templateForm',
                xtype: 'form',
                border: false,
                region: 'center',
                defaultType: 'textfield',
                layout: {
                    type: 'vbox',
                    align: 'stretch',
                    pack: 'start'
                },
                fieldDefaults: {
                    labelWidth: 60,
                    labelAlign: 'left',
                    anchor: '100%'
                },
                items: [
                    {
                        id: 'templateName',
                        name: 'name',
                        xtype: 'textfield',
                        margin: '5 5 5 5',
                        fieldLabel: '模版名称',
                        allowBlank: false,
                        listeners: {
                            change: {
                                fn: function (me, newValue, oldValue) {
                                    if (matrixWnd.currentNode != null) {
                                        var current = matrixWnd.currentNode,
                                            values = current.get('custom');

                                        values.name = newValue;
                                        current.set('text', newValue);
                                        current.set('custom', values);
                                    }
                                },
                                buffer: 500
                            }
                        }
                    },
                    {
                        id: 'templateDeviceType',
                        name: 'type',
                        xtype: 'DeviceTypeCombo',
                        margin: '5 5 5 5',
                        listeners: {
                            change: function (me, newValue, oldValue) {
                                if (matrixWnd.currentNode != null) {
                                    var current = matrixWnd.currentNode,
                                        values = current.get('custom');

                                    if (values.type !== newValue) {
                                        values.type = newValue;
                                        values.points = [];
                                        current.set('custom', values);
                                    }
                                }

                                var store = columnStore,
                                    proxy = store.getProxy();
                                
                                proxy.extraParams.node = newValue;
                                store.load();
                            }
                        }
                    },
                    {
                        id: 'templateValues',
                        name: 'points',
                        xtype: 'itemselector',
                        margin: '5 5 5 5',
                        flex: 1,
                        store: columnStore,
                        fieldLabel: '列名映射',
                        fromTitle: '待映射信号',
                        toTitle: '已映射信号',
                        displayField: 'text',
                        valueField: 'id',
                        value: [],
                        allowBlank: false,
                        minSelections: 1,
                        maxSelections: 20,
                        listeners: {
                            change: function (me, newValue, oldValue) {
                                if (matrixWnd.currentNode != null) {
                                    var current = matrixWnd.currentNode,
                                        values = current.get('custom');

                                    values.points = newValue;
                                    current.set('custom', values);
                                }
                            }
                        }
                    }
                ]
            }
        ],
        listeners: {
            close: function (panel) {
                resetMatrixForm();
            }
        },
        buttons: [
          { id: 'matrixResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '保存',
              handler: function (el, e) {
                  var result = Ext.getCmp('matrixResult'),
                      root = Ext.getCmp('templatePanel').getRootNode(),
                      datas = [];

                  var isValid = true;
                  if (root.hasChildNodes()) {
                      root.eachChild(function (c) {
                          var value = c.get('custom'),
                              name = value.name,
                              points = value.points;

                          if (Ext.isEmpty(points) || points.length === 0) {
                              result.setTextWithIcon(Ext.String.format('{0}映射信号最少选择1项', name), 'x-icon-error');
                              return isValid = false;
                          }

                          if (points.length > 20) {
                              result.setTextWithIcon(Ext.String.format('{0}映射信号最多选择20项', name), 'x-icon-error');
                              return isValid = false;
                          }

                          datas.push(value);
                      });
                  }

                  if (isValid === false) return false;
                  if (datas.length === 0) {
                      result.setTextWithIcon('未添加模版，无需保存。', 'x-icon-error');
                      return false;
                  }

                  result.setTextWithIcon('正在处理...', 'x-icon-loading');
                  Ext.Ajax.request({
                      url: '/Home/SaveMatrixTemplates',
                      method: 'POST',
                      jsonData: datas,
                      success: function (response, options) {
                          var data = Ext.decode(response.responseText, true);
                          if (data.success === true) {
                              templateStore.reload();
                              result.setTextWithIcon(data.message, 'x-icon-accept');
                          } else {
                              result.setTextWithIcon(data.message, 'x-icon-error');
                          }
                      },
                      failure: function (response, options) {
                          result.setTextWithIcon('unknown error.', 'x-icon-error');
                      }
                  });
              }
          }, {
              xtype: 'button',
              text: '关闭',
              handler: function (el, e) {
                  matrixWnd.close();
              }
          }
        ]
    });
    //#endregion

    //#region ContextMenu
    var almContextMenu = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        record: null,
        items: [{
            itemId: 'confirm',
            glyph: 0xf035,
            text: '选中告警确认',
            handler: function () {
                confirm(alarmGrid);
            }
        },
        {
            itemId: 'allconfirm',
            glyph: 0xf035,
            text: '全部告警确认',
            handler: function () {
                confirmAll(alarmGrid);
            }
        }, '-', {
            itemId: 'location',
            glyph: 0xf019,
            text: '告警定位',
            handler: function () {
                locateAlarm(almContextMenu.record);
            }
        }, '-', {
            itemId: 'refresh',
            glyph: 0xf058,
            text: '刷新列表',
            handler: function () {
                refresh();
            }
        }, '-', {
            itemId: 'export',
            glyph: 0xf010,
            text: '数据导出',
            handler: function () {
                download();
            }
        }]
    });

    var recordContextMenu = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        record: null,
        items: [{
            itemId: 'export',
            glyph: 0xf010,
            text: '数据导出',
            handler: function () {
                download();
            }
        }]
    });
    //#endregion

    //#region Methods
    var currentTab = function () {
        return centerPanel.getActiveTab();
    }

    var resetChart = function (fill) {
        fill = fill || false;

        gaugeOption.series[0].min = 0;
        gaugeOption.series[0].max = 100;
        gaugeOption.series[0].name = '';
        gaugeOption.series[0].data[0] = { value: 0, name: '' };
        gaugeChart.setOption(gaugeOption, true);

        lineOption.series[0].name = '';
        lineOption.series[0].data = fill ? [0] : [];
        lineOption.xAxis[0].data = fill ? ['00′00″'] : [];
        lineChart.setOption(lineOption, true);
    };

    var loadChart = function (record) {
        if (record != null) {
            var maxcount = 90,
                timestamp = record.get('timestamp'),
                point = record.get('point'),
                value = record.get('value'),
                unit = record.get('unit');

            if (value === 'NULL') value = 0;
            if (value >= 0) {
                if (value <= 100) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 100;
                } else if (value > 100 && value <= 500) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 500;
                } else if (value > 500 && value <= 1000) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 1000;
                } else if (value > 1000 && value <= 5000) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 5000;
                } else {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 10000;
                }
            } else {
                if (value >= -100) {
                    gaugeOption.series[0].min = -100;
                    gaugeOption.series[0].max = 0;
                } else if (value < -100 && value >= -500) {
                    gaugeOption.series[0].min = -500;
                    gaugeOption.series[0].max = 0;
                } else if (value < -500 && value >= -1000) {
                    gaugeOption.series[0].min = -1000;
                    gaugeOption.series[0].max = 0;
                } else if (value < -1000 && value >= -5000) {
                    gaugeOption.series[0].min = -5000;
                    gaugeOption.series[0].max = 0;
                } else {
                    gaugeOption.series[0].min = -10000;
                    gaugeOption.series[0].max = 0;
                }
            }

            gaugeOption.series[0].name = unit;
            gaugeOption.series[0].data[0].name = point;
            gaugeOption.series[0].data[0].value = value;
            gaugeChart.setOption(gaugeOption, true);

            if (lineOption.series[0].data.length >= maxcount) {
                lineOption.series[0].data.shift();
                lineOption.xAxis[0].data.shift();
            }

            lineOption.series[0].name = unit;
            lineOption.series[0].data.push(value);
            lineOption.xAxis[0].data.push(timestamp);
            lineChart.setOption(lineOption, true);
        }
    };

    var select = function (node) {
        resetPointGrid(node);

        var proxy0 = currentStore.getProxy(),
            proxy1 = matrixStore.getProxy(),
            proxy2 = alarmStore.getProxy(),
            proxy3 = recordStore.getProxy();

        proxy0.extraParams.node =
        proxy1.extraParams.node =
        proxy3.extraParams.node =
        proxy2.extraParams.baseNode = node.getId();
        reload();
    };

    var videor = function (node) {
        if (!Ext.isEmpty(node)) {
            Ext.util.Cookies.set('videor_node', node.getId());
        }

        if (videoIFrame.rendered) {
            var local = videoIFrame.getWin().location;
            videoIFrame.src = local.pathname + local.search;
            videoIFrame.load();
        }
    };

    var refresh = function (current) {
        var pager = (current || currentTab()).pager;
        if (Ext.isEmpty(pager)) return;
        pager.doRefresh();
    };

    var reload = function (current) {
        var pager = (current || currentTab()).pager;
        if (Ext.isEmpty(pager)) return;

        pager.getStore().loadPage(1);
    };

    var download = function (current) {
        var pager = (current || currentTab()).pager;
        if (Ext.isEmpty(pager)) return;

        var store = pager.getStore();
        if (Ext.isEmpty(store.downloadURL)) return;

        $$iPems.download({
            url: store.downloadURL,
            params: store.getProxy().extraParams
        });
    };

    var resetPointGrid = function (node) {
        var id = node.getId(),
            ids = $$iPems.SplitKeys(id),
            columns = pointGrid.columns,
            selection = pointGrid.getSelectionModel();

        if (ids.length === 2 && parseInt(ids[0]) === $$iPems.SSH.Device) {
            columns[1].hide();
            columns[2].hide();
            columns[3].hide();
            columns[4].hide();
        } else {
            columns[1].show();
            columns[2].show();
            columns[3].show();
            columns[4].show();
        }

        if (selection.hasSelection()) {
            selection.clearSelections();
            resetChart(true);
        }
    }

    var confirm = function (current) {
        var selection = current.getSelectionModel();
        if (!selection.hasSelection()) {
            Ext.Msg.show({ title: '系统警告', msg: '请勾选需要确认的告警', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        var keys = [], models = selection.getSelection();
        Ext.Msg.confirm('确认对话框', Ext.String.format('确认选中的{0}条告警，您确定吗？', models.length), function (buttonId, text) {
            if (buttonId === 'yes') {
                Ext.Array.each(models, function (item, index, allItems) {
                    keys.push(item.get('id'));
                });

                Ext.Ajax.request({
                    url: '/Home/ConfirmAlarms',
                    params: { keys: keys },
                    mask: new Ext.LoadMask(current.getView(), { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success)
                            refresh();
                        else
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                });
            }
        });
    };

    var confirmAll = function (current) {
        Ext.Msg.confirm('确认对话框', '您确定要全部确认吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                Ext.Ajax.request({
                    url: '/Home/ConfirmAllAlarms',
                    params: { onlyReservation: false, onlySystem: false },
                    mask: new Ext.LoadMask(current.getView(), { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success)
                            refresh();
                        else
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                });
            }
        });
    };

    var showResDetail = function (id, view) {
        if (Ext.isEmpty(id)) return false;
        Ext.Ajax.request({
            url: '/Home/GetReservation',
            Method: 'POST',
            params: { id: id },
            mask: new Ext.LoadMask(view, { msg: '正在处理...' }),
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var id = reservationWnd.getComponent('id'),
                        name = reservationWnd.getComponent('name'),
                        start = reservationWnd.getComponent('start'),
                        end = reservationWnd.getComponent('end'),
                        project = reservationWnd.getComponent('project'),
                        creator = reservationWnd.getComponent('creator'),
                        time = reservationWnd.getComponent('time'),
                        comment = reservationWnd.getComponent('comment');

                    id.setValue(data.data.id);
                    name.setValue(data.data.name);
                    start.setValue(data.data.startDate);
                    end.setValue(data.data.endDate);
                    project.setValue(data.data.projectName);
                    creator.setValue(data.data.creator);
                    time.setValue(data.data.createdTime);
                    comment.setValue(data.data.comment);
                    reservationWnd.show();
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    var locateAlarm = function (record) {
        if (Ext.isEmpty(record)) return;
        var device = record.get('deviceid'),
            point = record.get('pointid'),
            tree = leftPanel;

        Ext.Ajax.request({
            url: '/Component/GetDevicePath',
            params: { nodes: [Ext.String.format('{0}{1}{2}', $$iPems.SSH.Device, $$iPems.Separator, device)] },
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    Ext.each(data.data, function (item, index, all) {
                        item = Ext.Array.from(item);
                        if (item.length > 0) {
                            $$iPems.selectNodePath(tree, item, function () {
                                currentStore.selectPoint = point;
                                centerPanel.setActiveTab(pointPanel);
                            });
                        }
                    });
                }
            }
        });
    };

    var resetMatrixTree = function () {
        var me = Ext.getCmp('templatePanel'),
            store = me.getStore();

        matrixWnd.currentNode = null;
        store.load();
    };

    var resetMatrixForm = function () {
        var templateBasic = Ext.getCmp('templateForm').getForm(),
            templateDevType = Ext.getCmp('templateDeviceType'),
            templateDevStore = templateDevType.getStore(),
            templateResult = Ext.getCmp('matrixResult');

        templateBasic.reset();
        templateResult.setTextWithIcon('', '');
        if (templateDevStore.getCount()) templateDevType.select(templateDevStore.getAt(0));
    };

    var loadMatrixColumn = function (template) {
        var me = matrixStore,
            grid = matrixPanel,
            view = grid.getView(),
            mask = new Ext.LoadMask({ target: view, msg: '获取列名...' });

        Ext.Ajax.request({
            url: '/Home/GetMatrixColumns',
            params: { id: template },
            mask: grid.rendered === true ? mask : null,
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    me.model.prototype.fields.clear();
                    me.removeAll();
                    var columns = [];
                    if (data.data && Ext.isArray(data.data)) {
                        Ext.Array.each(data.data, function (item, index) {
                            me.model.prototype.fields.replace({ name: item.name, type: item.type });
                            if (Ext.isEmpty(item.column)) return true;
                            columns.push({ text: item.column, dataIndex: item.name, width: item.width });
                        });
                    }

                    grid.reconfigure(me, columns);
                    me.getProxy().extraParams.id = template;
                    me.loadPage(1);
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };
    //#endregion

    //#region onReady
    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }

        $$iPems.Tasks.actPointTask.run = function () {
            refresh();
            $$iPems.UpdateIcons(leftPanel, null);
        };
        $$iPems.Tasks.actPointTask.start();

        //初始化视频cookie
        Ext.util.Cookies.set('videor_node', 'root');
    });

    Ext.onReady(function () {
        gaugeChart = echarts.init(document.getElementById("gauge-chart"), 'shine');
        lineChart = echarts.init(document.getElementById("line-chart"), 'shine');

        //init charts
        gaugeChart.setOption(gaugeOption);
        lineChart.setOption(lineOption);
    });
    //#endregion

})();