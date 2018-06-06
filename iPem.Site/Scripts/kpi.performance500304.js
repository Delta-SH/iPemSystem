var now = new Date(),
    lineFields = [],
    lineChart = null,
    lineOption = {
        tooltip: {
            trigger: 'item',
            axisPointer: {
                type: 'cross'
            }
        },
        grid: {
            top: 15,
            left: 0,
            right: 5,
            bottom: 0,
            containLabel: true
        },
        xAxis: [{
            type: 'category',
            data: ['无数据'],
            splitLine: { show: false }
        }],
        yAxis: [{
            type: 'value',
            name: 'PUE'
        }],
        series: [{
            name: 'PUE',
            type: 'line',
            smooth: true,
            symbolSize: 1,
            data: [0]
        }]
    };

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    fields: [],
    proxy: {
        type: 'ajax',
        url: '/KPI/Request500304',
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
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

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
        title: 'PUE曲线',
        collapsible: true,
        collapseFirst: false,
        margin: '5 0 0 0',
        flex: 1,
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
        id: 'energy-grid',
        xtype: 'grid',
        glyph: 0xf029,
        title: 'PUE信息',
        collapsible: true,
        collapseFirst: false,
        margin: '5 0 0 0',
        flex: 1,
        store: currentStore,
        selType: 'checkboxmodel',
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [],
        bbar: currentPagingToolbar,
        listeners: {
            selectionchange: function (me, records) {
                dochart(records);
            }
        }
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
                xtype: 'StationPicker',
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
                    query();
                }
            }]
        }, {
            xtype: 'toolbar',
            border: false,
            items: [
            {
                id: 'station-type-multicombo',
                xtype: 'StationTypeMultiCombo',
                width: 280,
                emptyText: '默认全部'
            },
            {
                id: 'periodCombo',
                xtype: 'PeriodCombo',
                year: false,
                week: false,
                width: 280
            }, {
                id: 'exportButton',
                xtype: 'button',
                glyph: 0xf010,
                text: '数据导出',
                disabled: true,
                handler: function (me, event) {
                    print();
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
                value: Ext.Date.add(new Date(now.getFullYear(), now.getMonth(), 1), Ext.Date.MONTH, -1),
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

var query = function () {
    var range = Ext.getCmp('rangePicker'),
        types = Ext.getCmp('station-type-multicombo'),
        period = Ext.getCmp('periodCombo'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField'),
        grid = Ext.getCmp('energy-grid'),
        view = grid.getView();

    if (!range.isValid()) return;
    if (!start.isValid()) return;
    if (!end.isValid()) return;

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.parent = range.getValue();
    proxy.extraParams.types = types.getSelectedValues();
    proxy.extraParams.period = period.getValue();
    proxy.extraParams.startDate = start.getRawValue();
    proxy.extraParams.endDate = end.getRawValue();
    proxy.extraParams.cache = false;

    lineFields = [];
    Ext.Ajax.request({
        url: '/KPI/GetFields500304',
        params: proxy.extraParams,
        mask: new Ext.LoadMask({ target: view, msg: '获取列名...' }),
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                me.model.prototype.fields.clear();
                me.removeAll();
                var fields = [];
                if (data.data && Ext.isArray(data.data)) {
                    Ext.Array.each(data.data, function (item, index) {
                        me.model.prototype.fields.replace({ name: item.name, type: item.type });
                        lineFields.push(item.name);
                        if (!Ext.isEmpty(item.column)) {
                            fields.push({
                                text: item.column,
                                dataIndex: item.name,
                                width: item.width,
                                align: item.align
                            });
                        }
                    });
                }

                grid.reconfigure(me, fields);
                me.loadPage(1, {
                    callback: function (records, operation, success) {
                        proxy.extraParams.cache = success;
                        Ext.getCmp('exportButton').setDisabled(success === false);
                    }
                });
            } else {
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/KPI/Download500304',
        params: currentStore.getProxy().extraParams
    });
};

var dochart = function (data) {
    if (lineChart) {
        var xaxis = [], series = [];
        if (!Ext.isEmpty(data) && Ext.isArray(data) && data.length > 0) {
            Ext.Array.each(lineFields, function (field) {
                if (field === "index" || field === "name")
                    return true;

                xaxis.push(field);
            });

            Ext.Array.each(data, function (item) {
                var values = [];
                Ext.Array.each(lineFields, function (field) {
                    if (field === "index" || field === "name")
                        return true;

                    values.push(item.get(field));
                });

                series.push({
                    name: item.get('name'),
                    type: 'line',
                    smooth: true,
                    symbolSize: 1,
                    data: values
                });
            });
        }

        if (xaxis.length == 0) {
            xaxis.push('无数据');
        }

        if (series.length == 0) {
            series.push({
                name: 'PUE',
                type: 'line',
                smooth: true,
                symbolSize: 1,
                data: [0]
            });
        }

        lineOption.xAxis[0].data = xaxis;
        lineOption.series = series;
        lineChart.setOption(lineOption, true);
    }
}

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentLayout);
    }
});

Ext.onReady(function () {
    lineChart = echarts.init(document.getElementById("line-chart"), 'shine');

    //init charts
    lineChart.setOption(lineOption);
});