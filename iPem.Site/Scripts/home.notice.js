(function () {
    Ext.define('NoticeModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'id', type: 'string' },
            { name: 'title', type: 'string' },
            { name: 'content', type: 'string' },
            { name: 'created', type: 'string' },
            { name: 'readed', type: 'boolean' },
            { name: 'readtime', type: 'string' }
        ],
        idProperty: 'id'
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'NoticeModel',
        proxy: {
            type: 'ajax',
            url: '/Home/GetNotices',
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
                listModel: 'all' //all,readed,unread
            },
            simpleSortMode: true
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var refresh = function () {
        var unread = Ext.getCmp('unread'),
            readed = Ext.getCmp('readed');

        var cmd = 'all';
        if (unread.pressed)
            cmd = 'unread';
        else if (readed.pressed)
            cmd = 'readed';

        currentStore.getProxy().extraParams.listModel = cmd;
        currentStore.loadPage(1);
    };

    var detailWnd = Ext.create('Ext.window.Window', {
        title: 'Detail',
        glyph: 0xf024,
        height: 250,
        width: 400,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        bodyPadding: 10,
        layout: 'form',
        items: [{
            itemId: 'notice-detail',
            xtype: 'label',
            text: ''
        }, {
            xtype: 'component',
            height: 5
        }, {
            itemId: 'notice-created',
            xtype: 'label',
            text: ''
        }],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                detailWnd.hide();
            }
        }]
    });

    var currentGridPanel = Ext.create('Ext.grid.Panel', {
        glyph: 0xf025,
        title: '系统消息列表',
        region: 'center',
        store: currentStore,
        columnLines: true,
        selType: 'checkboxmodel',
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
            text: '消息标题',
            dataIndex: 'title',
            flex: 1,
            align: 'left',
            sortable: true,
            renderer: function (value, metaData, record, rowIndex, colIndex, store, view) {
                if (!record.raw.readed) {
                    return '<span style="font-weight:bold;">' + value + '</span>';
                }

                return value;
            }
        }, {
            text: '消息日期',
            dataIndex: 'created',
            width: 200,
            align: 'center',
            sortable: true,
            renderer: function (value, metaData, record, rowIndex, colIndex, store, view) {
                if (!record.raw.readed) {
                    return '<span style="font-weight:bold;">' + value + '</span>';
                }

                return value;
            }
        }, {
            xtype: 'actioncolumn',
            width: 50,
            align: 'center',
            menuDisabled: true,
            menuText: '操作',
            text: '操作',
            items: [{
                iconCls: 'x-cell-icon x-icon-detail',
                handler: function (grid, rowIndex, colIndex) {
                    var record = grid.getStore().getAt(rowIndex);
                    if (Ext.isEmpty(record)) return false;
                    
                    showNotice(record);
                }
            }]
        }],
        tbar: Ext.create('Ext.toolbar.Toolbar', {
            items: [{
                id: 'unread',
                xtype: 'button',
                text: '未读消息',
                enableToggle: true,
                glyph: 0xf025,
                toggleGroup:'notice',
                handler: function (el, e) {
                    refresh();
                }
            }, '-', {
                id: 'readed',
                xtype: 'button',
                text: '已读消息',
                enableToggle: true,
                toggleGroup: 'notice',
                glyph: 0xf024,
                handler: function (el, e) {
                    refresh();
                }
            }, '-', {
                xtype: 'button',
                text: '批量操作',
                glyph: 0xf008,
                menu: Ext.create('Ext.menu.Menu', {
                    plain: true,
                    border:false,
                    items: [
                        {
                            text: '标记为已读',
                            glyph: 0xf024,
                            handler: function () {
                                var models = currentGridPanel.getSelectionModel().getSelection();
                                var notices = [];
                                Ext.Array.each(models, function (el, index) {
                                    Ext.Array.push(notices,el.data.id);
                                });

                                if (notices.length > 0)
                                    statusNotice(notices, 'readed');
                            }
                        }, '-' ,{
                            text: '标记为未读',
                            glyph: 0xf025,
                            handler: function () {
                                var models = currentGridPanel.getSelectionModel().getSelection();
                                var notices = [];
                                Ext.Array.each(models, function (el, index) {
                                    Ext.Array.push(notices, el.data.id);
                                });

                                if (notices.length > 0)
                                    statusNotice(notices, 'unread');
                            }
                        }
                    ]
                })
            }]
        }),
        bbar: currentPagingToolbar,
        listeners: {
            itemdblclick: function (view, record, item, index, e) {
                showNotice(record);
            }
        }
    });

    var showNotice = function (record) {
        Ext.Ajax.request({
            url: '/Home/SetNotices',
            Method: 'POST',
            mask: Ext.create('Ext.LoadMask', {
                target: currentGridPanel,
                msg: '正在处理...'
            }),
            params: {
                notices: [record.raw.id],
                status: 'readed'
            },
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var notice = detailWnd.getComponent('notice-detail');
                    var created = detailWnd.getComponent('notice-created');

                    notice.setText(record.raw.content);
                    created.setText(record.raw.created);
                    detailWnd.setTitle(record.raw.title);
                    detailWnd.show();
                    currentPagingToolbar.doRefresh();
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    var statusNotice = function (notices,status) {
        Ext.Ajax.request({
            url: '/Home/SetNotices',
            Method: 'POST',
            mask: Ext.create('Ext.LoadMask', {
                target: currentGridPanel,
                msg: '正在处理...'
            }),
            params: {
                notices: notices,
                status: status
            },
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    currentPagingToolbar.doRefresh();
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentGridPanel);

            //load store data
            currentStore.load();
        }
    });
})();