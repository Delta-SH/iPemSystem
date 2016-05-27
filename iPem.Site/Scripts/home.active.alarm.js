(function () {
    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
			{ name: 'id', type: 'string' },
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
            { name: 'project', type: 'string' },
            { name: 'confirmedstatus', type: 'string' },
            { name: 'confirmedtime', type: 'string' },
            { name: 'confirmer', type: 'string' }
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

    var change = function (node, pagingtoolbar) {
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
    };

    var filter = function (pagingtoolbar) {
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
    };

    var print = function (store) {
        $$iPems.download({
            url: '/Home/DownloadActAlms',
            params: store.proxy.extraParams
        });
    };

    var confirm = function (pagingtoolbar) {
        var grid = Ext.getCmp('active-alarm-grid'),
            selmodel = grid.getSelectionModel();

        if (!selmodel.hasSelection()) {
            Ext.Msg.show({ title: $$iPems.lang.SysWarningTitle, msg: $$iPems.lang.ActiveAlarm.Confirm.Warning, buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        var models = selmodel.getSelection();
        Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, Ext.String.format($$iPems.lang.ActiveAlarm.Confirm.Confirm, models.length), function (buttonId, text) {
            if (buttonId === 'yes') {
                var ids = [];
                Ext.Array.each(models, function (item, index, allItems) {
                    ids.push(item.get('id'));
                });

                Ext.Ajax.request({
                    url: '/Home/ConfirmAlarms',
                    params: { ids: ids },
                    mask: new Ext.LoadMask(grid, { msg: $$iPems.lang.AjaxHandling }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success)
                            pagingtoolbar.doRefresh();
                        else
                            Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                });
            }
        });
    };

    var projectWnd = Ext.create('Ext.window.Window', {
        title: $$iPems.lang.ActiveAlarm.Window.Project.Title,
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
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.Id
        }, {
            itemId: 'startTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.StartTime
        }, {
            itemId: 'endTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.EndTime
        }, {
            itemId: 'projectName',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.ProjectName
        }, {
            itemId: 'creator',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.Creator
        }, {
            itemId: 'createdTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.CreatedTime
        }, {
            itemId: 'comment',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.ActiveAlarm.Window.Project.Fields.Comment
        }],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: $$iPems.lang.Close,
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
                        $$iPems.lang.ActiveAlarm.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.ActiveAlarm.Chart.PieRate,
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
                        $$iPems.lang.ActiveAlarm.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.ActiveAlarm.Chart.PieRate,
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
                        $$iPems.lang.ActiveAlarm.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.ActiveAlarm.Chart.PieRate,
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
            url: '/Home/RequestActAlarms',
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
                id: 'alarm-organization',
                region: 'west',
                xtype: 'treepanel',
                title: $$iPems.lang.ActiveAlarm.MenuNavTitle,
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
                viewConfig: {
                    loadMask: true
                },
                store: Ext.create('Ext.data.TreeStore', {
                    autoLoad: false,
                    nodeParam: 'node',
                    proxy: {
                        type: 'ajax',
                        url: '/Home/GetOrganization',
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
                    select: function (me, record, item, index) {
                        change(record, currentPagingToolbar);
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
                                    url: '/Home/SearchOrganization',
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
                    title: $$iPems.lang.ActiveAlarm.RateTitle,
                    collapsible: true,
                    collapseFirst: false,
                    margin: '5 0 0 0',
                    flex: 1,
                    tools: [{
                        type: 'print',
                        tooltip: $$iPems.lang.Import,
                        handler: function (event, toolEl, panelHeader) {
                            Ext.ux.ImageExporter.save([chartPie1, chartPie2, chartPie3]);
                        }
                    }],
                    layout: {
                        type: 'hbox',
                        align: 'stretch',
                        pack: 'start'
                    },
                    items: [chartPie1, chartPie2, chartPie3]
                }, {
                    xtype: 'panel',
                    glyph: 0xf029,
                    title: $$iPems.lang.ActiveAlarm.DetailTitle,
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
                                currentPagingToolbar.doRefresh();
                            }
                        },
                        {
                            type: 'print',
                            tooltip: $$iPems.lang.Import,
                            handler: function (event, toolEl, panelHeader) {
                                print(currentStore);
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
                        normalViewConfig: {
                            emptyText: $$iPems.lang.GridEmptyText
                        },
                        viewConfig: {
                            loadMask: false,
                            preserveScrollOnRefresh: true,
                            stripeRows: true,
                            trackOver: true,
                            getRowClass: function (record, rowIndex, rowParams, store) {
                                return $$iPems.GetAlmLevelCls(record.get("levelValue"));
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
                                    mask: new Ext.LoadMask(view.ownerCt, { msg: $$iPems.lang.AjaxHandling }),
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
                                            Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                        }
                                    }
                                });
                            }
                        },
                        columns: [
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Level,
                                dataIndex: 'level',
                                align: 'center',
                                locked: true,
                                tdCls: 'x-level-cell'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Start,
                                dataIndex: 'start',
                                align: 'center',
                                width: 150,
                                locked: true,
                                tdCls: 'x-level-cell'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Id,
                                dataIndex: 'index',
                                width: 60
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Area,
                                dataIndex: 'area'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Station,
                                dataIndex: 'station'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Room,
                                dataIndex: 'room'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.DevType,
                                dataIndex: 'devType'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Device,
                                dataIndex: 'device'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Logic,
                                dataIndex: 'logic'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Point,
                                dataIndex: 'point'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Comment,
                                dataIndex: 'comment'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Value,
                                dataIndex: 'value'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Frequency,
                                dataIndex: 'frequency'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Project,
                                dataIndex: 'project',
                                renderer: function (value, p, record) {
                                    return Ext.String.format('<a href="javascript:void(0)" style="color:#157fcc;">{0}</a>', value);
                                }
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.ConfirmedStatus,
                                dataIndex: 'confirmedstatus'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.ConfirmedTime,
                                dataIndex: 'confirmedtime'
                            },
                            {
                                text: $$iPems.lang.ActiveAlarm.Columns.Confirmer,
                                dataIndex: 'confirmer'
                            }
                        ],
                        bbar: currentPagingToolbar,
                    }]
                }],
                dockedItems: [{
                    xtype: 'panel',
                    glyph: 0xf034,
                    title: $$iPems.lang.ActiveAlarm.ConditionTitle,
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
                                    emptyText: $$iPems.lang.AllEmptyText
                                },
                                {
                                    id: 'room-type-multicombo',
                                    xtype: 'RoomTypeMultiCombo',
                                    emptyText: $$iPems.lang.AllEmptyText
                                },
                                {
                                    id: 'device-type-multicombo',
                                    xtype: 'DeviceTypeMultiCombo',
                                    emptyText: $$iPems.lang.AllEmptyText
                                },
                                {
                                    xtype: 'splitbutton',
                                    glyph: 0xf005,
                                    text: $$iPems.lang.Ok,
                                    handler: function (me, event) {
                                        filter(currentPagingToolbar);
                                    },
                                    menu: [
                                        {
                                            text: $$iPems.lang.ActiveAlarm.ToolBar.ConfirmAlarm,
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
                                            text: $$iPems.lang.Import,
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
                                    id: 'alarm-level-multicombo',
                                    xtype: 'AlarmLevelMultiCombo',
                                    emptyText: $$iPems.lang.AllEmptyText
                                },
                                {
                                    id: 'logic-type-multicombo',
                                    xtype: 'LogicTypeMultiCombo',
                                    emptyText: $$iPems.lang.AllEmptyText
                                },
                                {
                                    id: 'point-name-textfield',
                                    xtype: 'textfield',
                                    fieldLabel: $$iPems.lang.ActiveAlarm.ToolBar.PointName,
                                    emptyText: $$iPems.lang.MultiConditionEmptyText,
                                    labelWidth: 60,
                                    width: 220
                                },
                                {
                                    id: 'other-option-button',
                                    xtype: 'button',
                                    text: $$iPems.lang.ActiveAlarm.ToolBar.OtherOption,
                                    menu: [
                                        {
                                            id: 'show-confirm-menu',
                                            xtype: 'menucheckitem',
                                            text: $$iPems.lang.ActiveAlarm.ToolBar.ShowConfirm,
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
                                            text: $$iPems.lang.ActiveAlarm.ToolBar.ShowUnConfirm,
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
                                            text: $$iPems.lang.ActiveAlarm.ToolBar.ShowProject,
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
                                            text: $$iPems.lang.ActiveAlarm.ToolBar.ShowUnProject,
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