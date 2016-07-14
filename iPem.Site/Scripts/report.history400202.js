﻿(function () {
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
            url: '/Report/DownloadHistory400202',
            params: store.proxy.extraParams
        });
    };

    var projectWnd = Ext.create('Ext.window.Window', {
        title: $$iPems.lang.Report400202.Window.Project.Title,
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
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.Id
        }, {
            itemId: 'startTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.StartTime
        }, {
            itemId: 'endTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.EndTime
        }, {
            itemId: 'projectName',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.ProjectName
        }, {
            itemId: 'creator',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.Creator
        }, {
            itemId: 'createdTime',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.CreatedTime
        }, {
            itemId: 'comment',
            labelWidth: 60,
            fieldLabel: $$iPems.lang.Report400202.Window.Project.Fields.Comment
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
                        $$iPems.lang.Report400202.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400202.Chart.PieRate,
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
                        $$iPems.lang.Report400202.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400202.Chart.PieRate,
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
                        $$iPems.lang.Report400202.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400202.Chart.PieRate,
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
            url: '/Report/RequestHistory400202',
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
                title: $$iPems.lang.Report400202.RateTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                tools: [
                    //{
                    //    type: 'print',
                    //    tooltip: $$iPems.lang.Import,
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
                title: $$iPems.lang.Report400202.DetailTitle,
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 2,
                store: currentStore,
                loadMask: true,
                tools:[{
                    type: 'print',
                    tooltip: $$iPems.lang.Import,
                    handler: function(event, toolEl, panelHeader) {
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
                        text: $$iPems.lang.Report400202.Columns.Id,
                        dataIndex: 'index',
                        width: 60
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Area,
                        dataIndex: 'area'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Station,
                        dataIndex: 'station'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Room,
                        dataIndex: 'room'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.DevType,
                        dataIndex: 'devType'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Device,
                        dataIndex: 'device'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Logic,
                        dataIndex: 'logic'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Point,
                        dataIndex: 'point'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Level,
                        dataIndex: 'levelDisplay'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.StartTime,
                        dataIndex: 'startTime'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.EndTime,
                        dataIndex: 'endTime'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.StartValue,
                        dataIndex: 'startValue'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.EndValue,
                        dataIndex: 'endValue'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.AlmComment,
                        dataIndex: 'almComment'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.NormalComment,
                        dataIndex: 'normalComment'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Frequency,
                        dataIndex: 'frequency'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.EndType,
                        dataIndex: 'endType'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Project,
                        dataIndex: 'project',
                        renderer: function (value, p, record) {
                            return Ext.String.format('<a href="javascript:void(0)" style="color:#157fcc;">{0}</a>', value);
                        }
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.ConfirmedStatus,
                        dataIndex: 'confirmedStatus'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.ConfirmedTime,
                        dataIndex: 'confirmedTime'
                    },
                    {
                        text: $$iPems.lang.Report400202.Columns.Confirmer,
                        dataIndex: 'confirmer'
                    }
                ],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: $$iPems.lang.Report400202.ConditionTitle,
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
                                fieldLabel: $$iPems.lang.Report400202.ToolBar.Range,
                                emptyText: $$iPems.lang.AllEmptyText,
                                labelWidth: 60,
                                width: 220,
                            },
                            {
                                id: 'startField',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Report400202.ToolBar.Start,
                                labelWidth: 60,
                                width: 220,
                                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'endField',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Report400202.ToolBar.End,
                                labelWidth: 60,
                                width: 220,
                                value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                xtype: 'button',
                                glyph: 0xf005,
                                text: $$iPems.lang.Query,
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
                                xtype: 'button',
                                glyph: 0xf010,
                                text: $$iPems.lang.Import,
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
                                fieldLabel: $$iPems.lang.Report400202.ToolBar.PointName,
                                emptyText: $$iPems.lang.MultiConditionEmptyText,
                                labelWidth: 60,
                                width: 220
                            },
                            {
                                id: 'other-option-button',
                                xtype: 'button',
                                text: $$iPems.lang.Report400202.ToolBar.OtherOption,
                                menu: [
                                    {
                                        id: 'show-confirm-menu',
                                        xtype: 'menucheckitem',
                                        text: $$iPems.lang.Report400202.ToolBar.ShowConfirm,
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
                                        text: $$iPems.lang.Report400202.ToolBar.ShowUnConfirm,
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
                                        text: $$iPems.lang.Report400202.ToolBar.ShowProject,
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
                                        text: $$iPems.lang.Report400202.ToolBar.ShowUnProject,
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