(function () {
    var currentNode = null;

    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
			{ name: 'id', type: 'int' },
            { name: 'level', type: 'string' },
            { name: 'levelValue', type: 'int' },
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

    var selectPath = function (tree, ids, callback) {
        var root = tree.getRootNode(),
            field = 'id',
            separator = '/',
            path = ids.join(separator);

        path = separator + root.get(field) + separator + path;
        tree.selectPath(path, field, separator, callback || Ext.emptyFn);
    };

    var refresh = function (pagingtoolbar) {
        Ext.Ajax.request({
            url: '../Home/RequestRefreshActAlm',
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    pagingtoolbar.doRefresh();
                } else {
                    Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    var change = function (node, pagingtoolbar) {
        Ext.Ajax.request({
            url: '../Home/RequestRefreshActAlm',
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var me = pagingtoolbar.store, attributes = node.raw.attributes;
                    if (!Ext.isEmpty(attributes) && attributes.length > 0) {
                        var id = Ext.Array.findBy(attributes, function (item, index) {
                            return item.key === 'id';
                        });

                        if (!Ext.isEmpty(id))
                            me.proxy.extraParams.nodeid = id.value;

                        var type = Ext.Array.findBy(attributes, function (item, index) {
                            return item.key === 'type';
                        });

                        if (!Ext.isEmpty(type))
                            me.proxy.extraParams.nodetype = type.value;
                    }

                    me.loadPage(1);
                } else {
                    Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    var filter = function (pagingtoolbar) {
        Ext.Ajax.request({
            url: '../Home/RequestRefreshActAlm',
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var me = pagingtoolbar.store;

                    me.proxy.extraParams.statype = Ext.getCmp('station-type-multicombo').getSelectedValues();
                    me.proxy.extraParams.roomtype = Ext.getCmp('room-type-multicombo').getSelectedValues();
                    me.proxy.extraParams.devtype = Ext.getCmp('device-type-multicombo').getSelectedValues();
                    me.proxy.extraParams.almlevel = Ext.getCmp('alarm-level-multicombo').getSelectedValues();
                    me.proxy.extraParams.logictype = Ext.getCmp('logic-type-multicombo').getSelectedValues();
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
                } else {
                    Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

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
                    chartPie1.store.each(function (rec) {
                        total += rec.get('value');
                    });
                    this.setTitle(
                        $$iPems.lang.ActiveAlarm.PieTotal + total + '<br/>'
                        + storeItem.get('name') + ': ' + storeItem.get('value') + '<br/>'
                        + $$iPems.lang.ActiveAlarm.PieRate + Math.round(storeItem.get('value') / total * 100) + '%'
                        );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment'],
            proxy: {
                type: 'ajax',
                actionMethods: {
                    create: 'POST',
                    read: 'POST',
                    update: 'POST',
                    destroy: 'POST'
                },
                url: '../Home/RequestActChart1',
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
                        Ext.getCmp('chartPie2').store.load();
                    }
                }
            }
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
                    chartPie2.store.each(function (rec) {
                        total += rec.get('value');
                    });
                    this.setTitle(
                        $$iPems.lang.ActiveAlarm.PieTotal + total + '<br/>'
                        + storeItem.get('name') + ': ' + storeItem.get('value') + '<br/>'
                        + $$iPems.lang.ActiveAlarm.PieRate + Math.round(storeItem.get('value') / total * 100) + '%'
                        );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment'],
            proxy: {
                type: 'ajax',
                actionMethods: {
                    create: 'POST',
                    read: 'POST',
                    update: 'POST',
                    destroy: 'POST'
                },
                url: '../Home/RequestActChart2',
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
                        Ext.getCmp('chartPie3').store.load();
                    }
                }
            }
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
                    chartPie3.store.each(function (rec) {
                        total += rec.get('value');
                    });
                    this.setTitle(
                        $$iPems.lang.ActiveAlarm.PieTotal + total + '<br/>'
                        + storeItem.get('name') + ': ' + storeItem.get('value') + '<br/>'
                        + $$iPems.lang.ActiveAlarm.PieRate + Math.round(storeItem.get('value') / total * 100) + '%'
                        );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment'],
            proxy: {
                type: 'ajax',
                actionMethods: {
                    create: 'POST',
                    read: 'POST',
                    update: 'POST',
                    destroy: 'POST'
                },
                url: '../Home/RequestActChart3',
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
                        $$iPems.Tasks.actAlmTask.fireOnStart = false;
                        $$iPems.Tasks.actAlmTask.restart();
                    }
                }
            }
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
                point: '',
                confirm: 'all',
                project: 'all'
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    var chartStore1 = Ext.getCmp('chartPie1').store,
                        chartStore2 = Ext.getCmp('chartPie2').store,
                        chartStore3 = Ext.getCmp('chartPie3').store;

                    chartStore1.proxy.extraParams = me.proxy.extraParams;
                    chartStore2.proxy.extraParams = me.proxy.extraParams;
                    chartStore3.proxy.extraParams = me.proxy.extraParams;

                    chartStore1.load();
                }
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

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
                listeners:{
                    itemclick: function (me, record, item, index, event) {
                        currentNode = record;
                        change(currentNode, currentPagingToolbar);
                    }
                },
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

                                selectPath(tree, paths[index]);
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
                                                selectPath(tree, data.data[0]);
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
                id: 'alarm-dashboard',
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
                                refresh(currentPagingToolbar);
                            }
                        }
                    ],
                    items: [{
                        id: 'active-alarm-grid',
                        xtype: 'grid',
                        selType: 'checkboxmodel',
                        border: false,
                        store: currentStore,
                        loadMask: false,
                        viewConfig: {
                            loadMask: false,
                            preserveScrollOnRefresh: true,
                            stripeRows: true,
                            trackOver: true,
                            getRowClass: function (record, rowIndex, rowParams, store) {
                                return $$iPems.GetAlmLevelCls(record.get("levelValue"));
                            }
                        },
                        columns: [
                            { text: $$iPems.lang.ActiveAlarm.AlarmLevel, dataIndex: 'level', align: 'center', locked: true, tdCls: 'x-level-cell' },
                            { text: $$iPems.lang.ActiveAlarm.AlarmStart, dataIndex: 'start', align: 'center', width: 150, locked: true, tdCls: 'x-level-cell' },
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
                                    handler: function (me, event) {
                                        filter(currentPagingToolbar);
                                    },
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
                                                $$iPems.download({
                                                    url: '../Home/DownloadActAlms',
                                                    params: currentStore.proxy.extraParams
                                                });
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
})();