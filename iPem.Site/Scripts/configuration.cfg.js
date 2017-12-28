(function () {
    Ext.onReady(function () {
        var westPanel = Ext.create('Ext.tree.Panel', {
            id: 'view-west-panel',
            glyph: 0xf056,
            title: '系统层级',
            split: true,
            collapsible: true,
            collapsed: false,
            autoScroll: true,
            useArrows: false,
            rootVisible: false,
            region: 'west',
            width: 220,
            root: {
                id: 'root',
                text: '全部',
                expanded: true,
                icon: $$iPems.icons.All
            },
            viewConfig: {
                loadMask: true
            },
            store: Ext.create('Ext.data.TreeStore', {
                autoLoad: false,
                nodeParam: 'node',
                proxy: {
                    type: 'ajax',
                    url: '/Component/GetDevices',
                    reader: {
                        type: 'json',
                        successProperty: 'success',
                        messageProperty: 'message',
                        totalProperty: 'total',
                        root: 'data'
                    }
                }
            }),
            listeners: {
                select: function (me, record, item, index) {
                }
            },
            tbar: [
                    {
                        id: 'view-search-field',
                        xtype: 'textfield',
                        emptyText: '请输入筛选条件...',
                        flex: 1,
                        listeners: {
                            change: function (me, newValue, oldValue, eOpts) {
                                delete me._filterData;
                                delete me._filterIndex;
                            }
                        }
                    },
                    {
                        id: 'view-search-button',
                        xtype: 'button',
                        glyph: 0xf005,
                        handler: function () {
                            var tree = Ext.getCmp('view-west-panel'),
                                search = Ext.getCmp('view-search-field'),
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
                                    Ext.Msg.show({ title: '系统提示', msg: '搜索完毕', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                }

                                $$iPems.selectNodePath(tree, paths[index]);
                                search._filterIndex = index;
                            } else {
                                Ext.Ajax.request({
                                    url: '/Component/FilterRoomPath',
                                    params: { text: text },
                                    mask: new Ext.LoadMask({ target: tree, msg: '正在处理...' }),
                                    success: function (response, options) {
                                        var data = Ext.decode(response.responseText, true);
                                        if (data.success) {
                                            var len = data.data.length;
                                            if (len > 0) {
                                                $$iPems.selectNodePath(tree, data.data[0]);
                                                search._filterData = data.data;
                                                search._filterIndex = 0;
                                            } else {
                                                Ext.Msg.show({ title: '系统提示', msg: Ext.String.format('未找到指定内容:<br/>{0}', text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                            }
                                        } else {
                                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                        }
                                    }
                                });
                            }
                        }
                    }
            ]
        });
        var centerPanel = Ext.create('Ext.panel.Panel', {
            region: 'center',
            glyph: 0xf092,
            title: '组态配置',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            items: [
                Ext.create('Ext.ux.IFrame', {
                    flex: 1,
                    loadMask: '正在处理...',
                    src: '/WGViewEditer/WGView.Editer.xbap'
                })
            ]
        });

        var viewport = Ext.create('Ext.panel.Panel', {
            region: 'center',
            layout: 'border',
            border: false,
            items: [westPanel, centerPanel]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(viewport);
        }
    });
})();