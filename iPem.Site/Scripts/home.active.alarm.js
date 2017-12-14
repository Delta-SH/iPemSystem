(function () {

    //#region Model
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
            { name: 'supporter', type: 'string' },
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

    Ext.define('HisAlarmModel', {
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
            { name: 'supporter', type: 'string' },
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
    //#endregion

    //#region Store
    var currentStore_0 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ActAlarmModel',
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
                    $$iPems.Tasks.actAlmTask.fireOnStart = false;
                    $$iPems.Tasks.actAlmTask.restart();
                }
            }
        }
    });

    var currentStore_1 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ActAlarmModel',
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
                onlyConfirms: true,
                onlyReservations: false,
                onlySystem: false
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    $$iPems.Tasks.actAlmTask.fireOnStart = false;
                    $$iPems.Tasks.actAlmTask.restart();
                }
            }
        }
    });

    var currentStore_2 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ActAlarmModel',
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
                onlyReservations: true,
                onlySystem: false
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    $$iPems.Tasks.actAlmTask.fireOnStart = false;
                    $$iPems.Tasks.actAlmTask.restart();
                }
            }
        }
    });

    var currentStore_3 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'HisAlarmModel',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Home/RequestRecoveries',
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
                keywords: Ext.emptyString
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    $$iPems.Tasks.actAlmTask.fireOnStart = false;
                    $$iPems.Tasks.actAlmTask.restart();
                }
            }
        }
    });

    var currentStore_4 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ActAlarmModel',
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
                onlySystem: true
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    $$iPems.Tasks.actAlmTask.fireOnStart = false;
                    $$iPems.Tasks.actAlmTask.restart();
                }
            }
        }
    });

    var detailStore_0 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ActAlarmModel',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Home/RequestActAlmDetail',
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
                primary: false,
                related: false,
                filter: false
            },
            simpleSortMode: true
        }
    });

    var detailStore_1 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'HisAlarmModel',
        proxy: {
            type: 'ajax',
            actionMethods: {
                create: 'POST',
                read: 'POST',
                update: 'POST',
                destroy: 'POST'
            },
            url: '/Home/RequestHisAlmDetail',
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
                reversal: false
            },
            simpleSortMode: true
        }
    });
    //#endregion

    //#region Pager
    var currentPagingToolbar_0 = $$iPems.clonePagingToolbar(currentStore_0);
    var currentPagingToolbar_1 = $$iPems.clonePagingToolbar(currentStore_1);
    var currentPagingToolbar_2 = $$iPems.clonePagingToolbar(currentStore_2);
    var currentPagingToolbar_3 = $$iPems.clonePagingToolbar(currentStore_3);
    var currentPagingToolbar_4 = $$iPems.clonePagingToolbar(currentStore_4);
    var detailPagingToolbar_0 = $$iPems.clonePagingToolbar(detailStore_0);
    var detailPagingToolbar_1 = $$iPems.clonePagingToolbar(detailStore_1);
    //#endregion

    //#region Left UI
    var leftBase = Ext.create('Ext.tree.Panel', {
        id: 'baseConditionPanel',
        glyph: 0xf056,
        title: '基本条件',
        border: false,
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
                            $$iPems.UpdateIcons(leftBase, nodes);
                        }
                    }
                }
            }
        }),
        listeners: {
            select: function (me, record, item, index) {
                var baseNode = record.getId();
                currentStore_0.getProxy().extraParams.baseNode = baseNode;
                currentStore_1.getProxy().extraParams.baseNode = baseNode;
                currentStore_2.getProxy().extraParams.baseNode = baseNode;
                currentStore_3.getProxy().extraParams.baseNode = baseNode;
                reload();
            }
        },
        tbar: [
                {
                    id: 'base-search-field',
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
                    id: 'base-search-button',
                    xtype: 'button',
                    glyph: 0xf005,
                    handler: function () {
                        var tree = Ext.getCmp('baseConditionPanel'),
                            search = Ext.getCmp('base-search-field'),
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

    var leftSenior = Ext.create('Ext.tree.Panel', {
        id: 'seniorConditionPanel',
        glyph: 0xf034,
        title: '订制条件',
        border: false,
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
            autoLoad: false,
            nodeParam: 'node',
            proxy: {
                type: 'ajax',
                url: '/Component/GetSeniorConditions',
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
            select: function (me, record, item, index) {
                var seniorNode = record.getId(),
                    disabled = seniorNode === 'root';

                Ext.getCmp('tbar-edit').setDisabled(disabled);
                Ext.getCmp('tbar-delete').setDisabled(disabled);
            },
            itemcontextmenu: function (me, record, item, index, e) {
                showConditionContextMenu(me, record, item, index, e);
            }
        },
        tbar: [
                {
                    id: 'tbar-add',
                    xtype: 'button',
                    glyph: 0xf001,
                    tooltip: '新增条件',
                    handler: function () {
                        conditionAdd();
                    }
                }, '-',
                {
                    id: 'tbar-edit',
                    xtype: 'button',
                    glyph: 0xf002,
                    tooltip: '编辑条件',
                    disabled: true,
                    handler: function () {
                        conditionEdit();
                    }
                }, '-',
                {
                    id: 'tbar-delete',
                    xtype: 'button',
                    glyph: 0xf003,
                    tooltip: '删除条件',
                    disabled: true,
                    handler: function () {
                        conditionDelete();
                    }
                }
        ]
    });

    var leftPanel = Ext.create('Ext.tab.Panel', {
        border: true,
        region: 'west',
        title: '高级筛选',
        glyph: 0xf011,
        width: 220,
        tabPosition: 'bottom',
        split: true,
        collapsible: true,
        collapsed: false,
        bodyStyle: {
            'border-bottom': 'none'
        },
        items: [leftBase, leftSenior]
    });
    //#endregion

    //#region Center UI
    var centerGrid_0 = Ext.create('Ext.grid.Panel', {
        title: '全部告警',
        glyph: 0xf055,
        selType: 'checkboxmodel',
        border: false,
        store: currentStore_0,
        bbar: currentPagingToolbar_0,
        pager: currentPagingToolbar_0,
        downloadURL: '/Home/DownloadActAlms',
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
                text: '维护厂家',
                dataIndex: 'supporter'
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
                showAlarmContextMenu(me, record, item, index, e);
            }
        }
    });

    var centerGrid_1 = Ext.create('Ext.grid.Panel', {
        title: '确认告警',
        glyph: 0xf035,
        selType: 'checkboxmodel',
        border: false,
        store: currentStore_1,
        bbar: currentPagingToolbar_1,
        pager: currentPagingToolbar_1,
        downloadURL: '/Home/DownloadActAlms',
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
                text: '维护厂家',
                dataIndex: 'supporter'
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
                text: '工程信息',
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
                showAlarmContextMenu(me, record, item, index, e);
            }
        }
    });

    var centerGrid_2 = Ext.create('Ext.grid.Panel', {
        title: '工程告警',
        glyph: 0xf045,
        selType: 'checkboxmodel',
        border: false,
        store: currentStore_2,
        bbar: currentPagingToolbar_2,
        pager: currentPagingToolbar_2,
        downloadURL: '/Home/DownloadActAlms',
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
                text: '维护厂家',
                dataIndex: 'supporter'
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
                showAlarmContextMenu(me, record, item, index, e);
            }
        }
    });

    var centerGrid_3 = Ext.create('Ext.grid.Panel', {
        title: '恢复告警',
        glyph: 0xf057,
        selType: 'checkboxmodel',
        border: false,
        store: currentStore_3,
        bbar: currentPagingToolbar_3,
        pager: currentPagingToolbar_3,
        downloadURL: '/Home/DownloadRecoveries',
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
                text: '维护厂家',
                dataIndex: 'supporter'
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
            }
        }
    });

    var centerGrid_4 = Ext.create('Ext.grid.Panel', {
        title: '系统告警',
        glyph: 0xf063,
        selType: 'checkboxmodel',
        border: false,
        store: currentStore_4,
        bbar: currentPagingToolbar_4,
        pager: currentPagingToolbar_4,
        downloadURL: '/Home/DownloadActAlms',
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
            }
        ],
        listeners: {
            itemcontextmenu: function (me, record, item, index, e) {
                showAlarmContextMenu(me, record, item, index, e);
            }
        }
    });

    var detailGrid_0 = Ext.create('Ext.grid.Panel', {
        region: 'center',
        border: false,
        store: detailStore_0,
        bbar: detailPagingToolbar_0,
        pager: detailPagingToolbar_0,
        downloadURL: '/Home/DownloadActAlmDetail',
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
            }
        }
    });

    var detailGrid_1 = Ext.create('Ext.grid.Panel', {
        region: 'center',
        border: false,
        store: detailStore_1,
        bbar: detailPagingToolbar_1,
        pager: detailPagingToolbar_1,
        downloadURL: '/Home/DownloadHisAlmDetail',
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
            }
        }
    });

    var centerTab = Ext.create('Ext.tab.Panel', {
        xtype: 'tabpanel',
        margin: '5 0 0 0',
        flex: 1,
        items: [centerGrid_0, centerGrid_1, centerGrid_2, centerGrid_3, centerGrid_4],
        listeners: {
            tabchange: function (me, newCard, oldCard) {
                refresh(newCard);
            }
        }
    });

    var centerPanel = Ext.create('Ext.panel.Panel', {
        region: 'center',
        border: false,
        bodyCls: 'x-border-body-panel',
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        dockedItems: [
            {
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
                                glyph: 0xf005,
                                text: '应用条件',
                                handler: function (me, event) {
                                    var stationTypes = Ext.getCmp('station-type-multicombo').getValue(),
                                        roomTypes = Ext.getCmp('room-type-multicombo').getValue(),
                                        subDeviceTypes = Ext.getCmp('subdevice-type-multipicker').getValue(),
                                        subLogicTypes = Ext.getCmp('sublogic-type-multipicker').getValue(),
                                        points = Ext.getCmp('point-multipicker').getValue(),
                                        levels = Ext.getCmp('alarm-level-multicombo').getValue(),
                                        confirms = Ext.getCmp('confirm-multicombo').getValue(),
                                        reservations = Ext.getCmp('reservation-multicombo').getValue(),
                                        seniorNode = Ext.getCmp('senior-condition').getValue();

                                    var proxy0 = currentStore_0.getProxy(),
                                        proxy1 = currentStore_1.getProxy(),
                                        proxy2 = currentStore_2.getProxy(),
                                        proxy3 = currentStore_3.getProxy(),
                                        proxy4 = currentStore_4.getProxy();

                                    proxy0.extraParams.stationTypes =
                                    proxy1.extraParams.stationTypes =
                                    proxy2.extraParams.stationTypes =
                                    proxy3.extraParams.stationTypes = 
                                    proxy4.extraParams.stationTypes = stationTypes;

                                    proxy0.extraParams.roomTypes = 
                                    proxy1.extraParams.roomTypes = 
                                    proxy2.extraParams.roomTypes =
                                    proxy3.extraParams.roomTypes = 
                                    proxy4.extraParams.roomTypes = roomTypes;

                                    proxy0.extraParams.subDeviceTypes = 
                                    proxy1.extraParams.subDeviceTypes = 
                                    proxy2.extraParams.subDeviceTypes =
                                    proxy3.extraParams.subDeviceTypes = 
                                    proxy4.extraParams.subDeviceTypes = subDeviceTypes;

                                    proxy0.extraParams.subLogicTypes = 
                                    proxy1.extraParams.subLogicTypes = 
                                    proxy2.extraParams.subLogicTypes =
                                    proxy3.extraParams.subLogicTypes = 
                                    proxy4.extraParams.subLogicTypes = subLogicTypes;

                                    proxy0.extraParams.points = 
                                    proxy1.extraParams.points = 
                                    proxy2.extraParams.points =
                                    proxy3.extraParams.points = 
                                    proxy4.extraParams.points = points;

                                    proxy0.extraParams.levels = 
                                    proxy1.extraParams.levels = 
                                    proxy2.extraParams.levels =
                                    proxy3.extraParams.levels = 
                                    proxy4.extraParams.levels = levels;

                                    proxy0.extraParams.confirms = 
                                    proxy1.extraParams.confirms = 
                                    proxy2.extraParams.confirms =
                                    proxy3.extraParams.confirms =
                                    proxy4.extraParams.confirms = confirms;

                                    proxy0.extraParams.reservations = 
                                    proxy1.extraParams.reservations = 
                                    proxy2.extraParams.reservations =
                                    proxy3.extraParams.reservations = 
                                    proxy4.extraParams.reservations = reservations;

                                    proxy0.extraParams.seniorNode =
                                    proxy1.extraParams.seniorNode =
                                    proxy2.extraParams.seniorNode =
                                    proxy3.extraParams.seniorNode =
                                    proxy4.extraParams.seniorNode = seniorNode;

                                    reload();
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
                                xtype: 'button',
                                glyph: 0xf034,
                                text: '保存条件',
                                handler: function (me, event) {
                                    var active = leftPanel.getActiveTab();
                                    if (active != leftSenior) leftPanel.setActiveTab(leftSenior);

                                    var stationTypes = Ext.getCmp('station-type-multicombo').getValue(),
                                        roomTypes = Ext.getCmp('room-type-multicombo').getValue(),
                                        subDeviceTypes = Ext.getCmp('subdevice-type-multipicker').getValue(),
                                        subLogicTypes = Ext.getCmp('sublogic-type-multipicker').getValue(),
                                        points = Ext.getCmp('point-multipicker').getValue(),
                                        levels = Ext.getCmp('alarm-level-multicombo').getValue(),
                                        confirms = Ext.getCmp('confirm-multicombo').getValue(),
                                        reservations = Ext.getCmp('reservation-multicombo').getValue();

                                    conditionAdd({
                                        stationTypes: stationTypes,
                                        roomTypes: roomTypes,
                                        subDeviceTypes: subDeviceTypes,
                                        subLogicTypes: subLogicTypes,
                                        points: points,
                                        levels: levels,
                                        confirms: confirms,
                                        reservations: reservations
                                    });
                                }
                            }
                        ]
                    },
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'confirm-multicombo',
                                xtype: 'ConfirmMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'reservation-multicombo',
                                xtype: 'ReservationMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'senior-condition',
                                xtype: 'SeniorConditionCombo'
                            }, {
                                xtype: 'button',
                                glyph: 0xf010,
                                text: '数据导出',
                                handler: function (me, event) {
                                    download();
                                }
                            }
                        ]
                    }
                ]
            }
        ],
        items: [centerTab]
    });

    var currentLayout = Ext.create('Ext.panel.Panel', {
        id: 'currentLayout',
        region: 'center',
        layout: 'border',
        border: false,
        items: [leftPanel, centerPanel]
    });
    //#endregion

    //#region Window
    var conditionWnd = Ext.create('Ext.window.Window', {
        title: '新增条件',
        height: 350,
        width: 600,
        modal: true,
        border: false,
        hidden: true,
        glyph: 0xf001,
        closeAction: 'hide',
        opaction: $$iPems.Action.Add,
        items: [{
            id: 'conditionForm',
            xtype: 'form',
            border: false,
            defaultType: 'textfield',
            fieldDefaults: {
                labelWidth: 60,
                labelAlign: 'left',
                margin: '15 15 15 15',
                anchor: '100%'
            },
            items: [{
                id: 'conditionId',
                name: 'id',
                xtype: 'hiddenfield'
            }, {
                id: 'conditionName',
                name: 'name',
                xtype: 'textfield',
                fieldLabel: '条件名称',
                allowBlank: false,
                margin: '15 15 0 15'
            }, {
                xtype: 'container',
                layout: 'hbox',
                items: [
                    {
                        xtype: 'container',
                        flex: 1,
                        layout: 'anchor',
                        items: [
                            {
                                id: 'conditionStaionTypes',
                                name: 'stationTypes',
                                xtype: 'StationTypeMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'conditionSubDeviceTypes',
                                name: 'subDeviceTypes',
                                xtype: 'SubDeviceTypeMultiPicker',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'conditionPoints',
                                name: 'points',
                                xtype: 'PointMultiPicker',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'conditionConfirms',
                                name: 'confirms',
                                xtype: 'ConfirmMultiCombo',
                                emptyText: '默认全部'
                            }
                        ]
                    }, {
                        xtype: 'container',
                        flex: 1,
                        layout: 'anchor',
                        items: [
                            {
                                id: 'conditionRoomTypes',
                                name: 'roomTypes',
                                xtype: 'RoomTypeMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'conditionSubLogicTypes',
                                name: 'subLogicTypes',
                                xtype: 'SubLogicTypeMultiPicker',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'conditionLevels',
                                name: 'levels',
                                xtype: 'AlarmLevelMultiCombo',
                                emptyText: '默认全部'
                            },
                            {
                                id: 'conditionReservations',
                                name: 'reservations',
                                xtype: 'ReservationMultiCombo',
                                emptyText: '默认全部'
                            }
                        ]
                    }
                ]
            },
            {
                id: 'conditionKeywords',
                name: 'keywords',
                xtype: 'textfield',
                fieldLabel: '关键字',
                emptyText: '多关键字请以;分隔，例: A;B;C',
                margin: '0 15 15 15'
            }
            ]
        }],
        buttons: [
          { id: 'conditionResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button', text: '保存', handler: function (el, e) {
                  var form = Ext.getCmp('conditionForm').getForm(),
                      result = Ext.getCmp('conditionResult');

                  result.setTextWithIcon('', '');
                  if (form.isValid()) {
                      result.setTextWithIcon('正在处理...', 'x-icon-loading');
                      form.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/SaveSeniorCondition',
                          params: {
                              action: conditionWnd.opaction
                          },
                          success: function (form, action) {
                              result.setTextWithIcon(action.result.message, 'x-icon-accept');
                              leftSenior.getStore().reload();
                              Ext.getCmp('senior-condition').getStore().reload();
                          },
                          failure: function (form, action) {
                              var message = 'undefined error.';
                              if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                  message = action.result.message;

                              result.setTextWithIcon(message, 'x-icon-error');
                          }
                      });
                  }
              }
          },
          {
              xtype: 'button', text: '关闭', handler: function (el, e) {
                  conditionWnd.close();
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

    var actAlarmWnd = Ext.create('Ext.window.Window', {
        title: '告警详情',
        glyph: 0xf029,
        height: 500,
        width: 800,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        layout: 'border',
        items: [detailGrid_0],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: '导出',
            handler: function (el, e) {
                download(detailGrid_0);
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                actAlarmWnd.hide();
            }
        }]
    });

    var hisAlarmWnd = Ext.create('Ext.window.Window', {
        title: '告警详情',
        glyph: 0xf029,
        height: 500,
        width: 800,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        layout: 'border',
        items: [detailGrid_1],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: '导出',
            handler: function (el, e) {
                download(detailGrid_1);
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                hisAlarmWnd.hide();
            }
        }]
    });
    //#endregion

    //#region ContextMenu
    var conditionContextMenu = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        source: null,
        items: [{
            itemId: 'add',
            glyph: 0xf001,
            text: '新增条件',
            handler: function () {
                conditionAdd();
            }
        }, '-', {
            itemId: 'edit',
            glyph: 0xf002,
            text: '编辑条件',
            handler: function () {
                var me = conditionContextMenu;
                if (Ext.isEmpty(me.source) || me.source.getId() === 'root')
                    return false;

                conditionEdit(me.source.getId());
            }
        }, '-', {
            itemId: 'delete',
            glyph: 0xf003,
            text: '删除条件',
            handler: function () {
                var me = conditionContextMenu;
                if (Ext.isEmpty(me.source) || me.source.getId() === 'root')
                    return false;

                conditionDelete(me.source.getId());
            }
        }],
        listeners: {
            beforeshow: function (me) {
                var disabled = Ext.isEmpty(me.source) || me.source.getId() === 'root';
                me.getComponent('edit').setDisabled(disabled);
                me.getComponent('delete').setDisabled(disabled);
            }
        }
    });

    var almContextMenu = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        source: null,
        record: null,
        items: [{
            itemId: 'confirm',
            glyph: 0xf035,
            text: '选中告警确认',
            handler: function () {
                var me = almContextMenu;
                if (me.source == null) return false;

                confirm(me.source);
            }
        },
        {
            itemId: 'allconfirm',
            glyph: 0xf035,
            text: '全部告警确认',
            handler: function () {
                var me = almContextMenu;
                if (me.source == null) return false;

                confirmAll(me.source);
            }
        }, '-', {
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
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showActDetail(id, Ext.String.format('主次告警详单({0})', name), true, false, false);
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
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showActDetail(id, Ext.String.format('关联告警详单({0})', name), false, true, false);
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
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showActDetail(id, Ext.String.format('过滤告警详单({0})', name), false, false, true);
                    }
                }, {
                    itemId: 'reversal',
                    glyph: 0xf029,
                    text: '翻转告警',
                    handler: function () {
                        var me = almContextMenu;
                        if (me.record == null) return false;
                        var id = me.record.get('id');
                        if (Ext.isEmpty(id)) return false;
                        var name = me.record.get('point');
                        if (Ext.isEmpty(name)) name = '--';

                        showHisDetail(id, Ext.String.format('翻转告警详单({0})', name), true);
                    }
                }
            ]
        }, '-', {
            itemId: 'refresh',
            glyph: 0xf058,
            text: '刷新列表',
            handler: function () {
                var me = almContextMenu;
                if (me.source == null) return false;
                me.source.pager.doRefresh();
            }
        }, '-', {
            itemId: 'export',
            glyph: 0xf010,
            text: '数据导出',
            handler: function () {
                var me = almContextMenu;
                if (me.source == null) return false;
                download(me.source);
            }
        }],
        listeners: {
            beforeshow: function (me) {
                if (Ext.isEmpty(me.source)) return false;

                var disabled = me.source == centerGrid_1;
                me.getComponent('confirm').setDisabled(disabled);
                me.getComponent('allconfirm').setDisabled(disabled);
            }
        }
    });
    //#endregion

    //#region Methods
    var conditionAdd = function (condition) {
        var form = Ext.getCmp('conditionForm').getForm();
        form.load({
            url: '/Home/GetSeniorCondition',
            params: { id: '', action: $$iPems.Action.Add },
            waitMsg: '正在处理...',
            waitTitle: '系统提示',
            success: function (form, action) {
                form.clearInvalid();
                Ext.getCmp('conditionResult').setTextWithIcon('', '');
                if (!Ext.isEmpty(condition)) {
                    form.setValues(condition);
                }

                conditionWnd.setGlyph(0xf001);
                conditionWnd.setTitle('新增条件');
                conditionWnd.opaction = $$iPems.Action.Add;
                conditionWnd.show();
            }
        });
    }

    var conditionEdit = function (id) {
        if (id == null) {
            var selection = leftSenior.getSelectionModel();
            if (!selection.hasSelection()) {
                Ext.Msg.show({ title: '系统警告', msg: '请选择需要编辑的告警条件', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
                return false;
            }

            id = selection.getSelection()[0].getId();
        }

        if (id === 'root') {
            Ext.Msg.show({ title: '系统警告', msg: '无法编辑根节点', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        var form = Ext.getCmp('conditionForm').getForm();
        form.load({
            url: '/Home/GetSeniorCondition',
            params: { id: id, action: $$iPems.Action.Edit },
            waitMsg: '正在处理...',
            waitTitle: '系统提示',
            success: function (form, action) {
                form.clearInvalid();
                Ext.getCmp('conditionResult').setTextWithIcon('', '');

                conditionWnd.setGlyph(0xf002);
                conditionWnd.setTitle('编辑条件');
                conditionWnd.opaction = $$iPems.Action.Edit;
                conditionWnd.show();
            }
        });
    }

    var conditionDelete = function (id) {
        var tree = leftSenior;
        if (id == null) {
            var selection = tree.getSelectionModel();
            if (!selection.hasSelection()) {
                Ext.Msg.show({ title: '系统警告', msg: '请选择需要删除的告警条件', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
                return false;
            }

            id = selection.getSelection()[0].getId();
        }

        if (id === 'root') {
            Ext.Msg.show({ title: '系统警告', msg: '无法删除根节点', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        Ext.Msg.confirm('确认对话框', '您确认要删除吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                Ext.Ajax.request({
                    url: '/Home/DeleteSeniorCondition',
                    params: { id: id },
                    mask: new Ext.LoadMask(tree, { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            tree.getStore().reload();
                            Ext.getCmp('senior-condition').getStore().reload();
                        } else {
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        });
    }

    var showConditionContextMenu = function (me, record, item, index, e) {
        e.stopEvent();
        conditionContextMenu.source = record;
        conditionContextMenu.showAt(e.getXY());
    }

    var currentAlarmTab = function () {
        return centerTab.getActiveTab();
    }

    var refresh = function (current) {
        current = current || currentAlarmTab();
        current.pager.doRefresh();
    };

    var reload = function (current) {
        current = current || currentAlarmTab();
        current.getStore().loadPage(1);
    };

    var download = function (current) {
        current = current || currentAlarmTab();
        $$iPems.download({
            url: current.downloadURL,
            params: current.getStore().getProxy().extraParams
        });
    };

    var showAlarmContextMenu = function (me, record, item, index, e) {
        e.stopEvent();
        almContextMenu.record = record;
        almContextMenu.source = currentAlarmTab();
        almContextMenu.showAt(e.getXY());
    }

    var confirm = function (current) {
        if (current == centerGrid_1)
            return false;

        if (current == centerGrid_3)
            return false;

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
                            current.pager.doRefresh();
                        else
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                });
            }
        });
    };

    var confirmAll = function (current) {
        if (current == centerGrid_1)
            return false;

        if (current == centerGrid_3)
            return false;

        Ext.Msg.confirm('确认对话框', '您确定要全部确认吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                Ext.Ajax.request({
                    url: '/Home/ConfirmAllAlarms',
                    params: { onlyReservation: current == centerGrid_2, onlySystem: current == centerGrid_4 },
                    mask: new Ext.LoadMask(current.getView(), { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success)
                            current.pager.doRefresh();
                        else
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                });
            }
        });
    }

    var showResDetail = function (id, view) {
        if (Ext.isEmpty(id)) return false;
        view = view || currentAlarmTab().getView();

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
    }

    var showActDetail = function (id, title, primary, related, filter) {
        var store = detailGrid_0.getStore(),
            proxy = store.getProxy();

        proxy.extraParams.id = id;
        proxy.extraParams.title = title;
        proxy.extraParams.primary = primary;
        proxy.extraParams.related = related;
        proxy.extraParams.filter = filter;
        store.loadPage(1);

        actAlarmWnd.setTitle(title);
        actAlarmWnd.show();
    }

    var showHisDetail = function (id, title, reversal) {
        var store = detailGrid_1.getStore(),
            proxy = store.getProxy();

        proxy.extraParams.id = id;
        proxy.extraParams.title = title;
        proxy.extraParams.reversal = reversal;
        store.loadPage(1);

        hisAlarmWnd.setTitle(title);
        hisAlarmWnd.show();
    }
    //#endregion

    //#region onReady
    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }

        $$iPems.Tasks.actAlmTask.run = function () {
            refresh();
            $$iPems.UpdateIcons(leftBase, null);
        };
        $$iPems.Tasks.actAlmTask.start();
    });
    //#endregion

})();