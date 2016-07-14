﻿(function () {
    Ext.define('ReportModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'longitude', type: 'string' },
            { name: 'latitude', type: 'string' },
            { name: 'altitude', type: 'string' },
            { name: 'cityelecloadtype', type: 'string' },
            { name: 'cityeleccap', type: 'string' },
            { name: 'cityelecload', type: 'string' },
            { name: 'contact', type: 'string' },
            { name: 'lineradiussize', type: 'string' },
            { name: 'linelength', type: 'string' },
            { name: 'supppowertype', type: 'string' },
            { name: 'traninfo', type: 'string' },
            { name: 'trancontno', type: 'string' },
            { name: 'tranphone', type: 'string' },
            { name: 'comment', type: 'string' },
            { name: 'enabled', type: 'boolean' }
        ],
        idProperty: 'id'
    });

    var print = function (store) {
        $$iPems.download({
            url: '/Report/DownloadBase400102',
            params: store.proxy.extraParams
        });
    };

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
                        '站点总量',
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        '类型占比',
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
                        '站点总量',
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        '类型占比',
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
        model: 'ReportModel',
        groupField: 'type',
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Report/RequestBase400102',
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
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '站点统计条件',
                bodyCls: 'x-docked-top-with-bottom',
                collapsible: true,
                collapsed: false,
                dock: 'top',
                items: [{
                    xtype: 'toolbar',
                    border: false,
                    items: [{
                        id: 'areasfield',
                        xtype: 'AreaPicker',
                        emptyText: '默认全部'
                    }, {
                        id: 'typesfield',
                        xtype: 'StationTypeMultiCombo',
                        emptyText: '默认全部'
                    }, {
                        xtype: 'button',
                        text: '数据查询',
                        glyph: 0xf005,
                        handler: function (el, e) {
                            var areasfield = Ext.getCmp('areasfield'),
                                typesfield = Ext.getCmp('typesfield'),
                                parent = areasfield.getValue(),
                                types = typesfield.getValue();

                            if (!Ext.isEmpty(parent)) {
                                var proxy = currentStore.getProxy();
                                proxy.extraParams.parent = parent;
                                proxy.extraParams.types = types;
                                currentStore.loadPage(1);
                            }
                        }
                    }, '-', {
                        xtype: 'button',
                        text: '数据导出',
                        glyph: 0xf010,
                        handler: function (el, e) {
                            print(currentStore);
                        }
                    }]
                }]
            }],
            items: [{
                xtype: 'panel',
                glyph: 0xf030,
                title: '站点类型分类占比',
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
                xtype: 'gridpanel',
                glyph: 0xf029,
                flex: 2,
                margin: '5 0 0 0',
                collapsible: true,
                collapseFirst: false,
                title: '站点统计列表',
                store: currentStore,
                columnLines: true,
                disableSelection: false,
                loadMask: true,
                forceFit: false,
                tools: [{
                    type: 'print',
                    tooltip: '数据导出',
                    handler: function (event, toolEl, panelHeader) {
                        print(currentStore);
                    }
                }],
                viewConfig: {
                    forceFit: true,
                    trackOver: true,
                    stripeRows: true,
                    emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                    preserveScrollOnRefresh: true
                },
                features: [{
                    ftype: 'grouping',
                    groupHeaderTpl: '{columnName}: {name} ({rows.length}条)',
                    hideGroupedHeader: false,
                    startCollapsed: true
                }],
                columns: [{
                    text: '序号',
                    dataIndex: 'index',
                    width: 60,
                    align: 'left',
                    sortable: true
                }, {
                    text: '编号',
                    dataIndex: 'id',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '名称',
                    dataIndex: 'name',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '类型',
                    dataIndex: 'type',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '经度',
                    dataIndex: 'longitude',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '纬度',
                    dataIndex: 'latitude',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '海拔标高',
                    dataIndex: 'altitude',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '市电引入方式',
                    dataIndex: 'cityelecloadtype',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '市电容',
                    dataIndex: 'cityeleccap',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '市电引入',
                    dataIndex: 'cityelecload',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '维护责任人',
                    dataIndex: 'contact',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '线径',
                    dataIndex: 'lineradiussize',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '线缆长度',
                    dataIndex: 'linelength',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '供电性质',
                    dataIndex: 'supppowertype',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '转供信息',
                    dataIndex: 'traninfo',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '供电合同号',
                    dataIndex: 'trancontno',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '变电站电话',
                    dataIndex: 'tranphone',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '描述',
                    dataIndex: 'comment',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: '状态',
                    dataIndex: 'enabled',
                    align: 'center',
                    width: 100,
                    sortable: true,
                    renderer: function (value) {
                        return value ? '有效' : '禁用';
                    }
                }],
                bbar: currentPagingToolbar
            }]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
            currentStore.loadPage(1);
        }
    });
})();