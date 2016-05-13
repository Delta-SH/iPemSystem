Ext.define("Ext.ux.StationMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "station.multi.treepanel",
    fieldLabel: $$iPems.lang.Component.StationName,
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    searchChange: function (me, newValue, oldValue) {
        delete me._filterData;
        delete me._filterIndex;
    },
    listeners: {
        search: function (me, field, text) {
            var picker = me.getPicker(),
                separator = '/',
                root = picker.getRootNode();

            if (Ext.isEmpty(text, false)) {
                return;
            }

            if (text.length < 2) {
                return;
            }

            if (!picker)
                return;

            if (field._filterData != null && field._filterIndex != null) {
                var index = field._filterIndex + 1;
                var paths = field._filterData;
                if (index >= paths.length) {
                    index = 0;
                }

                var nodes = Ext.Array.from(paths[index]);
                var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                picker.selectPath(path);
                field._filterIndex = index;
            } else {
                Ext.Ajax.request({
                    url: '/Component/FilterStationPath',
                    params: { text: text },
                    mask: new Ext.LoadMask({ target: picker, msg: $$iPems.lang.AjaxHandling }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            var len = data.data.length;
                            if (len > 0) {
                                var nodes = Ext.Array.from(data.data[0]);
                                var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                                picker.selectPath(path);

                                field._filterData = data.data;
                                field._filterIndex = 0;
                            }
                        } else {
                            Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        },
        syncselect: function (me, selection) {
            var picker = me.getPicker(),
                separator = '/',
                root;

            Ext.Ajax.request({
                url: '/Component/GetStationPath',
                params: { nodes: selection },
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success && picker) {
                        root = picker.getRootNode();
                        Ext.Array.each(data.data, function (item, index, all) {
                            item = Ext.Array.from(item);

                            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), item.join(separator));
                            picker.expandPath(path);
                        });
                    }
                }
            });
        }
    },
    initComponent: function () {
        var me = this,
            store = me.store;

        me.callParent(arguments);
        store.proxy.extraParams.multiselect = me.multiSelect;
        store.proxy.extraParams.leafselect = me.selectOnLeaf;
        store.load();
    },
    store: Ext.create('Ext.data.TreeStore', {
        root: {
            id: 'root',
            text: $$iPems.lang.Component.All,
            root: true
        },
        proxy: {
            type: 'ajax',
            url: '/Component/GetStations',
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

Ext.define("Ext.ux.StationTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "station.treepanel",
    fieldLabel: $$iPems.lang.Component.StationName,
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    rootVisible: true,
    multiSelect: false,
    searchVisible: true,
    searchChange: function (me, newValue, oldValue) {
        delete me._filterData;
        delete me._filterIndex;
    },
    initComponent: function () {
        var me = this,
            store = me.store;

        me.callParent(arguments);
        store.proxy.extraParams.multiselect = me.multiSelect;
        store.proxy.extraParams.leafselect = me.selectOnLeaf;
        store.load({
            scope: me,
            callback: function (records, operation, success) {
                var root = store.getRootNode();
                if (root) root.expand();

                me.setValue('root');
            }
        });
    },
    listeners: {
        search: function (me, field, text) {
            var picker = me.getPicker(),
                separator = '/',
                root = picker.getRootNode();

            if (Ext.isEmpty(text, false)) {
                return;
            }

            if (text.length < 2) {
                return;
            }

            if (!picker)
                return;

            if (field._filterData != null && field._filterIndex != null) {
                var index = field._filterIndex + 1;
                var paths = field._filterData;
                if (index >= paths.length) {
                    index = 0;
                }

                var nodes = Ext.Array.from(paths[index]);
                var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                picker.selectPath(path);
                field._filterIndex = index;
            } else {
                Ext.Ajax.request({
                    url: '/Component/FilterStationPath',
                    params: { text: text },
                    mask: new Ext.LoadMask({ target: picker, msg: $$iPems.lang.AjaxHandling }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            var len = data.data.length;
                            if (len > 0) {
                                var nodes = Ext.Array.from(data.data[0]);
                                var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                                picker.selectPath(path);

                                field._filterData = data.data;
                                field._filterIndex = 0;
                            }
                        } else {
                            Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        },
        syncselect: function (me, selection) {
            var picker = me.getPicker(),
                separator = '/',
                root;

            Ext.Ajax.request({
                url: '/Component/GetStationPath',
                params: { nodes: selection },
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success && picker) {
                        root = picker.getRootNode();
                        Ext.Array.each(data.data, function (item, index, all) {
                            item = Ext.Array.from(item);

                            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), item.join(separator));
                            picker.expandPath(path);
                        });
                    }
                }
            });
        }
    },
    store: Ext.create('Ext.data.TreeStore', {
        root: {
            id: 'root',
            text: $$iPems.lang.Component.All,
            icon: $$iPems.icons.Home,
            root: true
        },
        proxy: {
            type: 'ajax',
            url: '/Component/GetStations',
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