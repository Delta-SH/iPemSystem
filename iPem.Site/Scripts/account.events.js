(function () {
    Ext.define('EventModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'level', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'shortMessage', type: 'string' },
            { name: 'fullMessage', type: 'string' },
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
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var currentGridPanel = Ext.create('Ext.grid.Panel', {
        glyph:0xf029,
        title: $$iPems.lang.Event.Title,
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
            emptyText: $$iPems.lang.GridEmptyText,
            preserveScrollOnRefresh: true
        },
        columns: [{
            text: $$iPems.lang.Event.Columns.Index,
            dataIndex: 'index',
            width: 60,
            align: 'left',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.Level,
            dataIndex: 'level',
            width: 100,
            align: 'center',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.Type,
            dataIndex: 'type',
            width: 100,
            align: 'center',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.ShortMessage,
            dataIndex: 'shortMessage',
            width: 100,
            align: 'left',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.FullMessage,
            dataIndex: 'fullMessage',
            flex: 1,
            minWidth:100,
            align: 'left',
            sortable: true,
            renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
                metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
                return value;
            }
        }, {
            text: $$iPems.lang.Event.Columns.IP,
            dataIndex: 'ip',
            width: 100,
            align: 'center',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.Page,
            dataIndex: 'page',
            width: 100,
            align: 'left',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.Referrer,
            dataIndex: 'referrer',
            width: 100,
            align: 'left',
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.User,
            dataIndex: 'user',
            align: 'left',
            width: 100,
            sortable: true
        }, {
            text: $$iPems.lang.Event.Columns.Created,
            dataIndex: 'created',
            align: 'center',
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
                        fieldLabel: $$iPems.lang.Event.ToolBar.Level,
                        valueField: 'id',
                        displayField: 'text',
                        delimiter: $$iPems.Delimiter,
                        queryMode: 'local',
                        triggerAction: 'all',
                        selectionMode: 'all',
                        emptyText: $$iPems.lang.AllEmptyText,
                        forceSelection: true,
                        labelWidth: 60,
                        width: 250,
                        store: comboLevelStore
                    }), Ext.create('Ext.ux.MultiCombo', {
                        id: 'types-multicombo',
                        fieldLabel: $$iPems.lang.Event.ToolBar.Type,
                        valueField: 'id',
                        displayField: 'text',
                        delimiter: $$iPems.Delimiter,
                        queryMode: 'local',
                        triggerAction: 'all',
                        selectionMode: 'all',
                        emptyText: $$iPems.lang.AllEmptyText,
                        forceSelection: true,
                        labelWidth: 60,
                        width: 250,
                        store: comboTypeStore
                    })]
                }),
                Ext.create('Ext.toolbar.Toolbar', {
                    border: false,
                    items: [{
                        id: 'begin-datefield',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Event.ToolBar.BeginTime,
                        labelWidth: 60,
                        width: 250,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                        editable: false,
                        allowBlank: false
                    }, {
                        id: 'end-datefield',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Event.ToolBar.EndTime,
                        labelWidth: 60,
                        width: 250,
                        value: new Date(),
                        editable: false,
                        allowBlank: false
                    }, {
                        xtype: 'splitbutton',
                        text: $$iPems.lang.Query,
                        glyph: 0xf005,
                        menu: {
                            //border: false,
                            //plain: true,
                            items: [
                                {
                                    text: $$iPems.lang.Event.ToolBar.Clear,
                                    glyph: 0xf023,
                                    handler: function (el, e) {
                                        Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.Event.Confirm, function (buttonId, text) {
                                            if (buttonId === 'yes') {
                                                Ext.Ajax.request({
                                                    url: '/Account/ClearEvents',
                                                    mask: new Ext.LoadMask(currentGridPanel, { msg: $$iPems.lang.AjaxHandling }),
                                                    success: function (response, options) {
                                                        var data = Ext.decode(response.responseText, true);
                                                        if (data.success)
                                                            Ext.Msg.show({ title: $$iPems.lang.SysTipTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                                        else
                                                            Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                                    }
                                                });
                                            }
                                        });
                                    }
                                }, '-',
                                {
                                    text: $$iPems.lang.Import,
                                    glyph: 0xf010,
                                    handler: function (el, e) {
                                        var params = currentStore.getProxy().extraParams;
                                        $$iPems.download({
                                            url: '/Account/DownloadEvents',
                                            params: {
                                                levels: params.levels,
                                                types: params.types,
                                                startDate: params.startDate,
                                                endDate: params.endDate
                                            }
                                        });
                                    }
                                }
                            ]
                        },
                        handler: function (el, e) {
                            var levels = Ext.getCmp('levels-multicombo').getSelectedValues(),
                                types = Ext.getCmp('types-multicombo').getSelectedValues(),
                                startDate = Ext.getCmp('begin-datefield').getRawValue(),
                                endDate = Ext.getCmp('end-datefield').getRawValue();

                            currentStore.getProxy().extraParams.levels = levels;
                            currentStore.getProxy().extraParams.types = types;
                            currentStore.getProxy().extraParams.startDate = startDate;
                            currentStore.getProxy().extraParams.endDate = endDate;
                            currentStore.loadPage(1);
                        }
                    }]
                })
            ]
        }],
        bbar: currentPagingToolbar
    });

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentGridPanel);

            //load store data
            currentStore.load();
            comboLevelStore.load();
            comboTypeStore.load();
        }
    });
})();