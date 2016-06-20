Ext.define("Ext.ux.RoomMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomMultiPicker",
    fieldLabel: $$iPems.lang.Component.RoomName,
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.RoomTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "RoomPicker",
    fieldLabel: $$iPems.lang.Component.RoomName,
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetRooms';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetRoomPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});