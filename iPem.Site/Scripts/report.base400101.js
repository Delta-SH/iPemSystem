(function () {
    Ext.define('ReportModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'type', type: 'string' },
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
                minWidth: 120,
                minHeight: 60,
                renderer: function (storeItem, item) {
                    var total = 0;
                    chartPie.store.each(function (rec) {
                        total += rec.get('value');
                    });

                    this.update(
                        Ext.String.format('{0}: {1}<br/>{2}: {3}<br/>{4}: {5}%',
                        $$iPems.lang.Report400101.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400101.Chart.PieRate,
                        (storeItem.get('value') / total * 100).toFixed(2))
                    );
                }
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment'],
            data: [{ name: 'NoData', value: 1, comment: '' }]
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
            highlightCfg:{
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
                        $$iPems.lang.Report400101.Chart.PieTotal,
                        total,
                        storeItem.get('name'),
                        storeItem.get('value'),
                        $$iPems.lang.Report400101.Chart.PieRate,
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
        //禁用自动排序，使用后台的排序方式
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Report/RequestBase400101',
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
                    if (data && data.chart) {
                        chartPie.getStore().loadData(data.chart, false);
                        chartColumn.getStore().loadData(data.chart, false);
                    }
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
            dockedItems: [
                {
                    xtype: 'panel',
                    glyph: 0xf034,
                    title: $$iPems.lang.Report400101.ConditionTitle,
                    bodyCls: 'x-docked-top-with-bottom',
                    collapsible: true,
                    collapsed: false,
                    dock: 'top',
                    items: [{
                        xtype: 'toolbar',
                        border: false,
                        items: [{
                            id: 'areasfield',
                            xtype: 'area.treepanel',
                            emptyText: $$iPems.lang.AllEmptyText
                        }, {
                            id: 'typesfield',
                            xtype: 'area.type.multicombo',
                            emptyText: $$iPems.lang.AllEmptyText
                        }, {
                            xtype: 'button',
                            text: $$iPems.lang.Query,
                            glyph: 0xf005,
                            handler: function (el, e) {
                                var areasfield = Ext.getCmp('areasfield');
                                var typesfield = Ext.getCmp('typesfield');
                                if (!Ext.isEmpty(areasfield)) {
                                    var proxy = currentStore.getProxy();
                                    proxy.extraParams.parent = areasfield.getValue();
                                    proxy.extraParams.types = typesfield.getValue();
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
                                    url: '/Report/DownloadBase400101',
                                    params: {
                                        parent: params.parent,
                                        onlyroot: params.types
                                    }
                                });
                            }
                        }]
                    }]
                }
            ],
            items: [
                {
                    xtype: 'panel',
                    glyph: 0xf030,
                    title: $$iPems.lang.Report400101.ChartTitle,
                    collapsible: true,
                    collapseFirst: false,
                    margin: '5 0 0 0',
                    flex: 1,
                    layout: {
                        type: 'hbox',
                        align: 'stretch',
                        pack: 'start'
                    },
                    items: [chartPie, {xtype: 'component',width: 20}, chartColumn]
                }, {
                    xtype: 'gridpanel',
                    glyph: 0xf029,
                    flex: 2,
                    margin: '5 0 0 0',
                    collapsible: true,
                    collapseFirst: false,
                    title: $$iPems.lang.Report400101.GridTitle,
                    store: currentStore,
                    columnLines: true,
                    disableSelection: false,
                    loadMask: true,
                    forceFit: false,
                    viewConfig: {
                        forceFit: true,
                        trackOver: true,
                        stripeRows: true,
                        preserveScrollOnRefresh: true
                    },
                    features: [{
                        ftype: 'grouping',
                        groupHeaderTpl: $$iPems.lang.Report400101.GroupTpl,
                        hideGroupedHeader: false,
                        startCollapsed: true
                    }],
                    columns: [{
                        text: $$iPems.lang.Report400101.Columns.Index,
                        dataIndex: 'index',
                        width: 80,
                        align: 'left',
                        sortable: true
                    }, {
                        text: $$iPems.lang.Report400101.Columns.Id,
                        dataIndex: 'id',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }, {
                        text: $$iPems.lang.Report400101.Columns.Name,
                        dataIndex: 'name',
                        width: 120,
                        align: 'left',
                        sortable: true
                    }, {
                        text: $$iPems.lang.Report400101.Columns.Type,
                        dataIndex: 'type',
                        align: 'left',
                        width: 120,
                        sortable: true
                    }, {
                        text: $$iPems.lang.Report400101.Columns.Comment,
                        dataIndex: 'comment',
                        align: 'left',
                        width: 120,
                        sortable: true
                    },  {
                        text: $$iPems.lang.Report400101.Columns.Enabled,
                        dataIndex: 'enabled',
                        align: 'center',
                        width: 120,
                        sortable: true,
                        renderer: function (value) {
                            return value ? $$iPems.lang.StatusTrue : $$iPems.lang.StatusFalse;
                        }
                    }],
                    bbar: currentPagingToolbar
                }
            ]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });
})();