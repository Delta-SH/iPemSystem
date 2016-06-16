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
            url: '/Report/DownloadHistory400203',
            params: store.proxy.extraParams
        });
    };

    var projectWnd = Ext.create('Ext.window.Window', {
        title: $$iPems.lang.Report400203.Window.Project.Title,
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
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.Id
        }, {
            itemId: 'startTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.StartTime
        }, {
            itemId: 'endTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.EndTime
        }, {
            itemId: 'projectName',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.ProjectName
        }, {
            itemId: 'creator',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.Creator
        }, {
            itemId: 'createdTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.CreatedTime
        }, {
            itemId: 'comment',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400203.Window.Project.Fields.Comment
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

    var chartPie = Ext.create('Ext.chart.Chart', {
        id: 'chartPie',
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
                    chartPie.store.each(function (rec) {
                        total += rec.get('value');
                    });

                    this.update(
                        Ext.String.format('{0}: {1}<br/>{2}: {3}<br/>{4}: {5}%',
                        $$iPems.lang.Report400203.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400203.Chart.PieRate,
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

    var chartColumn = Ext.create('Ext.chart.Chart', {
        id: 'chartColumn',
        xtype: 'chart',
        animate: true,
        shadow: false,
        flex: 2,
        axes: [{
            type: 'Numeric',
            position: 'left',
            fields: ['value'],
            grid: true,
            minimum: 0
        }, {
            type: 'Category',
            position: 'bottom',
            fields: ['name']
        }],
        series: [{
            type: 'column',
            axis: 'left',
            highlight: true,
            highlightCfg: {
                lineWidth: 0
            },
            tips: {
                trackMouse: true,
                minWidth: 120,
                minHeight: 60,
                renderer: function (storeItem, item) {
                    var total = 0;
                    chartPie.store.each(function (rec) {
                        total += rec.get('value');
                    });

                    this.update(
                        Ext.String.format('{0}: {1}<br/>{2}: {3}<br/>{4}: {5}%',
                        $$iPems.lang.Report400203.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400203.Chart.PieRate,
                        (storeItem.get('value') / total * 100).toFixed(2))
                    );
                }
            },
            label: {
                display: 'outside',
                'text-anchor': 'middle',
                field: 'value',
                renderer: Ext.util.Format.numberRenderer('0'),
                orientation: 'horizontal',
                color: '#333'
            },
            xField: 'name',
            yField: 'value'
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
        groupField: 'levelDisplay',
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
            url: '/Report/RequestHistory400203',
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
                    var data = me.proxy.reader.jsonData;
                    var chartDataPie = $$iPems.ChartEmptyDataPie;
                    var chartDataColumn = $$iPems.ChartEmptyDataColumn;
                    if (!Ext.isEmpty(data)
                        && !Ext.isEmpty(data.chart)
                        && Ext.isArray(data.chart)
                        && data.chart.length > 0)
                        chartDataPie = chartDataColumn = data.chart;

                    chartPie.getStore().loadData(chartDataPie, false);
                    chartColumn.getStore().loadData(chartDataColumn, false);
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
                title: $$iPems.lang.Report400203.RateTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                layout: {
                    type: 'hbox',
                    align: 'stretch',
                    pack: 'start'
                },
                items: [chartPie, { xtype: 'component', width: 20 }, chartColumn]
            }, {
                id: 'history-alarm-grid',
                xtype: 'grid',
                glyph: 0xf029,
                title: $$iPems.lang.Report400203.DetailTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 2,
                store: currentStore,
                loadMask: true,
                tools: [{
                    type: 'print',
                    tooltip: $$iPems.lang.Import,
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
                viewConfig: {
                    loadMask: false,
                    preserveScrollOnRefresh: true,
                    stripeRows: true,
                    trackOver: true,
                    emptyText: $$iPems.lang.GridEmptyText
                },
                features: [{
                    ftype: 'grouping',
                    groupHeaderTpl: $$iPems.lang.Report400203.GroupTpl,
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
                        text: $$iPems.lang.Report400203.Columns.Id,
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Area,
                        dataIndex: 'area'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Station,
                        dataIndex: 'station'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Room,
                        dataIndex: 'room'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.DevType,
                        dataIndex: 'devType'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Device,
                        dataIndex: 'device'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Logic,
                        dataIndex: 'logic'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Point,
                        dataIndex: 'point'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Level,
                        dataIndex: 'levelDisplay'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.StartTime,
                        dataIndex: 'startTime'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.EndTime,
                        dataIndex: 'endTime'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.StartValue,
                        dataIndex: 'startValue'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.EndValue,
                        dataIndex: 'endValue'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.AlmComment,
                        dataIndex: 'almComment'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.NormalComment,
                        dataIndex: 'normalComment'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Frequency,
                        dataIndex: 'frequency'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.EndType,
                        dataIndex: 'endType'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Project,
                        dataIndex: 'project',
                        renderer: function (value, p, record) {
                            return Ext.String.format('<a href="javascript:void(0)" style="color:#157fcc;">{0}</a>', value);
                        }
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.ConfirmedStatus,
                        dataIndex: 'confirmedStatus'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.ConfirmedTime,
                        dataIndex: 'confirmedTime'
                    },
                    {
                        text: $$iPems.lang.Report400203.Columns.Confirmer,
                        dataIndex: 'confirmer'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: $$iPems.lang.Report400203.ConditionTitle,
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
                                fieldLabel: $$iPems.lang.Report400203.ToolBar.Range,
                                emptyText: $$iPems.lang.AllEmptyText,
                                labelWidth: 60,
                                width: 220,
                            },
                            {
                                id: 'startField',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Report400203.ToolBar.Start,
                                labelWidth: 60,
                                width: 220,
                                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Report400203.ToolBar.End,
                                labelWidth: 60,
                                width: 220,
                                value: new Date(),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                xtype: 'splitbutton',
                                glyph: 0xf005,
                                text: $$iPems.lang.Ok,
                                handler: function (me, event) {
                                    query(currentPagingToolbar);
                                },
                                menu: [{
                                    text: $$iPems.lang.Import,
                                    glyph: 0xf010,
                                    handler: function (me, event) {
                                        print(currentStore);
                                    }
                                }]
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
                                id: 'other-option-button',
                                xtype: 'button',
                                text: $$iPems.lang.Report400203.ToolBar.OtherOption,
                                menu: [
                                    {
                                        id: 'show-confirm-menu',
                                        xtype: 'menucheckitem',
                                        text: $$iPems.lang.Report400203.ToolBar.ShowConfirm,
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
                                        text: $$iPems.lang.Report400203.ToolBar.ShowUnConfirm,
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
                                        text: $$iPems.lang.Report400203.ToolBar.ShowProject,
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
                                        text: $$iPems.lang.Report400203.ToolBar.ShowUnProject,
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
                                fieldLabel: $$iPems.lang.Report400203.ToolBar.PointName,
                                emptyText: $$iPems.lang.MultiConditionEmptyText,
                                labelWidth: 60,
                                width: 220
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