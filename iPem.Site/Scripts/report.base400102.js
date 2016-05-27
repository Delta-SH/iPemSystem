(function () {
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
                        $$iPems.lang.Report400102.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400102.Chart.PieRate,
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
                        $$iPems.lang.Report400102.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400102.Chart.PieRate,
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
                title: $$iPems.lang.Report400102.ConditionTitle,
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
                        emptyText: $$iPems.lang.AllEmptyText
                    }, {
                        id: 'typesfield',
                        xtype: 'StationTypeMultiCombo',
                        emptyText: $$iPems.lang.AllEmptyText
                    }, {
                        xtype: 'button',
                        text: $$iPems.lang.Query,
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
                        text: $$iPems.lang.Import,
                        glyph: 0xf010,
                        handler: function (el, e) {
                            var params = currentStore.getProxy().extraParams;
                            $$iPems.download({
                                url: '/Report/DownloadBase400102',
                                params: {
                                    parent: params.parent,
                                    types: params.types
                                }
                            });
                        }
                    }]
                }]
            }],
            items: [{
                xtype: 'panel',
                glyph: 0xf030,
                title: $$iPems.lang.Report400102.ChartTitle,
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
                title: $$iPems.lang.Report400102.GridTitle,
                store: currentStore,
                columnLines: true,
                disableSelection: false,
                loadMask: true,
                forceFit: false,
                viewConfig: {
                    forceFit: true,
                    trackOver: true,
                    stripeRows: true,
                    emptyText: $$iPems.lang.GridEmptyText,
                    preserveScrollOnRefresh: true
                },
                features: [{
                    ftype: 'grouping',
                    groupHeaderTpl: $$iPems.lang.Report400102.GroupTpl,
                    hideGroupedHeader: false,
                    startCollapsed: true
                }],
                columns: [{
                    text: $$iPems.lang.Report400102.Columns.Index,
                    dataIndex: 'index',
                    width: 60,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Id,
                    dataIndex: 'id',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Name,
                    dataIndex: 'name',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Type,
                    dataIndex: 'type',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Longitude,
                    dataIndex: 'longitude',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Latitude,
                    dataIndex: 'latitude',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Altitude,
                    dataIndex: 'altitude',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.CityElecLoadType,
                    dataIndex: 'cityelecloadtype',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.CityElecCap,
                    dataIndex: 'cityeleccap',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.CityElecLoad,
                    dataIndex: 'cityelecload',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Contact,
                    dataIndex: 'contact',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.LineRadiusSize,
                    dataIndex: 'lineradiussize',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.LineLength,
                    dataIndex: 'linelength',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.SuppPowerType,
                    dataIndex: 'supppowertype',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.TranInfo,
                    dataIndex: 'traninfo',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.TranContNo,
                    dataIndex: 'trancontno',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.TranPhone,
                    dataIndex: 'tranphone',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Comment,
                    dataIndex: 'comment',
                    width: 100,
                    align: 'left',
                    sortable: true
                }, {
                    text: $$iPems.lang.Report400102.Columns.Enabled,
                    dataIndex: 'enabled',
                    align: 'center',
                    width: 100,
                    sortable: true,
                    renderer: function (value) {
                        return value ? $$iPems.lang.StatusTrue : $$iPems.lang.StatusFalse;
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