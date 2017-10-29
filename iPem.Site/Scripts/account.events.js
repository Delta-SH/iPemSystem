(function () {
    Ext.define('EventModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'level', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'shortMessage', type: 'string' },
            //{ name: 'fullMessage', type: 'string' },
            { name: 'ip', type: 'string' },
            { name: 'page', type: 'string' },
            { name: 'referrer', type: 'string' },
            { name: 'user', type: 'string' },
            { name: 'created', type: 'string' },
        ],
        idProperty: 'id'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'EventModel',
        proxy: {
            type: 'ajax',
            url: '/Account/GetEvents',
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
            extraParams: {
                levels: [],
                types: [],
                startDate: '',
                endDate: ''
            },
            simpleSortMode: true
        }
    });

    var comboLevelStore =  Ext.create('Ext.data.Store',{
        autoLoad: false,
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Account/GetComboEventLevels',
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
            }
        }
    });

    var comboTypeStore = Ext.create('Ext.data.Store',{
        autoLoad: false,
        pageSize: 1024,
        fields: [
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'comment', type: 'string' }
        ],
        proxy: {
            type: 'ajax',
            url: '/Account/GetComboEventTypes',
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
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var currentGridPanel = Ext.create('Ext.grid.Panel', {
        glyph:0xf029,
        title: '日志信息',
        region: 'center',
        store: currentStore,
        columnLines: true,
        disableSelection: false,
        loadMask: true,
        forceFit: false,
        viewConfig: {
            forceFit: true,
            trackOver: true,
            stripeRows: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
            preserveScrollOnRefresh: true
        },
        columns: [{
            text: '序号',
            dataIndex: 'index',
            width: 60,
            align: 'left',
            sortable: true
        }, {
            text: '级别',
            dataIndex: 'level',
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
            text: '日志摘要',
            dataIndex: 'shortMessage',
            width: 100,
            align: 'left',
            flex: 1,
            sortable: true,
            renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
                metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
                return value;
            }
        },
        //{
        //    text: '详细信息',
        //    dataIndex: 'fullMessage',
        //    flex: 1,
        //    minWidth:100,
        //    align: 'left',
        //    sortable: true,
        //    renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
        //        metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
        //        return value;
        //    }
        //},
        {
            text: '客户端IP',
            dataIndex: 'ip',
            width: 100,
            align: 'left',
            sortable: true
        }, {
            text: '请求URL',
            dataIndex: 'page',
            width: 100,
            align: 'left',
            sortable: true
        }, {
            text: '关联URL',
            dataIndex: 'referrer',
            width: 100,
            align: 'left',
            sortable: true
        }, {
            text: '用户名称',
            dataIndex: 'user',
            align: 'left',
            width: 100,
            sortable: true
        }, {
            text: '记录时间',
            dataIndex: 'created',
            align: 'left',
            width: 100,
            sortable: true
        }],
        dockedItems: [{
            xtype: 'panel',
            dock: 'top',
            items: [
                Ext.create('Ext.toolbar.Toolbar', {
                    border: false,
                    items: [Ext.create('Ext.ux.MultiCombo', {
                        id: 'levels-multicombo',
                        fieldLabel: '日志级别',
                        valueField: 'id',
                        displayField: 'text',
                        delimiter: $$iPems.Delimiter,
                        queryMode: 'local',
                        triggerAction: 'all',
                        selectionMode: 'all',
                        emptyText: '默认全部',
                        forceSelection: true,
                        labelWidth: 60,
                        width: 250,
                        store: comboLevelStore
                    }), Ext.create('Ext.ux.MultiCombo', {
                        id: 'types-multicombo',
                        fieldLabel: '日志类型',
                        valueField: 'id',
                        displayField: 'text',
                        delimiter: $$iPems.Delimiter,
                        queryMode: 'local',
                        triggerAction: 'all',
                        selectionMode: 'all',
                        emptyText: '默认全部',
                        forceSelection: true,
                        labelWidth: 60,
                        width: 250,
                        store: comboTypeStore
                    }), {
                        id: 'query',
                        xtype: 'button',
                        glyph: 0xf005,
                        text: '数据查询',
                        handler: function (el, e) {
                            query();
                        }
                    }, '-', {
                        id: 'exportButton',
                        xtype: 'button',
                        glyph: 0xf010,
                        text: '数据导出',
                        disabled: true,
                        handler: function (el, e) {
                            print();
                        }
                    }]
                }),
                Ext.create('Ext.toolbar.Toolbar', {
                    border: false,
                    items: [{
                        id: 'begin-datefield',
                        xtype: 'datefield',
                        fieldLabel: '开始日期',
                        labelWidth: 60,
                        width: 250,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    }, {
                        id: 'end-datefield',
                        xtype: 'datefield',
                        fieldLabel: '结束日期',
                        labelWidth: 60,
                        width: 250,
                        value: new Date(),
                        editable: false,
                        allowBlank: false
                    }, {
                        xtype: 'button',
                        glyph: 0xf023,
                        text: '清理日志',
                        handler: function (el, e) {
                            Ext.Msg.confirm('确认对话框', '您确定要删除三个月之前的日志信息吗？', function (buttonId, text) {
                                if (buttonId === 'yes') {
                                    Ext.Ajax.request({
                                        url: '/Account/ClearEvents',
                                        mask: new Ext.LoadMask(currentGridPanel, { msg: '正在处理...' }),
                                        success: function (response, options) {
                                            var data = Ext.decode(response.responseText, true);
                                            if (data.success)
                                                Ext.Msg.show({ title: '系统提示', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                            else
                                                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                        }
                                    });
                                }
                            });
                        }
                    }]
                })
            ]
        }],
        bbar: currentPagingToolbar
    });

    var query = function () {
        var levels = Ext.getCmp('levels-multicombo').getSelectedValues(),
            types = Ext.getCmp('types-multicombo').getSelectedValues(),
            startDate = Ext.getCmp('begin-datefield').getRawValue(),
            endDate = Ext.getCmp('end-datefield').getRawValue();

        var me = currentStore, proxy = me.getProxy();
        proxy.extraParams.levels = levels;
        proxy.extraParams.types = types;
        proxy.extraParams.startDate = startDate;
        proxy.extraParams.endDate = endDate;
        proxy.extraParams.cache = false;
        me.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    };

    var print = function () {
        $$iPems.download({
            url: '/Account/DownloadEvents',
            params: currentStore.getProxy().extraParams
        });
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentGridPanel);

            //load store data
            comboLevelStore.load();
            comboTypeStore.load();
        }
    });
})();