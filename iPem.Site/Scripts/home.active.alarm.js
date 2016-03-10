(function () {
    var currentNode = null;

    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
			{ name: 'id', type: 'int' },
            { name: 'level', type: 'string' },
            { name: 'start', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'devType', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'logic', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'value', type: 'string' },
            { name: 'frequency', type: 'int' },
        ],
        idProperty: 'id'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        proxy: {
            type: 'ajax',
            method: 'POST',
            url: '../Home/RequestActAlarms',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                nodeid: 'root',
                nodetype: $$iPems.Organization.Area,
                statype: [],
                roomtype: [],
                devtype: [],
                almlevel: [],
                logictype: [],
                point: ''
            },
            simpleSortMode: true
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var createPieCharts = function (store) {
        return Ext.create('Ext.chart.Chart', {
            xtype: 'chart',
            store: store,
            animate: true,
            shadow: false,
            flex: 1,
            insetPadding: 5,
            theme: 'Base:gradients',
            legend: {
                position: 'right',
                itemSpacing: 3,
                boxStrokeWidth: 1,
                boxStroke: '#c0c0c0',
                labelFont: '12px Arial, sans-serif'
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
                    width: 120,
                    height: 50,
                    renderer: function (storeItem, item) {
                        var total = 0;
                        store.each(function (rec) {
                            total += rec.get('value');
                        });
                        this.setTitle(
                            $$iPems.lang.ActiveAlarm.PieTotal + total + '<br/>'
                            + storeItem.get('name') + ': ' + storeItem.get('value') + '<br/>'
                            + $$iPems.lang.ActiveAlarm.PieRate + Math.round(storeItem.get('value') / total * 100) + '%'
                            );
                    }
                }
            }]
        });
    };

    var chartStore1 = Ext.create('Ext.data.Store', {
        autoLoad: false,
        fields: ['name', 'value', 'comment'],
        proxy: {
            type: 'ajax',
            method: 'POST',
            url: '../Home/RequestActAlarms',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                nodeid: 'root',
                nodetype: $$iPems.Organization.Area,
                statype: [],
                roomtype: [],
                devtype: [],
                almlevel: [],
                logictype: [],
                point: ''
            },
            simpleSortMode: true
        }
    });

    var chartStore2 = Ext.create('Ext.data.Store', {
        fields: ['name', 'value', 'comment'],
        data: [
            { name: '开关电源', value: 96 },
            { name: '蓄电池组', value: 28 },
            { name: 'UPS', value: 105 },
            { name: '环境设备', value: 82 },
            { name: '其它设备', value: 88 },
        ]
    });

    var chartStore3 = Ext.create('Ext.data.Store', {
        fields: ['name', 'value', 'comment'],
        data: [
            { name: '浦东新区', value: 136 },
            { name: '徐汇区', value: 55 },
            { name: '黄浦区', value: 162 },
            { name: '静安区', value: 78 },
            { name: '虹口区', value: 28 }
        ]
    });

    var chartPie1 = createPieCharts(chartStore1);
    var chartPie2 = createPieCharts(chartStore2);
    var chartPie3 = createPieCharts(chartStore3);

    var selectAsyncNodePath = function (tree, ids, callback) {
        var root = tree.getRootNode(),
            field = 'id',
            separator = '/',
            path = ids.join(separator);

        path = separator + root.get(field) + separator + path;
        tree.selectPath(path, field, separator, callback || Ext.emptyFn);
    };

    Ext.onReady(function () {
        var currentLayout = Ext.create('Ext.panel.Panel', {
            region: 'center',
            layout: 'border',
            border: false,
            items: [{
                id: 'alarm-organization',
                region: 'west',
                xtype: 'treepanel',
                title: $$iPems.lang.ActiveAlarm.DevList,
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
                    text: $$iPems.lang.All,
                    expanded: true,
                    icon: $$iPems.icons.Home,
                    attributes: [
                        { key: 'id', value: 'root' },
                        { key: 'type', value: $$iPems.Organization.Area }
                    ]
                },
                store: Ext.create('Ext.data.TreeStore', {
                    autoLoad: false,
                    nodeParam: 'node',
                    proxy: {
                        type: 'ajax',
                        url: '../Home/GetOrganization',
                        extraParams: {
                            id: '',
                            type: -1
                        },
                        reader: {
                            type: 'json',
                            successProperty: 'success',
                            messageProperty: 'message',
                            totalProperty: 'total',
                            root: 'data'
                        }
                    },
                    listeners: {
                        beforeexpand: function (node) {
                            var me = this, attributes = node.raw.attributes;
                            if (!Ext.isEmpty(attributes) && attributes.length > 0) {
                                var id = Ext.Array.findBy(attributes, function (item, index) {
                                    return item.key === 'id';
                                });

                                if (!Ext.isEmpty(id))
                                    me.proxy.extraParams.id = id.value;

                                var type = Ext.Array.findBy(attributes, function (item, index) {
                                    return item.key === 'type';
                                });

                                if (!Ext.isEmpty(type))
                                    me.proxy.extraParams.type = type.value;
                            }
                        }
                    }
                }),
                tbar: [
                    {
                        id: 'alarm-search-field',
                        xtype: 'textfield',
                        emptyText: $$iPems.lang.PlsInputEmptyText,
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
                            var tree = Ext.getCmp('alarm-organization'),
                                search = Ext.getCmp('alarm-search-field'),
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
                                    Ext.Msg.show({ title: $$iPems.lang.SysTipTitle, msg: $$iPems.lang.SearchEndText, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                }

                                selectAsyncNodePath(tree, paths[index]);
                                search._filterIndex = index;
                            } else {
                                Ext.Ajax.request({
                                    url: '../Home/SearchOrganization',
                                    params: { text: text },
                                    mask: new Ext.LoadMask({ target: tree, msg: $$iPems.lang.AjaxHandling }),
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data.success) {
                                            var len = data.data.length;
                                            if (len > 0) {
                                                selectAsyncNodePath(tree, data.data[0]);
                                                search._filterData = data.data;
                                                search._filterIndex = 0;
                                            } else {
                                                Ext.Msg.show({ title: $$iPems.lang.SysTipTitle, msg: Ext.String.format($$iPems.lang.SearchEmptyText, text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                            }
                                        } else {
                                            Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
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
                    title: $$iPems.lang.ActiveAlarm.AlarmRate,
                    collapsible: true,
                    collapseFirst: false,
                    margin: '5 0 0 0',
                    flex: 1,
                    layout: {
                        type: 'hbox',
                        align: 'stretch',
                        pack: 'start'
                    },
                    tools: [
                        {
                            type: 'refresh',
                            tooltip: $$iPems.lang.Refresh,
                            handler: function (event, toolEl, panelHeader) {
                            }
                        }
                    ],
                    items: [chartPie1, chartPie2, chartPie3]
                }, {
                    xtype: 'panel',
                    glyph: 0xf029,
                    title: $$iPems.lang.ActiveAlarm.AlarmDetail,
                    collapsible: true,
                    collapseFirst: false,
                    layout: 'fit',
                    margin: '5 0 0 0',
                    flex: 2,
                    tools: [
                        {
                            type: 'refresh',
                            tooltip: $$iPems.lang.Refresh,
                            handler: function (event, toolEl, panelHeader) {
                            }
                        }
                    ],
                    items: [{
                        id: 'active-alarm-grid',
                        xtype: 'grid',
                        selType: 'checkboxmodel',
                        border: false,
                        columns: [
                            { text: $$iPems.lang.ActiveAlarm.AlarmLevel, dataIndex: 'level', align: 'center', locked: true },
                            { text: $$iPems.lang.ActiveAlarm.AlarmStart, dataIndex: 'start', align: 'center', locked: true },
                            { text: $$iPems.lang.ActiveAlarm.AlarmId, dataIndex: 'id', width: 80, align: 'center' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmArea, dataIndex: 'area' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmStation, dataIndex: 'station' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmRoom, dataIndex: 'room' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmDevType, dataIndex: 'devType' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmDevice, dataIndex: 'device' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmLogic, dataIndex: 'logic' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmPoint, dataIndex: 'point' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmComment, dataIndex: 'comment' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmValue, dataIndex: 'value' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmFrequency, dataIndex: 'frequency' }
                        ],
                        bbar: currentPagingToolbar,
                    }]
                }],
                dockedItems: [{
                    xtype: 'panel',
                    glyph: 0xf034,
                    title: $$iPems.lang.ActiveAlarm.AlarmCondition,
                    collapsible: true,
                    collapsed: false,
                    dock: 'top',
                    items: [
                        {
                            xtype: 'toolbar',
                            border: false,
                            items: [
                                { id: 'station-type-multicombo', xtype: 'station.type.multicombo' },
                                { id: 'room-type-multicombo', xtype: 'room.type.multicombo' },
                                { id: 'device-type-multicombo', xtype: 'device.type.multicombo' },
                                {
                                    xtype: 'splitbutton',
                                    glyph: 0xf005,
                                    text: $$iPems.lang.Ok,
                                    menu: [
                                        {
                                            text: $$iPems.lang.ConfirmAlarm,
                                            glyph: 0xf035,
                                            handler: function (me, event) {

                                            }
                                        },
                                        '-',
                                        {
                                            text: $$iPems.lang.Import,
                                            glyph: 0xf010,
                                            handler: function (me, event) {

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
                                { id: 'alarm-level-multicombo', xtype: 'alarm.level.multicombo' },
                                { id: 'logic-type-multicombo', xtype: 'logic.type.multicombo' },
                                { id: 'point-name-textfield', xtype: 'textfield', fieldLabel: $$iPems.lang.PointName, emptyText: $$iPems.lang.MultiConditionEmptyText, labelWidth: 60, width: 220 },
                                {
                                    id: 'other-option-button',
                                    xtype: 'button',
                                    text: $$iPems.lang.OtherOption,
                                    menu: [
                                        {
                                            id: 'show-confirm-menu',
                                            xtype: 'menucheckitem',
                                            text: $$iPems.lang.ShowConfirm,
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
                                            text: $$iPems.lang.ShowUnConfirm,
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
                                            text: $$iPems.lang.ShowProject,
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
                                            text: $$iPems.lang.ShowUnProject,
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

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });
})();