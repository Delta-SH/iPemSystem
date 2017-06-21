(function () {
    var now = new Date(),
        lineChart = null,
        lineOption = {
            tooltip: {
                trigger: 'axis'
            },
            grid: {
                top: 15,
                left: 5,
                right: 5,
                bottom: 0,
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                data: ['无数据']
            }],
            yAxis: [{
                type: 'value',
                axisLabel: {
                    formatter: '{value} kW·h'
                }
            }],
            series: [{ name: '趋势', type: 'line', smooth: true, data: [0] }]
        };

    var query = function (store, grid) {
        var range = Ext.getCmp('rangePicker'),
            period = Ext.getCmp('periodCombo'),
            size = Ext.getCmp('sizeCombo'),
            start = Ext.getCmp('startField'),
            end = Ext.getCmp('endField');

        if (!range.isValid()) return;
        if (!start.isValid()) return;
        if (!end.isValid()) return;

        store.proxy.extraParams.parent = range.getValue();
        store.proxy.extraParams.period = period.getValue();
        store.proxy.extraParams.size = size.getValue();
        store.proxy.extraParams.startDate = start.getRawValue();
        store.proxy.extraParams.endDate = end.getRawValue();

        Ext.Ajax.request({
            url: '/KPI/GetFields500302',
            params: store.proxy.extraParams,
            mask: new Ext.LoadMask({ target: grid.getView(), msg: '获取列名...' }),
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    store.model.prototype.fields.clear();
                    store.removeAll();
                    var columns = [];
                    if (data.data && Ext.isArray(data.data)) {
                        Ext.Array.each(data.data, function (item, index) {
                            store.model.prototype.fields.replace({ name: item });
                            columns.push({ text: item, dataIndex: item, width: index > 0 ? 100 : 60 });
                        });
                    }

                    grid.reconfigure(store, columns);
                    store.loadPage(1);
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    var print = function (store) {
        $$iPems.download({
            url: '/KPI/Download500302',
            params: store.proxy.extraParams
        });
    };

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        fields: [],
        proxy: {
            type: 'ajax',
            url: '/KPI/Request500302',
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
                if (successful && lineChart) {
                    lineOption.xAxis[0].data = [];
                    lineOption.series = [];
                    Ext.Array.each(records, function (item, index) {
                        var row = item.data,
                            name = row['名称'],
                            names = name.split(',');


                        var series = { name: names.pop(), type: 'line', smooth: true, data: [] };
                        for (var p in row) {
                            if(p === '序号' || p === '名称') continue;

                            lineOption.xAxis[0].data.push(p);
                            series.data.push(row[p]);
                        }

                        lineOption.series.push(series);
                    });

                    if (lineOption.xAxis[0].data.length == 0) lineOption.xAxis[0].data.push('无数据');
                    if (lineOption.series.length == 0) lineOption.series.push({ name: '趋势', type: 'line', smooth: true, data: [0] });
                    lineChart.setOption(lineOption, true);
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
                glyph: 0xf031,
                title: '趋势曲线',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex:1,
                layout: {
                    type: 'hbox',
                    align: 'stretch',
                    pack: 'start'
                },
                items: [
                    {
                        xtype: 'container',
                        flex: 1,
                        contentEl: 'line-chart'
                    }
                ],
                listeners: {
                    resize: function (me, width, height, oldWidth, oldHeight) {
                        var lineContainer = Ext.get('line-chart');
                        lineContainer.setHeight(height - 40);
                        if (lineChart) lineChart.resize();
                    }
                }
            }, {
                id: 'detail-grid',
                xtype: 'grid',
                glyph: 0xf029,
                title: '能耗信息',
                collapsible: true,
                collapseFirst: false,
                margin: '5 0 0 0',
                flex: 1,
                store: currentStore,
                tools: [{
                    type: 'print',
                    tooltip: '数据导出',
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
                viewConfig: {
                    loadMask: true,
                    stripeRows: true,
                    trackOver: true,
                    emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
                },
                columns: [],
                bbar: currentPagingToolbar,
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '筛选条件',
                collapsible: true,
                collapsed: false,
                dock: 'top',
                items: [{
                    xtype: 'toolbar',
                    border: false,
                    items: [{
                        id: 'rangePicker',
                        xtype: 'AreaPicker',
                        selectAll: true,
                        allowBlank: false,
                        emptyText: '请选择查询范围...',
                        fieldLabel: '查询范围',
                        width: 568,
                    }, {
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (me, event) {
                            query(currentStore, Ext.getCmp('detail-grid'));
                        }
                    }]
                }, {
                    xtype: 'toolbar',
                    border: false,
                    items: [{
                        id: 'periodCombo',
                        xtype: 'combobox',
                        fieldLabel: '统计周期',
                        displayField: 'text',
                        valueField: 'id',
                        typeAhead: true,
                        queryMode: 'local',
                        triggerAction: 'all',
                        selectOnFocus: true,
                        forceSelection: true,
                        labelWidth: 60,
                        width: 280,
                        value: $$iPems.Period.Month,
                        store: Ext.create('Ext.data.Store', {
                            fields: ['id', 'text'],
                            data: [
                                { id: $$iPems.Period.Month, text: '按月统计' },
                                { id: $$iPems.Period.Week, text: '按周统计' },
                                { id: $$iPems.Period.Day, text: '按日统计' },
                            ]
                        })
                    }, {
                        id: 'sizeCombo',
                        xtype: 'combobox',
                        fieldLabel: '统计粒度',
                        displayField: 'text',
                        valueField: 'id',
                        typeAhead: true,
                        queryMode: 'local',
                        triggerAction: 'all',
                        selectOnFocus: true,
                        forceSelection: true,
                        labelWidth: 60,
                        width: 280,
                        value: $$iPems.SSH.Area,
                        store: Ext.create('Ext.data.Store', {
                            fields: ['id', 'text'],
                            data: [
                                { id: $$iPems.SSH.Area, text: '区域' },
                                { id: $$iPems.SSH.Station, text: '站点' },
                                { id: $$iPems.SSH.Room, text: '机房' },
                            ]
                        })
                    }, {
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        handler: function (me, event) {
                            print(currentStore);
                        }
                    }]
                }, {
                    xtype: 'toolbar',
                    border: false,
                    items: [{
                        id: 'startField',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.YEAR, -1),
                        editable: false,
                        allowBlank: false
                    }, {
                        id: 'endField',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 280,
                        value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.SECOND, -1),
                        editable: false,
                        allowBlank: false
                    }]
                }]
            }]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        lineChart = echarts.init(document.getElementById("line-chart"), 'shine');
        lineChart.setOption(lineOption);
    });
})();