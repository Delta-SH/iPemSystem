(function () {
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
                    boxStroke: '#157fcc',
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
                                '告警总量: ' + total + '<br/>'
                                + storeItem.get('name') + ': ' + storeItem.get('value') + '<br/>'
                                + '告警占比: ' + Math.round(storeItem.get('value') / total * 100) + '%'
                                );
                        }
                    }
                }]
            });
    };

    var almDevTypeCombo = Ext.create('Ext.ux.MultiCombo', {
        fieldLabel: '告警设备',
        valueField: 'id',
        displayField: 'text',
        delimiter: $$iPems.Delimiter,
        queryMode: 'local',
        triggerAction: 'all',
        selectionMode: 'all',
        emptyText: $$iPems.lang.AllEmptyText,
        forceSelection: true,
        labelWidth: 60,
        width: 220,
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '../Account/GetComboEventLevels',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        })
    });

    var almLevelCombo = Ext.create('Ext.ux.MultiCombo', {
        fieldLabel: '告警级别',
        valueField: 'id',
        displayField: 'text',
        delimiter: $$iPems.Delimiter,
        queryMode: 'local',
        triggerAction: 'all',
        selectionMode: 'all',
        emptyText: $$iPems.lang.AllEmptyText,
        forceSelection: true,
        labelWidth: 60,
        width: 220,
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '../Account/GetComboEventLevels',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        })
    });

    var almLogicTypeCombo = Ext.create('Ext.form.ComboBox', {
        fieldLabel: '逻辑分类',
        displayField: 'text',
        valueField: 'id',
        typeAhead: true,
        queryMode: 'local',
        triggerAction: 'all',
        emptyText: $$iPems.lang.AllEmptyText,
        selectOnFocus: true,
        forceSelection: true,
        labelWidth: 60,
        width: 220,
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '../Account/GetComboRoles',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store, records, successful) {
                    if (successful
                        && !Ext.isEmpty(records)
                        && records.length > 0)
                        Ext.getCmp('roleId').select(records[0]);
                }
            }
        })
    });

    var almNameCombo = Ext.create('Ext.ux.MultiCombo', {
        fieldLabel: '信号名称',
        valueField: 'id',
        displayField: 'text',
        delimiter: $$iPems.Delimiter,
        queryMode: 'local',
        triggerAction: 'all',
        selectionMode: 'all',
        emptyText: $$iPems.lang.AllEmptyText,
        forceSelection: true,
        labelWidth: 60,
        width: 220,
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '../Account/GetComboEventLevels',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        })
    });

    var almConfirmCombo = Ext.create('Ext.ux.MultiCombo', {
        fieldLabel: '告警确认',
        valueField: 'id',
        displayField: 'text',
        delimiter: $$iPems.Delimiter,
        queryMode: 'local',
        triggerAction: 'all',
        selectionMode: 'all',
        emptyText: $$iPems.lang.AllEmptyText,
        forceSelection: true,
        labelWidth: 60,
        width: 220,
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '../Account/GetComboEventLevels',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        })
    });

    var almProjectCombo = Ext.create('Ext.ux.MultiCombo', {
        fieldLabel: '工程标识',
        valueField: 'id',
        displayField: 'text',
        delimiter: $$iPems.Delimiter,
        queryMode: 'local',
        triggerAction: 'all',
        selectionMode: 'all',
        emptyText: $$iPems.lang.AllEmptyText,
        forceSelection: true,
        labelWidth: 60,
        width: 220,
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            pageSize: 1024,
            fields: [
                { name: 'id', type: 'string' },
                { name: 'text', type: 'string' },
                { name: 'comment', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '../Account/GetComboEventLevels',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        })
    });

    var chartStore1 = Ext.create('Ext.data.Store', {
        fields: ['name', 'value'],
        data: [
            { name: '一级告警', value: 50 },
            { name: '二级告警', value: 100 },
            { name: '三级告警', value: 200 },
            { name: '四级告警', value: 400 }
        ]
    });

    var chartStore2 = Ext.create('Ext.data.Store', {
        fields: ['name', 'value'],
        data: [
            { name: '开关电源', value: 96 },
            { name: '蓄电池组', value: 28 },
            { name: 'UPS', value: 105 },
            { name: '环境设备', value: 82 },
            { name: '其它设备', value: 88 },
        ]
    });

    var chartStore3 = Ext.create('Ext.data.Store', {
        fields: ['name', 'value'],
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
    
    var currentLayout = Ext.create('Ext.panel.Panel', {
        region: 'center',
        layout: 'border',
        border: false,
        items: [{
            id: 'alarm-organization',
            region: 'west',
            xtype: 'treepanel',
            title: '告警设备列表',
            glyph: 0xf011,
            width: 220,
            split: true,
            collapsible: true,
            collapsed: false,
            autoScroll: true,
            useArrows: false,
            rootVisible: false,
            root: {
                id: 'root',
                text: 'Root',
                expanded: true,
                attributes: [
                    { key: 'id', value: 'root' },
                    { key: 'type', value: $$iPems.Organization.Area }
                ]
            },
            store: Ext.create('Ext.data.TreeStore', {
                autoLoad: false,
                nodeParam : 'node',
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

                            var type = Ext.Array.findBy(attributes, function (item,index) {
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
                        change: function(me, newValue, oldValue, eOpts ){
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
                title: '告警分类占比',
                collapsible: true,
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
                title: '告警详细信息',
                collapsible: true,
                layout: 'fit',
                margin: '5 0 0 0',
                flex: 2,
                items: [{
                    xtype: 'grid',
                    selType: 'checkboxmodel',
                    border: false,
                    columns: [
                        { text: 'Name', dataIndex: 'name' },
                        { text: 'Email', dataIndex: 'email', flex: 1 },
                        { text: 'Phone', dataIndex: 'phone' }
                    ]
                }]
            }],
            dockedItems: [{
                xtype: 'panel',
                glyph: 0xf034,
                title: '告警筛选条件',
                collapsible: true,
                collapsed: false,
                dock: 'top',
                items: [
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [almDevTypeCombo, almLogicTypeCombo, almNameCombo]
                    },
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [almLevelCombo, almConfirmCombo, almProjectCombo, {
                            id: 'alarm-filter-button', xtype: 'splitbutton', glyph: 0xf005, text: '确定', menu: [{text: '告警确认'},'-', {text: '数据导出'}]
                        }]
                    }
                ]
            }]
        }]
    });

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
            
            //load store data
        }
    });
})();